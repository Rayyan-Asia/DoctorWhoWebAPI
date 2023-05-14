using DoctorWho.Web;
using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Db.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private DoctorWhoCoreDbContext _context = new ();

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }
        public async Task<Doctor> UpdateDoctorAsync(Doctor updatedDoctor)
        {
            var originalDoctor = await _context.Doctors.FindAsync(updatedDoctor.DoctorId);
            if (originalDoctor == null)
            {
                throw new Exception("Doctor not found");
            }
            EntityMapper.TransferProperties(updatedDoctor, originalDoctor);
            _context.Doctors.Update(originalDoctor);
            await _context.SaveChangesAsync();
            return originalDoctor;
        }
        public async Task RemoveDoctorAsync(Doctor doctorToRemove)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorToRemove.DoctorId);
            if (existingDoctor == null)
            {
                throw new Exception($"Doctor with ID {doctorToRemove.DoctorId} not found");
            }

            _context.Doctors.Remove(existingDoctor);
            await _context.SaveChangesAsync();
        }


        public async Task<(List<Doctor>, PaginationMetadata)> GetAvailableDoctorsAsync(int pageNumber, int pageSize)
        {
            var doctors = await _context.Doctors.AsNoTracking().ToListAsync();

            var count =  doctors.Count;

            var metadata = new PaginationMetadata()
            {
                ItemCount = count,
                PageCount = pageNumber,
                PageSize = pageSize
            };

            var filteredDoctors = doctors.OrderBy(d => d.DoctorNumber)
                .Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return (filteredDoctors,metadata);

        }

        public Task<bool> DoctorExistsAsync(int id)
        {
            return _context.Doctors.AnyAsync(d => d.DoctorId == id);
        }

        public async Task DeleteDoctorAsync(int doctorId)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorId);
            if (existingDoctor == null)
            {
                throw new Exception($"Doctor with ID {doctorId} not found");
            }
            _context.Doctors.Remove(existingDoctor);
            await _context.SaveChangesAsync();
        }
    }
}
