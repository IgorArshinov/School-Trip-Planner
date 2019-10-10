using System;
using System.Collections.Generic;

namespace SchoolTripPlanner.Domain.DTOs
{
    public class SchoolTripDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ICollection<SchoolTripToddlerDTO> SchoolTripToddlers { get; set; }
        public long TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }
        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);
        public ICollection<ScanDTO> Scans { get; set; }
    }
}