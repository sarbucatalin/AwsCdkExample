using Microsoft.Extensions.Configuration;

namespace TestProjectCdk.PostBlogEndpoint.Config
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}