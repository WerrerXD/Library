using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Abstractions
{
    public interface IAuthorsService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<Guid> CreateAuthor(Author author);
        Task<Guid> UpdateAuthor(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country);
        Task<Guid> DeleteAuthor(Guid id);
        Task<List<Book>> GetAuthorsBooks(string authorName);

    }
}
