using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services
{
    public class RegistrationKeyService : IRegistrationKeyService
    {

        private CarTrackerDbContext _db;

        public RegistrationKeyService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public UserRegistrationKey Get(long id)
        {
            return _db.UserRegistrationKeys.FirstOrDefault(x => x.UserRegistrationKeyId == id);
        }

        public UserRegistrationKey Get(string key)
        {
            return _db.UserRegistrationKeys.FirstOrDefault(x => x.Key == key);
        }

        public PagedViewModel<UserRegistrationKey> GetAll(int skip, int take, SortParam sort)
        {
            return _db.UserRegistrationKeys.PageAndSort(skip, take, sort);
        }

        public bool IsValid(string key)
        {
            var regKey = Get(key);
            if (null != regKey)
            {
                return regKey.Active && regKey.UsesRemaining > 0;
            }
            return false;
        }

        public void UseKey(string keyValue, User user)
        {
            if (!IsValid(keyValue))
            {
                throw new EntityValidationException("Invalid Registration Key.");
            }
            var key = Get(keyValue);

            key.UsesRemaining--;
            
            key.UserRegistrationKeyUses.Add(new UserRegistrationKeyUse()
            {
                User = user,
                UserRegistrationKey = key
            });

            _db.SaveChanges();

        }

        public UserRegistrationKey Create(UserRegistrationKeyViewModel model)
        {
            if (null != Get(model.Key))
            {
                throw new EntityValidationException("A registration key with the given value already exists.");
            }

            var toCreate = new UserRegistrationKey()
            {
                UsesRemaining = model.UsesRemaining,
                Active = model.Active,
                Key = model.Key
            };

            _db.UserRegistrationKeys.Add(toCreate);
            _db.SaveChanges();

            return toCreate;
        }

        public UserRegistrationKey Update(UserRegistrationKeyViewModel model)
        {
            var toUpdate = Get(model.UserRegistrationKeyId);

            if (null == toUpdate)
            {
                throw new EntityValidationException("No registration key with that ID exists!");
            }
            if (!string.Equals(toUpdate.Key, model.Key, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new EntityValidationException("You cannot changed the key value!");
            }

            toUpdate.Active = model.Active;
            toUpdate.UsesRemaining = model.UsesRemaining;

            return toUpdate;
        }
    }
}
