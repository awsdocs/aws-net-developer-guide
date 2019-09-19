.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sns-apis-intro:

####################################################
Sending Notifications From the Cloud Using |SNSlong|
####################################################

.. meta::
   :description: .NET code examples for Amazon SNS
   :keywords: AWS SDK for .NET examples, SNS

The |sdk-net| supports |SNSlong| (|SNS|), which is a web service that enables applications, end
users, and devices to instantly send notifications from the cloud. For more information, 
see |sns|_.

.. _sns-list-example:

Listing Your |SNS| Topics
=========================

The following example shows how to list your |SNS| topics, the subscriptions to each topic, and the attributes for each topic.
This example uses the default 
:sdk-net-api:`AmazonSimpleNotificationServiceClient <SNS/MSNSctor>`,
which loads credentials from your default configuration.

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
        
.. _sns-send-message-example:

Sending a Message to an |SNS| Topic
===================================

The following example shows how to send a message to an |SNS| topic.
The example takes one argument, the ARN of the |SNS| topic.

.. literalinclude:: sns.dotnet.send-message.txt
   :language: csharp

See the
`complete example
<https://github.com/awsdocs/aws-doc-sdk-examples/blob/master/dotnet/example_code/sns/SnsSendMessage.cs>`_,
including information on how to build and run the example from the command line,
on GitHub.

.. _sns-send-sms-example:

Sending an SMS Message to a Phone Number
========================================

The following example shows how to send an SMS message to a telephone number.
The example takes one argument, the telephone number,
which must be in either of the two formats described in the comments.

.. literalinclude:: sns.dotnet.publish.txt 
   :language: csharp

See the
`complete example
<https://github.com/awsdocs/aws-doc-sdk-examples/blob/master/dotnet/example_code/sns/SnsPublish.cs>`_,
including information on how to build and run the example from the command line,
on GitHub.
