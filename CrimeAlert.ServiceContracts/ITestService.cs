namespace CrimeAlert.ServiceContracts
{
    public interface ITestService
    {
        string GetTestValue(int id);
        string SetTestValue(int id, string value);
    }
}
