using Library_API.Core.Models;

namespace Library_API.Core.Abstractions
{
    public interface IUsersRepository: IRepository<User>
    {
        Task<User> GetByEmail(string email);

        Task<int> AddBookByISBN(int isbn, string email);

        Task<List<Book>> GetBooks(string email);

        Task<int> AddBookByTitleAndAuthor(string title, string authorName, string email);
    }
}