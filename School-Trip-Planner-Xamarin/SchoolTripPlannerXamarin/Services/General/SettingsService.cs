using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.General;

namespace SchoolTripPlannerXamarin.Services.General
{
    class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public string GetItem(string key)
        {
            return _settings.GetValueOrDefault(key, string.Empty);
        }

        public string TeacherUsernameSetting
        {
            get => GetItem(SettingsKeysConstants.Username);
            set => AddItem(SettingsKeysConstants.Username, value);
        }

        public string TeacherIdSetting
        {
            get => GetItem(SettingsKeysConstants.UserId);
            set => AddItem(SettingsKeysConstants.UserId, value);
        }

        public string TeacherAccountNeedsUpdate
        {
            get => GetItem(SettingsKeysConstants.TeacherAccountNeedsUpdate);
            set => AddItem(SettingsKeysConstants.TeacherAccountNeedsUpdate, value);
        }

        public string TeacherIsLoggedIn
        {
            get => GetItem(SettingsKeysConstants.TeacherIsLoggedIn);
            set => AddItem(SettingsKeysConstants.TeacherIsLoggedIn, value);
        }
    }
}