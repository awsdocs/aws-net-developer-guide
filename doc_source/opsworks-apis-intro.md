--------

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Programming AWS OpsWorks to Work with stacks and applications<a name="opsworks-apis-intro"></a>

The AWS SDK for \.NET supports AWS OpsWorks, which provides a simple and flexible way to create and manage stacks and applications\. With AWS OpsWorks, you can provision AWS resources, manage their configuration, deploy applications to those resources, and monitor their health\. For more information, see the [OpsWorks product page](https://aws.amazon.com/opsworks/) and the [AWS OpsWorks User Guide](https://docs.aws.amazon.com/opsworks/latest/userguide/)\.

## APIs<a name="w8aac19c27b5"></a>

The AWS SDK for \.NET provides APIs for AWS OpsWorks\. The APIs enable you to work with AWS OpsWorks features such as [stacks](https://docs.aws.amazon.com/opsworks/latest/userguide/workingstacks.html) with their [layers](https://docs.aws.amazon.com/opsworks/latest/userguide/workinglayers.html), [instances](https://docs.aws.amazon.com/opsworks/latest/userguide/workinginstances.html), and [apps](https://docs.aws.amazon.com/opsworks/latest/userguide/workingapps.html)\. To view the full set of APIs, see the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) \(and scroll to "Amazon\.OpsWorks"\)\.

The AWS OpsWorks APIs are provided by the [AWSSDK\.OpsWorks](https://www.nuget.org/packages/AWSSDK.OpsWorks) NuGet package\.

## Prerequisites<a name="w8aac19c27b7"></a>

Before you begin, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md) and [SDK features](net-dg-sdk-features.md)\.