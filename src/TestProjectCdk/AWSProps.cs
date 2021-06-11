using System.Collections.Generic;
using Amazon.CDK;

namespace TestProjectCdk
{
    public static class AWSProps
    {
        public static IStackProps Create()
        {
            return new StackProps
            {
                StackName = AWSConstants.STACK_NAME,
                Env = new Environment
                {
                    Region = "eu-central-1",
                    Account = "683811971057"
                },
                Tags = new Dictionary<string, string>
                {
                    {"purpose", "Learn about AWS CDK"},
                    {"cost-code", "1"},
                }
            };
        }
    }
}