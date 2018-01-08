using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Auth;
using CarTracker.Common.Services;
using Microsoft.AspNetCore.Identity;

namespace CarTracker.Web.Auth
{
    public class UserAuthenticationManager
    {

        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticationManager(IUserService userService, IPasswordHasher passwordHasher)
        {
            this._userService = userService;
            this._passwordHasher = passwordHasher;
        }

        public bool LoginPassword(string username, string password)
        {
            
            var user = _userService.Get(username);
            if (null != user)
            {
                return _passwordHasher.VerifyPassword(user.Password, password);
            }
            //If no user with the given username exists, still run the password hasher 
            // to avoid timing attacks
            _passwordHasher.VerifyPassword("", password);
            return false;
        }

        public bool LoginToken(string username, string token)
        {
            return true; // for now lets just always return true. Most secure, I promise.
        }
    }
}
