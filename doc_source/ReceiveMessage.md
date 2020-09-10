--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Receiving Amazon SQS messages<a name="ReceiveMessage"></a>

This example shows you how to use the AWS SDK for \.NET to receive messages from an Amazon SQS queue, which you can create [programmatically](CreateQueue.md) or by using the [Amazon SQS console](https://console.aws.amazon.com/sqs)\. The application reads a single message from the queue, processes the message \(in this case, displays the message body on the console\), and then deletes the message from the queue\. The application repeats these steps until the user types a key on the keyboard\.

This example and the [previous example about sending messages](SendMessage.md) can be used together to see message flow in Amazon SQS\.

The following sections provide snippets of this example\. The [complete code for the example](#ReceiveMessage-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Receive a message](#ReceiveMessage-receive)
+ [Delete a message](#ReceiveMessage-delete)
+ [Complete code](#ReceiveMessage-complete-code)

## Receive a message<a name="ReceiveMessage-receive"></a>

The following snippet receives a message from the queue identified by the given queue URL\.

The example [at the end of this topic](#ReceiveMessage-complete-code) shows this snippet in use\.

```
    //
    // Method to read a message from the given queue
    // In this example, it gets one message at a time
    private static async Task<ReceiveMessageResponse> GetMessage(
      IAmazonSQS sqsClient, string qUrl, int waitTime=0)
    {
      return await sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest{
        QueueUrl=qUrl,
        MaxNumberOfMessages=MaxMessages,
        WaitTimeSeconds=waitTime
        // (Could also request attributes, set visibility timeout, etc.)
      });
    }
```

## Delete a message<a name="ReceiveMessage-delete"></a>

The following snippet deletes a message from the queue identified by the given queue URL\.

The example [at the end of this topic](#ReceiveMessage-complete-code) shows this snippet in use\.

```
    //
    // Method to delete a message from a queue
    private static async Task DeleteMessage(
      IAmazonSQS sqsClient, Message message, string qUrl)
    {
      Console.WriteLine($"\nDeleting message {message.MessageId} from queue...");
      await sqsClient.DeleteMessageAsync(qUrl, message.ReceiptHandle);
    }
```

## Complete code<a name="ReceiveMessage-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c25c27c21b5b1"></a>

NuGet packages:
+ [AWSSDK\.SQS](https://www.nuget.org/packages/AWSSDK.SQS)

Programming elements:
+ Namespace [Amazon\.SQS](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQS.html)

  Class [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html)
+ Namespace [Amazon\.SQS\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQSModel.html)

  Class [ReceiveMessageRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageRequest.html)

  Class [ReceiveMessageResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageResponse.html)

### The code<a name="w4aac19c25c27c21b7b1"></a>

```
using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSReceiveMessages
{
  class Program
  {
    private const int MaxMessages = 1;
    private const int WaitTime = 2;
    static async Task Main(string[] args)
    {
      // Do some checks on the command-line
      if(args.Length == 0)
      {
        Console.WriteLine("\nUsage: SQSReceiveMessages queue_url");
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
      // Read messages from the queue and perform appropriate actions
      Console.WriteLine($"Reading messages from queue\n  {args[0]}");
      Console.WriteLine("Press any key to stop. (Response might be slightly delayed.)");
      do
      {
        var msg = await GetMessage(sqsClient, args[0], WaitTime);
        if(msg.Messages.Count != 0)
        {
          if(ProcessMessage(msg.Messages[0]))
            await DeleteMessage(sqsClient, msg.Messages[0], args[0]);
        }
      } while(!Console.KeyAvailable);
    }


    //
    // Method to read a message from the given queue
    // In this example, it gets one message at a time
    private static async Task<ReceiveMessageResponse> GetMessage(
      IAmazonSQS sqsClient, string qUrl, int waitTime=0)
    {
      return await sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest{
        QueueUrl=qUrl,
        MaxNumberOfMessages=MaxMessages,
        WaitTimeSeconds=waitTime
        // (Could also request attributes, set visibility timeout, etc.)
      });
    }


    //
    // Method to process a message
    // In this example, it simply prints the message
    private static bool ProcessMessage(Message message)
    {
      Console.WriteLine($"\nMessage body of {message.MessageId}:");
      Console.WriteLine($"{message.Body}");
      return true;
    }


    //
    // Method to delete a message from a queue
    private static async Task DeleteMessage(
      IAmazonSQS sqsClient, Message message, string qUrl)
    {
      Console.WriteLine($"\nDeleting message {message.MessageId} from queue...");
      await sqsClient.DeleteMessageAsync(qUrl, message.ReceiptHandle);
    }
  }
}
```

**Additional considerations**
+ To specify long polling, this example uses the `WaitTimeSeconds` property for each call to the `ReceiveMessageAsync` method\.

  You can also specify long polling for all messages on a queue by using the `ReceiveMessageWaitTimeSeconds` attribute when [creating](CreateQueue.md) or [updating](UpdateSqsQueue.md) the queue\.

  For information about short polling versus long polling, see [Short and long polling](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-short-and-long-polling.html) in the *Amazon Simple Queue Service Developer Guide*\.
+ During message processing, you can use the receipt handle to change the message visibility timeout\. For information about how to do this, see the `ChangeMessageVisibilityAsync` methods of the [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) class\.
+ Calling the `DeleteMessageAsync` method unconditionally removes the message from the queue, regardless of the visibility timeout setting\.