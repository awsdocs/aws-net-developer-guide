# Sending an Amazon SQS Message<a name="SendMessage"></a>

You can use the AWS SDK for \.NET to send a message to an Amazon SQS queue\.

**Important**  
Due to the distributed nature of the queue, Amazon SQS can’t guarantee you will receive messages in the precise order they are sent\. If you need to preserve the message order, use an Amazon SQS FIFO queue\. For information about FIFO queues, see [Amazon SQS FIFO \(First\-In\-First\-Out\) Queues](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/FIFO-queues.html)\.

**To send a message to an Amazon SQS queue**

1. Create and initialize a [SendMessageRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageRequest.html) instance\. Specify the queue name and the message you want to send, as follows\.

   ```
   sendMessageRequest.QueueUrl = myQueueURL; sendMessageRequest.MessageBody = "{YOUR_QUEUE_MESSAGE}";
   ```

   For more information about your queue URL, see [Constructing Amazon SQS Queue URLs](QueueURL.md#sqs-queue-url)\.

   Each queue message must be composed of Unicode characters only, and can be up to 64 KB in size\. For more information about queue messages, see [SendMessage](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SendMessage.html) in the Amazon Simple Queue Service API Reference\.

1. After you create the request, pass it as a parameter to the [SendMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSendMessageSendMessageRequest.html) method\. The method returns a [SendMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageResponse.html) object, as follows\.

   ```
   var sendMessageResponse = sqsClient.SendMessage(sendMessageRequest);
   ```

   The sent message will stay in your queue until the visibility timeout is exceeded, or until it is deleted from the queue\. For more information about visibility timeouts, go to [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\.

For information about deleting messages from your queue, see [Deleting a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.

For information about receiving messages from your queue, see [Receiving a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.