using System.Security.Claims;

namespace Library_API.Application.Interfaces
{
    public interface ITokenService
    {
        ClaimsPrincipal ValidateAccessToken(string token);
    }
}