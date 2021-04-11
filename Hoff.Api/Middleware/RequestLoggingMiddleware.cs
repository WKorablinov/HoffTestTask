using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Hoff.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context, ILogger<RequestLoggingMiddleware> logger)
        {
            await LogRequest(context, logger);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context, ILogger logger)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStream(requestStream)}");
            context.Request.Body.Position = 0;
        }

        private string ReadStream(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
