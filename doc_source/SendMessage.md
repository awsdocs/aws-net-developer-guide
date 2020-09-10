--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Sending Amazon SQS messages<a name="SendMessage"></a>

This example shows you how to use the AWS SDK for \.NET to send messages to an Amazon SQS queue, which you can create [programmatically](CreateQueue.md) or by using the [Amazon SQS console](https://console.aws.amazon.com/sqs)\. The application sends a single message to the queue and then a batch of messages\. The application then waits for user input, which can be additional messages to send to the queue or a request to exit the application\.

This example and the [next example about receiving messages](ReceiveMessage.md) can be used together to see message flow in Amazon SQS\.

The following sections provide snippets of this example\. The [complete code for the example](#SendMessage-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Send a message](#SendMessage-send-message)
+ [Send a batch of messages](#SendMessage-send-batch)
+ [Delete all messages from the queue](#SendMessage-purge-messages)
+ [Complete code](#SendMessage-complete-code)

## Send a message<a name="SendMessage-send-message"></a>

The following snippet sends a message to the queue identified by the given queue URL\.

The example [at the end of this topic](#SendMessage-complete-code) shows this snippet in use\.

```
    //
    // Method to put a message on a queue
    // Could be expanded to include message attributes, etc., in a SendMessageRequest
    private static async Task SendMessage(
      IAmazonSQS sqsClient, string qUrl, string messageBody)
    {
      SendMessageResponse responseSendMsg =
        await sqsClient.SendMessageAsync(qUrl, messageBody);
      Console.WriteLine($"Message added to queue\n  {qUrl}");
      Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");
    }
```

## Send a batch of messages<a name="SendMessage-send-batch"></a>

The following snippet sends a batch of messages to the queue identified by the given queue URL\.

The example [at the end of this topic](#SendMessage-complete-code) shows this snippet in use\.

```
    //
    // Method to put a batch of messages on a queue
    // Could be expanded to include message attributes, etc.,
    // in the SendMessageBatchRequestEntry objects
    private static async Task SendMessageBatch(
      IAmazonSQS sqsClient, string qUrl, List<SendMessageBatchRequestEntry> messages)
    {
      Console.WriteLine($"\nSending a batch of messages to queue\n  {qUrl}");
      SendMessageBatchResponse responseSendBatch =
        await sqsClient.SendMessageBatchAsync(qUrl, messages);
      // Could test responseSendBatch.Failed here
      foreach(SendMessageBatchResultEntry entry in responseSendBatch.Successful)
        Console.WriteLine($"Message {entry.Id} successfully queued.");
    }
```

## Delete all messages from the queue<a name="SendMessage-purge-messages"></a>

The following snippet deletes all messages from the queue identified by the given queue URL\. This is also known as *purging the queue*\.

The example [at the end of this topic](#SendMessage-complete-code) shows this snippet in use\.

```
    //
    // Method to delete all messages from the queue
    private static async Task DeleteAllMessages(IAmazonSQS sqsClient, string qUrl)
    {
      Console.WriteLine($"\nPurging messages from queue\n  {qUrl}...");
      PurgeQueueResponse responsePurge = await sqsClient.PurgeQueueAsync(qUrl);
      Console.WriteLine($"HttpStatusCode: {responsePurge.HttpStatusCode}");
    }
```

## Complete code<a name="SendMessage-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c25c25c25b5b1"></a>

NuGet packages:
+ [AWSSDK\.SQS](https://www.nuget.org/packages/AWSSDK.SQS)

Programming elements:
+ Namespace [Amazon\.SQS](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQS.html)

  Class [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html)
+ Namespace [Amazon\.SQS\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQSModel.html)

  Class [PurgeQueueResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TPurgeQueueResponse.html)

  Class [SendMessageBatchResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageBatchResponse.html)

  Class [SendMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageResponse.html)

  Class [SendMessageBatchRequestEntry](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageBatchRequestEntry.html)

  Class [SendMessageBatchResultEntry](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageBatchResultEntry.html)

### The code<a name="w4aac19c25c25c25b7b1"></a>

```
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSSendMessages
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to send messages to a queue
  class Program
  {
    // Some example messages to send to the queue
    private const string JsonMessage = "{\"product\":[{\"name\":\"Product A\",\"price\": \"32\"},{\"name\": \"Product B\",\"price\": \"27\"}]}";
    private const string XmlMessage = "<products><product name=\"Product A\" price=\"32\" /><product name=\"Product B\" price=\"27\" /></products>";
    private const string CustomMessage = "||product|Product A|32||product|Product B|27||";
    private const string TextMessage = "Just a plain text message.";

    static async Task Main(string[] args)
    {
      // Do some checks on the command-line
      if(args.Length == 0)
      {
        Console.WriteLine("\nUsage: SQSSendMessages queue_url");
        Console.WriteLine("   queue_url - The URL of an existing SQS queue.");
        return;
      }
      if(!args[0].StartsWith("https://sqs."))
      {
        Console.WriteLine("\nThe command-line argument isn't a queue URL:");
        Console.WriteLine($"{args[0]}");
        return;
      }

      // Create the Amazon SQS client
      var sqsClient = new AmazonSQSClient();

      // (could verify that the queue exists)
      // Send some example messages to the given queue
      // A single message
      await SendMessage(sqsClient, args[0], JsonMessage);

      // A batch of messages
      var batchMessages = new List<SendMessageBatchRequestEntry>{
        new SendMessageBatchRequestEntry("xmlMsg", XmlMessage),
        new SendMessageBatchRequestEntry("customeMsg", CustomMessage),
        new SendMessageBatchRequestEntry("textMsg", TextMessage)};
      await SendMessageBatch(sqsClient, args[0], batchMessages);

      // Let the user send their own messages or quit
      await InteractWithUser(sqsClient, args[0]);

      // Delete all messages that are still in the queue
      await DeleteAllMessages(sqsClient, args[0]);
    }


    //
    // Method to put a message on a queue
    // Could be expanded to include message attributes, etc., in a SendMessageRequest
    private static async Task SendMessage(
      IAmazonSQS sqsClient, string qUrl, string messageBody)
    {
      SendMessageResponse responseSendMsg =
        await sqsClient.SendMessageAsync(qUrl, messageBody);
      Console.WriteLine($"Message added to queue\n  {qUrl}");
      Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");
    }


    //
    // Method to put a batch of messages on a queue
    // Could be expanded to include message attributes, etc.,
    // in the SendMessageBatchRequestEntry objects
    private static async Task SendMessageBatch(
      IAmazonSQS sqsClient, string qUrl, List<SendMessageBatchRequestEntry> messages)
    {
      Console.WriteLine($"\nSending a batch of messages to queue\n  {qUrl}");
      SendMessageBatchResponse responseSendBatch =
        await sqsClient.SendMessageBatchAsync(qUrl, messages);
      // Could test responseSendBatch.Failed here
      foreach(SendMessageBatchResultEntry entry in responseSendBatch.Successful)
        Console.WriteLine($"Message {entry.Id} successfully queued.");
    }


    //
    // Method to get input from the user
    // They can provide messages to put in the queue or exit the application
    private static async Task InteractWithUser(IAmazonSQS sqsClient, string qUrl)
    {
      string response;
      while (true)
      {
        // Get the user's input
        Console.WriteLine("\nType a message for the queue or \"exit\" to quit:");
        response = Console.ReadLine();
        if(response.ToLower() == "exit") break;

        // Put the user's message in the queue
        await SendMessage(sqsClient, qUrl, response);
      }
    }


    //
    // Method to delete all messages from the queue
    private static async Task DeleteAllMessages(IAmazonSQS sqsClient, string qUrl)
    {
      Console.WriteLine($"\nPurging messages from queue\n  {qUrl}...");
      PurgeQueueResponse responsePurge = await sqsClient.PurgeQueueAsync(qUrl);
      Console.WriteLine($"HttpStatusCode: {responsePurge.HttpStatusCode}");
    }
  }
}
```

**Additional considerations**
+ For information about various limitations on messages, including the allowed characters, see [Quotas related to messages](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-quotas.html#quotas-messages) in the [Amazon Simple Queue Service Developer Guide](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/)\.
+ Messages stay in queues until they are deleted or the queue is purged\. When a message has been received by an application, it won't be visible in the queue even though it still exists in the queue\. For more information about visibility timeouts, see [Amazon SQS visibility timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\.
+ In additional to the message body, you can also add attributes to messages\. For more information, see [Message metadata](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-message-metadata.html)\.