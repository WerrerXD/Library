using Library_API.Core.Models;

namespace Library_API.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Guid> Create(double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid);
        Task<Guid> Create2(Book book, Guid authorid);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<Book> GetById(Guid id);
        Task<Book> GetByISBN(int isbn);
        Task<Guid> Update(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid);
    }
}