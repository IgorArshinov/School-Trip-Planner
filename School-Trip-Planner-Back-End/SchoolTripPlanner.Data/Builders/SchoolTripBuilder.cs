using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class SchoolTripBuilder : BuilderWithId<SchoolTrip, SchoolTripBuilder>
    {
        private SchoolTrip _schoolTrip;

        public SchoolTripBuilder()
        {
            var random = new Random();
            var startDateNumber = random.Next(random.Next(5));
            _schoolTrip = new SchoolTrip
            {
                StartDateTime = DateTime.Now.AddDays(startDateNumber),
                EndDateTime = DateTime.Now.AddDays(startDateNumber + random.Next(5)),
                Title = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
            };
        }

        public override SchoolTripBuilder WithId(long id)
        {
            _schoolTrip.Id = id;
            return null;
        }

        public SchoolTripBuilder WithTeacherAndTeacherId(long id)
        {
            _schoolTrip.Teacher = new TeacherBuilder().WithId(id).Build();
            _schoolTrip.TeacherId = id;
            return this;
        }

        public SchoolTripBuilder WithTeacherId(long id)
        {
            _schoolTrip.TeacherId = id;
            return this;
        }

        public SchoolTripBuilder WithSchoolTripToddlers(int total)
        {
            ICollection<SchoolTripToddler> schoolTripToddlers = new Collection<SchoolTripToddler>();
            for (int i = 1; i <= total; i++)
            {
                schoolTripToddlers.Add(new SchoolTripToddlerBuilder().WithSchoolTripAndSchoolTripId(i).WithToddlerAndToddlerId(i).Build());
            }

            _schoolTrip.SchoolTripToddlers = schoolTripToddlers;
            return this;
        }

        public SchoolTripBuilder WithScans(int total)
        {
            ICollection<Scan> scans = new Collection<Scan>();
            for (int i = 1; i <= total; i++)
            {
                scans.Add(new ScanBuilder().WithId(i).Build());
            }

            _schoolTrip.Scans = scans;
            return this;
        }

        public override SchoolTrip Build()
        {
            return _schoolTrip;
        }
    }
}