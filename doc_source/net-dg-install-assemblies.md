--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Install AWSSDK packages with NuGet<a name="net-dg-install-assemblies"></a>

[NuGet](https://www.nuget.org/) is a package management system for the \.NET platform\. With NuGet, you can install the [AWSSDK packages](https://www.nuget.org/profiles/awsdotnet), as well as several other extensions, to your project\. For additional information, see the [aws/dotnet](https://github.com/aws/dotnet) repository on the GitHub website\.

NuGet always has the most recent versions of the AWSSDK packages, as well as previous versions\. NuGet is aware of dependencies between packages and installs all required packages automatically\.

**Warning**  
The list of NuGet packages might include one named simply "AWSSDK" \(with no appended identifier\)\. Do NOT install this NuGet package; it is legacy and should not be used for new projects\.

Packages installed with NuGet are stored with your project instead of in a central location\. This enables you to install assembly versions specific to a given application without creating compatibility issues for other applications\. For more information about NuGet, see the [NuGet documentation](https://docs.microsoft.com/en-us/nuget/)\.

**Note**  
If you can't or aren't allowed to download and install NuGet packages on a per\-project basis, you can obtain the AWSSDK assemblies and store them locally \(or on premises\)\.  
If this applies to you and you haven't already obtained the AWSSDK assemblies, see [Obtaining AWSSDK assemblies](net-dg-obtain-assemblies.md)\. To learn how to use the locally stored assemblies, see [Install AWSSDK assemblies without NuGet](net-dg-install-without-nuget.md)\.

## Using NuGet from the Command prompt or terminal<a name="package-install-nuget"></a>

1. Go to the [AWSSDK packages on NuGet](https://www.nuget.org/profiles/awsdotnet) and determine which packages you need in your project; for example, **[AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3/)**\.

1. Copy the \.NET CLI command from that package's webpage, as shown in the following example\.

   **`dotnet add package AWSSDK.S3 --version 3.3.110.19`**

1. In your project's directory, run that \.NET CLI command\. NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.

**Note**  
If you want only the latest version of a NuGet package, you can exclude version information from the command, as shown in the following example\.  
**`dotnet add package AWSSDK.S3`**

## Using NuGet from Visual Studio Solution Explorer<a name="package-install-gui"></a>

1. In **Solution Explorer**, right\-click your project, and then choose **Manage NuGet Packages** from the context menu\.

1. In the left pane of the **NuGet Package Manager**, choose **Browse**\. You can then use the search box to search for the package you want to install\.

   The following figure shows installation of the **AWSSDK\.S3** package\. NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.  
![\[AWSSDK.S3 package shown in NuGet Packages Manager.\]](http://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/images/nuget-install-vs-dlg.png)

## Using NuGet from the Package Manager Console<a name="package-install-cmd"></a>

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