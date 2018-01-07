using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels;
using CarTracker.Common.ViewModels.Auth;

namespace CarTracker.Common.Services
{
    public interface IRegistrationKeyService
    {

        UserRegistrationKey Get(long id);

        UserRegistrationKey Get(string key);

        PagedViewModel<UserRegistrationKey> GetAll(int skip, int take, SortParam sort);

        bool IsValid(string key);

        bool UseKey(string keyValue, User user);

        UserRegistrationKey Create(UserRegistrationKeyViewModel model);

        UserRegistrationKey Update(UserRegistrationKeyViewModel model);

    }
}
