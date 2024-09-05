using Library_API.Core.Models;

namespace Library_API.Contracts
{
    public record AuthorsResponse
    (
        Guid Id,
        string UserName,
        string LastName,
        DateOnly DateOfBirth,
        string Country
    );
}
