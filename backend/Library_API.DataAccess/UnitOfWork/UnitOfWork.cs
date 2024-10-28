using Library_API.Core.Abstractions;
using Library_API.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        private IAuthorsRepository authorsRepository;
        private IBooksRepository booksRepository;
        private IUsersRepository usersRepository;
        private IRefreshTokensRepository refreshTokensRepository;


        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                this.authorsRepository ??= new AuthorsRepository(_context);
                return authorsRepository;
            }
        }

        public IBooksRepository BooksRepository
        {
            get
            {
                this.booksRepository ??= new BooksRepository(_context);
                return booksRepository;
            }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                this.usersRepository ??= new UsersRepository(_context);
                return usersRepository;
            }
        }

        public IRefreshTokensRepository RefreshTokensRepository
        {
            get
            {
                this.refreshTokensRepository ??= new RefreshTokensRepository(_context);
                return refreshTokensRepository;
            }
        }



        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
