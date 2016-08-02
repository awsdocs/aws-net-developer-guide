.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-resource-api-examples:

###########################################################
|IAMlong| Code Examples with the AWS Resource APIs for .NET
###########################################################

The following code examples demonstrate how to program with |IAM| by using the AWS Resource APIs for
.NET.

The AWS Resource APIs for .NET provide a resource-level programming model that enables you to write
code to work more directly with resources that are managed by AWS services. For more information
about the AWS Resource APIs for .NET, including how to download and reference these resource APIs,
see :ref:`resource-level-apis-intro`.

The AWS Resource APIs for .NET are currently provided as a preview. This means that these resource
APIs may frequently change in response to customer feedback, and these changes may happen without
advance notice. Until these resource APIs exit the preview stage, please be cautious about writing
and distributing production-quality code that relies on them.




.. contents:: **Topics**
    :local:
    :depth: 1

.. _iam-resource-api-examples-get-user:

Get User Account Information
============================

The following example displays information about an existing user account, including its associated
groups, policies, and access key IDs:

.. literalinclude:: how-to/iam/iam-get-user-resource-level.txt
   :language: csharp

The following example displays a list of all accessible user accounts. For each user account, its
associated groups, policies, and access key IDs are also displayed:

.. literalinclude:: how-to/iam/iam-list-users-resource-level.txt
   :language: csharp

.. _iam-resource-api-examples-get-group:

Get Group Information
=====================

The following example displays information about an existing group, including its associated
policies and user accounts:

.. literalinclude:: how-to/iam/iam-get-group-resource-level.txt
   :language: csharp

The following example displays a list of all accessible groups. For each group, its associated
policies and user accounts are also displayed:

.. literalinclude:: how-to/iam/iam-list-groups-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-get-role:

Get Role Information
====================

The following example displays information about an existing role, including its associated policies
and instance profiles:

.. literalinclude:: how-to/iam/iam-get-role-resource-level.txt
   :language: csharp

The following example displays a list of all accessible roles. For each role, its associated
policies and instance profiles are also displayed:

.. literalinclude:: how-to/iam/iam-list-roles-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-create-user:

Create a User Account
=====================

The following example creates a new user account and then displays some information about it:

.. literalinclude:: how-to/iam/iam-create-user-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-create-group:

Create a Group
==============

The following example creates a new group and then confirms whether the group was successfully
created:

.. literalinclude:: how-to/iam/iam-create-group-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-create-role:

Create a Role
=============

The following example creates a new role and then confirms whether the group was successfully
created.

.. literalinclude:: how-to/iam/iam-create-role-resource-level.txt
   :language: csharp

The preceding example relies on the following example to create the new policy.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating a policy document. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-create-asssume-role-policy-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-add-user-to-group:

Add a User Account to a Group
=============================

The following example adds an existing user account to an existing group and then displays a list of
the group's associated user accounts:

.. literalinclude:: how-to/iam/iam-add-user-to-group-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-add-policy:

Add a Policy to a User Account, Group, or Role
==============================================

.. _iam-resource-api-examples-add-policy-user:

Add a Policy to a User Account
------------------------------

The following example creates a new policy, adds the new policy to an existing user account, and
then displays a list of the user account's associated policies:

.. literalinclude:: how-to/iam/iam-add-policy-to-user-resource-level.txt
   :language: csharp

The preceding example relies on the following example to create the new policy.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating a policy document. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-create-policy-for-user-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-add-policy-group:

Add a Policy to a Group
-----------------------

The following example creates a new policy, adds the new policy to an existing group, and then
displays a list of the group's associated policies:

.. literalinclude:: how-to/iam/iam-add-policy-to-group-resource-level.txt
   :language: csharp

The preceding example relies on the following example to create the new policy.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating a policy document. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-create-policy-for-group-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-add-policy-role:

Add a Policy to a Role
----------------------

The following example creates a new policy and then adds the new policy to an existing role.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support adding a policy to a role. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-add-policy-to-role-resource-level.txt
   :language: csharp

The preceding example relies on the following example to create the new policy.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating a policy document. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-create-policy-for-role-resource-level.txt
   :language: csharp



.. _iam-resource-api-examples-create-access-key:

Create an Access Key for a User Account
=======================================

The following example creates an access key for a user account and then displays the access key's ID
and secret access key:

.. literalinclude:: how-to/iam/iam-create-access-key-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-create-login-profile:

Create a Login Profile for a User Account
=========================================

The following example creates a login profile for a user account.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating a login profile for a user account. However, this example is presented for
completeness:

.. literalinclude:: how-to/iam/iam-create-login-profile-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-create-instance-profile:

Create an Instance Profile
==========================

The following example creates an instance profile.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support creating an instance profile. However, this example is presented for completeness:

.. literalinclude:: how-to/iam/iam-create-instance-profile-resource-level.txt
   :language: csharp


.. _iam-resource-api-examples-attach-instance-profile:

Attach an Instance Profile to a Role
====================================

The following example attaches an instance profile to a role.

The following example doesn't use the AWS Resource APIs for .NET, as the resource APIs currently
don't support attaching an instance profile to a role. However, this example is presented for
completeness:

.. literalinclude:: how-to/iam/iam-attach-instance-profile-to-role-resource-level.txt
   :language: csharp



