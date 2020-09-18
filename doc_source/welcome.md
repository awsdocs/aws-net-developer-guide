--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# What Is the AWS SDK for \.NET<a name="welcome"></a>

The AWS SDK for \.NET makes it easier to build \.NET applications that tap into cost\-effective, scalable, and reliable AWS services such as Amazon Simple Storage Service \(Amazon S3\) and Amazon Elastic Compute Cloud \(Amazon EC2\)\. The AWS SDK for \.NET supports \.NET Framework 3\.5, \.NET Framework 4\.5, \.NET Standard 2\.0, Portable Class Library, Xamarin, and Unity\.

Unless stated otherwise, the information in this guide applies to all of the supported targets\.

\(**Got it\!** I'm ready for a [tutorial](quick-start.md) or to start [setting up](net-dg-setup.md)\.\)

## Maintenance and support<a name="maintenance-and-support"></a>

For information about maintenance and support for SDK major versions and their underlying dependencies see the following in the [AWS SDKs and Tools Shared Configuration and Credentials Reference Guide](https://docs.aws.amazon.com/credref/latest/refdocs/overview.html):
+ [AWS SDKs and Tools Maintenance Policy](https://docs.aws.amazon.com/credref/latest/refdocs/maint-policy.html)
+ [AWS SDKs and Tools Version Support Matrix](https://docs.aws.amazon.com/credref/latest/refdocs/version-support-matrix.html)

## AWS Tools Related to the SDK<a name="about-tools"></a>

### Tools for Windows PowerShell and Tools for PowerShell Core<a name="aws-tools-for-windows-powershell-and-powershell-core"></a>

The AWS Tools for Windows PowerShell and AWS Tools for PowerShell Core are PowerShell modules that are built on the functionality exposed by the AWS SDK for \.NET\. The AWS PowerShell Tools enable you to script operations on your AWS resources from the PowerShell prompt\. Although the cmdlets are implemented using the service clients and methods from the SDK, the cmdlets provide an idiomatic PowerShell experience for specifying parameters and handling results\.

To get started, see [AWS Tools for Windows PowerShell](https://aws.amazon.com/powershell)\.

### Toolkit for VS Code<a name="toolkit-vscode"></a>

The [AWS Toolkit for Visual Studio Code](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/) is a plugin for the Visual Studio Code \(VS Code\) editor\. The toolkit makes it easier for you to develop, debug, and deploy applications that use AWS\. 

With the toolkit, you can do such things as the following:
+ Create serverless applications that contain AWS Lambda functions, and then deploy the applications to an AWS CloudFormation stack\.
+ Work with Amazon EventBridge schemas\.
+ Use IntelliSense when working with Amazon ECS task\-definition files\.
+ Visualize an AWS Cloud Development Kit \(AWS CDK\) application\.

### Toolkit for Visual Studio<a name="tvslong"></a>

The AWS Toolkit for Visual Studio is a plugin for the Visual Studio IDE that makes it easier for you to develop, debug, and deploy \.NET applications that use Amazon Web Services\. The Toolkit for Visual Studio provides Visual Studio templates for services such as Lambda and deployment wizards for web applications and serverless applications\. You can use the AWS Explorer to manage Amazon EC2 instances, work with Amazon DynamoDB tables, publish messages to Amazon Simple Notification Service \(Amazon SNS\) queues, and more, all within Visual Studio\.

To get started, see [Setting up the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/setup.html)\.

### Toolkit for Azure DevOps<a name="ttslong"></a>

The AWS Toolkit for Microsoft Azure DevOps adds tasks to easily enable build and release pipelines in Azure DevOps and Azure DevOps Server to work with AWS services\. You can work with Amazon S3, AWS Elastic Beanstalk, AWS CodeDeploy, Lambda, AWS CloudFormation, Amazon Simple Queue Service \(Amazon SQS\), and Amazon SNS\. You can also run commands using the Windows PowerShell module and the AWS Command Line Interface \(AWS CLI\)\.

To get started with the AWS Toolkit for Azure DevOps, see the [AWS Toolkit for Microsoft Azure DevOps User Guide](https://docs.aws.amazon.com/vsts/latest/userguide/)\.

## Version 3\.5 of the AWS SDK for \.NET<a name="version-3-5-summary"></a>

Version 3\.5 of the AWS SDK for \.NET further standardizes the \.NET experience by transitioning support for all non\-Framework variations of the SDK to [\.NET Standard 2\.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)\.

Depending on your environment and code base, to take advantage of version 3\.5 features, you might need to perform certain migration work\. For details about version 3\.5 and possible migration work, see [Migrating to Version 3\.5 of the AWS SDK for \.NET](net-dg-v35.md)\.

## How to Use This Guide<a name="guidemap"></a>

The *AWS SDK for \.NET Developer Guide* describes how to implement applications for AWS using the AWS SDK for \.NET, and includes the following tasks and resources\.

**[Get started quickly with the AWS SDK for \.NET](quick-start.md)**  
For someone who is new to \.NET development on AWS, basic tutorials that show you a few common scenarios, as well as a minimal setup to support them\.

** [Setting up the AWS SDK for \.NET](net-dg-setup.md) **  
How to install and configure the AWS SDK for \.NET\. If you have not used the AWS SDK for \.NET before or are having trouble with its configuration, start here\.

** [Programming with the AWS SDK for \.NET](net-dg-programming-techniques.md) **  
The basics of how to implement applications with the AWS SDK for \.NET that apply to all AWS services\. This section also includes information about how to migrate code to the latest version of the AWS SDK for \.NET, and describes the differences between the earlier version and this one\.

** [Code Examples](tutorials-examples.md) **  
A set of tutorials, walkthroughs, and examples showing how to use the AWS SDK for \.NET to create applications for particular AWS services\. You can browse the AWS SDK for \.NET examples in the [AWS Code Sample Catalog](https://docs.aws.amazon.com/code-samples/latest/catalog/code-catalog-dotnet.html)\.  
The [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) provides a detailed description of each namespace and class\.

** [Additional Resources](net-dg-additional-resources.md) **  
More resources outside of this guide that provide valuable information about AWS and the AWS SDK for \.NET\. If you are unfamiliar with AWS services, see the [Overview of Amazon Web Services](https://docs.aws.amazon.com/whitepapers/latest/aws-overview/introduction.html)\.

## Supported Services and Revision History<a name="supported-services"></a>

The AWS SDK for \.NET supports most AWS infrastructure products, and more services are added frequently\. For a list of the AWS services supported by the SDK, see the [SDK README file](https://github.com/aws/aws-sdk-net/blob/master/README.md)\.

To see what changed in a given release, see the [SDK change log](https://github.com/aws/aws-sdk-net/blob/master/SDK.CHANGELOG.md)\.