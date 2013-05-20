using System;

namespace CrimeAlert.DataContracts.Exceptions
{
    public abstract class KnownException : ApplicationException
    {
        protected KnownException(string message)
            : base(message)
        {
        }

        protected KnownException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
