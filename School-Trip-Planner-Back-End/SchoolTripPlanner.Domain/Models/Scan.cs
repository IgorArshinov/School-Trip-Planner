using System.Collections.Generic;

namespace SchoolTripPlanner.Domain.Models
{
    public class Scan
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public ICollection<ScanToddler> ScanToddlers { get; set; }
        public string Title { get; set; }
    }
}