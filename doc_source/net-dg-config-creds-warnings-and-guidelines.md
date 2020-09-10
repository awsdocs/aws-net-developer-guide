--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Important warnings and guidance for credentials<a name="net-dg-config-creds-warnings-and-guidelines"></a>

**Warnings for credentials**
+ ***Do NOT*** use your account's root credentials to access AWS resources\. These credentials provide unrestricted account access and are difficult to revoke\.
+ ***Do NOT*** put literal access keys in your application files\. If you do, you create a risk of accidentally exposing your credentials if, for example, you upload the project to a public repository\.
+ ***Do NOT*** include files that contain credentials in your project area\.
+ Credentials in one of the credential\-storage mechanisms, the shared AWS credentials file, are stored in plaintext\.

**Additional guidance for securely managing credentials**

For a general discussion of how to securely manage AWS credentials, see [Best practices for managing AWS access keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html) in the [AWS General Reference](https://docs.aws.amazon.com/general/latest/gr/)\. In addition to that discussion, consider the following:
+ Create IAM users and use their credentials instead of using your AWS root user\. IAM user credentials can be revoked if necessary\. In addition, you can apply a policy to each IAM user for access to certain resources and actions\.
+ Use [IAM roles for tasks](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/task-iam-roles.html) for Amazon Elastic Container Service \(Amazon ECS\) tasks\.
+ Use [IAM roles](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_roles.html) for applications that are running on Amazon EC2 instances\.
+ Use [temporary credentials](creds-assign.md#net-dg-config-creds-assign-role) or environment variables for applications that are available to users outside your organization\.