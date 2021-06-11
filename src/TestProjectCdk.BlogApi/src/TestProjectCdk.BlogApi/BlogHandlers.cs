using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestProjectCdk.PostBlogEndpoint.Config;


namespace TestProjectCdk.PostBlogEndpoint
{
    public class BlogHandlers
    {
        public IConfigurationService ConfigService { get; }

        public BlogHandlers()
        {
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            Startup.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get Configuration Service from DI system
            ConfigService = serviceProvider.GetService<IConfigurationService>();
        }

        public BlogHandlers(IConfigurationService configService)
        {
            ConfigService = configService;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public Task<APIGatewayProxyResponse> PostBlog(APIGatewayProxyRequest request)
        {
            var model = JsonConvert.DeserializeObject<PostBlogModel>(request.Body);
            return Task.FromResult(new APIGatewayProxyResponse
            {
                Body =  JsonConvert.SerializeObject(model)
            });
        }
    }
}