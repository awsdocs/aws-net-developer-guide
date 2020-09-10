--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Configure AWS credentials<a name="net-dg-config-creds"></a>

After you [create an AWS account](net-dg-signup.md) and [create the required user accounts](net-dg-users-roles.md), you can manage credentials for those user accounts\. You need these credentials to perform many of the tasks and examples in this guide\.

The following is a high\-level process for credential management and use\.

1. Create the credentials you need:
   + You can create credentials when you're creating a user account\. See [User accounts](net-dg-users-roles.md#net-dg-users-roles-user) for an example\.
   + You can also create credentials for an existing user account\. See [Managing access keys for IAM users](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html#Using_CreateAccessKey)\.

1. Store the credentials \(for example, in the [shared AWS credentials file](creds-file.md) or the [SDK Store](sdk-store.md)\)\.

1. Configure your project so that your application can [find the credentials](creds-assign.md)\.

The following topics provide information you can use to determine how to manage and use credentials in your environment\.

**Topics**
+ [Important warnings and guidelines](net-dg-config-creds-warnings-and-guidelines.md)
+ [Using the shared AWS credentials file](creds-file.md)
+ [Using the SDK Store \(Windows only\)](sdk-store.md)
+ [Credential and profile resolution](creds-assign.md)