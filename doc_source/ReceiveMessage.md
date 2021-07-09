--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Receiving a Message from an Amazon SQS Queue<a name="ReceiveMessage"></a>

The content in this topic is for **version 3\.0** of the AWS SDK for \.NET\.

For content related to **version 3\.5 or later** \(and \.NET Core\), see the [latest developer guide](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/ReceiveMessage.html) instead\.

## Latest content \- \.NET Core and ASP\.NET Core<a name="w8aac15c35c29b7b1"></a>

[Latest developer guide](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/ReceiveMessage.html)\.

## V3 content \- \.NET Framework and ASP\.NET 4\.x<a name="w8aac15c35c29b9b1"></a>

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
   foreach (var message in receiveMessageResponse.Messages)
   {
     ProcessMessage(message);  // Go to a method to process messages.
   }
   ```

   The `ProcessMessage` method can use the `ReceiptHandle` property to obtain a receipt handle for the message\. You can use this receipt handle to change the message visibility timeout or to delete the message from the queue\. For more information about how to change the visibility timeout for a message, see [ChangeMessageVisibility](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSChangeMessageVisibilityChangeMessageVisibilityRequest.html)\.

For information about sending a message to your queue, see [Sending an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For more information about deleting a message from the queue, see [Deleting a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.