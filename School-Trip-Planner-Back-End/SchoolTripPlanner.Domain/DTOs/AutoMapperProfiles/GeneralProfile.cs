using AutoMapper;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Domain.DTOs.AutoMapperProfiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<Scan, ScanDTO>().ForMember(s => s.SchoolTrip, s => s.Ignore());
            CreateMap<ScanToddler, ScanToddlerDTO>().ForMember(s => s.Scan, s => s.Ignore());
            CreateMap<SchoolTrip, SchoolTripDTO>();
            CreateMap<SchoolTripToddler, SchoolTripToddlerDTO>().ForMember(s => s.SchoolTrip, s => s.Ignore());
        }
    }
}