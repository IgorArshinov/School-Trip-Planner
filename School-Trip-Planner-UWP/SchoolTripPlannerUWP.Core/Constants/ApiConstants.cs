namespace SchoolTripPlannerUWP.Core.Constants
{
    public static class ApiConstants
    {
        //        public const string BaseApiUrl = "https://my.api.mockaroo.com/";
        //        public const string BaseApiUrl = "https://toddlerscan.azurewebsites.net";
        public const string BaseApiUrl = "http://schooltripplanner.gear.host";

//        public const string BaseApiUrl = "http://localhost:53942";
        public const string GetSchoolTripsEndpoint = "/schooltrips";
        public const string GetClassesEndpoint = "/classes";
        public const string GetToddlersEndpoint = "/toddlers";
        public const string GetTeachersEndpoint = "/teachers";
        public const string GetTeacherByIdEndpoint = GetTeachersEndpoint + "/";
        public const string GetSchoolTripByIdEndpoint = GetSchoolTripsEndpoint + "/";
        public const string BaseApiUriPart = "/api";
        public const string GetToddlerByIdEndpoint = GetToddlersEndpoint + "/";
        public const string PostToddlerEndpoint = GetToddlersEndpoint;
        public const string PutToddlerEndpoint = GetToddlersEndpoint + "/";
        public const string PostSchoolTripEndpoint = GetSchoolTripsEndpoint + "/";
    }
}