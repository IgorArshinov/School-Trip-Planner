using SchoolTripPlannerUWP.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerUWP.Core.Contracts.Services.Data
{
    public interface ISchoolTripDataService
    {
        Task<IEnumerable<SchoolTrip>> GetAllSchoolTripsAsync();
        Task<SchoolTrip> GetSchoolTripByIdAsync(long id);
        Task<SchoolTrip> PostSchoolTrip(SchoolTrip schoolTrip);
    }
}