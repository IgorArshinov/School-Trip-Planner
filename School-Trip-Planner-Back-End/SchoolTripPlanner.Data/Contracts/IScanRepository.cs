using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface IScanRepository
    {
        void Add(Scan scan);
        Scan GetById(long id);
    }
}