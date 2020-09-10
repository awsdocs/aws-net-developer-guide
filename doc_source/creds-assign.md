--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Credential and profile resolution<a name="creds-assign"></a>

The AWS SDK for \.NET searches for credentials in a certain order and uses the first available set for the current application\.

**Note**  
The information in this topic regarding profile and credential precedence is centered around \.NET Core and ASP\.NET Core, and is different from similar information for \.NET Framework\.  
In addition, information about specifying profiles using \.NET Framework mechanisms such as `App.config` and `Web.config` doesn't apply to \.NET Core and so isn't present\.  
If you're looking for \.NET Framework information, see [version 3](../../v3/developer-guide/net-dg-config-creds.html#creds-assign) of the guide instead\.

**Credential search order**

1. Credentials that are explicitly set on the AWS service client, as described in [Accessing credentials and profiles in an application](creds-locate.md)\.
**Note**  
That topic is in the [Special considerations](special-considerations.md) section because it isn't the preferred method for specifying credentials\.

1. A credentials profile with the name specified by a value in [AWSConfigs\.AWSProfileName](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html)\.

1. A credentials profile with the name specified by the `AWS_PROFILE` environment variable\.

1. The `[default]` credentials profile\.

1. [SessionAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSessionAWSCredentials.html) that are created from the `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, and `AWS_SESSION_TOKEN` environment variables, if they're all non\-empty\.

1.  [BasicAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TBasicAWSCredentials.html) that are created from the `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` environment variables, if they're both non\-empty\.

1. [IAM Roles for Tasks](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/task-iam-roles.html) for Amazon ECS tasks\.

1. Amazon EC2 instance metadata\.

If your application is running on an Amazon EC2 instance, such as in a production environment, use an IAM role as described in [Granting access by using an AWS IAM role](net-dg-hosm.md)\. Otherwise, such as in prerelease testing, store your credentials in a file that uses the AWS credentials file format that your web application has access to on the server\.

## Profile resolution<a name="net-dg-config-creds-profile-resolution"></a>

With two different storage mechanisms for credentials, it's important to understand how to configure the AWS SDK for \.NET to use them\. The [AWSConfigs\.AWSProfilesLocation](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property controls how the AWS SDK for \.NET finds credential profiles\.


****  

| AWSProfilesLocation | Profile resolution behavior | 
| --- | --- | 
|  null \(not set\) or empty  |  Search the SDK Store if the platform supports it, and then search the shared AWS credentials file in the [default location](creds-file.md)\. If the profile isn't in either of those locations, search `~/.aws/config` \(Linux or macOS\) or `%USERPROFILE%\.aws\config` \(Windows\)\.  | 
|  The path to a file in the AWS credentials file format  |  Search *only* the specified file for a profile with the specified name\.  | 

## Using federated user account credentials<a name="net-dg-config-creds-saml"></a>

Applications that use the AWS SDK for \.NET \([AWSSDK\.Core](https://www.nuget.org/packages/AWSSDK.Core/) version 3\.1\.6\.0 and later\) can use federated user accounts through Active Directory Federation Services \(AD FS\) to access AWS services by using Security Assertion Markup Language \(SAML\)\.

Federated access support means users can authenticate using your Active Directory\. Temporary credentials are granted to the user automatically\. These temporary credentials, which are valid for one hour, are used when your application invokes AWS services\. The SDK handles management of the temporary credentials\. For domain\-joined user accounts, if your application makes a call but the credentials have expired, the user is reauthenticated automatically and fresh credentials are granted\. \(For non\-domain\-joined accounts, the user is prompted to enter credentials before reauthentication\.\)

To use this support in your \.NET application, you must first set up the role profile by using a PowerShell cmdlet\. To learn how, see the [AWS Tools for Windows PowerShell documentation](https://docs.aws.amazon.com/powershell/latest/userguide/saml-pst.html)\.

After you set up the role profile, reference the profile in your application\. There are a number of ways to do this, one of which is by using the [AWSConfigs\.AWSProfileName](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property in the same way you would with other credential profiles\.

The *AWS Security Token Service* assembly \([AWSSDK\.SecurityToken](https://www.nuget.org/packages/AWSSDK.SecurityToken/)\) provides the SAML support to obtain AWS credentials\. To use federated user account credentials, be sure this assembly is available to your application\.

## Specifying roles or temporary credentials<a name="net-dg-config-creds-assign-role"></a>

For applications that run on Amazon EC2 instances, the most secure way to manage credentials is to use IAM roles, as described in [Granting access by using an AWS IAM role](net-dg-hosm.md)\.

For application scenarios in which the software executable is available to users outside your organization, we recommend that you design the software to use *temporary security credentials*\. In addition to providing restricted access to AWS resources, these credentials have the benefit of expiring after a specified period of time\. For more information about temporary security credentials, see the following:
+  [Temporary security credentials](https://docs.aws.amazon.com/IAM/latest/UserGuide/TokenBasedAuth.html) 
+  [Amazon Cognito identity pools](https://docs.aws.amazon.com/cognito/latest/developerguide/cognito-identity.html)

## Using proxy credentials<a name="net-dg-config-creds-proxy"></a>

If your software communicates with AWS through a proxy, you can specify credentials for the proxy by using the `ProxyCredentials` property of the `Config` class of a service\. The `Config` class of a service is typically part of the primary namespace for the service\. Examples include the following: [AmazonCloudDirectoryConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudDirectory/TCloudDirectoryConfig.html) in the [Amazon\.CloudDirectory](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudDirectory/NCloudDirectory.html) namespace and [AmazonGameLiftConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/GameLift/TGameLiftConfig.html) in the [Amazon\.GameLift](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/GameLift/NGameLift.html) namespace\.

For [Amazon S3](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Config.html), for example, you could use code similar to the following, where \{my\-username\} and \{my\-password\} are the proxy user name and password specified in a [NetworkCredential](https://docs.microsoft.com/en-us/dotnet/api/system.net.networkcredential) object\.

```
AmazonS3Config config = new AmazonS3Config();
config.ProxyCredentials = new NetworkCredential("my-username", "my-password");
```

**Note**  
Earlier versions of the SDK used `ProxyUsername` and `ProxyPassword`, but these properties are deprecated\.