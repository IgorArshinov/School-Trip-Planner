namespace SchoolTripPlannerXamarin.Constants
{
    public static class CacheNameConstants
    {
        public const string AllSchoolTrips = "/schooltrips";
        public const string AllClasses = "/classes";
        public const string AllToddlers = "/toddlers";
        public const string AllTeachers = "/teachers";
        public const string TeacherById = AllTeachers + "/";
        public const string SchoolTripById = AllSchoolTrips + "/";
        public const string ToddlerById = AllToddlers + "/";
        public const string SchoolTripsByTeacherId = AllSchoolTrips + "/teacher/";
        public const string AllScans = "/scans";
        public const string ScanById = AllScans + "/";
    }
}