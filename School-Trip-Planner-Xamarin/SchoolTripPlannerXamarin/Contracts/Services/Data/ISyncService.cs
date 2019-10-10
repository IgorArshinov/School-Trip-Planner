using SchoolTripPlannerXamarin.Models;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface ISyncService
    {
        Task SaveTeacherToLocalDatabase(Teacher teacher);
        Task<Teacher> GetLastModifiedTeacherAccount();
    }
}