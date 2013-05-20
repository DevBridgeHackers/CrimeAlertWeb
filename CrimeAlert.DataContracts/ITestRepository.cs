using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataContracts
{
    public interface ITestRepository : IRepository<Test>, IUnitOfWorkRepository
    {

    }
}
