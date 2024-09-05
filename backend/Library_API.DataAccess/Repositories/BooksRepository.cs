using Library_API.Core.Models;
using Library_API.DataAccess.Entities;
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
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.ISBN, b.Title, b.Genre, b.Description, b.AuthorName, b.DateIn, b.DateOut, b.AuthorId, b.CoverImageUrl).book)
                .ToList();

            return books;
        }

        public async Task<Book> GetById(Guid id)
        {
            var bookEntity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new Exception("Wrong id");


            var book = Book.Create(
                bookEntity.Id,
                bookEntity.ISBN,
                bookEntity.Title,
                bookEntity.Genre,
                bookEntity.Description,
                bookEntity.AuthorName,
                bookEntity.DateIn,
                bookEntity.DateOut,
                bookEntity.AuthorId,
                bookEntity.CoverImageUrl
            ).book;

            return book;
        }

        public async Task<Book> GetByISBN(int isbn)
        {
            var bookEntity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn)
                ?? throw new Exception("Wrong isbn");


            var book = Book.Create(
                bookEntity.Id,
                bookEntity.ISBN,
                bookEntity.Title,
                bookEntity.Genre,
                bookEntity.Description,
                bookEntity.AuthorName,
                bookEntity.DateIn,
                bookEntity.DateOut,
                bookEntity.AuthorId,
                bookEntity.CoverImageUrl
            ).book;

            return book;
        }

        public async Task<List<BookEntity>> GetByPage(int page, int pageSize)
        {
             return await _context.Books
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }

        public async Task<Guid> Create(double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid)
        {
            var bookEntity = new BookEntity
            {
                ISBN = isbn,
                Title = title,
                Genre = genre,
                Description = description,
                AuthorName = authorname,
                DateIn = datein,
                DateOut = dateout,
                AuthorId = authorid
            };

            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorid) ?? throw new Exception("Wrong authorID");

            author?.AuthorBooks.Add(bookEntity);

            await _context.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Create2(Book book, Guid authorid)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorid)
                ?? throw new Exception("Wrong authorID");

            var bookEntity = new BookEntity
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Genre = book.Genre,
                Description = book.Description,
                AuthorName = book.AuthorName,
                DateIn = book.DateIn,
                DateOut = book.DateOut,
                AuthorId = authorid,
                CoverImageUrl = book.CoverImageUrl
            };

            author?.AuthorBooks.Add(bookEntity);

            await _context.SaveChangesAsync();

            return bookEntity.Id;
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
