--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Using Amazon SQS Queues<a name="UsingSQSQueues"></a>

Amazon SQS offers *standard* as the default queue type\. A standard queue enables you to have a nearly\-unlimited number of transactions per second\. Standard queues support at\-least\-once message delivery\. However, occasionally more than one copy of a message might be delivered out of order\. Standard queues provide best\-effort ordering, which ensures that messages are generally delivered in the same order as they’re sent\.

You can use standard message queues in many scenarios, as long as your application can process messages that arrive more than once and out of order\.

This code example demonstrates how to use queues by using these methods of the `AmazonSQSClient` class:
+  [ListQueues](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSListQueuesListQueuesRequest.html): Gets a list of your message queues
+  [GetQueueUrl](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSGetQueueUrlGetQueueUrlRequest.html): Obtains the URL for a particular queue
+  [DeleteQueue](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSDeleteQueueDeleteQueueRequest.html): Deletes a queue

For more information about Amazon SQS messages, see [How Amazon SQS Queues Work](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-how-it-works.html) in the Amazon Simple Queue Service Developer Guide\.

## List Your Queues<a name="list-your-queues"></a>

Create a [ListQueuesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TListQueuesRequest.html) object containing the properties needed to list your queues, which by default is an empty object\. Call the [ListQueues](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSListQueuesListQueuesRequest.html) method with the `ListQueuesRequest` as a parameter to retrieve the list of queues\. The [ListQueuesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TListQueuesResponse.html) returned by the call contains the URLs of all queues\.

```
AmazonSQSClient client = new AmazonSQSClient();

ListQueuesResponse response = client.ListQueues(new ListQueuesRequest());
foreach (var queueUrl in response.QueueUrls)
{
    Console.WriteLine(queueUrl);
}
```

## Get the URL for a Queue<a name="get-the-url-for-a-queue"></a>

Create a [GetQueueUrlRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TGetQueueUrlRequest.html) object containing the properties needed to identify your queue, which must include the name of the queue whose URL you want\. Call the [GetQueueUrl](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSGetQueueUrlGetQueueUrlRequest.html) method using the `GetQueueUrlRequest` object as a parameter\. The call returns a [GetQueueUrlResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TGetQueueUrlResponse.html) object containing the URL of the specified queue\.

```
AmazonSQSClient client = new AmazonSQSClient();

var request = new GetQueueUrlRequest
{
    QueueName = "SQS_QUEUE_NAME"
};

GetQueueUrlResponse response = client.GetQueueUrl(request);
Console.WriteLine("The SQS queue's URL is {1}", response.QueueUrl);
```

## Delete a Queue<a name="delete-a-queue"></a>

Create a [DeleteQueueRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TDeleteQueueRequest.html) object containing the URL of the queue you want to delete\. Call the [DeleteQueue](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSDeleteQueueDeleteQueueRequest.html) method with the `DeleteQueueRequest` object as the parameter\.

```
AmazonSQSClient client = new AmazonSQSClient();

var request = new DeleteQueueRequest
{
    QueueUrl = "SQS_QUEUE_URL"
};

client.DeleteQueue(request);
```