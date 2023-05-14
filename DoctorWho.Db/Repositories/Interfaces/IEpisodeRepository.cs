using DoctorWho.Web;
using DoctorWhoDomain;
namespace DoctorWho.Db
{
    public interface IEpisodeRepository
    {
        Task AddCompanionToEpisodeAsync(Companion companion, Episode episode);
        Task AddEnemyToEpisodeAsync(Enemy enemy, Episode episode);
        Task<Episode> CreateEpisodeAsync(Episode episode);
        Task RemoveEpisodeAsync(Episode episodeToRemove);
        Task<Episode> UpdateEpisodeAsync(Episode updatedEpisode);

        Task<(List<Episode>, PaginationMetadata)> GetEpisodesAsync(int pageNumber, int pageSize);
    }
}