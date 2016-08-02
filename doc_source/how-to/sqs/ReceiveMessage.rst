.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _receive-sqs-message:

##########################################
Receive a Message from an Amazon SQS Queue
##########################################

 You can use the Amazon SDK for .NET to receive messages from an Amazon SQS queue.

**To receive a message from an Amazon SQS queue**

1. Create and initialize a :sdk-net-api-v2:`ReceiveMessageRequest 
   <MSQSSQSReceiveMessageReceiveMessageRequestNET45>` instance. Specify the queue URL to
   receive a message from, as follows:

   .. code-block:: csharp
   
       ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
       
       receiveMessageRequest.QueueUrl = myQueueURL;
   
   For more information about your queue URL, see :ref:`Your Amazon SQS Queue URL <sqs-queue-url>`.

2. Pass the request object as a parameter to the :sdk-net-api-v2:`ReceiveMessage 
   <MSQSSQSReceiveMessageReceiveMessageRequestNET45>` method, as follows:

   .. code-block:: csharp

       ReceiveMessageResponse receiveMessageResponse =
         amazonSQSClient.ReceiveMessage(receiveMessageRequest);

   The method returns a :sdk-net-api-v2:`ReceiveMessageResponse <TSQSReceiveMessageResponseNET45>` instance,
   containing the list of messages the queue contains.

3. The response object contains a :sdk-net-api-v2:`ReceiveMessageResult <TSQSReceiveMessageResultNET45>` member.
   This member includes a :sdk-net-api-v2:`Messages <TSQSMessageNET45>` list. Iterate through this list to
   find a specific message, and use the :code:`Body` property to determine if the list contains a
   specified message, as follows:

   .. code-block:: csharp

       if (result.Message.Count != 0)
       {
         for (int i = 0; i < result.Message.Count; i++)
         {
           if (result.Message[i].Body == messageBody)
           {
             receiptHandle = result.Message[i].ReceiptHandle;
           }
         }
       }

   Once the message is found in the list, use the :code:`ReceiptHandle` property to obtain a
   receipt handle for the message. You can use this receipt handle to change message visibility
   timeout or to delete the message from the queue. For more information about how to change the
   visibility timeout for a message, go to :sdk-net-api-v2:`ChangeMessageVisibility
   <TSQSChangeMessageVisibilityRequestNET45>`.

For information about sending a message to your queue, see :ref:`Send an Amazon SQS Message
<send-sqs-message>`.

For more information about deleting a message from the queue, see :ref:`Delete a Message from an
Amazon SQS Queue <delete-sqs-message>`.


