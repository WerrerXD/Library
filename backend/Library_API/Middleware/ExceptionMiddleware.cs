//using FluentValidation;
//using System.Threading.Tasks;
//using System.Net;
//using System.Text.Json;
//using Azure;

//namespace Library_API.Middleware
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<ExceptionMiddleware> _logger;

//        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, ex.Message);

//                var responseError = new ResponseError("server.internal", ex.Message, null);
//                var envelope = Envelope.Error([responseError]);

//                await HandleExceptionAsync(context, ex);

//                var code = HttpStatusCode.InternalServerError;
//                var result = string.Empty;
//                switch (ex)
//                {
//                    case ValidationException validationException:
//                        code = HttpStatusCode.BadRequest;
//                        result = JsonSerializer.Serialize(validationException.Value);
//                        break;
//                    case NotFoundException:
//                        code = HttpStatusCode.NotFound;
//                        break;
//                }
//                context.Response.ContentType = "application/json";
//                context.Response.StatusCode = (int)code;

//                if (result == string.Empty)
//                {
//                    result = JsonSerializer.Serialize(new { errpr = ex.Message });
//                }

//                return context.Response.WriteAsync(result);
//            }
//        }
//    }
//}
using Library_API.Exceptions;
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

        if (excpType == typeof(BadRequestException))
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (excpType == typeof(NotFoundException))
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

        var result = JsonSerializer.Serialize(new { message = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);

    }
}
