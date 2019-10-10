using System;
using SchoolTripPlanner.Data.Utilities;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Builders
{
    public class TeacherBuilder : BuilderWithId<Teacher, TeacherBuilder>
    {
        private Teacher _teacher;

        public TeacherBuilder()
        {
            _teacher = new Teacher
            {
                Name = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                Surname = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                Username = GuidGenerator.GenerateNewGuidString(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                PasswordHash = GuidGenerator.GenerateNewGuidByteArray(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                PasswordSalt = GuidGenerator.GenerateNewGuidByteArray(RandomNumberGenerator.GenerateRandomInteger(5, 10)),
                LastModified = DateTime.Now
            };
        }

        public override TeacherBuilder WithId(long id)
        {
            _teacher.Id = id;
            return this;
        }

        public override Teacher Build()
        {
            return _teacher;
        }
    }
}