--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Amazon Simple Queue Service Programming with the AWS SDK for \.NET<a name="sqs-apis-intro"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c25b3b1"></a>

The AWS SDK for \.NET supports Amazon Simple Queue Service \(Amazon SQS\), which is a messaging queue service that handles message or workflows between other components in a system\. For more information, see the [SQS Getting Started Guide](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSGettingStartedGuide/)\.

The following information introduces you to the Amazon SQS programming models in the SDK\.

### Programming Models<a name="sqs-apis-intro-models"></a>

The SDK provides two programming models for working with Amazon SQS\. These programming models are known as the *low\-level* and *resource* models\. The following information describes these models, how to use them, and why you would want to use them\.

#### Low\-Level APIs<a name="sqs-apis-intro-low-level"></a>

The SDK provides low\-level APIs for programming with Amazon SQS\. These APIs typically consist of sets of matching request\-and\-response objects that correspond to HTTP\-based API calls focusing on their corresponding service\-level constructs\.

The following example shows how to use the APIs to list accessible queues in Amazon SQS:

```
// using Amazon.SQS;
// using Amazon.SQS.Model;

var client = new AmazonSQSClient();

// List all queues that start with "aws".
var request = new ListQueuesRequest
{
  QueueNamePrefix = "aws"
};

var response = client.ListQueues(request);
var urls = response.QueueUrls;

if (urls.Any())
{
  Console.WriteLine("Queue URLs:");

  foreach (var url in urls)
  {
    Console.WriteLine("  " + url);
  }
}
else
{
  Console.WriteLine("No queues.");
}
```

For additional examples, see the following:
+  [Create an Amazon SQS Client](InitSQSClient.md#init-sqs-client) 
+  [Create an Amazon SQS Queue](CreateQueue.md#create-sqs-queue) 
+  [Send an Amazon SQS Message](SendMessage.md#send-sqs-message) 
+  [Receive a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message) 
+  [Delete a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message) 

For related API reference information, see `Amazon.SQS`, `Amazon.SQS.Model`, and `Amazon.SQS.Util` in the [AWS SDK for \.NET Reference](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/)\.

#### Resource APIs<a name="sqs-apis-intro-resource-level"></a>

The the SDK provides the AWS Resource APIs for \.NET for programming with Amazon SQS\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with Amazon SQS resources as compared to their low\-level API counterparts\. \(For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.\)

The following example shows how to use the AWS Resource APIs for \.NET to list accessible queues in Amazon SQS

```
// using Amazon.SQS.Resources;

var sqs = new SQS();
 
// List all queues that start with "aws".
var queues = sqs.GetQueues("aws");

if (queues.Any())
{
  Console.WriteLine("Queue URLs:");

  foreach (var queue in queues)
  {
    Console.WriteLine("  " + queue.Url);
  }
}
else
{
  Console.WriteLine("No queues.");
}
```

For related API reference information, see [Amazon\.SQS\.Resources](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NSQSResourcesNET45.html)\.

**Topics**
+ [Creating and Using an Amazon SQS Queue with the AWS SDK for \.NET](how-to-sqs.md)