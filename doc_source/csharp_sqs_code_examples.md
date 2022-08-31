# Amazon SQS examples using AWS SDK for \.NET<a name="csharp_sqs_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon SQS\.

*Actions* are code excerpts that show you how to call individual Amazon SQS functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Amazon SQS functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w198aac21c17b9c35c13)

## Actions<a name="w198aac21c17b9c35c13"></a>

### Authorize a bucket to send messages to a queue<a name="sqs_AuthorizeS3ToSendMessage_csharp_topic"></a>

The following code example shows how to authorize an Amazon S3 bucket to send messages to an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.SQS;

    public class AuthorizeS3ToSendMessage
    {
        /// <summary>
        /// Initializes the Amazon SQS client object and then calls the
        /// AuthorizeS3ToSendMessageAsync method to authorize the named
        /// bucket to send messages in response to S3 events.
        /// </summary>
        public static async Task Main()
        {
            string queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/Example_Queue";
            string bucketName = "doc-example-bucket";

            // Create an Amazon SQS client object using the
            // default user. If the AWS Region you want to use
            // is different, supply the AWS Region as a parameter.
            IAmazonSQS client = new AmazonSQSClient();

            var queueARN = await client.AuthorizeS3ToSendMessageAsync(queueUrl, bucketName);

            if (!string.IsNullOrEmpty(queueARN))
            {
                Console.WriteLine($"The Amazon S3 bucket: {bucketName} has been successfully authorized.");
                Console.WriteLine($"{bucketName} can now send messages to the queue with ARN: {queueARN}.");
            }
        }
    }
```

### Create a queue<a name="sqs_CreateQueue_csharp_topic"></a>

The following code example shows how to create an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class CreateQueue
    {
        /// <summary>
        /// Initializes the Amazon SQS client object and then calls the
        /// CreateQueueAsync method to create the new queue. If the call is
        /// successful, it displays the URL of the new queue on the console.
        /// </summary>
        public static async Task Main()
        {
            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            string queueName = "New-Example-Queue";
            int maxMessage = 256 * 1024;
            var attrs = new Dictionary<string, string>
            {
                {
                    QueueAttributeName.DelaySeconds,
                    TimeSpan.FromSeconds(5).TotalSeconds.ToString()
                },
                {
                    QueueAttributeName.MaximumMessageSize,
                    maxMessage.ToString()
                },
                {
                    QueueAttributeName.MessageRetentionPeriod,
                    TimeSpan.FromDays(4).TotalSeconds.ToString()
                },
                {
                    QueueAttributeName.ReceiveMessageWaitTimeSeconds,
                    TimeSpan.FromSeconds(5).TotalSeconds.ToString()
                },
                {
                    QueueAttributeName.VisibilityTimeout,
                    TimeSpan.FromHours(12).TotalSeconds.ToString()
                },
            };

            var request = new CreateQueueRequest
            {
                Attributes = attrs,
                QueueName = queueName,
            };

            var response = await client.CreateQueueAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Successfully created Amazon SQS queue.");
                Console.WriteLine($"Queue URL: {response.QueueUrl}");
            }
        }
    }
```
Create an Amazon SQS queue and send a message to it\.  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class CreateSendExample
    {
        // Specify your AWS Region (an example Region is shown).
        private static readonly string QueueName = "Example_Queue";
        private static readonly RegionEndpoint ServiceRegion = RegionEndpoint.USWest2;
        private static IAmazonSQS client;

        public static async Task Main()
        {
            client = new AmazonSQSClient(ServiceRegion);
            var createQueueResponse = await CreateQueue(client, QueueName);

            string queueUrl = createQueueResponse.QueueUrl;

            Dictionary<string, MessageAttributeValue> messageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                { "Title",   new MessageAttributeValue { DataType = "String", StringValue = "The Whistler" } },
                { "Author",  new MessageAttributeValue { DataType = "String", StringValue = "John Grisham" } },
                { "WeeksOn", new MessageAttributeValue { DataType = "Number", StringValue = "6" } },
            };

            string messageBody = "Information about current NY Times fiction bestseller for week of 12/11/2016.";

            var sendMsgResponse = await SendMessage(client, queueUrl, messageBody, messageAttributes);
        }

        /// <summary>
        /// Creates a new Amazon SQS queue using the queue name passed to it
        /// in queueName.
        /// </summary>
        /// <param name="client">An SQS client object used to send the message.</param>
        /// <param name="queueName">A string representing the name of the queue
        /// to create.</param>
        /// <returns>A CreateQueueResponse that contains information about the
        /// newly created queue.</returns>
        public static async Task<CreateQueueResponse> CreateQueue(IAmazonSQS client, string queueName)
        {
            var request = new CreateQueueRequest
            {
                QueueName = queueName,
                Attributes = new Dictionary<string, string>
                {
                    { "DelaySeconds", "60" },
                    { "MessageRetentionPeriod", "86400" },
                },
            };

            var response = await client.CreateQueueAsync(request);
            Console.WriteLine($"Created a queue with URL : {response.QueueUrl}");

            return response;
        }

        /// <summary>
        /// Sends a message to an SQS queue.
        /// </summary>
        /// <param name="client">An SQS client object used to send the message.</param>
        /// <param name="queueUrl">The URL of the queue to which to send the
        /// message.</param>
        /// <param name="messageBody">A string representing the body of the
        /// message to be sent to the queue.</param>
        /// <param name="messageAttributes">Attributes for the message to be
        /// sent to the queue.</param>
        /// <returns>A SendMessageResponse object that contains information
        /// about the message that was sent.</returns>
        public static async Task<SendMessageResponse> SendMessage(
            IAmazonSQS client,
            string queueUrl,
            string messageBody,
            Dictionary<string, MessageAttributeValue> messageAttributes)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                DelaySeconds = 10,
                MessageAttributes = messageAttributes,
                MessageBody = messageBody,
                QueueUrl = queueUrl,
            };

            var response = await client.SendMessageAsync(sendMessageRequest);
            Console.WriteLine($"Sent a message with id : {response.MessageId}");

            return response;
        }
    }
```
+  For API details, see [CreateQueue](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/CreateQueue) in *AWS SDK for \.NET API Reference*\. 

### Delete a message from a queue<a name="sqs_DeleteMessage_csharp_topic"></a>

The following code example shows how to delete a message from an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class DeleteMessage
    {
        /// <summary>
        /// Initializes the Amazon SQS client object. It then calls the
        /// ReceiveMessageAsync method to retrieve information about the
        /// available methods before deleting them.
        /// </summary>
        public static async Task Main()
        {
            string queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/Example_Queue";
            var attributeNames = new List<string>() { "All" };
            int maxNumberOfMessages = 5;
            var visibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
            var waitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds;

            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = attributeNames,
                MaxNumberOfMessages = maxNumberOfMessages,
                VisibilityTimeout = visibilityTimeout,
                WaitTimeSeconds = waitTimeSeconds,
            };

            var response = await client.ReceiveMessageAsync(request);

            if (response.Messages.Count > 0)
            {
                response.Messages.ForEach(async m =>
                {
                    Console.Write($"Message ID: '{m.MessageId}'");

                    var delRequest = new DeleteMessageRequest
                    {
                        QueueUrl = "https://sqs.us-east-1.amazonaws.com/0123456789ab/MyTestQueue",
                        ReceiptHandle = m.ReceiptHandle,
                    };

                    var delResponse = await client.DeleteMessageAsync(delRequest);
                });
            }
            else
            {
                Console.WriteLine("No messages to delete.");
            }
        }
    }
```
Receive a message from an Amazon SQS queue and then delete the message\.  

```
        public static async Task Main()
        {
            // If the AWS Region you want to use is different from
            // the AWS Region defined for the default user, supply
            // the specify your AWS Region to the client constructor.
            var client = new AmazonSQSClient();
            string queueName = "Example_Queue";

            var queueUrl = await GetQueueUrl(client, queueName);
            Console.WriteLine($"The SQS queue's URL is {queueUrl}");

            var response = await ReceiveAndDeleteMessage(client, queueUrl);

            Console.WriteLine($"Message: {response.Messages[0]}");
        }

        /// <summary>
        /// Retrieve the queue URL for the queue named in the queueName
        /// property using the client object.
        /// </summary>
        /// <param name="client">The Amazon SQS client used to retrieve the
        /// queue URL.</param>
        /// <param name="queueName">A string representing  name of the queue
        /// for which to retrieve the URL.</param>
        /// <returns>The URL of the queue.</returns>
        public static async Task<string> GetQueueUrl(IAmazonSQS client, string queueName)
        {
            var request = new GetQueueUrlRequest
            {
                QueueName = queueName,
            };

            GetQueueUrlResponse response = await client.GetQueueUrlAsync(request);
            return response.QueueUrl;
        }

        /// <summary>
        /// Retrieves the message from the quque at the URL passed in the
        /// queueURL parameters using the client.
        /// </summary>
        /// <param name="client">The SQS client used to retrieve a message.</param>
        /// <param name="queueUrl">The URL of the queue from which to retrieve
        /// a message.</param>
        /// <returns>The response from the call to ReceiveMessageAsync.</returns>
        public static async Task<ReceiveMessageResponse> ReceiveAndDeleteMessage(IAmazonSQS client, string queueUrl)
        {
            // Receive a single message from the queue.
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = queueUrl,
                VisibilityTimeout = 0,
                WaitTimeSeconds = 0,
            };

            var receiveMessageResponse = await client.ReceiveMessageAsync(receiveMessageRequest);

            // Delete the received message from the queue.
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiveMessageResponse.Messages[0].ReceiptHandle,
            };

            await client.DeleteMessageAsync(deleteMessageRequest);

            return receiveMessageResponse;
        }
    }
```
+  For API details, see [DeleteMessage](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/DeleteMessage) in *AWS SDK for \.NET API Reference*\. 

### Delete a queue<a name="sqs_DeleteQueue_csharp_topic"></a>

The following code example shows how to delete an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.SQS;

    public class DeleteQueue
    {
        /// <summary>
        /// Initializes the Amazon SQS client object and then calls the
        /// DeleteQueueAsync method to delete the queue.
        /// </summary>
        public static async Task Main()
        {
            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            string queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/New-Example-Queue";

            var response = await client.DeleteQueueAsync(queueUrl);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Successfully deleted the queue.");
            }
            else
            {
                Console.WriteLine("Could not delete the crew.");
            }
        }
    }
```
+  For API details, see [DeleteQueue](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/DeleteQueue) in *AWS SDK for \.NET API Reference*\. 

### Get attributes for a queue<a name="sqs_GetQueueAttributes_csharp_topic"></a>

The following code example shows how to get attributes for an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class GetQueueAttributes
    {
        /// <summary>
        /// Initializes the Amazon SQS client and then uses it to call the
        /// GetQueueAttributesAsync method to retrieve the attributes for the
        /// Amazon SQS queue.
        /// </summary>
        public static async Task Main()
        {
            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            var queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/New-Example-Queue";
            var attrs = new List<string>() { "All" };

            var request = new GetQueueAttributesRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = attrs,
            };

            var response = await client.GetQueueAttributesAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                DisplayAttributes(response);
            }
        }

        /// <summary>
        /// Displays the attributes passed to the method on the console.
        /// </summary>
        /// <param name="attrs">The attributes for the Amazon SQS queue.</param>
        public static void DisplayAttributes(GetQueueAttributesResponse attrs)
        {
            Console.WriteLine($"Attributes for queue ARN '{attrs.QueueARN}':");
            Console.WriteLine($"  Approximate number of messages: {attrs.ApproximateNumberOfMessages}");
            Console.WriteLine($"  Approximate number of messages delayed: {attrs.ApproximateNumberOfMessagesDelayed}");
            Console.WriteLine($"  Approximate number of messages not visible: {attrs.ApproximateNumberOfMessagesNotVisible}");
            Console.WriteLine($"  Queue created on: {attrs.CreatedTimestamp}");
            Console.WriteLine($"  Delay seconds: {attrs.DelaySeconds}");
            Console.WriteLine($"  Queue last modified on: {attrs.LastModifiedTimestamp}");
            Console.WriteLine($"  Maximum message size: {attrs.MaximumMessageSize}");
            Console.WriteLine($"  Message retention period: {attrs.MessageRetentionPeriod}");
            Console.WriteLine($"  Visibility timeout: {attrs.VisibilityTimeout}");
            Console.WriteLine($"  Policy: {attrs.Policy}\n");
            Console.WriteLine("  Attributes:");

            foreach (var attr in attrs.Attributes)
            {
                Console.WriteLine($"    {attr.Key}: {attr.Value}");
            }
        }
    }
```
+  For API details, see [GetQueueAttributes](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/GetQueueAttributes) in *AWS SDK for \.NET API Reference*\. 

### Get the URL of a queue<a name="sqs_GetQueueUrl_csharp_topic"></a>

The following code example shows how to get the URL of an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class GetQueueUrl
    {
        /// <summary>
        /// Initializes the Amazon SQS client object and then calls the
        /// GetQueueUrlAsync method to retrieve the URL of an Amazon SQS
        /// queue.
        /// </summary>
        public static async Task Main()
        {
            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            string queueName = "New-Exampe-Queue";

            try
            {
                var response = await client.GetQueueUrlAsync(queueName);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"The URL for {queueName} is: {response.QueueUrl}");
                }
            }
            catch (QueueDoesNotExistException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"The queue {queueName} was not found.");
            }
        }
    }
```
+  For API details, see [GetQueueUrl](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/GetQueueUrl) in *AWS SDK for \.NET API Reference*\. 

### Receive messages from a queue<a name="sqs_ReceiveMessage_csharp_topic"></a>

The following code example shows how to receive messages from an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class ReceiveFromQueue
    {
        public static async Task Main(string[] args)
        {
            string queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/Example_Queue";
            var attributeNames = new List<string>() { "All" };
            int maxNumberOfMessages = 5;
            var visibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
            var waitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds;

            // If the Amazon SQS message queue is not in the same AWS Region as your
            // default user, you need to provide the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonSQSClient();

            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = attributeNames,
                MaxNumberOfMessages = maxNumberOfMessages,
                VisibilityTimeout = visibilityTimeout,
                WaitTimeSeconds = waitTimeSeconds,
            };

            var response = await client.ReceiveMessageAsync(request);

            if (response.Messages.Count > 0)
            {
                DisplayMessages(response.Messages);
            }
        }

        /// <summary>
        /// Display message information for a list of Amazon SQS messages.
        /// </summary>
        /// <param name="messages">The list of Amazon SQS Message objects to display.</param>
        public static void DisplayMessages(List<Message> messages)
        {
            messages.ForEach(m =>
            {
                Console.WriteLine($"For message ID {m.MessageId}:");
                Console.WriteLine($"  Body: {m.Body}");
                Console.WriteLine($"  Receipt handle: {m.ReceiptHandle}");
                Console.WriteLine($"  MD5 of body: {m.MD5OfBody}");
                Console.WriteLine($"  MD5 of message attributes: {m.MD5OfMessageAttributes}");
                Console.WriteLine("  Attributes:");

                foreach (var attr in m.Attributes)
                {
                    Console.WriteLine($"\t {attr.Key}: {attr.Value}");
                }
            });
        }
    }
```
Receive a message from an Amazon SQS queue, and then delete the message\.  

```
        public static async Task Main()
        {
            // If the AWS Region you want to use is different from
            // the AWS Region defined for the default user, supply
            // the specify your AWS Region to the client constructor.
            var client = new AmazonSQSClient();
            string queueName = "Example_Queue";

            var queueUrl = await GetQueueUrl(client, queueName);
            Console.WriteLine($"The SQS queue's URL is {queueUrl}");

            var response = await ReceiveAndDeleteMessage(client, queueUrl);

            Console.WriteLine($"Message: {response.Messages[0]}");
        }

        /// <summary>
        /// Retrieve the queue URL for the queue named in the queueName
        /// property using the client object.
        /// </summary>
        /// <param name="client">The Amazon SQS client used to retrieve the
        /// queue URL.</param>
        /// <param name="queueName">A string representing  name of the queue
        /// for which to retrieve the URL.</param>
        /// <returns>The URL of the queue.</returns>
        public static async Task<string> GetQueueUrl(IAmazonSQS client, string queueName)
        {
            var request = new GetQueueUrlRequest
            {
                QueueName = queueName,
            };

            GetQueueUrlResponse response = await client.GetQueueUrlAsync(request);
            return response.QueueUrl;
        }

        /// <summary>
        /// Retrieves the message from the quque at the URL passed in the
        /// queueURL parameters using the client.
        /// </summary>
        /// <param name="client">The SQS client used to retrieve a message.</param>
        /// <param name="queueUrl">The URL of the queue from which to retrieve
        /// a message.</param>
        /// <returns>The response from the call to ReceiveMessageAsync.</returns>
        public static async Task<ReceiveMessageResponse> ReceiveAndDeleteMessage(IAmazonSQS client, string queueUrl)
        {
            // Receive a single message from the queue.
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = queueUrl,
                VisibilityTimeout = 0,
                WaitTimeSeconds = 0,
            };

            var receiveMessageResponse = await client.ReceiveMessageAsync(receiveMessageRequest);

            // Delete the received message from the queue.
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiveMessageResponse.Messages[0].ReceiptHandle,
            };

            await client.DeleteMessageAsync(deleteMessageRequest);

            return receiveMessageResponse;
        }
    }
```
+  For API details, see [ReceiveMessage](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/ReceiveMessage) in *AWS SDK for \.NET API Reference*\. 

### Send a message to a queue<a name="sqs_SendMessage_csharp_topic"></a>

The following code example shows how to send a message to an Amazon SQS queue\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/SQS#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class SendMessageToQueue
    {
        /// <summary>
        /// Initialize the Amazon SQS client object and use the
        /// SendMessageAsync method to send a message to an Amazon SQS queue.
        /// </summary>
        public static async Task Main()
        {
            string messageBody = "This is a sample message to send to the example queue.";
            string queueUrl = "https://sqs.us-east-2.amazonaws.com/0123456789ab/Example_Queue";

            // Create an Amazon SQS client object using the
            // default user. If the AWS Region you want to use
            // is different, supply the AWS Region as a parameter.
            IAmazonSQS client = new AmazonSQSClient();

            var request = new SendMessageRequest
            {
                MessageBody = messageBody,
                QueueUrl = queueUrl,
            };

            var response = await client.SendMessageAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully sent message. Message ID: {response.MessageId}");
            }
            else
            {
                Console.WriteLine("Could not send message.");
            }
        }
    }
```
Create an Amazon SQS queue and send a message to it\.  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    public class CreateSendExample
    {
        // Specify your AWS Region (an example Region is shown).
        private static readonly string QueueName = "Example_Queue";
        private static readonly RegionEndpoint ServiceRegion = RegionEndpoint.USWest2;
        private static IAmazonSQS client;

        public static async Task Main()
        {
            client = new AmazonSQSClient(ServiceRegion);
            var createQueueResponse = await CreateQueue(client, QueueName);

            string queueUrl = createQueueResponse.QueueUrl;

            Dictionary<string, MessageAttributeValue> messageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                { "Title",   new MessageAttributeValue { DataType = "String", StringValue = "The Whistler" } },
                { "Author",  new MessageAttributeValue { DataType = "String", StringValue = "John Grisham" } },
                { "WeeksOn", new MessageAttributeValue { DataType = "Number", StringValue = "6" } },
            };

            string messageBody = "Information about current NY Times fiction bestseller for week of 12/11/2016.";

            var sendMsgResponse = await SendMessage(client, queueUrl, messageBody, messageAttributes);
        }

        /// <summary>
        /// Creates a new Amazon SQS queue using the queue name passed to it
        /// in queueName.
        /// </summary>
        /// <param name="client">An SQS client object used to send the message.</param>
        /// <param name="queueName">A string representing the name of the queue
        /// to create.</param>
        /// <returns>A CreateQueueResponse that contains information about the
        /// newly created queue.</returns>
        public static async Task<CreateQueueResponse> CreateQueue(IAmazonSQS client, string queueName)
        {
            var request = new CreateQueueRequest
            {
                QueueName = queueName,
                Attributes = new Dictionary<string, string>
                {
                    { "DelaySeconds", "60" },
                    { "MessageRetentionPeriod", "86400" },
                },
            };

            var response = await client.CreateQueueAsync(request);
            Console.WriteLine($"Created a queue with URL : {response.QueueUrl}");

            return response;
        }

        /// <summary>
        /// Sends a message to an SQS queue.
        /// </summary>
        /// <param name="client">An SQS client object used to send the message.</param>
        /// <param name="queueUrl">The URL of the queue to which to send the
        /// message.</param>
        /// <param name="messageBody">A string representing the body of the
        /// message to be sent to the queue.</param>
        /// <param name="messageAttributes">Attributes for the message to be
        /// sent to the queue.</param>
        /// <returns>A SendMessageResponse object that contains information
        /// about the message that was sent.</returns>
        public static async Task<SendMessageResponse> SendMessage(
            IAmazonSQS client,
            string queueUrl,
            string messageBody,
            Dictionary<string, MessageAttributeValue> messageAttributes)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                DelaySeconds = 10,
                MessageAttributes = messageAttributes,
                MessageBody = messageBody,
                QueueUrl = queueUrl,
            };

            var response = await client.SendMessageAsync(sendMessageRequest);
            Console.WriteLine($"Sent a message with id : {response.MessageId}");

            return response;
        }
    }
```
+  For API details, see [SendMessage](https://docs.aws.amazon.com/goto/DotNetSDKV3/sqs-2012-11-05/SendMessage) in *AWS SDK for \.NET API Reference*\. 