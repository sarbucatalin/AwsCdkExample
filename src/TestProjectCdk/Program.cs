using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new TestProjectCdkStack(app, AWSConstants.STACK_NAME, AWSProps.Create());
            
            app.Synth();
        }
    }
}
