using Akavache;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Repository;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Services.Data
{
    public class ScanDataService : BaseService, IScanDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ScanDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Scan>> GetAllScansAsync()
        {
            var scansFromCache = await GetFromCache<List<Scan>>(CacheNameConstants.AllScans);

            if (scansFromCache != null)
            {
                return scansFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetScansEndpoint,
            };

            var scans = await _genericRepository.GetAsync<List<Scan>>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.AllScans, scans, DateTimeOffset.Now.AddSeconds(20));

            return scans;
        }

        public async Task<Scan> GetScanByIdAsync(long id)
        {
            var scanFromCache = await GetFromCache<Scan>(CacheNameConstants.ScanById + id);

            if (scanFromCache != null)
            {
                return scanFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetScanByIdEndpoint + id,
            };

            var scan = await _genericRepository.GetAsync<Scan>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.ScanById + id, scan, DateTimeOffset.Now.AddSeconds(20));

            return scan;
        }

        public async Task<Scan> PostScan(Scan scan)
        {
            foreach (var scanToddler in scan.ScanToddlers)
            {
                scanToddler.Toddler = null;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PostScanEndpoint
            };

            return await _genericRepository.PostAsync(builder.ToString(), scan);
        }
    }
}