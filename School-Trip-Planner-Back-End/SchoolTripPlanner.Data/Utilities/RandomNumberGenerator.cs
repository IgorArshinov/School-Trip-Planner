using System;

namespace SchoolTripPlanner.Data.Utilities
{
    public static class RandomNumberGenerator
    {
        private static readonly Random Random = new Random();

        public static int GenerateRandomInteger(int minimumValue, int maximumValue)
        {
            return Random.Next(minimumValue, maximumValue);
        }
    }
}