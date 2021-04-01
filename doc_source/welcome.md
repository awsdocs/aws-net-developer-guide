--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# AWS SDK for \.NET Developer Guide<a name="welcome"></a>

## Version 2 content \(see announcement above\)<a name="w3aab7b3b1"></a>

The AWS SDK for \.NET makes it easier for Windows developers to build \.NET applications that tap into the cost\-effective, scalable, and reliable AWS services such as Amazon Simple Storage Service \(Amazon S3\) and Amazon Elastic Compute Cloud \(Amazon EC2\)\. The the SDK supports development on any platform that supports the \.NET Framework 3\.5 or later, and you can develop applications with the SDK using Visual Studio 2010 or later\.

The AWS SDK for \.NET includes the following:
+ The current version of the AWS SDK for \.NET\.
+ All previous major versions of the AWS SDK for \.NET\.
+ Sample code that demonstrates how to use the AWS SDK for \.NET with several AWS services\.

To simplify installation, AWS provides the AWS Tools for Windows, which is a Windows installation package that includes:
+ The AWS SDK for \.NET\.
+ The AWS Tools for Windows PowerShell\. For more information about the AWS Tools for Windows PowerShell, see the [AWS Tools for PowerShell User Guide](https://docs.aws.amazon.com/powershell/latest/userguide/)\.
+ The AWS Toolkit for Visual Studio\. For more information about the AWS Toolkit for Visual Studio, see the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/)\.

As an alternative to installing the AWS Tools for Windows, you can use NuGet to download the AWSSDK assembly for a specific application project\. For more information, see [Install AWS Assemblies with NuGet](net-dg-nuget.md)\.

**Note**  
We recommend using Visual Studio Professional 2010 or higher to implement your applications\. It is possible to use Visual Studio Express to implement applications with the the SDK, including installing the Toolkit for Visual Studio\. However, the installation includes only the AWS project templates and the Standalone Deployment Tool\. In particular, Toolkit for Visual Studio on Visual Studio Express does not support AWS Explorer\.

### How to Use This Guide<a name="guidemap"></a>

The *AWS SDK for \.NET Developer Guide* describes how to implement applications for AWS using the the SDK, and includes the following:

** [Getting Started with the AWS SDK for \.NET](net-dg-setup.md) **  
How to install and configure the the SDK\. If you have not used the the SDK before or are having trouble with its configuration, you should start here\.

** [Programming with the AWS SDK for \.NET](net-dg-programming-techniques.md) **  
The basics of how to implement applications with the the SDK that applies to all AWS services\. This chapter also includes information about how to migrate code to the latest version of the the SDK, and describes the differences between the last version and this one\.

** [Programming AWS Services with the AWS SDK for \.NET](tutorials-examples.md) **  
A set of tutorials, walkthroughs, and examples of how to use the the SDK to create applications for particular AWS services\.

** [Additional Resources](net-dg-additional-resources.md) **  
Additional resources outside of this guide that provide more information about AWS and the the SDK\.

**Note**  
A related document, [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/), provides a detailed description of each namespace and class\.

### Supported Services and Revision History<a name="supported-services"></a>

The AWS SDK for \.NET supports most AWS infrastructure products, and we regularly release updates to the the SDK to support new services and new service features\. To see what changed with a given release, see the [the SDK README file](https://github.com/aws/aws-sdk-net/blob/master/README.md)\.

To see what changed in a given release, see the [the SDK change log](https://github.com/aws/aws-sdk-net/blob/master/SDK.CHANGELOG.md)\.

### About Amazon Web Services<a name="about-aws"></a>

Amazon Web Services \(AWS\) is a collection of digital infrastructure services that developers can leverage when developing their applications\. The services include computing, storage, database, and application synchronization \(messaging and queuing\)\.

AWS uses a pay\-as\-you\-go service model\. You are charged only for the services that you—or your applications—use\. Also, to make AWS useful as a platform for prototyping and experimentation, AWS offers a free usage tier, in which services are free below a certain level of usage\. For more information about AWS costs and the free usage tier go to [Test\-Driving AWS in the Free Usage Tier](https://docs.aws.amazon.com/awsaccountbilling/latest/aboutv2/billing-free-tier.html)\.

To obtain an AWS account, go to the [AWS home page](https://portal.aws.amazon.com/gp/aws/developer/registration/index.html) and click **Sign Up Now**\.