using Library_API.Core.Models;

namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface ICreateBookUseCase
    {
        Task<Guid> ExecuteAsync(Book book);
    }
}