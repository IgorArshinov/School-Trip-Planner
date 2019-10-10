using SchoolTripPlannerXamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Contracts.Services.Data
{
    public interface ISchoolTripDataService
    {
        Task<IEnumerable<SchoolTrip>> GetAllSchoolTripsAsync();
        Task<SchoolTrip> GetSchoolTripByIdAsync(long id);
        Task<SchoolTrip> PostSchoolTrip(SchoolTrip schoolTrip);
        Task<IEnumerable<SchoolTrip>> GetAllSchoolTripsByTeacherIdAsync(long id);
        Task<SchoolTrip> UpdateSchoolTrip(long id, SchoolTrip schoolTrip);
    }
}