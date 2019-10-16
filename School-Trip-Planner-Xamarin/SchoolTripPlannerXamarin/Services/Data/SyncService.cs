using Plugin.Connectivity.Abstractions;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Services.Data
{
    public class SyncService : ISyncService
    {
        private readonly IConnectionService _connectionService;
        private readonly ISettingsService _settingsService;
        private readonly ITeacherDataService _teacherDataService;
        private readonly ITeacherSQLDataService _teacherSQLDataService;
        private readonly IDialogService _dialogService;

        public SyncService(IDialogService dialogService, ITeacherSQLDataService teacherSQLDataService, ITeacherDataService teacherDataService, IConnectionService connectionService,
            ISettingsService settingsService)
        {
            _connectionService = connectionService;
            _settingsService = settingsService;
            _teacherDataService = teacherDataService;
            _teacherSQLDataService = teacherSQLDataService;
            _dialogService = dialogService;
            _connectionService.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (_connectionService.IsConnected && _settingsService.TeacherAccountNeedsUpdate.Equals(bool.TrueString))
            {
                await GetLastModifiedTeacherAccount();
                await _dialogService.ShowAlert("Uw gegevens zijn bijgewerkt!", DialogConstants.Info, DialogConstants.Ok);
            }
        }

        public async Task<Teacher> GetLastModifiedTeacherAccount()
        {
            long.TryParse(_settingsService.TeacherIdSetting, out var id);
            var teacherLocal = await _teacherSQLDataService.GetTeacherByIdAsync(id);
            try
            {
                var teacherOnline = await _teacherDataService.GetTeacherByIdAsync(id);

                if (teacherOnline.LastModified.CompareTo(teacherLocal.LastModified) > 0)
                {
                    await SaveTeacherToLocalDatabase(teacherOnline);
                    return teacherOnline;
                }
                else if (teacherOnline.LastModified.CompareTo(teacherLocal.LastModified) < 0)
                {
                    var updatedTeacher = await
                        _teacherDataService.UpdateTeacher(teacherLocal.Id, teacherLocal);

                    if (updatedTeacher != null && updatedTeacher.IsAuthenticated)
                    {
                        return updatedTeacher.Teacher;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            _settingsService.TeacherAccountNeedsUpdate = bool.FalseString;
            return teacherLocal;
        }

        public async Task SaveTeacherToLocalDatabase(Teacher teacher)
        {
            await _teacherSQLDataService.SaveTeacherAsync(teacher);
            _settingsService.TeacherIdSetting = teacher.Id.ToString();
            _settingsService.TeacherUsernameSetting = teacher.Username;
        }
    }
}