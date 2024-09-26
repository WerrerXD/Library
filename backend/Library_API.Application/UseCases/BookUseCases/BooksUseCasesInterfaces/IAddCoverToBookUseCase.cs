using Microsoft.AspNetCore.Http;

namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IAddCoverToBookUseCase
    {
        Task<string> ExecuteAsync(Guid id, IFormFile CoverPhoto, string serverFolder, string folder);
    }
}