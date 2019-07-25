.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sqs-apis-intro:

########################################
|SQSlong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |SQSlong| (|SQS|), which is a messaging queue service that handles message or
workflows between other components in a system. For more information, see the 
`SQS Getting Started Guide <http://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSGettingStartedGuide/>`_.

The following information introduces you to the |SQS| programming models in the the SDK.



.. _sqs-apis-intro-models:

Programming Models
==================

The SDK provides two programming models for working with |SQS|. These programming models are
known as the *low-level* and *resource* models. The following information describes these models,
how to use them, and why you would want to use them.

.. _sqs-apis-intro-low-level:

Low-Level APIs
--------------

The the SDK provides low-level APIs for programming with |SQS|. These APIs typically consist of sets
of matching request-and-response objects that correspond to HTTP-based API calls focusing on their
corresponding service-level constructs.

The following example shows how to use the APIs to list accessible queues in |SQS|:

.. literalinclude:: how-to/sqs/sqs-list-queues-low-level.txt
   :language: csharp

For additional examples, see the following:

* :ref:`init-sqs-client`

* :ref:`create-sqs-queue`

* :ref:`send-sqs-message`

* :ref:`receive-sqs-message`

* :ref:`delete-sqs-message`

For related API reference information, see :code:`Amazon.SQS`, :code:`Amazon.SQS.Model`, and
:code:`Amazon.SQS.Util` in the |sdk-net-ref|_.


.. _sqs-apis-intro-resource-level:

Resource APIs
-------------

The the SDK provides the AWS Resource APIs for .NET for programming with |SQS|. These resource APIs
provide a resource-level programming model that enables you to write code to work more directly with
|SQS| resources as compared to their low-level API counterparts. (For more information about the AWS
Resource APIs for .NET, including how to download and reference these resource APIs, see
:ref:`resource-level-apis-intro`.)

The following example shows how to use the AWS Resource APIs for .NET to list accessible queues in
|SQS|

.. literalinclude:: how-to/sqs/sqs-list-queues-resource-level.txt
   :language: csharp

For related API reference information, see :sdk-net-api-v2:`Amazon.SQS.Resources <NSQSResourcesNET45>`.



.. toctree::
    :titlesonly:
    :maxdepth: 1

    how-to-sqs


