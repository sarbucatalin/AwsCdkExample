using Microsoft.Extensions.DependencyInjection;
using TestProjectCdk.PostBlogEndpoint.Config;

namespace TestProjectCdk.PostBlogEndpoint
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEnvironmentService, EnvironmentService>();
            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();
        }
    }
}