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


        public async Task<List<Doctor>> GetAvailableDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }


    }
}
