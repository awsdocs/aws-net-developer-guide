--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Platforms supported by the AWS SDK for \.NET<a name="net-dg-platform-diffs-v3"></a>

The AWS SDK for \.NET provides distinct groups of assemblies for developers to target different platforms\. However, not all SDK functionality is the same on each of these platforms\. This topic describes the differences in support for each platform\.<a name="net-dg-platform-diff-netfx35"></a>

## \.NET Core<a name="net-core"></a>

The AWS SDK for \.NET supports applications written for \.NET Core\. AWS service clients only support asynchronous calling patterns in \.NET core\. This also affects many of the high level abstractions built on top of service clients like Amazon S3’s `TransferUtility` which will only support asynchronous calls in the \.NET Core environment\. For details, see [Configuring the AWS SDK for \.NET with \.NET Core](net-dg-config-netcore.md)\.

## \.NET Framework 4\.5<a name="net-dg-platform-diff-netfx45"></a>

This version of the AWS SDK for \.NET is compiled against \.NET Framework 4\.5 and runs in the \.NET 4\.0 runtime\. AWS service clients support synchronous and asynchronous calling patterns and use the [async and await](https://docs.microsoft.com/en-us/previous-versions/hh191443(v=vs.140)) keywords introduced in [C\# 5\.0](https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29#Versions)\.

## \.NET Framework 3\.5<a name="net-dg-platform-diff-winrt"></a>

This version of the AWS SDK for \.NET is compiled against \.NET Framework 3\.5, and runs either the \.NET 2\.0 or \.NET 4\.0 runtime\. AWS service clients support synchronous and asynchronous calling patterns and use the older Begin and End pattern\.

**Note**  
The AWS SDK for \.NET is not Federal Information Processing Standard \(FIPS\) compliant when used by applications built against version 2\.0 of the CLR\. For details on how you can substitute a FIPS compliant implementation in that environment, refer to [CryptoConfig](https://docs.microsoft.com/en-us/archive/blogs/shawnfa/cryptoconfig) on the Microsoft blog and the [CLR Security](http://clrsecurity.codeplex.com/) team’s HMACSHA256 class \( HMACSHA256Cng \) in Security\.Cryptography\.dll\.

## Portable Class Library<a name="portable-class-library"></a>

The AWS SDK for \.NET also contains a Portable Class Library implementation\. The Portable Class Library implementation can target multiple platforms, including Universal Windows Platform \(UWP\), and Xamarin on iOS and Android\. See the Mobile SDK for \.NET and Xamarin for more details\. AWS service clients only support asynchronous calling patterns\.

## Unity support<a name="unity-support"></a>

The AWS SDK for \.NET supports generating Assemblies for Unity\. More information can be found in the [Unity README](https://github.com/aws/aws-sdk-unity-net)\.

**Note**  
The reference given above is for ***legacy*** Unity support\. Starting with version 3\.5 of the AWS SDK for \.NET, projects using Unity 2018\.1 or later should target the \.NET Standard 2\.0 release of the SDK\. You can find additional information in the latest developer guide: [Unity support](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/unity-special.html) and [Migrating your Unity application](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/net-dg-v35.html#net-dg-v35-changes-unity)\.

## More info<a name="more-info"></a>
+ [Migrating to Version 3 of the AWS SDK for \.NET](migration-v3.md)
+ [Migrating to Version 3\.5 of the AWS SDK for \.NET](net-dg-v35.md)