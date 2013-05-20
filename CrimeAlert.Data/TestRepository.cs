using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.Data
{
    public class TestRepository : RepositoryBase<Test>, ITestRepository
    {
        public TestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

    }
}
