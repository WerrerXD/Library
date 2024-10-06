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
    public class AuthorsRepository : Repository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(LibraryDbContext context)
            : base(context)
        {
        }

        public async Task<List<Book>> GetBooks(string authorName, string authorLastName)
        {
            var authorEntity = await _context.Authors
                 .AsNoTracking()
                 .Include(a => a.AuthorBooks)
                 .FirstOrDefaultAsync(a => a.LastName == authorLastName && a.UserName == authorName);

            return authorEntity.AuthorBooks;
        }

        public async Task<bool> IsExistByName(string Name, string LastName)
        {
            return await _context.Authors.AnyAsync(a => a.UserName == Name && a.LastName == LastName);
        }


        //public async Task<List<Author>> GetByPage(int page, int pageSize)
        //{
        //    return await _context.Authors
        //       .AsNoTracking()
        //       .Skip((page - 1) * pageSize)
        //       .Take(pageSize)
        //       .ToListAsync();
        //}
    }
}
