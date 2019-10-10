using System;

namespace SchoolTripPlannerXamarin.Models
{
    public class AuthenticationRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Id { get; set; }
        public DateTime LastModified { get; set; }
    }
}
