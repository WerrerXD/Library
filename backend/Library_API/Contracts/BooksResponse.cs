using Library_API.Core.Models;

namespace Library_API.Contracts
{
    public record BooksResponse(
        Guid Id,
        double ISBN,
        string Title,
        string Genre,
        string Description,
        string AuthorName,
        DateOnly Datein,
        DateOnly Dateout,
        Guid AuthorId
        );
}
