--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Accessing AWS CloudFormation with the AWS SDK for \.NET<a name="cloudformation-apis-intro"></a>

The AWS SDK for \.NET supports [AWS CloudFormation](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/), which creates and provisions AWS infrastructure deployments predictably and repeatedly\.

## APIs<a name="w8aac19c15b5"></a>

The AWS SDK for \.NET provides APIs for AWS CloudFormation clients\. The APIs enable you to work with AWS CloudFormation features such as templates and stacks\. This section contains a small number of examples that show you the patterns you can follow when working with these APIs\. To view the full set of APIs, see the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) \(and scroll to "Amazon\.CloudFormation"\)\.

The AWS CloudFormation APIs are provided by the [AWSSDK\.CloudFormation](https://www.nuget.org/packages/AWSSDK.CloudFormation/) package\.

## Prerequisites<a name="w8aac19c15b7"></a>

Before you begin, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md) and [SDK features](net-dg-sdk-features.md)\.

## Topics<a name="w8aac19c15b9"></a>

**Topics**
+ [APIs](#w8aac19c15b5)
+ [Prerequisites](#w8aac19c15b7)
+ [Topics](#w8aac19c15b9)
+ [Listing AWS resources](cfn-list-resources.md)