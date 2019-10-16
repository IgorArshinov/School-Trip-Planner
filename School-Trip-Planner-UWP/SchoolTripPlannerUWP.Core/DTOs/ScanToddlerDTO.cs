namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class ScanToddlerDTO
    {
        public ToddlerDTO Toddler { get; set; }
        public long ToddlerId { get; set; }
        public ScanDTO Scan { get; set; }
        public long ScanId { get; set; }
        public bool ToddlerIsScanned { get; set; }
    }
}
