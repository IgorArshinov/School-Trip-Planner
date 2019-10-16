using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ScanToddlerRepository : IScanToddlerRepository
    {
        private readonly ISchoolTripPlannerContext _context;

        public ScanToddlerRepository(ISchoolTripPlannerContext context)
        {
            _context = context;
        }

        public void Update(ScanToddler updatedScanToddler)
        {
            _context.ScanToddlers.Update(updatedScanToddler);
        }

        public bool ScanToddlerExists(long id)
        {
            return _context.ScanToddlers.Any(e => e.ToddlerId == id);
        }
    }
}