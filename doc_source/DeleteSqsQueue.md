--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Deleting Amazon SQS queues<a name="DeleteSqsQueue"></a>

This example shows you how to use the AWS SDK for \.NET to delete an Amazon SQS queue\. The application deletes the queue, waits for up to a given amount of time for the queue to be gone, and then shows a list of the remaining queues\.

If you don't supply any command\-line arguments, the application simply shows a list of the existing queues\.

The following sections provide snippets of this example\. The [complete code for the example](#DeleteSqsQueue-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Delete the queue](#DeleteSqsQueue-delete-queue)
+ [Wait for the queue to be gone](#DeleteSqsQueue-wait)
+ [Show a list of existing queues](#DeleteSqsQueue-list-queues)
+ [Complete code](#DeleteSqsQueue-complete-code)

## Delete the queue<a name="DeleteSqsQueue-delete-queue"></a>

The following snippet deletes the queue identified by the given queue URL\.

The example [at the end of this topic](#DeleteSqsQueue-complete-code) shows this snippet in use\.

```
    //
    // Method to delete an SQS queue
    private static async Task DeleteQueue(IAmazonSQS sqsClient, string qUrl)
    {
      Console.WriteLine($"Deleting queue {qUrl}...");
      await sqsClient.DeleteQueueAsync(qUrl);
      Console.WriteLine($"Queue {qUrl} has been deleted.");
    }
```

## Wait for the queue to be gone<a name="DeleteSqsQueue-wait"></a>

The following snippet waits for the deletion process to finish, which might take 60 seconds\.

The example [at the end of this topic](#DeleteSqsQueue-complete-code) shows this snippet in use\.

```
    //
    // Method to wait up to a given number of seconds
    private static async Task Wait(
      IAmazonSQS sqsClient, int numSeconds, string qUrl)
    {
      Console.WriteLine($"Waiting for up to {numSeconds} seconds.");
      Console.WriteLine("Press any key to stop waiting. (Response might be slightly delayed.)");
      for(int i=0; i<numSeconds; i++)
      {
        Console.Write(".");
        Thread.Sleep(1000);
        if(Console.KeyAvailable) break;

        // Check to see if the queue is gone yet
        var found = false;
        ListQueuesResponse responseList = await sqsClient.ListQueuesAsync("");
        foreach(var url in responseList.QueueUrls)
        {
          if(url == qUrl)
          {
            found = true;
            break;
          }
        }
        if(!found) break;
      }
    }
```

## Show a list of existing queues<a name="DeleteSqsQueue-list-queues"></a>

The following snippet shows a list of the existing queues in the SQS client's region\.

The example [at the end of this topic](#DeleteSqsQueue-complete-code) shows this snippet in use\.

```
    //
    // Method to show a list of the existing queues
    private static async Task ListQueues(IAmazonSQS sqsClient)
    {
      ListQueuesResponse responseList = await sqsClient.ListQueuesAsync("");
      Console.WriteLine("\nList of queues:");
      foreach(var qUrl in responseList.QueueUrls)
        Console.WriteLine($"- {qUrl}");
    }
```

## Complete code<a name="DeleteSqsQueue-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c25c23c25b5b1"></a>

NuGet packages:
+ [AWSSDK\.SQS](https://www.nuget.org/packages/AWSSDK.SQS)

Programming elements:
+ Namespace [Amazon\.SQS](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQS.html)

  Class [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html)
+ Namespace [Amazon\.SQS\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/NSQSModel.html)

  Class [ListQueuesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TListQueuesResponse.html)

### The code<a name="w4aac19c25c23c25b7b1"></a>

```
using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSDeleteQueue
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to update a queue
  class Program
  {
    private const int TimeToWait = 60;

    static async Task Main(string[] args)
    {
      // Create the Amazon SQS client
      var sqsClient = new AmazonSQSClient();

      // If no command-line arguments, just show a list of the queues
      if(args.Length == 0)
      {
        Console.WriteLine("\nUsage: SQSCreateQueue queue_url");
        Console.WriteLine("   queue_url - The URL of the queue you want to delete.");
        Console.WriteLine("\nNo arguments specified.");
        Console.Write("Do you want to see a list of the existing queues? ((y) or n): ");
        var response = Console.ReadLine();
        if((string.IsNullOrEmpty(response)) || (response.ToLower() == "y"))
          await ListQueues(sqsClient);
        return;
      }

      // If given a queue URL, delete that queue
      if(args[0].StartsWith("https://sqs."))
      {
        // Delete the queue
        await DeleteQueue(sqsClient, args[0]);
        // Wait for a little while because it takes a while for the queue to disappear
        await Wait(sqsClient, TimeToWait, args[0]);
        // Show a list of the remaining queues
        await ListQueues(sqsClient);
      }
      else
      {
        Console.WriteLine("The command-line argument isn't a queue URL:");
        Console.WriteLine($"{args[0]}");
      }
    }


    //
    // Method to delete an SQS queue
    private static async Task DeleteQueue(IAmazonSQS sqsClient, string qUrl)
    {
      Console.WriteLine($"Deleting queue {qUrl}...");
      await sqsClient.DeleteQueueAsync(qUrl);
      Console.WriteLine($"Queue {qUrl} has been deleted.");
    }


    //
    // Method to wait up to a given number of seconds
    private static async Task Wait(
      IAmazonSQS sqsClient, int numSeconds, string qUrl)
    {
      Console.WriteLine($"Waiting for up to {numSeconds} seconds.");
      Console.WriteLine("Press any key to stop waiting. (Response might be slightly delayed.)");
      for(int i=0; i<numSeconds; i++)
      {
        Console.Write(".");
        Thread.Sleep(1000);
        if(Console.KeyAvailable) break;

        // Check to see if the queue is gone yet
        var found = false;
        ListQueuesResponse responseList = await sqsClient.ListQueuesAsync("");
        foreach(var url in responseList.QueueUrls)
        {
          if(url == qUrl)
          {
            found = true;
            break;
          }
        }
        if(!found) break;
      }
    }


    //
    // Method to show a list of the existing queues
    private static async Task ListQueues(IAmazonSQS sqsClient)
    {
      ListQueuesResponse responseList = await sqsClient.ListQueuesAsync("");
      Console.WriteLine("\nList of queues:");
      foreach(var qUrl in responseList.QueueUrls)
        Console.WriteLine($"- {qUrl}");
    }
  }
}
```

**Additional considerations**
+ The `DeleteQueueAsync` API call doesn't check to see if the queue you're deleting is being used as a dead\-letter queue\. A more sophisticated procedure could check for this\.
+ You can also see the list of queues and the results of this example in the [Amazon SQS console](https://console.aws.amazon.com/sqs)\.