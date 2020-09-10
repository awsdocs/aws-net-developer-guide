--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Start a new project<a name="net-dg-start-new-project"></a>

There are several techniques you can use to start a new project to access AWS services\. The following are some of those techniques:
+ If you're new to \.NET development on AWS or at least new to the AWS SDK for \.NET, you can see complete examples in [Quick start](quick-start.md)\. It gives you an introduction to the SDK\.
+ You can start a basic project by using the \.NET CLI\. To see an example of this, open a command prompt or terminal, create a folder or directory and navigate to it, and then enter the following\.

  ```
  dotnet new console --name [SOME-NAME]
  ```

  An empty project is created to which you can add code and NuGet packages\. For more information, see the [\.NET Core guide](https://docs.microsoft.com/en-us/dotnet/core/)\.
+ The AWS Toolkit for Visual Studio includes C\# project templates for a variety of AWS services\. After you [install the toolkit](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/setup.html) in Visual Studio, you can access the templates while creating a new project\.

  To see this, go to [Working with AWS services](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/working-with-services.html) in the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\. Several of the examples in that section create new projects\.
+ If you develop with Visual Studio on Windows but without the AWS Toolkit for Visual Studio, use your typical techniques for creating a new project, but be sure to choose a \.NET Core template\.

  To see an example, open Visual Studio and choose **File**, **New**, **Project**\. Search for "\.net core" and choose the C\# version of the **Console App \(\.NET Core\)** template\. An empty project is created to which you can add code and NuGet packages\.

After you create your project, perform additional appropriate tasks for [setting up your project](net-dg-config.md)\.

You can find some examples of how to work with AWS services in [Working with AWS services](tutorials-examples.md)\.