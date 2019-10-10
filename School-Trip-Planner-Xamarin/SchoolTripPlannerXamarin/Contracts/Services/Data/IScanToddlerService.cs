using SchoolTripPlannerXamarin.Models;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface IScanToddlerService
    {
        Task<ScanToddler> UpdateScanToddler(long id, ScanToddler updatedScanToddler);
    }
}