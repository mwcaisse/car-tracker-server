using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface IReaderLogService
    {

        /// <summary>
        /// Fetches all reader logs
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReaderLog> GetAll();

        /// <summary>
        /// Fetches all reader logs with the given paging + sorting
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortParam"></param>
        /// <returns></returns>
        PagedViewModel<ReaderLog> GetAllPaged(int skip = 0, int take = 10, SortParam sortParam = null);

        /// <summary>
        /// Fetches the reader log with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReaderLog Get(long id);

        /// <summary>
        /// Creates the given reader log
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        ReaderLog Create(ReaderLogViewModel toCreate);


        /// <summary>
        /// Updates the given reader log from the view model
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        ReaderLog Update(ReaderLogViewModel toUpdate);

        /// <summary>
        /// Creates reader logs in bulk
        /// </summary>
        /// <param name="readerLogs"></param>
        /// <returns></returns>
        IEnumerable<BulkUploadResultViewModel> BulkUpload(
            IEnumerable<BulkUploadViewModel<ReaderLogViewModel>> readerLogs);
    }
}
