using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface IUserAuthenticationTokenService
    {

        /// <summary>
        /// Fetches the User Authentication Token with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserAuthenticationToken Get(long id);

        /// <summary>
        /// Fetches the active tokens for the give user and device uuid
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deviceUuid"></param>
        /// <returns></returns>
        IEnumerable<UserAuthenticationToken> GetActiveTokensForUserDevice(long userId, string deviceUuid);

            /// <summary>
        /// Fetches Active tokens for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<UserAuthenticationToken> GetActiveForUser(long userId, int skip, int take, 
            SortParam sort);

        /// <summary>
        /// Creates a token for the given user and device
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deviceUuid"></param>
        /// <returns></returns>
        string CreateToken(long userId, string deviceUuid);

        /// <summary>
        /// Updates the given User Authentication Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserAuthenticationToken Update(UserAuthenticationToken token);

        /// <summary>
        /// Records a log in using the given authentication token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserAuthenticationToken RecordUserLogin(UserAuthenticationToken token);

    }
}
