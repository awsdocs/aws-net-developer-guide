--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Working with Amazon EC2<a name="ec2-apis-intro"></a>

The AWS SDK for \.NET supports [Amazon EC2](https://docs.aws.amazon.com/ec2/), which is a web service that provides resizable computing capacity\. You use this computing capacity to build and host your software systems\.

## APIs<a name="w4aac19c19b5"></a>

The AWS SDK for \.NET provides APIs for Amazon EC2 clients\. The APIs enable you to work with EC2 features such as security groups and key pairs\. The APIs also enable you to control Amazon EC2 instances\. This section contains a small number of examples that show you the patterns you can follow when working with these APIs\. To view the full set of APIs, see the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) \(and scroll to "Amazon\.EC2"\)\.

The Amazon EC2 APIs are provided by the [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2) NuGet package\.

## Prerequisites<a name="w4aac19c19b7"></a>

Before you begin, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md) and [SDK features](net-dg-sdk-features.md)\.

## About the examples<a name="ec2-apis-intro-about"></a>

The examples in this section show you how to work with Amazon EC2 clients and manage Amazon EC2 instances\.

The [EC2 Spot Instance tutorial](how-to-spot-instances.md) shows you how to request Amazon EC2 Spot Instances\. Spot Instances enable you to access unused EC2 capacity for less than the On\-Demand price\.

**Topics**
+ [APIs](#w4aac19c19b5)
+ [Prerequisites](#w4aac19c19b7)
+ [About the examples](#ec2-apis-intro-about)
+ [Security groups](security-groups.md)
+ [Key pairs](key-pairs.md)
+ [Regions and Availability Zones](using-regions-and-availability-zones.md)
+ [EC2 instances](how-to-ec2.md)
+ [Spot Instance tutorial](how-to-spot-instances.md)