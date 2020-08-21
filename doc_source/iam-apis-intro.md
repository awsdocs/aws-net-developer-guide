--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Managing Users With AWS Identity and Access Management \(IAM\)<a name="iam-apis-intro"></a>

The AWS SDK for \.NET supports IAM, which is a web service that enables AWS customers to manage users and user permissions in AWS\.

The sample code is written in C\#, but you can use the AWS SDK for \.NET with any compatible language\. When you install the AWS Toolkit for Visual Studio a set of C\# project templates are installed\. So the simplest way to start this project is to open Visual Studio, and then choose **File**, **New Project**, **AWS Sample Projects**, **Deployment and Management**, **AWS Identity and Access Management User**\.

For related API reference information, see [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html) and [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)\.

 **Prerequisites** 

Before you begin, be sure that you have created an AWS account and set up your AWS credentials\. For more information, see [Setting up the AWS SDK for \.NET](net-dg-setup.md)\.

**Topics**
+ [Managing IAM Aliases](iam-examples-account-aliases.md)
+ [Managing IAM Users](iam-examples-managing-users.md)
+ [Managing IAM Access Keys](iam-examples-managing-access-keys.md)
+ [Working with IAM Policies](iam-examples-policies.md)
+ [Working with IAM Server Certificates](iam-examples-server-certificates.md)
+ [List IAM Account Information](iam-examples-list-user-info.md)
+ [Granting Access Using an IAM Role](net-dg-hosm.md)