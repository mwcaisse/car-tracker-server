using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
        /// <param name="requestBody"></param>
        /// <param name="responseStatus"></param>
        /// <param name="responseBody"></param>
        void LogRequest(string clientAddress,
            string requestUrl,
            string requestMethod,
            string requestBody,
            string responseStatus,
            string responseBody);
    }
}
