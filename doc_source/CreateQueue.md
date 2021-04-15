--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Creating an Amazon SQS Queue<a name="CreateQueue"></a>

Creating an Amazon SQS queue is an administrative task that you can do by using the [SQS Management Console](https://console.aws.amazon.com/sqs/home)\. However, you can also use the AWS SDK for \.NET to programmatically create an Amazon SQS queue\.

**To create an Amazon SQS queue**

1. Create and initialize a [CreateQueueRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TCreateQueueRequest.html) instance\. Provide the name of your queue and specify a visibility timeout for your queue messages, as follows\.

   ```
   var createQueueRequest = new CreateQueueRequest();
   
   createQueueRequest.QueueName = "MySQSQueue";
   var attrs = new Dictionary<string, string>();
   attrs.Add(QueueAttributeName.VisibilityTimeout, "10");
   createQueueRequest.Attributes = attrs;
   ```

   Your queue name must be composed of only alphanumeric characters, hyphens, and underscores\.

   Any message in the queue remains in the queue unless the specified visibility timeout is exceeded\. The default visibility timeout for a queue is 30 seconds\. For more information about visibility timeouts, see [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\. For more information about different queue attributes you can set, see [SetQueueAttributes](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SetQueueAttributes.html)\.

1. After you create the request, pass it as a parameter to the [CreateQueue](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/MSQSCreateQueueCreateQueueRequest.html) method\. The method returns a [CreateQueueResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TCreateQueueResponse.html) object, as follows\.

   ```
   var createQueueResponse = sqsClient.CreateQueue(createQueueRequest);
   ```

For information about how queues work in Amazon SQS, see [How SQS Queues Work](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/SQSConcepts.html)\.

For information about your queue URL, see [Constructing Amazon SQS Queue URLs](QueueURL.md#sqs-queue-url)\.