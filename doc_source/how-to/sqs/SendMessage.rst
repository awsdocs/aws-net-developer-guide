.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _send-sqs-message:

########################
Sending an |SQS| Message
########################

You can use the |sdk-net| to send a message to an |SQS| queue.

.. important:: Due to the distributed nature of the queue, |SQS| can't guarantee you will receive
   messages in the precise order they are sent. If you need to preserve the message order,
   place sequencing information in each message so you can reorder the messages upon receipt.

.. topic:: To end a message to an |SQS| queue

    #. Create and initialize a :sdk-net-api:`SendMessageRequest <SQS/TSQSSendMessageRequest>` instance.
       Specify the queue name and the message you want to send, as follows.

       .. code-block:: csharp

          sendMessageRequest.QueueUrl = myQueueURL; sendMessageRequest.MessageBody = "{YOUR_QUEUE_MESSAGE}";

       For more information about your queue URL, see :ref:`sqs-queue-url`.

       Each queue message must be composed of Unicode characters only, and can be up to 64 KB in size.
       For more information about queue messages, see :sqs-api:`SendMessage` in the |SQS-api|.

    #. After you create the request, pass it as a parameter to the
       :sdk-net-api:`SendMessage <SQS/MSQSSQSSendMessageSendMessageRequest>` method.
       The method returns a :sdk-net-api:`SendMessageResponse <SQS/TSQSSendMessageResponse>` object,
       as follows.

       .. code-block:: csharp

          var sendMessageResponse = sqsClient.SendMessage(sendMessageRequest);

       The sent message will stay in your queue until the visibility timeout is exceeded,
       or until it is deleted from the queue. For more information about visibility timeouts,
       go to :sqs-dg:`Visibility Timeout <AboutVT>`.

    For information about deleting messages from your queue, see :ref:`delete-sqs-message`.

    For information about receiving messages from your queue, see :ref:`receive-sqs-message`.
