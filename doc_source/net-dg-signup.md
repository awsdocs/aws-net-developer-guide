--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Create an AWS Account and Credentials<a name="net-dg-signup"></a>

To use the AWS SDK for \.NET to access AWS services, you need an AWS account and AWS credentials\. To increase the security of your AWS account, we recommend that you use an *IAM user* to provide access credentials instead of using your root account credentials\.
+ To create an AWS account, see [How do I create and activate a new Amazon Web Services account?](https://aws.amazon.com/premiumsupport/knowledge-center/create-and-activate-aws-account)\.
+ Avoid using your root user account \(the initial account you create\) to access services\. Instead, create an administrative user account, as explained in [Creating Your First IAM Admin User and Group](https://docs.aws.amazon.com/IAM/latest/UserGuide/getting-started_create-admin-group.html)\. After you create the administrative user account and record the login details, sign out of your root user account and sign back in using the administrative account\.
+ To perform many of the tasks and examples in this guide, you will need access keys for a user account so that you can access AWS services programmatically\. To create access keys for an existing user, see [Managing Access Keys \(Console\)](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html#Using_CreateAccessKey)\. Alternatively, you can create access keys when you create a user\. When creating the user, choose an **Access type** of **Programmatic access** instead of \(or in addition to\) **AWS Management Console access**\.
+ To close your AWS account, see [Closing an Account](https://docs.aws.amazon.com/awsaccountbilling/latest/aboutv2/close-account.html)\.

For additional information about how to handle certificates and security, see [IAM Best Practices and Use Cases](https://docs.aws.amazon.com/IAM/latest/UserGuide/IAMBestPracticesAndUseCases.html) in the *[IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/)*