--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Delete a Message from an Amazon SQS Queue<a name="DeleteMessage"></a>

You can use the Amazon SDK for \.NET to receive messages from an Amazon SQS queue\.

 **To delete a message from an Amazon SQS queue** 

1. Create and initialize a [DeleteMessageRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSDeleteMessageRequestNET45.html) instance\. Specify the Amazon SQS queue to delete a message from and the receipt handle of the message to delete, as follows:

   ```
   DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest();
   
   deleteMessageRequest.QueueUrl = queueUrl;
   deleteMessageRequest.ReceiptHandle = recieptHandle;
   ```

1. Pass the request object as a parameter to the [DeleteMessage](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MSQSSQSDeleteMessageDeleteMessageRequestNET45.html) method\. The method returns a [DeleteMessageResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSDeleteMessageResponseNET45.html) object, as follows:

   ```
   DeleteMessageResponse response =
     amazonSQSClient.DeleteMessage(deleteMessageRequest);
   ```

   Calling `DeleteMessage` unconditionally removes the message from the queue, regardless of the visibility timeout setting\. For more information about visibility timeouts, go to [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\.

For information about sending a message to a queue, see [Sending an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For information about receiving messages from a queue, see [Receiving a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.