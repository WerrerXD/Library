using Library_API.Core.Models;

namespace Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces
{
    public interface IGetAuthorByIdUseCase
    {
        Task<Author?> ExecuteAsync(Guid id);
    }
}