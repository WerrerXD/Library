using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorsRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> Get()
        {
            var authors = await _context.Authors
                .AsNoTracking()
                .ToListAsync();

            return authors;
        }

        public async Task<Author> GetById(Guid id)
        {
            var author = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task<List<Author>> GetByPage(int page, int pageSize)
        {
            return await _context.Authors
               .AsNoTracking()
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

        }

        public async Task<Guid> Create(Author author)
        {

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return author.Id;
        }

        public async Task<Guid> Update(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country)
        {
            await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.UserName, a => userName)
                    .SetProperty(a => a.LastName, a => lastName)
                    .SetProperty(a => a.DateOfBirth, a => dateOfBirth)
                    .SetProperty(a => a.Country, a => country)
                    );
            return id;
        }


        public async Task<Guid> Delete(Guid id)
        {
            await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Book>> GetBooks(string authorName)
        {
            var authorEntity = await _context.Authors
                 .AsNoTracking()
                 .Include(a => a.AuthorBooks)
                 .FirstOrDefaultAsync(a => a.LastName == authorName);

            return authorEntity?.AuthorBooks;
        }
    }
}
