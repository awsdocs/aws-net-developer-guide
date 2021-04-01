--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Migrating Your Code to the Version 2 of the AWS SDK for \.NET<a name="migration-v2-net-sdk"></a>

## Version 2 content \(see announcement above\)<a name="w3aac11c13b3b1"></a>

This guide describes changes in the version 2 of the SDK, and how you can migrate your code to this version of the SDK\.

**Topics**
+ [Introduction](#net-dg-migrate-v2-intro)
+ [What’s New](#net-dg-migrate-v2-new)
+ [What’s Different](#net-dg-migrate-v2-diff)

### Introduction<a name="net-dg-migrate-v2-intro"></a>

The AWS SDK for \.NET was released in November 2009 and was originally designed for \.NET Framework 2\.0\. Since then, \.NET has improved with \.NET 4\.0 and \.NET 4\.5\. Since \.NET 2\.0, \.NET has also added new target platforms: WinRT and Windows Phone 8\.

AWS SDK for \.NET version 2 has been updated to take advantage of the new features of the \.NET platform and to target WinRT and Windows Phone 8\.

### What’s New<a name="net-dg-migrate-v2-new"></a>
+ Support for `Task`\-based asynchronous API
+ Support for Windows Store apps
+ Support for Windows Phone 8
+ Ability to configure service region via `App.config` or `Web.config` 
+ Collapsed `Response` and `Result` classes
+ Updated names for classes and properties to follow \.NET conventions

### What’s Different<a name="net-dg-migrate-v2-diff"></a>

#### Architecture<a name="net-dg-migrate-v2-arch"></a>

The AWS SDK for \.NET uses a common runtime library to make AWS service requests\. In version 1 of the SDK, this “common” runtime was added *after the initial release*, and several of the older AWS services did not use it\. As a result, there was a higher degree of variability among services in the functionality provided by the AWS SDK for \.NET version 1\.

In version 2 of the SDK, all services now use the common runtime, so future changes to the core runtime will propagate to all services, increasing their uniformity and easing demands on developers who want to target multiple services\.

However, separate runtimes are provided for \.NET 3\.5 and \.NET 4\.5:
+ The version 2 runtime for *\.NET 3\.5* is similar to the existing version 1 runtime, which is based on the [System\.Net\.HttpWebRequest](http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest%28v=vs.90%29.aspx) class and uses the `Begin` and `End` pattern for asynchronous methods\.
+ The version 2 runtime for *\.NET 4\.5* is based on the new [System\.Net\.Http\.HttpClient](http://msdn.microsoft.com/en-us/library/system.net.http.httpclient%28v=vs.110%29.aspx) class and uses `Tasks` for asynchronous methods, which enables users to use the new `async` and `await` keywords in C\# 5\.0\.

The WinRT and Windows Phone 8 versions of the SDK reuse the runtime for \.NET 4\.5, with the exception that they support *asynchronous methods* only\. Windows Phone 8 doesn’t natively support `System.Net.Http.HttpClient`, so the SDK depends on Microsoft’s portable class implementation of `HttpClient`, which is hosted on *NuGet* at the following URL:
+  [http://nuget\.org/packages/Microsoft\.Net\.Http/2\.1\.10](http://nuget.org/packages/Microsoft.Net.Http/2.1.10) 

#### Removal of the “With” Methods<a name="net-dg-migrate-v2-rm-with"></a>

The “With” methods have been removed from version 2 of the SDK for the following reasons:
+ In \.NET 3\.0, *constructor initializers* were added, making the “With” methods redundant\.
+ The “With” methods added significant overhead to the API design and worked poorly in cases of inheritance\.

For example, in version 1 of the SDK, you would use “With” methods to set up a `TransferUtilityUploadRequest`:

```
TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest()
  .WithBucketName("my-bucket")
  .WithKey("test")
  .WithFilePath("c:\test.txt")
  .WithServerSideEncryptionMethod(ServerSideEncryptionMethod.AES256);
```

In the current version of the SDK, use constructor initializers instead:

```
TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest() {
  BucketName = "my-bucket", Key = "test", FilePath = "c:\test.txt",
  ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
};
```

#### Removal of SecureString<a name="net-dg-migrate-v2-secure-string"></a>

The use of `System.Security.SecureString` was removed in version 2 of the SDK because it is not available on the WinRT and Windows Phone 8 platforms\.

#### Breaking Changes<a name="net-dg-migrate-v2-breaking"></a>

Many classes and properties were changed to either meet \.NET naming conventions or more closely follow service documentation\. Amazon Simple Storage Service \(Amazon S3\) and Amazon Elastic Compute Cloud \(Amazon EC2\) were the most affected by this because they are the oldest services in the SDK and were moved to the new common runtime\. Below are the most visible changes\.
+ All client interfaces have been renamed to follow the \.NET convention of starting with the letter “I”\. For example, the `AmazonEC2` class is now [IAmazonEC2](TEC2IEC2NET45.html)\.
+ Properties for collections have been properly pluralized\.
+  `AWSClientFactory.CreateAmazonSNSClient` has been renamed [CreateAmazonSimpleNotificationServiceClient](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MAWSClientFactoryCreateSNSClientNET45.html)\.
+  `AWSClientFactory.CreateAmazonIdentityManagementClient` has been renamed [CreateAmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MAWSClientFactoryCreateIdentityManagementServiceClientNET45.html)\.

##### Amazon DynamoDB<a name="net-dg-migrate-v2-ddb"></a>
+ The `amazon.dynamodb` namespace has been removed; only the [amazon\.dynamodbv2](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NDynamoDBv2NET45.html) namespace remains\.
+ Service\-response collections that were set to null in version 1 are now set to an empty collection\. For example, [QueryResult\.LastEvaluatedKey](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PDynamoDBv2QueryResultLastEvaluatedKeyNET45.html) and [ScanResponse\.LastEvaluatedKey](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PDynamoDBv2ScanResultLastEvaluatedKeyNET45.html) will be set to *empty* collections when there are no more items to query/scan\. If your code depends on `LastEvaluatedKey` to be `null`, it now has to check the collection’s `Count` field to avoid a possible infinite loop\.

##### Amazon EC2<a name="net-dg-migrate-v2-ec2"></a>
+  `Amazon.EC2.Model.RunningInstance` has been renamed [Instance](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2InstanceNET45.html)\.

  Additionally, the `GroupName` and `GroupId` properties of `RunningInstance` have been combined into the [SecurityGroups](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2InstanceSecurityGroupsNET45.html) property, which takes a [GroupIdentifier](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2GroupIdentifierNET45.html) object, in `Instance`\.
+  `Amazon.EC2.Model.IpPermissionSpecification` has been renamed [IpPermission](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2IpPermissionNET45.html)\.
+  `Amazon.EC2.Model.Volume.Status` has been renamed [State](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2VolumeStateNET45.html)\.
+  [AuthorizeSecurityGroupIngressRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2AuthorizeSecurityGroupIngressRequestNET45.html) removed root properties for `ToPort` and `FromPort` in favor of always using [IpPermissions](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2AuthorizeSecurityGroupIngressRequestIpPermissionsNET45.html)\.

  This was done because the root properties were silently ignored when set for an instance running in a VPC\.
+ The [AmazonEC2Exception](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2EC2ExceptionNET45.html) class is now based on [AmazonServiceException](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRuntimeServiceExceptionNET45.html) instead of `System.Exception`\.

  As a result, many of the exception properties have changed; the `XML` property is no longer provided, for example\.

##### Amazon Redshift<a name="net-dg-migrate-v2-redshift"></a>
+ The `ClusterVersion.Name` property has been renamed [ClusterVersion\.Version](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PRedshiftClusterVersionVersionNET45.html)\.

##### Amazon S3<a name="net-dg-migrate-v2-s3"></a>
+  `AmazonS3Config.CommunicationProtocol` was removed to be consistent with other services where [ServiceURL](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PRuntimeClientConfigServiceURLNET45.html) contains the protocol\.
+ The `PutACLRequest.ACL` property has been renamed [AccessControlList](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PS3PutACLRequestAccessControlListNET45.html) to make it consistent with [GetACLResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3GetACLResponseNET45.html)\.
+  `GetNotificationConfigurationRequest`/`Response` and `SetNotificationConfigurationRequest`/`Response` have been renamed [GetBucketNotificationRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3GetBucketNotificationRequestNET45.html)/ [Response](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3GetBucketNotificationResponseNET45.html) and [PutBucketNotificationRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutBucketNotificationRequestNET45.html)/ [Response](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutBucketNotificationResponseNET45.html), respectively\.
+  `EnableBucketLoggingRequest`/`Response` and `DisableBucketLoggingRequest`/`Response` were consolidated into [PutBucketLoggingRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutBucketLoggingRequestNET45.html)/ [Response](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutBucketLoggingResponseNET45.html)\.
+ The `GenerateMD5` property has been removed from [PutObjectRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutObjectRequestNET45.html) and [UploadPartRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3UploadPartRequestNET45.html) because this is now automatically computed as the object is being written to Amazon S3 and compared against the MD5 returned in the response from Amazon S3\.
+ The `PutBucketTagging.TagSets` collection is now [PutBucketTagging\.TagSet](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PS3PutBucketTaggingRequestTagSetNET45.html), and now takes a list of [Tag](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3TagNET45.html) objects\.
+ The [AmazonS3Util](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3UtilS3UtilNET45.html) utility methods [DoesS3BucketExist](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MS3UtilS3UtilDoesS3BucketExistIS3StringNET45.html), [SetObjectStorageClass](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MS3UtilS3UtilSetObjectStorageClassIS3StringStringS3StorageClassNET45.html), [SetServerSideEncryption](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MS3UtilS3UtilSetServerSideEncryptionIS3StringStringServerSideEncryptionMethodNET45.html), [SetWebsiteRedirectLocation](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MS3UtilS3UtilSetWebsiteRedirectLocationIS3StringStringStringNET45.html), and [DeleteS3BucketWithObjects](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MS3UtilS3UtilDeleteS3BucketWithObjectsIS3StringNET45.html) were changed to take [IAmazonS3](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3IS3NET45.html) as the first parameter to be consistent with other high\-level APIs in the SDK\.
+ Only responses that return a `Stream` like [GetObjectResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3GetObjectResponseNET45.html) are `IDisposable`\. In version 1, all responses were `IDisposable`\.
+ The `BucketName` property has been removed from [Amazon\.S3\.Model\.S3Object](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3S3ObjectNET45.html)\.

##### Amazon Simple Workflow Service<a name="net-dg-migrate-v2-swf"></a>
+ The `DomainInfos.Name` property has been renamed [DomainInfos\.Infos](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PSimpleWorkflowDomainInfosInfosNET45.html)\.

#### Configuring the AWS Region<a name="net-dg-migrate-v2-config-region"></a>

Regions can be set in the `App.config` or `Web.config` files \(depending on your project type\)\. The recommended approach is to use the `aws` element, although using the `appSettings` element is still supported\.

For example, the following specification configures all clients that don’t explicitly set the region to point to us\-west\-2 through use of the `aws` element\.

```
<configuration> <configSections> <section name="aws" type="Amazon.AWSSection, AWSSDK"/> </configSections> <aws profileName="{profile_name}" region="us-west-2"/>
</configuration>
```

Alternatively, you can use the `appSettings` element\.

```
 <configuration> <appSettings> <add key="AWSProfileName" value="{profile_name}"/>
    <add key="AWSRegion" value="us-west-2"/>
  </appSettings>
</configuration>
```

#### Response and Result Classes<a name="net-dg-migrate-v2-response-result"></a>

To simplify your code, the `Response` and `Result` classes that are returned when creating a service object have been collapsed\. For example, the code to get an Amazon SQS queue URL previously looked like this:

```
GetQueueUrlResponse response = SQSClient.GetQueueUrl(request);
Console.WriteLine(response.CreateQueueResult.QueueUrl);
```

You can now get the queue URL simply by referring to the `QueueUrl` member of the [CreateQueueResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSCreateQueueResponseNET45.html) returned by the [AmazonSQSClient\.CreateQueue](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MSQSSQSCreateQueueCreateQueueRequestNET45.html) method:

```
Console.WriteLine(response.QueueUrl);
```

The `CreateQueueResult` property still exists, but has been marked as *deprecated*, and may be removed in a future version of the SDK\. Use the `QueueUrl` member instead\.

Additionally, all of the service response values are based on a common response class, [AmazonWebServiceResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRuntimeWebServiceResponseNET45.html), instead of individual response classes per service\. For example, the [PutBucketResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3PutBucketResponseNET45.html) class in Amazon S3 is now based on this common class instead of `S3Response` in version 1\. As a result, the methods and properties available for `PutBucketResponse` have changed\.

Refer to the return value type of the *Create\** method for the service client that you’re using to see what values are returned\. These are all listed in the [AWS SDK for \.NET Reference](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/)\.