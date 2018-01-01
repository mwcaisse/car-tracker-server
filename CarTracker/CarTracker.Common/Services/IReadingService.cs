using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface IReadingService
    {

        /// <summary>
        /// Gets a reading by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Reading Get(long id);

        /// <summary>
        /// Gets all of the readings for a given trip
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        IEnumerable<Reading> GetForTrip(long tripId);

        /// <summary>
        /// Creates a series of Readings and records the results for each one
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="readings"></param>
        /// <returns></returns>
        IEnumerable<BulkUploadResultViewModel> BulkUpload(long tripId,
            IEnumerable<BulkUploadViewModel<ReadingViewModel>> readings);
    }
}
