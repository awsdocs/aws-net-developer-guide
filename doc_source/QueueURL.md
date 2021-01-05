--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Constructing Amazon SQS Queue URLs<a name="QueueURL"></a>

You need a queue URL to send, receive, and delete queue messages\. You can get your queue URL using the [GetQueueUrl](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSGetQueueUrlGetQueueUrlRequest.html) method\.

**Note**  
For \.NET Core, PCL and Unity this operation is only available in asynchronous form using [GetQueueUrlAsync](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSGetQueueUrlAsyncGetQueueUrlRequestCancellationToken.html)\.

```
var client = new AmazonSQSClient();
var request = new GetQueueUrlRequest
{
  QueueName = "MyTestQueue",
  QueueOwnerAWSAccountId = "80398EXAMPLE"
};
var response = client.GetQueueUrl(request);
Console.WriteLine("Queue URL: " + response.QueueUrl);
```

To find your AWS account number, go to [Security Credentials](https://console.aws.amazon.com/iam/home)\. Your account number is located under **Account Number** at the top\-right of the page\.

For information about sending a message to a queue, see [Sending an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For information about receiving messages from a queue, see [Receiving a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.

For information about deleting messages from a queue, see [Deleting a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.