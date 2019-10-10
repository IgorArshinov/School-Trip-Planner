using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SchoolTripPlannerUWP.Contracts
{
    public interface INavigationServiceEx
    {
        event NavigatedEventHandler Navigated;
        event NavigationFailedEventHandler NavigationFailed;
        event NavigatingCancelEventHandler Navigating;
        Frame Frame { get; set; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        bool GoBack();
        void GoForward();
        bool NavigateAsync(string pageKey, object parameter = null, NavigationTransitionInfo infoOverride = null);
        void Configure(string key, Type pageType);
        string GetNameOfRegisteredPage(Type page);
        void RegisterFrameEvents();
        void UnregisterFrameEvents();
        void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e);
        void Frame_Navigated(object sender, NavigationEventArgs e);
        void Frame_Navigating(object sender, NavigatingCancelEventArgs e);
    }
}
