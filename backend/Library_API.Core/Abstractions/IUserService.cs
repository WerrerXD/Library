
using Library_API.Core.Models;
using System.Net.Http;

namespace Library_API.Application.Services
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string userName, string email, string password);

        Task AddBookByISBN(int isbn, string email);

        Task<List<Book>> GetUserBooks(string email);

        Task AddBookByTitleAndAuthor(string title, string authorName, string email);
    }
}