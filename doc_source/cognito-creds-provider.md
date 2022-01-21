--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Amazon Cognito credentials provider<a name="cognito-creds-provider"></a>

**Note**  
The information in this topic is specific to projects based on \.NET Framework and the AWS SDK for \.NET version 3\.3 and earlier\.

 `Amazon.CognitoIdentity.CognitoAWSCredentials`, found in the [AWSSDK\.CognitoIdentity](https://www.nuget.org/packages/AWSSDK.CognitoIdentity/) NuGet package, is a credentials object that uses Amazon Cognito and the AWS Security Token Service \(AWS STS\) to retrieve credentials to make AWS calls\.

The first step in setting up `CognitoAWSCredentials` is to create an “identity pool”\. \(An identity pool is a store of user identity information that is specific to your account\. The information is retrievable across client platforms, devices, and operating systems, so that if a user starts using the app on a phone and later switches to a tablet, the persisted app information is still available for that user\. You can create a new identity pool from the Amazon Cognito console\. If you are using the console, it will also provide you the other pieces of information you need:
+ Your account number\- A 12\-digit number, such as 123456789012, that is unique to your account\.
+ The unauthenticated role ARN\- A role that unauthenticated users will assume\. For example, this role can provide read\-only permissions to your data\.
+ The authenticated role ARN\- A role that authenticated users will assume\. This role can provide more extensive permissions to your data\.

## Set up CognitoAWSCredentials<a name="set-up-cognitoawscredentials"></a>

The following code example shows how to set up `CognitoAWSCredentials`, which you can then use to make a call to Amazon S3 as an unauthenticated user\. This enables you to make calls with just a minimum amount of data required to authenticate the user\. User permissions are controlled by the role, so you can configure access as you need\.

```
CognitoAWSCredentials credentials = new CognitoAWSCredentials(
    accountId,        // Account number
    identityPoolId,   // Identity pool ID
    unAuthRoleArn,    // Role for unauthenticated users
    null,             // Role for authenticated users, not set
    region);
using (var s3Client = new AmazonS3Client(credentials))
{
    s3Client.ListBuckets();
}
```

## Use AWS as an unauthenticated user<a name="use-aws-as-an-unauthenticated-user"></a>

The following code example shows how you can start using AWS as an unauthenticated user, then authenticate through Facebook and update the credentials to use Facebook credentials\. Using this approach, you can grant different capabilities to authenticated users via the authenticated role\. For instance, you might have a phone application that permits users to view content anonymously, but allows them to post if they are logged on with one or more of the configured providers\.

```
CognitoAWSCredentials credentials = new CognitoAWSCredentials(
    accountId, identityPoolId,
    unAuthRoleArn,    // Role for unauthenticated users
    authRoleArn,      // Role for authenticated users
    region);
using (var s3Client = new AmazonS3Client(credentials))
{
    // Initial use will be unauthenticated
    s3Client.ListBuckets();

    // Authenticate user through Facebook
    string facebookToken = GetFacebookAuthToken();

    // Add Facebook login to credentials. This clears the current AWS credentials
    // and retrieves new AWS credentials using the authenticated role.
    credentials.AddLogin("graph.facebook.com", facebookAccessToken);

    // This call is performed with the authenticated role and credentials
    s3Client.ListBuckets();
}
```

The `CognitoAWSCredentials` object provides even more functionality if you use it with the `AmazonCognitoSyncClient` that is part of the AWS SDK for \.NET\. If you're using both `AmazonCognitoSyncClient` and `CognitoAWSCredentials`, you don't have to specify the `IdentityPoolId` and `IdentityId` properties when making calls with the `AmazonCognitoSyncClient`\. These properties are automatically filled in from `CognitoAWSCredentials`\. The next code example illustrates this, as well as an event that notifies you whenever the `IdentityId` for `CognitoAWSCredentials` changes\. The `IdentityId` can change in some cases, such as when changing from an unauthenticated user to an authenticated one\.

```
CognitoAWSCredentials credentials = GetCognitoAWSCredentials();

// Log identity changes
credentials.IdentityChangedEvent += (sender, args) =>
{
    Console.WriteLine("Identity changed: [{0}] => [{1}]", args.OldIdentityId, args.NewIdentityId);
};

using (var syncClient = new AmazonCognitoSyncClient(credentials))
{
    var result = syncClient.ListRecords(new ListRecordsRequest
    {
        DatasetName = datasetName
        // No need to specify these properties
        //IdentityId = "...",
        //IdentityPoolId = "..."        
    });
}
```