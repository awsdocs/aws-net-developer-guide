.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _glacier-apis-intro:

#################
|GLlong| Examples
#################

The |sdk-net| supports |GLlong|, which is a storage service optimized for infrequently used data, or
*cold data*. The service provides durable and extremely low-cost storage with security features for
data archiving and backup. For more information, see |GL-dg|_.

The following information introduces you to the |GL| programming models in the |sdk-net|.

.. _glacier-apis-intro-models:

Programming Models
==================

The |sdk-net| provides two programming models for working with |GL|. The following information
describes these models and why and how to use them.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _glacier-apis-intro-low-level:

Low-Level APIs
--------------

The |sdk-net| provides low-level APIs for programming with |GL|. These low-level APIs map closely to
the underlying REST API supported by |GL|. For each |GL| REST operation, the low-level APIs provide
a corresponding method, a request object for you to provide request information, and a response
object for you to process the |GL| response. The low-level APIs are the most complete implementation
of the underlying |GL| operations.

The following example shows how to use the low-level APIs to list accessible vaults in |GL|:

.. literalinclude:: how-to/glacier/glacier-list-vaults-low-level.txt
   :language: csharp

For more examples, see:

* :gl-dg:`Using the AWS SDK for .NET <using-aws-sdk-for-dot-net>`

* :gl-dg:`Creating a Vault <creating-vaults-dotnet-sdk.html#create-vault-dotnet-lowlevel>`

* :gl-dg:`Retrieving Vault Metadata <retrieving-vault-info-sdk-dotnet>`

* :gl-dg:`Downloading a Vault Inventory <retrieving-vault-inventory-sdk-dotnet>`

* :gl-dg:`Configuring Vault Notifications <configuring-notifications-sdk-dotnet>`

* :gl-dg:`Deleting a Vault <deleting-vaults-sdk-dotnet>`

* :gl-dg:`Uploading an Archive in a Single Operation <uploading-an-archive-single-op-using-dotnet>`

* :gl-dg:`Uploading Large Archives in Parts <uploading-an-archive-mpu-using-dotnet>`

* :gl-dg:`Downloading an Archive <downloading-an-archive-using-dotnet>`

* :gl-dg:`Deleting an Archive <deleting-an-archive-using-dot-net>`

For related API reference information, see :sdk-net-api:`Amazon.Glacier <Glacier/NGlacier>` 
and :sdk-net-api:`Amazon.Glacier <Glacier/NGlacierModel>`. 


.. _glacier-apis-intro-high-level:

High-Level APIs
---------------

The |sdk-net| provides high-level APIs for programming with |GL|. To further simplify application
development, these high-level APIs offer a higher-level abstraction for some of the operations,
including uploading an archive and downloading an archive or vault inventory.

For examples, see the following topics in the |GL-dg|_:

* :gl-dg:`Using the AWS SDK for .NET <using-aws-sdk-for-dot-net>`

* :gl-dg:`Creating a Vault <creating-vaults-dotnet-sdk>`

* :gl-dg:`Deleting a Vault <deleting-vaults-sdk-dotnet>`

* :gl-dg:`Upload an Archive to a Vault <getting-started-upload-archive-dotnet>`

* :gl-dg:`Uploading an Archive <uploading-an-archive-single-op-using-dotnet>`

* :gl-dg:`Uploading Large Archives in Parts <uploading-an-archive-mpu-using-dotnet>`

* :gl-dg:`Download an Archive from a Vault <getting-started-download-archive-dotnet>`

* :gl-dg:`Downloading an Archive <downloading-an-archive-using-dotnet>`

* :gl-dg:`Delete an Archive from a Vault <getting-started-delete-archive-dotnet>`

* :gl-dg:`Deleting an Archive <deleting-an-archive-using-dot-net>`

For related API reference information, see :sdk-net-api:`Amazon.Glacier.Transfer <Glacier/NGlacierTransfer>` 
in the |sdk-net-api|.
