using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Models
{
    public class User: IEntity
    {
        public User() { }
        private User(Guid id, string userName, string passwordHash, string email)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
        }

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public static User Create(Guid id, string userName, string passwordHash, string email)
        {
        
            return new User(id, userName, passwordHash, email);
        }
    }
}
