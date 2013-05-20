using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataEntities.Mappings
{
    public class UserMap : BaseMap<User>
    {
        public UserMap()
        {
            Table("Users");

            Map(x => x.AuthToken);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
        }
    }
}