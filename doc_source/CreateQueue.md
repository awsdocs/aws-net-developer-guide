--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.

--------

# Create an Amazon SQS Queue<a name="CreateQueue"></a>

You can use the AWS SDK for \.NET to programmatically create an Amazon SQS queue\. Creating an Amazon SQS Queue is an administrative task\. You can create a queue by using the [AWS Management Console](https://console.aws.amazon.com/sqs/home) instead of creating a queue programmatically\.

 **To create an Amazon SQS queue** 

1. Create and initialize a [CreateQueueRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSCreateQueueRequestNET45.html) instance\. Provide the name of your queue and specify a visibility timeout for your queue messages, as follows:

   ```
   CreateQueueRequest createQueueRequest =
       new CreateQueueRequest();
   
   createQueueRequest.QueueName = "MySQSQueue";
   createQueueRequest.DefaultVisibilityTimeout = 10;
   ```

   Your queue name must only be composed of alphanumeric characters, hyphens, and underscores\.

   Any message in the queue remains in the queue unless the specified visibility timeout is exceeded\. The default visibility timeout for a queue is 30 seconds\. For more information about visibility timeouts, go to [Visibility Timeout](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/AboutVT.html)\. For more information about different queue attributes you can set, go to [SetQueueAttributes](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SetQueueAttributes.html)\.

1. After you create the request, pass it as a parameter to the [CreateQueue](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MSQSSQSCreateQueueCreateQueueRequestNET45.html) method\. The method returns a [CreateQueueResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSCreateQueueResponseNET45.html) object, as follows:

   ```
   CreateQueueResponse createQueueResponse =
       amazonSQSClient.CreateQueue(createQueueRequest);
   ```

For information about how queues work in Amazon SQS, go to [How SQS Queues Work](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/SQSConcepts.html)\.

For information about your queue URL, see [Amazon SQS Queue URLs](QueueURL.md#sqs-queue-url)\.