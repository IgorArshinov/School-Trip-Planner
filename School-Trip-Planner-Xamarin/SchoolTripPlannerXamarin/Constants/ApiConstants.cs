namespace SchoolTripPlannerXamarin.Constants
{
    public static class ApiConstants
    {
        //        public const string BaseApiUrl = "https://toddlerscan.azurewebsites.net";
        public const string BaseApiUrl = "http://schooltripplanner.gear.host";

//        public const string BaseApiUrl = "http://10.0.2.2:53942";
        public const string GetSchoolTripsEndpoint = "/schooltrips";
        public const string BaseApiUriPart = "/api";
        public const string RegisterEndpoint = GetTeachersEndpoint + "/register";
        public const string AuthenticateEndpoint = GetTeachersEndpoint + "/authenticate";
        public const string PutTeacherEndpoint = GetTeachersEndpoint + "/";
        public const string PutScanToddlerEndpoint = "/scantoddlers/";
        public const string GetSchoolTripsByTeacherId = GetSchoolTripsEndpoint + "/teacher/";
        public const string GetScansEndpoint = "/scans";
        public const string GetScanByIdEndpoint = GetScansEndpoint + "/";
        public const string PostScanEndpoint = GetScansEndpoint + "/";
        public const string PutSchoolTripEndpoint = GetSchoolTripsEndpoint + "/";
        public const string GetTeachersEndpoint = "/teachers";
        public const string GetTeacherByIdEndpoint = GetTeachersEndpoint + "/";
        public const string GetSchoolTripByIdEndpoint = GetSchoolTripsEndpoint + "/";
        public const string PostSchoolTripEndpoint = GetSchoolTripsEndpoint + "/";
    }
}