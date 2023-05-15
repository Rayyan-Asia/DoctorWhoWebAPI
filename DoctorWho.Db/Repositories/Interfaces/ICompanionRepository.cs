using DoctorWhoDomain;

namespace DoctorWho.Db
{
    public interface ICompanionRepository
    {
        Task<bool> CompanionExistsAsync(int companionId);
        Task<Companion> CreateCompanionAsync(Companion companion);
        Task<Companion> GetCompanionWithIdAsync(int companionId);
        Task RemoveCompanionAsync(Companion companionToRemove);
        Task<Companion> UpdateCompanionAsync(Companion updatedCompanion);
    }
}