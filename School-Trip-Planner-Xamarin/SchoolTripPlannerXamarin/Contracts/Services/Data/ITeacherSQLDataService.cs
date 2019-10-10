using SchoolTripPlannerXamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface ITeacherSQLDataService
    {
        Task<List<Teacher>> GetTeachersAsync();
        Task<Teacher> GetTeacherByUsernameAsync(string username);
        Task<long> SaveTeacherAsync(Teacher teacher);
        Task<int> DeleteTeacherAsync(Teacher teacher);
        Task<Teacher> GetTeacherByIdAsync(long id);

    }
}