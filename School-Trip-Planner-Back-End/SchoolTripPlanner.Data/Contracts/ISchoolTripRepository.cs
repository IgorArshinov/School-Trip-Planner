using System.Collections.Generic;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface ISchoolTripRepository
    {
        List<SchoolTripDTO> GetAllByTeacherId(long id);
        List<SchoolTrip> GetAll();
        void Add(SchoolTrip schoolTrip);
        SchoolTripDTO GetById(long id);
        void Update(SchoolTrip schoolTrip);
        bool SchoolTripExists(long id);
    }
}