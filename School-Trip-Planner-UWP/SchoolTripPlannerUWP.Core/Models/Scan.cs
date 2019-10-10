using System;
using System.Collections.Generic;

namespace SchoolTripPlannerUWP.Core.Models
{
    public class Scan
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public ICollection<ScanToddler> ScanToddlers { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
