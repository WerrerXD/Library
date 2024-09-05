using Microsoft.AspNetCore.Http;

namespace Library_API.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 150;

        public Book() { }

        private Book(Guid id, double isbn, string title, string genre, string description, string author, DateOnly datein, DateOnly dateout, Guid authorid, string coverImageUrl)
        {
            Id = id;
            ISBN = isbn;
            Title = title;
            Genre = genre;
            Description = description;
            AuthorName = author;
            DateIn = datein;
            DateOut = dateout;
            AuthorId = authorid;
            Author?.AuthorBooks.Add( this );
            CoverImageUrl = coverImageUrl;
            UserId = Guid.Empty;
        }

        public Book(Guid id, double isbn, string title, string genre, string description, string author, DateOnly datein, DateOnly dateout, Guid authorid, IFormFile coverPhoto, string coverImageUrl)
        {
            Id = id;
            ISBN = isbn;
            Title = title;
            Genre = genre;
            Description = description;
            AuthorName = author;
            DateIn = datein;
            DateOut = dateout;
            AuthorId = authorid;
            Author?.AuthorBooks.Add(this);
            CoverPhoto = coverPhoto;
            CoverImageUrl = coverImageUrl;
            UserId = Guid.Empty;
        }

        public Guid Id { get;}
        public double ISBN { get;}

        public string Title { get;} = string.Empty;

        public string Genre { get;} = string.Empty;

        public string Description { get;} = string.Empty;

        public string AuthorName { get;} = string.Empty;

        public Author? Author { get; }

        public DateOnly DateIn { get;} 

        public DateOnly DateOut { get;} 

        public Guid AuthorId { get;}

        public Guid UserId { get; set; } = Guid.Empty;

        public IFormFile? CoverPhoto { get; set; }
        public string? CoverImageUrl { get; set; }


        public static (Book book, string Error) Create(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid, string coverImageUrl)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title can not be empty or longer then 150 symbols";
            }

            var book = new Book(id, isbn, title, genre, description, authorname, datein, dateout, authorid, coverImageUrl);

            book.Author?.AuthorBooks.Add(book);

            return (book, error);
        }
    }
}
