--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Sending Notifications From the Cloud Using Amazon Simple Notification Service<a name="sns-apis-intro"></a>

The AWS SDK for \.NET supports Amazon Simple Notification Service \(Amazon SNS\), which is a web service that enables applications, end users, and devices to instantly send notifications from the cloud\. For more information, see [Amazon SNS](https://aws.amazon.com/sns/)\.

## Listing Your Amazon SNS Topics<a name="sns-list-example"></a>

The following example shows how to list your Amazon SNS topics, the subscriptions to each topic, and the attributes for each topic\. This example uses the default [AmazonSimpleNotificationServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SNS/MSNSctor.html), which loads credentials from your default configuration\.

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

## Sending a Message to an Amazon SNS Topic<a name="sns-send-message-example"></a>

The following example shows how to send a message to an Amazon SNS topic\. The example takes one argument, the ARN of the Amazon SNS topic\.

```
using System;
using System.Linq;
using System.Threading.Tasks;

using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SnsSendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Topic ARNs must be in the correct format:
             *   arn:aws:sns:REGION:ACCOUNT_ID:NAME
             *
             *  where:
             *  REGION     is the region in which the topic is created, such as us-west-2
             *  ACCOUNT_ID is your (typically) 12-character account ID
             *  NAME       is the name of the topic
             */
            string topicArn = args[0];
            string message = "Hello at " + DateTime.Now.ToShortTimeString();

            var client = new AmazonSimpleNotificationServiceClient(region: Amazon.RegionEndpoint.USWest2);

            var request = new PublishRequest
            {
                Message = message,
                TopicArn = topicArn
            };

            try
            {
                var response = client.Publish(request);

                Console.WriteLine("Message sent to topic:");
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception publishing request:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```

See the [complete example](https://github.com/awsdocs/aws-doc-sdk-examples/tree/master/dotnet/example_code_legacy/SNS/SnsSendMessage.cs), including information on how to build and run the example from the command line, on GitHub\.

## Sending an SMS Message to a Phone Number<a name="sns-send-sms-example"></a>

The following example shows how to send an SMS message to a telephone number\. The example takes one argument, the telephone number, which must be in either of the two formats described in the comments\.

```
using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SnsPublish
{
    class Program
    {
        static void Main(string[] args)
        {
            // US phone numbers must be in the correct format:
            // +1 (nnn) nnn-nnnn OR +1nnnnnnnnnn
            string number = args[0];
            string message = "Hello at " + DateTime.Now.ToShortTimeString();

            var client = new AmazonSimpleNotificationServiceClient(region: Amazon.RegionEndpoint.USWest2);
            var request = new PublishRequest
            {
                Message = message,
                PhoneNumber = number
            };

            try
            {
                var response = client.Publish(request);

                Console.WriteLine("Message sent to " + number + ":");
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception publishing request:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```

See the [complete example](https://github.com/awsdocs/aws-doc-sdk-examples/tree/master/dotnet/example_code_legacy/SNS/SnsPublish.cs), including information on how to build and run the example from the command line, on GitHub\.