using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BirthdayApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal Server Error.";

            // Customize error handling based on specific exceptions if needed
            if (exception is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "Unauthorized.";
            }
            else
            {
                message = exception.Message;
            }

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = message
            });

            return context.Response.WriteAsync(result);
        }

    }
}
