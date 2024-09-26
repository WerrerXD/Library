using Library_API.Core.Models;

namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IGetAllBooksUseCase
    {
        Task<List<Book>> ExecuteAsync();
    }
}