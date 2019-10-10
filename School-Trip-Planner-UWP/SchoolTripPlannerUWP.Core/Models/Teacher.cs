using System;

namespace SchoolTripPlannerUWP.Core.Models
{
    public class Teacher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateTime LastModified { get; set; }
        public string Password { get; set; }
    }
}