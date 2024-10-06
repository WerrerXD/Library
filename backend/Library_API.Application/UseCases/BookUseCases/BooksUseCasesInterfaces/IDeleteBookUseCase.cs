
namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IDeleteBookUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}