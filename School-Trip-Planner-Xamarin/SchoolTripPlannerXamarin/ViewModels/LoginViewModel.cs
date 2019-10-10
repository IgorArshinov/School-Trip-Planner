using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.Exceptions;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private readonly IConnectionService _connectionService;
        private readonly ITeacherDataService _teacherDataService;
        private readonly IDialogService _dialogService;
        private readonly ISettingsService _settingsService;
        private readonly ISyncService _syncService;
        private INavigationService _navigationService;
        public ICommand LoginButtonCommand { get; }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public LoginViewModel(ISyncService syncService, ISettingsService settingsService, ITeacherDataService teacherDataService,
            IConnectionService connectionService, IDialogService dialogService, INavigationService navigationService)
        {
            Title = "Login";
            _teacherDataService = teacherDataService;
            _connectionService = connectionService;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _syncService = syncService;
            _navigationService = navigationService;
            LoginButtonCommand = new Command<Page>(LoginTeacher);
        }

        private async void LoginTeacher(Page page)
        {
            if (_connectionService.IsConnected)
            {
                try
                {
                    var authenticationResponse = await _teacherDataService.AuthenticateTeacher(Username, Password);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        await _syncService.SaveTeacherToLocalDatabase(authenticationResponse.Teacher);

                        _settingsService.TeacherAccountNeedsUpdate = bool.FalseString;
                        _settingsService.TeacherIsLoggedIn = bool.TrueString;

                        string isRegistered = Xamarin.Essentials.SecureStorage.GetAsync(SettingsKeysConstants.TeacherIsRegistered).Result ?? string.Empty;

                        if (!isRegistered.Equals(bool.TrueString))
                        {
                            await Xamarin.Essentials.SecureStorage.SetAsync(SettingsKeysConstants.TeacherIsRegistered, bool.TrueString);
                        }

                        await _navigationService.NavigateToTabAsync(typeof(SchoolTripsViewModel));
                        await _navigationService.ClearBackStack();
                        var currentTabbedPage = page.Parent as TabbedPage;
                        Shell.SetTabBarIsVisible(currentTabbedPage, true);
                    }
                }
                catch (HttpRequestExceptionEx exception)
                {
                    if (exception.HttpCode == HttpStatusCode.BadRequest)
                    {
                        await _dialogService.ShowDialog("Wachtwoord of gebruikersnaam is verkeerd!", DialogConstants.Fout, DialogConstants.Ok);
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