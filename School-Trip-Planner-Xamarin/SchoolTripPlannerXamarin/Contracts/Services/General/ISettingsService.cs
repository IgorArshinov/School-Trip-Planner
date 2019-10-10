namespace SchoolTripPlannerXamarin.Contracts.Services.General
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);
        string TeacherUsernameSetting { get; set; }
        string TeacherAccountNeedsUpdate { get; set; }
        string TeacherIdSetting { get; set; }
        string TeacherIsLoggedIn { get; set; }
    }
}