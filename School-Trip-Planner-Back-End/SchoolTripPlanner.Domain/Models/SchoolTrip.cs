using System;
using System.Collections.Generic;

namespace SchoolTripPlanner.Domain.Models
{
    public class SchoolTrip
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ICollection<SchoolTripToddler> SchoolTripToddlers { get; set; }
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Scan> Scans { get; set; }
    }
}