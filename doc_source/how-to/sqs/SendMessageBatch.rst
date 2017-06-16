.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _send-message-batch:

##############################
Sending an |SQS| Message Batch
##############################

You can use the |sdk-net| to send batch messages to an |SQS| queue. The
:sdk-net-api:`SendMessageBatch <SQS/MSQSSQSSendMessageBatchSendMessageBatchRequest>` method
delivers up to 10 messages to the specified queue. This is a batch version of
:sdk-net-api:`SendMessage <SQS/MSQSSQSSendMessageSendMessageRequest>`.

For a FIFO queue, multiple messages within a single batch are enqueued in the order they are sent.

For more information about sending batch messages, see
`SendMessageBatch <http://docs.aws.amazon.com/AWSSimpleQueueService/latest/APIReference/API_SendMessageBatch.html>`_
in the |SQS-api|.


.. topic:: To send batch messages to an |SQS| queue

    #. Create an :sdk-net-api:`AmazonSQSClient <SQS/TSQSSQSClient>` instance and initialize a
       :sdk-net-api:`SendMessageBatchRequest <SQS/TSQSSendMessageBatchRequest>` object.
       Specify the queue name and the message you want to send, as follows.

       .. code-block:: csharp

                AmazonSQSClient client = new AmazonSQSClient();
                var sendMessageBatchRequest = new SendMessageBatchRequest
                {
                    Entries = new List<SendMessageBatchRequestEntry>
                    {
                        new SendMessageBatchRequestEntry("message1", "FirstMessageContent"),
                        new SendMessageBatchRequestEntry("message2", "SecondMessageContent"),
                        new SendMessageBatchRequestEntry("message3", "ThirdMessageContent")
                    },
                    QueueUrl = "SQS_QUEUE_URL"
                };

       For more information about your queue URL, see :ref:`sqs-queue-url`.

       Each queue message must be composed of Unicode characters only, and can be up to 64 KB in size.
       For more information about queue messages, see :sqs-api:`SendMessage` in the |SQS-api|.

    #. After you create the request, pass it as a parameter to the
       :sdk-net-api:`SendMessageBatch <SQS/MSQSSQSSendMessageBatchSendMessageBatchRequest>` method.
       The method returns a :sdk-net-api:`SendMessageBatchResponse <SQS/TSQSSendMessageBatchResponse>` object,
       which contains the unique ID of each message and the message content for each successfully sent message.
       It also returns the message ID, message content, and a sender's fault flag if the message failed to send.

       .. code-block:: csharp

                SendMessageBatchResponse response = client.SendMessageBatch(sendMessageBatchRequest);
                Console.WriteLine("Messages successfully sent:");
                foreach (var success in response.Successful)
                {
                    Console.WriteLine("    Message id : {0}", success.MessageId);
                    Console.WriteLine("    Message content MD5 : {0}", success.MD5OfMessageBody);
                }

                Console.WriteLine("Messages failed to send:");
                foreach (var failed in response.Failed)
                {
                    Console.WriteLine("    Message id : {0}", failed.Id);
                    Console.WriteLine("    Message content : {0}", failed.Message);
                    Console.WriteLine("    Sender's fault? : {0}", failed.SenderFault);
                }
