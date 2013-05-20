using CrimeAlert.ServiceContracts;
using Moq;
using NUnit.Framework;

namespace CrimeAlert.Tests.ServiceTests.TestService
{
    [TestFixture]
    public class GetDataTests
    {
        // TODO fix
        [Test]
        public void Should_Fail_On_Wrong_Result()
        {
            var mockTestService = new Mock<ITestService>();
            mockTestService
                .Setup(service => service.GetTestValue(1))
                .Returns("Test data from database");
        }
    }
}