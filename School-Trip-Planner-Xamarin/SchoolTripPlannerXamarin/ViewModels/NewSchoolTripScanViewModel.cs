using Akavache;
using AutoMapper;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class NewSchoolTripScanViewModel : ViewModelBase
    {
        private ScanDTO _scan = new ScanDTO();
        private SchoolTripDTO _selectedSchoolTrip;
        private readonly INavigationService _navigationService;
        private readonly IScanDataService _scanDataService;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;
        public ICommand CancelToolbarItemCommand { get; }
        public ICommand AddToolbarItemCommand { get; }

        public ScanDTO Scan
        {
            get => _scan;
            set => Set(ref _scan, value);
        }

        public NewSchoolTripScanViewModel(IDialogService dialogService, INavigationService navigationService, IScanDataService scanDataService, IMapper mapper)
        {
            _scanDataService = scanDataService;
            _navigationService = navigationService;
            _mapper = mapper;
            _dialogService = dialogService;
            CancelToolbarItemCommand = new Command(CancelAddNewScan);
            AddToolbarItemCommand = new Command(AddNewScan);
        }

        private async void CancelAddNewScan()
        {
            await _navigationService.NavigateBackAsync();
        }

        public override async Task InitializeAsync(object parameter)
        {
            _selectedSchoolTrip = parameter as SchoolTripDTO;
            await base.InitializeAsync(parameter);
        }

        private async void AddNewScan()
        {
            try
            {
                var scanSchoolTripId = _selectedSchoolTrip.Id;

                Scan.ScanToddlers = new List<ScanToddlerDTO>();
                Scan.SchoolTripId = scanSchoolTripId;
                foreach (var schoolTripToddler in _selectedSchoolTrip.SchoolTripToddlers)
                {
                    Scan.ScanToddlers.Add(new ScanToddlerDTO {Toddler = schoolTripToddler.Toddler, ToddlerId = schoolTripToddler.ToddlerId, ToddlerIsScanned = false});
                }

                var scan = _mapper.Map<Scan>(Scan);
                await _scanDataService.PostScan(scan);
                await BlobCache.LocalMachine.Invalidate(CacheNameConstants.SchoolTripById + scanSchoolTripId);
                MessagingCenter.Send(this, MessagingConstants.AddSchoolTripScan, Scan);
                await _navigationService.NavigateBackAsync();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}