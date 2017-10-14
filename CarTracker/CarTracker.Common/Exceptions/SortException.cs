using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Exceptions
{
    public class SortException : Exception
    {

        public SortException()
        {
            
        }

        public SortException(string message)
            : base(message)
        {
            
        }

        public SortException(string message, Exception innerException) 
            : base(message, innerException)
        {
            
        }
    }
}
