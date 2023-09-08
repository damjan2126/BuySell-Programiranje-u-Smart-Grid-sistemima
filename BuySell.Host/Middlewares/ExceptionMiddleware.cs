using BuySell.Contracts.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace BuySell.Host.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (ValidationException exception)
            {
                _logger.LogError(exception, "Validation Exception");
                httpContext.Response.StatusCode = 400;
                var messages = exception.Errors.Select(x => x.ErrorMessage).ToList();
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = messages
                };
                await httpContext.Response.WriteAsJsonAsync(validationFailureResponse);
            }
            // Some custom Exceptions
            catch (NotFoundException ex)
            {

                _logger.LogError(ex, "Not Found Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.NotFound);
            }
            catch (InvalidParametersException ex)
            {
                _logger.LogError(ex, "Invalid Parameters Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.BadRequest);
            }
            catch (InvalidTokenException ex)
            {
                _logger.LogError(ex, "Invalid Token Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.Unauthorized);
            }
            catch (AlreadyExistsException ex)
            {

                _logger.LogError(ex, "Already Exists Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.BadRequest);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Database Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.InternalServerError);
            }
            catch (MethodNotAllowedException ex)
            {
                _logger.LogError(ex, "Method Not Allowed Exception");
                await HandleExceptionAsync(httpContext, ex, ex.Message,
                    HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                await HandleSomethingWentWrongAsync(httpContext, ex);
            }
        }

        private async Task HandleSomethingWentWrongAsync(HttpContext httpContext, Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(httpContext, ex);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string? message = null,
            HttpStatusCode? responseCode = null)
        {
            _logger.LogError(message, "Something went wrong");
            context.Response.StatusCode = responseCode.HasValue ? (int)responseCode : 500;
            context.Response.ContentType = "application/json";
            var response = new
            {
                Message = message ?? $"Nešto nije u redu {exception.Message}",
                StackTrace = exception.StackTrace
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
    public static class ExceptionMIddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
