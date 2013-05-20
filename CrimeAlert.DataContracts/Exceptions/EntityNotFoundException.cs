﻿using System;

namespace CrimeAlert.DataContracts.Exceptions
{
    public class EntityNotFoundException : DataException
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityNotFoundException(Type entityType, int id)
            : base(string.Format("{0} entity not found by id={1}.", entityType.Name, id))
        {
        }

        public EntityNotFoundException(Type entityType, string filter)
            : base(string.Format("{0} entity not found by filter {1}.", entityType.Name, filter))
        {
        } 
    }
}
