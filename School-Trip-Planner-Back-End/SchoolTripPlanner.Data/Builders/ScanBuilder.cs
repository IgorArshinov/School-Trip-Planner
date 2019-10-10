using System.Collections.Generic;
using System.Collections.ObjectModel;
using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class ScanBuilder : BuilderWithId<Scan, ScanBuilder>
    {
        private Scan _scan;

        public ScanBuilder()
        {
            _scan = new Scan
            {
                Title = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10))
            };
        }

        public ScanBuilder WithSchoolTripAndSchoolTripId(long id)
        {
            _scan.SchoolTripId = id;
            _scan.SchoolTrip = new SchoolTripBuilder().WithId(id).Build();
            return this;
        }

        public override ScanBuilder WithId(long id)
        {
            _scan.Id = id;
            return this;
        }

        public ScanBuilder WithScanToddlers(int total)
        {
            ICollection<ScanToddler> scanToddlers = new Collection<ScanToddler>();
            for (int i = 1; i <= total; i++)
            {
                scanToddlers.Add(new ScanToddlerBuilder().WithScanAndScanId(i).WithToddlerAndToddlerId(i).Build());
            }

            _scan.ScanToddlers = scanToddlers;
            return this;
        }

        public override Scan Build()
        {
            return _scan;
        }
    }
}