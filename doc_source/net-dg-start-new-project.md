--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Start a new project<a name="net-dg-start-new-project"></a>

There are several techniques you can use to start a new project to access AWS services\. The following are some of those techniques:
+ If you're new to \.NET development on AWS or at least new to the AWS SDK for \.NET, you can see complete examples in [Quick tour](quick-start.md)\. It gives you an introduction to the SDK\.
+ You can start a basic project by using the \.NET CLI\. To see an example of this, open a command prompt or terminal, create a folder or directory and navigate to it, and then enter the following\.

  ```
  dotnet new console --name [SOME-NAME]
  ```

  An empty project is created to which you can add code and NuGet packages\. For more information, see the [\.NET Core guide](https://docs.microsoft.com/en-us/dotnet/core/)\.

  To see a list of project templates, use the following: `dotnet new --list`
+ The AWS Toolkit for Visual Studio includes C\# project templates for a variety of AWS services\. After you [install the toolkit](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/setup.html) in Visual Studio, you can access the templates while creating a new project\.

  To see this, go to [Working with AWS services](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/working-with-services.html) in the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\. Several of the examples in that section create new projects\.
+ If you develop with Visual Studio on Windows but without the AWS Toolkit for Visual Studio, use your typical techniques for creating a new project\.

  To see an example, open Visual Studio and choose **File**, **New**, **Project**\. Search for "\.net core" and choose the C\# version of the **Console App \(\.NET Core\)** or **WPF App \(\.NET Core\)** template\. An empty project is created to which you can add code and NuGet packages\.

After you create your project, perform additional appropriate tasks for [setting up your project](net-dg-config.md)\.

You can find some examples of how to work with AWS services in [Code examples with guidance](tutorials-examples.md)\.