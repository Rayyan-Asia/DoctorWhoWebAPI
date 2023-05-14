using AutoMapper;
using DoctorWhoDomain;

namespace DoctorWho.Web
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, CreateDoctorDTO>();
            CreateMap<Doctor, DoctorDTO>();
        }
    }

}
