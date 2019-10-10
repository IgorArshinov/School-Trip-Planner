using SchoolTripPlannerUWP.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerUWP.Core.Contracts.Services.Data
{
    public interface IClassDataService
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
    }
}