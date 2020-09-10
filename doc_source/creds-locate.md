--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Accessing credentials and profiles in an application<a name="creds-locate"></a>

The preferred method for using credentials is to allow the AWS SDK for \.NET to find and retrieve them for you, as described in [Credential and profile resolution](creds-assign.md)\.

However, you can also configure your application to actively retrieve profiles and credentials, and then explicitly use those credentials when creating an AWS service client\.

To actively retrieve profiles and credentials, use classes from the [Amazon\.Runtime\.CredentialManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/NRuntimeCredentialManagement.html) namespace\.
+ To find a profile in a file that uses the AWS credentials file format \(either the [shared AWS credentials file in its default location](creds-file.md) or a custom credentials file\), use the [SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TSharedCredentialsFile.html) class\. Files in this format are sometimes simply called *credentials files* in this text for brevity\.
+ To find a profile in the SDK Store, use the [NetSDKCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TNetSDKCredentialsFile.html) class\.
+ To search in both a credentials file and the SDK Store, depending on the configuration of a class property, use the [CredentialProfileStoreChain](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileStoreChain.html) class\.

  You can use this class to find profiles\. You can also use this class to request AWS credentials directly instead of using the `AWSCredentialsFactory` class \(described next\)\.
+ To retrieve or create various types of credentials from a profile, use the [AWSCredentialsFactory](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentialsFactory.html) class\.

The following sections provide examples for these classes\.

## Examples for class CredentialProfileStoreChain<a name="creds-locate-chain"></a>

You can get credentials or profiles from the [CredentialProfileStoreChain](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TCredentialProfileStoreChain.html) class by using the [TryGetAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/MCredentialProfileStoreChainTryGetAWSCredentialsStringAWSCredentials.html) or [TryGetProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/MCredentialProfileStoreChainTryGetProfileStringCredentialProfile.html) methods\. The `ProfilesLocation` property of the class determines the behavior of the methods, as follows:
+ If `ProfilesLocation` is null or empty, search the SDK Store if the platform supports it, and then search the shared AWS credentials file in the default location\.
+ If the `ProfilesLocation` property contains a value, search the credentials file specified in the property\.

### Get credentials from the SDK Store or the shared AWS credentials file<a name="creds-locate-chain-get-credentials-default-location"></a>

This example shows you how to get credentials by using the `CredentialProfileStoreChain` class and then use the credentials to create an [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/S3/TS3Client.html) object\. The credentials can come from the SDK Store or from the shared AWS credentials file at the default location\.

This example also uses the [Amazon\.Runtime\.AWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TAWSCredentials.html) class\.

```
var chain = new CredentialProfileStoreChain();
AWSCredentials awsCredentials;
if (chain.TryGetAWSCredentials("some_profile", out awsCredentials))
{
    // Use awsCredentials to create an Amazon S3 service client
    using (var client = new AmazonS3Client(awsCredentials))
    {
        var response = await client.ListBucketsAsync();
        Console.WriteLine($"Number of buckets: {response.Buckets.Count}");
    }
}
```

### Get a profile from the SDK Store or the shared AWS credentials file<a name="creds-locate-chain-get-profile-default-location"></a>

This example shows you how to get a profile by using the CredentialProfileStoreChain class\. The credentials can come from the SDK Store or from the shared AWS credentials file at the default location\.

This example also uses the [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TCredentialProfile.html) class\.

```
var chain = new CredentialProfileStoreChain();
CredentialProfile basicProfile;
if (chain.TryGetProfile("basic_profile", out basicProfile))
{
    // Use basicProfile
}
```

### Get credentials from a custom credentials file<a name="creds-locate-chain-get-credentials-alternate-location"></a>

This example shows you how to get credentials by using the CredentialProfileStoreChain class\. The credentials come from a file that uses the AWS credentials file format but is at an alternate location\.

This example also uses the [Amazon\.Runtime\.AWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TAWSCredentials.html) class\.

```
var chain = new
    CredentialProfileStoreChain("c:\\Users\\sdkuser\\customCredentialsFile.ini");
AWSCredentials awsCredentials;
if (chain.TryGetAWSCredentials("basic_profile", out awsCredentials))
{
    // Use awsCredentials to create an AWS service client
}
```

## Examples for classes SharedCredentialsFile and AWSCredentialsFactory<a name="creds-locate-cred-shared-file"></a>

### Create an AmazonS3Client by using the SharedCredentialsFile class<a name="creds-locate-cred-shared-file-create-s3-client"></a>

This examples shows you how to find a profile in the shared AWS credentials file, create AWS credentials from the profile, and then use the credentials to create an [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/S3/TS3Client.html) object\. The example uses the [SharedCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TSharedCredentialsFile.html) class\.

This example also uses the [CredentialProfile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TCredentialProfile.html) class and the [Amazon\.Runtime\.AWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs//items/Runtime/TAWSCredentials.html) class\.

```
CredentialProfile basicProfile;
AWSCredentials awsCredentials;
var sharedFile = new SharedCredentialsFile();
if (sharedFile.TryGetProfile("basic_profile", out basicProfile) &&
    AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, sharedFile, out awsCredentials))
{
    // use awsCredentials to create an Amazon S3 service client
    using (var client = new AmazonS3Client(awsCredentials, basicProfile.Region))
    {
        var response = await client.ListBucketsAsync();
        Console.WriteLine($"Number of buckets: {response.Buckets.Count}");
    }
}
```

**Note**  
The [NetSDKCredentialsFile](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TNetSDKCredentialsFile.html) class can be used in exactly the same way, except you would instantiate a new NetSDKCredentialsFile object instead of a SharedCredentialsFile object\.