using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_API.Core.Models
{
    public class Book: IEntity
    {
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
            CoverImageUrl = coverImageUrl;
            UserId = Guid.Empty;
        }

        public Guid Id { get; set; }
        public double ISBN { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public Author? Author { get; set; }

        public DateOnly DateIn { get; set; } 

        public DateOnly DateOut { get; set; } 

        public Guid AuthorId { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;
        [NotMapped]
        public IFormFile? CoverPhoto { get; set; }
        public string? CoverImageUrl { get; set; } = string.Empty;

    }
}
