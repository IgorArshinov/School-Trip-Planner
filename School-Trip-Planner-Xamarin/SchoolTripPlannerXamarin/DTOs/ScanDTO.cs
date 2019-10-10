using System.Collections.Generic;

namespace SchoolTripPlannerXamarin.DTOs
{
    public class ScanDTO
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTripDTO SchoolTrip { get; set; }
        public ICollection<ScanToddlerDTO> ScanToddlers { get; set; }
        public string Title { get; set; }
    }
}