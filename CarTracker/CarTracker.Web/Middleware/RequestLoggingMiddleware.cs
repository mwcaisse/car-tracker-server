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
            var requestBody = ReadRequestBody(context);
            var bodyStream = context.Response.Body;

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                await _next(context);
            }
            finally
            {
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                CreateLog(context, requestLogger, requestBody, responseBody);
              
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(bodyStream);
            }
        }

        protected void CreateLog(HttpContext context, IRequestLogger requestLogger, string requestBody, 
            string responseBody)
        {
            requestLogger.LogRequest(
               clientAddress: context.Connection.RemoteIpAddress.ToString(),
               requestUrl: context.Request.GetDisplayUrl(),
               requestMethod: context.Request.Method,
               requestHeaders: context.Request.Headers,
               requestBody: requestBody,
               responseStatus: context.Response.StatusCode.ToString(),
               responseHeaders: context.Response.Headers,
               responseBody: responseBody);
        }

        protected string ReadRequestBody(HttpContext context)
        {
            context.Request.EnableRewind();
            var body = new StreamReader(context.Request.Body).ReadToEnd();
            //Move the stream back to the begining
            context.Request.Body.Position = 0;
            return body;
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
