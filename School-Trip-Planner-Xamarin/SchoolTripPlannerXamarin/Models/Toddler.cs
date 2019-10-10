namespace SchoolTripPlannerXamarin.Models
{
    public class Toddler
    {
        public long Id { get; set; }
        public Class Class { get; set; }
        public long ClassId { get; set; }
        public string QRCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}