using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db;
public class EpisodeRepository : IEpisodeRepository
{
    private readonly DoctorWhoCoreDbContext _context = new();
    public async Task<Episode> CreateEpisodeAsync(Episode episode)
    {

        var author = await _context.Authors.FindAsync(episode.AuthorId);
        if (author == null)
        {
            throw new Exception("Author Does Not Exist");
        }
        if (episode.DoctorId != null)
        {
            var Doctor = await _context.Doctors.FindAsync(episode.DoctorId);
            if (Doctor == null)
            {
                throw new Exception("Doctor Does Not Exist");
            }
        }


        _context.Episodes.Add(episode);
        await _context.SaveChangesAsync();
        return episode;
    }
    public async Task<Episode> UpdateEpisodeAsync(Episode updatedEpisode)
    {
        var originalEpisode = await _context.Episodes.FindAsync(updatedEpisode.EpisodeId);
        if (updatedEpisode == null)
        {
            throw new Exception("Episode Doesn't Exist");
        }

        var author = await _context.Authors.FindAsync(updatedEpisode.AuthorId);
        if (author == null)
        {
            throw new Exception("Author Does Not Exist");
        }

        if (updatedEpisode.DoctorId != null)
        {
            var Doctor = await _context.Doctors.FindAsync(updatedEpisode.DoctorId);
            if (Doctor == null)
            {
                throw new Exception("Doctor Does Not Exist");
            }
        }
        EntityMapper.TransferProperties(updatedEpisode, originalEpisode);
        _context.Episodes.Update(originalEpisode);
        await _context.SaveChangesAsync();
        return updatedEpisode;
    }


    public async Task RemoveEpisodeAsync(Episode episodeToRemove)
    {
        var existingEpisode = await _context.Episodes.FindAsync(episodeToRemove.EpisodeId);
        if (existingEpisode == null)
        {
            throw new Exception($"Episode with ID {episodeToRemove.EpisodeId} not found");
        }

        _context.Episodes.Remove(existingEpisode);
        await _context.SaveChangesAsync();
    }



    public async Task AddEnemyToEpisodeAsync(Enemy enemy, Episode episode)
    {
        var episodes = _context.Episodes.Include(e => e.Enemies);
        Episode? existingEpisode = await episodes.SingleOrDefaultAsync(e => e.EpisodeId == episode.EpisodeId);

        if (existingEpisode != null && await _context.Enemies.FindAsync(enemy.EnemyId) != null)
        {
            if (existingEpisode.Enemies.SingleOrDefault(e => e.EnemyId == enemy.EnemyId) == null)
            {
                existingEpisode.Enemies.Add(enemy);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Episode already has this enemy.");
            }
        }
    }

    public async Task AddCompanionToEpisodeAsync(Companion companion, Episode episode)
    {
        var episodes = _context.Episodes.Include(e => e.Companions);
        Episode? existingEpisode = await episodes.SingleOrDefaultAsync(e => e.EpisodeId == episode.EpisodeId);

        if (existingEpisode != null && await _context.Companions.FindAsync(companion.CompanionId) != null)
        {
            if (existingEpisode.Companions.SingleOrDefault(c => c.CompanionId == companion.CompanionId) == null)
            {
                existingEpisode.Companions.Add(companion);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Episode already has this companion.");
            }
        }
    }


}

