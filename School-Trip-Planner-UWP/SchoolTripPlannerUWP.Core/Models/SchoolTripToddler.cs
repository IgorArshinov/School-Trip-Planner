namespace SchoolTripPlannerUWP.Core.Models
{
    public class SchoolTripToddler
    {
        public Toddler Toddler { get; set; }
        public long ToddlerId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public long SchoolTripId { get; set; }
    }
}