--------

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Migrating from \.NET Standard 1\.3<a name="migration-from-net-standard-1-3"></a>

On June 27 2019 Microsoft [ended support](https://devblogs.microsoft.com/dotnet/net-core-1-0-and-1-1-will-reach-end-of-life-on-june-27-2019/) for \.NET Core 1\.0 and \.NET Core 1\.1 versions\. Following this announcement, AWS ended support for \.NET Standard 1\.3 on the AWS SDK for \.NET on December 31, 2020\.

AWS continued to provide service updates and security fixes on the AWS SDK for \.NET targeting \.NET Standard 1\.3 until October 1, 2020\. After that date, the \.NET Standard 1\.3 target went into Maintenance mode, which meant that no new updates were released; AWS applied critical bug fixes and security patches only\.

On December 31, 2020, support for \.NET Standard 1\.3 on the AWS SDK for \.NET came to its end of life\. After that date no bug fixes or security patches were applied\. Artifacts built with that target remain available for download on NuGet\.

**What you need to do**
+ If you're running applications using \.NET Framework, you're not affected\.
+ If you're running applications using \.NET Core 2\.0 or higher, you're not affected\.
+ If you're running applications using \.NET Core 1\.0 or \.NET Core 1\.1, migrate your applications to a newer version of \.NET Core by following [Microsoft migration instructions](https://docs.microsoft.com/en-us/dotnet/core/migration/)\. We recommend a minimum of \.NET Core 3\.1\.
+ If you're running business critical applications that cannot be upgraded at this time, you can continue using your current version of AWS SDK for \.NET\.

If you have questions or concerns, [contact AWS Support](https://console.aws.amazon.com/support)\.