using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Auth;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Logic.Services
{
    public class UserAuthenticationTokenService : IUserAuthenticationTokenService
    {

        private readonly CarTrackerDbContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticationTokenService(CarTrackerDbContext db, IPasswordHasher passwordHasher)
        {
            this._db = db;
            this._passwordHasher = passwordHasher;
        }

        public UserAuthenticationToken Get(long id)
        {
            return _db.UserAuthenticationTokens.FirstOrDefault(ut => ut.UserAuthenticationTokenId == id);
        }

        public IEnumerable<UserAuthenticationToken> GetActiveTokensForUserDevice(long userId, string deviceUuid)
        {
            return _db.UserAuthenticationTokens
                .Active()
                .Where(ut => ut.UserId == userId && ut.DeviceUuid == deviceUuid);
        }

        public PagedViewModel<UserAuthenticationToken> GetActiveForUser(long userId, int skip, int take, SortParam sort)
        {
            return _db.UserAuthenticationTokens
                .Active()
                .Where(ut => ut.UserId == userId)
                .PageAndSort(skip, take, sort);
        }

        public string CreateToken(long userId, string deviceUuid)
        {

            var user = _db.Users.Active().FirstOrDefault(u => u.UserId == userId);
            if (null == user)
            {
                throw new EntityValidationException("Cannot create Authentication Token. User does not exist.");
            }
            if (string.IsNullOrWhiteSpace(deviceUuid))
            {
                throw new EntityValidationException("Cannot create Authentication Token. DeviceUuid cannot be null.");
            }

            var token = CreateRandomTokenValue();
            var authToken = new UserAuthenticationToken()
            {
                User = user,
                Active = true,
                DeviceUuid = deviceUuid,
                Token = _passwordHasher.HashPassword(token)
            };
            _db.UserAuthenticationTokens.Add(authToken);
            _db.SaveChanges();

            return token;
        }

        private string CreateRandomTokenValue()
        {
            return Guid.NewGuid().ToString();
        }

        public UserAuthenticationToken Update(UserAuthenticationToken token)
        {
            _db.Entry(token).State = EntityState.Modified;
            _db.SaveChanges();
            return token;
        }
    }
}
