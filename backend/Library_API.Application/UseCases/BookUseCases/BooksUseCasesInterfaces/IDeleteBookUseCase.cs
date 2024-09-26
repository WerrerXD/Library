
namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IDeleteBookUseCase
    {
        Task<Guid> ExecuteAsync(Guid id);
    }
}