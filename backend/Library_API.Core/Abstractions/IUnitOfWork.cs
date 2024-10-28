    
namespace Library_API.Core.Abstractions
{
    public interface IUnitOfWork
    {
        IAuthorsRepository AuthorsRepository { get; }
        IBooksRepository BooksRepository { get; }
        IUsersRepository UsersRepository { get; }
        IRefreshTokensRepository RefreshTokensRepository { get; }

        void Dispose();
        Task Save();
    }
}