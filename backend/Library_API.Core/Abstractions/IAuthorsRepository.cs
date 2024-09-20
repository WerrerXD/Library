using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Abstractions
{
    public interface IAuthorsRepository
    {
        Task<List<Author>> Get();

        Task<Author> GetById(Guid id);

        Task<Guid> Create(Author author);

        Task<Guid> Update(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country);

        Task<Guid> Delete(Guid id);
        Task<List<Book>> GetBooks(string authorName);
    }
}
