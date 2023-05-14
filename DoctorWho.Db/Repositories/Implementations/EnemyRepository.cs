using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db.Repositories.Implementations
{
    public class EnemyRepository : IEnemyRepository
    {
        private DoctorWhoCoreDbContext _context = new();
        public async Task<Enemy> CreateEnemyAsync(Enemy enemy)
        {
            _context.Enemies.Add(enemy);
            await _context.SaveChangesAsync();
            return enemy;
        }
        public async Task<Enemy> UpdateEnemyAsync(Enemy updatedEnemy)
        {
            var originalEnemy = await _context.Enemies.FindAsync(updatedEnemy.EnemyId);
            if (originalEnemy == null)
            {
                throw new Exception("Enemy not found");
            }
            EntityMapper.TransferProperties(updatedEnemy, originalEnemy);
            _context.Enemies.Update(originalEnemy);
            await _context.SaveChangesAsync();
            return originalEnemy;
        }
        public async Task RemoveEnemyAsync(Enemy enemyToRemove)
        {
            var existingEnemy = await _context.Enemies.FindAsync(enemyToRemove.EnemyId);
            if (existingEnemy == null)
            {
                throw new Exception($"Enemy with ID {enemyToRemove.EnemyId} not found");
            }

            _context.Enemies.Remove(existingEnemy);
            await _context.SaveChangesAsync();
        }

        public async Task<Enemy> GetEnemyWithIdAsync(int enemyId)
        {
            var enemy = await _context.Enemies.FindAsync(enemyId);
            if (enemy == null)
            {
                throw new Exception("Enemy with this Id does not exist");
            }
            return enemy;
        }

        public async Task<bool> EnemyExistsAsync(int enemyId)
        {
            return await _context.Enemies.AnyAsync(e => e.EnemyId == enemyId);
        }
    }
}
