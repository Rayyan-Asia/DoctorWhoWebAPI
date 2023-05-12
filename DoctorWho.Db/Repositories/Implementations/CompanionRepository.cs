using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Db
{
    public class CompanionRepository : ICompanionRepository
    {
        private DoctorWhoCoreDbContext _context = new();

        public async Task<Companion> CreateCompanionAsync(Companion companion)
        {
            _context.Companions.Add(companion);
            await _context.SaveChangesAsync();
            return companion;
        }
        public async Task<Companion> UpdateCompanionAsync(Companion updatedCompanion)
        {
            var originalCompanion = await _context.Companions.FindAsync(updatedCompanion.CompanionId);
            if (originalCompanion == null)
            {
                throw new Exception("Companion not found");
            }
            EntityMapper.TransferProperties(updatedCompanion, originalCompanion);
            _context.Companions.Update(originalCompanion);
            await _context.SaveChangesAsync();
            return originalCompanion;
        }
        public async Task RemoveCompanionAsync(Companion companionToRemove)
        {
            var existingCompanion = await _context.Companions.FindAsync(companionToRemove.CompanionId);
            if (existingCompanion == null)
            {
                throw new Exception($"Companion with ID {companionToRemove.CompanionId} not found");
            }

            _context.Companions.Remove(existingCompanion);
            await _context.SaveChangesAsync();
        }

        public async Task<Companion> GetCompanionWithIdAsync(int companionId)
        {
            var companion = await _context.Companions.FindAsync(companionId);
            if (companion == null)
            {
                throw new Exception("Companion with this Id does not exist");
            }
            return companion;
        }
    }
}
