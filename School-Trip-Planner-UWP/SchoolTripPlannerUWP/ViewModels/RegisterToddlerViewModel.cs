using Akavache;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
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
    public class RegisterToddlerViewModel : BaseViewModel
    {
        private ObservableCollection<ClassDTO> _classes;
        private readonly IClassDataService _classDataService;
        private readonly IToddlerDataService _toddlerDataService;
        private PrintHelper _printHelper;
        private ClassDTO _class;
        private ToddlerDTO _toddler = new ToddlerDTO();
        private readonly IMapper _mapper;
        public RelayCommand GenerateQrCodeImageCommand { get; }
        public RelayCommand PrintButtonCommand { get; }
        public RelayCommand RegisterButtonCommand { get; }
        public Boolean PrinterIsSupported { get; private set; }
        public Boolean CanRegister { get; }

        public ObservableCollection<ClassDTO> Classes
        {
            get => _classes;
            private set => Set(ref _classes, value);
        }

        public ClassDTO Class
        {
            get => _class;
            set => Set(ref _class, value);
        }

        public ToddlerDTO Toddler
        {
            get => _toddler;
            set => Set(ref _toddler, value);
        }

        public RegisterToddlerViewModel(IClassDataService classDataService, IToddlerDataService toddlerDataService, IMapper mapper)
        {
            _mapper = mapper;
            _classDataService = classDataService;
            _toddlerDataService = toddlerDataService;
            CanRegister = true;
            GenerateQrCodeImageCommand = new RelayCommand(GenerateQrCodeImage);
            PrintButtonCommand = new RelayCommand(ShowPrintUi);
            RegisterButtonCommand = new RelayCommand(RegisterToddler);
        }

        private async void RegisterToddler()
        {
            try
            {
                var toddler = _mapper.Map<Toddler>(Toddler);
                await _toddlerDataService.PostToddler(toddler);
                await BlobCache.LocalMachine.Invalidate(CacheNameConstants.AllToddlers);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private async void ShowPrintUi()
        {
            await _printHelper.ShowPrintUIAsync();
        }

        private void GenerateQrCodeImage()
        {
            if (Toddler.QrCode.Equals(string.Empty))
            {
                Toddler.QrCodeImageSource = null;
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

            Toddler.QrCodeImageSource = barcodeWriter.Write(Toddler.QrCode);

            var result = ScanBitmap(Toddler.QrCodeImageSource);
        }

        private async Task<Result> ScanBitmap(WriteableBitmap writeableBmp)
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

            var result = barcodeReader.Decode(new RGBLuminanceSource(pixels, writeableBmp.PixelWidth, writeableBmp.PixelHeight, RGBLuminanceSource.BitmapFormat.BGRA32));

            return result;
        }

        private async Task<byte[]> ConvertBitmapToByteArray(WriteableBitmap bitmap)
        {
            var pixelStream = bitmap.PixelBuffer.AsStream();
            var pixels = new byte[pixelStream.Length];

            await pixelStream.ReadAsync(pixels, 0, pixels.Length);
            return pixels;
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
            _printHelper.PreparePrintContent(new PrintRegisteredToddler());

            try
            {
                var classes = await _classDataService.GetAllClassesAsync();
                Classes = _mapper.Map<IEnumerable<Class>, IEnumerable<ClassDTO>>(classes).ToObservableCollection();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        public override async Task NavigatedFromAsync(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            _printHelper?.UnregisterForPrinting();
            await base.NavigatedFromAsync(sender, navigatingCancelEventArgs);
        }
    }
}
