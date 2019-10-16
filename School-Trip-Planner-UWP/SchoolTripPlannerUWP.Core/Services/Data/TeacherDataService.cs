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
    public class TeacherDataService : BaseService, ITeacherDataService
    {
        private readonly IGenericRepository _genericRepository;

        public TeacherDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            List<Teacher> teachersFromCache = await GetFromCache<List<Teacher>>(CacheNameConstants.AllTeachers);

            if (teachersFromCache != null)
            {
                return teachersFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetTeachersEndpoint,
            };

            var teachers = await _genericRepository.GetAsync<List<Teacher>>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.AllTeachers, teachers, DateTimeOffset.Now.AddSeconds(20));

            return teachers;
        }

        public async Task<Teacher> GetTeacherByIdAsync(long id)
        {
            Teacher teacherFromCache = await GetFromCache<Teacher>(CacheNameConstants.TeacherById + id);

            if (teacherFromCache != null)
            {
                return teacherFromCache;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.GetTeacherByIdEndpoint + id,
            };

            var teacher = await _genericRepository.GetAsync<Teacher>(builder.ToString());

            await Cache.InsertObject(CacheNameConstants.TeacherById + id, teacher, DateTimeOffset.Now.AddSeconds(20));

            return teacher;
        }
    }
}