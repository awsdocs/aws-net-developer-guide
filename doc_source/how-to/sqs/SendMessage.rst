.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _send-sqs-message:

##########################
Send an Amazon SQS Message
##########################

 You can use the Amazon SDK for .NET to send a message to an |SQS| queue.

.. important:: Due to the distributed nature of the queue, |SQS| cannot guarantee you will receive messages in
   the exact order they are sent. If you require that message order be preserved, place sequencing
   information in each message so you can reorder the messages upon receipt.

**To send a message to an Amazon SQS queue**

1. Create and initialize a :sdk-net-api-v2:`SendMessageRequest <TSQSSendMessageRequestNET45>` instance. Specify
   the queue name and the message you want to send, as follows:

   .. code-block:: csharp

       sendMessageRequest.QueueUrl = myQueueURL; sendMessageRequest.MessageBody = "{YOUR_QUEUE_MESSAGE}";

   For more information about your queue URL, see :ref:`sqs-queue-url`.

   Each queue message must be composed of only Unicode characters, and can be up to 64 kB in size.
   For more information about queue messages, go to :sqs-api:`SendMessage <SendMessage>` in the Amazon SQS
   service API reference.

2. After you create the request, pass it as a parameter to the :sdk-net-api-v2:`SendMessage
   <TSQSSendMessageRequestNET45>` method. The method returns a :sdk-net-api-v2:`SendMessageResponse
   <TSQSSendMessageResponseNET45>` object, as follows:

   .. code-block:: csharp

       SendMessageResponse sendMessageResponse = 
           amazonSQSClient.SendMessage(sendMessageRequest);

   The sent message will stay in your queue until the visibility timeout is exceeded, or until it
   is deleted from the queue. For more information about visibility timeouts, go to :sqs-dg:`Visibility 
   Timeout <AboutVT>`.

For information on deleting messages from your queue, see :ref:`delete-sqs-message`.

For information on receiving messages from your queue, see :ref:`receive-sqs-message`.


