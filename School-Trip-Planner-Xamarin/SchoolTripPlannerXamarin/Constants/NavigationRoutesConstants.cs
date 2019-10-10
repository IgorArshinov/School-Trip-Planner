namespace SchoolTripPlannerXamarin.Constants
{
    public static class NavigationRoutesConstants
    {
        private const string Main = "//main";
        public const string SchoolTripsPage = Main + "/schooltrips/content";
        public const string AccountTabbedPage = Main +  "/account/content";
        public const string Teacher = Main + "/teacher/content";
        public const string SchoolTripDetails = SchoolTripsPage + "/details";
        public const string NewSchoolTripScan = SchoolTripsPage + "/scan";
        public const string LoginPage = AccountTabbedPage + "/login";
    }
}