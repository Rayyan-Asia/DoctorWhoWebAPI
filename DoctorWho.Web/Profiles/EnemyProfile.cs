using AutoMapper;
using DoctorWhoDomain;

namespace DoctorWho.Web
{
    public class EnemyProfile : Profile
    {
        public EnemyProfile()
        {
            CreateMap<Enemy, CreateEnemyDTO>();
            CreateMap<Enemy, EnemyDTO>();
            CreateMap<CreateEnemyDTO, Enemy>();
            CreateMap<EnemyDTO, Enemy>();
        }
    }
}
