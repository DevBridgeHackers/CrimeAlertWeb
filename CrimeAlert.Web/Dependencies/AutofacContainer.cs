using Autofac;
using Autofac.Integration.Mvc;
using CrimeAlert.Data;
using CrimeAlert.Data.DataContext;
using CrimeAlert.Services;

namespace CrimeAlert.Web.Dependencies
{
    public class AutofacContainer
    {
        public static IContainer CreateDependenciesContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterType<SessionFactoryProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder
                .RegisterType<UnitOfWorkFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder
                .RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            // services
            containerBuilder
                .RegisterType<TestService>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder
                .RegisterType<ConfigurationLoaderService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder
                .RegisterType<UploadService>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder
                .RegisterType<UserService>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder
                .RegisterType<ReportService>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            // repositories
            containerBuilder
                .RegisterType<TestRepository>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder
                .RegisterType<UserRepository>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder
                .RegisterType<ReportRepository>()
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            containerBuilder.RegisterControllers(typeof(AutofacContainer).Assembly);

            return containerBuilder.Build();
        }
    }
}