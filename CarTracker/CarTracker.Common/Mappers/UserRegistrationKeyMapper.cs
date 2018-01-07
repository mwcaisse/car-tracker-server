using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class UserRegistrationKeyMapper
    {
        public static UserRegistrationKeyViewModel ToViewModel(this UserRegistrationKey key)
        {
            var vm = new UserRegistrationKeyViewModel()
            {
                UsesRemaining = key.UsesRemaining,
                Active = key.Active,
                Key = key.Key,
                UserRegistrationKeyId = key.UserRegistrationKeyId
            };

            return vm;
        }

        public static IEnumerable<UserRegistrationKeyViewModel> ToViewModel(this
            IEnumerable<UserRegistrationKey> keys)
        {
            return keys.Select(k => k.ToViewModel());
        }

        public static PagedViewModel<UserRegistrationKeyViewModel> ToViewModel(this
            PagedViewModel<UserRegistrationKey> pagedKeys)
        {
            return new PagedViewModel<UserRegistrationKeyViewModel>()
            {
                Take = pagedKeys.Take,
                Skip = pagedKeys.Skip,
                Data = pagedKeys.Data.ToViewModel(),
                Total = pagedKeys.Total
            };
        }
    }
}
