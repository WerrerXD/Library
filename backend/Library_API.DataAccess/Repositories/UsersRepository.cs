using AutoMapper;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Repositories
{
    public class UsersRepository : Repository<User> ,IUsersRepository
    {
        public UsersRepository(LibraryDbContext context)
            :base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<List<Book>> GetBooks(string email)
        {
            var user = await GetByEmail(email);


            var books = await _context.Books
                .AsNoTracking()
                .Where(b => b.UserId == user.Id)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) >= 0)
                .ToListAsync();

            return books;
        }

        public async Task<int> AddBookByISBN(int isbn, string email)
        {
            var user = await GetByEmail(email);

            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);

            var count = await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN == isbn)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) <= 0)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.DateIn, b => DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(b => b.DateOut, b => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(14))
                    .SetProperty(b => b.UserId, b => user.Id));
            return count;
        }

        public async Task<int> AddBookByTitleAndAuthor(string title, string authorName, string email)
        {
            var user = await GetByEmail(email);

            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title == title && b.AuthorName == authorName);

            var count = await _context.Books
                .AsNoTracking()
                .Where(b => b.Title == title)
                .Where(b => b.AuthorName == authorName)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) <= 0)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.DateIn, b => DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(b => b.DateOut, b => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(14))
                    .SetProperty(b => b.UserId, b => user.Id));
            return count;
        }


    }
}
