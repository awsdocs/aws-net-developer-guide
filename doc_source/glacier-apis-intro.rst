.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _glacier-apis-intro:

#######################################
|GLlong| Programming with the |sdk-net|
#######################################

The |sdk-net| supports |GLlong|, which is a storage service optimized for infrequently used data, or
"cold data." The service provides durable and extremely low-cost storage with security features for
data archiving and backup. For more information, see `Amazon Glacier <http:///glacier/>`_.

The following information introduces you to the |GL| programming models in the the SDK.

.. _glacier-apis-intro-models:

Programming Models
==================

The the SDK provides three programming models for working with |GL|. These programming models are
known as the *low-level*, *high-level*, and *resource* models. The following information describes
these models, why you would want to use them, and how to use them.

.. _glacier-apis-intro-low-level:

Low-Level APIs
--------------

The the SDK provides low-level APIs for programming with |GL|. These low-level APIs map closely the
underlying REST API supported by |GL|. For each |GL| REST operation, the low-level APIs provide a
corresponding method, a request object for you to provide request information, and a response object
for you to process the |GL| response. The low-level APIs are the most complete implementation of the
underlying |GL| operations.

The following example shows how to use the low-level APIs to list accessible vaults in |GL|:

.. literalinclude:: how-to/glacier/glacier-list-vaults-low-level.txt
   :language: csharp

For additional examples, see:

* :gl-dg:`Using the AWS SDK for .NET <using-aws-sdk-for-dot-net.html>`

* :gl-dg:`Creating a Vault <creating-vaults-dotnet-sdk.html#create-vault-dotnet-lowlevel.html>`

* :gl-dg:`Retrieving Vault Metadata <retrieving-vault-info-sdk-dotnet.html>`

* :gl-dg:`Downloading a Vault Inventory <retrieving-vault-inventory-sdk-dotnet.html>`

* :gl-dg:`Configuring Vault Notifications <configuring-notifications-sdk-dotnet.html>`

* :gl-dg:`Deleting a Vault <deleting-vaults-sdk-dotnet.html#deleting-vault-sdk-dotnet-low-level>`

* :gl-dg:`Uploading an Archive in a Single Operation <uploading-an-archive-single-op-using-dotnet.html#uploading-an-archive-single-op-lowlevel-using-dotnet>`

* :gl-dg:`Uploading Large Archives in Parts <uploading-an-archive-mpu-using-dotnet.html#uploading-an-archive-in-parts-lowlevel-using-dotnet>`

* :gl-dg:`Downloading an Archive <downloading-an-archive-using-dotnet.html#downloading-an-archive-using-dotnet-lowlevel-api>`

* :gl-dg:`Deleting an Archive <deleting-an-archive-using-dot-net.html#delete-archive-using-dot-net-low-level>`

For related API reference information, see :code:`Amazon.Glacier` and :code:`Amazon.Glacier.Model`
in the |sdk-net-api|_.


.. _glacier-apis-intro-high-level:

High-Level APIs
---------------

The the SDK provides high-level APIs for programming with |GL|. To further simplify application
development, these high-level APIs offer a higher-level abstraction for some of the operations,
including uploading an archive and downloading an archive or vault inventory.

For examples, see:

* :gl-dg:`Using the AWS SDK for .NET <using-aws-sdk-for-dot-net.html>`

* :gl-dg:`Creating a Vault <creating-vaults-dotnet-sdk.html#create-vault-dotnet-highlevel>`

* :gl-dg:`Deleting a Vault <deleting-vaults-sdk-dotnet.html#deleting-vault-sdk-dotnet-high-level>`

* :gl-dg:`Upload an Archive to a Vault <getting-started-upload-archive-dotnet.html>`

* :gl-dg:`Uploading an Archive <uploading-an-archive-single-op-using-dotnet.html#uploading-an-archive-single-op-highlevel-using-dotnet>`

* :gl-dg:`Uploading Large Archives in Parts <uploading-an-archive-mpu-using-dotnet.html#uploading-an-archive-in-parts-highlevel-using-dotnet>`

* :gl-dg:`Download an Archive from a Vault <getting-started-download-archive-dotnet.html>`

* :gl-dg:`Downloading an Archive <downloading-an-archive-using-dotnet.html#downloading-an-archive-using-dotnet-highlevel-api>`

* :gl-dg:`Delete an Archive from a Vault <getting-started-delete-archive-dotnet.html>`

* :gl-dg:`Deleting an Archive <deleting-an-archive-using-dot-net.html#delete-archive-using-dot-net-high-level>`

For related API reference information, see :sdk-net-api-v2:`Amazon.Glacier.Transfer <NGlacierTransfer>` 
in the |sdk-net-api|.


.. _glacier-apis-intro-resource-level:

Resource APIs
-------------

The the SDK provides the AWS Resource APIs for .NET for programming with |GL|. These resource APIs
provide a resource-level programming model that enables you to write code to work more directly with
|GL| resources as compared to their low-level and high-level API counterparts. (For more information
about the AWS Resource APIs for .NET, including how to download and reference these resource APIs,
see :ref:`resource-level-apis-intro`.)

The following example shows how to use the AWS Resource APIs for .NET to list accessible vaults in
|GL|:

.. literalinclude:: how-to/glacier/glacier-list-vaults-resource-level.txt
   :language: csharp

For related API reference information, see :sdk-net-api-v2:`Amazon.Glacier.Resources <NGlacierResourcesNET45>`.
