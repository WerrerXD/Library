using Library_API.Core.Models;

namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface IGetUserBooksUseCase
    {
        Task<List<Book>> ExecuteAsync(string email);
    }
}