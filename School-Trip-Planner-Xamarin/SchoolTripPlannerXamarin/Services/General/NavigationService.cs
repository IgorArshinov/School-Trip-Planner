using SchoolTripPlannerXamarin.Bootstrap;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.ViewModels;
using SchoolTripPlannerXamarin.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.Services.General
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, string> _mappingsShell = new Dictionary<Type, string>();
        private ISettingsService _settingsService;
        public static Application CurrentApplication => Application.Current;
        public static Shell CurrentShell => Shell.Current;
        public Page CurrentPage { get; set; }

        public NavigationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            RegisterRoutes();
            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            string isRegistered = Xamarin.Essentials.SecureStorage.GetAsync(SettingsKeysConstants.TeacherIsRegistered).Result ?? string.Empty;
            _settingsService.TeacherIsLoggedIn = bool.FalseString;

            if (!isRegistered.Equals(bool.TrueString) || !_settingsService.TeacherIsLoggedIn.Equals(bool.TrueString))
            {
                await NavigateToTabAsync(typeof(AccountTabbedViewModel));
            }
        }

        public async Task NavigateToTabAsync(Type viewModelType)
        {
            await InternalNavigateToTabAsync(viewModelType, null);
        }

        public async Task NavigateToTabAsync(Type viewModelType, object parameter)
        {
            await InternalNavigateToTabAsync(viewModelType, parameter);
        }

        protected async Task InternalNavigateToTabAsync(Type viewModelType, object parameter)
        {
            string navigationRoute = GetPageRouteForViewModel(viewModelType);

            await CurrentShell.GoToAsync(navigationRoute, true);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public Task NavigateToModallyAsync(Type viewModelType)
        {
            return InternalNavigateToModallyAsync(viewModelType, null);
        }

        public Task NavigateToModallyAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToModallyAsync(viewModelType, parameter);
        }

        public async Task ClearBackStack()
        {
            await CurrentShell.Navigation.PopToRootAsync();
        }

        public async Task NavigateBackAsync()
        {
            await CurrentShell.Navigation.PopModalAsync();
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            return Task.FromResult(true);
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task InternalNavigateToModallyAsync(Type viewModelType, object parameter)
        {
            CurrentPage = CreateAndBindPage(viewModelType);

            await CurrentShell.Navigation.PushModalAsync(new NavigationPage(CurrentPage));

            if (CurrentPage.BindingContext is ViewModelBase viewModel) await viewModel.InitializeAsync(parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            CurrentPage = CreateAndBindPage(viewModelType);

            await CurrentShell.Navigation.PushAsync(CurrentPage);

            if (CurrentPage.BindingContext is ViewModelBase viewModel) await viewModel.InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected string GetPageRouteForViewModel(Type viewModelType)
        {
            if (!_mappingsShell.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No route for ${viewModelType} was found on navigation mappings");
            }

            return _mappingsShell[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private void RegisterRoutes()
        {
//            Routing.RegisterRoute(NavigationRoutesConstants.LoginPage, typeof(LoginPage));
//             Routing.RegisterRoute(NavigationRoutesConstants.AccountTabbedPage, typeof(AccountTabbedViewModel));
//            Routing.RegisterRoute(NavigationRoutesConstants.SchoolTripDetails, typeof(SchoolTripDetailsPage));
//            Routing.RegisterRoute(NavigationRoutesConstants.NewSchoolTripScan, typeof(NewSchoolTripScanPage));
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(NewSchoolTripScanViewModel), typeof(NewSchoolTripScanPage));
            _mappings.Add(typeof(ScanToddlerDetailsViewModel), typeof(ScanToddlerDetailsPage));
            _mappings.Add(typeof(SchoolTripDetailsViewModel), typeof(SchoolTripDetailsPage));
//            _mappingsShell.Add(typeof(TeacherViewModel), NavigationRoutesConstants.Teacher);
            _mappingsShell.Add(typeof(AccountTabbedViewModel), NavigationRoutesConstants.AccountTabbedPage);
            _mappingsShell.Add(typeof(SchoolTripsViewModel), NavigationRoutesConstants.SchoolTripsPage);
//            _mappingsShell.Add(typeof(LoginViewModel), NavigationRoutesConstants.LoginPage);
            //            _mappings.Add(typeof(SchoolTripsViewModel), typeof(SchoolTripsPage));
        }
    }
}