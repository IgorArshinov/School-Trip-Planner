using System.Collections.ObjectModel;

namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class ScanDTO
    {
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTripDTO SchoolTrip { get; set; }
        public ObservableCollection<ScanToddlerDTO> ScanToddlers { get; set; }
        public string Title { get; set; }
    }
}