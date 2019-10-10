using Toddler_Scan_UWP.Core.Models;

namespace Toddler_Scan_UWP.Core.DTOs
{
    public class ScanToddlerDTO
    {
        public Toddler Toddler { get; set; }
        public long ToddlerId { get; set; }
        public Scan Scan { get; set; }
        public long ScanId { get; set; }
        public bool ToddlerIsScanned { get; set; }
    }
}
