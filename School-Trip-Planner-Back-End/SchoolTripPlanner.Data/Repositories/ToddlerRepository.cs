using Microsoft.EntityFrameworkCore;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class ToddlerRepository : IToddlerRepository
    {
        private readonly ISchoolTripPlannerContext _context;

        public ToddlerRepository(ISchoolTripPlannerContext context)
        {
            _context = context;
        }

        public List<Toddler> GetAll()
        {
            return _context.Toddlers.Include(c => c.Class).ToList();
        }

        public void Update(Toddler updatedToddler)
        {
            _context.Toddlers.Update(updatedToddler);
        }

        public bool ToddlerExists(long id)
        {
            return _context.Toddlers.Any(t => t.Id == id);
        }

        public void Add(Toddler toddler)
        {
            _context.Toddlers.Add(toddler);
        }

        public Toddler GetById(long id)
        {
            return _context.Toddlers.SingleOrDefault(t => t.Id == id);
        }
    }
}