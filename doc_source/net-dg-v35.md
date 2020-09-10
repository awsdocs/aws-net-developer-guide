--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Migrating to version 3\.5 of the AWS SDK for \.NET<a name="net-dg-v35"></a>

Version 3\.5 of the AWS SDK for \.NET further standardizes the \.NET experience by transitioning support for all non\-Framework variations of the SDK to [\.NET Standard 2\.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)\. Depending on your environment and code base, to take advantage of version 3\.5 features, you might need to perform certain migration work\.

This topic describes the changes in version 3\.5 and possible work that you might need to do to migrate your environment or code from version 3\.

## What's changed for version 3\.5<a name="net-dg-v35-changes"></a>

The following describes what has or hasn't changed in the AWS SDK for \.NET version 3\.5\.

### \.NET Framework and \.NET Core<a name="net-dg-v35-changes-dotnet"></a>

Support for \.NET Framework and \.NET Core has not changed\.

### Xamarin<a name="net-dg-v35-changes-xamarin"></a>

Xamarin projects \(new and existing\) must target \.NET Standard 2\.0\. See [\.NET Standard 2\.0 Support in Xamarin\.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/internals/net-standard) and [\.NET implementation support](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support)\.

### Unity<a name="net-dg-v35-changes-unity"></a>

Unity apps must target \.NET Standard 2\.0 or \.NET 4\.x profiles using Unity 2018\.1 or later\. For more information, see [\.NET profile support](https://docs.unity3d.com/2020.1/Documentation/Manual/dotnetProfileSupport.html)\. In addition, if you're using **IL2CPP** to build, you must disable code stripping by adding a *link\.xml* file, as described in [Referencing the AWS SDK for \.NET Standard 2\.0 from Unity, Xamarin, or UWP](http://aws.amazon.com/blogs/developer/referencing-the-aws-sdk-for-net-standard-2-0-from-unity-xamarin-or-uwp)\. After you port your code to one of the recommended code bases, your Unity app can access all of the services offered by the SDK\.

Because Unity supports \.NET Standard 2\.0, the **AWSSDK\.Core** package of the SDK version 3\.5 no longer has Unity\-specific code, including some higher\-level functionality\. To provide a better transition, all of the Unity code is available for reference in the [aws/aws\-sdk\-unity\-net](https://github.com/aws/aws-sdk-unity-net) GitHub repository\. If you find missing functionality that impacts your use of AWS with Unity, you can file a feature request at [https://github\.com/aws/dotnet/issues](https://github.com/aws/dotnet/issues)\.

Also see [Special considerations for Unity support](unity-special.md)\.

### Universal Windows Platform \(UWP\)<a name="net-dg-v35-changes-uwp"></a>

Target your UWP application to [version 16299 or later](https://docs.microsoft.com/en-us/windows/uwp/updates-and-versions/choose-a-uwp-version) \(Fall Creators update, version 1709, released October 2017\)\.

### Windows Phone and Silverlight<a name="net-dg-v35-changes-phone-silverlight"></a>

Version 3\.5 of the AWS SDK for \.NET does not support these platforms because Microsoft is no longer actively developing them\. For more information, see the following:
+ [Windows 10 Mobile end of support](https://support.microsoft.com/en-us/help/4485197/windows-10-mobile-end-of-support-faq)
+ [Silverlight end of support](https://support.microsoft.com/en-us/help/4511036/silverlight-end-of-support)

### Legacy portable class libraries \(profile\-based PCLs\)<a name="net-dg-v35-changes-pcl"></a>

Consider retargeting your library to \.NET Standard\. For more information, see [Comparison to Portable Class Libraries](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#comparison-to-portable-class-libraries) from Microsoft\.

### Amazon Cognito Sync Manager and Amazon Mobile Analytics Manager<a name="net-dg-v35-changes-cog-ma"></a>

High\-level abstractions that ease the use of Amazon Cognito Sync and Amazon Mobile Analytics are removed from version 3\.5 of the AWS SDK for \.NET\. AWS AppSync is the preferred replacement for Amazon Cognito Sync\. Amazon Pinpoint is the preferred replacement for Amazon Mobile Analytics\.

If your code is affected by the lack of higher\-level library code for AWS AppSync and Amazon Pinpoint, you can record your interest in one or both of the following GitHub issues: [https://github\.com/aws/dotnet/issues/20](https://github.com/aws/dotnet/issues/20) and [https://github\.com/aws/dotnet/issues/19](https://github.com/aws/dotnet/issues/19)\. You can also obtain the libraries for Amazon Cognito Sync Manager and Amazon Mobile Analytics Manager from the following GitHub repositories: [aws/amazon\-cognito\-sync\-manager\-net](https://github.com/aws/amazon-cognito-sync-manager-net) and [aws/aws\-mobile\-analytics\-manager\-net](https://github.com/aws/aws-mobile-analytics-manager-net)\.

## Migrating synchronous code<a name="net-dg-v35-migrate-code"></a>

Version 3\.5 of the AWS SDK for \.NET supports only asynchronous calls to AWS services\. You must change synchronous code you want to run using version 3\.5 so that it runs asynchronously\.

The following code snippets show how you might change synchronous code into asynchronous code\. The code in these snippets is used to display the number of Amazon S3 buckets\.

The original code calls [ListBuckets](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3ListBuckets.html)\.

```
private static ListBucketsResponse MyListBuckets()
{
  var s3Client = new AmazonS3Client();
  var response = s3Client.ListBuckets();
  return response;
}

// From the calling function
ListBucketsResponse response = MyListBuckets();
Console.WriteLine($"Number of buckets: {response.Buckets.Count}");
```

To use version 3\.5 of the SDK, call [ListBucketsAsync](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3ListBucketsAsyncCancellationToken.html) instead\.

```
private static async Task<ListBucketsResponse> MyListBuckets()
{
  var s3Client = new AmazonS3Client();
  var response = await s3Client.ListBucketsAsync();
  return response;
}


// From an **asynchronous** calling function
ListBucketsResponse response = await MyListBuckets();
Console.WriteLine($"Number of buckets: {response.Buckets.Count}");

// OR From a **synchronous** calling function
Task<ListBucketsResponse> response = MyListBuckets();
Console.WriteLine($"Number of buckets: {response.Result.Buckets.Count}");
```