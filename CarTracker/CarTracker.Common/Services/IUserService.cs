using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels.Auth;

namespace CarTracker.Common.Services
{
    public interface IUserService
    {

        /// <summary>
        /// Fetches the user with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user or null if one doesn't exist</returns>
        User Get(long id);

        /// <summary>
        /// Fetches the user with the given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The user or null if one doesn't exist</returns>
        User Get(string username);

        /// <summary>
        /// Registers the given user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        User RegisterUser(UserRegistrationViewModel registration);

        /// <summary>
        /// Checks if the given username is available / unregistered
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if the usename is available, false otherwise</returns>
        bool IsUsernameAvailable(string username);
    }
}
