using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Models
{
    public class RefreshTokenModel : IEntity
    {
        public RefreshTokenModel() 
        {

        }
        public Guid Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public Guid UserId { get; set; }
    }
}
