using SchoolTripPlanner.Domain.DTOs;

namespace SchoolTripPlanner.Domain.Models
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public TeacherDTO Teacher { get; set; }
    }
}