--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Using Amazon Simple Storage Service Internet storage<a name="s3-apis-intro"></a>

The AWS SDK for \.NET supports [Amazon S3](https://aws.amazon.com/s3/), which is storage for the Internet\. It is designed to make web\-scale computing easier for developers\.

## APIs<a name="w4aac19c23b5"></a>

The AWS SDK for \.NET provides APIs for Amazon S3 clients\. The APIs enable you to work with Amazon S3 resources such as buckets and items\. To view the full set of APIs for Amazon S3, see the following:
+ [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/) \(and scroll to "Amazon\.S3"\)\.
+ [Amazon\.Extensions\.S3\.Encryption](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.html) documentation

The Amazon S3 APIs are provided by the following NuGet packages:
+ [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3)
+ [Amazon\.Extensions\.S3\.Encryption](https://www.nuget.org/packages/Amazon.Extensions.S3.Encryption)

## Prerequisites<a name="w4aac19c23b7"></a>

Before you begin, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md) and [SDK features](net-dg-sdk-features.md)\.

## Examples in this document<a name="s3-apis-examples"></a>

The following topics in this document show you how to use the AWS SDK for \.NET to work with Amazon S3\.
+ [Using KMS keys for S3 encryption](kms-keys-s3-encryption.md)

## Examples in other documents<a name="s3-apis-examples-other"></a>

The following links to the [Amazon S3 Developer Guide](https://docs.aws.amazon.com/AmazonS3/latest/dev/) provide additional examples of how to use the AWS SDK for \.NET to work with Amazon S3\.

**Note**  
Although these examples and additional programming considerations were created for Version 3 of the AWS SDK for \.NET using \.NET Framework, they are also viable for later versions of the AWS SDK for \.NET using \.NET Core\. Small adjustments in the code are sometimes necessary\.

**Amazon S3 programming examples**
+  [Managing ACLs](https://docs.aws.amazon.com/AmazonS3/latest/dev/acl-using-dot-net-sdk.html) 
+  [Creating a Bucket](https://docs.aws.amazon.com/AmazonS3/latest/dev/create-bucket-get-location-example.html#create-bucket-get-location-dotnet) 
+  [Upload an Object](https://docs.aws.amazon.com/AmazonS3/latest/dev/UploadObjSingleOpNET.html) 
+  [Multipart Upload with the High\-Level API \([Amazon\.S3\.Transfer\.TransferUtility](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TTransferUtility.html)\)](https://docs.aws.amazon.com/AmazonS3/latest/dev/usingHLmpuDotNet.html) 
+  [Multipart Upload with the Low\-Level API](https://docs.aws.amazon.com/AmazonS3/latest/dev/usingLLmpuDotNet.html) 
+  [Listing Objects](https://docs.aws.amazon.com/AmazonS3/latest/dev/list-obj-version-enabled-bucket.html#list-obj-version-enabled-bucket-sdk-examples) 
+  [Listing Keys](https://docs.aws.amazon.com/AmazonS3/latest/dev/ListingObjectKeysUsingNetSDK.html) 
+  [Get an Object](https://docs.aws.amazon.com/AmazonS3/latest/dev/RetrievingObjectUsingNetSDK.html) 
+  [Copy an Object](https://docs.aws.amazon.com/AmazonS3/latest/dev/CopyingObjectUsingNetSDK.html) 
+  [Copy an Object with the Multipart Upload API](https://docs.aws.amazon.com/AmazonS3/latest/dev/CopyingObjctsUsingLLNetMPUapi.html) 
+  [Deleting an Object](https://docs.aws.amazon.com/AmazonS3/latest/dev/DeletingOneObjectUsingNetSDK.html) 
+  [Deleting Multiple Objects](https://docs.aws.amazon.com/AmazonS3/latest/dev/DeletingMultipleObjectsUsingNetSDK.html) 
+  [Restore an Object](https://docs.aws.amazon.com/AmazonS3/latest/dev/restore-object-dotnet.html) 
+  [Configure a Bucket for Notifications](https://docs.aws.amazon.com/AmazonS3/latest/dev/ways-to-add-notification-config-to-bucket.html) 
+  [Manage an Object’s Lifecycle](https://docs.aws.amazon.com/AmazonS3/latest/dev/manage-lifecycle-using-dot-net.html) 
+  [Generate a Pre\-signed Object URL](https://docs.aws.amazon.com/AmazonS3/latest/dev/ShareObjectPreSignedURLDotNetSDK.html) 
+  [Managing Websites](https://docs.aws.amazon.com/AmazonS3/latest/dev/ConfigWebSiteDotNet.html) 
+  [Enabling Cross\-Origin Resource Sharing \(CORS\)](https://docs.aws.amazon.com/AmazonS3/latest/dev/ManageCorsUsingDotNet.html) 

**Additional programming considerations**
+  [Using the AWS SDK for \.NET for Amazon S3 Programming](https://docs.aws.amazon.com/AmazonS3/latest/dev/UsingTheMPDotNetAPI.html) 
+  [Making Requests Using IAM User Temporary Credentials](https://docs.aws.amazon.com/AmazonS3/latest/dev/AuthUsingTempSessionTokenDotNet.html) 
+  [Making Requests Using Federated User Temporary Credentials](https://docs.aws.amazon.com/AmazonS3/latest/dev/AuthUsingTempFederationTokenDotNet.html) 
+  [Specifying Server\-Side Encryption](https://docs.aws.amazon.com/AmazonS3/latest/dev/SSEUsingDotNetSDK.html) 
+  [Specifying Server\-Side Encryption with Customer\-Provided Encryption Keys](https://docs.aws.amazon.com/AmazonS3/latest/dev/sse-c-using-dot-net-sdk.html) 