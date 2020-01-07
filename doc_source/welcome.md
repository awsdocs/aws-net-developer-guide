# AWS SDK for \.NET Developer Guide<a name="welcome"></a>

The AWS SDK for \.NET makes it easier for Windows developers to build \.NET applications that tap into the cost\-effective, scalable, and reliable AWS services such as Amazon Simple Storage Service \(Amazon S3\) and Amazon Elastic Compute Cloud \(Amazon EC2\)\. The AWS SDK for \.NET supports \.NET Framework 3\.5, \.NET Framework 4\.5, \.NET Standard 1\.3, \.NET Standard 2\.0, Portable Class Library, and Xamarin\. You can develop applications with the SDK using Visual Studio 2010 or later\.

Unless explicitly stated otherwise, all information within this guide applies to all of the supported targets\.

## What’s in the SDK<a name="what-s-in-the-sdk"></a>

The AWS SDK for \.NET includes the following:
+ The current version of the AWS SDK for \.NET
+ All previous major versions of the AWS SDK for \.NET
+ Sample code that demonstrates how to use the AWS SDK for \.NET with several AWS services

To simplify installation, AWS provides the AWS Tools for Windows, which is a Windows installation package that includes:
+ The [AWS SDK for \.NET](https://aws.amazon.com/sdk-for-net/) 
+ The AWS Tools for Windows PowerShell \(see the [AWS Tools for PowerShell User Guide](https://docs.aws.amazon.com/powershell/latest/userguide/)\)
+ The AWS Toolkit for Visual Studio \(see the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/)\)

To install the AWS Tools for Windows, navigate to the [AWS SDK for \.NET](https://aws.amazon.com/sdk-for-net/) select **Download MSI Installer**, save the MSI file, and run it\.

As an alternative to installing the AWS Tools for Windows, you can use NuGet to download individual AWSSDK service assemblies for a specific application project\. For more information, see [Installing AWSSDK Assemblies with NuGet](net-dg-install-assemblies.md#net-dg-nuget)\.

**Note**  
We recommend using Visual Studio Professional 2010 or later to implement your applications\.

## About the AWS Tools for Windows Tools<a name="about-tools"></a>

### AWS SDK for \.NET<a name="sdk-net"></a>

This document is the developer guide for the AWS SDK for \.NET\. The AWS SDK for \.NET makes it easier for Windows developers to build \.NET applications that tap in to the cost\-effective, scalable, and reliable AWS infrastructure services such as Amazon Simple Storage Service, Amazon Elastic Compute Cloud, AWS Lambda, and more\.

### AWS Tools for Windows PowerShell and PowerShell Core<a name="aws-tools-for-windows-powershell-and-powershell-core"></a>

The AWS Tools for Windows PowerShell and AWS Tools for PowerShell Core are PowerShell modules that are built on the functionality exposed by the AWS SDK for \.NET\. The AWS PowerShell Tools enable you to script operations on your AWS resources from the PowerShell command line\. Although the cmdlets are implemented using the service clients and methods from the SDK, the cmdlets provide an idiomatic PowerShell experience for specifying parameters and handling results\.

See [AWS Tools for Windows PowerShell](https://aws.amazon.com/powershell) to get started\.

### AWS Toolkit for Visual Studio<a name="tvslong"></a>

The Toolkit for Visual Studio is a plugin for the Visual Studio IDE that makes it easier for you to develop, debug, and deploy \.NET applications that use Amazon Web Services\. The Toolkit for Visual Studio provides Visual Studio templates for AWS services and deployment wizards for web applications and serverless applications\. You can use the AWS Explorer to manage Amazon Elastic Compute Cloud instances, work with Amazon DynamoDB tables, publish messages to Amazon Simple Notification Service queues, and more, all within Visual Studio\.

You can quickly deploy an existing web application using Toolkit for Visual Studio\. See [Deploying Web ApplicationsDeploying Web Applications from Visual Studio](https://docs.aws.amazon.com/sdk-for-net/v3/ndg/web-deploy-vs.html)\.

See [Setting up the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/setup.html) to get started\.

### AWS Tools for Microsoft Visual Studio Team Services<a name="ttslong"></a>

In addition to the tools that the AWS Tools for Windows installs, you can also install the AWS Tools for Microsoft Visual Studio Team Services\.

AWS Tools for Microsoft Visual Studio Team Services \(VSTS\) adds tasks to easily enable build and release pipelines in VSTS and Team Foundation Server \(TFS\) to work with AWS services\. You can work with Amazon S3, AWS Elastic Beanstalk, AWS CodeDeploy, AWS Lambda, AWS CloudFormation, Amazon Simple Queue Service \(Amazon SQS\), and Amazon Simple Notification Service \(Amazon SNS\)\. You can also run commands using the Windows PowerShell module and the AWS CLI\.

To get started with AWS Tools for Microsoft Visual Studio Team Services, see [AWS Tools for Microsoft Visual Studio Team Services](https://aws.amazon.com/vsts/)\.

## How to Use This Guide<a name="guidemap"></a>

The *AWS SDK for \.NET Developer Guide* describes how to implement applications for AWS using the AWS SDK for \.NET, and includes the following:

** [Getting Started with the AWS SDK for \.NET](net-dg-setup.md) **  
How to install and configure the AWS SDK for \.NET\. If you have not used the AWS SDK for \.NET before or are having trouble with its configuration, you should start here\.

** [Programming with the AWS SDK for \.NET](net-dg-programming-techniques.md) **  
The basics of how to implement applications with the AWS SDK for \.NET that applies to all AWS services\. This section also includes information about how to migrate code to the latest version of the AWS SDK for \.NET, and describes the differences between the last version and this one\.

** [Code Examples](tutorials-examples.md) **  
A set of tutorials, walkthroughs, and examples showing how to use the AWS SDK for \.NET to create applications for particular AWS services\. You can browse the AWS SDK for \.NET examples in the [AWS Code Sample Catalog](https://docs.aws.amazon.com/code-samples/latest/catalog/code-catalog-dotnet.html)\.

** [Additional Resources](net-dg-additional-resources.md) **  
More resources outside of this guide that provide valuable information about AWS and the AWS SDK for \.NET\. If you are unfamiliar with AWS services, see the [Overview of Amazon Web Services](https://docs.aws.amazon.com/whitepapers/latest/aws-overview/introduction.html)\.

A related document, [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/), provides a detailed description of each namespace and class\.

## Supported Services and Revision History<a name="supported-services"></a>

The AWS SDK for \.NET supports most AWS infrastructure products, and more services are added frequently\. For a list of the AWS services supported by the SDK, see the [SDK README file](https://github.com/aws/aws-sdk-net/blob/master/README.md)\.

To see what changed in a given release, see the [SDK change log](https://github.com/aws/aws-sdk-net/blob/master/SDK.CHANGELOG.md)\.