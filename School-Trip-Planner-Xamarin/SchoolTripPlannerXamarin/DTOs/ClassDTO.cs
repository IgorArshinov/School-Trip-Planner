using System;

namespace SchoolTripPlannerXamarin.DTOs
{
    public class ClassDTO : ICloneable
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}