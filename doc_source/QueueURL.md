--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Amazon SQS Queue URLs<a name="QueueURL"></a>

You require the queue URL to send, receive, and delete queue messages\. A queue URL is constructed in the following format:

```
https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
```

For information on sending a message to a queue, see [Send an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For information about receiving messages from a queue, see [Receive a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.

For information about deleting messages from a queue, see [Delete a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.