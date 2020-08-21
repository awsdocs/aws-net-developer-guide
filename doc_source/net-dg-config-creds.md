--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Configuring AWS Credentials<a name="net-dg-config-creds"></a>

You must manage your AWS credentials securely and avoid practices that can unintentionally expose your credentials to the public\. In this topic, we describe how you configure your application’s AWS credentials so that they remain secure\.
+ Don’t use your account’s root credentials to access your AWS resources\. These credentials provide unrestricted account access and are difficult to revoke\.
+ Don’t put literal access keys in your application, including the project’s `App.config` or `Web.config` file\. If you do, you create a risk of accidentally exposing your credentials if, for example, you upload the project to a public repository\.

**Note**  
We assume you have created an AWS account and have access to your credentials\. If you haven’t yet, see [Create an AWS Account and Credentials](net-dg-signup.md)\.

The following are general guidelines for securely managing credentials:
+ Create IAM users and use their IAM user credentials instead of using your AWS root user\. IAM user credentials are easier to revoke if they’re compromised\. You can apply a policy to each IAM user that restricts the user to a specific set of resources and actions\.
+ During application development, the preferred approach for managing credentials is to put a profile for each set of IAM user credentials in the SDK Store\. You can also use a plaintext credentials file to store profiles that contain credentials\. Then you can reference a specific profile programmatically instead of storing the credentials in your project files\. To limit the risk of unintentionally exposing credentials, you should store the SDK Store or credentials file separately from your project files\.
+ Use [IAM Roles for Tasks](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/task-iam-roles.html) for Amazon Elastic Container Service \(Amazon ECS\) tasks\.
+ Use IAM roles for applications that are running on Amazon EC2 instances\.
+ Use temporary credentials or environment variables for applications that are available to users outside your organization\.

The following topics describe how to manage credentials for an AWS SDK for \.NET application\. For a discussion of how to securely manage AWS credentials, see [Best Practices for Managing AWS Access Keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html)\.

## Using the SDK Store<a name="sdk-store"></a>

During development of your AWS SDK for \.NET application, add a profile to the SDK Store for each set of credentials you want to use in your application\. This prevents the accidental exposure of your AWS credentials\. The SDK Store is located in the `C:\Users\<username>\AppData\Local\AWSToolkit` folder in the `RegisteredAccounts.json` file\. The SDK Store provides the following benefits:
+ The SDK Store can contain multiple profiles from any number of accounts\.
+ The credentials in the SDK Store are encrypted, and the SDK Store resides in the user’s home directory\. This limits the risk of accidentally exposing your credentials\.
+ You reference the profile by name in your application and the associated credentials are referenced at run time\. Your source files never contain the credentials\.
+ If you include a profile named `default`, the AWS SDK for \.NET uses that profile\. This is also true if you don’t provide another profile name, or if the specified name isn’t found\.
+ The SDK Store also provides credentials to AWS Tools for Windows PowerShell and the AWS Toolkit for Visual Studio\.

**Note**  
SDK Store profiles are specific to a particular user on a particular host\. They cannot be copied to other hosts or other users\. For this reason, you cannot use SDK Store profiles in production applications\. For more information, see [Credential and Profile Resolution](#creds-assign)\.

You can manage the profiles in the SDK Store in several ways\.
+ Use the graphical user interface \(GUI\) in the AWS Toolkit for Visual Studio to manage profiles\. For more information about adding credentials to the SDK Store by using the GUI, see [Providing AWS Credentials](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/credentials.html) in the AWS Toolkit for Visual Studio\.
+ You can manage your profiles from the command line by using the `Set-AWSCredentials` cmdlet in AWS Tools for Windows PowerShell\. For more information, see [Using AWS Credentials](https://docs.aws.amazon.com/powershell/latest/userguide/specifying-your-aws-credentials.html)\.
+ You can create and manage your profiles programmatically by using the [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) class\.

The following examples show how to create a basic profile and SAML profile and add them to the SDK Store by using the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile.html) method\.

### Create a Profile and Save it to the \.NET Credentials File<a name="create-a-profile-and-save-it-to-the-net-credentials-file"></a>

Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html) object and set its `AccessKey` and `SecretKey` properties\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object\. Provide the name of the profile and the `CredentialProfileOptions` object you created\. Optionally, set the Region property for the profile\. Instantiate a NetSDKCredentialsFile object and call the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile.html) method to register the profile\.

```
var options = new CredentialProfileOptions
{
    AccessKey = "access_key",
    SecretKey = "secret_key"
};
var profile = new Amazon.Runtime.CredentialManagement.CredentialProfile("basic_profile", options);
profile.Region = RegionEndpoint.USWest1;
var netSDKFile = new NetSDKCredentialsFile();
netSDKFile.RegisterProfile(profile);
```

The `RegisterProfile` method is used to register a new profile\. Your application typically calls this method only once for each profile\.

### Create a SAMLEndpoint and an Associated Profile and Save it to the \.NET Credentials File<a name="create-a-samlendpoint-and-an-associated-profile-and-save-it-to-the-net-credentials-file"></a>

Create an [Amazon\.Runtime\.CredentialManagement\.SAMLEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSAMLEndpoint.html) object\. Provide the name and endpoint URI parameters\. Create an [Amazon\.Runtime\.CredentialManagement\.SAMLEndpointManager](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSAMLEndpointManager.html) object\. Call the [RegisterEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MSAMLEndpointManagerRegisterEndpointSAMLEndpoint.html) method to register the endpoint\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html) object and set its `EndpointName` and `RoleArn` properties\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object and provide the name of the profile and the `CredentialProfileOptions` object you created\. Optionally, set the Region property for the profile\. Instantiate a NetSDKCredentialsFile object and call the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile.html) method to register the profile\.

```
var endpoint = new SAMLEndpoint("endpoint1", new Uri("https://some_saml_endpoint"), SAMLAuthenticationType.Kerberos);
var endpointManager = new SAMLEndpointManager();
endpointManager.RegisterEndpoint(endpoint);
options = new CredentialProfileOptions
{
    EndpointName = "endpoint1",
    RoleArn = "arn:aws:iam::999999999999:role/some-role"
};
profile = new CredentialProfile("federated_profile", options);
netSDKFile = new NetSDKCredentialsFile();
netSDKFile.RegisterProfile(profile);
```

## Using a Credentials File<a name="creds-file"></a>

You can also store profiles in your *shared AWS credentials file*\. This file can be used by the other AWS SDKs, the AWS CLI and AWS Tools for Windows PowerShell\. To reduce the risk of accidentally exposing credentials, store the credentials file separately from any project files, usually in the user’s home folder\. *Be aware that the profiles in credentials files are stored in plaintext\.* 

 By default, this file is located in the `.aws` directory within your home directory and is named `credentials`\. For more information, see [Where Are Configuration Settings Stored?](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html#cli-configure-files-where) in the *[AWS Command Line Interface User Guide](https://docs.aws.amazon.com/cli/latest/userguide/)*\.

You can manage the profiles in the shared credentials file in two ways:
+ You can use a text editor\. The file is named `credentials`, and the default location is under your user’s home folder\. For example, if your user name is `awsuser`, the credentials file would be `C:\users\awsuser\.aws\credentials`\.

  The following is an example of a profile in the credentials file\.

  ```
  [{profile_name}]
  aws_access_key_id = {accessKey}
  aws_secret_access_key = {secretKey}
  ```

  For more information, see [Best Practices for Managing AWS Access Keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html)\.
**Note**  
If you include a profile named `default`, the AWS SDK for \.NET uses that profile by default if it can’t find the specified profile\.

  You can store the credentials file that contains the profiles in a location you choose, such as `C:\aws_service_credentials\credentials`\. You then explicitly specify the file path in the `AWSProfilesLocation` attribute in your project’s `App.config` or `Web.config` file\. For more information, see [Specifying a Profile](#net-dg-config-creds-assign-profile)\.
+ You can programmatically manage the credentials file by using the classes in the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace\.

### Setting an Alternative Credentials Profile<a name="setting-an-alternative-credentials-profile"></a>

The AWS SDK for \.NET uses the *default* profile by default, but you can change which profile is used from the credentials file by using the **AWS\_Profile** environment variable\.

For example, on Linux, macOS, or Unix run the following command to change the profile to *myProfile*\.

```
export AWS_PROFILE="myProfile"
```

On Windows use the following command\.

```
set AWS_PROFILE=myProfile
```

Setting the **AWS\_PROFILE** environment variable affects credential loading for all officially supported AWS SDKs and Tools, including the AWS CLI and the AWS CLI for PowerShell\.

**Note**  
The environment variable takes precedence over the system property\.

### Create a Profile and Save it to the Shared Credentials File<a name="create-a-profile-and-save-it-to-the-shared-credentials-file"></a>

Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html) object and set its `AccessKey` and `SecretKey` properties\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object\. Provide the name of the profile and the `CredentialProfileOptions` you created\. Optionally, set the Region property for the profile\. Instantiate an [Amazon\.Runtime\.CredentialManagement\.SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html) object and call the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MSharedCredentialsFileRegisterProfileCredentialProfile.html) method to register the profile\.

```
options = new CredentialProfileOptions
{
    AccessKey = "access_key",
    SecretKey = "secret_key"
};
profile = new CredentialProfile("shared_profile", options);
profile.Region = RegionEndpoint.USWest1;
var sharedFile = new SharedCredentialsFile();
sharedFile.RegisterProfile(profile);
```

The `RegisterProfile` method is used to register a new profile\. Your application will normally call this method only once for each profile\.

### Create a Source Profile and an Associated Assume Role Profile and Save It to the Credentials File<a name="create-a-source-profile-and-an-associated-assume-role-profile-and-save-it-to-the-credentials-file"></a>

Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html) object for the source profile and set its `AccessKey` and `SecretKey` properties\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object\. Provide the name of the profile and the `CredentialProfileOptions` you created\. Instantiate an [Amazon\.Runtime\.CredentialManagement\.SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html) object and call the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile.html) method to register the profile\. Create another [Amazon\.Runtime\.CredentialManagement\.CredentialProfileOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileOptions.html) object for the assumed role profile and set the `SourceProfile` and `RoleArn` properties for the profile\. Create an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object for the assumed role\. Provide the name of the profile and the `CredentialProfileOptions` you created\.

```
// Create the source profile and save it to the shared credentials file
var sourceProfileOptions = new CredentialProfileOptions
{
    AccessKey = "access_key",
    SecretKey = "secret_key"
};
var sourceProfile = new CredentialProfile("source_profile", sourceProfileOptions);
sharedFile = new SharedCredentialsFile();
sharedFile.RegisterProfile(sourceProfile);

// Create the assume role profile and save it to the shared credentials file
var assumeRoleProfileOptions = new CredentialProfileOptions
{
    SourceProfile = "source_profile",
    RoleArn = "arn:aws:iam::999999999999:role/some-role"
};
var assumeRoleProfile = new CredentialProfile("assume_role_profile", assumeRoleProfileOptions);
sharedFile.RegisterProfile(assumeRoleProfile);
```

### Update an Existing Profile in the Shared Credentials File<a name="update-an-existing-profile-in-the-shared-credentials-file"></a>

Create an [Amazon\.Runtime\.CredentialManagement\.SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html) object\. Set the `Region`, `AccessKey` and `SecretKey` properties for the profile\. Call the [TryGetProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MSharedCredentialsFileTryGetProfileStringCredentialProfile.html) method\. If the profile exists, use an [Amazon\.Runtime\.CredentialManagement\.SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html) instance to call the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile.html) method to register the updated profile\.

```
sharedFile = new SharedCredentialsFile();
CredentialProfile basicProfile;
if (sharedFile.TryGetProfile("basicProfile", out basicProfile))
{
    basicProfile.Region = RegionEndpoint.USEast1;
    basicProfile.Options.AccessKey = "different_access_key";
    basicProfile.Options.SecretKey = "different_secret_key";

    sharedFile.RegisterProfile(basicProfile);
}
```

## Accessing Credentials and Profiles in an Application<a name="creds-locate"></a>

You can easily locate credentials and profiles in the \.NET credentials file or in the shared credentials file by using the [Amazon\.Runtime\.CredentialManagement\.CredentialProfileStoreChain](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileStoreChain.html) class\. This is the way the \.NET SDK looks for credentials and profiles\. The `CredentialProfileStoreChain` class automatically checks in both credentials files\.

You can get credentials or profiles by using the [TryGetAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MCredentialProfileStoreChainTryGetAWSCredentialsStringAWSCredentials.html) or [TryGetProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/MCredentialProfileStoreChainTryGetProfileStringCredentialProfile.html) methods\. The `ProfilesLocation` property determines the behavior of the `CredentialsProfileChain`, as follows:

1. If ProfilesLocation is non\-null and non\-empty, search the shared credentials file at the disk path in the `ProfilesLocation` property\.

1. If `ProfilesLocation` is null or empty and the platform supports the \.NET credentials file, search the \.NET credentials file\. If the profile is not found, search the shared credentials file in the default location\.

1. If `ProfilesLocation` is null or empty and the platform doesn’t support the \.NET credentials file, search the shared credentials file in the default location\.

### Get Credentials from the SDK Credentials File or the Shared Credentials File in the Default Location\.<a name="get-credentials-from-the-sdk-credentials-file-or-the-shared-credentials-file-in-the-default-location"></a>

Create a `CredentialProfileStoreChain` object and an [Amazon\.Runtime\.AWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html) object\. Call the `TryGetAWSCredentials` method\. Provide the profile name and the `AWSCredentials` object in which to return the credentials\.

```
var chain = new CredentialProfileStoreChain();
AWSCredentials awsCredentials;
if (chain.TryGetAWSCredentials("basic_profile", out awsCredentials))
{
    // use awsCredentials
}
```

### Get a Profile from the SDK Credentials File or the Shared Credentials File in the Default Location<a name="get-a-profile-from-the-sdk-credentials-file-or-the-shared-credentials-file-in-the-default-location"></a>

Create a `CredentialProfileStoreChain` object and an [Amazon\.Runtime\.CredentialManagement\.CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfile.html) object\. Call the `TryGetProfile` method and provide the profile name and `CredentialProfile` object in which to return the credentials\.

```
var chain = new CredentialProfileStoreChain();
CredentialProfile basicProfile;
if (chain.TryGetProfile("basic_profile", out basicProfile))
{
    // Use basicProfile
}
```

### Get AWSCredentials from a File in the Shared Credentials File Format at a File Location<a name="get-awscredentials-from-a-file-in-the-shared-credentials-file-format-at-a-file-location"></a>

Create a `CredentialProfileStoreChain` object and provide the path to the credentials file\. Create an `AWSCredentials` object\. Call the `TryGetAWSCredentials` method\. Provide the profile name and the `AWSCredentials` object in which to return the credentials\.

```
var chain = new
    CredentialProfileStoreChain("c:\\Users\\sdkuser\\customCredentialsFile.ini");
AWSCredentials awsCredentials;
if (chain.TryGetAWSCredentials("basic_profile", out awsCredentials))
{
    // Use awsCredentials
}
```

### How to Create an AmazonS3Client Using the SharedCredentialsFile Class<a name="how-to-create-an-amazons3client-using-the-sharedcredentialsfile-class"></a>

You can create an [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) object that uses the credentials for a specific profile by using the [Amazon\.Runtime\.CredentialManagement\.SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSharedCredentialsFile.html) class\. The AWS SDK for \.NET loads the credentials contained in the profile automatically\. You might do this if you want to use a specific profile for a given client that is different from the `profile` you specify in `App.Config`\.

```
CredentialProfile basicProfile;
AWSCredentials awsCredentials;
var sharedFile = new SharedCredentialsFile();
if (sharedFile.TryGetProfile("basic_profile", out basicProfile) &&
    AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, sharedFile, out awsCredentials))
{
    using (var client = new AmazonS3Client(awsCredentials, basicProfile.Region))
    {
        var response = client.ListBuckets();
    }
}
```

If you want to use the default profile, and have the AWS SDK for \.NET automatically use your default credentials to create the client object use the following code\.

```
using (var client = new AmazonS3Client(RegionEndpoint.US-West2))
{
    var response = client.ListBuckets();
}
```

## Credential and Profile Resolution<a name="creds-assign"></a>

The AWS SDK for \.NET searches for credentials in the following order and uses the first available set for the current application\.

1. The client configuration, or what is explicitly set on the AWS service client\.

1.  `BasicAWSCredentials` that are created from the `AWSAccessKey` and `AWSSecretKey` `AppConfig` values, if they’re available\.

1. A credentials profile with the name specified by a value in `AWSConfigs.AWSProfileName` \(set explicitly or in `AppConfig`\)\.

1. The `default` credentials profile\.

1.  `SessionAWSCredentials` that are created from the `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, and `AWS_SESSION_TOKEN` environment variables, if they’re all non\-empty\.

1.  `BasicAWSCredentials` that are created from the `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` environment variables, if they’re both non\-empty\.

1. [IAM Roles for Tasks](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/task-iam-roles.html) for Amazon ECS tasks\.

1. EC2 instance metadata\.

SDK Store profiles are specific to a particular user on a particular host\. You can’t copy them to other hosts or other users\. For this reason, you can’t reuse SDK Store profiles that are on your development machine on other hosts or developer machines\. If your application is running on an Amazon EC2 instance, such as in a production environment, use an IAM role as described in [Using IAM Roles for EC2 Instances with the AWS SDK for \.NET](net-dg-hosm.md)\. Otherwise, such as in pre\-release testing, store your credentials in a credentials file that your web application has access to on the server\.

### Profile Resolution<a name="net-dg-config-creds-profile-resolution"></a>

With two different credentials file types, it’s important to understand how to configure the AWS SDK for \.NET and AWS Tools for Windows PowerShell to use them\. The `AWSConfigs.AWSProfilesLocation` \(set explicitly or in `AppConfig`\) controls how the AWS SDK for \.NET finds credential profiles\. The `-ProfileLocation` command line argument controls how AWS Tools for Windows PowerShell finds a profile\. Here’s how the configuration works in both cases\.


****  

| Profile Location Value | Profile Resolution Behavior | 
| --- | --- | 
|  null \(not set\) or empty  |  First search the \.NET credentials file for a profile with the specified name\. If the profile isn’t there, search `%HOME%\.aws\credentials`\. If the profile isn’t there, search `%HOME%\.aws\config`\.  | 
|  The path to a file in the shared credentials file format  |  Search *only* the specified file for a profile with the specified name\.  | 

### Specifying a Profile<a name="net-dg-config-creds-assign-profile"></a>

Profiles are the preferred way to use credentials in an AWS SDK for \.NET application\. You don’t have to specify where the profile is stored\. You only reference the profile by name\. The AWS SDK for \.NET retrieves the corresponding credentials, as described in the previous section\.

The preferred way to specify a profile is to define an `AWSProfileName` value in the `appSettings` section of your application’s `App.config` or `Web.config` file\. The associated credentials are incorporated into the application during the build process\.

The following example specifies a profile named `development`\.

```
<configuration>
  <appSettings>
    <add key="AWSProfileName" value="development"/>
  </appSettings>
</configuration>
```

This example assumes the profile exists in the SDK Store or in a credentials file in the default location\.

If your profiles are stored in a credentials file in another location, specify the location by adding a `AWSProfilesLocation` attribute value in the `<appSettings>` element\. The following example specifies `C:\aws_service_credentials\credentials` as the credentials file\.

```
<configuration>
  <appSettings>
    <add key="AWSProfileName" value="development"/>
    <add key="AWSProfilesLocation" value="C:\aws_service_credentials\credentials"/>
  </appSettings>
</configuration>
```

The deprecated alternative way to specify a profile is shown below for completeness, but we do not recommend it\.

```
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
  </configSections>
  <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
</configuration>

<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection,AWSSDK.Core"/>
  </configSections>
  <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
</configuration>
```

### Using Federated User Account Credentials<a name="net-dg-config-creds-saml"></a>

Applications that use the AWS SDK for \.NET \(`AWSSDK.Core` version 3\.1\.6\.0 and later\) can use federated user accounts through Active Directory Federation Services \(AD FS\) to access AWS web services by using Security Assertion Markup Language \(SAML\)\.

Federated access support means users can authenticate using your Active Directory\. Temporary credentials are granted to the user automatically\. These temporary credentials, which are valid for one hour, are used when your application invokes AWS web services\. The SDK handles management of the temporary credentials\. For domain\-joined user accounts, if your application makes a call but the credentials have expired, the user is reauthenticated automatically and fresh credentials are granted\. \(For non\-domain\-joined accounts, the user is prompted to enter credentials before reauthentication\.\)

To use this support in your \.NET application, you must first set up the role profile by using a PowerShell cmdlet\. To learn how, see the [AWS Tools for Windows PowerShell documentation](https://docs.aws.amazon.com/powershell/latest/userguide/saml-pst.html)\.

After you setup the role profile, reference the profile in your application’s app\.config/web\.config file with the `AWSProfileName` key in the same way you would with other credential profiles\.

The SDK Security Token Service assembly \(`AWSSDK.SecurityToken.dll`\), which is loaded at runtime, provides the SAML support to obtain AWS credentials\. Be sure this assembly is available to your application at run time\.

### Specifying Roles or Temporary Credentials<a name="net-dg-config-creds-assign-role"></a>

For applications that run on Amazon EC2 instances, the most secure way to manage credentials is to use IAM roles, as described in [Using IAM Roles for EC2 Instances with the AWS SDK for \.NET](net-dg-hosm.md)\.

For application scenarios in which the software executable is available to users outside your organization, we recommend you design the software to use *temporary security credentials*\. In addition to providing restricted access to AWS resources, these credentials have the benefit of expiring after a specified period of time\. For more information about temporary security credentials, see the following:
+  [Using Security Tokens to Grant Temporary Access to Your AWS Resources](https://docs.aws.amazon.com/IAM/latest/UserGuide/TokenBasedAuth.html) 
+  [Authenticating Users of AWS Mobile Applications with a Token Vending Machine](https://aws.amazon.com/articles/4611615499399490)\.

Although the title of the second article refers specifically to mobile applications, the article contains information that is useful for any AWS application deployed outside of your organization\.

### Using Proxy Credentials<a name="net-dg-config-creds-proxy"></a>

If your software communicates with AWS through a proxy, you can specify credentials for the proxy by using the `ProxyCredentials` property on the [AmazonS3Config](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Config.html) class for the service\. For example, for Amazon S3 you could use code similar to the following, where \{my\-username\} and \{my\-password\} are the proxy user name and password specified in a [NetworkCredential](https://docs.microsoft.com/en-us/dotnet/api/system.net.networkcredential) object\.

```
AmazonS3Config config = new AmazonS3Config();
config.ProxyCredentials = new NetworkCredential("my-username", "my-password");
```

Earlier versions of the SDK used `ProxyUsername` and `ProxyPassword`, but these properties are deprecated\.