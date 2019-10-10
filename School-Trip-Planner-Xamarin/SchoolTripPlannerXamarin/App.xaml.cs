using SchoolTripPlannerXamarin.Bootstrap;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.Views;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeApp();
            InitializeNavigation();
        }

        private void InitializeNavigation()
        {
            AppContainer.Resolve<INavigationService>().InitializeAsync();
        }

        private void InitializeApp()
        {
            
            AppContainer.RegisterDependencies();
            Current.MainPage = new ShellPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}