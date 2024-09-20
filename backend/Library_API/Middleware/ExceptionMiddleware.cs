using Library_API.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Net;
using System.Text.Json;

namespace Library_API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception excp)
        {
            await ExceptionAsync(context, excp);
        }
    }

    private static Task ExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        string message = "Unexpected error";
        var excpType = ex.GetType();

        if (excpType == typeof(BadRequest))
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (excpType == typeof(NotFound))
        {
            statusCode = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (excpType == typeof(Exceptions.NotImplementedException))
        {
            statusCode = HttpStatusCode.NotImplemented;
            message = ex.Message;
        }
        else if (excpType == typeof(UnauthorizedException))
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = ex.Message;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = ex.Message;
        }

        var result = JsonSerializer.Serialize(new { message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);

    }
}
