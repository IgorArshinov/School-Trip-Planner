namespace SchoolTripPlannerXamarin.Models
{
    public class ScanToddler
    {
        public long ScanId { get; set; }
        public Scan Scan { get; set; }
        public Toddler Toddler { get; set; }
        public long ToddlerId { get; set; }
        public bool ToddlerIsScanned { get; set; }
    }
}