using SchoolTripPlannerUWP.Core.Enumerations;
using System;
using System.Collections.ObjectModel;

namespace SchoolTripPlannerUWP.Core.DTOs
{
    public class SchoolTripDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ObservableCollection<SchoolTripToddlerDTO> SchoolTripToddlers { get; set; }
        public long TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }
        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);
        public ObservableCollection<ScanDTO> Scans { get; set; }

        public DateTimeOffset StartDate
        {
            get => new DateTimeOffset(StartDateTime, TimeZoneInfo.Local.GetUtcOffset(StartDateTime));
            set => StartDateTime = DateTime.SpecifyKind(value.DateTime.Date.Add(StartTime), DateTimeKind.Local);
        }

        public TimeSpan StartTime
        {
            get => StartDateTime.TimeOfDay;
            set => StartDateTime = StartDateTime.Date.Add(value);
        }

        public DateTimeOffset EndDate
        {
            get => new DateTimeOffset(EndDateTime, TimeZoneInfo.Local.GetUtcOffset(EndDateTime));
            set => EndDateTime = DateTime.SpecifyKind(value.DateTime.Date.Add(EndTime), DateTimeKind.Local);
        }

        public TimeSpan EndTime
        {
            get => EndDateTime.TimeOfDay;
            set => EndDateTime = EndDateTime.Date.Add(value);
        }

        public SchoolTripState CurrentState
        {
            get
            {
                if (StartDateTime > DateTime.UtcNow)
                {
                    return SchoolTripState.Planned;
                }
                else if (EndDateTime < DateTime.UtcNow)
                {
                    return SchoolTripState.Over;
                }
                else
                {
                    return SchoolTripState.Ongoing;
                }
            }
        }
    }
}