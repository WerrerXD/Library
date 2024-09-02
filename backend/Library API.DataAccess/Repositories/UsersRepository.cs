using AutoMapper;
using Library_API.Core.Models;
using Library_API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryDbContext _context;

        public UsersRepository(LibraryDbContext context)
        {
            _context = context;

        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("Wrong email");

            var user = User.Create(
                userEntity.Id,
                userEntity.UserName,
                userEntity.PasswordHash,
                userEntity.Email
                );

            return user;
        }

        public async Task<List<Book>> GetBooks(string email)
        {
            var user = await GetByEmail(email);


            var bookEntities = await _context.Books
                .AsNoTracking()
                .Where(b => b.UserId == user.Id)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) >= 0)
                .ToListAsync() ?? throw new Exception("No books were taken");

            List<Book> Books = [];

            for (int i = 0; i < bookEntities.Count; i++)
            {
                Books.Add(Book.Create(
                bookEntities[i].Id,
                bookEntities[i].ISBN,
                bookEntities[i].Title,
                bookEntities[i].Genre,
                bookEntities[i].Description,
                bookEntities[i].AuthorName,
                bookEntities[i].DateIn,
                bookEntities[i].DateOut,
                bookEntities[i].AuthorId,
                bookEntities[i].CoverImageUrl
                ).book);
                Books[i].UserId = user.Id;
            }
            return Books;
        }

        public async Task AddBookByISBN(int isbn, string email)
        {
            var user = await GetByEmail(email);

            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn) ?? throw new Exception("No books with this isbn");

            await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN == isbn)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) <= 0)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.DateIn, b => DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(b => b.DateOut, b => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(14))
                    .SetProperty(b => b.UserId, b => user.Id));
        }

        public async Task AddBookByTitleAndAuthor(string title, string authorName, string email)
        {
            var user = await GetByEmail(email);

            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title == title && b.AuthorName == authorName) ?? throw new Exception("No books with this title and author");

            await _context.Books
                .AsNoTracking()
                .Where(b => b.Title == title)
                .Where(b => b.AuthorName == authorName)
                .Where(b => b.DateOut.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) <= 0)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.DateIn, b => DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(b => b.DateOut, b => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(14))
                    .SetProperty(b => b.UserId, b => user.Id));
        }


    }
}
