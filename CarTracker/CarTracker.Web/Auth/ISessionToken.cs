using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CarTracker.Web.Auth
{
    public interface ISessionToken
    {
        string Id { get; }
        string Username { get; }
        AuthenticationTicket Ticket { get; }
        bool Expired { get; }
    }
}
