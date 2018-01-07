﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Auth;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class UserService : IUserService
    {

        private readonly CarTrackerDbContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(CarTrackerDbContext db, IPasswordHasher passwordHasher)
        {
            this._db = db;
            this._passwordHasher = passwordHasher;
        }

        public User Get(long id)
        {
            return _db.Users.FirstOrDefault(x => x.UserId == id);
        }

        public User Get(string username)
        {
            return _db.Users.FirstOrDefault(x => x.Username == username);
        }

        public User RegisterUser(UserRegistrationViewModel registration)
        {
            //TODO: Add Registration Key check
            ValidateRegistration(registration);
            User user = new User()
            {
                Username = registration.Username,
                Email = registration.Email,
                Password = _passwordHasher.HashPassword(registration.Password),
                Name = registration.Name,
                Active = true,
                Locked = false
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }

        public bool IsUsernameAvailable(string username)
        {
            return ! _db.Users.Any(x => x.Username == username);
        }

        public UserAuthenticationToken CreateAuthenticationToken(long userId, string deviceUuid)
        {
            throw new NotImplementedException();
        }

        protected void ValidateRegistration(UserRegistrationViewModel registration)
        {
            if (string.IsNullOrWhiteSpace(registration.Username))
            {
                throw new EntityValidationException("Username cannot be blank!");
            }
            if (!IsUsernameAvailable(registration.Username))
            {
                throw new EntityValidationException("Username is not available!");
            }
            if (string.IsNullOrWhiteSpace(registration.Password))
            {
                throw new EntityValidationException("Password must not be blank!");
            }
            if (registration.Password.Length < 8)
            {
                throw new EntityValidationException("Password must be at least 8 characters long.");
            }
            if (string.IsNullOrWhiteSpace(registration.Email))
            {
                throw new EntityValidationException("Email must not be blank!");
            }
        }
         
    }
}
