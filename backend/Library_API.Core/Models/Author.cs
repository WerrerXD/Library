using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Models
{
    public class Author: IEntity
    {
        public Author() { }
        private Author(Guid id, string name, string lastname, DateOnly dateofbirth, string country)
        {
            Id = id;
            UserName = name;
            LastName = lastname;
            DateOfBirth = dateofbirth;
            Country = country;
        }

        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Country { get; set; } = string.Empty;

        public List<Book> AuthorBooks { get; set; } = [];

    }
}
