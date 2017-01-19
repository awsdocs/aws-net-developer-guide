.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

The |sdk-net| supports |SQSlong| (|SQS|), which is a message queuing service that handles message or
workflows between other components in a system. For more information, see |SQS|_

The following example shows how to use the APIs to list accessible queues in |SQS|:

.. literalinclude:: how-to/sqs/sqs-list-queues-low-level.txt
   :language: csharp

.. toctree::
    :titlesonly:
    :caption: For more examples, see the following:
    :maxdepth: 1

    how-to-sqs

For related API reference information, see :sdk-net-api:`Amazon.SQS <SQS/NSQS>`, 
:sdk-net-api:`Amazon.SQS.Model <SQS/NSQS>`, and 
:sdk-net-api:`Amazon.SQS.Util <SQS/NSQSUtil>` in the |sdk-net-ref|.
