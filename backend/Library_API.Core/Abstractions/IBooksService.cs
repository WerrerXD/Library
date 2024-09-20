using Library_API.Core.Models;

namespace Library_API.Application.Services
{
    public interface IBooksService
    {
        Task<Guid> CreateBook2(Book book, Guid authorid);
        Task<Guid> DeleteBook(Guid id);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);
        Task<Book> GetBookByISBN(int isbn);
        Task<Guid> UpdateBook(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid);
    }
}