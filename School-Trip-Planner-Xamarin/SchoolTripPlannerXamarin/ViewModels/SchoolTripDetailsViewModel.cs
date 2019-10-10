using AutoMapper;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs;
using SchoolTripPlannerXamarin.Extenstions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class SchoolTripDetailsViewModel : ViewModelBase
    {
        private SchoolTripDTO _selectedSchoolTrip = new SchoolTripDTO();
        private ScanDTO _selectedScan;
        private ObservableCollection<ScanDTO> _scans;
        private ScanToddlerDTO _selectedScanToddler;
        private readonly ISchoolTripDataService _schoolTripDataService;
        private readonly INavigationService _navigationService;
        private readonly IMapper _mapper;
        public ICommand AddScanCommand { get; }
        public ICommand ScanToddlerSelectedCommand { get; }

        public ScanDTO SelectedScan
        {
            get => _selectedScan;
            set => Set(ref _selectedScan, value);
        }

        public SchoolTripDTO SelectedSchoolTrip
        {
            get => _selectedSchoolTrip;
            set => Set(ref _selectedSchoolTrip, value);
        }

        public ScanToddlerDTO SelectedScanToddler
        {
            get => _selectedScanToddler;
            set => Set(ref _selectedScanToddler, value);
        }

        public ObservableCollection<ScanDTO> Scans
        {
            get => _scans;
            set => Set(ref _scans, value);
        }

        public SchoolTripDetailsViewModel(IMapper mapper, INavigationService navigationService, ISchoolTripDataService schoolTripDataService)
        {
            Title = "Schooluitstap Details";
            _navigationService = navigationService;
            _schoolTripDataService = schoolTripDataService;
            _mapper = mapper;
            AddScanCommand = new Command(NavigateToNewSchoolTripScanPage);
            ScanToddlerSelectedCommand = new Command<ListView>(ScanToddlerSelected);
            MessagingCenter.Subscribe<NewSchoolTripScanViewModel, ScanDTO>(this, MessagingConstants.AddSchoolTripScan, (sender, newScan) => AddSchoolTripScan(sender, newScan));
        }

        private void ScanToddlerSelected(ListView listView)
        {
            var scanToddler = listView.SelectedItem as ScanToddlerDTO;
            if (scanToddler == null)
                return;
            scanToddler.Scan = SelectedScan;
            _navigationService.NavigateToModallyAsync(typeof(ScanToddlerDetailsViewModel), scanToddler);

            SelectedScanToddler = null;
        }

        private void AddSchoolTripScan(NewSchoolTripScanViewModel sender, ScanDTO createdScan)
        {
            try
            {
                Scans.Add(createdScan);
                SelectedSchoolTrip.Scans.Add(createdScan);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            try
            {
                var schoolTrip = await _schoolTripDataService.GetSchoolTripByIdAsync((long) parameter);
                SelectedSchoolTrip = _mapper.Map<SchoolTripDTO>(schoolTrip);

                Scans = SelectedSchoolTrip.Scans.ToObservableCollection();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            var currentPage = _navigationService.CurrentPage;
            currentPage.Disappearing += Disappearing;
        }

        private void Disappearing(object sender, EventArgs eventArgs)
        {
            MessagingCenter.Unsubscribe<NewSchoolTripScanViewModel, ScanDTO>(this, MessagingConstants.AddSchoolTripScan);
        }

        public async void NavigateToNewSchoolTripScanPage()
        {
            await _navigationService.NavigateToModallyAsync(typeof(NewSchoolTripScanViewModel), SelectedSchoolTrip);
        }
    }
}