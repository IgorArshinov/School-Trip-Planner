using Akavache;
using AutoMapper;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class ScanToddlerDetailsViewModel : ViewModelBase
    {
        private ScanToddlerDTO _scanToddler;
        private readonly IDialogService _dialogService;
        private readonly IScanToddlerService _scanToddlerDataService;
        private readonly INavigationService _navigationService;
        private readonly IMapper _mapper;
        public ICommand ScanCommand { get; }
        public ICommand CancelToolbarItemCommand { get; }

        public ScanToddlerDTO ScanToddler
        {
            get => _scanToddler;
            set => Set(ref _scanToddler, value);
        }

        public ScanToddlerDetailsViewModel(IMapper mapper, IScanToddlerService scanToddlerDataService, IDialogService dialogService, INavigationService navigationService)
        {
            _scanToddlerDataService = scanToddlerDataService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _mapper = mapper;
            CancelToolbarItemCommand = new Command(CancelAddNewScan);
            ScanCommand = new Command(ScanQrCode);
        }

        private async void CancelAddNewScan()
        {
            await _navigationService.NavigateBackModallyAsync();
        }

        private async void ScanQrCode()
        {
            if (!ScanToddler.ToddlerIsScanned)
            {
                try
                {
                    ZXingScannerPage scanPage = new ZXingScannerPage();
                    scanPage.OnScanResult += async (result) =>
                    {
                        scanPage.IsScanning = false;
                        if (result.Text.Equals(ScanToddler.Toddler.QrCode))
                        {
                            ScanToddler.ToddlerIsScanned = true;

                            var scanToddler = _mapper.Map<ScanToddler>(ScanToddler);
                            await _scanToddlerDataService.UpdateScanToddler(scanToddler.ToddlerId, scanToddler);
                            await BlobCache.LocalMachine.Invalidate(CacheNameConstants.SchoolTripById + ScanToddler.Scan.SchoolTripId);
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _navigationService.NavigateBackModallyAsync();
                            await _dialogService.ShowAlert($"Barcode is {result.Text}.\nKleuter is gescand.", DialogConstants.Info, DialogConstants.Ok);
                            await _navigationService.NavigateBackModallyAsync();
                        });
                    };

                    await _navigationService.NavigateToPageModallyAsync(scanPage);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            var selectedScanToddler = parameter as ScanToddlerDTO;
            ScanToddler = selectedScanToddler;
            await base.InitializeAsync(parameter);
        }
    }
}