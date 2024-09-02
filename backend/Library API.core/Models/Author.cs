using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Models
{
    public class Author
    {
        private Author(Guid id, string name, string lastname, DateOnly dateofbirth, string country)
        {
            Id = id;
            UserName = name;
            LastName = lastname;
            DateOfBirth = dateofbirth;
            Country = country;
        }

        public Guid Id { get; }

        public string UserName { get; } = string.Empty;

        public string LastName { get; } = string.Empty;

        public DateOnly DateOfBirth { get; }

        public string Country { get; } = string.Empty;

        public List<Book> AuthorBooks { get; set; } = [];

        public static (Author author, string Error) Create(Guid id, string name, string lastname, DateOnly dateofbirth, string country)
        {
            var error = string.Empty;

            var author = new Author(id, name, lastname, dateofbirth, country);

            return (author, error);
        }

        public static (Author author, string Error) Create(Guid id, string name, string lastname, DateOnly dateofbirth, string country, List<Book> authorBooks)
        {
            var error = string.Empty;

            var author = new Author(id, name, lastname, dateofbirth, country);

            author.AuthorBooks = authorBooks;

            return (author, error);
        }
    }
}
