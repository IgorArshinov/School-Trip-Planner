using System;
using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class ToddlerBuilder : BuilderWithId<Toddler, ToddlerBuilder>
    {
        private Toddler _toddler;

        public ToddlerBuilder()
        {
            Random random = new Random();
            _toddler = new Toddler
            {
                Name = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                Surname = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10))
            };
            _toddler.QRCode = _toddler.Surname + _toddler.Name;
        }

        public override ToddlerBuilder WithId(long id)
        {
            _toddler.Id = id;
            return this;
        }

        public ToddlerBuilder WithClassAndClassId(long id)
        {
            _toddler.Class = new ClassBuilder().WithId(id).Build();
            _toddler.ClassId = id;
            return this;
        }

        public ToddlerBuilder WithClassId(long id)
        {
            _toddler.ClassId = id;
            return this;
        }

        public override Toddler Build()
        {
            return _toddler;
        }
    }
}