using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Country { get; set; } = string.Empty;

        public List<BookEntity> AuthorBooks { get; set; } = [];
    }
}
