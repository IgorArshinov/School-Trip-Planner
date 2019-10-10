using Akavache;
using SchoolTripPlannerUWP.Core.Constants;
using SchoolTripPlannerUWP.Core.Contracts.Repository;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SchoolTripPlannerUWP.Core.Services.Data
{
    public class SchoolTripDataService : BaseService, ISchoolTripDataService
    {
        private readonly IGenericRepository _genericRepository;

        public SchoolTripDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<SchoolTrip>> GetAllSchoolTripsAsync()
        {
            List<SchoolTrip> schooltripsFromCache = await GetFromCache<List<SchoolTrip>>(CacheNameConstants.AllSchoolTrips);

            if (schooltripsFromCache != null) 
            {
                return schooltripsFromCache;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.BaseApiUriPart + ApiConstants.GetSchoolTripsEndpoint,
                };

                var schooltrips = await _genericRepository.GetAsync<List<SchoolTrip>>(builder.ToString());

                await Cache.InsertObject(CacheNameConstants.AllSchoolTrips, schooltrips, DateTimeOffset.Now.AddSeconds(20));

                return schooltrips;
            }
        }

        public async Task<SchoolTrip> GetSchoolTripByIdAsync(long id)
        {
            SchoolTrip schoolTripFromCache = await GetFromCache<SchoolTrip>(CacheNameConstants.SchoolTripById + id);

            if (schoolTripFromCache != null)
            {
                return schoolTripFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetSchoolTripByIdEndpoint + id,
            };

            var schoolTrip = await _genericRepository.GetAsync<SchoolTrip>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.SchoolTripById + id, schoolTrip, DateTimeOffset.Now.AddSeconds(20));

            return schoolTrip;
        }

        public async Task<SchoolTrip> PostSchoolTrip(SchoolTrip schoolTrip)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PostSchoolTripEndpoint
            };

            var result = await _genericRepository.PostAsync(builder.ToString(), schoolTrip);

            return schoolTrip;
        }
    }
}