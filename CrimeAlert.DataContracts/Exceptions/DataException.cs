using System;

namespace CrimeAlert.DataContracts.Exceptions
{
    public class DataException : KnownException
    {
        public DataException(string message)
            : base(message)
        {
        }

        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        } 
    }
}
