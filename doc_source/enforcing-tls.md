--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Enforcing TLS 1\.2 in this AWS Product or Service<a name="enforcing-tls"></a>

To increase security when communicating with AWS services, you should configure this AWS product or service to use TLS 1\.2 or later\.

The AWS SDK for \.NET uses the underlying \.NET runtime to determine which security protocol to use\. By default, current versions of \.NET use the latest configured protocol that the operating system supports\. Your application can override this SDK behavior, but it's *not recommended* to do so\.

## \.NET Core<a name="enforcing-tls-dotnet-core"></a>

By default, \.NET Core uses the latest configured protocol that the operating system supports\. The AWS SDK for \.NET doesn't provide a mechanism to override this\.

If you're using a \.NET Core version earlier than 2\.1, we *strongly* recommend you upgrade your \.NET Core version\.

See the following for information specific to each operating system\.

**Windows**

Modern distributions of Windows have TLS 1\.2 support [enabled by default](https://docs.microsoft.com/en-us/windows/win32/secauthn/protocols-in-tls-ssl--schannel-ssp-)\. If you're running on Windows 7 SP1 or Windows Server 2008 R2 SP1, you need to ensure that TLS 1\.2 support is enabled in the registry, as described at [https://docs\.microsoft\.com/en\-us/windows\-server/security/tls/tls\-registry\-settings\#tls\-12](https://docs.microsoft.com/en-us/windows-server/security/tls/tls-registry-settings#tls-12)\. If you're running an earlier distribution, you must upgrade your operating system\.

**macOS**

If you're running \.NET Core 2\.1 or later, TLS 1\.2 is enabled by default\. TLS 1\.2 is supported by [OS X Mavericks v10\.9 or later](https://support.apple.com/en-us/HT202854)\. \.NET Core version 2\.1 and later require newer versions of macOS, as described at [https://docs\.microsoft\.com/en\-us/dotnet/core/install/dependencies?tabs=netcore21&pivots=os\-macos](https://docs.microsoft.com/en-us/dotnet/core/install/dependencies?tabs=netcore21&pivots=os-macos)\.

If you're using \.NET Core 1\.0, \.NET Core [uses OpenSSL on macOS](https://github.com/dotnet/announcements/issues/21), a dependency that must be installed separately\. OpenSSL added support for TLS 1\.2 in [version 1\.0\.1](https://www.openssl.org/news/changelog.html#x35) \(03/14/2012\)\.

**Linux**

\.NET Core on Linux requires OpenSSL, which comes bundled with many Linux distributions\. But it can also be installed separately\. OpenSSL added support for TLS 1\.2 in [version 1\.0\.1](https://www.openssl.org/news/changelog.html#x35) \(03/14/2012\)\. If you're using a modern version of \.NET Core \(2\.1 or later\) and have installed a package manager, it's likely that a more modern version of OpenSSL was installed for you\.

To be sure, you can run **`openssl version`** in a terminal and verify that the version is later than 1\.0\.1\.

## \.NET Framework<a name="enforcing-tls-dotnet-framework"></a>

If you're running a modern version of \.NET Framework \(4\.7 or later\) and a modern version of Windows \(at least Windows 8 for clients, Windows Server 2012 or later for servers\), TLS 1\.2 is enabled and used by default\.

If you're using a \.NET Framework runtime that doesn't use the operating system settings \(\.NET Framework 3\.5 through 4\.5\.2\), the AWS SDK for \.NET will attempt to [add support for TLS 1\.1 and TLS 1\.2](https://github.com/aws/aws-sdk-net/blob/master/sdk/src/Core/Amazon.Runtime/Pipeline/HttpHandler/_bcl/AmazonSecurityProtocolManager.cs) to the supported protocols\. If you're using \.NET Framework 3\.5, this will be successful only if the appropriate hot patch is installed, as follows:
+ Windows 10 version 1511 and Windows Server 2016 – [KB3156421](https://support.microsoft.com/kb/3156421)
+ Windows 8\.1 and Windows Server 2012 R2 – [KB3154520](https://support.microsoft.com/kb/3154520)
+ Windows Server 2012 – [KB3154519](https://support.microsoft.com/kb/3154519)
+ Windows 7 SP1 and Server 2008 R2 SP1 – [KB3154518](https://support.microsoft.com/kb/3154518)

If your application is running on a newer \.NET Framework on Windows 7 SP1 or Windows Server 2008 R2 SP1, you need to ensure that TLS 1\.2 support is enabled in the registry, as described at [https://docs\.microsoft\.com/en\-us/windows\-server/security/tls/tls\-registry\-settings\#tls\-12](https://docs.microsoft.com/en-us/windows-server/security/tls/tls-registry-settings#tls-12)\. Newer versions of Windows have it [enabled by default](https://docs.microsoft.com/en-us/windows/win32/secauthn/protocols-in-tls-ssl--schannel-ssp-)\.

For detailed best practices for using TLS with \.NET Framework, see the Microsoft article at [https://docs\.microsoft\.com/en\-us/dotnet/framework/network\-programming/tls](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls)\.

## AWS Tools for PowerShell<a name="enforcing-tls-ps"></a>

[AWS Tools for PowerShell](https://docs.aws.amazon.com/powershell/latest/userguide/) use the AWS SDK for \.NET for all calls to AWS services\. The behavior of your environment depends on the version of Windows PowerShell you're running, as follows\.

**Windows PowerShell 2\.0 through 5\.x**

Windows PowerShell 2\.0 through 5\.x run on \.NET Framework\. You can verify which \.NET runtime \(2\.0 or 4\.0\) is being used by PowerShell by using the following command\.

```
$PSVersionTable.CLRVersion
```
+ When using \.NET Runtime 2\.0, follow the instructions provided earlier regarding the AWS SDK for \.NET and \.NET Framework 3\.5\.
+ When using \.NET Runtime 4\.0, follow the instructions provided earlier regarding the AWS SDK for \.NET and \.NET Framework 4\+\.

**Windows PowerShell 6\.0**

Windows PowerShell 6\.0 and newer run on \.NET Core\. You can verify which version of \.NET Core is being used by running the following command\.

```
[System.Reflection.Assembly]::GetEntryAssembly().GetCustomAttributes([System.Runtime.Versioning.TargetFrameworkAttribute], $true).FrameworkName
```

Follow the instructions provided earlier regarding the AWS SDK for \.NET and the relevant version of \.NET Core\.

## Xamarin<a name="enforcing-tls-xamarin"></a>

For Xamarin, see the directions at [https://docs\.microsoft\.com/en\-us/xamarin/cross\-platform/app\-fundamentals/transport\-layer\-security](https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/transport-layer-security)\. In summary:

**For Android**
+ Requires Android 5\.0 or later\.
+ **Project Properties**, **Android Options**: HttpClient implementation must be set to **Android** and the SSL/TLS implementation set to **Native TLS 1\.2\+**\.

**For iOS**
+ Requires iOS 7 or later\.
+ **Project Properties**, **iOS Build**: HttpClient implementation must be set to **NSUrlSession**\.

**For macOS**
+ Requires macOS 10\.9 or later\.
+ **Project Options**, **Build**, **Mac Build**: HttpClient implementation must be set to **NSUrlSession**\.

## Unity<a name="enforcing-tls-unity"></a>

You must use Unity 2018\.2 or later, and use the \.NET 4\.x Equivalent scripting runtime\. You can set this in **Project Settings**, **Configuration**, **Player**, as described at [https://docs\.unity3d\.com/2019\.1/Documentation/Manual/ScriptingRuntimeUpgrade\.html](https://docs.unity3d.com/2019.1/Documentation/Manual/ScriptingRuntimeUpgrade.html)\. The \.NET 4\.x Equivalent scripting runtime enables TLS 1\.2 support to all Unity platforms running Mono or IL2CPP\. For more information, see [https://blogs\.unity3d\.com/2018/07/11/scripting\-runtime\-improvements\-in\-unity\-2018\-2/](https://blogs.unity3d.com/2018/07/11/scripting-runtime-improvements-in-unity-2018-2/)\.

## Browser \(for Blazor WebAssembly\)<a name="enforcing-tls-browser"></a>

WebAssembly runs in the browser instead of on the server, and uses the browser for handling HTTP traffic\. Therefore, TLS support is determined by browser support\.

Blazor WebAssembly, in preview for ASP\.NET Core 3\.1, is supported only in browsers that support WebAssembly, as described at [https://docs\.microsoft\.com/en\-us/aspnet/core/blazor/supported\-platforms](https://docs.microsoft.com/en-us/aspnet/core/blazor/supported-platforms)\. All mainstream browsers supported TLS 1\.2 before supporting WebAssembly\. If this is the case for your browser, then if your app runs, it can communicate over TLS 1\.2\.

See your browser's documentation for more information and verification\.