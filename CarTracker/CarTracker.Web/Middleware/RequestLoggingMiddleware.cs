using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        protected string GetClientAddress(HttpContext context)
        {
            // If the server is behind a proxy, the X-Forwaded-For header will be set and contain
            // the actual client URL
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                return headers["X-Forwarded-For"];
            }
            return context.Connection.RemoteIpAddress.ToString();
        }

        protected string GetUrl(HttpContext context)
        {
            var headers = context.Request.Headers;
            var requestUrl = context.Request.GetDisplayUrl();
            var regex = new Regex(@"^http(s)*://localhost([:0-9])*/", RegexOptions.IgnoreCase);
            
            if (headers.ContainsKey("X-Forwarded-Proto") && headers.ContainsKey("X-Forwarded-Host") &&
                regex.IsMatch(requestUrl))
            {
                return String.Format("{0}://{1}/{2}",
                    headers["X-Forwarded-Proto"],
                    headers["X-Forwarded-Host"], 
                    regex.Replace(requestUrl, ""));
            }
            return context.Request.GetDisplayUrl();
        }

        protected void CreateLog(HttpContext context, IRequestLogger requestLogger, string requestBody, 
            string responseBody)
        {
            requestLogger.LogRequest(
               clientAddress: GetClientAddress(context),
               requestUrl: GetUrl(context),
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
