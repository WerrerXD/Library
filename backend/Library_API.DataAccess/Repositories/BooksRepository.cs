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

        public async Task<Book> GetByISBN(int isbn)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
            return book;
        }

        public async Task<Guid> Create2(Book book, Guid authorid)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorid);

            author?.AuthorBooks.Add(book);

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task<bool> IsExistByTitleAuthor(string Title, string LastName)
        {
            return await _context.Books.AnyAsync(a => a.Title == Title && a.AuthorName == LastName);
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
