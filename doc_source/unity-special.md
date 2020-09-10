--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Special considerations for Unity support<a name="unity-special"></a>

When using the AWS SDK for \.NET and [\.NET Standard 2\.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) for your Unity application, your application must reference the AWS SDK for \.NET assemblies \(DLL files\) directly rather than using NuGet\. Given this requirement, the following are important actions you will need to perform\.
+ You need to obtain the AWS SDK for \.NET assemblies and apply them to your project\. For information about how to do this, see [Download and extract ZIP files](net-dg-obtain-assemblies.md#download-zip-files) in the topic [Obtaining AWSSDK assemblies](net-dg-obtain-assemblies.md)\.
+ If you're using [IL2CPP](https://docs.unity3d.com/Manual/IL2CPP.html) to build your Unity project, you must add a `link.xml` file to your Asset folder to prevent code stripping\. The `link.xml` file must list all of the AWSSDK assemblies you are using, and each must include the `preserve="all"` attribute\. The following snippet shows an example of this file\.

  ```
  <linker>
      <assembly fullname="AWSSDK.Core" preserve="all"/>
      <assembly fullname="AWSSDK.DynamoDBv2" preserve="all"/>
      <assembly fullname="AWSSDK.Lambda" preserve="all"/>
  </linker>
  ```

**Note**  
To read interesting background information related to this requirement, see the article at [https://aws\.amazon\.com/blogs/developer/referencing\-the\-aws\-sdk\-for\-net\-standard\-2\-0\-from\-unity\-xamarin\-or\-uwp/](https://aws.amazon.com/blogs/developer/referencing-the-aws-sdk-for-net-standard-2-0-from-unity-xamarin-or-uwp/)\.

In addition to these special considerations, see [What's changed for version 3\.5](net-dg-v35.md#net-dg-v35-changes) for information about migrating your Unity application to version 3\.5 of the AWS SDK for \.NET\.