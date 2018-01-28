using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Models
{
    public interface IRequestInformation
    {

        bool IsAuthenticated { get; }
        long? UserId { get; }
        string Username { get; }
        string ClientAddress { get; }

    }
}
