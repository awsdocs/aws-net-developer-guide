--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Using the SDK Store \(Windows only\)<a name="sdk-store"></a>

\(Be sure to review the [important warnings and guidelines](net-dg-config-creds-warnings-and-guidelines.md)\.\)

On Windows, the *SDK Store* is another place to create profiles and store encrypted credentials for your AWS SDK for \.NET application\. It's located in `%USERPROFILE%\AppData\Local\AWSToolkit\RegisteredAccounts.json`\. You can use the SDK Store during development as an alternative to the [shared AWS credentials file](creds-file.md)\.

## General information<a name="sdk-store-general-info"></a>

The SDK Store provides the following benefits:
+ The credentials in the SDK Store are encrypted, and the SDK Store resides in the user's home directory\. This limits the risk of accidentally exposing your credentials\.
+ The SDK Store also provides credentials to the [AWS Tools for Windows PowerShell](https://docs.aws.amazon.com/powershell/latest/userguide/) and the [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\.

SDK Store profiles are specific to a particular user on a particular host\. You can't copy them to other hosts or other users\. This means that you can't reuse SDK Store profiles that are on your development machine for other hosts or developer machines\. It also means that you can't use SDK Store profiles in production applications\.

You can manage the profiles in the SDK Store in the following ways:
+ Use the graphical user interface \(GUI\) in the [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/credentials.html)\.
+ Use the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace of the AWS SDK for \.NET API, as shown later in this topic\.
+ Use commands from the [AWS Tools for Windows PowerShell](https://docs.aws.amazon.com/powershell/latest/userguide/specifying-your-aws-credentials.html); for example, `Set-AWSCredential` and `Remove-AWSCredentialProfile`\.

## Examples of profile management<a name="sdk-store-examples"></a>

The following examples show you how to programmatically create and update a profile in the SDK Store\.

### Create a profile programmatically<a name="sdk-store-create-programmatically"></a>

This example shows you how to create a profile and save it to the SDK Store programmatically\. It uses the following classes of the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace: [CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html), [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html), and [NetSDKCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TNetSDKCredentialsFile.html)\.

```
using Amazon.Runtime.CredentialManagement;
...

// For illustrative purposes only--do not include credentials in your code.
WriteProfile("my_new_profile", "AKIAIOSFODNN7EXAMPLE", "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY");
...

void WriteProfile(string profileName, string keyId, string secret)
{
    Console.WriteLine($"Create the [{profileName}] profile...");
    var options = new CredentialProfileOptions
    {
        AccessKey = keyId,
        SecretKey = secret
    };
    var profile = new CredentialProfile(profileName, options);
    var netSdkStore = new NetSDKCredentialsFile();
    netSdkStore.RegisterProfile(profile);
}
```

**Warning**  
Code such as this generally shouldn't be in your application\. If it's included in your application, take appropriate precautions to ensure that plaintext keys can't possibly be seen in the code, over the network, or even in computer memory\.

The following is the profile that's created by this example\.

```
"[generated GUID]" : {
    "AWSAccessKey" : "01000000D08...[etc., encrypted access key ID]",
    "AWSSecretKey" : "01000000D08...[etc., encrypted secret access key]",
    "ProfileType"  : "AWS",
    "DisplayName"  : "my_new_profile",
}
```

### Update an existing profile programmatically<a name="sdk-store-update-programmatically"></a>

This example shows you how to programmatically update the profile that was created earlier\. It uses the following classes of the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace: [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) and [NetSDKCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TNetSDKCredentialsFile.html)\. It also uses the [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpoint.html) class of the [Amazon](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/N.html) namespace\.

```
using Amazon.Runtime.CredentialManagement;
...

AddRegion("my_new_profile", RegionEndpoint.USWest2);
...

void AddRegion(string profileName, RegionEndpoint region)
{
    var netSdkStore = new NetSDKCredentialsFile();
    CredentialProfile profile;
    if (netSdkStore.TryGetProfile(profileName, out profile))
    {
        profile.Region = region;
        netSdkStore.RegisterProfile(profile);
    }
}
```

The following is the updated profile\.

```
"[generated GUID]" : {
    "AWSAccessKey" : "01000000D08...[etc., encrypted access key ID]",
    "AWSSecretKey" : "01000000D08...[etc., encrypted secret access key]",
    "ProfileType"  : "AWS",
    "DisplayName"  : "my_new_profile",
    "Region"       : "us-west-2"
}
```

**Note**  
You can also set the AWS Region in other locations and by using other methods\. For more information, see [Configure the AWS Region](net-dg-region-selection.md)\.