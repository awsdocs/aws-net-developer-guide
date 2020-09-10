--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Messaging using Amazon SQS<a name="sqs-apis-intro"></a>

The AWS SDK for \.NET supports [Amazon Simple Queue Service \(Amazon SQS\)](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/), which is a message queuing service that handles messages or workflows between components in a system\.

Amazon SQS queues provide a mechanism that enables you to send, store, and receive messages between software components such as microservices, distributed systems, and serverless applications\. This enables you to decouple such components and frees you from the need to design and operate your own messaging system\. For information about how queues and messages work in Amazon SQS, see [Amazon SQS tutorials](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-tutorials.html) and [How Amazon SQS works](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/SQSConcepts.html) in the [Amazon Simple Queue Service Developer Guide](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/)\.

**Important**  
Due to the distributed nature of queues, Amazon SQS can't guarantee that you'll receive messages in the precise order they're sent\. If you need to preserve message order, use an [Amazon SQS FIFO queue](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/FIFO-queues.html)\.

## APIs<a name="w4aac19c25c11"></a>

The AWS SDK for \.NET provides APIs for Amazon SQS clients\. The APIs enable you to work with Amazon SQS features such as queues and messages\. This section contains a small number of examples that show you the patterns you can follow when working with these APIs\. To view the full set of APIs, see the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) \(and scroll to "Amazon\.SQS"\)\.

The Amazon SQS APIs are provided by the [AWSSDK\.SQS](https://www.nuget.org/packages/AWSSDK.SQS) NuGet package\.

## Prerequisites<a name="w4aac19c25c13"></a>

Before you begin, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md) and [SDK features](net-dg-sdk-features.md)\.

## Topics<a name="w4aac19c25c15"></a>

**Topics**
+ [APIs](#w4aac19c25c11)
+ [Prerequisites](#w4aac19c25c13)
+ [Topics](#w4aac19c25c15)
+ [Creating queues](CreateQueue.md)
+ [Updating queues](UpdateSqsQueue.md)
+ [Deleting queues](DeleteSqsQueue.md)
+ [Sending messages](SendMessage.md)
+ [Receiving messages](ReceiveMessage.md)