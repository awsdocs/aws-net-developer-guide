--------

End of support announcement: [http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/](http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

This documentation is for version 2\.0 of the AWS SDK for \.NET\.** For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/) of the AWS SDK for \.NET developer guide instead\.**

--------

# Receive a Message from an Amazon SQS Queue<a name="ReceiveMessage"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c25b7c15b3b1"></a>

You can use the Amazon SDK for \.NET to receive messages from an Amazon SQS queue\.

 **To receive a message from an Amazon SQS queue** 

1. Create and initialize a [ReceiveMessageRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MSQSSQSReceiveMessageReceiveMessageRequestNET45.html) instance\. Specify the queue URL to receive a message from, as follows:

   ```
   ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
   
   receiveMessageRequest.QueueUrl = myQueueURL;
   ```

   For more information about your queue URL, see [Your Amazon SQS Queue URL](QueueURL.md#sqs-queue-url)\.

1. Pass the request object as a parameter to the [ReceiveMessage](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MSQSSQSReceiveMessageReceiveMessageRequestNET45.html) method, as follows:

   ```
   ReceiveMessageResponse receiveMessageResponse =
     amazonSQSClient.ReceiveMessage(receiveMessageRequest);
   ```

   The method returns a [ReceiveMessageResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSReceiveMessageResponseNET45.html) instance, containing the list of messages the queue contains\.

1. The response object contains a [ReceiveMessageResult](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSReceiveMessageResultNET45.html) member\. This member includes a [Messages](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSMessageNET45.html) list\. Iterate through this list to find a specific message, and use the `Body` property to determine if the list contains a specified message, as follows:

   ```
   if (result.Message.Count != 0)
   {
     for (int i = 0; i < result.Message.Count; i++)
     {
       if (result.Message[i].Body == messageBody)
       {
         receiptHandle = result.Message[i].ReceiptHandle;
       }
     }
   }
   ```

   Once the message is found in the list, use the `ReceiptHandle` property to obtain a receipt handle for the message\. You can use this receipt handle to change message visibility timeout or to delete the message from the queue\. For more information about how to change the visibility timeout for a message, go to [ChangeMessageVisibility](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSChangeMessageVisibilityRequestNET45.html)\.

For information about sending a message to your queue, see [Send an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For more information about deleting a message from the queue, see [Delete a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.