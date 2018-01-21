using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;

namespace CarTracker.Web.Middleware
{
    public class RequestLoggingMiddleware
    {

        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRequestLogger requestLogger)
        {
            requestLogger.LogRequest(
                clientAddress: context.Connection.RemoteIpAddress.ToString(),
                requestUrl: context.Request.GetDisplayUrl(),
                requestMethod: context.Request.Method,
                requestBody: ReadRequestBody(context),
                responseStatus: context.Response.StatusCode.ToString(),
                responseBody: await ReadResponseBody(context));
        }

        protected string ReadRequestBody(HttpContext context)
        {
            context.Request.EnableRewind();
            var body = new StreamReader(context.Request.Body).ReadToEnd();
            //Move the stream back to the begining
            context.Request.Body.Position = 0;
            return body;
        }

        protected async Task<string> ReadResponseBody(HttpContext context)
        {
            var originalBody = context.Response.Body;
            var responseBody = "";
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _next(context);

                    memStream.Position = 0;
                    responseBody = new StreamReader(memStream).ReadToEnd();

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

            return responseBody;
        }

    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
