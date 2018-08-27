.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sqs-queue-url:

#############################
Constructing |SQS| Queue URLs
#############################

You need a queue URL to send, receive, and delete queue messages. You can get your queue URL using the :sdk-net-api:`GetQueueUrl <SQS/MSQSGetQueueUrlGetQueueUrlRequest>` method. 

.. code-block:: csharp

    var client = new AmazonSQSClient();
    var request = new GetQueueUrlRequest
    {
      QueueName = "MyTestQueue",
      QueueOwnerAWSAccountId = "80398EXAMPLE"
    };
    var response = client.GetQueueUrl(request);
    Console.WriteLine("Queue URL: " + response.QueueUrl);

To find your AWS account number, go to :console:`Security Credentials <iam>`.
Your account number is located under :guilabel:`Account Number` at the top-right of the page.

For information about sending a message to a queue, see :ref:`send-sqs-message`.

For information about receiving messages from a queue, see :ref:`receive-sqs-message`.

For information about deleting messages from a queue, see :ref:`delete-sqs-message`.


