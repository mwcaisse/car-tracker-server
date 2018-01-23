using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Services
{
    public interface IServerLogger
    {

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Warn(string message, Exception e);

        void Warn(Exception e);

        void Error(string message);

        void Error(string message, Exception e);

        void Error(Exception e);
    }
}
