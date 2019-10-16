using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class SchoolTripRepository : ISchoolTripRepository
    {
        private readonly ISchoolTripPlannerContext _context;
        private readonly IMapper _mapper;

        public SchoolTripRepository(ISchoolTripPlannerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SchoolTrip> GetAll()
        {
            return _context.SchoolTrips.ToList();
        }

        public List<SchoolTripDTO> GetAllByTeacherId(long id)
        {
            var allSchoolTrips = _context.SchoolTrips
                .Include(st => st.SchoolTripToddlers)
                .Include(t => t.Teacher)
                .Include(s => s.Scans)
                .ThenInclude(st => st.ScanToddlers)
                .Where(s => s.TeacherId == id)
                .ProjectTo<SchoolTripDTO>(_mapper.ConfigurationProvider)
                .ToList();

            return allSchoolTrips;
        }

        public void Add(SchoolTrip schoolTrip)
        {
            _context.SchoolTrips.Add(schoolTrip);
        }

        public SchoolTripDTO GetById(long id)
        {
            return _context.SchoolTrips.Include(t => t.Teacher)
                .Include(st => st.SchoolTripToddlers)
                .ThenInclude(t => t.Toddler)
                .Include(s => s.Scans)
                .ThenInclude(st => st.ScanToddlers)
                .ThenInclude(t => t.Toddler)
                .ProjectTo<SchoolTripDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(SchoolTrip updatedSchoolTrip)
        {
            _context.SchoolTrips.Update(updatedSchoolTrip);
        }

        public bool SchoolTripExists(long id)
        {
            return _context.SchoolTrips.Any(e => e.Id == id);
        }
    }
}