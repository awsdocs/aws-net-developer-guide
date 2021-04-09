--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Install AWSSDK Assemblies<a name="net-dg-install-assemblies"></a>

The AWSSDK assemblies are available from [NuGet](https://www.nuget.org/profiles/awsdotnet) or through a Windows installation package\. The AWS SDK for \.NET is also available from the [aws/aws\-sdk\-net](https://github.com/aws/aws-sdk-net) repository on GitHub\. Finally, you can find information about many AWS things related to \.NET in the [aws/dotnet](https://github.com/aws/dotnet) repository on GitHub\.

## Installing AWSSDK packages with NuGet<a name="net-dg-nuget"></a>

[NuGet](https://www.nuget.org/) is a package management system for the \.NET platform\. With NuGet, you can install the [AWSSDK packages](https://www.nuget.org/profiles/awsdotnet), as well as several other extensions, to your project\. For additional information, see the [aws/dotnet](https://github.com/aws/dotnet) repository on the GitHub website\.

NuGet always has the most recent versions of the AWSSDK packages, as well as previous versions\. NuGet is aware of dependencies between packages and installs all required packages automatically\.

**Warning**  
The list of NuGet packages might include one named simply "AWSSDK" \(with no appended identifier\)\. Do NOT install this NuGet package; it is legacy and should not be used for new projects\.

Packages installed with NuGet are stored with your project instead of in a central location\. This enables you to install assembly versions specific to a given application without creating compatibility issues for other applications\. For more information about NuGet, see the [NuGet documentation](https://docs.microsoft.com/en-us/nuget/)\.

NuGet is installed automatically with Visual Studio 2010 or later\. If you are using an earlier version of Visual Studio, you can install NuGet from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=NuGetTeam.NuGetPackageManager)\.

### Using NuGet from the Command prompt or terminal<a name="package-install-nuget"></a>

1. Go to the [AWSSDK packages on NuGet](https://www.nuget.org/profiles/awsdotnet) and determine which packages you need in your project; for example, **[AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3/)**\.

1. Copy the \.NET CLI command from that package's webpage, as shown in the following example\.

   **`dotnet add package AWSSDK.S3 --version 3.3.110.19`**

1. In your project's directory, run that \.NET CLI command\. NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.

**Note**  
If you want only the latest version of a NuGet package, you can exclude version information from the command, as shown in the following example\.  
**`dotnet add package AWSSDK.S3`**

### Using NuGet from Visual Studio Solution Explorer<a name="package-install-gui"></a>

1. In **Solution Explorer**, right\-click your project, and then choose **Manage NuGet Packages** from the context menu\.

1. In the left pane of the **NuGet Package Manager**, choose **Browse**\. You can then use the search box to search for the package you want to install\.

   The following figure shows installation of the **AWSSDK\.S3** package\. NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.  
![\[AWSSDK.S3 package shown in NuGet Packages Manager.\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/nuget-install-vs-dlg.png)

### Using NuGet from the Package Manager Console<a name="package-install-cmd"></a>

In Visual Studio, choose **Tools**, **NuGet Package Manager**, **Package Manager Console**\.

You can install the AWSSDK packages you want from the Package Manager Console by using the **`Install-Package`** command\. For example, to install [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3), use the following command\.

```
PM> Install-Package AWSSDK.S3
```

NuGet also installs any dependencies, such as [AWSSDK\.Core](https://www.nuget.org/packages/AWSSDK.Core)\.

If you need to install an earlier version of a package, use the `-Version` option and specify the package version you want, as shown in the following example\.

```
PM> Install-Package AWSSDK.S3 -Version 3.3.106.6
```

For more information about Package Manager Console commands, see the [PowerShell reference](https://docs.microsoft.com/en-us/nuget/reference/powershell-reference) in Microsoft's [NuGet documentation](https://docs.microsoft.com/en-us/nuget/)\.

## Download and extract ZIP files<a name="download-zip-files"></a>

The preferred method of installing the AWS SDK for \.NET is to install AWSSDK NuGet packages as needed\. This is described in the [previous sections of this topic](#net-dg-nuget)\.

If you can't or aren't allowed to download and install NuGet packages on a per\-project basis, you can download a ZIP file that contains the AWSSDK assemblies\. If this is the case for you, do the following\.

1. Download one of the following ZIP files:
   + [aws\-sdk\-netstandard2\.0\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-netstandard2.0.zip)
   + [aws\-sdk\-net45\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net45.zip)
   + [aws\-sdk\-net35\.zip](https://sdk-for-net.amazonwebservices.com/latest/v3/aws-sdk-net35.zip)

1. Extract the assemblies to a folder on your file system\. Make note of this folder\.

1. When you configure your project, copy the required assemblies from this folder into your project area\. Then add references in your project to the assemblies that you copied\.

## Installing the AWS SDK for \.NET on Windows<a name="net-dg-install-net-sdk"></a>

The preferred method of installing the AWS SDK for \.NET on Windows is to install AWSSDK NuGet packages as needed\. This is described in the [previous sections of this topic](#net-dg-nuget)\.

You use NuGet to install individual AWSSDK service assemblies and extensions for the SDK\.

**Note**  
If you are required to install an MSI instead of using NuGet, you can find the ***legacy*** MSI at [https://sdk\-for\-net\.amazonwebservices\.com/latest/AWSToolsAndSDKForNet\.msi](https://sdk-for-net.amazonwebservices.com/latest/AWSToolsAndSDKForNet.msi)\. By default, the AWS SDK for \.NET is installed in the `Program Files` directory, which requires administrator privileges\. To install the SDK as a non\-administrator, choose a different installation directory\.