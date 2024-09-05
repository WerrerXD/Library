using Library_API.Core.Models;

namespace Library_API.DataAccess
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}