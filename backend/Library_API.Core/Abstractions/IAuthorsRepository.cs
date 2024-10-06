using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Abstractions
{
    public interface IAuthorsRepository: IRepository<Author>
    {
        Task<List<Book>> GetBooks(string authorName, string authorLastName);
        Task<bool> IsExistByName(string Name, string LastName);
    }
}
