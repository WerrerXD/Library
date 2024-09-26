using Library_API.Core.Models;

namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IGetBookByIsbnUseCase
    {
        Task<Book> ExecuteAsync(int isbn);
    }
}