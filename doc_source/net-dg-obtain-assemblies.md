--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Obtaining assemblies for the AWS SDK for \.NET<a name="net-dg-obtain-assemblies"></a>

This topic describes how you can obtain the AWSSDK assemblies and store them locally \(or on premises\) for use in your projects\. This is **not** the recommended method for handling SDK references, but is required in some environments\.

**Note**  
The recommended method for handling SDK references is to download and install just the NuGet packages that each project needs\. That method is described in [Install AWSSDK packages with NuGet](net-dg-install-assemblies.md)\.

If you can't or aren't allowed to download and install NuGet packages on a per\-project basis, the following options are available to you\.

## Download and extract ZIP files<a name="download-zip-files"></a>

\(Remember that this isn't the [recommended method](net-dg-install-assemblies.md) for handling references to the AWS SDK for \.NET\.\)

1. Download one of the following ZIP files:
   + [aws\-sdk\-netstandard2\.0\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-netstandard2.0.zip)
   + [aws\-sdk\-net45\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net45.zip)
   + [aws\-sdk\-net35\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net35.zip)

1. Extract the assemblies to some "download" folder on your file system; it doesn't matter where\. Make note of this folder\.

1. When you set up your project, you get the required assemblies from this folder, as described in [Install AWSSDK assemblies without NuGet](net-dg-install-without-nuget.md)\.

## Install the MSI on Windows<a name="net-dg-install-net-sdk"></a>

\(Remember that this isn't the [recommended method](net-dg-install-assemblies.md) for handling references to the AWS SDK for \.NET\.\)

If you are required to install an MSI instead of using NuGet or one of the methods described earlier, you can find the ***legacy*** MSI at [https://sdk\-for\-net\.amazonwebservices\.com/latest/AWSToolsAndSDKForNet\.msi](https://sdk-for-net.amazonwebservices.com/latest/AWSToolsAndSDKForNet.msi)\.

By default, the AWS SDK for \.NET is installed in the `Program Files` folder, which requires administrator privileges\. To install the SDK as a non\-administrator, choose a different folder\.