.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _receive-sqs-message:

#######################################
Receiving a Message from an |SQS| Queue
#######################################

You can use the |sdk-net| to receive messages from an |SQS| queue.

.. topic:: To receive a message from an |SQS| queue

#. Create and initialize a :sdk-net-api:`ReceiveMessageRequest <SQS/TSQSReceiveMessageRequest>`
   instance. Specify the queue URL to receive a message from, as follows.

   .. code-block:: csharp

       var receiveMessageRequest = new ReceiveMessageRequest();

       receiveMessageRequest.QueueUrl = myQueueURL;

   For more information about your queue URL, see :ref:`Your Amazon SQS Queue URL <sqs-queue-url>`.

#. Pass the request object as a parameter to the
   :sdk-net-api:`ReceiveMessage<SQS/MSQSSQSReceiveMessageReceiveMessageRequest>` method, as
   follows.

   .. code-block:: csharp

       var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);

   The method returns a :sdk-net-api:`ReceiveMessageResponse <SQS/TSQSReceiveMessageResponse>`
   instance, containing the list of messages the queue contains.

#. The :code:`ReceiveMessageResponse.ReceiveMessageResult` property contains a
   :sdk-net-api:`ReceiveMessageResponse <SQS/TSQSReceiveMessageResponse>` object, which contains
   a list of the messages that were received. Iterate through this list and call the :code:`ProcessMessage`
   method to process each message.

   .. code-block:: csharp

       foreach (var message in result.Messages)
       {
         ProcessMessage(message);  // Go to a method to process messages.
       }

   The :code:`ProcessMessage` method can use the :code:`ReceiptHandle` property to obtain a
   receipt handle for the message. You can use this receipt handle to change the message visibility
   timeout or to delete the message from the queue. For more information about how to change the
   visibility timeout for a message, see
   :sdk-net-api:`ChangeMessageVisibility<SQS/MSQSSQSChangeMessageVisibilityChangeMessageVisibilityRequest>`.

For information about sending a message to your queue, see :ref:`send-sqs-message`.

For more information about deleting a message from the queue, see :ref:`delete-sqs-message`.


