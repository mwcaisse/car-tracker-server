using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.ViewModels;
using CarTracker.Common.ViewModels.Auth;

namespace CarTracker.Common.Mappers.Auth
{
    public static class UserRegistrationKeyUseMapper
    {
        public static UserRegistrationKeyUseViewModel ToViewModel(this
            UserRegistrationKeyUse use)
        {
            var vm = new UserRegistrationKeyUseViewModel()
            {
                Id = use.UserRegistrationKeyUseId,
                UserRegistrationKeyId = use.UserRegistrationKeyId,
                CreateDate = use.CreateDate,
                User = use.User.ToViewModel()
            };

            return vm;
        }

        public static IEnumerable<UserRegistrationKeyUseViewModel> ToViewModel(this
            IEnumerable<UserRegistrationKeyUse> uses)
        {
            if (null == uses)
            {
                return new List<UserRegistrationKeyUseViewModel>();
            }
            return uses.Select(u => u.ToViewModel());
        }

        public static PagedViewModel<UserRegistrationKeyUseViewModel> ToViewModel(this
            PagedViewModel<UserRegistrationKeyUse> pagedUses)
        {
            return new PagedViewModel<UserRegistrationKeyUseViewModel>()
            {
                Take = pagedUses.Take,
                Skip = pagedUses.Skip,
                Data = pagedUses.Data.ToViewModel(),
                Total = pagedUses.Total
            };
        }

    }
}
