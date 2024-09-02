using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Library_API.DataAccess.Entities;
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
            var authorEntities = await _context.Authors
                .AsNoTracking()
                .ToListAsync();

            var authors = authorEntities
                .Select(a => Author.Create(a.Id, a.UserName, a.LastName, a.DateOfBirth, a.Country).author)
                .ToList();

            return authors;
        }

        public async Task<Author> GetById(Guid id)
        {
            var authorEntity = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Wrong id");

            var author = Author.Create(
                authorEntity.Id,
                authorEntity.UserName,
                authorEntity.LastName,
                authorEntity.DateOfBirth,
                authorEntity.Country
            ).author;

            return author;
        }

        public async Task<List<AuthorEntity>> GetByPage(int page, int pageSize)
        {
            return await _context.Authors
               .AsNoTracking()
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

        }

        public async Task<Guid> Create(string userName, string lastName, DateOnly dateOfBirth, string country)
        {
            var authorEntity = new AuthorEntity
            {
                UserName = userName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Country = country
            };



            await _context.Authors.AddAsync(authorEntity);
            await _context.SaveChangesAsync();

            return authorEntity.Id;
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
                 .FirstOrDefaultAsync(a => a.LastName == authorName)
                 ?? throw new Exception("Wrong authorLastName");


            List<Book> Books = [];

            for (int i = 0; i < authorEntity.AuthorBooks.Count; i++)
            {
                Books.Add(Book.Create(
                authorEntity.AuthorBooks[i].Id,
                authorEntity.AuthorBooks[i].ISBN,
                authorEntity.AuthorBooks[i].Title,
                authorEntity.AuthorBooks[i].Genre,
                authorEntity.AuthorBooks[i].Description,
                authorEntity.AuthorBooks[i].AuthorName,
                authorEntity.AuthorBooks[i].DateIn,
                authorEntity.AuthorBooks[i].DateOut,
                authorEntity.AuthorBooks[i].AuthorId,
                authorEntity.AuthorBooks[i].CoverImageUrl
                ).book);
            }

            return Books;
        }
    }
}
