using System;

namespace CrimeAlert.Services.Exceptions
{
    public class ReportServiceException : ServiceException
    {
        public ReportServiceException(string message)
            : base(message)
        {
        }

        public ReportServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
