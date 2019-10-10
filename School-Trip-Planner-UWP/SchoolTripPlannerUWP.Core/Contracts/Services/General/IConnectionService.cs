using Plugin.Connectivity.Abstractions;

namespace SchoolTripPlannerUWP.Core.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
