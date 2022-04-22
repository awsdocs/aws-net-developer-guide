--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Setting up your AWS SDK for \.NET project<a name="net-dg-config"></a>

In addition to [setting up your environment](net-dg-setup.md), you need to configure each project that you create\.

There are a few essential things your application needs to access AWS services through the AWS SDK for \.NET:
+ An appropriate user account or role
+ Credentials for that user account or to assume that role
+ Specification of the AWS Region
+ AWSSDK packages or assemblies

Some of the topics in this section provide information about how to configure these essential things\.

Other topics in this section and other sections provide information about more advanced ways that you can configure your project\.

**Topics**
+ [Start a new project](net-dg-start-new-project.md)
+ [Create users and roles](net-dg-users-roles.md)
+ [Configure AWS credentials](net-dg-config-creds.md)
+ [Configure the AWS Region](net-dg-region-selection.md)
+ [Install AWSSDK packages with NuGet](net-dg-install-assemblies.md)
+ [Install AWSSDK assemblies without NuGet](net-dg-install-without-nuget.md)
+ [Advanced configuration](net-dg-advanced-config.md)