using AutoMapper;
using DoctorWhoDomain;

namespace DoctorWho.Web
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, CreateEpisodeDTO>();
            CreateMap<Episode, EpisodeDTO>();
            CreateMap<CreateEpisodeDTO, Episode>();
            CreateMap<EpisodeDTO, Episode>();
        }
    }

}
