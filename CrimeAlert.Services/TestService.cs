using System.Linq;
using CrimeAlert.DataContracts;
using CrimeAlert.ServiceContracts;

namespace CrimeAlert.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;

        public TestService(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public string GetTestValue(int id)
        {
            var firstOrDefault = testRepository.AsQueryable().Where(x => x.Id == id).ToList().FirstOrDefault();
            return firstOrDefault != null ? firstOrDefault.Value : null;
        }

        public string SetTestValue(int id, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
