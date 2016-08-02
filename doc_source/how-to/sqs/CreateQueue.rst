.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _create-sqs-queue:

##########################
Create an Amazon SQS Queue
##########################

You can use the |sdk-net| to programmatically create an Amazon SQS queue. Creating an Amazon SQS
Queue is an administrative task. You can create a queue by using the 
:console:`AWS Management Console <sqs>` instead of creating a queue programmatically.

**To create an Amazon SQS queue**

1. Create and initialize a :sdk-net-api-v2:`CreateQueueRequest <TSQSCreateQueueRequestNET45>` instance. 
   Provide the name of your queue and specify a visibility timeout for your queue messages, as 
   follows:

   .. code-block:: csharp

       CreateQueueRequest createQueueRequest =
           new CreateQueueRequest();
       
       createQueueRequest.QueueName = "MySQSQueue";
       createQueueRequest.DefaultVisibilityTimeout = 10;

   Your queue name must only be composed of alphanumeric characters, hyphens, and underscores.

   Any message in the queue remains in the queue unless the specified visibility timeout is
   exceeded. The default visibility timeout for a queue is 30 seconds. For more information about
   visibility timeouts, go to :sqs-dg:`Visibility Timeout <AboutVT>`. For more information
   about different queue attributes you can set, go to :sqs-api:`SetQueueAttributes <SetQueueAttributes>`.

2. After you create the request, pass it as a parameter to the 
   :sdk-net-api-v2:`CreateQueue <MSQSSQSCreateQueueCreateQueueRequestNET45>` 
   method. The method returns a :sdk-net-api-v2:`CreateQueueResponse <TSQSCreateQueueResponseNET45>` 
   object, as follows:

   .. code-block:: csharp

       CreateQueueResponse createQueueResponse =
           amazonSQSClient.CreateQueue(createQueueRequest);

For information about how queues work in Amazon SQS, go to :sqs-dg:`How SQS Queues Work <SQSConcepts>`.

For information about your queue URL, see :ref:`sqs-queue-url`.


