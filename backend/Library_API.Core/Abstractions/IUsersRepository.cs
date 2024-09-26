using Library_API.Core.Models;

namespace Library_API.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);

        Task AddBookByISBN(int isbn, string email);

        Task<List<Book>> GetBooks(string email);

        Task AddBookByTitleAndAuthor(string title, string authorName, string email);
    }
}