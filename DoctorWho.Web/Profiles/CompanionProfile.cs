using AutoMapper;
using DoctorWhoDomain;
using DoctorWhoDomain.DTOs;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DoctorWho.Web
{
    public class CompanionProfile : Profile
    {
        public CompanionProfile() {
            CreateMap<Companion, CreateCompanionDTO>();
            CreateMap<Companion, CompanionDTO>();
        }
    }
}
