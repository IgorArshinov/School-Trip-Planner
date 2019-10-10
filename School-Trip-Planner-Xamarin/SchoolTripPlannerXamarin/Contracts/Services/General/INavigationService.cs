using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolTripPlannerXamarin.Contracts.Services.General
{
    public interface INavigationService
    {
        Page CurrentPage { get; }
        Task InitializeAsync();
        Task NavigateToAsync(Type viewModelType);
        Task ClearBackStack();
        Task NavigateToAsync(Type viewModelType, object parameter);
        Task NavigateToModallyAsync(Type viewModelType);
        Task NavigateToModallyAsync(Type viewModelType, object parameter);
        Task NavigateBackAsync();
        Task NavigateToTabAsync(Type viewModelType);
        Task NavigateToTabAsync(Type viewModelType, object parameter);
        Task RemoveLastFromBackStackAsync();
        Task PopToRootAsync();
    }
}