using AutoMapper;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class SchoolTripDetailsViewModel : ViewModelBase
    {
        private SchoolTripDTO _selectedSchoolTrip;
        private ScanDTO _selectedScan;
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

        public SchoolTripDetailsViewModel(IMapper mapper, INavigationService navigationService, ISchoolTripDataService schoolTripDataService)
        {
            Title = "Schooluitstap Details";
            _navigationService = navigationService;
            _schoolTripDataService = schoolTripDataService;
            _mapper = mapper;
            AddScanCommand = new Command(NavigateToNewSchoolTripScanPage);
            ScanToddlerSelectedCommand = new Command(ScanToddlerSelected);

//            MessagingCenter.Subscribe<NewSchoolTripScanViewModel, SchoolTripDTO>(this, MessagingConstants.AddSchoolTripScan,
//                (sender, schoolTripWithNewScan) => AddSchoolTripScan(sender, schoolTripWithNewScan));
        }

//        private void Appearing(object sender, EventArgs e)
//        {
//            Debug.WriteLine("SelectedSchoolTrip: " + SelectedSchoolTrip);
//        }

        private void ScanToddlerSelected()
        {
            if (SelectedScanToddler == null)
                return;
            SelectedScanToddler.Scan = SelectedScan;
            SelectedScanToddler.ScanId = SelectedScan.Id;
            _navigationService.NavigateToModallyAsync(typeof(ScanToddlerDetailsViewModel), SelectedScanToddler);

            SelectedScanToddler = null;
        }

//        private void AddSchoolTripScan(NewSchoolTripScanViewModel sender, SchoolTripDTO schoolTripWithNewScan)
//        {
////            SelectedSchoolTrip = schoolTripWithNewScan;
//        }

        public override async Task InitializeAsync(object parameter)
        {
            try
            {
                var schoolTrip = await _schoolTripDataService.GetSchoolTripByIdAsync((long) parameter);
                SelectedSchoolTrip = _mapper.Map<SchoolTripDTO>(schoolTrip);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            var currentPage = _navigationService.CurrentPage;
//            currentPage.Disappearing += Disappearing;
//            currentPage.Appearing += Appearing;
        }

//        private void Disappearing(object sender, EventArgs eventArgs)
//        {
//            MessagingCenter.Unsubscribe<NewSchoolTripScanViewModel, ScanDTO>(this, MessagingConstants.AddSchoolTripScan);
//        }

        public async void NavigateToNewSchoolTripScanPage()
        {
            await _navigationService.NavigateToModallyAsync(typeof(NewSchoolTripScanViewModel), SelectedSchoolTrip);
        }
    }
}