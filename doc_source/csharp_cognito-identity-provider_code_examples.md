# Amazon Cognito Identity Provider examples using AWS SDK for \.NET<a name="csharp_cognito-identity-provider_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon Cognito Identity Provider\.

*Actions* are code excerpts that show you how to call individual Amazon Cognito Identity Provider functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Amazon Cognito Identity Provider functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w359aac21c17c13c17c13)
+ [Scenarios](#w359aac21c17c13c17c15)

## Actions<a name="w359aac21c17c13c17c13"></a>

### Confirm a user<a name="cognito-identity-provider_ConfirmSignUp_csharp_topic"></a>

The following code example shows how to confirm an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Confirms that a user has been signed up successfully.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="code">The code sent by the authentication provider
        /// to confirm a user's membership in the pool.</param>
        /// <param name="userName">The user to confirm.</param>
        /// <returns>A Boolean value indicating the success of the confirmation
        /// operation.</returns>
        public static async Task<bool> ConfirmSignUp(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string clientId,
            string code,
            string userName)
        {
            var signUpRequest = new ConfirmSignUpRequest
            {
                ClientId = clientId,
                ConfirmationCode = code,
                Username = userName,
            };

            var response = await identityProviderClient.ConfirmSignUpAsync(signUpRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{userName} was confirmed");
                return true;
            }
            else
            {
                return false;
            }
        }
```
+  For API details, see [ConfirmSignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ConfirmSignUp) in *AWS SDK for \.NET API Reference*\. 

### Get a token to associate an MFA application with a user<a name="cognito-identity-provider_AssociateSoftwareToken_csharp_topic"></a>

The following code example shows how to get a token to associate an MFA application with an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Gets the secret token that will enable multi-factor
        /// authentication (MFA) for the user.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="session">The currently active session.</param>
        /// <returns>Returns a string representing the currently active
        /// session.</returns>
        public static async Task<string> GetSecretForAppMFA(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string session)
        {
            var softwareTokenRequest = new AssociateSoftwareTokenRequest
            {
                Session = session,
            };

            var tokenResponse = await identityProviderClient.AssociateSoftwareTokenAsync(softwareTokenRequest);
            var secretCode = tokenResponse.SecretCode;

            Console.WriteLine($"Enter the following token into Google Authenticator: {secretCode}");

            return tokenResponse.Session;
        }
```
+  For API details, see [AssociateSoftwareToken](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AssociateSoftwareToken) in *AWS SDK for \.NET API Reference*\. 

### Get information about a user<a name="cognito-identity-provider_AdminGetUser_csharp_topic"></a>

The following code example shows how to get information about an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Checks the status of a user for a particular Amazon Cognito user
        /// pool.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="userName">The user name for which we want to check
        /// the status.</param>
        /// <param name="poolId">The user pool for which we want to check the
        /// user's status.</param>
        /// <returns>The UserStatusType object indicating the user's status.</returns>
        public static async Task<UserStatusType> GetAdminUser(AmazonCognitoIdentityProviderClient identityProviderClient, string userName, string poolId)
        {
            var userRequest = new AdminGetUserRequest
            {
                Username = userName,
                UserPoolId = poolId,
            };

            var response = await identityProviderClient.AdminGetUserAsync(userRequest);

            Console.WriteLine($"User status {response.UserStatus}");
            return response.UserStatus;
        }
```
+  For API details, see [AdminGetUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminGetUser) in *AWS SDK for \.NET API Reference*\. 

### Resend a confirmation code<a name="cognito-identity-provider_ResendConfirmationCode_csharp_topic"></a>

The following code example shows how to resend an Amazon Cognito confirmation code\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Causes the confirmation code for user registration to be sent
        /// again.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="userName">The user name to be confirmed.</param>
        /// <returns>A System Threading Task.</returns>
        public static async Task ResendConfirmationCode(AmazonCognitoIdentityProviderClient identityProviderClient, string clientId, string userName)
        {
            var codeRequest = new ResendConfirmationCodeRequest
            {
                ClientId = clientId,
                Username = userName,
            };

            var response = await identityProviderClient.ResendConfirmationCodeAsync(codeRequest);

            Console.WriteLine($"Method of delivery is {response.CodeDeliveryDetails.DeliveryMedium}");
        }
```
+  For API details, see [ResendConfirmationCode](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ResendConfirmationCode) in *AWS SDK for \.NET API Reference*\. 

### Respond to SRP authentication challenges<a name="cognito-identity-provider_RespondToAuthChallenge_csharp_topic"></a>

The following code example shows how to respond to Amazon Cognito SRP authentication challenges\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Responds to an authentication challenge for an Amazon Cognito user.
        /// </summary>
        /// <param name="identityProviderClient">The Amazon Cognito client object.</param>
        /// <param name="userName">The user name of the user to authenticate.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="mfaCode">The MFA code supplied by the user.</param>
        /// <param name="session">The session for which the user will be authenticated.</param>
        /// <returns>A Boolean value that indicates the success of the authentication.</returns>
        public static async Task<bool> AdminRespondToAuthChallenge(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string userName,
            string clientId,
            string mfaCode,
            string session)
        {
            Console.WriteLine("SOFTWARE_TOKEN_MFA challenge is generated");

            var challengeResponses = new Dictionary<string, string>
            {
                { "USERNAME", userName },
                { "SOFTWARE_TOKEN_MFA_CODE", mfaCode },
            };

            var respondToAuthChallengeRequest = new RespondToAuthChallengeRequest
            {
                ChallengeName = ChallengeNameType.SOFTWARE_TOKEN_MFA,
                ClientId = clientId,
                ChallengeResponses = challengeResponses,
                Session = session,
            };

            var response = await identityProviderClient.RespondToAuthChallengeAsync(respondToAuthChallengeRequest);
            Console.WriteLine($"response.getAuthenticationResult() {response.AuthenticationResult}");

            return response.AuthenticationResult is not null;
        }
```
+  For API details, see [RespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/RespondToAuthChallenge) in *AWS SDK for \.NET API Reference*\. 

### Respond to an authentication challenge<a name="cognito-identity-provider_AdminRespondToAuthChallenge_csharp_topic"></a>

The following code example shows how to respond to an Amazon Cognito authentication challenge\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Responds to an authentication challenge for an Amazon Cognito user.
        /// </summary>
        /// <param name="identityProviderClient">The Amazon Cognito client object.</param>
        /// <param name="userName">The user name of the user to authenticate.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="mfaCode">The MFA code supplied by the user.</param>
        /// <param name="session">The session for which the user will be authenticated.</param>
        /// <returns>A Boolean value that indicates the success of the authentication.</returns>
        public static async Task<bool> AdminRespondToAuthChallenge(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string userName,
            string clientId,
            string mfaCode,
            string session)
        {
            Console.WriteLine("SOFTWARE_TOKEN_MFA challenge is generated");

            var challengeResponses = new Dictionary<string, string>
            {
                { "USERNAME", userName },
                { "SOFTWARE_TOKEN_MFA_CODE", mfaCode },
            };

            var respondToAuthChallengeRequest = new RespondToAuthChallengeRequest
            {
                ChallengeName = ChallengeNameType.SOFTWARE_TOKEN_MFA,
                ClientId = clientId,
                ChallengeResponses = challengeResponses,
                Session = session,
            };

            var response = await identityProviderClient.RespondToAuthChallengeAsync(respondToAuthChallengeRequest);
            Console.WriteLine($"response.getAuthenticationResult() {response.AuthenticationResult}");

            return response.AuthenticationResult is not null;
        }
```
+  For API details, see [AdminRespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminRespondToAuthChallenge) in *AWS SDK for \.NET API Reference*\. 

### Sign up a user<a name="cognito-identity-provider_SignUp_csharp_topic"></a>

The following code example shows how to sign up a user with Amazon Cognito\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Add a new user to an Amazon Cognito user pool.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="userName">The user name of the user to sign up.</param>
        /// <param name="password">The password for the user.</param>
        /// <param name="email">The user's email address.</param>
        /// <returns>A System Threading Task.</returns>
        public static async Task<string> SignUp(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string clientId,
            string userName,
            string password,
            string email)
        {
            var userAttrs = new AttributeType
            {
                Name = "email",
                Value = email,
            };

            var userAttrsList = new List<AttributeType>
            {
                userAttrs,
            };

            var signUpRequest = new SignUpRequest
            {
                UserAttributes = userAttrsList,
                Username = userName,
                ClientId = clientId,
                Password = password,
            };

            var response = await identityProviderClient.SignUpAsync(signUpRequest);
            Console.WriteLine("User has been signed up.");
            return response.UserSub;
        }
```
+  For API details, see [SignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/SignUp) in *AWS SDK for \.NET API Reference*\. 

### Start authentication with a tracked device<a name="cognito-identity-provider_InitiateAuth_csharp_topic"></a>

The following code example shows how to start authentication with a device tracked by Amazon Cognito\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Initiates the authorization process.
        /// </summary>
        /// <param name="identityProviderClient">An initialized Identity
        /// Provider client object.</param>
        /// <param name="clientId">The client Id of the application associated
        /// with the user pool.</param>
        /// <param name="userName">The user name to be authorized.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The response from the client from the InitiateAuthAsync
        /// call.</returns>
        public static async Task<InitiateAuthResponse> InitiateAuth(AmazonCognitoIdentityProviderClient identityProviderClient, string clientId, string userName, string password)
        {
            var authParameters = new Dictionary<string, string>
            {
                { "USERNAME", userName },
                { "PASSWORD", password },
            };

            var authRequest = new InitiateAuthRequest
            {
                ClientId = clientId,
                AuthParameters = authParameters,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            };

            var response = await identityProviderClient.InitiateAuthAsync(authRequest);
            Console.WriteLine($"Result Challenge is : {response.ChallengeName}");

            return response;
        }
```
+  For API details, see [InitiateAuth](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/InitiateAuth) in *AWS SDK for \.NET API Reference*\. 

### Verify an MFA application with a user<a name="cognito-identity-provider_VerifySoftwareToken_csharp_topic"></a>

The following code example shows how to verify an MFA application with an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
        /// <summary>
        /// Verifies that the user has supplied the correct one-time password
        /// and registers for multi-factor authentication (MFA).
        /// </summary>
        /// <param name="identityProviderClient">The Amazon Cognito client object.</param>
        /// <param name="session">The session for which the user will be
        /// authenticated.</param>
        /// <param name="code">The code provided by the user.</param>
        /// <returns>A Boolean value that indicates the success of the authentication.</returns>
        public static async Task<bool> VerifyTOTP(
            AmazonCognitoIdentityProviderClient identityProviderClient,
            string session,
            string code)
        {
            var tokenRequest = new VerifySoftwareTokenRequest
            {
                UserCode = code,
                Session = session,
            };

            var response = await identityProviderClient.VerifySoftwareTokenAsync(tokenRequest);

            Console.WriteLine($"The status of the token is {response.Status}");

            return response.Status == VerifySoftwareTokenResponseType.SUCCESS;
        }
```
+  For API details, see [VerifySoftwareToken](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/VerifySoftwareToken) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w359aac21c17c13c17c15"></a>

### Sign up a user with a user pool that requires MFA<a name="cognito-identity-provider_Scenario_SignUpUserWithMfa_csharp_topic"></a>

The following code example shows how to:
+ Sign up a user with a user name, password, and email address\.
+ Confirm the user from a code sent in email\.
+ Set up multi\-factor authentication by associating an MFA application with the user\.
+ Sign in by using a password and an MFA code\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
global using Amazon;
global using Amazon.CognitoIdentityProvider;
global using Amazon.CognitoIdentityProvider.Model;
global using Cognito_MVP;



// Before running this AWS SDK for .NET (v3) code example, set up your development environment, including your credentials.
// For more information, see the following documentation:
// https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-setup.html
// TIP: To set up the required user pool, run the AWS Cloud Development Kit (AWS CDK)
// script provided in this GitHub repo at:
// resources/cdk/cognito_scenario_user_pool_with_mfa.
// This code example performs the following operations:
// 1. Invokes the signUp method to sign up a user.
// 2. Invokes the adminGetUser method to get the user's confirmation status.
// 3. Invokes the ResendConfirmationCode method if the user requested another code.
// 4. Invokes the confirmSignUp method.
// 5. Invokes the initiateAuth to sign in. This results in being prompted to set up TOTP (time-based one-time password). (The response is “ChallengeName”: “MFA_SETUP”).
// 6. Invokes the AssociateSoftwareToken method to generate a TOTP MFA private key. This can be used with Google Authenticator.
// 7. Invokes the VerifySoftwareToken method to verify the TOTP and register for MFA.
// 8. Invokes the AdminInitiateAuth to sign in again. This results in being prompted to submit a TOTP (Response: “ChallengeName”: “SOFTWARE_TOKEN_MFA”).
// 9. Invokes the AdminRespondToAuthChallenge to get back a token.

// Set the following variables:
// clientId - Fill in the app client Id value from the AWS CDK script.
string clientId = "";

// poolId - Fill in the pool Id that you can get from the AWS CDK script.
string poolId = "";
var userName = string.Empty;
var password = string.Empty;
var email = string.Empty;

string sepBar = new string('-', 80);
var identityProviderClient = new AmazonCognitoIdentityProviderClient();

do
{
    Console.Write("Enter your user name: ");
    userName = Console.ReadLine();
}
while (userName == string.Empty);

Console.WriteLine($"User name: {userName}");

do
{
    Console.Write("Enter your password: ");
    password = Console.ReadLine();
}
while (password == string.Empty);
Console.WriteLine($"Signing up {userName}");

do
{
    Console.Write("Enter your email: ");
    email = Console.ReadLine();
} while (email == string.Empty);

await CognitoMethods.SignUp(identityProviderClient, clientId, userName, password, email);

Console.WriteLine(sepBar);
Console.WriteLine($"Getting {userName} status from the user pool");
await CognitoMethods.GetAdminUser(identityProviderClient, userName, poolId);

Console.WriteLine(sepBar);
Console.WriteLine($"Conformation code sent to {userName}. Would you like to send a new code? (Yes/No)");
var ans = Console.ReadLine();

if (ans.ToUpper() == "YES")
{
    await CognitoMethods.ResendConfirmationCode(identityProviderClient, clientId, userName);
    Console.WriteLine("Sending a new confirmation code");
}

Console.WriteLine(sepBar);
Console.WriteLine("*** Enter confirmation code that was emailed");
string code = Console.ReadLine();

await CognitoMethods.ConfirmSignUp(identityProviderClient, clientId, code, userName);

Console.WriteLine($"Rechecking the status of {userName} in the user pool");
await CognitoMethods.GetAdminUser(identityProviderClient, userName, poolId);

var authResponse = await CognitoMethods.InitiateAuth(identityProviderClient, clientId, userName, password);
var mySession = authResponse.Session;

var newSession = await CognitoMethods.GetSecretForAppMFA(identityProviderClient, mySession);

Console.WriteLine("Enter the 6-digit code displayed in Google Authenticator");
string myCode = Console.ReadLine();

// Verify the TOTP and register for MFA.
await CognitoMethods.VerifyTOTP(identityProviderClient, newSession, myCode);
Console.WriteLine("Enter the new 6-digit code displayed in Google Authenticator");
string mfaCode = Console.ReadLine();

Console.WriteLine(sepBar);
var authResponse1 = await CognitoMethods.InitiateAuth(identityProviderClient, clientId, userName, password);
var session2 = authResponse1.Session;
await CognitoMethods.AdminRespondToAuthChallenge(identityProviderClient, userName, clientId, mfaCode, session2);

Console.WriteLine("The Cognito MVP application has completed.");
```
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [AdminGetUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminGetUser)
  + [AdminInitiateAuth](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminInitiateAuth)
  + [AdminRespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminRespondToAuthChallenge)
  + [AssociateSoftwareToken](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AssociateSoftwareToken)
  + [ConfirmDevice](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ConfirmDevice)
  + [ConfirmSignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ConfirmSignUp)
  + [InitiateAuth](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/InitiateAuth)
  + [ListUsers](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ListUsers)
  + [ResendConfirmationCode](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ResendConfirmationCode)
  + [RespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/RespondToAuthChallenge)
  + [SignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/SignUp)
  + [VerifySoftwareToken](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/VerifySoftwareToken)