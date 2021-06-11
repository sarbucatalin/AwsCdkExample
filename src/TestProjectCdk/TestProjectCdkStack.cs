using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;

namespace TestProjectCdk
{
    public class TestProjectCdkStack : Stack
    {
        internal TestProjectCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var blogPostTable = new Table(this, AWSConstants.STACK_NAME, new TableProps
            {
                TableName = AWSConstants.TABLE_NAME,
                BillingMode = BillingMode.PAY_PER_REQUEST,
                PartitionKey = new Amazon.CDK.AWS.DynamoDB.Attribute
                {
                    Name = "Id",
                    Type = AttributeType.STRING
                },
                RemovalPolicy = RemovalPolicy.DESTROY
            });
            
            
            var lambdaPackage = Path.Combine(System.Environment.CurrentDirectory, "dist/LambdaPackage.zip");
            
            var saveBlogPostLambda = CreateFunction("postBlog", lambdaPackage,
                "TestProjectCdk.BlogApi::TestProjectCdk.BlogApi.BlogHandlers::PostBlog", 45);
            
            blogPostTable.AllowReadWrite(saveBlogPostLambda);
            
            
            var restApi = new RestApi(this, AWSConstants.API_NAME, new RestApiProps
            {
                Deploy = true,
                Description = "Api endpoints for the Blogging System",
                RestApiName = "blog-api"
            });
            var blogResource = restApi.Root.AddResource("blog");
            var postBlogIntegration = new LambdaIntegration(saveBlogPostLambda, new LambdaIntegrationOptions());
            blogResource.AddMethod("POST", postBlogIntegration);
        }
        
        private Function CreateFunction(string name, string lambdaPackage, string handler, int timeout)
        {
            return new Function(this, name, new FunctionProps
            {
                Code = Code.FromAsset(lambdaPackage),
                Runtime = Runtime.DOTNET_CORE_3_1,
                FunctionName = name,
                Handler = handler,
                Timeout = Duration.Seconds(timeout),
                
            });
        }
    }
}
