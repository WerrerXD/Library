
using Library_API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_API.DataAccess.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public double ISBN { get; set;  }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public AuthorEntity? Author { get; set; }

        public DateOnly DateIn { get; set; }

        public DateOnly DateOut { get; set; }

        public Guid AuthorId { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;

    }
}
