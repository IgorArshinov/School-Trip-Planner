using Akavache;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.Exceptions;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class TeacherViewModel : ViewModelBase
    {
        private Teacher _teacher = new Teacher();
        private readonly ISettingsService _settingsService;
        public ICommand EditButtonCommand { get; }
        private readonly IConnectionService _connectionService;
        private readonly IDialogService _dialogService;
        private readonly ITeacherSQLDataService _teacherSQLDataService;
        private readonly ITeacherDataService _teacherDataService;
        private readonly ISyncService _syncService;
        public ICommand PageAppearingCommand { get; }

        public Teacher Teacher
        {
            get => _teacher;
            set => Set(ref _teacher, value);
        }

        public TeacherViewModel(ISyncService syncService, ISettingsService settingsService, ITeacherSQLDataService teacherSQLDataService, ITeacherDataService teacherDataService,
            IConnectionService connectionService, IDialogService dialogService)
        {
            Title = "Uw Gegevens";
            _teacherSQLDataService = teacherSQLDataService;
            _teacherDataService = teacherDataService;
            _connectionService = connectionService;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _syncService = syncService;
            EditButtonCommand = new Command(EditTeacher);
            PageAppearingCommand = new Command(PageAppearingAsync);
        }

        private async void PageAppearingAsync()
        {
            long.TryParse(_settingsService.TeacherIdSetting, out var id);
            if (_connectionService.IsConnected)
            {
//                Teacher = await _teacherDataService.GetTeacherByIdAsync(id);
                Teacher = await _syncService.GetLastModifiedTeacherAccount();
            }
            else
            {
                Teacher = await _teacherSQLDataService.GetTeacherByIdAsync(id);
            }
        }

        private async void EditTeacher()
        {
            if (_connectionService.IsConnected)
            {
                long.TryParse(_settingsService.TeacherIdSetting, out var id);

                try
                {
                    var authenticationResponse = await
                        _teacherDataService.UpdateTeacher(id, Teacher);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        await _syncService.SaveTeacherToLocalDatabase(authenticationResponse.Teacher);
                        await BlobCache.LocalMachine.Invalidate(CacheNameConstants.TeacherById + authenticationResponse.Teacher.Id);
                        await _dialogService.ShowDialog("Uw gegevens zijn succesvol gewijzigd!", DialogConstants.Info, DialogConstants.Ok);
                    }
                }
                catch (HttpRequestExceptionEx exception)
                {
                    if (exception.HttpCode == HttpStatusCode.BadRequest)
                    {
                        await _dialogService.ShowDialog("Deze gebruikersnaam wordt al gebruikt!", DialogConstants.Fout, DialogConstants.Ok);
                    }

                    Debug.WriteLine(exception);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }

            else
            {
                await _syncService.SaveTeacherToLocalDatabase(Teacher);
                _settingsService.TeacherAccountNeedsUpdate = bool.TrueString;

                await _dialogService.ShowDialog("Uw gegevens zullen worden bijgewerkt, wanneer er een internetverbinding is!", DialogConstants.Fout, DialogConstants.Ok);
            }
        }
    }
}