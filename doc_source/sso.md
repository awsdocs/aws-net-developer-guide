# Single sign\-on \(SSO\) with the AWS SDK for \.NET<a name="sso"></a>

AWS IAM Identity Center \(successor to AWS Single Sign\-On\) is a cloud\-based single sign\-on \(SSO\) service that makes it easy to centrally manage SSO access to all of your AWS accounts and cloud applications\. For full details, see the [IAM Identity Center User Guide](https://docs.aws.amazon.com/singlesignon/latest/userguide/)\.

If you're unfamiliar with how an SDK interacts with IAM Identity Center, see the following information\.

## High\-level pattern of interaction<a name="w359aac15b7b7b1"></a>

At a high level, SDKs interact with IAM Identity Center in a manner similar to the following pattern:

1. IAM Identity Center is configured, typically through the [IAM Identity Center console](https://console.aws.amazon.com/singlesignon), and an SSO user is invited to participate\.

1. The shared AWS `config` file on the user's computer is updated with SSO information\.

1. The user signs in through IAM Identity Center and is given short\-term credentials for the AWS Identity and Access Management \(IAM\) permissions that have been configured for them\. This sign\-in can be initiated through a non\-SDK tool like the AWS CLI, or programmatically through a \.NET application\.

1. The user proceeds to do their work\. When they run other applications that are using SSO, they don't need to sign in again to open the applications\.

**Topics**
+ [Prerequisites](#sso-prereq)
+ [Setting up an SSO profile](#sso-profiles)
+ [Generating and using SSO tokens](#sso-generate-use-token-overview)
+ [Additional resources](#sso-resources)
+ [Tutorials](#sso-tutorial-links)

## Prerequisites<a name="sso-prereq"></a>

Before using IAM Identity Center, you must perform certain tasks, such as choosing an identity source and configuring the relevant AWS accounts and applications\. For additional information, see the following:
+ For general information about these tasks, see [Getting started](https://docs.aws.amazon.com/singlesignon/latest/userguide/getting-started.html) in the *IAM Identity Center User Guide*\.
+ For specific task examples, see the list of tutorials at the end of this topic\. However, be sure to review the information in this topic before trying out the tutorials\.

## Setting up an SSO profile<a name="sso-profiles"></a>

After IAM Identity Center is [configured](https://docs.aws.amazon.com/singlesignon/latest/userguide/getting-started.html) in the relevant AWS account, a named profile for SSO must be added to the user's shared AWS `config` file\. This profile is used to connect to the *[AWS access portal](https://docs.aws.amazon.com/singlesignon/latest/userguide/using-the-portal.html)*, which returns short\-term credentials for the IAM permissions that have been configured for the user\.

The shared `config` file is typically named `%USERPROFILE%\.aws\config` in Windows and `~/.aws/config` in Linux and macOS\. You can use your preferred text editor to add a new profile for SSO\. Alternatively, you can use the `aws configure sso` command\. For more information about this command, see [Configuring the AWS CLI to use IAM Identity Center](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-sso.html) in the *AWS Command Line Interface User Guide*\.

The new profile is similar to the following:

```
[profile my-sso-profile]
sso_start_url = https://my-sso-portal.awsapps.com/start
sso_region = us-west-2
sso_account_id = 123456789012
sso_role_name = SSOReadOnlyRole
```

The settings for the new profile are defined below\. The first two settings define the AWS access portal\. The other two settings are a pair that, taken together, define the permissions that have been configured for a user\. All four settings are required\.

**`sso_start_url`**  
The URL that points to the organization's [AWS access portal](https://docs.aws.amazon.com/singlesignon/latest/userguide/using-the-portal.html)\. To find this value, open the [IAM Identity Center console](https://console.aws.amazon.com/singlesignon), choose **Settings**, and find **portal URL**\.

**`sso_region`**  
The AWS Region that contains the access portal host\. This is the Region that was selected as you enabled IAM Identity Center\. It can be different from the Regions that you use for other tasks\.  
For a complete list of the AWS Regions and their codes, see [Regional Endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html#regional-endpoints) in the *Amazon Web Services General Reference*\.

**`sso_account_id`**  
The ID of an AWS account that was added through the AWS Organizations service\. To see the list of available accounts, go to the [IAM Identity Center console](https://console.aws.amazon.com/singlesignon) and open the **AWS accounts** page\. The account ID that you choose for this setting will correspond to the value that you plan to give to the `sso_role_name` setting, which is shown next\.

**`sso_role_name`**  
The name of an IAM Identity Center permission set\. This permission set defines the permissions that a user is given through IAM Identity Center\.  
The following procedure is one way to find the value for this setting\.  

1. Go to the [IAM Identity Center console](https://console.aws.amazon.com/singlesignon) and open the **AWS accounts** page\.

1. Choose an account to display its details\. The account you choose will be the one that contains the SSO user or group that you want to give SSO permissions to\.

1. Look at the list of users and groups that are assigned to the account and find the user or group of interest\. The permission set that you specify in the `sso_role_name` setting is one of the sets associated with this user or group\.
When giving a value to this setting, use the name of the permission set, not the Amazon Resource Name \(ARN\)\.  
Permission sets have IAM policies and custom\-permissions policies attached to them\. For more information, see [Permission sets](https://docs.aws.amazon.com/singlesignon/latest/userguide/permissionsets.html) in the *IAM Identity Center User Guide*\.

## Generating and using SSO tokens<a name="sso-generate-use-token-overview"></a>

To use SSO, a user must first generate a temporary token and then use that token to access appropriate AWS applications and resources\. For \.NET applications, you can use the following methods to generate and use these temporary tokens:
+ Generate a token with the AWS CLI and then use the token in \.NET applications\.
+ Create \.NET applications that generate a token first, if necessary, and then use the token\.

These methods are described in the following sections and demonstrated in the [tutorials](#sso-tutorial-links)\.

### AWS CLI and \.NET application<a name="sso-generate-use-token-cli-and-app-summary"></a>

This section shows you how to generate a temporary SSO token by using the AWS CLI, and how to use that token in an application\. For a full tutorial of this process, see [Tutorial for SSO using the AWS CLI and \.NET applications](sso-tutorial-cli-and-app.md)\.

#### Generate an SSO token by using the AWS CLI<a name="sso-generate-token-cli"></a>

The following information shows you how to use the AWS CLI to generate a temporary token for later use\.

After the user creates an SSO\-enabled profile as shown in a [previous section](#sso-profiles), they run the `aws sso login` command from the AWS CLI\. They must be sure to include the `--profile` parameter with the name of the SSO\-enabled profile\. This is shown in the following example:

```
aws sso login --profile my-sso-profile
```

If the user wants to generate a new temporary token after the current one expires, they can run the same command again\.

#### Use the generated SSO token in a \.NET application<a name="sso-use-profile-dotnet"></a>

The following information shows you how to use a temporary token that has already been generated\.

**Important**  
Your application must reference the following NuGet packages so that SSO resolution can work:  
`AWSSDK.SSO`
`AWSSDK.SSOOIDC`
Failure to reference these packages will result in a *runtime* exception\.

Your application creates an [https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html) object for the SSO profile, which loads the temporary credentials generated earlier by the AWS CLI\. This is similar to the methods shown in [Accessing credentials and profiles in an application](creds-locate.md) and has the following form:

```
static AWSCredentials LoadSsoCredentials()
{
    var chain = new CredentialProfileStoreChain();
    if (!chain.TryGetAWSCredentials("my-sso-profile", out var credentials))
        throw new Exception("Failed to find the my-sso-profile profile");

    return credentials;
}
```

The `AWSCredentials` object is then passed to the constructor for a service client\. For example:

```
var S3Client_SSO = new AmazonS3Client(LoadSsoCredentials());
```

**Note**  
Using `AWSCredentials` to load temporary credentials isn't necessary if your application has been built to use the `[default]` profile for SSO\. In that case, the application can create AWS service clients without parameters, similar to "`var client = new AmazonS3Client();`"\.

[Tutorial for SSO using the AWS CLI and \.NET applications](sso-tutorial-cli-and-app.md)

### \.NET application only<a name="sso-generate-use-token-app-only-summary"></a>

This section shows you how to create a \.NET application that generates a temporary SSO token, if necessary, and then uses that token\. For a full tutorial of this process, see [Tutorial for SSO using only \.NET applications](sso-tutorial-app-only.md)\.

#### Generate and use an SSO token programmatically<a name="sso-generate-token-prog"></a>

In addition to using the AWS CLI, you can also generate an SSO token programmatically\.

To do this, your application creates an [https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html) object for the SSO profile, which loads temporary credentials if any are available\. Then, your application must cast the `AWSCredentials` object to an [https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSSOAWSCredentials.html](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSSOAWSCredentials.html) object and set some [Options](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSSOAWSCredentialsOptions.html) properties, including a callback method that is used to prompt the user for sign\-in information, if necessary\.

This method is shown in the following code example:

```
static AWSCredentials LoadSsoCredentials()
{
    var chain = new CredentialProfileStoreChain();
    if (!chain.TryGetAWSCredentials("my-sso-profile", out var credentials))
        throw new Exception("Failed to find the my-sso-profile profile");

    var ssoCredentials = credentials as SSOAWSCredentials;

    ssoCredentials.Options.ClientName = "Example-SSO-App";
    ssoCredentials.Options.SsoVerificationCallback = args =>
    {
        // Launch a browser window that prompts the SSO user to complete an SSO sign-in.
        // This method is only invoked if the session doesn't already have a valid SSO token.
        // NOTE: Process.Start might not support launching a browser on macOS or Linux. If not,
        //       use an appropriate mechanism on those systems instead.
        Process.Start(new ProcessStartInfo
        {
            FileName = args.VerificationUriComplete,
            UseShellExecute = true
        });
    };

    return ssoCredentials;
}
```

If an appropriate SSO token isn't available, the default browser window is launched and the appropriate sign\-in page is opened\. For example, if you’re using IAM Identity Center as the **Identity source**, the user sees a sign\-in page similar to the following:

![\[AWS IAM Identity Center (successor to AWS Single Sign-On) sign-in page.\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SSO-login.png)

**Note**  
The text string that you provide for `SSOAWSCredentials.Options.ClientName` can't have spaces\. If the string does have spaces, you'll get a *runtime* exception\.

[Tutorial for SSO using only \.NET applications](sso-tutorial-app-only.md)

## Additional resources<a name="sso-resources"></a>

For additional help, see the following resources\.
+ [What is IAM Identity Center?](https://docs.aws.amazon.com/singlesignon/latest/userguide/what-is.html)
+ [Configuring the AWS CLI to use IAM Identity Center](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-sso.html#sso-configure-profile)
+ [Using IAM Identity Center credentials in the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/sso-credentials.html)

## Tutorials<a name="sso-tutorial-links"></a>

**Topics**
+ [Prerequisites](#sso-prereq)
+ [Setting up an SSO profile](#sso-profiles)
+ [Generating and using SSO tokens](#sso-generate-use-token-overview)
+ [Additional resources](#sso-resources)
+ [Tutorials](#sso-tutorial-links)
+ [Tutorial: AWS CLI and \.NET application](sso-tutorial-cli-and-app.md)
+ [Tutorial: \.NET application only](sso-tutorial-app-only.md)