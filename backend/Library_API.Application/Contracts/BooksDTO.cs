namespace Library_API.Application.Contracts
{
    public class BooksDTO
    {
        public required double ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required string AuthorName { get; set; }
        public required DateOnly Datein { get; set; }
        public required DateOnly Dateout { get; set; }
        public required Guid AuthorId { get; set; }
    }
}
