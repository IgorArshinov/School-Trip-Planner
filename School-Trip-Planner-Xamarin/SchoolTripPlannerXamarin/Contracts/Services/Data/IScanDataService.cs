using SchoolTripPlannerXamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface IScanDataService
    {
        Task<IEnumerable<Scan>> GetAllScansAsync();
        Task<Scan> GetScanByIdAsync(long id);
        Task<Scan> PostScan(Scan scan);
    }
}