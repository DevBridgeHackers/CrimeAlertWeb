using CrimeAlert.Data.DataContext.Conventions;
using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ForeignKey = FluentNHibernate.Conventions.Helpers.ForeignKey;

namespace CrimeAlert.Data.DataContext
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly static object LockObject = new object();

        private volatile ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            get
            {
                if (this.sessionFactory == null)
                {
                    lock (LockObject)
                    {
                        if (this.sessionFactory == null)
                        {
                            this.sessionFactory = CreateSessionFactory();
                        }
                    }
                }

                return this.sessionFactory;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(config => config.FromConnectionStringWithKey("ApplicationServices")))
                .Mappings(mappingConfiguration => mappingConfiguration.FluentMappings
                                   .AddFromAssemblyOf<IEntity>()
                                   .Conventions.Add(ForeignKey.EndsWith("Id"))
                                   .Conventions.Add<EnumConvention>())
                .BuildConfiguration()
                .BuildSessionFactory();
        }
    }
}
