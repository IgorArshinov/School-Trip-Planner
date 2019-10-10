using System.Collections.Generic;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface ITeacherRepository
    {
        Teacher Authenticate(TeacherDTO teacherDTO);
        List<TeacherDTO> GetAll();
        TeacherDTO GetById(long id);
        long Add(TeacherDTO teacherDTO);
        void Update(TeacherDTO teacherDTO);
        void Delete(long id);
        bool TeacherExists(long id);
    }
}