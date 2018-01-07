using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface IRegistrationKeyService
    {

        UserRegistrationKey Get(long id);

        UserRegistrationKey Get(string key);

        PagedViewModel<UserRegistrationKey> GetAll(int skip, int take, SortParam sort);

        bool IsValid(string key);

        void UseKey(string keyValue, User user);

        UserRegistrationKey Create(UserRegistrationKeyViewModel toCreate);

        UserRegistrationKey Update(UserRegistrationKeyViewModel toUpdate);

    }
}
