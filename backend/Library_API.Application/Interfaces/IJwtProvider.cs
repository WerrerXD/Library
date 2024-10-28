using Library_API.Core.Models;

namespace Library_API.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateRefreshToken();
        string GenerateToken(User user);
    }
}