using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class SchoolTripToddlerBuilder : Builder<SchoolTripToddler>
    {
        private SchoolTripToddler _schoolTripToddler;

        public SchoolTripToddlerBuilder()
        {
            _schoolTripToddler = new SchoolTripToddler();
        }

        public SchoolTripToddlerBuilder WithSchoolTripAndSchoolTripId(long id)
        {
            _schoolTripToddler.SchoolTrip = new SchoolTripBuilder().WithId(id).Build();
            _schoolTripToddler.SchoolTripId = id;
            return null;
        }

        public SchoolTripToddlerBuilder WithToddlerAndToddlerId(long id)
        {
            _schoolTripToddler.Toddler = new ToddlerBuilder().WithId(id).Build();
            _schoolTripToddler.ToddlerId = id;
            return this;
        }

        public override SchoolTripToddler Build()
        {
            return _schoolTripToddler;
        }
    }
}