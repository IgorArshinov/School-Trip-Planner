using SchoolTripPlannerUWP.Core.Models;

namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class SchoolTripToddlerDTO
    {
        public ToddlerDTO Toddler { get; set; }
        public long ToddlerId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public long SchoolTripId { get; set; }
    }
}