using System;

namespace CrimeAlert.DataContracts.Exceptions
{
    public class RecordAlreadyExistsException : DataException
    {
        public RecordAlreadyExistsException(string message)
            : base(message)
        {
        }

        public RecordAlreadyExistsException(Type entityType, object recordKey, Exception innerException)
            : base(string.Format("Cannot insert a duplicated record of type {0} with key {1}", entityType.Name, recordKey), innerException)
        {
        }

        public RecordAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
