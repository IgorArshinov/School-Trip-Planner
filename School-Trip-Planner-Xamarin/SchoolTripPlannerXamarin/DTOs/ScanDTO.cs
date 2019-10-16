using System.Collections.ObjectModel;

namespace SchoolTripPlannerXamarin.DTOs
{
    public class ScanDTO : ObservableObject
    {
        private string _title;
        public long Id { get; set; }
        public long SchoolTripId { get; set; }
        public SchoolTripDTO SchoolTrip { get; set; }
        public ObservableCollection<ScanToddlerDTO> ScanToddlers { get; set; }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}