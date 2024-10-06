using Microsoft.AspNetCore.Http;

namespace Library_API.Application.Contracts
{
    public class BooksRequestWithPicture: BooksDTO
    {
        public required IFormFile CoverPhoto { get; set; }
    }
}
