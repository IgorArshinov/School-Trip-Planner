using SchoolTripPlannerUWP.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTripPlannerUWP.Core.Contracts.Services.Data
{
    public interface IToddlerDataService
    {
        Task<IEnumerable<Toddler>> GetAllToddlersAsync();
        Task<Toddler> GetToddlerByIdAsync(long id);
        Task<Toddler> PostToddler(Toddler toddler);
        Task<Toddler> PutToddler(long toddlerId, Toddler toddler);
        Task<IEnumerable<Toddler>> GetAllToddlersAfterEditAsync();
    }
}