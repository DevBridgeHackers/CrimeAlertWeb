using System;

namespace CrimeAlert.DataEntities
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}