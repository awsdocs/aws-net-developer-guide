--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Platform Differences in the AWS SDK for \.NET<a name="net-dg-platform-diffs-v2"></a>

The AWS SDK for \.NET provides four distinct assemblies for developers to target different platforms\. However, not all SDK functionality is available on each of these platforms\. This topic describes the differences in support for each platform\.

## AWS SDK for \.NET Framework 3\.5<a name="net-dg-platform-diff-netfx35"></a>

This version of the the SDK is the one most similar to version 1\. This version, compiled against \.NET Framework 3\.5, supports the same set of services as version 1\. It also uses the same [pattern for making asynchronous calls](sdk-net-async-api.md)\.

**Note**  
This version contains a number of changes that may break code that was designed for version 1\. For more information, see the [Migration Guide](migration-v2-net-sdk.md#net-dg-migration-guide-v2)\.

## AWS SDK for \.NET Framework 4\.5<a name="net-dg-platform-diff-netfx45"></a>

The version of the the SDK compiled against \.NET Framework 4\.5 supports the same set of services as version 1 of the SDK\. However, it uses a different pattern for asynchronous calls\. Instead of the Begin/End pattern it uses the task\-based pattern, which allows developers to use the new [async and await](http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx) keywords introduced in [C\# 5\.0](https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29#Versions)\.

## AWS SDK for Windows RT<a name="net-dg-platform-diff-winrt"></a>

The version of the the SDK compiled for [WinRT](http://windows.microsoft.com/en-us/windows/rt-welcome) supports only asynchronous method calls using `async` and `await`\.

This version does not provide all of the functionality for Amazon S3 and DynamoDB that was available in version 1 of the SDK\. The following Amazon S3 functionality is currently unavailable in the Windows RT version of SDK\.
+  [Transfer Utility](TS3TransferTransferUtilityNET45.html) 
+  [IO Namespace](NS3IONET45.html) 

The Windows RT version of the SDK does not support decryption of the Windows password using the [GetDecryptedPassword](MEC2GetPasswordDataResultGetDecryptedPasswordStringNET45.html) method\.

## AWS SDK for Windows Phone 8<a name="net-dg-platform-diff-winphone"></a>

The version of the the SDK compiled for Windows Phone 8 has a programming model similar to Windows RT\. As with the Windows RT version, it supports only asynchronous method calls using `async` and `await`\. Also, because Windows Phone 8 doesn’t natively support `System.Net.Http.HttpClient`, the SDK depends on Microsoft’s portable class implementation of `HttpClient`, which is hosted on nuget at the following URL:
+  [http://nuget\.org/packages/Microsoft\.Net\.Http/2\.1\.10](http://nuget.org/packages/Microsoft.Net.Http/2.1.10) 

This version of the AWS SDK for \.NET supports the same set of services supported in the [AWS Mobile SDK for Android](https://aws.amazon.com/mobile/sdk/) and the [AWS Mobile SDK for iOS](https://aws.amazon.com/mobile/sdk/):
+ Amazon EC2
+ Elastic Load Balancing
+ Auto Scaling
+ Amazon S3
+ Amazon SNS
+ Amazon SQS
+ Amazon SES
+ DynamoDB
+ Amazon SimpleDB
+ CloudWatch
+ AWS STS

This version does not provide all of the functionality for Amazon S3 and DynamoDB available in version 1 of the SDK\. The following Amazon S3 functionality is currently unavailable in the Windows Phone 8 version of SDK\.
+  [Transfer Utility](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TS3TransferTransferUtilityNET45.html) 
+  [IO Namespace](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NS3IONET45.html) 

Also, the Windows Phone 8 version of the SDK does not support decryption of the Windows password using the [GetDecryptedPassword](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2GetPasswordDataResultGetDecryptedPasswordStringNET45.html) method\.