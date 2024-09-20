using Microsoft.AspNetCore.Http;

namespace Library_API.Core.Contracts
{
    public class BooksRequestWithPicture: BooksDTO
    {
        public required IFormFile CoverPhoto { get; set; }
    }
}
