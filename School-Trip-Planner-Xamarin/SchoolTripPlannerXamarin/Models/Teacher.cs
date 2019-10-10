using SQLite;
using System;

namespace SchoolTripPlannerXamarin.Models
{
    public class Teacher
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastModified { get; set; }
    }
}