using System.Linq;
using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public User GetUserByToken(string authToken)
        {
            return AsQueryable().FirstOrDefault(u => u.AuthToken == authToken);
        }

        public User GetUser(int userId)
        {
            return AsQueryable().FirstOrDefault(u => u.Id == userId);
        }
    }
}
