using System;

namespace CrimeAlert.Services.Exceptions
{
    public class UploadServiceException : ServiceException
    {
        public UploadServiceException(string message)
            : base(message)
        {
        }

        public UploadServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
