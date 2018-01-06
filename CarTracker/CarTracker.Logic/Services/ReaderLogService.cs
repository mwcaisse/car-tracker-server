using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services
{
    public class ReaderLogService : IReaderLogService
    {

        private readonly CarTrackerDbContext _db;

        public ReaderLogService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<ReaderLog> GetAll()
        {
            return _db.ReaderLogs.ToList();
        }

        public PagedViewModel<ReaderLog> GetAllPaged(int skip = 0, int take = 10, SortParam sortParam = null)
        {
            if (string.IsNullOrWhiteSpace(sortParam?.ColumnName))
            {
                sortParam = new SortParam()
                {
                    ColumnName = "CreateDate",
                    Ascending = false
                };
            }

            return _db.ReaderLogs.PageAndSort(skip, take, sortParam);
        }

        public ReaderLog Get(long id)
        {
            return _db.ReaderLogs.FirstOrDefault(rl => rl.ReaderLogId == id);
        }

        public ReaderLog Create(ReaderLogViewModel toCreate)
        {
            ValidateReaderLog(toCreate);

            var readerLog = new ReaderLog()
            {
                Date = toCreate.Date,
                Message = toCreate.Message,
                Type = toCreate.Type
            };

            _db.ReaderLogs.Add(readerLog);
            _db.SaveChanges();

            return readerLog;
        }

        public ReaderLog Update(ReaderLogViewModel toUpdate)
        {
            ValidateReaderLog(toUpdate);
            var readerLog = _db.ReaderLogs.FirstOrDefault(rl => rl.ReaderLogId == toUpdate.Id);
            if (null == readerLog)
            {
                throw new EntityValidationException("Reader log does not exist.");
            }

            readerLog.Date = toUpdate.Date;
            readerLog.Message = toUpdate.Message;
            readerLog.Type = toUpdate.Type;

            _db.SaveChanges();

            return readerLog;
        }

        public IEnumerable<BulkUploadResultViewModel> BulkUpload(
            IEnumerable<BulkUploadViewModel<ReaderLogViewModel>> readerLogs)
        {
            var results = new List<BulkUploadResultViewModel>();

            foreach (var uploadVm in readerLogs)
            {
                var result = new BulkUploadResultViewModel
                {
                    Uuid = uploadVm.Uuid
                };
                try
                {
                    var res = Create(uploadVm.Data);
                    result.Id = res.ReaderLogId;
                    result.Successful = true;
                }
                catch (Exception e)
                {
                    result.Successful = false;
                    result.ErrorMessage = e.Message;
                }

                results.Add(result);
            }

            return results;
        }

        private void ValidateReaderLog(ReaderLogViewModel readerLog)
        {
            if (string.IsNullOrWhiteSpace(readerLog.Message))
            {
                throw new EntityValidationException("The message cannot be blank.");
            }
        }
    }
}
