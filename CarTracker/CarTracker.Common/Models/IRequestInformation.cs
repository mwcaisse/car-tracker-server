using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Models
{
    public interface IRequestInformation
    {

        bool IsAuthenticated { get; }

        /// <summary>
        /// To avoid doing a null check when checking for a UserId this is a long.
        /// 
        /// Value will be -1 if the user isn't authenticated. Will only contain a valid value
        ///     if IsAuthenticated == true
        /// </summary>
        long UserId { get; }
        string Username { get; }
        string ClientAddress { get; }

    }
}
