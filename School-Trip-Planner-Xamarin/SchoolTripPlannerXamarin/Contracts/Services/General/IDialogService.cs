using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        void ShowToast(string message);
    }
}
