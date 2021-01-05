--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Amazon CognitoAuthentication Extension Library Examples<a name="cognito-authentication-extension"></a>

The CognitoAuthentication extension library simplifies the authentication process of Amazon Cognito user pools for \.NET Core and Xamarin developers\. The library is built on top of the Amazon Cognito Identity Provider API to create and send user authentication API calls\. You can get **Amazon\.Extensions\.CognitoAuthentication** from the NuGet gallery\.

## Using the CognitoAuthentication Extension Library<a name="using-the-cognitoauthentication-extension-library"></a>

Amazon Cognito has some built\-in `AuthFlow` and `ChallengeName` values for a standard authentication flow to validate username and password through the Secure Remote Password \(SRP\)\. For more information about authentication flow, see [Amazon Cognito User Pool Authentication Flow](https://docs.aws.amazon.com/cognito/latest/developerguide/amazon-cognito-user-pools-authentication-flow.html)\.

The following examples require these `using` statements:

```
// Required for all examples
using System;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
// Required for the GetS3BucketsAsync example
using Amazon.S3;
using Amazon.S3.Model;
```

### Use Basic Authentication<a name="use-basic-authentication"></a>

Create an [AmazonCognitoIdentityProviderClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CognitoIdentityProvider/TCognitoIdentityProviderClient.html) using [AnonymousAWSCredentials](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAnonymousAWSCredentials.html), which do not require signed requests\. You do not need to supply a region, the underlying code calls `FallbackRegionFactory.GetRegionEndpoint()` if a region is not provided\. Create `CognitoUserPool` and `CognitoUser` objects\. Call the `StartWithSrpAuthAsync` method with an `InitiateSrpAuthRequest` that contains the user password\.

```
public static async void GetCredsAsync()
{
    AmazonCognitoIdentityProviderClient provider =
        new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials());
    CognitoUserPool userPool = new CognitoUserPool("poolID", "clientID", provider);
    CognitoUser user = new CognitoUser("username", "clientID", userPool, provider);
    InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
    {
        Password = "userPassword"
    };

    AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
    accessToken = authResponse.AuthenticationResult.AccessToken;

}
```

### Authenticate with Challenges<a name="authenticate-with-challenges"></a>

Continuing the authentication flow with challenges, such as with NewPasswordRequired and Multi\-Factor Authentication \(MFA\), is also simpler\. The only requirements are the CognitoAuthentication objects, user’s password for SRP, and the necessary information for the next challenge, which is acquired after prompting the user to enter it\. The following code shows one way to check the challenge type and get the appropriate responses for MFA and NewPasswordRequired challenges during the authentication flow\.

Do a basic authentication request as before, and `await` an `AuthFlowResponse`\. When the response is received loop through the returned `AuthenticationResult` object\. If the `ChallengeName` type is `NEW_PASSWORD_REQUIRED`, call the `RespondToNewPasswordRequiredAsync` method\.

```
public static async void GetCredsChallengesAsync()
{
    AmazonCognitoIdentityProviderClient provider = 
        new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials());
    CognitoUserPool userPool = new CognitoUserPool("poolID", "clientID", provider);
    CognitoUser user = new CognitoUser("username", "clientID", userPool, provider);
    InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest(){
        Password = "userPassword"
    };

    AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

    while (authResponse.AuthenticationResult == null)
    {
        if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
        {
            Console.WriteLine("Enter your desired new password:");
            string newPassword = Console.ReadLine();

            authResponse = await user.RespondToNewPasswordRequiredAsync(new RespondToNewPasswordRequiredRequest()
            {
                SessionID = authResponse.SessionID,
                NewPassword = newPassword
            });
            accessToken = authResponse.AuthenticationResult.AccessToken;
        }
        else if (authResponse.ChallengeName == ChallengeNameType.SMS_MFA)
        {
            Console.WriteLine("Enter the MFA Code sent to your device:");
            string mfaCode = Console.ReadLine();

            AuthFlowResponse mfaResponse = await user.RespondToSmsMfaAuthAsync(new RespondToSmsMfaRequest()
            {
                SessionID = authResponse.SessionID,
                MfaCode = mfaCode

            }).ConfigureAwait(false);
            accessToken = authResponse.AuthenticationResult.AccessToken;
        }
        else
        {
            Console.WriteLine("Unrecognized authentication challenge.");
            accessToken = "";
            break;
        }
    }

    if (authResponse.AuthenticationResult != null)
    {
        Console.WriteLine("User successfully authenticated.");
    }
    else
    {
        Console.WriteLine("Error in authentication process.");
    }
 
}
```

### Use AWS Resources after Authentication<a name="use-aws-resources-after-authentication"></a>

Once a user is authenticated using the CognitoAuthentication library, the next step is to allow the user to access the appropriate AWS resources\. To do this you must create an identity pool through the Amazon Cognito Federated Identities console\. By specifying the Amazon Cognito user pool you created as a provider, using its poolID and clientID, you can allow your Amazon Cognito user pool users to access AWS resources connected to your account\. You can also specify different roles to enable both unauthenticated and authenticated users to access different resources\. You can change these rules in the IAM console, where you can add or remove permissions in the **Action** field of the role’s attached policy\. Then, using the appropriate identity pool, user pool, and Amazon Cognito user information, you can make calls to different AWS resources\. The following example shows a user authenticated with SRP accessing the different Amazon S3 buckets permitted by the associated identity pool’s role

```
public async void GetS3BucketsAsync()
{
    var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials());
    CognitoUserPool userPool = new CognitoUserPool("poolID", "clientID", provider);
    CognitoUser user = new CognitoUser("username", "clientID", userPool, provider);

    string password = "userPassword";

    AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
    {
        Password = password
    }).ConfigureAwait(false);

    CognitoAWSCredentials credentials =
        user.GetCognitoAWSCredentials("identityPoolID", RegionEndpoint.< YourIdentityPoolRegion >);

    using (var client = new AmazonS3Client(credentials))
    {
        ListBucketsResponse response =
            await client.ListBucketsAsync(new ListBucketsRequest()).ConfigureAwait(false);

        foreach (S3Bucket bucket in response.Buckets)
        {
            Console.WriteLine(bucket.BucketName);
        }
    }
}
```

## More Authentication Options<a name="more-authentication-options"></a>

In addition to SRP, NewPasswordRequired, and MFA, the CognitoAuthentication extension library offers an easier authentication flow for:
+ Custom \- Initiate with a call to `StartWithCustomAuthAsync(InitiateCustomAuthRequest customRequest)` 
+ RefreshToken \- Initiate with a call to `StartWithRefreshTokenAuthAsync(InitiateRefreshTokenAuthRequest refreshTokenRequest)` 
+ RefreshTokenSRP \- Initiate with a call to `StartWithRefreshTokenAuthAsync(InitiateRefreshTokenAuthRequest refreshTokenRequest)` 
+ AdminNoSRP \- Initiate with a call to `StartWithAdminNoSrpAuthAsync(InitiateAdminNoSrpAuthRequest adminAuthRequest)` 

Call the appropriate method depending on the flow you want\. Then continue prompting the user with challenges as they are presented in the `AuthFlowResponse` objects of each method call\. Also call the appropriate response method, such as `RespondToSmsMfaAuthAsync` for MFA challenges and `RespondToCustomAuthAsync` for custom challenges\.