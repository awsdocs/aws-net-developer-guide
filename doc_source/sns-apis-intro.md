--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Amazon Simple Notification Service Programming with the AWS SDK for \.NET<a name="sns-apis-intro"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c23b3b1"></a>

The AWS SDK for \.NET supports Amazon Simple Notification Service \(Amazon SNS\), which is a web service that enables applications, end\-users, and devices to instantly send and receive notifications from the cloud\. For more information, see [Amazon SNS](https://aws.amazon.com/sns/)\.

The following information introduces you to the Amazon SNS programming models in the the SDK\.

### Programming Models<a name="sns-apis-intro-models"></a>

The SDK provides two programming models for working with Amazon SNS\. These programming models are known as the *low\-level* and *resource* models\. The following information describes these models, how to use them, and why you would want to use them\.

#### Low\-Level APIs<a name="sns-apis-intro-low-level"></a>

The SDK provides low\-level APIs for programming with Amazon SNS\. These low\-level APIs typically consist of sets of matching request\-and\-response objects that correspond to HTTP\-based API calls focusing on their corresponding service\-level constructs\.

The following example shows how to use the low\-level APIs to list accessible topics in Amazon SNS:

```
// using Amazon.SimpleNotificationService;
// using Amazon.SimpleNotificationService.Model;

var client = new AmazonSimpleNotificationServiceClient();
var request = new ListTopicsRequest();
var response = new ListTopicsResponse();

do
{
  response = client.ListTopics(request);  

  foreach (var topic in response.Topics)
  {
    Console.WriteLine("Topic: {0}", topic.TopicArn);

    var subs = client.ListSubscriptionsByTopic(
      new ListSubscriptionsByTopicRequest
      {
        TopicArn = topic.TopicArn
      });

    var ss = subs.Subscriptions;

    if (ss.Any())
    {
      Console.WriteLine("  Subscriptions:");

      foreach (var sub in ss)
      {
        Console.WriteLine("    {0}", sub.SubscriptionArn);
      }
    }

    var attrs = client.GetTopicAttributes(
      new GetTopicAttributesRequest
      {
        TopicArn = topic.TopicArn
      }).Attributes;

    if (attrs.Any())
    {
      Console.WriteLine("  Attributes:");

      foreach (var attr in attrs)
      {
        Console.WriteLine("    {0} = {1}", attr.Key, attr.Value);
      }
    }    

    Console.WriteLine();
  }

  request.NextToken = response.NextToken;

} while (!string.IsNullOrEmpty(response.NextToken));
```

For related API reference information, see `Amazon.SimpleNotificationService`, `Amazon.SimpleNotificationService.Model`, and `Amazon.SimpleNotificationService.Util` in the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/)\.\.

#### Resource APIs<a name="sns-apis-intro-resource-level"></a>

The SDK provides the AWS Resource APIs for \.NET for programming with Amazon SNS\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with Amazon SNS resources as compared to their low\-level API counterparts\. \(For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.\)

The following example shows how to use the AWS Resource APIs for \.NET to list accessible topics in Amazon SNS:

```
// using Amazon.SimpleNotificationService.Resources;

var sns = new SimpleNotificationService();
var topics = sns.GetTopics();

if (topics.Any())
{
  Console.WriteLine("Topics:");

  foreach (var topic in topics)
  {
    Console.WriteLine("  Topic ARN: {0}", topic.Arn);
  
    if (topic.Attributes.Count > 0)
    {
      Console.WriteLine("    Attributes:");

      foreach (var attr in topic.Attributes)
      {
        Console.WriteLine("{0} = {1}", attr.Key, attr.Value);
      }

    }
    
  }

}

var subs = sns.GetSubscriptions();

if (subs.Any())
{
  Console.WriteLine("Subscriptions:");
 
  foreach (var sub in subs)
  {
    Console.WriteLine("  Subscription ARN: {0}", sub.Arn);

    var attrs = sub.Attributes;

    if (attrs.Any())
    {
      Console.WriteLine("    Attributes:");

      foreach (var attr in attrs)
      {
        Console.WriteLine("{0} = {1}", attr.Key, attr.Value);
      }
    }

  }

}
```

For related API reference information, see [Amazon\.SNS\.Resources](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NSNSResourcesNET45.html)\.