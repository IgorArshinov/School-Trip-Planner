using AutoMapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.DTOs;

namespace SchoolTripPlannerUWP.ViewModels
{
    public class SchoolTripsDetailsViewModel : BaseViewModel
    {
        private SchoolTripDTO _selectedSchoolTrip;
        private readonly ISchoolTripDataService _schoolTripDataService;
        private readonly IMapper _mapper;

        public SchoolTripDTO SelectedSchoolTrip
        {
            get => _selectedSchoolTrip;
            set => Set(ref _selectedSchoolTrip, value);
        }

        public RelayCommand OnLoadedCommand { get; set; }
        public RelayCommand OnUnloadedCommand { get; set; }

        public SchoolTripsDetailsViewModel(ISchoolTripDataService schoolTripDataService, IMapper mapper)
        {
            _schoolTripDataService = schoolTripDataService;
            _mapper = mapper;
            OnLoadedCommand = new RelayCommand(OnLoaded);
            OnUnloadedCommand = new RelayCommand(OnUnloaded);
        }

        private async void OnSchoolTripChangedAsync(PropertyChangedMessage<SchoolTripDTO> propertyDetails)
        {
            if (propertyDetails.PropertyName == "SelectedSchoolTrip" && propertyDetails.NewValue != null)
            {
                var selectedId = propertyDetails.NewValue.Id;

                try
                {
                    var schoolTrip = await _schoolTripDataService.GetSchoolTripByIdAsync(selectedId);

                    SelectedSchoolTrip = _mapper.Map<SchoolTripDTO>(schoolTrip);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister<PropertyChangedMessage<SchoolTripDTO>>(this);
        }

        public void OnUnloaded()
        {
            base.Cleanup();
        }

        public void OnLoaded()
        {
            Messenger.Default.Register<PropertyChangedMessage<SchoolTripDTO>>(this, OnSchoolTripChangedAsync);
        }
    }
}
