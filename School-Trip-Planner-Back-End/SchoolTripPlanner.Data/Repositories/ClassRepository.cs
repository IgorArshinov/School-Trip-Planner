using System.Collections.Generic;
using System.Linq;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IToddlerScanContext _context;

        public ClassRepository(IToddlerScanContext context)
        {
            _context = context;
        }

        public List<Class> GetAll()
        {
            return _context.Classes.ToList();
        }
    }
}