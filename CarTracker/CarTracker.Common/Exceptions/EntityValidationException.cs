using System;

namespace CarTracker.Common.Exceptions
{
    public class EntityValidationException : Exception
    {

        public EntityValidationException() { }

        public EntityValidationException(string message) : 
            base(message) { }

        public EntityValidationException(string message, Exception innerException) : 
            base (message, innerException) { }
        
    }
}
