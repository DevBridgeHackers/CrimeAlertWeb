
namespace CrimeAlert.DataEntities.Entities
{
    public class User : EntityBase
    {
        public virtual string AuthToken { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
    }
}