using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuntoTechnicalTest.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (AuthorizationException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleAuthorizationExceptionAsync(context, ex);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var json = JsonSerializer.Serialize(new ValidationProblemDetails(exception.Erros)
            {
                Instance = $"{context.Request.Method} {context.Request.Scheme}//{context.Request.Host}{context.Request.Path.Value}",
                Status = context.Response.StatusCode,
                //Detail = exception.StackTrace,
                Title = exception.Message,
            });

            return context.Response.WriteAsync(json);
        }
        private Task HandleAuthorizationExceptionAsync(HttpContext context, AuthorizationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;            

            return context.Response.WriteAsync(exception.Message);
        }
    }
}
