--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Create users and roles<a name="net-dg-users-roles"></a>

As a result of [creating an AWS account](net-dg-signup.md), you have \(at least\) two user accounts:
+ Your root user account, which was created for you and has full access to everything\.
+ An administrative user account, which you created and gave full access to *almost* everything\.

Neither of these user accounts is appropriate for doing \.NET development on AWS or for running \.NET applications on AWS\. As such, you need to create user accounts and service roles that are appropriate for these tasks\.

The specific user accounts and service roles that you create, and the way in which you use them, will depend on the requirements of your applications\. The following are some of the simplest types of user accounts and service roles, and some information about why they might be used and how to create them\.

## User accounts<a name="net-dg-users-roles-user"></a>

You can use a user account with long\-term credentials to access AWS services through your application\. This type of access is appropriate if a single user will be using your application \(you, for example\)\. The most common scenario for using this type of access is during development, but other scenarios are possible\.

The process for creating a user varies depending on the situation, but is essentially the following\.

1. Sign in to the AWS Management Console and open the IAM console at [https://console\.aws\.amazon\.com/iam/](https://console.aws.amazon.com/iam/)\.

1. Choose **Users**, and then choose **Add user**\.

1. Provide a user name\.

1. Under **Select AWS access type**, select **Programmatic access**, and then choose **Next: Permissions**\.

1. Choose **Attach existing policies directly**, and then select the [appropriate policies](https://docs.aws.amazon.com/IAM/latest/UserGuide/access_policies.html) for the AWS services that your application will use\.
**Warning**  
Do ***NOT*** choose the **AdministratorAccess** policy because that policy enables read and write permissions to almost everything in your account\.

1. Choose **Next: Tags** and enter any tags you want\.

   You can find information about tags in [Control access using AWS resource tags](https://docs.aws.amazon.com/IAM/latest/UserGuide/access_tags.html) in the [IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/)\.

1. Choose **Next: Review**, and then choose **Create user**\.

1. Record the credentials for the new user\. You can do this by downloading the cleartext `.csv` file or by copying and pasting the *access key ID* and *secret access key*\.

   These are the credentials that you will need for your application\.
**Warning**  
Use [appropriate security measures](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html#iam-user-access-keys) to keep these credentials safe and rotated\.

You can find high\-level information about IAM users in [Identities \(users, groups, and roles\)](https://docs.aws.amazon.com/IAM/latest/UserGuide/id.html) in the [IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/)\. Find detailed information about users in that guide's [IAM users](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_users.html) topic\.

## Service roles<a name="net-dg-users-roles-service-role"></a>

You can set up an AWS service role to access AWS services on behalf of users\. This type of access is appropriate if multiple people will be running your application remotely; for example, on an Amazon EC2 instance that you have created for this purpose\.

The process for creating a service role varies depending on the situation, but is essentially the following\.

1. Sign in to the AWS Management Console and open the IAM console at [https://console\.aws\.amazon\.com/iam/](https://console.aws.amazon.com/iam/)\.

1. Choose **Roles**, and then choose **Create role**\.

1. Choose **AWS service**, find and select **EC2** \(for example\), and then choose the **EC2** use case \(for example\)\.

1. Choose **Next: Permissions**, and select the [appropriate policies](https://docs.aws.amazon.com/IAM/latest/UserGuide/access_policies.html) for the AWS services that your application will use\.
**Warning**  
Do ***NOT*** choose the **AdministratorAccess** policy because that policy enables read and write permissions to almost everything in your account\.

1. Choose **Next: Tags** and enter any tags you want\.

   You can find information about tags in [Control access using AWS resource tags](https://docs.aws.amazon.com/IAM/latest/UserGuide/access_tags.html) in the [IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/)\.

1. Choose **Next: Review** and provide a **Role name** and **Role description**\. Then choose **Create role**\.

You can find high\-level information about IAM roles in [Identities \(users, groups, and roles\)](https://docs.aws.amazon.com/IAM/latest/UserGuide/id.html) in the [IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/)\. Find detailed information about roles in that guide's [IAM roles](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_roles.html) topic\.