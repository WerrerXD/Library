using Library_API.Core.Models;

namespace Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces
{
    public interface IGetAuthorsBooksUseCase
    {
        Task<List<Book>> ExecuteAsync(string lastname);
    }
}