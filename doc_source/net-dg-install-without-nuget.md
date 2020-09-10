--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Install AWSSDK assemblies without NuGet<a name="net-dg-install-without-nuget"></a>

This topic describes how you can use the AWSSDK assemblies that you obtained and stored locally \(or on premises\) as described in [Obtaining AWSSDK assemblies](net-dg-obtain-assemblies.md)\. This is **not** the recommended method for handling SDK references, but is required in some environments\.

**Note**  
The recommended method for handling SDK references is to download and install just the NuGet packages that each project needs\. That method is described in [Install AWSSDK packages with NuGet](net-dg-install-assemblies.md)\.

**To install AWSSDK assemblies**

1. Create a folder in your project area for the required AWSSDK assemblies\. As an example, you might call this folder `AwsAssemblies`\.

1. If you haven't already done so, [obtain the AWSSDK assemblies](net-dg-obtain-assemblies.md), which places the assemblies in some local download or installation folder\. Copy the DLL files for the required assemblies from that download folder into your project \(into the `AwsAssemblies` folder, in our example\)\.

   Be sure to also copy any dependencies\. You can find information about dependencies on the [GitHub](https://github.com/aws/aws-sdk-net/blob/master/generator/ServiceModels/_sdk-versions.json) website\.

1. Make reference to the required assemblies as follows\.

------
#### [ Cross\-platform development ]

   1. Open your project's `.csproj` file and add an `<ItemGroup>` element\.

   1. In the `<ItemGroup>` element, add a `<Reference>` element with an `Include` attribute for each required assembly\.

      For Amazon S3, for example, you would add the following lines to your project's `.csproj` file\.

      **On Linux and macOS:**

      ```
      <ItemGroup>
        <Reference Include="./AwsAssemblies/AWSSDK.Core.dll" />
        <Reference Include="./AwsAssemblies/AWSSDK.S3.dll" />
      </ItemGroup>
      ```

      **On Windows:**

      ```
      <ItemGroup>
        <Reference Include="AwsAssemblies\AWSSDK.Core.dll" />
        <Reference Include="AwsAssemblies\AWSSDK.S3.dll" />
      </ItemGroup>
      ```

   1. Save you project's `.csproj` file\.

------
#### [ Windows with Visual Studio and \.NET Core ]

   1. In Visual Studio, load your project and open **Project**, **Add Reference**\.

   1. Choose the **Browse** button on the bottom of the dialog box\. Navigate to your project's folder and the subfolder that you copied the required DLL files to \(`AwsAssemblies`, for example\)\.

   1. Select all the DLL files, choose **Add**, and choose **OK**\.

   1. Save your project\.

------