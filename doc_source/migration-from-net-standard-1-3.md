--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Migrating from \.NET Standard 1\.3<a name="migration-from-net-standard-1-3"></a>

On June 27 2019 Microsoft [ended support](https://devblogs.microsoft.com/dotnet/net-core-1-0-and-1-1-will-reach-end-of-life-on-june-27-2019/) for \.NET Core 1\.0 and \.NET Core 1\.1 versions\. Following this announcement, AWS will end support for \.NET Standard 1\.3 on the AWS SDK for \.NET on December 31, 2020\.

AWS will continue to provide service updates and security fixes on the AWS SDK for \.NET targeting \.NET Standard 1\.3 until October 1, 2020\. After this date, the \.NET Standard 1\.3 target will go into Maintenance mode, which means that no new updates will be released; AWS will apply critical bug fixes and security patches only\.

On December 31, 2020, support for \.NET Standard 1\.3 on the AWS SDK for \.NET will come to its end of life\. After this date no bug fixes or security patches will be applied\. Artifacts built with that target will still remain available for download on NuGet\.

**What you need to do**
+ If you're running applications using \.NET Framework, you're not affected\.
+ If you're running applications using \.NET Core 2\.0 or higher, you're not affected\.
+ If you're running applications using \.NET Core 1\.0 or \.NET Core 1\.1, migrate your applications to a newer version of \.NET Core by following [Microsoft migration instructions](https://docs.microsoft.com/en-us/dotnet/core/migration/)\. We recommend a minimum of \.NET Core 3\.1\.
+ If you're running business critical applications that cannot be upgraded at this time, you can continue using your current version of AWS SDK for \.NET\.

If you have questions or concerns, [contact AWS Support](https://console.aws.amazon.com/support)\.