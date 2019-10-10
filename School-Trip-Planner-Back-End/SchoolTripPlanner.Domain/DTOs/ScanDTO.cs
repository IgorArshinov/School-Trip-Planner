using System.Collections.Generic;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Domain.DTOs
{
    public class ScanDTO
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTrip SchoolTrip { get; set; }
        public ICollection<ScanToddlerDTO> ScanToddlers { get; set; }
        public string Title { get; set; }
    }
}
