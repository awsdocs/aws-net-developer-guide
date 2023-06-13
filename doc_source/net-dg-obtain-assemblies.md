# Obtaining assemblies for the AWS SDK for \.NET<a name="net-dg-obtain-assemblies"></a>

This topic describes how you can obtain the AWSSDK assemblies and store them locally \(or on premises\) for use in your projects\. This is **not** the recommended method for handling SDK references, but is required in some environments\.

**Note**  
The recommended method for handling SDK references is to download and install just the NuGet packages that each project needs\. That method is described in [Install AWSSDK packages with NuGet](net-dg-install-assemblies.md)\.

If you can't or aren't allowed to download and install NuGet packages on a per\-project basis, the following options are available to you\.

## Download and extract ZIP files<a name="download-zip-files"></a>

\(Remember that this isn't the [recommended method](net-dg-install-assemblies.md) for handling references to the AWS SDK for \.NET\.\)

1. Download one of the following ZIP files:
   + [aws\-sdk\-netcoreapp3\.1\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-netcoreapp3.1.zip) \- Assemblies that support \.NET Core 3\.1 and later\.
   + [aws\-sdk\-netstandard2\.0\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-netstandard2.0.zip) \- Assemblies that support \.NET Standard 2\.0 and 2\.1\.
   + [aws\-sdk\-net45\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net45.zip) \- Assemblies that support \.NET Framework 4\.5 and later\.
   + [aws\-sdk\-net35\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net35.zip) \- Assemblies that support \.NET Framework 3\.5\.

1. Extract the assemblies to some "download" folder on your file system; it doesn't matter where\. Make note of this folder\.

1. When you set up your project, you get the required assemblies from this folder, as described in [Install AWSSDK assemblies without NuGet](net-dg-install-without-nuget.md)\.

## Install the MSI on Windows<a name="net-dg-install-net-sdk"></a>

**Note**  
Support for the AWS Tools for Windows installer ends on June 1, 2023\. The installer will continue to be available for some time after this date, but will not contain the latest changes in the SDK\. For additional information, see the [AWS Developer Tools Blog post](http://aws.amazon.com/blogs/developer/announcing-the-end-of-windows-installer-support/)\. Also see the [recommended method](net-dg-install-assemblies.md) for handling references to the AWS SDK for \.NET\.

### Open to view information about the MSI\.<a name="w2aac27b9c21b5b1"></a>

\(Remember that this isn't the [recommended method](net-dg-install-assemblies.md) for handling references to the AWS SDK for \.NET\.\)

If you are required to install an MSI instead of using NuGet or one of the methods described earlier, you can find the ***legacy*** MSI at [https://sdk\-for\-net\.amazonwebservices\.com/latest/AWSToolsAndSDKForNet\.msi](https://sdk-for-net.amazonwebservices.com/latest/AWSToolsAndSDKForNet.msi)\.

By default, the AWS SDK for \.NET is installed in the `Program Files` folder, which requires administrator privileges\. To install the SDK as a non\-administrator, choose a different folder\.