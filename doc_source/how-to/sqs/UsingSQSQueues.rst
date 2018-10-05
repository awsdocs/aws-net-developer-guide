.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _using-sqs-queues:

##################
Using |SQS| Queues
##################

|SQS| offers *standard* as the default queue type. A standard queue enables you to have a nearly-unlimited
number of transactions per second. Standard queues support at-least-once message delivery. However,
occasionally more than one copy of a message might be delivered out of order. Standard queues provide
best-effort ordering, which ensures that messages are generally delivered in the same order as they're sent.

You can use standard message queues in many scenarios, as long as your application can process messages
that arrive more than once and out of order.

This code example demonstrates how to use queues by using these methods of the :code:`AmazonSQSClient`class:

* :sdk-net-api:`ListQueues <SQS/MSQSListQueuesListQueuesRequest>`: Gets a list of
  your message queues
* :sdk-net-api:`GetQueueUrl <SQS/MSQSGetQueueUrlGetQueueUrlRequest>`: Obtains the
  URL for a particular queue
* :sdk-net-api:`DeleteQueue <SQS/MSQSDeleteQueueDeleteQueueRequest>`: Deletes a queue

For more information about |SQS| messages, see
:sqs-dg:`How Amazon SQS Queues Work <sqs-how-it-works>`
in the |SQS-dg|.

List Your Queues
================

Create a :sdk-net-api:`ListQueuesRequest <SQS/TListQueuesRequest>` object containing the properties needed to list your queues, which by default
is an empty object. Call the :sdk-net-api:`ListQueues <SQS/MSQSListQueuesListQueuesRequest>` method
with the :code:`ListQueuesRequest` as a parameter to retrieve
the list of queues. The :sdk-net-api:`ListQueuesResponse <SQS/TListQueuesResponse>` returned by the call contains the URLs of all queues.

.. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();

            ListQueuesResponse response = client.ListQueues(new ListQueuesRequest());
            foreach (var queueUrl in response.QueueUrls)
            {
                Console.WriteLine(queueUrl);
            }


Get the URL for a Queue
=======================

Create a :sdk-net-api:`GetQueueUrlRequest <SQS/TGetQueueUrlRequest>` object containing the properties needed to identify your queue, which must
include the name of the queue whose URL you want. Call the :sdk-net-api:`GetQueueUrl <SQS/MSQSGetQueueUrlGetQueueUrlRequest>`
method using the :code:`GetQueueUrlRequest` object as a parameter.
The call returns a :sdk-net-api:`GetQueueUrlResponse <SQS/TGetQueueUrlResponse>` object containing the URL of the specified queue.

.. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();

            var request = new GetQueueUrlRequest
            {
                QueueName = "SQS_QUEUE_NAME"
            };

            GetQueueUrlResponse response = client.GetQueueUrl(request);
            Console.WriteLine("The SQS queue's URL is {1}", response.QueueUrl);


Delete a Queue
==============

Create a :sdk-net-api:`DeleteQueueRequest <SQS/TDeleteQueueRequest>` object containing the URL of the queue you want to delete. Call the
:sdk-net-api:`DeleteQueue <SQS/MSQSDeleteQueueDeleteQueueRequest>` method with the :code:`DeleteQueueRequest`
object as the parameter.

.. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();

            var request = new DeleteQueueRequest
            {
                QueueUrl = "SQS_QUEUE_URL"
            };

            client.DeleteQueue(request);

