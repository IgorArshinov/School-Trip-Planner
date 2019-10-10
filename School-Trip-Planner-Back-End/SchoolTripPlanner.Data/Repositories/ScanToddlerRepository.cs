using System.Linq;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ScanToddlerRepository : IScanToddlerRepository
    {
        private readonly IToddlerScanContext _context;

        public ScanToddlerRepository(IToddlerScanContext context)
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