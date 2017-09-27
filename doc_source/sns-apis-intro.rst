.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sns-apis-intro:

##################################################################
Sending and Receiving Notifications From the Cloud Using |SNSlong| 
##################################################################

.. meta::
   :description: .NET code examples for Amazon SNS
   :keywords: AWS SDK for .NET examples, SNS

The |sdk-net| supports |SNSlong| (|SNS|), which is a web service that enables applications, end
users, and devices to instantly send and receive notifications from the cloud. For more information, 
see |sns|_.

The following example shows how to use the low-level APIs to list accessible topics in |SNS|. This
example uses the default client constructor which constructs 
:sdk-net-api:`AmazonSimpleNotificationServiceClient <SNS/MSNSSNSctor>` with the credentials loaded from 
the application's default configuration, and if unsuccessful from the Instance Profile service on an 
EC2 instance.

.. code-block:: csharp

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
        



