using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Primitives;

namespace CarTracker.Common.Services
{
    public interface IRequestLogger
    {

        /// <summary>
        /// Logs the given request
        /// </summary>
        /// <param name="clientAddress"></param>
        /// <param name="requestUrl"></param>
        /// <param name="requestMethod"></param>
        /// <param name="requestHeaders"></param>
        /// <param name="requestBody"></param>
        /// <param name="responseStatus"></param>
        /// <param name="responseHeaders"></param>
        /// <param name="responseBody"></param>
        void LogRequest(string clientAddress,
            string requestUrl,
            string requestMethod,
            IEnumerable requestHeaders,
            string requestBody,
            string responseStatus,
            IEnumerable responseHeaders,
            string responseBody);
    }
}
