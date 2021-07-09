--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Authenticating Users with Amazon Cognito<a name="cognito-apis-intro"></a>

Using Amazon Cognito Identity, you can create unique identities for your users and authenticate them for secure access to your AWS resources such as Amazon S3 or Amazon DynamoDB\. Amazon Cognito Identity supports public identity providers such as Amazon, Facebook, Twitter/Digits, Google, or any OpenID Connect\-compatible provider as well as unauthenticated identities\. Amazon Cognito also supports [developer authenticated identities](http://aws.amazon.com/blogs/mobile/amazon-cognito-announcing-developer-authenticated-identities/), which let you register and authenticate users using your own backend authentication process, while still using Amazon Cognito Sync to synchronize user data and access AWS resources\.

For more information on [Amazon Cognito](https://aws.amazon.com/cognito/), see the [Amazon Cognito Developer Guide](https://docs.aws.amazon.com/cognito/latest/developerguide/what-is-amazon-cognito.html) 

The following code examples show how to easily use Amazon Cognito Identity\. The *Amazon Cognito Credentials Provider* example shows how to create and authenticate user identities\. The *Amazon CognitoAuthentication Extension Library* example shows how to use the CognitoAuthentication extension library to authenticate Amazon Cognito user pools\.

**Topics**
+ [Amazon Cognito Credentials Provider](cognito-creds-provider.md)
+ [Amazon CognitoAuthentication Extension Library Examples](cognito-authentication-extension.md)