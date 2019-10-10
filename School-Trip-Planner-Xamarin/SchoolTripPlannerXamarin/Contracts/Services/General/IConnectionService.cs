using Plugin.Connectivity.Abstractions;

namespace SchoolTripPlannerXamarin.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
