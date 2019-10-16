using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowAlert(string message, string title, string buttonLabel);

        void ShowToast(string message);
    }
}
