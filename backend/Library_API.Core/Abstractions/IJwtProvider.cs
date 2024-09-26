using Library_API.Core.Models;

namespace Library_API.Core.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}