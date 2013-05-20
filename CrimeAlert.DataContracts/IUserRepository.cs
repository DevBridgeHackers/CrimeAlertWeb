using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataContracts
{
    public interface IUserRepository : IRepository<User>, IUnitOfWorkRepository
    {
        User GetUserByToken(string authToken);
        User GetUser(int userId);
    }
}
