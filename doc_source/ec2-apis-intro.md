--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Deploying Applications Using Amazon EC2<a name="ec2-apis-intro"></a>

The AWS SDK for \.NET supports Amazon EC2, which is a web service that provides resizable computing capacity—literally, servers in Amazon’s data centers—that you use to build and host your software systems\. The Amazon EC2 APIs are provided by the [AWSSDK\.EC2](http://www.nuget.org/packages/AWSSDK.EC2) assembly\.

The [Amazon EC2 Instances Examples](how-to-ec2.md) are intended to help you get started with Amazon EC2\.

The [Amazon EC2 Spot Instance Examples](how-to-spot-instances.md) show you how to use Spot Instances, which enable you to access unused EC2 capacity at up to a 90% discount compared to the On\-Demand Instance price\. Amazon EC2 sets Spot Instance prices and adjusts them gradually based on long\-term trends in supply and demand for Spot Instance capacity\. You can specify the amount you are willing to pay for a Spot Instance as a percentage of the On\-Demand Instance price; customers whose requests meet or exceed it gain access to the available Spot Instances\.

For more information, see [Spot Instances](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances.html) in the Amazon EC2 User Guide for Linux Instances and [Spot Instances](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-spot-instances.html) in the Amazon EC2 User Guide for Windows Instances\.

**Topics**
+ [Amazon EC2 Instances Examples](how-to-ec2.md)
+ [Amazon EC2 Spot Instance Examples](how-to-spot-instances.md)