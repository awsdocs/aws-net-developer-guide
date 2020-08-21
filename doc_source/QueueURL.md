--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

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