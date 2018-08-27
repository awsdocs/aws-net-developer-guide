.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _enable-long-polling:

##############################
Enabling Long Polling in |SQS|
##############################

Long polling reduces the number of empty responses by allowing |SQS| to wait a specified time
for a message to become available in the queue before sending a response. Also, long polling eliminates
false empty responses by querying all the servers instead of a sampling of servers. To enable long
polling, you must specify a non-zero wait time for received messages. You can do this by setting the
:code:`ReceiveMessageWaitTimeSeconds` parameter of a queue or by setting the :code:`WaitTimeSeconds` parameter
on a message when it's received. This .NET example shows you how to enable long polling in |SQS| for a newly
created or existing queue, or upon receipt of a message.

These examples use the following methods of the :sdk-net-api:`AmazonSQSClient <SQS/TSQSSQSClient>` class
to enable long polling:

* :sdk-net-api:`CreateQueue <SQS/MSQSCreateQueueCreateQueueRequest>`
* :sdk-net-api:`SetQueueAttributes <SQS/MSQSSetQueueAttributesSetQueueAttributesRequest>`
* :sdk-net-api:`ReceiveMessage <SQS/MSQSReceiveMessageReceiveMessageRequest>`

For more information about long polling, see :sqs-dg:`Amazon SQS Long Polling <sqs-long-polling>`
in the |SQS-dg|.

Enable Long Polling When Creating a Queue
=========================================

Create an :sdk-net-api:`AmazonSQSClient <SQS/TSQSClient>` service object. Create a
:sdk-net-api:`CreateQueueRequest <SQS/TCreateQueueRequest>` object containing the properties
needed to create a queue, including a non-zero value for the :code:`ReceiveMessageWaitTimeSeconds` property.

Call the :sdk-net-api:`CreateQueue <SQS/MSQSCreateQueueCreateQueueRequest>` method. Long polling
is then enabled for the queue.

    .. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();
            var request = new CreateQueueRequest
            {
                QueueName = "SQS_QUEUE_NAME",
                Attributes = new Dictionary<string, string>
                {
                    { "ReceiveMessageWaitTimeSeconds", "20"}
                }
            };
            var response = client.CreateQueue(request);
            Console.WriteLine("Created a queue with URL : {0}", response.QueueUrl);


Enable Long Polling on an Existing Queue
========================================

Create an :sdk-net-api:`AmazonSQSClient <SQS/TSQSClient>` service object.
Create a :sdk-net-api:`SetQueueAttributesRequest <SQS/TSetQueueAttributesRequest>`
object containing the properties needed to set the attributes of the queue, including a non-zero value
for the :code:`ReceiveMessageWaitTimeSeconds` property and the URL of the queue. Call the
:sdk-net-api:`SetQueueAttributes <SQS/MSQSSetQueueAttributesSetQueueAttributesRequest>` method.
Long polling is then enabled for the queue.

    .. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();

            var request = new SetQueueAttributesRequest
            {
                Attributes = new Dictionary<string, string>
                {
                    { "ReceiveMessageWaitTimeSeconds", "20"}
                },
                QueueUrl = "SQS_QUEUE_URL"
            };

            var response = client.SetQueueAttributes(request);

Receive a Message
=================

Create an :sdk-net-api:`AmazonSQSClient <SQS/TSQSClient>` service object. Create a
:sdk-net-api:`ReceiveMessageRequest <SQS/TReceiveMessageRequest>` object containing the properties
needed to receive a message, including a non-zero value for the :code:`WaitTimeSeconds` parameter and the
URL of the queue. Call the :sdk-net-api:`ReceiveMessage <SQS/MSQSReceiveMessageReceiveMessageRequest>`
method.

    .. code-block:: c#

        public void OnMessageReceipt()
        {
            AmazonSQSClient client = new AmazonSQSClient();

            var request = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = "SQS_QUEUE_URL",
                WaitTimeSeconds = 20
            };

            var response = client.ReceiveMessage(request);
        }

