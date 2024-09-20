using Library_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDbContext _context;

        public BooksRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var books = await _context.Books
                .AsNoTracking()
                .ToListAsync();


            return books;
        }

        public async Task<Book> GetById(Guid id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        public async Task<Book> GetByISBN(int isbn)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);

            return book;
        }

        public async Task<List<Book>> GetByPage(int page, int pageSize)
        {
             return await _context.Books
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }


        public async Task<Guid> Create2(Book book, Guid authorid)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorid);

            author?.AuthorBooks.Add(book);

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }



        public async Task<Guid> Update(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.ISBN, b => isbn)
                    .SetProperty(b => b.Title, b => title)
                    .SetProperty(b => b.Genre, b => genre)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.AuthorName, b => authorname)
                    .SetProperty(b => b.DateIn, b => datein)
                    .SetProperty(b => b.DateOut, b => dateout)
                    .SetProperty(b => b.AuthorId, b => authorid));
            return id;
        }


        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
