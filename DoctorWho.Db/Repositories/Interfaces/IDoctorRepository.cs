using DoctorWho.Web;
using DoctorWhoDomain;
namespace DoctorWho.Db
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<(List<Doctor>, PaginationMetadata)> GetAvailableDoctorsAsync(int pageNumber, int pageSize);
        Task RemoveDoctorAsync(Doctor doctorToRemove);
        Task<Doctor> UpdateDoctorAsync(Doctor updatedDoctor);
    }
}