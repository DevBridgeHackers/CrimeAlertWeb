using NHibernate;

namespace CrimeAlert.DataContracts
{
    public interface ISessionFactoryProvider 
    {
        ISessionFactory SessionFactory { get; } 
    }
}
