using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using System.Windows.Input;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.ViewModels
{
    public class AccountTabbedViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        public ICommand PageAppearingCommand { get; }

        public AccountTabbedViewModel(ISettingsService settingsService, INavigationService navigationService)
        {
            Title = "Account";
            _settingsService = settingsService;
            _navigationService = navigationService;
            PageAppearingCommand = new Command<TabbedPage>(PageAppearing);
        }

        private void PageAppearing(TabbedPage tabbedPage)
        {
            var currentTabbedPage = tabbedPage;

            string isRegistered = Xamarin.Essentials.SecureStorage.GetAsync(SettingsKeysConstants.TeacherIsRegistered).Result ?? string.Empty;

            if (!isRegistered.Equals(bool.TrueString) || !_settingsService.TeacherIsLoggedIn.Equals(bool.TrueString))
            {
                Shell.SetTabBarIsVisible(currentTabbedPage, false);
            }
        }
    }
}