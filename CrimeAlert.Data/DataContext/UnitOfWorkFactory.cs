using CrimeAlert.DataContracts;

namespace CrimeAlert.Data.DataContext
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISessionFactoryProvider sessionFactoryProvider;

        public UnitOfWorkFactory(ISessionFactoryProvider sessionFactoryProvider)
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
        }

        public IUnitOfWork New()
        {
            return new UnitOfWork(this.sessionFactoryProvider);
        }
    }
}
