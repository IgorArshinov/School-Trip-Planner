namespace SchoolTripPlannerXamarin.DTOs
{
    public class ScanToddlerDTO : ObservableObject
    {
        private bool _toddlerIsScanned;
        public ToddlerDTO Toddler { get; set; }
        public long ToddlerId { get; set; }
        public ScanDTO Scan { get; set; }
        public long ScanId { get; set; }
        public bool ToddlerIsScanned
        {
            get => _toddlerIsScanned;
            set => Set(ref _toddlerIsScanned, value);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
}
}
