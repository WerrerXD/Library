using Library_API.Core.Models;

namespace Library_API.Core.Abstractions
{
    public interface IBooksRepository: IRepository<Book>
    {
        Task<Guid> Create2(Book book, Guid authorid);
        Task<Book> GetByISBN(int isbn);
        Task<bool> IsExistByTitleAuthor(string Title, string LastName);
    }
}