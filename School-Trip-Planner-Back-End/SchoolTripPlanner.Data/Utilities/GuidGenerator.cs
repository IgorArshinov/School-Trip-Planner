using System;

namespace SchoolTripPlanner.Data.Utilities
{
    public static class GuidGenerator
    {
        public static string GenerateNewGuidString(int length)
        {
            var guid = GenerateGuid();
            length = CalculateLength(length, guid);

            return guid.ToString().Substring(0, length);
        }

        private static int CalculateLength(int length, Guid guid)
        {
            if (length < 0)
            {
                length = 0;
            }
            else if (length > guid.ToString().Length)
            {
                length = guid.ToString().Length;
            }

            return length;
        }

        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public static byte[] GenerateNewGuidByteArray(int length)
        {
            var guid = GenerateGuid();
            length = CalculateLength(length, guid);

            var guidByteArray = guid.ToByteArray();
            Array.Resize(ref guidByteArray, length);
            return guidByteArray;
        }
    }
}