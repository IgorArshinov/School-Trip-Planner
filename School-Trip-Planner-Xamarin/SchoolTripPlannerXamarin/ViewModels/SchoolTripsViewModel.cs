using AutoMapper;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs;
using SchoolTripPlannerXamarin.Extenstions;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class SchoolTripsViewModel : ViewModelBase
    {
        private readonly ISchoolTripDataService _schoolTripDataService;
        private ObservableCollection<SchoolTripDTO> _schoolTrips = new ObservableCollection<SchoolTripDTO>();
        private readonly ISettingsService _settingsService;
        private SchoolTripDTO _selectedSchoolTrip;
        private readonly INavigationService _navigationService;
        private readonly IMapper _mapper;
        public ICommand RefreshSchoolTripsCommand { get; }
        public ICommand SchoolTripSelectedCommand { get; }
        public ICommand PageAppearingCommand { get; }

        public ObservableCollection<SchoolTripDTO> SchoolTrips
        {
            get => _schoolTrips;
            set => Set(ref _schoolTrips, value);
        }

        public SchoolTripDTO SelectedSchoolTrip
        {
            get => _selectedSchoolTrip;
            set => Set(ref _selectedSchoolTrip, value);
        }

        public SchoolTripsViewModel(IMapper mapper, ISchoolTripDataService schoolTripDataService, ISettingsService settingsService, INavigationService navigationService)
        {
            Title = "Schooluitstappen";
            _schoolTripDataService = schoolTripDataService;
            _settingsService = settingsService;
            _navigationService = navigationService;
            _mapper = mapper;
            PageAppearingCommand = new Command(PageAppearingAsync);
            RefreshSchoolTripsCommand = new Command(ExecuteRefreshSchoolTripsCommand);
            SchoolTripSelectedCommand = new Command<ListView>(SchoolTripSelected);
        }

        private async void PageAppearingAsync()
        {
            await GetSchoolTrips();
        }

        public async void SchoolTripSelected(ListView listView)
        {
            if (SelectedSchoolTrip == null)
                return;

            await _navigationService.NavigateToAsync(typeof(SchoolTripDetailsViewModel), SelectedSchoolTrip.Id);

            SelectedSchoolTrip = null;
        }

        private async void ExecuteRefreshSchoolTripsCommand()
        {
            await GetSchoolTrips();
        }

        private async Task GetSchoolTrips()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SchoolTrips.Clear();
                long.TryParse(_settingsService.TeacherIdSetting, out var id);
                var schoolTrips = await _schoolTripDataService.GetAllSchoolTripsByTeacherIdAsync(id);
                SchoolTrips = _mapper.Map<IEnumerable<SchoolTrip>, IEnumerable<SchoolTripDTO>>(schoolTrips)
                    .OrderBy(s => s.CurrentState)
                    .ThenBy(s => s.StartDateTime.Subtract(DateTime.UtcNow).Duration())
                    .ToObservableCollection();
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
    }
}