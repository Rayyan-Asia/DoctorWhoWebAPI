using AutoMapper;
using DoctorWhoDomain;

namespace DoctorWho.Web
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<DoctorDTO,Doctor>();
        }
    }

}
