using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.Exceptions;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private Teacher _teacher = new Teacher();
        public ICommand RegisterButtonCommand { get; }
        private readonly IConnectionService _connectionService;
        private readonly IDialogService _dialogService;
        private readonly ITeacherSQLDataService _teacherSQLDataService;
        private readonly ITeacherDataService _teacherDataService;

        public Teacher Teacher
        {
            get => _teacher;
            set => Set(ref _teacher, value);
        }

        public RegistrationViewModel(ITeacherSQLDataService teacherSQLDataService, ITeacherDataService teacherDataService, IConnectionService connectionService,
            IDialogService dialogService)
        {
            Title = "Registratie";
            RegisterButtonCommand = new Command(RegisterTeacher);
            _teacherSQLDataService = teacherSQLDataService;
            _teacherDataService = teacherDataService;
            _connectionService = connectionService;
            _dialogService = dialogService;
        }

        private async void RegisterTeacher()
        {
            if (_connectionService.IsConnected)
            {
                try
                {
                    var authenticationResponse = await
                        _teacherDataService.RegisterTeacher(Teacher);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        await _teacherSQLDataService.SaveTeacherAsync(authenticationResponse.Teacher);
                        await Xamarin.Essentials.SecureStorage.SetAsync(SettingsKeysConstants.TeacherIsRegistered, bool.TrueString);
                        await _dialogService.ShowAlert("Succesvol geregistreerd!", DialogConstants.Info, DialogConstants.Ok);
                    }
                }
                catch (HttpRequestExceptionEx exception)
                {
                    if (exception.HttpCode == HttpStatusCode.BadRequest)
                    {
                        await _dialogService.ShowAlert("Deze gebruikersnaam wordt al gebruikt!", DialogConstants.Fout, DialogConstants.Ok);
                    }

                    Debug.WriteLine(exception);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }
    }
}