using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly LibraryDbContext _context;

        public Repository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<Guid> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Guid id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }
    }
}
