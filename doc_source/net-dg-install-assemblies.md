# Install AWSSDK Assemblies<a name="net-dg-install-assemblies"></a>

The AWSSDK assemblies are available from [NuGet](https://www.nuget.org/profiles/awsdotnet) or through a Windows installation package\. The AWS SDK for \.NET is also available from the [aws/aws\-sdk\-net](https://github.com/aws/aws-sdk-net) repository on GitHub\. Finally, you can find information about many things AWS \.NET in the [aws/dotnet](https://github.com/aws/dotnet) repository on GitHub\.

## Installing AWSSDK Assemblies with NuGet<a name="net-dg-nuget"></a>

[NuGet](http://nuget.org/) is a package management system for the \.NET platform\. With NuGet, you can add the AWSSDK assemblies, as well as several other extensions, to your application\. For additional information, see the [aws/dotnet](https://github.com/aws/dotnet) repository on GitHub\.

NuGet always has the most recent versions of the AWSSDK assemblies, and also enables you to install previous versions\. NuGet is aware of dependencies between assemblies and installs all required assemblies automatically\. Assemblies installed with NuGet are stored with your solution instead of in a central location, such as in the Program Files directory\. This enables you to install assembly versions specific to a given application without creating compatibility issues for other applications\. For more information about NuGet, see the [NuGet documentation](http://docs.nuget.org/)\.

NuGet is installed automatically with Visual Studio 2010 or later\. If you are using an earlier version of Visual Studio, you can install NuGet from the [Visual Studio Gallery on MSDN](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c)\.

### NuGet AWSSDK Packages<a name="nuget-awssdk-packages"></a>

The NuGet website provides a page for every package available through NuGet\. The page for each package includes a sample command line for installing the package using the Package Manager Console\. Each page also includes a list of the previous versions of the package that are available through NuGet\. For a list of AWSSDK packages available from NuGet, see [AWSSDK Packages](http://www.nuget.org/profiles/awsdotnet)\.

### Using NuGet from the Command Line or Terminal<a name="package-install-nuget"></a>

1. Go to the [NuGet packages for AWS](https://www.nuget.org/profiles/awsdotnet) and determine which NuGet packages you need in your project; for example, **[AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3/)**\.

1. Copy the \.NET CLI command from that package’s webpage; for example:

   **`dotnet add package AWSSDK.S3 --version 3.3.110.19`**

1. In your project's directory run that \.NET CLI command\.

### Using NuGet from Solution Explorer<a name="package-install-gui"></a>

1. In **Solution Explorer**, right\-click your project and then choose **Manage NuGet Packages** from the context menu\.

1. In the left pane of the **NuGet Package Manager**, choose **Browse**\. You can then use the search box to search for the package you want to install\.

   The following figure shows installation for the **AWSSDK\.S3** package\. NuGet automatically installs any dependency, which in this case is the `AWSSDK.Core` package\.  
![\[AWSSDK.S3 package shown in NuGet Packages Manager.\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/nuget-install-vs-dlg.png)

### Using NuGet from the Package Manager Console<a name="package-install-cmd"></a>

From the Visual Studio **Tools** menu, choose **NuGet Package Manager**, **Package Manager Console**\.

You can install the AWSSDK assemblies you want from the Package Manager Console by using the ** `Install-Package` ** command\. For example, to install [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3), use the following command\.

```
PM> Install-Package AWSSDK.S3
```

NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.

If you need to install an earlier version of a package, use the `-Version` option and specify the package version you want\. For example:

```
PM> Install-Package AWSSDK.S3 -Version 3.3.106.6
```

For more information about Package Manager Console commands, see [PowerShell reference](https://docs.microsoft.com/en-us/nuget/reference/powershell-reference) in Microsoft's [NuGet documentation](https://docs.microsoft.com/en-us/nuget/)\.

## Installing the AWS SDK for \.NET on Windows<a name="net-dg-install-net-sdk"></a>

The preferred method of installing the AWS SDK for \.NET on Windows is to install AWSSDK NuGet packages as needed\. This is described in the [previous sections of this topic](#net-dg-nuget)\.

You use NuGet to install individual AWSSDK service assemblies and extensions for the SDK\.

**Note**  
If you are required to install an MSI instead of using NuGet, you can find the MSI at [https://sdk\-for\-net\.amazonwebservices\.com/latest/AWSToolsAndSDKForNet\.msi](https://sdk-for-net.amazonwebservices.com/latest/AWSToolsAndSDKForNet.msi)\. By default, the AWS SDK for \.NET is installed in the `Program Files` directory, which requires administrator privileges\. To install the SDK as a non\-administrator, choose a different installation directory\.