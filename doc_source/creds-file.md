--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Using the shared AWS credentials file<a name="creds-file"></a>

\(Be sure to review the [important warnings and guidance for credentials](net-dg-config-creds-warnings-and-guidelines.md)\.\)

One way to provide credentials for your applications is to create profiles in the *shared AWS credentials file* and then store credentials in those profiles\. This file can be used by the other AWS SDKs\. It can also be used by the [AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/), the [AWS Tools for Windows PowerShell](https://docs.aws.amazon.com/powershell/latest/userguide/), and the AWS toolkits for [Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/), [JetBrains](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/), and [VS Code](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/)\.

## General information<a name="creds-file-general-info"></a>

By default, the shared AWS credentials file is located in the `.aws` directory within your home directory and is named `credentials`; that is, `~/.aws/credentials` \(Linux or macOS\) or `%USERPROFILE%\.aws\credentials` \(Windows\)\. For information about alternative locations, see [Location of the shared files](https://docs.aws.amazon.com/credref/latest/refdocs/file-location.html) in the [AWS SDKs and Tools Shared Configuration and Credentials Reference Guide](https://docs.aws.amazon.com/credref/latest/refdocs/overview.html)\. Also see [Accessing credentials and profiles in an application](creds-locate.md)\.

The shared AWS credentials file is a plaintext file and follows a certain format\. For information about the format of AWS credentials files, see [Format of the credentials file](https://docs.aws.amazon.com/credref/latest/refdocs/file-format.html#file-format-creds) in the *AWS SDKs and Tools Shared Configuration and Credentials Reference Guide*\.

You can manage the profiles in the shared AWS credentials file in several ways\.
+ Use any text editor to create and update the shared AWS credentials file\.
+ Use the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace of the AWS SDK for \.NET API, as shown later in this topic\.
+ Use commands and procedures for the [AWS Tools for PowerShell](https://docs.aws.amazon.com/powershell/latest/userguide/specifying-your-aws-credentials.html) and the AWS toolkits for [Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/credentials.html), [JetBrains](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/setup-credentials.html), and [VS Code\.](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/setup-credentials.html)
+ Use [AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html) commands; for example, `aws configure set aws_access_key_id` and `aws configure set aws_secret_access_key`\.

## Examples of profile management<a name="creds-file-examples"></a>

The following sections show examples of profiles in the shared AWS credentials file\. Some of the examples show the result, which can be obtained through any of the credential\-management methods described earlier\. Other examples show how to use a particular method\.

### The default profile<a name="creds-file-default"></a>

The shared AWS credentials file will almost always have a profile named *default*\. This is where the AWS SDK for \.NET looks for credentials if no other profiles are defined\.

The `[default]` profile typically looks something like the following\.

```
[default]
aws_access_key_id = AKIAIOSFODNN7EXAMPLE
aws_secret_access_key = wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
```

### Create a profile programmatically<a name="creds-file-create-programmatically"></a>

This example shows you how to create a profile and save it to the shared AWS credentials file programmatically\. It uses the following classes of the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace: [CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html), [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html), and [SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html)\.

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
    var sharedFile = new SharedCredentialsFile();
    sharedFile.RegisterProfile(profile);
}
```

**Warning**  
Code such as this generally shouldn't be in your application\. If you include it in your application, take appropriate precautions to ensure that plaintext keys can't possibly be seen in the code, over the network, or even in computer memory\.

The following is the profile that's created by this example\.

```
[my_new_profile]
aws_access_key_id=AKIAIOSFODNN7EXAMPLE
aws_secret_access_key=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
```

### Update an existing profile programmatically<a name="creds-file-update-programmatically"></a>

This example shows you how to programmatically update the profile that was created earlier\. It uses the following classes of the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace: [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) and [SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html)\. It also uses the [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpoint.html) class of the [Amazon](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/N.html) namespace\.

```
using Amazon.Runtime.CredentialManagement;
...

AddRegion("my_new_profile", RegionEndpoint.USWest2);
...

void AddRegion(string profileName, RegionEndpoint region)
{
    var sharedFile = new SharedCredentialsFile();
    CredentialProfile profile;
    if (sharedFile.TryGetProfile(profileName, out profile))
    {
        profile.Region = region;
        sharedFile.RegisterProfile(profile);
    }
}
```

The following is the updated profile\.

```
[my_new_profile]
aws_access_key_id=AKIAIOSFODNN7EXAMPLE
aws_secret_access_key=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
region=us-west-2
```

**Note**  
You can also set the AWS Region in other locations and by using other methods\. For more information, see [Configure the AWS Region](net-dg-region-selection.md)\.