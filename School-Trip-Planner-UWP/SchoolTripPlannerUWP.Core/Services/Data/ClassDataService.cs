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
    public class ClassDataService : BaseService, IClassDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ClassDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            List<Class> classFromCache = await GetFromCache<List<Class>>(CacheNameConstants.AllClasses);

            if (classFromCache != null)
            {
                return classFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetClassesEndpoint,
            };

            var classes = await _genericRepository.GetAsync<List<Class>>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.AllClasses, classes, DateTimeOffset.Now.AddSeconds(20));

            return classes;
        }
    }
}