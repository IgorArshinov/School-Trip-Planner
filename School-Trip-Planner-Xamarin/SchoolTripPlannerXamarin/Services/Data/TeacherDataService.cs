using Akavache;
using SchoolTripPlannerXamarin.Constants;
using SchoolTripPlannerXamarin.Contracts.Repository;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Models;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Services.Data
{
    public class TeacherDataService : BaseService, ITeacherDataService
    {
        private readonly IGenericRepository _genericRepository;

        public TeacherDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<AuthenticationResponse> RegisterTeacher(Teacher teacher)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.RegisterEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Name = teacher.Name,
                Surname = teacher.Surname,
                Username = teacher.Username,
                Password = teacher.Password,
                LastModified = DateTime.Now
            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }

        public async Task<AuthenticationResponse> UpdateTeacher(long id, Teacher teacher)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.PutTeacherEndpoint + id
            };

            AuthenticationRequest updatedTeacher = new AuthenticationRequest()
            {
                Id = id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Username = teacher.Username,
                Password = teacher.Password,
                LastModified = DateTime.Now
            };

            return await _genericRepository.PutAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), updatedTeacher);
        }

        public async Task<AuthenticationResponse> AuthenticateTeacher(string username, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.BaseApiUriPart + ApiConstants.AuthenticateEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Username = username,
                Password = password
            };

            var authenticationResponse = await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
            var teacher = authenticationResponse.Teacher;
            await Cache.InsertObject(CacheNameConstants.TeacherById + teacher.Id, teacher, DateTimeOffset.Now.AddSeconds(20));

            return authenticationResponse;
        }

        public async Task<Teacher> GetTeacherByIdAsync(long id)
        {
            var teacherFromCache = await GetFromCache<Teacher>(CacheNameConstants.TeacherById + id);

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