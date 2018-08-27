.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _create-sqs-queue:

#######################
Creating an |SQS| Queue
#######################

Creating an |SQS| queue is an administrative task that you can do by using the
:console:`SQS Management Console <sqs>`. However, you can also use the |sdk-net| to
programmatically create an |SQS| queue.

.. topic:: To create an |SQS| queue

    #. Create and initialize a :sdk-net-api:`CreateQueueRequest <SQS/TCreateQueueRequest>` instance.
       Provide the name of your queue and specify a visibility timeout for your queue messages, as follows.

       .. code-block:: csharp

          var createQueueRequest = new CreateQueueRequest();

          createQueueRequest.QueueName = "MySQSQueue";
          var attrs = new Dictionary<string, string>();
          attrs.Add(QueueAttributeName.VisibilityTimeout, "10");
          createQueueRequest.Attributes = attrs;

       Your queue name must be composed of only alphanumeric characters, hyphens, and underscores.

       Any message in the queue remains in the queue unless the specified visibility timeout is
       exceeded. The default visibility timeout for a queue is 30 seconds. For more information about
       visibility timeouts, see :sqs-dg:`Visibility Timeout <AboutVT>`. For more information about
       different queue attributes you can set, see :sqs-api:`SetQueueAttributes <SetQueueAttributes>`.

    #. After you create the request, pass it as a parameter to the
       :sdk-net-api:`CreateQueue <SQS/MSQSCreateQueueCreateQueueRequest>` method.
       The method returns a :sdk-net-api:`CreateQueueResponse<SQS/TCreateQueueResponse>`
       object, as follows.

       .. code-block:: csharp

          var createQueueResponse = sqsClient.CreateQueue(createQueueRequest);

    For information about how queues work in |SQS|, see :sqs-dg:`How SQS Queues Work <SQSConcepts>`.

    For information about your queue URL, see :ref:`sqs-queue-url`.
