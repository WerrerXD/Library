namespace Library_API.Contracts
{
    public record BooksRequestWithPicture(
    double ISBN,
    string Title,
    string Genre,
    string Description,
    string AuthorName,
    DateOnly Datein,
    DateOnly Dateout,
    Guid AuthorId,
    IFormFile CoverPhoto
    );
}
