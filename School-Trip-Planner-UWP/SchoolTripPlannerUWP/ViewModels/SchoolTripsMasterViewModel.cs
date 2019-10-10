using AutoMapper;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Controls;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.DTOs;
using SchoolTripPlannerUWP.Core.Models;
using SchoolTripPlannerUWP.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace SchoolTripPlannerUWP.ViewModels
{
    public class SchoolTripsMasterViewModel : BaseViewModel
    {
        private SchoolTripDTO _selectedSchoolTrip;
        private ObservableCollection<SchoolTripDTO> _schoolTrips = new ObservableCollection<SchoolTripDTO>();
        private readonly ISchoolTripDataService _schoolTripDataService;
        private readonly IMapper _mapper;
        public RelayCommand<MasterDetailsView> MasterDetailsViewLoadedCommand { get; private set; }

        public ObservableCollection<SchoolTripDTO> SchoolTrips
        {
            get => _schoolTrips;
            private set => Set(ref _schoolTrips, value);
        }

        public SchoolTripDTO SelectedSchoolTrip
        {
            get => _selectedSchoolTrip;
            set => Set(ref _selectedSchoolTrip, value, true);
        }

        public SchoolTripsMasterViewModel(ISchoolTripDataService schoolTripDataService, ITeacherDataService teacherDataService, IMapper mapper)
        {
            _schoolTripDataService = schoolTripDataService;
            _mapper = mapper;

            MasterDetailsViewLoadedCommand = new RelayCommand<MasterDetailsView>(MasterDetailsViewLoaded);
        }

        private async void MasterDetailsViewLoaded(MasterDetailsView masterDetailsView)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SchoolTrips.Clear();

                var schoolTrips = await _schoolTripDataService.GetAllSchoolTripsAsync();
                SchoolTrips = _mapper.Map<IEnumerable<SchoolTrip>, IEnumerable<SchoolTripDTO>>(schoolTrips)
                    .OrderBy(s => s.CurrentState)
                    .ThenBy(s => s.StartDateTime.Subtract(DateTime.UtcNow).Duration())
                    .ToObservableCollection();

                if (masterDetailsView.ViewState == MasterDetailsViewState.Both)
                {
                    if (SchoolTrips != null)
                    {
                        SelectedSchoolTrip = SchoolTrips.First();
                    }
                }
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

        public override async Task NavigatedFromAsync(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            SelectedSchoolTrip = null;
            await base.NavigatedFromAsync(sender, navigatingCancelEventArgs);
        }
    }
}
