.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sns-apis-intro:

########################################
|SNSlong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |SNSlong| (|SNS|), which is a web service that enables applications,
end-users, and devices to instantly send and receive notifications from the cloud. For more
information, see |SNS|_.

The following information introduces you to the |SNS| programming models in the the SDK.

.. _sns-apis-intro-models:

Programming Models
==================

The the SDK provides two programming models for working with |SNS|. These programming models are
known as the *low-level* and *resource* models. The following information describes these models,
how to use them, and why you would want to use them.

.. _sns-apis-intro-low-level:

Low-Level APIs
--------------

The the SDK provides low-level APIs for programming with |SNS|. These low-level APIs typically
consist of sets of matching request-and-response objects that correspond to HTTP-based API calls
focusing on their corresponding service-level constructs.

The following example shows how to use the low-level APIs to list accessible topics in |SNS|:

.. literalinclude:: how-to/sns/sns-list-topics-low-level.txt
   :language: csharp

For related API reference information, see :code:`Amazon.SimpleNotificationService`,
:code:`Amazon.SimpleNotificationService.Model`, and :code:`Amazon.SimpleNotificationService.Util` in
the |sdk-net-api|_..


.. _sns-apis-intro-resource-level:

Resource APIs
-------------

The the SDK provides the AWS Resource APIs for .NET for programming with |SNS|. These resource APIs
provide a resource-level programming model that enables you to write code to work more directly with
|SNS| resources as compared to their low-level API counterparts. (For more information about the AWS
Resource APIs for .NET, including how to download and reference these resource APIs, see
:ref:`resource-level-apis-intro`.)

The following example shows how to use the AWS Resource APIs for .NET to list accessible topics in
|SNS|:

.. literalinclude:: how-to/sns/sns-list-topics-resource-level.txt

For related API reference information, see :sdk-net-api-v2:`Amazon.SNS.Resources <NSNSResourcesNET45>`.




