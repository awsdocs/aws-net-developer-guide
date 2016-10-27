.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-migration-guide-v3:

#################################################
Migrating Your Code to Version 3 of the |sdk-net|
#################################################

This topic describes changes in version 3 of the |sdk-net| and how to migrate your code to
this version of the SDK.

.. _net-dg-migrate-v3-intro:

About the |sdk-net| Versions
============================

The |sdk-net|, originally released in November 2009, was designed for .NET Framework 2.0.
Since that release, .NET has improved with .NET Framework 4.0 and .NET Framework 4.5, and added new
target platforms: WinRT and Windows Phone.

|sdk-net| version 2 was updated to take advantage of the new features of the .NET platform and to
target WinRT and Windows Phone.

|sdk-net| version 3 has been updated to make the assemblies modular.


.. _net-dg-migrate-v3-arch:

Architecture Redesign for the SDK
=================================

The entire version 3 of the |sdk-net| is redesigned to be modular. Each service is now implemented in its own assembly,
instead of in one global assembly. You no longer have to add the entire |sdk-net| to your
application. You can now add assemblies only for the AWS services your application uses.


.. _net-dg-migrate-v3-breaking:

Breaking Changes
================

The following sections describe changes to version 3 of the |sdk-net|.

.. _awsclientfactory-removed:

AWSClientFactory Removed
------------------------

The :classname:`Amazon.AWSClientFactory` class was removed. Now, to create a service client,
use the constructor of the service client. For example, to create an :classname:`AmazonEC2Client`:

.. code-block:: csharp

    var ec2Client = new Amazon.EC2.AmazonEC2Client();


.. _assumeroleawscredentials-removed:

Amazon.Runtime.AssumeRoleAWSCredentials Removed
-----------------------------------------------

The :classname:`Amazon.Runtime.AssumeRoleAWSCredentials` class was removed because it was in a core
namespace but had a dependency on the |STSlong|, and because it has been obsolete in the SDK for some
time. Use the :classname:`Amazon.SecurityToken.AssumeRoleAWSCredentials` class
instead.


.. _setacl-removed:

SetACL Method Removed from S3Link
---------------------------------

The :classname:`S3Link` class is part of the :classname:`Amazon.DynamoDBv2` package and is used for
storing objects in |S3| that are references in a |DDB| item. This is a useful feature, but we didn't
want to create a compile dependency on the :classname:`Amazon.S3` package for |DDB|. Consequently,
we simplified the exposed :classname:`Amazon.S3` methods from the :classname:`S3Link` class,
replacing the :methodname:`SetACL` method with the :methodname:`MakeS3ObjectPublic` method. For
more control over the access control list (ACL) on the object, use the :classname:`Amazon.S3` package
directly.


.. _result-classes-removed:

Removal of Obsolete Result Classes
----------------------------------

For most services in the |sdk-net|, operations return a response object that contains metadata for
the operation, such as the request ID and a result object. Having a separate response and
result class was redundant and created extra typing for developers. In version 2 of the |sdk-net|, 
we put all the information in the result class into the response class. We also marked
the result classes obsolete to discourage their use. In version 3 of the |sdk-net|, we removed 
these obsolete result classes to help reduce the SDK's size.


.. _configs-changes:

AWS Config Section Changes
--------------------------

It is possible to do advanced configuration of the |sdk-net| through the :file:`App.config` or
:file:`Web.config` file. You do this through an :code:`<aws>` config section like the following, 
which references the SDK assembly name.

.. code-block:: none

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
      </configSections>
      <aws region="us-west-2">
        <logging logTo="Log4Net"/>  
      </aws>
    </configuration>

In version 3 of the |sdk-net|, the :classname:`AWSSDK` assembly no longer exists. We put the common 
code into the :classname:`AWSSDK.Core` assembly. As a result, you will need to change the references
to the :classname:`AWSSDK` assembly in your :file:`App.config` or :file:`Web.config` file to the
:classname:`AWSSDK.Core` assembly, as follows.

.. code-block:: none

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws region="us-west-2">
        <logging logTo="Log4Net"/>  
      </aws>
    </configuration>

You can also manipulate the config settings with the :classname:`Amazon.AWSConfigs` class. In
version 3 of the |sdk-net|, we moved the config settings for |DDB| from
the :classname:`Amazon.AWSConfigs` class to the :classname:`Amazon.AWSConfigsDynamoDB` class.





