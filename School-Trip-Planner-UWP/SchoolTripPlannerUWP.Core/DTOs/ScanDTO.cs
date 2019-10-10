using SchoolTripPlannerUWP.Core.Models;
using System;
using System.Collections.Generic;

namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class ScanDTO
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTripDTO SchoolTrip { get; set; }
        public ICollection<ScanToddler> ScanToddlers { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}