

using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace DoctorWho.Db
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DoctorWhoCoreDbContext _context = new();
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<Author> UpdateAuthorAsync(Author updatedAuthor)
        {
            var originalAuthor = await _context.Authors.FindAsync(updatedAuthor.AuthorId);
            if (originalAuthor == null)
            {
                throw new Exception("Author not found");
            }
            EntityMapper.TransferProperties(updatedAuthor, originalAuthor);

            _context.Authors.Update(originalAuthor);
            await _context.SaveChangesAsync();
            return originalAuthor;
        }
        public async Task RemoveAuthorAsync(Author authorToRemove)
        {
            var existingAuthor = await _context.Authors.FindAsync(authorToRemove.AuthorId);
            if (existingAuthor == null)
            {
                throw new Exception($"Author with ID {authorToRemove.AuthorId} not found");
            }

            _context.Authors.Remove(existingAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _context.Authors.AnyAsync(a => a.AuthorId == authorId);
        }
    }
}
