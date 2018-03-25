using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Common.Services.Places;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services.Places
{
    public class UserPlaceService : IUserPlaceService
    {

        private readonly CarTrackerDbContext _db;
        private readonly IRequestInformation _requestInformation;

        public UserPlaceService(CarTrackerDbContext db, IRequestInformation requestInformation)
        {
            _db = db;
            _requestInformation = requestInformation;
        }

        public UserPlace Get(long id)
        {
            return _db.UserPlaces.Build()
                                 .Active()
                                 .FirstOrDefault(x => x.UserPlaceId == id && 
                                                      x.OwnerId == _requestInformation.UserId);
        }

        public PagedViewModel<UserPlace> GetMine(int skip, int take, SortParam sort)
        {
            return _db.UserPlaces.Build()
                                 .Active()
                                 .Where(x => x.OwnerId == _requestInformation.UserId)
                                 .PageAndSort(skip, take, sort, new SortParam()
                                 {
                                     ColumnName = "CreateDate",
                                     Ascending = true
                                 });
        }

        public UserPlace Create(UserPlaceViewModel toCreate)
        {
            ValidateUserPlace(toCreate);
            var userPlace = new UserPlace()
            {
                Place = new Place()
                {
                    Name = toCreate.Name,
                    Latitude = toCreate.Latitude,
                    Longitude = toCreate.Longitude,
                },
                OwnerId = _requestInformation.UserId ?? 0,
                Active = true
            };
            _db.UserPlaces.Add(userPlace);
            _db.SaveChanges();
            return userPlace;
        }

        public UserPlace Update(UserPlaceViewModel toUpdate)
        {
            var userPlace = Get(toUpdate.UserPlaceId);
            if (null == userPlace)
            {
                throw new EntityValidationException("User Place doesn't exist");
            }
            ValidateUserPlace(toUpdate);

            userPlace.Place.Name = toUpdate.Name;
            userPlace.Place.Longitude = toUpdate.Longitude;
            userPlace.Place.Latitude = toUpdate.Latitude;

            _db.SaveChanges();

            return userPlace;
        }

        public void Delete(long id)
        {
            var userPlace = Get(id);
            if (null != userPlace)
            {
                userPlace.Active = false;
                _db.SaveChanges();
            }
            else
            {
                throw new EntityValidationException("User Place doesn't exist.");
            }
        }

        protected void ValidateUserPlace(UserPlaceViewModel userPlace)
        {
            if (string.IsNullOrWhiteSpace(userPlace.Name))
            {
                throw new EntityValidationException("Name is a required field.");
            }
        }
    }
}
