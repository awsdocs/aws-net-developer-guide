--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/BannerButton.png) ](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Configure AWS credentials<a name="net-dg-config-creds"></a>

After you [create an AWS account](net-dg-signup.md) and [create the required user accounts](net-dg-users-roles.md), you can manage credentials for those user accounts\. You need these credentials to perform many of the tasks and examples in this guide\.

The following is a high\-level process for credential management and use\.

1. Create the credentials you need:
   + You can create credentials when you're creating a user account\. See [User accounts](net-dg-users-roles.md#net-dg-users-roles-user) for an example\.
   + You can also create credentials for an existing user account\. See [Managing access keys for IAM users](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html#Using_CreateAccessKey)\.

1. Store the credentials \(for example, in the [shared AWS credentials file](creds-file.md) or the [SDK Store](sdk-store.md)\)\.

1. Configure your project so that your application can [find the credentials](creds-assign.md)\.

The following topics provide information you can use to determine how to manage and use credentials in your environment\.

**Topics**
+ [Important warnings and guidelines](net-dg-config-creds-warnings-and-guidelines.md)
+ [Using the shared AWS credentials file](creds-file.md)
+ [Using the SDK Store \(Windows only\)](sdk-store.md)
+ [Credential and profile resolution](creds-assign.md)