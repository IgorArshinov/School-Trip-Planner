using SchoolTripPlannerUWP.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerUWP.Core.Contracts.Services.Data
{
    public interface ITeacherDataService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    }
}