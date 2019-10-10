namespace SchoolTripPlannerXamarin.Constants
{
    public static class ApiConstants
    {
//        public const string BaseApiUrl = "https://toddlerscan.azurewebsites.net";
        public const string BaseApiUrl = "http://10.0.2.2:53942";
        public const string GetSchoolTripsEndpoint = "/schooltrips";
        public const string GetClassesEndpoint = "/classes";
        public const string GetToddlersEndpoint = "/toddlers";
        public const string GetTeachersEndpoint = "/teachers";
        public const string GetTeacherByIdEndpoint = "/teachers/";
        public const string GetSchoolTripByIdEndpoint = "/schooltrips/";
        public const string BaseApiUriPart = "/api";
        public const string GetToddlerByIdEndpoint = "/toddlers/";
        public const string PostToddlerEndpoint = "/toddlers";
        public const string PutToddlerEndpoint = "/toddlers/";
        public const string PostSchoolTripEndpoint = "/schooltrips";
        public const string RegisterEndpoint = "/teachers/register";
        public const string AuthenticateEndpoint = "/teachers/authenticate";
        public const string TeacherUpdateEndpoint = "/teachers/";
        public const string ScanToddlerUpdateEndpoint = "/scantoddlers/";
        public const string GetSchoolTripsByTeacherId = "/schooltrips/teacher/";
        public const string GetScansEndpoint = "/scans";
        public const string GetScanByIdEndpoint = "/scans/";
        public const string PostScanEndpoint = "/scans";
        public const string SchoolTripUpdateEndpoint = "/schooltrips/";
    }
}