using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Library_API.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorsRepository.Get();
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _authorsRepository.GetById(id);
        }

        public async Task<Guid> CreateAuthor(Author author)
        {
            return await _authorsRepository.Create(author);
        }

        public async Task<Guid> UpdateAuthor(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country)
        {
            return await _authorsRepository.Update(id, userName, lastName, dateOfBirth, country);
        }

        public async Task<Guid> DeleteAuthor(Guid id)
        {
            return await _authorsRepository.Delete(id);
        }

        public async Task<List<Book>> GetAuthorsBooks(string authorName)
        {
            return await _authorsRepository.GetBooks(authorName);
        }
    }
}
