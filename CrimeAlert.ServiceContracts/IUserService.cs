using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.ServiceContracts
{
    public interface IUserService
    {
        User GetUser(string authToken);
        User GetUser(int userId);
        User AddUser(string authToken, string firstName, string lastName, string email);
        void Test();
    }
}
