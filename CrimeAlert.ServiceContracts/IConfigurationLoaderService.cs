using System.Configuration;

namespace CrimeAlert.ServiceContracts
{
    public interface IConfigurationLoaderService
    {
        T LoadConfig<T>() where T : ConfigurationSection;
    }
}
