using Microsoft.AspNetCore.Http;

namespace Library_API.Core.Abstractions
{
    public interface IBookCoverService
    {
        Task<string> AddCoverToBookAsync(Guid bookId, IFormFile coverPhoto);
    }
}