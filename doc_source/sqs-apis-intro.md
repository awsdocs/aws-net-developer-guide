--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Messaging Using Amazon SQS<a name="sqs-apis-intro"></a>

The AWS SDK for \.NET supports Amazon SQS, which is a message queuing service that handles messages or workflows between components in a system\. For more information, see [Amazon SQS](https://aws.amazon.com/sqs/)\.

The following examples demonstrate how to use the AWS SDK for \.NET to create and use Amazon SQS queues\.

The sample code is written in C\#, but you can use the AWS SDK for \.NET with any compatible language\. The AWS SDK for \.NET installs a set of C\# project templates\.

 **Prerequisite Tasks** 

Before you begin, be sure that you have created an AWS account and set up your AWS credentials\. For more information, see [Setting up the AWS SDK for \.NET](net-dg-setup.md)\.

For related API reference information, see [Amazon\.SQS](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQS.html), [Amazon\.SQS\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQSModel.html), and [Amazon\.SQS\.Util](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQSUtil.html) in the AWS SDK for \.NET API Reference\.

**Topics**
+ [Creating an Amazon SQS Client](InitSQSClient.md)
+ [Creating an Amazon SQS Queue](CreateQueue.md)
+ [Constructing Amazon SQS Queue URLs](QueueURL.md)
+ [Sending an Amazon SQS Message](SendMessage.md)
+ [Sending an Amazon SQS Message Batch](SendMessageBatch.md)
+ [Receiving a Message from an Amazon SQS Queue](ReceiveMessage.md)
+ [Deleting a Message from an Amazon SQS Queue](DeleteMessage.md)
+ [Enabling Long Polling in Amazon SQS](EnableLongPolling.md)
+ [Using Amazon SQS Queues](UsingSQSQueues.md)
+ [Using Amazon SQS Dead Letter Queues](UsingSQSDeadLetterQueues.md)