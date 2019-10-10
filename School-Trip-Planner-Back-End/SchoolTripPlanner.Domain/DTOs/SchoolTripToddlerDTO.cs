using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Domain.DTOs
{
    public class SchoolTripToddlerDTO
    {
        public Toddler Toddler { get; set; }
        public long ToddlerId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public long SchoolTripId { get; set; }
    }
}