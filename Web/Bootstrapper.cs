using System.Web.Http;
using Configurations;
using CustomClient;
using Dal;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Web
{
    public class Bootstrapper
    {
        public static Container Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.RegisterSingleton<Configuration>();
            container.RegisterSingleton<IConfiguration>(container.GetInstance<Configuration>);
            container.RegisterSingleton<IClient, Client>();

            container.RegisterSingleton<IDataBaseAccessor,DataBaseAccessor>();

            RepositoryComponentRegistration.RegisterRepositories(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }
    }
}