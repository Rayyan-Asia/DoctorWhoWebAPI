﻿using DoctorWhoDomain;

namespace DoctorWho.Db
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAuthorAsync(Author author);
        Task RemoveAuthorAsync(Author authorToRemove);
        Task<Author> UpdateAuthorAsync(Author updatedAuthor);

        Task<bool> AuthorExistsAsync(int authorId);
    }
}