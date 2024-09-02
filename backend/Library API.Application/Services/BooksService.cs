using Library_API.Core.Models;
using Library_API.DataAccess.Repositories;

namespace Library_API.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksRepository.Get();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _booksRepository.GetById(id);
        }
        public async Task<Book> GetBookByISBN(int isbn)
        {
            return await _booksRepository.GetByISBN(isbn);
        }

        public async Task<Guid> CreateBook(double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid)
        {
            return await _booksRepository.Create(isbn, title, genre, description,authorname, datein, dateout, authorid);
        }

        public async Task<Guid> CreateBook2(Book book, Guid authorid)
        {
            return await _booksRepository.Create2(book, authorid);
        }

        public async Task<Guid> UpdateBook(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid)
        {
            return await _booksRepository.Update(id, isbn, title, genre, description, authorname, datein, dateout, authorid);
        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            return await _booksRepository.Delete(id);
        }
    }
}
