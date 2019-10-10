using System;

namespace SchoolTripPlanner.Domain.DTOs
{
    public class TeacherDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastModified { get; set; }
    }
}