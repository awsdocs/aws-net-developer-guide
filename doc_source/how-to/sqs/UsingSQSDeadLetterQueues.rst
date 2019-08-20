.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _using-dead-letter-queues:

##############################
Using |SQS| Dead Letter Queues
##############################

This example shows you how to use a queue to receive and hold messages from other queues that the
queues can't process.

A dead letter queue is one that other (source) queues can target for messages that can't be processed
successfully. You can set aside and isolate these messages in the dead letter queue to determine why
their processing did not succeed. You must individually configure each source queue that sends messages
to a dead letter queue. Multiple queues can target a single dead letter queue.

In this example, an :sdk-net-api:`AmazonSQSClient <SQS/TSQSClient>` object uses the
:sdk-net-api:`SetQueueAttributesRequest <SQS/MSQSSetQueueAttributesSetQueueAttributesRequest>`
method to configure a source queue to use a dead letter queue.

For more information about |SQS| dead letter queues, see
:sqs-dg:`Using Amazon SQS Dead Letter Queues <sqs-dead-letter-queues>`
in the |SQS-dg|.

Configure a Source Queue
========================

This code example assumes you have created a queue to act as a dead letter queue. See :ref:`create-sqs-queue`
for information about creating a queue. After creating the dead letter queue, you must configure the
other queues to route unprocessed messages to the dead letter queue. To do this, specify a redrive
policy that identifies the queue to use as a dead letter queue and the maximum number of receives by
individual messages before they are routed to the dead letter queue.

Create an :sdk-net-api:`AmazonSQSClient <SQS/TSQSClient>` object to set the queue attributes. Create
a :sdk-net-api:`SetQueueAttributesRequest <SQS/MSQSSetQueueAttributesSetQueueAttributesRequest>` object
containing the properties needed to update queue attributes, including the :code:`RedrivePolicy` property
that specifies both the Amazon Resource Name (ARN) of the dead letter queue, and the value of :code:`maxReceiveCount`. Also specify the URL source queue you want to configure. Call the 
:sdk-net-api:`SetQueueAttributes <SQS/MSQSSetQueueAttributesSetQueueAttributesRequest>` method.

.. code-block:: c#

            AmazonSQSClient client = new AmazonSQSClient();

            var setQueueAttributeRequest = new SetQueueAttributesRequest
            {
                Attributes = new Dictionary<string, string>
                {
                    {"RedrivePolicy",   @"{ ""deadLetterTargetArn"" : ""DEAD_LETTER_QUEUE_ARN"", ""maxReceiveCount"" : ""10""}" }
                },
                QueueUrl = "SOURCE_QUEUE_URL"
            };

            client.SetQueueAttributes(setQueueAttributeRequest)
