using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace SchoolTripPlannerUWP.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public virtual Task NavigatedToAsync(object sender, NavigationEventArgs navigationEventArgs)
        {
            return Task.FromResult(false);
        }

        public virtual Task NavigatedFromAsync(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            return Task.FromResult(false);
        }
    }
}
