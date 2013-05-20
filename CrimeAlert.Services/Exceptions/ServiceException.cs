using System;

namespace CrimeAlert.Services.Exceptions
{
    public class ServiceException : ApplicationException
    {
        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
