using System.Linq;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ScanRepository : IScanRepository
    {
        private readonly IToddlerScanContext _context;

        public ScanRepository(IToddlerScanContext context)
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