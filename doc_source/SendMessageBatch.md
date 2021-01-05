--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Sending an Amazon SQS Message Batch<a name="SendMessageBatch"></a>

You can use the AWS SDK for \.NET to send batch messages to an Amazon SQS queue\. The [SendMessageBatch](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSendMessageBatchSendMessageBatchRequest.html) method delivers up to 10 messages to the specified queue\. This is a batch version of [SendMessage](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSendMessageSendMessageRequest.html)\.

For a FIFO queue, multiple messages within a single batch are enqueued in the order they are sent\.

For more information about sending batch messages, see [SendMessageBatch](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SendMessageBatch.html) in the Amazon Simple Queue Service API Reference\.

**To send batch messages to an Amazon SQS queue**

1. Create an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) instance and initialize a [SendMessageBatchRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageBatchRequest.html) object\. Specify the queue name and the message you want to send, as follows\.

   ```
   AmazonSQSClient client = new AmazonSQSClient();
   var sendMessageBatchRequest = new SendMessageBatchRequest
   {
       Entries = new List<SendMessageBatchRequestEntry>
       {
           new SendMessageBatchRequestEntry("message1", "FirstMessageContent"),
           new SendMessageBatchRequestEntry("message2", "SecondMessageContent"),
           new SendMessageBatchRequestEntry("message3", "ThirdMessageContent")
       },
       QueueUrl = "SQS_QUEUE_URL"
   };
   ```

   For more information about your queue URL, see [Constructing Amazon SQS Queue URLs](QueueURL.md#sqs-queue-url)\.

   Each queue message must be composed of Unicode characters only, and can be up to 64 KB in size\. For more information about queue messages, see [SendMessage](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SendMessage.html) in the Amazon Simple Queue Service API Reference\.

1. After you create the request, pass it as a parameter to the [SendMessageBatch](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSSendMessageBatchSendMessageBatchRequest.html) method\. The method returns a [SendMessageBatchResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageBatchResponse.html) object, which contains the unique ID of each message and the message content for each successfully sent message\. It also returns the message ID, message content, and a sender’s fault flag if the message failed to send\.

   ```
   SendMessageBatchResponse response = client.SendMessageBatch(sendMessageBatchRequest);
   Console.WriteLine("Messages successfully sent:");
   foreach (var success in response.Successful)
   {
       Console.WriteLine("    Message id : {0}", success.MessageId);
       Console.WriteLine("    Message content MD5 : {0}", success.MD5OfMessageBody);
   }
   
   Console.WriteLine("Messages failed to send:");
   foreach (var failed in response.Failed)
   {
       Console.WriteLine("    Message id : {0}", failed.Id);
       Console.WriteLine("    Message content : {0}", failed.Message);
       Console.WriteLine("    Sender's fault? : {0}", failed.SenderFault);
   }
   ```