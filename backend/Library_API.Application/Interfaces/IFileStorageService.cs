using Microsoft.AspNetCore.Http;

namespace Library_API.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath);
    }
}