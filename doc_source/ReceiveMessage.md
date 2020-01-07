# Receiving a Message from an Amazon SQS Queue<a name="ReceiveMessage"></a>

You can use the AWS SDK for \.NET to receive messages from an Amazon SQS queue\.

**To receive a message from an Amazon SQS queue**

1. Create and initialize a [ReceiveMessageRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageRequest.html) instance\. Specify the queue URL to receive a message from, as follows\.

   ```
   var receiveMessageRequest = new ReceiveMessageRequest();
   
   receiveMessageRequest.QueueUrl = myQueueURL;
   ```

   For more information about your queue URL, see [Your Amazon SQS Queue URL](QueueURL.md#sqs-queue-url)\.

1. Pass the request object as a parameter to the [ReceiveMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSReceiveMessageReceiveMessageRequest.html) method, as follows\.

   ```
   var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);
   ```

   The method returns a [ReceiveMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageResponse.html) instance, containing the list of messages the queue contains\.

1. The `ReceiveMessageResponse.ReceiveMessageResult` property contains a [ReceiveMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageResponse.html) object, which contains a list of the messages that were received\. Iterate through this list and call the `ProcessMessage` method to process each message\.

   ```
   foreach (var message in result.Messages)
   {
     ProcessMessage(message);  // Go to a method to process messages.
   }
   ```

   The `ProcessMessage` method can use the `ReceiptHandle` property to obtain a receipt handle for the message\. You can use this receipt handle to change the message visibility timeout or to delete the message from the queue\. For more information about how to change the visibility timeout for a message, see [ChangeMessageVisibility](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSChangeMessageVisibilityChangeMessageVisibilityRequest.html)\.

For information about sending a message to your queue, see [Sending an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For more information about deleting a message from the queue, see [Deleting a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.