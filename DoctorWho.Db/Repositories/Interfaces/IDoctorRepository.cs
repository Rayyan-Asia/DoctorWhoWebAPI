using DoctorWhoDomain;
namespace DoctorWho.Db
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<List<Doctor>> GetAvailableDoctorsAsync();
        Task RemoveDoctorAsync(Doctor doctorToRemove);
        Task<Doctor> UpdateDoctorAsync(Doctor updatedDoctor);
    }
}