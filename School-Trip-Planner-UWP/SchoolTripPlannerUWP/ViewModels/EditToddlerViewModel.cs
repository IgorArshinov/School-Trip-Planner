using Akavache;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using SchoolTripPlannerUWP.Converters;
using SchoolTripPlannerUWP.Core.Constants;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.DTOs;
using SchoolTripPlannerUWP.Core.Models;
using SchoolTripPlannerUWP.Extensions;
using SchoolTripPlannerUWP.Helpers;
using SchoolTripPlannerUWP.Views.Printing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing;
using ZXing.Mobile;

namespace SchoolTripPlannerUWP.ViewModels
{
    public class EditToddlerViewModel : BaseViewModel
    {
        private ObservableCollection<ClassDTO> _classes;
        private ObservableCollection<ToddlerDTO> _toddlers = new ObservableCollection<ToddlerDTO>();
        private ToddlerDTO _toddler;
        private ToddlerDTO _toddlerToEdit = new ToddlerDTO();
        private readonly IClassDataService _classDataService;
        private readonly IToddlerDataService _toddlerDataService;
        private PrintHelper _printHelper;
        private readonly IMapper _mapper;
        public RelayCommand<TextBox> GenerateQrCodeImageCommand { get; }
        public RelayCommand PrintButtonCommand { get; }
        public Boolean PrinterIsSupported { get; private set; }
        public RelayCommand EditButtonCommand { get; }

        public ObservableCollection<ClassDTO> Classes
        {
            get => _classes;
            private set => Set(ref _classes, value);
        }

        public ObservableCollection<ToddlerDTO> Toddlers
        {
            get => _toddlers;
            set => Set(ref _toddlers, value);
        }

        public ToddlerDTO Toddler
        {
            get => _toddler;
            set
            {
                if (_toddler != value && value != null)
                {
                    _toddler = value;

                    ToddlerToEdit = value.Clone() as ToddlerDTO;
                    RaisePropertyChanged();
                }
            }
        }

        public ToddlerDTO ToddlerToEdit
        {
            get => _toddlerToEdit;
            set => Set(ref _toddlerToEdit, value);
        }

        public EditToddlerViewModel(IClassDataService classDataService, IToddlerDataService toddlerDataService, IMapper mapper)
        {
            _classDataService = classDataService;
            _toddlerDataService = toddlerDataService;
            _mapper = mapper;
            GenerateQrCodeImageCommand = new RelayCommand<TextBox>(GenerateQrCodeImage);
            PrintButtonCommand = new RelayCommand(ShowPrintUi);
            EditButtonCommand = new RelayCommand(EditToddler);
        }

        private async void EditToddler()
        {
            try
            {
                var toddler = _mapper.Map<Toddler>(ToddlerToEdit);
                await _toddlerDataService.PutToddler(toddler.Id, toddler);
                await BlobCache.LocalMachine.Invalidate(CacheNameConstants.AllToddlers);
                await BlobCache.LocalMachine.Invalidate(CacheNameConstants.AllSchoolTrips);
                Toddlers[Toddlers.IndexOf(Toddler)] = ToddlerToEdit;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private async Task GetAllToddlersAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Toddlers.Clear();
                var toddlers = await _toddlerDataService.GetAllToddlersAsync();
                Toddlers = _mapper.Map<IEnumerable<Toddler>, IEnumerable<ToddlerDTO>>(toddlers).ToObservableCollection();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ShowPrintUi()
        {
            await _printHelper.ShowPrintUIAsync();
        }

        private void GenerateQrCodeImage(TextBox textBox)
        {
            ToTitleCaseConverter.StaticConvert(textBox);
            if (ToddlerToEdit == null || ToddlerToEdit.QrCode.Equals(string.Empty))
            {
                if (ToddlerToEdit != null) ToddlerToEdit.QrCodeImageSource = null;
                return;
            }

            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 250,
                    Height = 250
                }
            };

            ToddlerToEdit.QrCodeImageSource = barcodeWriter.Write(ToddlerToEdit.QrCode);
            //            await ScanBitmap(QrCodeImageSource);
        }

        public override async Task NavigatedToAsync(object sender, NavigationEventArgs navigationEventArgs)
        {
            if (!PrintManager.IsSupported())
            {
                PrinterIsSupported = false;
            }

            PrinterIsSupported = true;

            _printHelper = new PrintHelper(navigationEventArgs.Content as Page);
            _printHelper.RegisterForPrinting();
            _printHelper.PreparePrintContent(new PrintEditedToddler());

            try
            {
                var classes = await _classDataService.GetAllClassesAsync();
                Classes = _mapper.Map<IEnumerable<Class>, IEnumerable<ClassDTO>>(classes).ToObservableCollection();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            await GetAllToddlersAsync();
        }

        public override async Task NavigatedFromAsync(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            ToddlerToEdit = null;
            _printHelper?.UnregisterForPrinting();
            await base.NavigatedFromAsync(sender, navigatingCancelEventArgs);
        }

        private async Task ScanBitmap(WriteableBitmap writeableBmp)
        {
            var barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options =
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat> {BarcodeFormat.QR_CODE}
                }
            };

            var pixels = await ConvertBitmapToByteArray(writeableBmp);

            barcodeReader.Decode(new RGBLuminanceSource(pixels, writeableBmp.PixelWidth, writeableBmp.PixelHeight, RGBLuminanceSource.BitmapFormat.BGRA32));
        }

        private async Task<byte[]> ConvertBitmapToByteArray(WriteableBitmap bitmap)
        {
            var pixelStream = bitmap.PixelBuffer.AsStream();
            var pixels = new byte[pixelStream.Length];

            await pixelStream.ReadAsync(pixels, 0, pixels.Length);
            return pixels;
        }
    }
}
