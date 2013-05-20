using System;

namespace CrimeAlert.DataEntities.Entities
{
    public class EntityBase : IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime? DeletedOn { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Id: {1}", GetType().FullName, Id);
        }
    }
}