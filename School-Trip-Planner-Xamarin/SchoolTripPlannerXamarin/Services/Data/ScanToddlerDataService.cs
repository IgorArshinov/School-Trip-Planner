using Akavache;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Repository;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Services.Data
{
    public class ScanToddlerDataService : BaseService, IScanToddlerService
    {
        private readonly IGenericRepository _genericRepository;

        public ScanToddlerDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ScanToddler> UpdateScanToddler(long id, ScanToddler updatedScanToddler)
        {
            updatedScanToddler.Toddler = null;
            updatedScanToddler.Scan = null;

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PutScanToddlerEndpoint + id
            };

            return await _genericRepository.PutAsync(builder.ToString(), updatedScanToddler);
        }
    }
}