--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Enabling Long Polling in Amazon SQS<a name="EnableLongPolling"></a>

Long polling reduces the number of empty responses by allowing Amazon SQS to wait a specified time for a message to become available in the queue before sending a response\. Also, long polling eliminates false empty responses by querying all the servers instead of a sampling of servers\. To enable long polling, you must specify a non\-zero wait time for received messages\. You can do this by setting the `ReceiveMessageWaitTimeSeconds` parameter of a queue or by setting the `WaitTimeSeconds` parameter on a message when it’s received\. This \.NET example shows you how to enable long polling in Amazon SQS for a newly created or existing queue, or upon receipt of a message\.

These examples use the following methods of the [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) class to enable long polling:
+  [CreateQueue](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSCreateQueueCreateQueueRequest.html) 
+  [SetQueueAttributes](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSetQueueAttributesSetQueueAttributesRequest.html) 
+  [ReceiveMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSReceiveMessageReceiveMessageRequest.html) 

For more information about long polling, see [Amazon SQS Long Polling](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-long-polling.html) in the Amazon Simple Queue Service Developer Guide\.

## Enable Long Polling When Creating a Queue<a name="enable-long-polling-when-creating-a-queue"></a>

Create an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) service object\. Create a [CreateQueueRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TCreateQueueRequest.html) object containing the properties needed to create a queue, including a non\-zero value for the `ReceiveMessageWaitTimeSeconds` property\.

Call the [CreateQueue](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSCreateQueueCreateQueueRequest.html) method\. Long polling is then enabled for the queue\.

```
AmazonSQSClient client = new AmazonSQSClient();
var request = new CreateQueueRequest
{
    QueueName = "SQS_QUEUE_NAME",
    Attributes = new Dictionary<string, string>
    {
        { "ReceiveMessageWaitTimeSeconds", "20"}
    }
};
var response = client.CreateQueue(request);
Console.WriteLine("Created a queue with URL : {0}", response.QueueUrl);
```

## Enable Long Polling on an Existing Queue<a name="enable-long-polling-on-an-existing-queue"></a>

Create an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) service object\. Create a [SetQueueAttributesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSetQueueAttributesRequest.html) object containing the properties needed to set the attributes of the queue, including a non\-zero value for the `ReceiveMessageWaitTimeSeconds` property and the URL of the queue\. Call the [SetQueueAttributes](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSetQueueAttributesSetQueueAttributesRequest.html) method\. Long polling is then enabled for the queue\.

```
AmazonSQSClient client = new AmazonSQSClient();

var request = new SetQueueAttributesRequest
{
    Attributes = new Dictionary<string, string>
    {
        { "ReceiveMessageWaitTimeSeconds", "20"}
    },
    QueueUrl = "SQS_QUEUE_URL"
};

var response = client.SetQueueAttributes(request);
```

## Receive a Message<a name="receive-a-message"></a>

Create an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) service object\. Create a [ReceiveMessageRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageRequest.html) object containing the properties needed to receive a message, including a non\-zero value for the `WaitTimeSeconds` parameter and the URL of the queue\. Call the [ReceiveMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSReceiveMessageReceiveMessageRequest.html) method\.

```
public void OnMessageReceipt()
{
    AmazonSQSClient client = new AmazonSQSClient();

    var request = new ReceiveMessageRequest
    {
        AttributeNames = { "SentTimestamp" },
        MaxNumberOfMessages = 1,
        MessageAttributeNames = { "All" },
        QueueUrl = "SQS_QUEUE_URL",
        WaitTimeSeconds = 20
    };

    var response = client.ReceiveMessage(request);
}
```