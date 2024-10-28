using Library_API.Core.Models;

namespace Library_API.Core.Abstractions
{
    public interface IBooksRepository: IRepository<Book>
    {
        Task<Guid> Create(Book book);
        Task<Book> GetByISBN(double isbn);
        Task<bool> IsExistByTitleAuthor(string Title, string LastName);
        Task<bool> IsExistByIsbn(double isbn);
        Task<int> GetCountByISBN(double isbn);
    }
}