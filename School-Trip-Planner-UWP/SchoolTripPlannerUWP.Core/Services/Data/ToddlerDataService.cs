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
    public class ToddlerDataService : BaseService, IToddlerDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ToddlerDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Toddler>> GetAllToddlersAsync()
        {
            List<Toddler> toddlersFromCache = await GetFromCache<List<Toddler>>(CacheNameConstants.AllToddlers);

            if (toddlersFromCache != null)
            {
                return toddlersFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetToddlersEndpoint,
            };

            var toddlers = await _genericRepository.GetAsync<List<Toddler>>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.AllToddlers, toddlers, DateTimeOffset.Now.AddSeconds(20));

            return toddlers;
        }

        public async Task<Toddler> GetToddlerByIdAsync(long id)
        {
            Toddler toddlerFromCache = await GetFromCache<Toddler>(CacheNameConstants.ToddlerById + id);

            if (toddlerFromCache != null)
            {
                return toddlerFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetToddlerByIdEndpoint + id,
            };

            var toddler = await _genericRepository.GetAsync<Toddler>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.ToddlerById + id, toddler, DateTimeOffset.Now.AddSeconds(20));

            return toddler;
        }

        public async Task<Toddler> PostToddler(Toddler toddler)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PostToddlerEndpoint
            };

            return await _genericRepository.PostAsync<Toddler>(builder.ToString(), toddler);
        }

        public async Task<Toddler> PutToddler(long toddlerId, Toddler toddler)
        {
            toddler.Class = null;

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PutToddlerEndpoint + toddlerId
            };

            return await _genericRepository.PutAsync<Toddler>(builder.ToString(), toddler);
        }
    }
}