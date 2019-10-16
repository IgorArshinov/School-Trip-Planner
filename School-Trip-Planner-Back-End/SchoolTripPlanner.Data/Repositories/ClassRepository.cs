using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ISchoolTripPlannerContext _context;

        public ClassRepository(ISchoolTripPlannerContext context)
        {
            _context = context;
        }

        public List<Class> GetAll()
        {
            return _context.Classes.ToList();
        }
    }
}