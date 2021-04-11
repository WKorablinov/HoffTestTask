using System;
using System.Net;
using System.Threading.Tasks;

using Hoff.Api.Infrastructure.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hoff.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessLogicException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            var statusCode = (int)httpStatusCode;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(new ErrorDetails(statusCode, exception.Message).ToString());
        }
    }
}
