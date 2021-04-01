--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Send an Amazon SQS Message<a name="SendMessage"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c25b7c13b3b1"></a>

You can use the Amazon SDK for \.NET to send a message to an Amazon SQS queue\.

**Important**  
Due to the distributed nature of the queue, Amazon SQS cannot guarantee you will receive messages in the exact order they are sent\. If you require that message order be preserved, place sequencing information in each message so you can reorder the messages upon receipt\.

 **To send a message to an Amazon SQS queue** 

1. Create and initialize a [SendMessageRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSSendMessageRequestNET45.html) instance\. Specify the queue name and the message you want to send, as follows:

   ```
   sendMessageRequest.QueueUrl = myQueueURL; sendMessageRequest.MessageBody = "{YOUR_QUEUE_MESSAGE}";
   ```

   For more information about your queue URL, see [Amazon SQS Queue URLs](QueueURL.md#sqs-queue-url)\.

   Each queue message must be composed of only Unicode characters, and can be up to 64 kB in size\. For more information about queue messages, go to [SendMessage](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SendMessage.html) in the Amazon SQS service API reference\.

1. After you create the request, pass it as a parameter to the [SendMessage](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSSendMessageRequestNET45.html) method\. The method returns a [SendMessageResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSSendMessageResponseNET45.html) object, as follows:

   ```
   SendMessageResponse sendMessageResponse =
       amazonSQSClient.SendMessage(sendMessageRequest);
   ```

   The sent message will stay in your queue until the visibility timeout is exceeded, or until it is deleted from the queue\. For more information about visibility timeouts, go to [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\.

For information on deleting messages from your queue, see [Delete a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.

For information on receiving messages from your queue, see [Receive a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.