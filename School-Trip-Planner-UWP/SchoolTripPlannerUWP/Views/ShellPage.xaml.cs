using SchoolTripPlannerUWP.Utilities;
using SchoolTripPlannerUWP.ViewModels;

namespace SchoolTripPlannerUWP.Views
{
    public sealed partial class ShellPage
    {
        private ShellViewModel ViewModel => ViewModelLocator.Current.ShellViewModel;

        public ShellPage()
        {
            InitializeComponent();
            ViewModel.Initialize(ShellFrame, NavigationView, KeyboardAccelerators);
        }
    }
}
