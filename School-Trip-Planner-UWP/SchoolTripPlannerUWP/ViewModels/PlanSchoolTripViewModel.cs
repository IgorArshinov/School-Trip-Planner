using Akavache;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using SchoolTripPlannerUWP.Core.Constants;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.DTOs;
using SchoolTripPlannerUWP.Core.Models;
using SchoolTripPlannerUWP.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SchoolTripPlannerUWP.ViewModels
{
    public class PlanSchoolTripViewModel : BaseViewModel
    {
        public SchoolTripDTO SchoolTrip
        {
            get => _schoolTrip;
            set => Set(ref _schoolTrip, value);
        }

        private ObservableCollection<ToddlerDTO> _toddlers;
        private ObservableCollection<TeacherDTO> _teachers;
        private readonly IToddlerDataService _toddlerDataService;
        private readonly ITeacherDataService _teacherDataService;
        private readonly ISchoolTripDataService _schoolTripDataService;
        private readonly IMapper _mapper;
        private TeacherDTO _teacher;

        private SchoolTripDTO _schoolTrip = new SchoolTripDTO
        {
            StartDate = DateTimeOffset.Now,
            StartTime = DateTime.Now.TimeOfDay,
            EndDate = DateTimeOffset.Now,
            EndTime = DateTime.Now.TimeOfDay,
        };

        public RelayCommand PlanButtonCommand { get; }
        public RelayCommand<ListView> UpdateSelectedToddlersCommand { get; }
        public bool PlanButtonEnabled { get; }
        public List<ToddlerDTO> SelectedToddlers { get; set; } = new List<ToddlerDTO>();

        public ObservableCollection<ToddlerDTO> Toddlers
        {
            get => _toddlers;
            private set => Set(ref _toddlers, value);
        }

        public ObservableCollection<TeacherDTO> Teachers
        {
            get => _teachers;
            private set => Set(ref _teachers, value);
        }

        public TeacherDTO Teacher
        {
            get => _teacher;
            set
            {
                if (_teacher != value && value != null)
                {
                    SchoolTrip.TeacherId = value.Id;
                    _teacher = value;
                }
            }
        }

        public PlanSchoolTripViewModel(IToddlerDataService toddlerDataService, ITeacherDataService teacherDataService, ISchoolTripDataService schoolTripDataService, IMapper mapper)
        {
            _toddlerDataService = toddlerDataService;
            _teacherDataService = teacherDataService;
            _schoolTripDataService = schoolTripDataService;
            _mapper = mapper;
            PlanButtonCommand = new RelayCommand(PlanSchoolTrip);
            UpdateSelectedToddlersCommand = new RelayCommand<ListView>(UpdateSelectedToddlers);
            PlanButtonEnabled = true;
        }

        public override async Task NavigatedToAsync(object sender, NavigationEventArgs navigationEventArgs)
        {
            try
            {
                var toddlers = await _toddlerDataService.GetAllToddlersAsync();
                Toddlers = _mapper.Map<IEnumerable<Toddler>, IEnumerable<ToddlerDTO>>(toddlers).ToObservableCollection();

                var teachers = await _teacherDataService.GetAllTeachersAsync();
                Teachers = _mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers).ToObservableCollection();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void UpdateSelectedToddlers(ListView listView)
        {
            SelectedToddlers = listView.SelectedItems.Cast<ToddlerDTO>().ToList();
        }

        private async void PlanSchoolTrip()
        {
            try
            {
                var schoolTrip = _mapper.Map<SchoolTrip>(SchoolTrip);
                schoolTrip.SchoolTripToddlers = new List<SchoolTripToddler>();
                foreach (var toddler in SelectedToddlers)
                {
                    schoolTrip.SchoolTripToddlers.Add(new SchoolTripToddler
                    {
                        ToddlerId = toddler.Id,
                        SchoolTripId = schoolTrip.Id
                    });
                }

                await _schoolTripDataService.PostSchoolTrip(schoolTrip);
                await BlobCache.LocalMachine.Invalidate(CacheNameConstants.AllSchoolTrips);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
