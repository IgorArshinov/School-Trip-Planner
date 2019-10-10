using SchoolTripPlannerXamarin.Models;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface ITeacherDataService
    {
        Task<AuthenticationResponse> RegisterTeacher(Teacher teacher);
        Task<AuthenticationResponse> AuthenticateTeacher(string username, string password);
        bool IsTeacherAuthenticated();
        Task<AuthenticationResponse> UpdateTeacher(long id, Teacher teacher);
        Task<Teacher> GetTeacherByIdAsync(long id);

    }
}