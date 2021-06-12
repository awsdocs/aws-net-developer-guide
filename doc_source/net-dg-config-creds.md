--------

End of support announcement: [http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/](http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

This documentation is for version 2\.0 of the AWS SDK for \.NET\.** For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/) of the AWS SDK for \.NET developer guide instead\.**

--------

# Configuring AWS Credentials<a name="net-dg-config-creds"></a>

## Version 2 content \(see announcement above\)<a name="w3aac11b7b7b3b1"></a>

This topic describes how to configure your application’s AWS credentials\. It assumes you have created an AWS account and have access to your credentials, as described in [Create an AWS Account and Credentials](net-dg-setup.md#net-dg-signup)\. It is important to manage your credentials securely and avoid practices that could unintentionally expose your credentials publicly\. In particular:
+ Don’t use your account’s root credentials to access your AWS resources\. These credentials provide unrestricted account access and are difficult to revoke\.
+ Don’t put literal access keys in your application, including the project’s `App.config` or `Web.config` file\. Doing so creates a risk of accidentally exposing your credentials if, for example, you upload the project to a public repository\.

Some general guidelines for securely managing credentials include:
+ Create IAM users and use the credentials for the IAM users instead of your account’s root credentials to provide account access\. IAM user credentials are easier to revoke if they are compromised\. You can apply to each IAM user a policy that restricts the user to a specified set of resources and actions\.
+ The preferred approach for managing credentials during application development is to put a profile for each set of IAM user credentials in the SDK Store\. You can also use a credentials file to store profiles that contain credentials\. You can then reference a particular profile programmatically or in your application’s `App.config` or `Web.config` file instead of storing the credentials in your project files\. To limit the risk of unintentionally exposing credentials, the SDK Store or credentials file should be stored separately from your project files\.
+ Use IAM roles for applications that are running on Amazon EC2 instances\.
+ Use temporary credentials for applications that are available to users outside your organization\.

The following topics describe how to manage credentials for an AWS SDK for \.NET application\. For a general discussion of how to securely manage AWS credentials, see [Best Practices for Managing AWS Access Keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html)\.

**Topics**
+ [Using the SDK Store](#sdk-store)
+ [Using a Credentials File](#creds-file)
+ [Using Credentials in an Application](#creds-assign)

### Using the SDK Store<a name="sdk-store"></a>

During development of your AWS SDK for \.NET application, you should add a profile to the SDK Store for each set of credentials you want to use in your application\. This will prevent the accidental exposure of your AWS credentials while developing your application\. The SDK Store provides the following benefits:
+ The SDK Store can contain multiple profiles from any number of accounts\.
+ The credentials in the SDK Store are encrypted, and the SDK Store resides in the user’s home directory, which limits the risk of accidentally exposing your credentials\.
+ You reference the profile by name in your application and the associated credentials are incorporated at build time\. Your source files never contain the credentials\.
+ If you include a profile named `default`, the AWS SDK for \.NET will use that profile by default\.
+ The SDK Store also provides credentials to the [AWS Tools for PowerShell User Guide](https://docs.aws.amazon.com/powershell/latest/userguide/)\.

SDK Store profiles are specific to a particular user on a particular host\. They cannot be copied to other hosts or other users\. For this reason, SDK Store profiles cannot be used in production applications\. For more information, see [Using Credentials in an Application](#creds-assign)\.

There are several ways to manage the profiles in the SDK Store\.
+ The Toolkit for Visual Studio includes a graphical user interface for managing profiles\. For more information about adding credentials to the SDK Store with the graphical user interface, see [Specifying Credentials](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/tkv_setup.html) in the AWS Toolkit for Visual Studio User Guide\.
+ You can manage your profiles from the command line by using the AWS Tools for Windows PowerShell\. For more information, see [Using AWS Credentials](https://docs.aws.amazon.com/powershell/latest/userguide/specifying-your-aws-credentials.html) in the AWS Tools for Windows PowerShell User Guide\.
+ You can manage your profiles programmatically using the [Amazon\.Util\.ProfileManager](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TUtilProfileManagerNET45.html) class\. The following example uses the [RegisterProfile](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MUtilProfileManagerRegisterProfileStringStringStringNET45.html) method to add a new profile to the SDK Store\.

  ```
  Amazon.Util.ProfileManager.RegisterProfile({profileName}, {accessKey}, {secretKey})
  ```

  The `RegisterProfile` method is used to register a new profile\. Your application will normally call this method only once for each profile\.

### Using a Credentials File<a name="creds-file"></a>

You can also store profiles in a credentials file, which can be used by the other AWS SDKs, the AWS CLI, and Tools for Windows PowerShell\. To reduce the risk of accidentally exposing credentials, the credentials file should be stored separately from any project files, usually in the user’s home folder\. Be aware that the profiles in a credentials files are stored in plaintext\.

You use a text editor to manage the profiles in a credentials file\. The file is named `credentials`, and the default location is under your user’s home folder\. For example, if your user name is `awsuser`, the credentials file would be `C:\users\awsuser\.aws\credentials`\.

Each profile has the following format:

```
[{profile_name}]
aws_access_key_id = {accessKey}
aws_secret_access_key = {secretKey}
```

A profile can optionally include a session token\. For more information, see [Best Practices for Managing AWS Access Keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html)\.

If federated account information is used for access, the credential file must include the session token\. Otherwise the SDK returns an Invalid Session Token exception\. An example profile with a session token:

```
[{profile_name}]
aws_access_key_id = {accessKey}
aws_secret_access_key = {secretKey}
aws_session_token = {sessionToken}
```

**Note**  
If you include a profile named `default`, the AWS SDK for \.NET will use that profile by default if it cannot find the specified profile\.

You can store profiles in a credentials file in a location you choose, such as `C:\aws_service_credentials\credentials`\. You then explicitly specify the file path in the `profilesLocation` attribute in your project’s `App.config` or `Web.config` file\. For more information, see [Specifying a Profile](#net-dg-config-creds-assign-profile)\.

### Using Credentials in an Application<a name="creds-assign"></a>

The AWS SDK for \.NET searches for credentials in the following order and uses the first available set for the current application\.

1. Access key and secret key values that are stored in the application’s `App.config` or `Web.config` file\. We strongly recommend using profiles rather than storing literal credentials in your project files\.

1. If a profile is specified:

   1. The specified profile in the SDK Store\.

   1. The specified profile in the credentials file\.

   If no profile is specified:

   1. A profile named `default` in the SDK Store\.

   1. A profile named `default` in the credentials file\.

1. Credentials stored in the `AWS_ACCESS_KEY_ID` and `AWS_SECRET_KEY` environment variables\.

1. For applications running on an Amazon EC2 instance, credentials stored in an instance profile\.

SDK Store profiles are specific to a particular user on a particular host\. They cannot be copied to other hosts or other users\. For this reason, SDK Store profiles cannot be used in production applications\. If your application is running on an Amazon EC2 instance, you should use an IAM role as described in [Using IAM Roles for EC2 Instances with the AWS SDK for \.NET](net-dg-hosm.md)\. Otherwise, you should store your credentials in a credentials file on the server your web application has access to\.

#### Specifying a Profile<a name="net-dg-config-creds-assign-profile"></a>

Profiles are the preferred way to use credentials in an AWS SDK for \.NET application\. You don’t have to specify where the profile is stored; you only reference the profile by name\. The AWS SDK for \.NET retrieves the corresponding credentials, as described in the previous section\.

The recommended way to specify a profile is to define an `<aws>` element in your application’s `App.config` or `Web.config` file\. The associated credentials are incorporated into the application during the build process\.

The following example specifies a profile named `development`\.

```
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
  </configSections>
  <aws profileName="development"/>
</configuration>
```

**Note**  
The `<configSections>` element must be the first child of the `<configuration>` element\.

Another way to specify a profile is to define an `AWSProfileName` value in the `appSettings` section of your application’s `App.config` or `Web.config` file\. The associated credentials are incorporated into the application during the build process\.

The following example specifies a profile named `development`\.

```
<configuration>
  <appSettings>
    <add key="AWSProfileName" value="development"/>
  </appSettings>
</configuration>
```

This example assumes the profile exists in the SDK Store or a credentials file in the default location\. If your profiles are stored in a credentials file in another location, specify the location by adding a `profilesLocation` attribute value to the `<aws>` element\. The following example specifies `C:aws_service_credentialscredentials` as the credentials file by using the recommended `<aws>` element\.

```
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
  </configSections>
  <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
</configuration>
```

Another way to specify a credentials file is with the `<appSettings>` element\.

```
<configuration>
  <appSettings>
    <add key="AWSProfileName" value="development"/>
    <add key="AWSProfilesLocation" value="C:\aws_service_credentials\credentials"/>
  </appSettings>
</configuration>
```

Although you can reference a profile programmatically using the [Amazon\.Runtime\.StoredProfileAWSCredentials](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRuntimeStoredProfileAWSCredentialsNET45.html) class, we recommend that you use the `aws` element instead\. The following example demonstrates how to create an `AmazonS3Client` object that uses the credentials for a specific profile\.

```
var credentials = new StoredProfileAWSCredentials(profileName);
var s3Client = new AmazonS3Client(credentials, RegionEndpoint.USWest2);
```

**Note**  
If you want to use the default profile, omit the `AWSCredentials` object, and the AWS SDK for \.NET will automatically use your default credentials to create the client object\.

#### Specifying Roles or Temporary Credentials<a name="net-dg-config-creds-assign-role"></a>

For applications that run on Amazon EC2 instances, the most secure way to manage credentials is to use IAM roles, as described in [Using IAM Roles for EC2 Instances with the AWS SDK for \.NET](net-dg-hosm.md)\.

For application scenarios in which the software executable will be available to users outside your organization, we recommend you design the software to use *temporary security credentials*\. In addition to providing restricted access to AWS resources, these credentials have the benefit of expiring after a specified period of time\. For more information about temporary security credentials, go to:
+  [Using Security Tokens to Grant Temporary Access to Your AWS Resources](https://docs.aws.amazon.com/IAM/latest/UserGuide/TokenBasedAuth.html) 
+  [Authenticating Users of AWS Mobile Applications with a Token Vending Machine](https://aws.amazon.com/articles/4611615499399490)\.

Although the title of the second article refers specifically to mobile applications, the article contains information that is useful for any AWS application deployed outside of your organization\.

#### Using Proxy Credentials<a name="net-dg-config-creds-proxy"></a>

If your software communicates with AWS through a proxy, you can specify credentials for the proxy using the `ProxyCredentials` property on the [ClientConfig](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRuntimeClientConfigNET45.html) class for the service\. For example, for Amazon S3, you could use code similar to the following, where \{my\-username\} and \{my\-password\} are the proxy user name and password specified in a [NetworkCredential](http://msdn.microsoft.com/en-us/library/system.net.networkcredential.aspx) object\.

```
AmazonS3Config config = new AmazonS3Config();
config.ProxyCredentials = new NetworkCredential("my-username", "my-password");
```

Earlier versions of the SDK used `ProxyUsername` and `ProxyPassword`, but these properties have been deprecated\.