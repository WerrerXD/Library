using Library_API.Application.Exceptions;
using Library_API.Application.Interfaces;
using Library_API.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Library_API.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;

        public TokenValidationMiddleware(RequestDelegate next, ITokenService tokenService, IHttpContextAccessor httpContext)
        {
            _next = next;
            _tokenService = tokenService;
            _httpContext = httpContext;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var requiresAuthorization = endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>() != null;

            if (!requiresAuthorization)
            {
                await _next(context);
                return;
            }

            var accessToken = _httpContext.HttpContext.Request.Cookies["tasty-cookies"];
            if (accessToken.IsNullOrEmpty())
            {
                await _next(context);
                return;
            }

            
            var user = _tokenService.ValidateAccessToken(accessToken);
            if (user == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                throw new UnauthorizedException("Your jwt Token has expired, take new with the method Refresh Token");
            }

            await _next(context);
        }
    }

}
