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

        //Task<List<AuthorEntity>> GetByPage(int page, int pageSize);

        Task<Guid> Create(string userName, string lastName, DateOnly dateOfBirth, string country);

        Task<Guid> Update(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country);

        Task<Guid> Delete(Guid id);
        Task<List<Book>> GetBooks(string authorName);
    }
}
