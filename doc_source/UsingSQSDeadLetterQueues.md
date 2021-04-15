--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Using Amazon SQS Dead Letter Queues<a name="UsingSQSDeadLetterQueues"></a>

This example shows you how to use a queue to receive and hold messages from other queues that the queues can’t process\.

A dead letter queue is one that other \(source\) queues can target for messages that can’t be processed successfully\. You can set aside and isolate these messages in the dead letter queue to determine why their processing did not succeed\. You must individually configure each source queue that sends messages to a dead letter queue\. Multiple queues can target a single dead letter queue\.

In this example, an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) object uses the [SetQueueAttributesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSetQueueAttributesSetQueueAttributesRequest.html) method to configure a source queue to use a dead letter queue\.

For more information about Amazon SQS dead letter queues, see [Using Amazon SQS Dead Letter Queues](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-dead-letter-queues.html) in the Amazon Simple Queue Service Developer Guide\.

## Configure a Source Queue<a name="configure-a-source-queue"></a>

This code example assumes you have created a queue to act as a dead letter queue\. See [Creating an Amazon SQS Queue](CreateQueue.md#create-sqs-queue) for information about creating a queue\. After creating the dead letter queue, you must configure the other queues to route unprocessed messages to the dead letter queue\. To do this, specify a redrive policy that identifies the queue to use as a dead letter queue and the maximum number of receives by individual messages before they are routed to the dead letter queue\.

Create an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) object to set the queue attributes\. Create a [SetQueueAttributesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSetQueueAttributesSetQueueAttributesRequest.html) object containing the properties needed to update queue attributes, including the `RedrivePolicy` property that specifies both the Amazon Resource Name \(ARN\) of the dead letter queue, and the value of `maxReceiveCount`\. Also specify the URL source queue you want to configure\. Call the [SetQueueAttributes](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSetQueueAttributesSetQueueAttributesRequest.html) method\.

```
AmazonSQSClient client = new AmazonSQSClient();

var setQueueAttributeRequest = new SetQueueAttributesRequest
{
    Attributes = new Dictionary<string, string>
    {
        {"RedrivePolicy",   @"{ ""deadLetterTargetArn"" : ""DEAD_LETTER_QUEUE_ARN"", ""maxReceiveCount"" : ""10""}" }
    },
    QueueUrl = "SOURCE_QUEUE_URL"
};

client.SetQueueAttributes(setQueueAttributeRequest)
```