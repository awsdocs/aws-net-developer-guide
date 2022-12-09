# Tutorial for SSO using only \.NET applications<a name="sso-tutorial-app-only"></a>

This tutorial shows you how to enable SSO for a basic application and a test SSO user\. It configures the application to generate a temporary SSO token programmatically instead of [using the AWS CLI](sso-tutorial-cli-and-app.md)\.

Before you start this tutorial, see the [background information](sso.md) for using IAM Identity Center with the AWS SDK for \.NET\. Also see the high\-level description for this scenario in the subsection called [\.NET application only](sso.md#sso-generate-use-token-app-only-summary)\.

**Note**  
Several of the steps in this tutorial help you configure services like AWS Organizations and IAM Identity Center\. If you've already performed that configuration, or if you're only interested in the code, you can skip to the section with the [example code](#sso-tutorial-app-only-code)\.

## Prerequisites<a name="sso-tutorial-app-only-prereq"></a>
+ Configure your development environment if you haven't already done so\. This is described in sections like [Install and configure your toolchain](net-dg-dev-env.md) and [Setting up your project](net-dg-config.md)\.
+ Identify or create at least one AWS account that you can use to test SSO\. For the purposes of this tutorial, this is called the *test AWS account* or simply *test account*\.
+ Identify an *SSO user* who can test SSO for you\. This is a person who will be using SSO and the basic applications that you create\. For this tutorial, that person might be you \(the developer\), or someone else\. We also recommend a setup in which the SSO user is working on a computer that is not in your development environment\. However, this isn't strictly necessary\.
+ The SSO user's computer must have a \.NET framework installed that's compatible with the one you used to set up your development environment\.

## Set up AWS<a name="sso-tutorial-app-only-setup-aws"></a>

This section shows you how to set up various AWS services for this tutorial\.

To perform this setup, first sign in to the test AWS account as an administrator\. Then, do the following:

### Amazon S3<a name="w2aac15b7c35c15b3b5"></a>

Go to the [Amazon S3 console](https://console.aws.amazon.com/s3/home) and add some innocuous buckets\. Later in this tutorial, the SSO user will retrieve a list of these buckets\.

### AWS IAM<a name="w2aac15b7c35c15b3b7"></a>

Go to the [IAM console](https://console.aws.amazon.com/iam/home#/users) and add a few IAM users\. If you give the IAM users permissions, limit the permissions to a few innocuous read\-only permissions\. Later in this tutorial, the SSO user will retrieve a list of these IAM users\.

### AWS Organizations<a name="w2aac15b7c35c15b3b9"></a>

Go to the [AWS Organizations console](https://console.aws.amazon.com/organizations/) and enable Organizations\. For more information, see [Creating an organization](https://docs.aws.amazon.com/organizations/latest/userguide/orgs_manage_org_create.html) in the [AWS Organizations User Guide](https://docs.aws.amazon.com/organizations/latest/userguide/)\.

This action adds the test AWS account to the organization as the *management account*\. If you have additional test accounts, you can invite them to join the organization, but doing so isn't necessary for this tutorial\.

### IAM Identity Center<a name="w2aac15b7c35c15b3c11"></a>

Go to the [IAM Identity Center console](https://console.aws.amazon.com/singlesignon/) and enable SSO\. Perform email verification if necessary\. For more information, see [Enable IAM Identity Center](https://docs.aws.amazon.com/singlesignon/latest/userguide/step1.html) in the [IAM Identity Center User Guide](https://docs.aws.amazon.com/singlesignon/latest/userguide/)\.

Then, perform the following configuration\.

#### Configure IAM Identity Center<a name="w2aac15b7c35c15b3c11b7b1"></a>

1. Go to the **Settings** page\. Look for the **"User portal URL"** and record the value for later use in the `sso_start_url` setting\.

1. In the banner of the AWS Management Console, look for the AWS Region that was set when you enabled SSO\. This is the dropdown menu to the left of the AWS account ID\. Record the Region code for later use in the `sso_region` setting\. This code will be similar to `us-east-1`\.

1. Create an SSO user as follows:

   1. Go to the **Users** page\.

   1. Choose **Add user** and enter the user's **Username**, **Email address**, **First name**, and **Last name**\. Then, choose **Next**\.

   1. Choose **Next** on the page for groups, then review the information and choose **Add user**\.

1. Create a group as follows:

   1. Go to the **Groups** page\.

   1. Choose **Create group** and enter the group's **Group name** and **Description**\.

   1. In the **Add users to group** section, select the test SSO user that you created earlier\. Then, select **Create group\.**

1. Create a permission set as follows:

   1. Go to the **Permission sets** page and choose **Create permission set**\.

   1. Select **Create a custom permission set** and choose **Next: Details**\.

   1. For this tutorial, enter `SSOReadOnlyRole` for the **Name** and enter a **Description**\.

   1. Select **Create a custom permissions policy** and enter the following policy:

      ```
      {
          "Version": "2012-10-17",
          "Statement": [
              {
                  "Sid": "VisualEditor0",
                  "Effect": "Allow",
                  "Action": [
                      "s3:ListAllMyBuckets",
                      "iam:ListUsers"
                  ],
                  "Resource": "*"
              }
          ]
      }
      ```

   1. Choose **Next: Tags**, **Next: Review**, and **Create**\.

   1. Record the name of the permission set for later use in the `sso_role_name` setting\.

1. Go to the **AWS accounts** page and choose the AWS account that you added to the organization earlier\.

1. In the **Overview** section of that page, find the **Account ID** and record it for later use in the `sso_account_id` setting\.

1. Choose the **Users and groups** tab and then choose **Assign users or groups**\.

1. On the **Assign users and groups** page, choose the **Groups** tab, select the group that you created earlier, and choose **Next**\.

1. Select the permission set that you created earlier and choose **Next**, then choose **Submit**\. The configuration takes a few moments\.

## Create example applications<a name="sso-tutorial-app-only-code"></a>

Create the following applications\. They will be run on the SSO user's computer\.

### List Amazon S3 buckets<a name="sso-tutorial-app-only-code-s3"></a>

Include NuGet packages `AWSSDK.SSO` and `AWSSDK.SSOOIDC` in addition to `AWSSDK.S3` and `AWSSDK.SecurityToken`\.

```
using System;
using System.Threading.Tasks;
using System.Diagnostics;


// NuGet packages: AWSSDK.S3, AWSSDK.SecurityToken, AWSSDK.SSO, AWSSDK.SSOOIDC
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace SSOExample.S3.Programmatic_login
{
    class Program
    {
        // Requirements:
        // - An SSO profile in the SSO user's shared config file.

        // Class members.
        private static string profile = "my-sso-profile";

        static async Task Main(string[] args)
        {
            // Get SSO credentials from the information in the shared config file.
            var ssoCreds = LoadSsoCredentials(profile);

            // Display the caller's identity.
            var ssoProfileClient = new AmazonSecurityTokenServiceClient(ssoCreds);
            Console.WriteLine($"\nSSO Profile:\n {await ssoProfileClient.GetCallerIdentityArn()}");

            // Display a list of the account's S3 buckets.
            // The S3 client is created using the SSO credentials obtained earlier.
            var s3Client = new AmazonS3Client(ssoCreds);
            Console.WriteLine("\nGetting a list of your buckets...");
            var listResponse = await s3Client.ListBucketsAsync();
            Console.WriteLine($"Number of buckets: {listResponse.Buckets.Count}");
            foreach (S3Bucket b in listResponse.Buckets)
            {
                Console.WriteLine(b.BucketName);
            }
            Console.WriteLine();
        }

        // Method to get SSO credentials from the information in the shared config file.
        static AWSCredentials LoadSsoCredentials(string profile)
        {
            var chain = new CredentialProfileStoreChain();
            if (!chain.TryGetAWSCredentials(profile, out var credentials))
                throw new Exception($"Failed to find the {profile} profile");

            var ssoCredentials = credentials as SSOAWSCredentials;

            ssoCredentials.Options.ClientName = "Example-SSO-App";
            ssoCredentials.Options.SsoVerificationCallback = args =>
            {
                // Launch a browser window that prompts the SSO user to complete an SSO login.
                //  This method is only invoked if the session doesn't already have a valid SSO token.
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

    }

    // Class to read the caller's identity.
    public static class Extensions
    {
        public static async Task<string> GetCallerIdentityArn(this IAmazonSecurityTokenService stsClient)
        {
            var response = await stsClient.GetCallerIdentityAsync(new GetCallerIdentityRequest());
            return response.Arn;
        }
    }
}
```

### List IAM users<a name="sso-tutorial-app-only-code-iam"></a>

Include NuGet packages `AWSSDK.SSO` and `AWSSDK.SSOOIDC` in addition to `AWSSDK.IdentityManagement` and `AWSSDK.SecurityToken`\.

```
using System;
using System.Threading.Tasks;
using System.Diagnostics;


// NuGet packages: AWSSDK.IdentityManagement, AWSSDK.SecurityToken, AWSSDK.SSO, AWSSDK.SSOOIDC
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace SSOExample.IAM.Programmatic_login
{
    class Program
    {
        // Requirements:
        // - An SSO profile in the SSO user's shared config file.

        // Class members.
        private static string profile = "my-sso-profile";

        static async Task Main(string[] args)
        {
            // Get SSO credentials from the information in the shared config file.
            var ssoCreds = LoadSsoCredentials(profile);

            // Display the caller's identity.
            var ssoProfileClient = new AmazonSecurityTokenServiceClient(ssoCreds);
            Console.WriteLine($"\nSSO Profile:\n {await ssoProfileClient.GetCallerIdentityArn()}");

            // Display a list of the account's IAM users.
            // The IAM client is created using the SSO credentials obtained earlier.
            var iamClient = new AmazonIdentityManagementServiceClient(ssoCreds);
            Console.WriteLine("\nGetting a list of IAM users...");
            var listResponse = await iamClient.ListUsersAsync();
            Console.WriteLine($"Number of IAM users: {listResponse.Users.Count}");
            foreach (User u in listResponse.Users)
            {
                Console.WriteLine(u.UserName);
            }
            Console.WriteLine();
        }

        // Method to get SSO credentials from the information in the shared config file.
        static AWSCredentials LoadSsoCredentials(string profile)
        {
            var chain = new CredentialProfileStoreChain();
            if (!chain.TryGetAWSCredentials(profile, out var credentials))
                throw new Exception($"Failed to find the {profile} profile");

            var ssoCredentials = credentials as SSOAWSCredentials;

            ssoCredentials.Options.ClientName = "Example-SSO-App";
            ssoCredentials.Options.SsoVerificationCallback = args =>
            {
                // Launch a browser window that prompts the SSO user to complete an SSO login.
                //  This method is only invoked if the session doesn't already have a valid SSO token.
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

    }

    // Class to read the caller's identity.
    public static class Extensions
    {
        public static async Task<string> GetCallerIdentityArn(this IAmazonSecurityTokenService stsClient)
        {
            var response = await stsClient.GetCallerIdentityAsync(new GetCallerIdentityRequest());
            return response.Arn;
        }
    }
}
```

In addition to displaying lists of Amazon S3 buckets and IAM users, these applications display the user identity ARN for the SSO\-enabled profile, which is `my-sso-profile` in this tutorial\.

These applications perform SSO sign\-in tasks by providing a callback method in the [Options](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSSOAWSCredentialsOptions.html) property of an [SSOAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TSSOAWSCredentials.html) object\.

## Instruct SSO user<a name="sso-tutorial-app-only-user"></a>

Ask the SSO user to check their email and accept the SSO invitation\. They are prompted to set a password\. The message might take a few minutes to arrive in the SSO user's inbox\.

Give the SSO user the applications that you created earlier\.

Then, have the SSO user do the following:

1. If the folder that contains the shared AWS `config` file doesn't exist, create it\. If the folder does exist and has a subfolder called `.sso`, delete that subfolder\.

   The location of this folder is typically `%USERPROFILE%\.aws` in Windows and `~/.aws` in Linux and macOS\.

1. Create a shared AWS `config` file in that folder, if necessary, and add a profile to it as follows:

   ```
   [default]
   region = <default Region>
   
   [profile my-sso-profile]
   sso_start_url = <user portal URL recorded earlier>
   sso_region = <Region code recorded earlier>
   sso_account_id = <account ID recorded earlier>
   sso_role_name = SSOReadOnlyRole
   ```

1. Run the Amazon S3 application\.

1. In the resulting web sign\-in page, sign in\. Use the user name from the invitation message and the password that was created in response to the message\.

1. When sign\-in is complete, the application displays the list of S3 buckets\.

1. Run the IAM application\. The application displays the list of IAM users\. This is true even though a second sign\-in wasn't performed\. The IAM application uses the temporary token that was created earlier\.

## Cleanup<a name="sso-tutorial-app-only-cleanup"></a>

If you don't want to keep the resources that you created during this tutorial, clean them up\. These might be AWS resources or resources in your development environment such as files and folders\.