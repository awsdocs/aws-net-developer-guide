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

1. In **Solution Explorer**, right\-click your project, and then choose **Manage NuGet Packages** from the context menu\.

1. In the left pane of the **Manage NuGet Packages** dialog box, choose **Online**\. You can then use the search box in the top\-right corner to search for the package you want to install\.

   The following figure shows the **AWSSDK \- Core Runtime** assembly package\. You can see NuGet is aware that this package has a dependency on the `AWSSDK.Core` assembly package; NuGet automatically installs the `AWSSDK.Core` package, if it is not already installed\.  
![\[AWSSDK Core Runtime package and dependency on :code:`AWSSDK.Core` assembly shown in Manage NuGet Packages dialog\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/nuget-install-vs-dlg.png)

### Using NuGet from the Package Manager Console<a name="package-install-cmd"></a>
+  *Visual Studio 2010* 

  From the **Tools** menu, choose **Library Package Manager**, and then click **Package Manager Console**\.
+  *Visual Studio 2012 and later* 

  From the **Tools** menu, choose **Nuget Package Manager**, and then click **Package Manager Console**\.

You can install the AWSSDK assemblies you want from the Package Manager Console by using the ** `Install-Package` ** command\. For example, to install the [AWSSDK\.AutoScaling](http://www.nuget.org/packages/AWSSDK.AutoScaling) assembly, use the following command\.

```
PM> Install-Package AWSSDK.AutoScaling
```

NuGet also installs any dependencies, such as [AWSSDK\.Core](http://www.nuget.org/packages/AWSSDK.Core)\.

To install an earlier version of a package, use the `-Version` option and specify the package version you want\. For example, to install version 3\.1\.0\.0 of the AWS SDK for \.NET assembly, use the following command line\.

```
PM> Install-Package AWSSDK.Core -Version 3.1.0.0
```

For more information about Package Manager Console commands, see [Package Manager Console Commands \(v1\.3\)](http://nuget.codeplex.com/wikipage?title=Package%20Manager%20Console%20Command%20Reference%20%28v1.3%29)\.

## Installing the AWS SDK for \.NET on Windows<a name="net-dg-install-net-sdk"></a>

The following procedure describes how to install the AWS SDK for \.NET on Windows\.

**Note**  
The preferred method of installing the AWS SDK for \.NET is to install AWSSDK NuGet packages as needed\. This is described in the [previous section](#net-dg-nuget)\.

**To install the AWS SDK for \.NET on Windows**

1. Go to [AWS SDK for \.NET](https://aws.amazon.com/sdk-for-net/)\.

1. In the **Downloads** section, choose **Download MSI Installer** to download the installer\.

1. To start installation, run the downloaded installer and follow the on\-screen instructions\.
**Note**  
By default, the AWS SDK for \.NET is installed in the Program Files directory, which requires administrator privileges\. To install the AWS SDK for \.NET as a non\-administrator, choose a different installation directory\.

1. \(Optional\) You can use NuGet to install individual AWSSDK service assemblies and extensions for the AWS SDK for \.NET, which include a session state provider and a trace listener\. For more information, see [Installing AWSSDK Assemblies with NuGet](#net-dg-nuget)\.