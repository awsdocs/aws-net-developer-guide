.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _delete-sqs-message:

#########################################
Delete a Message from an Amazon SQS Queue
#########################################

 You can use the |sdk-net| to receive messages from an |SQS| queue.

**To delete a message from an Amazon SQS queue**

1. Create and initialize a :sdk-net-api:`DeleteMessageRequest <SQS/TSQSDeleteMessageRequest>` instance. 
   Specify the |SQS| queue to delete a message from and the receipt handle of the message to delete, 
   as follows:

   .. code-block:: csharp

      var deleteMessageRequest = new DeleteMessageRequest();
    
      deleteMessageRequest.QueueUrl = queueUrl;
      deleteMessageRequest.ReceiptHandle = recieptHandle;

2. Pass the request object as a parameter to the 
   :sdk-net-api:`DeleteMessage <SQS/MSQSSQSDeleteMessageDeleteMessageRequest>` method. The method returns 
   a :sdk-net-api:`DeleteMessageResponse <SQS/TSQSDeleteMessageResponse>` object, as follows:

   .. code-block:: csharp

      var response = sqsClient.DeleteMessage(deleteMessageRequest);

   Calling :code:`DeleteMessage` unconditionally removes the message from the queue, regardless of
   the visibility timeout setting. For more information about visibility timeouts, see 
   :sqs-dg:`Visibility Timeout <AboutVT>`.

For information about sending a message to a queue, see 
:ref:`Sending an Amazon SQS Message <send-sqs-message>`.

For information about receiving messages from a queue, see 
:ref:`Receiving a Message from an Amazon SQS Queue <receive-sqs-message>`.


