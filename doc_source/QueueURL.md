--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Amazon SQS Queue URLs<a name="QueueURL"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c25b7c11b3b1"></a>

You require the queue URL to send, receive, and delete queue messages\. A queue URL is constructed in the following format:

```
https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
```

For information on sending a message to a queue, see [Send an Amazon SQS Message](SendMessage.md#send-sqs-message)\.

For information about receiving messages from a queue, see [Receive a Message from an Amazon SQS Queue](ReceiveMessage.md#receive-sqs-message)\.

For information about deleting messages from a queue, see [Delete a Message from an Amazon SQS Queue](DeleteMessage.md#delete-sqs-message)\.