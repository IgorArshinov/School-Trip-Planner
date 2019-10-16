using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ScanRepository : IScanRepository
    {
        private readonly ISchoolTripPlannerContext _context;

        public ScanRepository(ISchoolTripPlannerContext context)
        {
            _context = context;
        }

        public void Add(Scan scan)
        {
            _context.Scans.Add(scan);
        }

        public Scan GetById(long id)
        {
            return _context.Scans.FirstOrDefault(s => s.Id == id);
        }
    }
}