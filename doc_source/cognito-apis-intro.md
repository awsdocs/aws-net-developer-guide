# Authenticating users with Amazon Cognito<a name="cognito-apis-intro"></a>

**Note**  
The information in this topic is specific to projects based on \.NET Framework and the AWS SDK for \.NET version 3\.3 and earlier\.

Using Amazon Cognito Identity, you can create unique identities for your users and authenticate them for secure access to your AWS resources such as Amazon S3 or Amazon DynamoDB\. Amazon Cognito Identity supports public identity providers such as Amazon, Facebook, Twitter/Digits, Google, or any OpenID Connect\-compatible provider as well as unauthenticated identities\. Amazon Cognito also supports [developer authenticated identities](http://aws.amazon.com/blogs/mobile/amazon-cognito-announcing-developer-authenticated-identities/), which let you register and authenticate users using your own backend authentication process, while still using Amazon Cognito Sync to synchronize user data and access AWS resources\.

For more information on [Amazon Cognito](https://aws.amazon.com/cognito/), see the [Amazon Cognito Developer Guide](https://docs.aws.amazon.com/cognito/latest/developerguide/)\.

The following code examples show how to easily use Amazon Cognito Identity\. The [Credentials provider](cognito-creds-provider.md) example shows how to create and authenticate user identities\. The [CognitoAuthentication extension library](cognito-authentication-extension.md) example shows how to use the CognitoAuthentication extension library to authenticate Amazon Cognito user pools\.

**Topics**
+ [Credentials provider](cognito-creds-provider.md)
+ [CognitoAuthentication extension library](cognito-authentication-extension.md)