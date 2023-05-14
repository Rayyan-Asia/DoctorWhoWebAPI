using AutoMapper;
using DoctorWhoDomain;

namespace DoctorWho.Web
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, CreateAuthorDTO>();
            CreateMap<Author, AuthorDTO>();
        }
    }

}
