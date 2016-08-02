.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-intro:

########################################
|IAMlong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |IAMlong| (|IAM|), which is a web service that enables |AWSlong| (AWS)
customers to manage users and user permissions in AWS.

The following information introduces you to the |IAM| programming models in the the SDK. There are
also links to additional |IAM| programming resources within the the SDK.

.. toctree::
    :titlesonly:
    :maxdepth: 1

    iam-resource-api-examples
    net-dg-hosm

.. _iam-apis-intro-models:

Programming Models
==================

The the SDK provides two programming models for working with |IAM|. These programming models are
known as the *low-level* model and the *resource* model. The following information describes these
models and how to use them.

.. _iam-apis-intro-low-level:

Low-Level APIs
--------------

The the SDK provides low-level APIs for programming with |IAM|. These low-level APIs typically
consist of sets of matching request-and-response objects that correspond to HTTP-based API calls
focusing on their corresponding service-level constructs.

The following example shows how to use the low-level APIs to list accessible user accounts in |IAM|.
For each user account, its associated groups, policies, and access key IDs are also listed:

.. literalinclude:: how-to/iam/iam-list-users-low-level.txt
   :language: csharp

For additional examples, see :ref:`net-dg-roles`.

For related API reference information, see :sdk-net-api-v2:`Amazon.IdentityManagement <NIAMNET45>` and
:sdk-net-api-v2:`Amazon.IdentityManagement.Model <NIAMNET45>`.


.. _iam-apis-intro-resource-level:

Resource APIs
-------------

The the SDK provides the AWS Resource APIs for .NET for programming with |IAM|. These resource APIs
provide a resource-level programming model that enables you to write code to work more directly with
|IAM| resources as compared to their low-level API counterparts. (For more information about the AWS
Resource APIs for .NET, including how to download and reference these resource APIs, see
:ref:`resource-level-apis-intro`.)

The AWS Resource APIs for .NET are currently provided as a preview. This means that these resource
APIs may frequently change in response to customer feedback, and these changes may happen without
advance notice. Until these resource APIs exit the preview stage, please be cautious about writing
and distributing production-quality code that relies on them.

The following example shows how to use the AWS Resource APIs for .NET to list accessible user
accounts in |IAM|. For each user account, its associated groups, policies, and access key IDs are
also listed:

.. literalinclude:: how-to/iam/iam-list-users-resource-level.txt
   :language: csharp

For additional examples, see :ref:`iam-resource-api-examples`.

For related API reference information, see :sdk-net-api-v2:`Amazon.IdentityManagement
<NIAMNET45>`.
