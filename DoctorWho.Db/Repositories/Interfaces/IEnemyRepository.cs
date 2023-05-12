namespace DoctorWho.Db
{
    public interface IEnemyRepository
    {
        Task<Enemy> CreateEnemyAsync(Enemy enemy);
        Task<Enemy> GetEnemyWithIdAsync(int enemyId);
        Task RemoveEnemyAsync(Enemy enemyToRemove);
        Task<Enemy> UpdateEnemyAsync(Enemy updatedEnemy);
    }
}