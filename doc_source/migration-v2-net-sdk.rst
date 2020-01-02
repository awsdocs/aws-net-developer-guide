.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-migration-guide-v2:

#####################################################
Migrating Your Code to the Version 2 of the |sdk-net|
#####################################################

This guide describes changes in the version 2 of the SDK, and how you can migrate your code to this
version of the SDK.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _net-dg-migrate-v2-intro:

Introduction
============

The |sdk-net| was released in November 2009 and was originally designed for .NET Framework 2.0.
Since then, .NET has improved with .NET 4.0 and .NET 4.5. Since .NET 2.0, .NET has also added new
target platforms: WinRT and |WP|.

|sdk-net| version 2 has been updated to take advantage of the new features of the .NET platform and
to target WinRT and |WP|.


.. _net-dg-migrate-v2-new:

What's New
==========

* Support for :code:`Task`-based asynchronous API

* Support for Windows Store apps

* Support for |WP|

* Ability to configure service region via :file:`App.config` or :file:`Web.config`

* Collapsed :code:`Response` and :code:`Result` classes

* Updated names for classes and properties to follow .NET conventions


.. _net-dg-migrate-v2-diff:

What's Different
================

.. _net-dg-migrate-v2-arch:

Architecture
------------

The |sdk-net| uses a common runtime library to make AWS service requests. In version 1 of the SDK,
this "common" runtime was added *after the initial release*, and several of the older AWS services
did not use it. As a result, there was a higher degree of variability among services in the
functionality provided by the |sdk-net| version 1.

In version 2 of the SDK, all services now use the common runtime, so future changes to the core
runtime will propagate to all services, increasing their uniformity and easing demands on developers
who want to target multiple services.

However, separate runtimes are provided for .NET 3.5 and .NET 4.5:

* The version 2 runtime for *.NET 3.5* is similar to the existing version 1 runtime, which is based on
  the `System.Net.HttpWebRequest
  <http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest%28v=vs.90%29.aspx>`_ class
  and uses the :code:`Begin` and :code:`End` pattern for asynchronous methods.

* The version 2 runtime for *.NET 4.5* is based on the new `System.Net.Http.HttpClient
  <http://msdn.microsoft.com/en-us/library/system.net.http.httpclient%28v=vs.110%29.aspx>`_ class
  and uses :code:`Tasks` for asynchronous methods, which enables users to use the new
  :code:`async` and :code:`await` keywords in C# 5.0.

The WinRT and |WP| versions of the SDK reuse the runtime for .NET 4.5, with the exception that they
support *asynchronous methods* only. |WP8| doesn't natively support
:code:`System.Net.Http.HttpClient`, so the SDK depends on Microsoft's portable class implementation
of :code:`HttpClient`, which is hosted on *NuGet* at the following URL:

* http://nuget.org/packages/Microsoft.Net.Http/2.1.10


.. _net-dg-migrate-v2-rm-with:

Removal of the "With" Methods
-----------------------------

 The "With" methods have been removed from version 2 of the SDK for the following reasons:

* In .NET 3.0, *constructor initializers* were added, making the "With" methods redundant.

* The "With" methods added significant overhead to the API design and worked poorly in cases of
  inheritance.

For example, in version 1 of the SDK, you would use "With" methods to set up a
:code:`TransferUtilityUploadRequest`:

.. code-block:: csharp

    TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest()
      .WithBucketName("my-bucket")
      .WithKey("test")
      .WithFilePath("c:\test.txt")
      .WithServerSideEncryptionMethod(ServerSideEncryptionMethod.AES256);

In the current version of the SDK, use constructor initializers instead:

.. code-block:: csharp

    TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest() {
      BucketName = "my-bucket", Key = "test", FilePath = "c:\test.txt",
      ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
    };


.. _net-dg-migrate-v2-secure-string:

Removal of SecureString
-----------------------

The use of :code:`System.Security.SecureString` was removed in version 2 of the SDK because it is
not available on the WinRT and Windows Phone 8 platforms.


.. _net-dg-migrate-v2-breaking:

Breaking Changes
----------------

Many classes and properties were changed to either meet .NET naming conventions or more closely
follow service documentation. |S3long| (|S3|) and |EC2long| (|EC2|) were the most affected by this
because they are the oldest services in the SDK and were moved to the new common runtime. Below are
the most visible changes.

* All client interfaces have been renamed to follow the .NET convention of starting with the letter
  "I". For example, the :code:`AmazonEC2` class is now `IAmazonEC2 <TEC2IEC2NET45.html>`_.

* Properties for collections have been properly pluralized.

* :code:`AWSClientFactory.CreateAmazonSNSClient` has been renamed
  :sdk-net-api-v2:`CreateAmazonSimpleNotificationServiceClient <MAWSClientFactoryCreateSNSClientNET45>`.

* :code:`AWSClientFactory.CreateAmazonIdentityManagementClient` has been renamed
  :sdk-net-api-v2:`CreateAmazonIdentityManagementServiceClient
  <MAWSClientFactoryCreateIdentityManagementServiceClientNET45>`.

.. _net-dg-migrate-v2-ddb:

|DDBlong|
~~~~~~~~~

* The :code:`amazon.dynamodb` namespace has been removed; only the 
  :sdk-net-api-v2:`amazon.dynamodbv2 <NDynamoDBv2NET45>` namespace remains.

* Service-response collections that were set to null in version 1 are now set to an empty collection.
  For example, :sdk-net-api-v2:`QueryResult.LastEvaluatedKey <PDynamoDBv2QueryResultLastEvaluatedKeyNET45>`
  and :sdk-net-api-v2:`ScanResponse.LastEvaluatedKey <PDynamoDBv2ScanResultLastEvaluatedKeyNET45>` will be
  set to *empty* collections when there are no more items to query/scan. If your code depends on
  :code:`LastEvaluatedKey` to be :code:`null`, it now has to check the collection's :code:`Count`
  field to avoid a possible infinite loop.


.. _net-dg-migrate-v2-ec2:

|EC2|
~~~~~

* :code:`Amazon.EC2.Model.RunningInstance` has been renamed :sdk-net-api-v2:`Instance <TEC2InstanceNET45>`.

  Additionally, the :code:`GroupName` and :code:`GroupId` properties of :code:`RunningInstance`
  have been combined into the :sdk-net-api-v2:`SecurityGroups <PEC2InstanceSecurityGroupsNET45>` property,
  which takes a :sdk-net-api-v2:`GroupIdentifier <TEC2GroupIdentifierNET45>` object, in :code:`Instance`.

* :code:`Amazon.EC2.Model.IpPermissionSpecification` has been renamed 
  :sdk-net-api-v2:`IpPermission <TEC2IpPermissionNET45>`.

* :code:`Amazon.EC2.Model.Volume.Status` has been renamed :sdk-net-api-v2:`State <PEC2VolumeStateNET45>`.

* :sdk-net-api-v2:`AuthorizeSecurityGroupIngressRequest <TEC2AuthorizeSecurityGroupIngressRequestNET45>` 
  removed root properties for :code:`ToPort` and :code:`FromPort` in favor of always using 
  :sdk-net-api-v2:`IpPermissions <PEC2AuthorizeSecurityGroupIngressRequestIpPermissionsNET45>`.

  This was done because the root properties were silently ignored when set for an instance running
  in a VPC.

* The :sdk-net-api-v2:`AmazonEC2Exception <TEC2EC2ExceptionNET45>` class is now based on 
  :sdk-net-api-v2:`AmazonServiceException <TRuntimeServiceExceptionNET45>` instead of 
  :code:`System.Exception`.

  As a result, many of the exception properties have changed; the :code:`XML` property is no
  longer provided, for example.


.. _net-dg-migrate-v2-redshift:

|RS|
~~~~

* The :code:`ClusterVersion.Name` property has been renamed :sdk-net-api-v2:`ClusterVersion.Version
  <PRedshiftClusterVersionVersionNET45>`.


.. _net-dg-migrate-v2-s3:

|S3|
~~~~

* :code:`AmazonS3Config.CommunicationProtocol` was removed to be consistent with other services where
  :sdk-net-api-v2:`ServiceURL <PRuntimeClientConfigServiceURLNET45>` contains the protocol.

* The :code:`PutACLRequest.ACL` property has been renamed :sdk-net-api-v2:`AccessControlList
  <PS3PutACLRequestAccessControlListNET45>` to make it consistent with :sdk-net-api-v2:`GetACLResponse
  <TS3GetACLResponseNET45>`.

* :code:`GetNotificationConfigurationRequest`/:code:`Response` and
  :code:`SetNotificationConfigurationRequest`/:code:`Response` have been renamed
  :sdk-net-api-v2:`GetBucketNotificationRequest <TS3GetBucketNotificationRequestNET45>`/
  :sdk-net-api-v2:`Response <TS3GetBucketNotificationResponseNET45>` and 
  :sdk-net-api-v2:`PutBucketNotificationRequest <TS3PutBucketNotificationRequestNET45>`/
  :sdk-net-api-v2:`Response <TS3PutBucketNotificationResponseNET45>`, respectively.

* :code:`EnableBucketLoggingRequest`/:code:`Response` and
  :code:`DisableBucketLoggingRequest`/:code:`Response` were consolidated into
  :sdk-net-api-v2:`PutBucketLoggingRequest <TS3PutBucketLoggingRequestNET45>`/
  :sdk-net-api-v2:`Response <TS3PutBucketLoggingResponseNET45>`.

* The :code:`GenerateMD5` property has been removed from :sdk-net-api-v2:`PutObjectRequest
  <TS3PutObjectRequestNET45>` and :sdk-net-api-v2:`UploadPartRequest <TS3UploadPartRequestNET45>`
  because this is now automatically computed as the object is being written to |S3| and compared
  against the MD5 returned in the response from |S3|.

* The :code:`PutBucketTagging.TagSets` collection is now :sdk-net-api-v2:`PutBucketTagging.TagSet
  <PS3PutBucketTaggingRequestTagSetNET45>`, and now takes a list of :sdk-net-api-v2:`Tag
  <TS3TagNET45>` objects.

* The :sdk-net-api-v2:`AmazonS3Util <TS3UtilS3UtilNET45>` utility methods :sdk-net-api-v2:`DoesS3BucketExist
  <MS3UtilS3UtilDoesS3BucketExistIS3StringNET45>`, :sdk-net-api-v2:`SetObjectStorageClass
  <MS3UtilS3UtilSetObjectStorageClassIS3StringStringS3StorageClassNET45>`, 
  :sdk-net-api-v2:`SetServerSideEncryption  <MS3UtilS3UtilSetServerSideEncryptionIS3StringStringServerSideEncryptionMethodNET45>`, 
  :sdk-net-api-v2:`SetWebsiteRedirectLocation <MS3UtilS3UtilSetWebsiteRedirectLocationIS3StringStringStringNET45>`, and 
  :sdk-net-api-v2:`DeleteS3BucketWithObjects <MS3UtilS3UtilDeleteS3BucketWithObjectsIS3StringNET45>` were
  changed to take :sdk-net-api-v2:`IAmazonS3 <TS3IS3NET45>` as the first parameter to be consistent with
  other high-level APIs in the SDK.

* Only responses that return a :code:`Stream` like :sdk-net-api-v2:`GetObjectResponse
  <TS3GetObjectResponseNET45>` are :code:`IDisposable`. In version 1, all responses were
  :code:`IDisposable`.

* The :code:`BucketName` property has been removed from :sdk-net-api-v2:`Amazon.S3.Model.S3Object
  <TS3S3ObjectNET45>`.


.. _net-dg-migrate-v2-swf:

|SWFlong|
~~~~~~~~~

* The :code:`DomainInfos.Name` property has been renamed 
  :sdk-net-api-v2:`DomainInfos.Infos <PSimpleWorkflowDomainInfosInfosNET45>`.



.. _net-dg-migrate-v2-config-region:

Configuring the AWS Region
--------------------------

Regions can be set in the :file:`App.config` or :file:`Web.config` files (depending on your project
type). The recommended approach is to use the :code:`aws` element, although using the
:code:`appSettings` element is still supported.

For example, the following specification configures all clients that don't explicitly set the region
to point to us-west-2 through use of the :code:`aws` element.

.. code-block:: csharp

    <configuration> <configSections> <section name="aws" type="Amazon.AWSSection, AWSSDK"/> </configSections> <aws profileName="{profile_name}" region="us-west-2"/>
    </configuration>

Alternatively, you can use the :code:`appSettings` element.

.. code-block:: csharp

     <configuration> <appSettings> <add key="AWSProfileName" value="{profile_name}"/>
        <add key="AWSRegion" value="us-west-2"/>
      </appSettings>
    </configuration>


.. _net-dg-migrate-v2-response-result:

Response and Result Classes
---------------------------

To simplify your code, the :code:`Response` and :code:`Result` classes that are returned when
creating a service object have been collapsed. For example, the code to get an |SQS| queue URL
previously looked like this:

.. code-block:: csharp

    GetQueueUrlResponse response = SQSClient.GetQueueUrl(request);
    Console.WriteLine(response.CreateQueueResult.QueueUrl);

You can now get the queue URL simply by referring to the :code:`QueueUrl` member of the
:sdk-net-api-v2:`CreateQueueResponse <TSQSCreateQueueResponseNET45>` returned by the
:sdk-net-api-v2:`AmazonSQSClient.CreateQueue <MSQSSQSCreateQueueCreateQueueRequestNET45>` method:

.. code-block:: csharp

    Console.WriteLine(response.QueueUrl);

The :code:`CreateQueueResult` property still exists, but has been marked as *deprecated*, and may be
removed in a future version of the SDK. Use the :code:`QueueUrl` member instead.

Additionally, all of the service response values are based on a common response class,
:sdk-net-api-v2:`AmazonWebServiceResponse <TRuntimeWebServiceResponseNET45>`, instead of individual response
classes per service. For example, the :sdk-net-api-v2:`PutBucketResponse <TS3PutBucketResponseNET45>` class in
Amazon S3 is now based on this common class instead of :code:`S3Response` in version 1. As a result,
the methods and properties available for :code:`PutBucketResponse` have changed.

Refer to the return value type of the *Create** method for the service client that you're using to
see what values are returned. These are all listed in the |sdk-net-ref|_.




