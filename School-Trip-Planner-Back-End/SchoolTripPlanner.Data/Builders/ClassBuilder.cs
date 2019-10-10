using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class ClassBuilder : BuilderWithId<Class, ClassBuilder>
    {
        private Class _class;

        public ClassBuilder()
        {
            _class = new Class
            {
                Name = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10))
            };
        }

        public override ClassBuilder WithId(long id)
        {
            _class.Id = id;
            return this;
        }

        public override Class Build()
        {
            return _class;
        }
    }
}