using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.DataAccess.Repositories
{
    public class BooksRepository : Repository<Book> ,IBooksRepository
    {
        public BooksRepository(LibraryDbContext context)
            :base(context)
        {
        }

        public async Task<Book> GetByISBN(double isbn)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
            return book;
        }

        public async Task<Guid> Create(Book book)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);

            author?.AuthorBooks.Add(book);

            await _context.AddAsync(book);

            return book.Id;
        }

        public async Task<bool> IsExistByTitleAuthor(string Title, string LastName)
        {
            return await _context.Books.AnyAsync(a => a.Title == Title && a.AuthorName == LastName);
        }

        public async Task<bool> IsExistByIsbn(double isbn)
        {
            return await _context.Books.AnyAsync(a => a.ISBN == isbn);
        }

        public async Task<int> GetCountByISBN(double isbn)
        {
            var books = await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN == isbn)
                .ToListAsync();
            return books.Count;
        }

        //public async Task<List<Book>> GetByPage(int page, int pageSize)
        //{
        //     return await _context.Books
        //        .AsNoTracking()
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();
        //}

    }
}
