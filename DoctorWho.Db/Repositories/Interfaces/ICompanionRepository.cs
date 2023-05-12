namespace DoctorWho.Db
{
    public interface ICompanionRepository
    {
        Task<Companion> CreateCompanionAsync(Companion companion);
        Task<Companion> GetCompanionWithIdAsync(int companionId);
        Task RemoveCompanionAsync(Companion companionToRemove);
        Task<Companion> UpdateCompanionAsync(Companion updatedCompanion);
    }
}