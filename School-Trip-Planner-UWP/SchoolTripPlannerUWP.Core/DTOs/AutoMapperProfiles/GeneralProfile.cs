using AutoMapper;
using SchoolTripPlannerUWP.Core.Models;

namespace SchoolTripPlannerUWP.Core.DTOs.AutoMapperProfiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<Toddler, ToddlerDTO>();
            CreateMap<ToddlerDTO, Toddler>();
            CreateMap<Class, ClassDTO>();
            CreateMap<ClassDTO, Class>();
            CreateMap<Scan, ScanDTO>();
            CreateMap<ScanDTO, Scan>();
            CreateMap<SchoolTrip, SchoolTripDTO>();
            CreateMap<SchoolTripDTO, SchoolTrip>();
            CreateMap<SchoolTripToddler, SchoolTripToddlerDTO>();
            CreateMap<SchoolTripToddlerDTO, SchoolTripToddler>();
        }
    }
}