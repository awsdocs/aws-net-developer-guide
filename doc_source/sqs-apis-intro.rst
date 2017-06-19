.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sqs-apis-intro:

##############
|SQS| Examples
##############

.. meta::
   :description: .NET code examples for Amazon SQS
   :keywords: AWS SDK for .NET examples, SQS


The |sdk-net|_ supports |SQS|, which is a message queuing service that handles messages or
workflows between components in a system. For more information, see |SQS|_.

The following examples demonstrate how to use the |sdk-net| to create and use |SQS| queues.

The sample code is written in C#, but you can use the |sdk-net| with any compatible
language. The |sdk-net| installs a set of C# project templates. So the simplest way to start this
project is to open Visual Studio, and then choose :guilabel:`File`, :guilabel:`New Project`,
:guilabel:`AWS Sample Projects`, :guilabel:`App Services`,
:guilabel:`AWS SQS Sample`.

**Prerequisite Tasks**

Before you begin, be sure that you have created an AWS account and set up your AWS credentials. For
more information, see :ref:`net-dg-setup`.

For related API reference information, see :sdk-net-api:`Amazon.SQS <SQS/NSQS>`,
:sdk-net-api:`Amazon.SQS.Model <SQS/NSQSModel>`, and
:sdk-net-api:`Amazon.SQS.Util <SQS/NSQSUtil>` in the |sdk-net-ref|.

**Examples**

.. toctree::
    :titlesonly:
    :maxdepth: 1

    how-to/sqs/InitSQSClient
    how-to/sqs/CreateQueue
    how-to/sqs/QueueURL
    how-to/sqs/SendMessage
    how-to/sqs/SendMessageBatch
    how-to/sqs/ReceiveMessage
    how-to/sqs/DeleteMessage
    how-to/sqs/EnableLongPolling
    how-to/sqs/UsingSQSQueues
    how-to/sqs/UsingSQSDeadLetterQueues


