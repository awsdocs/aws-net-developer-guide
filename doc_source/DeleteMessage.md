--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Deleting a Message from an Amazon SQS Queue<a name="DeleteMessage"></a>

You can use the AWS SDK for \.NET to delete messages from an Amazon SQS queue\.

**To delete a message from an Amazon SQS queue**

1. Create and initialize a [DeleteMessageRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TDeleteMessageRequest.html) object\. Specify the Amazon SQS queue to delete a message from and the receipt handle of the message to delete, as follows\.

   ```
   var deleteMessageRequest = new DeleteMessageRequest();
   
   deleteMessageRequest.QueueUrl = queueUrl;
   deleteMessageRequest.ReceiptHandle = receiptHandle;
   ```

1. Pass the request object as a parameter to the [DeleteMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSDeleteMessageDeleteMessageRequest.html) method\. The method returns a [DeleteMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TDeleteMessageResponse.html) object, as follows\.

   ```
   var response = sqsClient.DeleteMessage(deleteMessageRequest);
   ```

   Calling `DeleteMessage` unconditionally removes the message from the queue, regardless of the visibility timeout setting\. For more information about visibility timeouts, see [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\.

For information about sending a message to a queue, see [Sending an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For information about receiving messages from a queue, see [Receiving a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.