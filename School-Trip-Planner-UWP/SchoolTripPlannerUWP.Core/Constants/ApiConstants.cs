namespace SchoolTripPlannerUWP.Core.Constants
{
    public static class ApiConstants
    {
        //        public const string BaseApiUrl = "https://my.api.mockaroo.com/";
        //        public const string BaseApiUrl = "https://toddlerscan.azurewebsites.net";
        public const string BaseApiUrl = "http://localhost:53942";
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
        public const string PostSchoolTripEndpoint = "/schooltrips/";
    }
}