using Library_API.Core.Models;

namespace Library_API.Core.Contracts
{
    public class AuthorsResponse: AuthorsDTO
    {
        public required Guid Id { get; set; }
    }
}
