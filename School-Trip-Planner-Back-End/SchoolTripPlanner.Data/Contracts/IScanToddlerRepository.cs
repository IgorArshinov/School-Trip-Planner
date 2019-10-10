using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface IScanToddlerRepository
    {
        void Update(ScanToddler updatedScanToddler);
        bool ScanToddlerExists(long id);
    }
}