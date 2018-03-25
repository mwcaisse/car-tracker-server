using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services.Places
{
    public interface IUserPlaceService
    {

        /// <summary>
        /// Fetches the user place with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserPlace Get(long id);

        /// <summary>
        /// Gets all user places for the current user
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<UserPlace> GetMine(int skip, int take, SortParam sort);

        /// <summary>
        /// Creates a user place
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        UserPlace Create(UserPlaceViewModel toCreate);

        /// <summary>
        /// Updates the given user place
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        UserPlace Update(UserPlaceViewModel toUpdate);

        /// <summary>
        /// Deletes the user place with the given id
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

    }
}
