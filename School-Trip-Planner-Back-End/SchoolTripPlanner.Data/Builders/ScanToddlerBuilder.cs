using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class ScanToddlerBuilder : Builder<ScanToddler>
    {
        private ScanToddler _scanToddler;

        public ScanToddlerBuilder()
        {
            _scanToddler = new ScanToddler
            {
                ToddlerIsScanned = RandomNumberGenerator.GenerateRandomInteger(0, 1) == 0
            };
        }

        public ScanToddlerBuilder WithToddlerAndToddlerId(long id)
        {
            _scanToddler.ToddlerId = id;
            _scanToddler.Toddler = new ToddlerBuilder().WithId(id).Build();
            return this;
        }

        public ScanToddlerBuilder WithScanAndScanId(long id)
        {
            _scanToddler.ScanId = id;
            _scanToddler.Scan = new ScanBuilder().WithId(id).Build();
            return this;
        }

        public override ScanToddler Build()
        {
            return _scanToddler;
        }
    }
}