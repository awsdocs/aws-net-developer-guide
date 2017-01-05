.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

The following example shows how to use the low-level APIs to list accessible user accounts in |IAM|.
For each user account, its associated groups, policies, and access key IDs are also listed:

.. literalinclude:: how-to/iam/iam-list-users-low-level.txt
   :language: csharp

For more examples, see :ref:`net-dg-roles`.

For related API reference information, see :sdk-net-api:`Amazon.IdentityManagement <IAM/NIAM>` and
:sdk-net-api:`Amazon.IdentityManagement.Model <IAM/NIAMModel>`.

.. toctree::
   :titlesonly:
   :maxdepth: 1
   
   net-dg-hosm
   



