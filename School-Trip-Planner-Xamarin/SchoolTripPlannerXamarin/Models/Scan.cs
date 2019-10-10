using System.Collections.Generic;

namespace SchoolTripPlannerXamarin.Models
{
    public class Scan
    {
        public long Id { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public long SchoolTripId { get; set; }
        public string Title { get; set; }
        public List<ScanToddler> ScanToddlers { get; set; }
    }
}