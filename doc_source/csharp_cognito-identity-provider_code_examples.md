# Amazon Cognito Identity Provider examples using AWS SDK for \.NET<a name="csharp_cognito-identity-provider_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon Cognito Identity Provider\.

*Actions* are code excerpts that show you how to call individual service functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple functions within the same service\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#actions)
+ [Scenarios](#scenarios)

## Actions<a name="actions"></a>

### Confirm a user<a name="cognito-identity-provider_ConfirmSignUp_csharp_topic"></a>

The following code example shows how to confirm an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Confirm that the user has signed up.
    /// </summary>
    /// <param name="clientId">The Id of this application.</param>
    /// <param name="code">The confirmation code sent to the user.</param>
    /// <param name="userName">The username.</param>
    /// <returns></returns>
    public async Task<bool> ConfirmSignupAsync(string clientId, string code, string userName)
    {
        var signUpRequest = new ConfirmSignUpRequest
        {
            ClientId = clientId,
            ConfirmationCode = code,
            Username = userName,
        };

        var response = await _cognitoService.ConfirmSignUpAsync(signUpRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"{userName} was confirmed");
            return true;
        }
        return false;
    }
```
+  For API details, see [ConfirmSignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ConfirmSignUp) in *AWS SDK for \.NET API Reference*\. 

### Confirm an MFA device for tracking<a name="cognito-identity-provider_ConfirmDevice_csharp_topic"></a>

The following code example shows how to confirm an MFA device for tracking by Amazon Cognito\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Initiates and confirms tracking of the device.
    /// </summary>
    /// <param name="accessToken">The user's access token.</param>
    /// <param name="deviceKey">The key of the device from Amazon Cognito.</param>
    /// <param name="deviceName">The device name.</param>
    /// <returns></returns>
    public async Task<bool> ConfirmDeviceAsync(string accessToken, string deviceKey, string deviceName)
    {
        var request = new ConfirmDeviceRequest
        {
            AccessToken = accessToken,
            DeviceKey = deviceKey,
            DeviceName = deviceName
        };

        var response = await _cognitoService.ConfirmDeviceAsync(request);
        return response.UserConfirmationNecessary;
    }
```
+  For API details, see [ConfirmDevice](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ConfirmDevice) in *AWS SDK for \.NET API Reference*\. 

### Get a token to associate an MFA application with a user<a name="cognito-identity-provider_AssociateSoftwareToken_csharp_topic"></a>

The following code example shows how to get a token to associate an MFA application with an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Get an MFA token to authenticate the user with the authenticator.
    /// </summary>
    /// <param name="session">The session name.</param>
    /// <returns>Returns the session name.</returns>
    public async Task<string> AssociateSoftwareTokenAsync(string session)
    {
        var softwareTokenRequest = new AssociateSoftwareTokenRequest
        {
            Session = session,
        };

        var tokenResponse = await _cognitoService.AssociateSoftwareTokenAsync(softwareTokenRequest);
        var secretCode = tokenResponse.SecretCode;

        Console.Write("Enter the following token into the authenticator: {secretCode}");

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
    /// Get the specified user from an Amazon Cognito user pool with administrator access.
    /// </summary>
    /// <param name="userName">The name of the user.</param>
    /// <param name="poolId">The Id of the Amazon Cognito user pool.</param>
    /// <returns></returns>
    public async Task<UserStatusType> GetAdminUserAsync(string userName, string poolId)
    {
        AdminGetUserRequest userRequest = new AdminGetUserRequest
        {
            Username = userName,
            UserPoolId = poolId,
        };

        var response = await _cognitoService.AdminGetUserAsync(userRequest);

        Console.WriteLine($"User status {response.UserStatus}");
        return response.UserStatus;
    }
```
+  For API details, see [AdminGetUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminGetUser) in *AWS SDK for \.NET API Reference*\. 

### List the user pools<a name="cognito-identity-provider_ListUserPools_csharp_topic"></a>

The following code example shows how to list the Amazon Cognito user pools\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// List the Amazon Cognito user pools for an account.
    /// </summary>
    /// <returns>A list of UserPoolDescriptionType objects.</returns>
    public async Task<List<UserPoolDescriptionType>> ListUserPoolsAsync()
    {
        var userPools = new List<UserPoolDescriptionType>();

        var userPoolsPaginator = _cognitoService.Paginators.ListUserPools(new ListUserPoolsRequest());

        await foreach (var response in userPoolsPaginator.Responses)
        {
            userPools.AddRange(response.UserPools);
        }

        return userPools;
    }
```
+  For API details, see [ListUserPools](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ListUserPools) in *AWS SDK for \.NET API Reference*\. 

### List users<a name="cognito-identity-provider_ListUsers_csharp_topic"></a>

The following code example shows how to list Amazon Cognito users\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Get a list of users for the Amazon Cognito user pool.
    /// </summary>
    /// <param name="userPoolId">The user pool Id.</param>
    /// <returns>A list of users.</returns>
    public async Task<List<UserType>> ListUsersAsync(string userPoolId)
    {
        var request = new ListUsersRequest
        {
            UserPoolId = userPoolId
        };

        var users = new List<UserType>();

        var usersPaginator = _cognitoService.Paginators.ListUsers(request);
        await foreach (var response in usersPaginator.Responses)
        {
            users.AddRange(response.Users);
        }

        return users;
    }
```
+  For API details, see [ListUsers](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ListUsers) in *AWS SDK for \.NET API Reference*\. 

### Resend a confirmation code<a name="cognito-identity-provider_ResendConfirmationCode_csharp_topic"></a>

The following code example shows how to resend an Amazon Cognito confirmation code\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Send a new confirmation code to a user.
    /// </summary>
    /// <param name="clientId">The Id of the client application.</param>
    /// <param name="userName">The username of user who will receive the code.</param>
    /// <returns></returns>
    public async Task<CodeDeliveryDetailsType> ResendConfirmationCodeAsync(string clientId, string userName)
    {
        var codeRequest = new ResendConfirmationCodeRequest
        {
            ClientId = clientId,
            Username = userName,
        };

        var response = await _cognitoService.ResendConfirmationCodeAsync(codeRequest);

        Console.WriteLine($"Method of delivery is {response.CodeDeliveryDetails.DeliveryMedium}");

        return response.CodeDeliveryDetails;
    }
```
+  For API details, see [ResendConfirmationCode](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/ResendConfirmationCode) in *AWS SDK for \.NET API Reference*\. 

### Respond to SRP authentication challenges<a name="cognito-identity-provider_RespondToAuthChallenge_csharp_topic"></a>

The following code example shows how to respond to Amazon Cognito SRP authentication challenges\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Respond to an authentication challenge.
    /// </summary>
    /// <param name="userName">The name of the user.</param>
    /// <param name="clientId">The client Id.</param>
    /// <param name="mfaCode">The multi-factor authentication code.</param>
    /// <param name="session">The current application session.</param>
    /// <returns>An async Task.</returns>
    public async Task<AuthenticationResultType> RespondToAuthChallengeAsync(string userName, string clientId, string mfaCode, string session)
    {
        Console.WriteLine("SOFTWARE_TOKEN_MFA challenge is generated");

        var challengeResponses = new Dictionary<string, string>();
        challengeResponses.Add("USERNAME", userName);
        challengeResponses.Add("SOFTWARE_TOKEN_MFA_CODE", mfaCode);

        var respondToAuthChallengeRequest = new RespondToAuthChallengeRequest
        {
            ChallengeName = ChallengeNameType.SOFTWARE_TOKEN_MFA,
            ClientId = clientId,
            ChallengeResponses = challengeResponses,
            Session = session
        };

        var response = await _cognitoService.RespondToAuthChallengeAsync(respondToAuthChallengeRequest);
        Console.WriteLine($"Response to Authentication {response.AuthenticationResult}");
        return response.AuthenticationResult;
    }
```
+  For API details, see [RespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/RespondToAuthChallenge) in *AWS SDK for \.NET API Reference*\. 

### Respond to an authentication challenge<a name="cognito-identity-provider_AdminRespondToAuthChallenge_csharp_topic"></a>

The following code example shows how to respond to an Amazon Cognito authentication challenge\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    public async Task<AuthenticationResultType> AdminRespondToAuthChallengeAsync(string userPoolId, string userName, string clientId, string mfaCode, string session)
    {
        var challengeResponses = new Dictionary<string, string>();
        challengeResponses.Add("USERNAME", userName);
        challengeResponses.Add("SOFTWARE_TOKEN_MFA_CODE", mfaCode);

        var request = new AdminRespondToAuthChallengeRequest
        {
            ClientId = clientId,
            UserPoolId = userPoolId,
            ChallengeResponses = challengeResponses,
            Session = session
        };

        var response = await _cognitoService.AdminRespondToAuthChallengeAsync(request);
        return response.AuthenticationResult;
    }
```
+  For API details, see [AdminRespondToAuthChallenge](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminRespondToAuthChallenge) in *AWS SDK for \.NET API Reference*\. 

### Sign up a user<a name="cognito-identity-provider_SignUp_csharp_topic"></a>

The following code example shows how to sign up a user with Amazon Cognito\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Sign up a new user.
    /// </summary>
    /// <param name="clientId">The client Id of the application.</param>
    /// <param name="userName">The username to use.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A Boolean value indicating whether the user was confirmed.</returns>
    public async Task<bool> SignUpAsync(string clientId, string userName, string password, String email)
    {
        var userAttrs = new AttributeType
        {
            Name = "email",
            Value = email,
        };

        var userAttrsList = new List<AttributeType>();

        userAttrsList.Add(userAttrs);

        var signUpRequest = new SignUpRequest
        {
            UserAttributes = userAttrsList,
            Username = userName,
            ClientId = clientId,
            Password = password
        };

        var response = await _cognitoService.SignUpAsync(signUpRequest);
        return response.UserConfirmed;
    }
```
+  For API details, see [SignUp](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/SignUp) in *AWS SDK for \.NET API Reference*\. 

### Start authentication with a tracked device<a name="cognito-identity-provider_InitiateAuth_csharp_topic"></a>

The following code example shows how to start authentication with a device tracked by Amazon Cognito\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Initiate authorization.
    /// </summary>
    /// <param name="clientId">The client Id of the application.</param>
    /// <param name="userName">The name of the user who is authenticating.</param>
    /// <param name="password">The password for the user who is authenticating.</param>
    /// <returns>The response from the call to InitiateAuthAsync.</returns>
    public async Task<InitiateAuthResponse> InitiateAuthAsync(string clientId, string userName, string password)
    {
        var authParameters = new Dictionary<string, string>();
        authParameters.Add("USERNAME", userName);
        authParameters.Add("PASSWORD", password);

        var authRequest = new InitiateAuthRequest

        {
            ClientId = clientId,
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };

        var response = await _cognitoService.InitiateAuthAsync(authRequest);
        Console.WriteLine($"Result Challenge is : {response.ChallengeName}");

        return response;
    }
```
+  For API details, see [InitiateAuth](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/InitiateAuth) in *AWS SDK for \.NET API Reference*\. 

### Start authentication with administrator credentials<a name="cognito-identity-provider_AdminInitiateAuth_csharp_topic"></a>

The following code example shows how to start authentication with Amazon Cognito and administrator credentials\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    public async Task<string> AdminInitiateAuthAsync(string clientId, string userPoolId, string userName, string password)
    {
        var authParameters = new Dictionary<string, string>();
        authParameters.Add("USERNAME", userName);
        authParameters.Add("PASSWORD", password);

        var request = new AdminInitiateAuthRequest
        {
            ClientId = clientId,
            UserPoolId = userPoolId,
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };

        var response = await _cognitoService.AdminInitiateAuthAsync(request);
        return response.Session;
    }
```
+  For API details, see [AdminInitiateAuth](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/AdminInitiateAuth) in *AWS SDK for \.NET API Reference*\. 

### Verify an MFA application with a user<a name="cognito-identity-provider_VerifySoftwareToken_csharp_topic"></a>

The following code example shows how to verify an MFA application with an Amazon Cognito user\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
    /// <summary>
    /// Verify the TOTP and register for MFA.
    /// </summary>
    /// <param name="session">The name of the session.</param>
    /// <param name="code">The MFA code.</param>
    /// <returns>The status of the software token.</returns>
    public async Task<VerifySoftwareTokenResponseType> VerifySoftwareTokenAsync(string session, string code)
    {
        var tokenRequest = new VerifySoftwareTokenRequest
        {
            UserCode = code,
            Session = session,
        };

        var verifyResponse = await _cognitoService.VerifySoftwareTokenAsync(tokenRequest);

        return verifyResponse.Status;
    }
```
+  For API details, see [VerifySoftwareToken](https://docs.aws.amazon.com/goto/DotNetSDKV3/cognito-idp-2016-04-18/VerifySoftwareToken) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="scenarios"></a>

### Sign up a user with a user pool that requires MFA<a name="cognito-identity-provider_Scenario_SignUpUserWithMfa_csharp_topic"></a>

The following code example shows how to:
+ Sign up a user with a user name, password, and email address\.
+ Confirm the user from a code sent in email\.
+ Set up multi\-factor authentication by associating an MFA application with the user\.
+ Sign in by using a password and an MFA code\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Cognito#code-examples)\. 
  

```
global using Amazon.CognitoIdentityProvider;
global using Amazon.CognitoIdentityProvider.Model;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Console;
global using Microsoft.Extensions.Logging.Debug;


namespace CognitoBasics;

public class CognitoBasics
{
    private static ILogger logger = null!;

    static async Task Main(string[] args)
    {
        // Set up dependency injection for Amazon Cognito.
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
                logging.AddFilter("System", LogLevel.Debug)
                    .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Information)
                    .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Trace))
            .ConfigureServices((_, services) =>
            services.AddAWSService<IAmazonCognitoIdentityProvider>()
            .AddTransient<CognitoWrapper>()
            .AddTransient<UiMethods>()
            )
            .Build();

        logger = LoggerFactory.Create(builder => { builder.AddConsole(); })
            .CreateLogger<CognitoBasics>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("settings.json") // Load settings from .json file.
            .AddJsonFile("settings.local.json",
                true) // Optionally load local settings.
            .Build();

        var cognitoWrapper = host.Services.GetRequiredService<CognitoWrapper>();
        var uiMethods = host.Services.GetRequiredService<UiMethods>();

        // clientId - The app client Id value that you get from the AWS CDK script.
        string clientId = configuration["ClientId"]; // "*** REPLACE WITH CLIENT ID VALUE FROM CDK SCRIPT";

        // poolId - The pool Id that you get from the AWS CDK script.
        string poolId = configuration["PoolId"]; // "*** REPLACE WITH POOL ID VALUE FROM CDK SCRIPT";
        var userName = configuration["UserName"];
        var password = configuration["Password"];
        var email = configuration["Email"];
        var userPoolId = configuration["UserPoolId"];

        // If the username wasn't set in the configuration file,
        // get it from the user now.
        if (userName is null)
        {
            do
            {
                Console.Write("Username: ");
                userName = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(userName));
        }
        Console.WriteLine($"\nUsername: {userName}");

        // If the password wasn't set in the configuration file,
        // get it from the user now.
        if (password is null)
        {
            do
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(password));
        }

        // If the email address wasn't set in the configuration file,
        // get it from the user now.
        if (email is null)
        {
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
            } while (string.IsNullOrEmpty(email));
        }

        // Now sign up the user.
        Console.WriteLine($"\nSigning up {userName} with email address: {email}");
        await cognitoWrapper.SignUpAsync(clientId, userName, password, email);

        // Add the user to the user pool.
        Console.WriteLine($"Adding {userName} to the user pool");
        await cognitoWrapper.GetAdminUserAsync(userName, poolId);

        uiMethods.DisplayTitle("Get confirmation code");
        Console.WriteLine($"Conformation code sent to {userName}.");
        Console.Write("Would you like to send a new code? (Yes/No) ");
        var answer = Console.ReadLine();

        if (answer.ToLower() == "YES")
        {
            await cognitoWrapper.ResendConfirmationCodeAsync(clientId, userName);
            Console.WriteLine("Sending a new confirmation code");
        }

        Console.Write("Enter confirmation code (from Email): ");
        string code = Console.ReadLine();

        await cognitoWrapper.ConfirmSignupAsync(clientId, code, userName);

        uiMethods.DisplayTitle("Checking status");
        Console.WriteLine($"Rechecking the status of {userName} in the user pool");
        await cognitoWrapper.GetAdminUserAsync(userName, poolId);

        var authResponse = await cognitoWrapper.InitiateAuthAsync(clientId, userName, password);
        var mySession = authResponse.Session;

        var newSession = await cognitoWrapper.AssociateSoftwareTokenAsync(mySession);

        Console.Write("Enter the 6-digit code displayed in Google Authenticator: ");
        string myCode = Console.ReadLine();

        // Verify the TOTP and register for MFA.
        await cognitoWrapper.GetAdminUserAsync(newSession, myCode);
        Console.Write("Re-enter the 6-digit code displayed in your authenticator");
        string mfaCode = Console.ReadLine();

        var session2 = await cognitoWrapper.AdminInitiateAuthAsync(clientId, userPoolId, userName, password);
        await cognitoWrapper.RespondToAuthChallengeAsync(userName, clientId, mfaCode, session2);
    }
}


using System.Net;

namespace CognitoActions;

/// <summary>
/// Methods to perform Amazon Cognito Identity Provider actions.
/// </summary>
public class CognitoWrapper
{
    private readonly IAmazonCognitoIdentityProvider _cognitoService;

    /// <summary>
    /// Constructor for the wrapper class containing Amazon Cognito actions.
    /// </summary>
    /// <param name="cognitoService">The Amazon Cognito client object.</param>
    public CognitoWrapper(IAmazonCognitoIdentityProvider cognitoService)
    {
        _cognitoService = cognitoService;
    }

    /// <summary>
    /// List the Amazon Cognito user pools for an account.
    /// </summary>
    /// <returns>A list of UserPoolDescriptionType objects.</returns>
    public async Task<List<UserPoolDescriptionType>> ListUserPoolsAsync()
    {
        var userPools = new List<UserPoolDescriptionType>();

        var userPoolsPaginator = _cognitoService.Paginators.ListUserPools(new ListUserPoolsRequest());

        await foreach (var response in userPoolsPaginator.Responses)
        {
            userPools.AddRange(response.UserPools);
        }

        return userPools;
    }


    /// <summary>
    /// Get a list of users for the Amazon Cognito user pool.
    /// </summary>
    /// <param name="userPoolId">The user pool Id.</param>
    /// <returns>A list of users.</returns>
    public async Task<List<UserType>> ListUsersAsync(string userPoolId)
    {
        var request = new ListUsersRequest
        {
            UserPoolId = userPoolId
        };

        var users = new List<UserType>();

        var usersPaginator = _cognitoService.Paginators.ListUsers(request);
        await foreach (var response in usersPaginator.Responses)
        {
            users.AddRange(response.Users);
        }

        return users;
    }


    public async Task<AuthenticationResultType> AdminRespondToAuthChallengeAsync(string userPoolId, string userName, string clientId, string mfaCode, string session)
    {
        var challengeResponses = new Dictionary<string, string>();
        challengeResponses.Add("USERNAME", userName);
        challengeResponses.Add("SOFTWARE_TOKEN_MFA_CODE", mfaCode);

        var request = new AdminRespondToAuthChallengeRequest
        {
            ClientId = clientId,
            UserPoolId = userPoolId,
            ChallengeResponses = challengeResponses,
            Session = session
        };

        var response = await _cognitoService.AdminRespondToAuthChallengeAsync(request);
        return response.AuthenticationResult;
    }


    /// <summary>
    /// Respond to an authentication challenge.
    /// </summary>
    /// <param name="userName">The name of the user.</param>
    /// <param name="clientId">The client Id.</param>
    /// <param name="mfaCode">The multi-factor authentication code.</param>
    /// <param name="session">The current application session.</param>
    /// <returns>An async Task.</returns>
    public async Task<AuthenticationResultType> RespondToAuthChallengeAsync(string userName, string clientId, string mfaCode, string session)
    {
        Console.WriteLine("SOFTWARE_TOKEN_MFA challenge is generated");

        var challengeResponses = new Dictionary<string, string>();
        challengeResponses.Add("USERNAME", userName);
        challengeResponses.Add("SOFTWARE_TOKEN_MFA_CODE", mfaCode);

        var respondToAuthChallengeRequest = new RespondToAuthChallengeRequest
        {
            ChallengeName = ChallengeNameType.SOFTWARE_TOKEN_MFA,
            ClientId = clientId,
            ChallengeResponses = challengeResponses,
            Session = session
        };

        var response = await _cognitoService.RespondToAuthChallengeAsync(respondToAuthChallengeRequest);
        Console.WriteLine($"Response to Authentication {response.AuthenticationResult}");
        return response.AuthenticationResult;
    }


    /// <summary>
    /// Verify the TOTP and register for MFA.
    /// </summary>
    /// <param name="session">The name of the session.</param>
    /// <param name="code">The MFA code.</param>
    /// <returns>The status of the software token.</returns>
    public async Task<VerifySoftwareTokenResponseType> VerifySoftwareTokenAsync(string session, string code)
    {
        var tokenRequest = new VerifySoftwareTokenRequest
        {
            UserCode = code,
            Session = session,
        };

        var verifyResponse = await _cognitoService.VerifySoftwareTokenAsync(tokenRequest);

        return verifyResponse.Status;
    }


    /// <summary>
    /// Get an MFA token to authenticate the user with the authenticator.
    /// </summary>
    /// <param name="session">The session name.</param>
    /// <returns>Returns the session name.</returns>
    public async Task<string> AssociateSoftwareTokenAsync(string session)
    {
        var softwareTokenRequest = new AssociateSoftwareTokenRequest
        {
            Session = session,
        };

        var tokenResponse = await _cognitoService.AssociateSoftwareTokenAsync(softwareTokenRequest);
        var secretCode = tokenResponse.SecretCode;

        Console.Write("Enter the following token into the authenticator: {secretCode}");

        return tokenResponse.Session;
    }


    public async Task<string> AdminInitiateAuthAsync(string clientId, string userPoolId, string userName, string password)
    {
        var authParameters = new Dictionary<string, string>();
        authParameters.Add("USERNAME", userName);
        authParameters.Add("PASSWORD", password);

        var request = new AdminInitiateAuthRequest
        {
            ClientId = clientId,
            UserPoolId = userPoolId,
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };

        var response = await _cognitoService.AdminInitiateAuthAsync(request);
        return response.Session;
    }

    /// <summary>
    /// Initiate authorization.
    /// </summary>
    /// <param name="clientId">The client Id of the application.</param>
    /// <param name="userName">The name of the user who is authenticating.</param>
    /// <param name="password">The password for the user who is authenticating.</param>
    /// <returns>The response from the call to InitiateAuthAsync.</returns>
    public async Task<InitiateAuthResponse> InitiateAuthAsync(string clientId, string userName, string password)
    {
        var authParameters = new Dictionary<string, string>();
        authParameters.Add("USERNAME", userName);
        authParameters.Add("PASSWORD", password);

        var authRequest = new InitiateAuthRequest

        {
            ClientId = clientId,
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };

        var response = await _cognitoService.InitiateAuthAsync(authRequest);
        Console.WriteLine($"Result Challenge is : {response.ChallengeName}");

        return response;
    }


    /// <summary>
    /// Confirm that the user has signed up.
    /// </summary>
    /// <param name="clientId">The Id of this application.</param>
    /// <param name="code">The confirmation code sent to the user.</param>
    /// <param name="userName">The username.</param>
    /// <returns></returns>
    public async Task<bool> ConfirmSignupAsync(string clientId, string code, string userName)
    {
        var signUpRequest = new ConfirmSignUpRequest
        {
            ClientId = clientId,
            ConfirmationCode = code,
            Username = userName,
        };

        var response = await _cognitoService.ConfirmSignUpAsync(signUpRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"{userName} was confirmed");
            return true;
        }
        return false;
    }


    /// <summary>
    /// Initiates and confirms tracking of the device.
    /// </summary>
    /// <param name="accessToken">The user's access token.</param>
    /// <param name="deviceKey">The key of the device from Amazon Cognito.</param>
    /// <param name="deviceName">The device name.</param>
    /// <returns></returns>
    public async Task<bool> ConfirmDeviceAsync(string accessToken, string deviceKey, string deviceName)
    {
        var request = new ConfirmDeviceRequest
        {
            AccessToken = accessToken,
            DeviceKey = deviceKey,
            DeviceName = deviceName
        };

        var response = await _cognitoService.ConfirmDeviceAsync(request);
        return response.UserConfirmationNecessary;
    }


    /// <summary>
    /// Send a new confirmation code to a user.
    /// </summary>
    /// <param name="clientId">The Id of the client application.</param>
    /// <param name="userName">The username of user who will receive the code.</param>
    /// <returns></returns>
    public async Task<CodeDeliveryDetailsType> ResendConfirmationCodeAsync(string clientId, string userName)
    {
        var codeRequest = new ResendConfirmationCodeRequest
        {
            ClientId = clientId,
            Username = userName,
        };

        var response = await _cognitoService.ResendConfirmationCodeAsync(codeRequest);

        Console.WriteLine($"Method of delivery is {response.CodeDeliveryDetails.DeliveryMedium}");

        return response.CodeDeliveryDetails;
    }


    /// <summary>
    /// Get the specified user from an Amazon Cognito user pool with administrator access.
    /// </summary>
    /// <param name="userName">The name of the user.</param>
    /// <param name="poolId">The Id of the Amazon Cognito user pool.</param>
    /// <returns></returns>
    public async Task<UserStatusType> GetAdminUserAsync(string userName, string poolId)
    {
        AdminGetUserRequest userRequest = new AdminGetUserRequest
        {
            Username = userName,
            UserPoolId = poolId,
        };

        var response = await _cognitoService.AdminGetUserAsync(userRequest);

        Console.WriteLine($"User status {response.UserStatus}");
        return response.UserStatus;
    }


    /// <summary>
    /// Sign up a new user.
    /// </summary>
    /// <param name="clientId">The client Id of the application.</param>
    /// <param name="userName">The username to use.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A Boolean value indicating whether the user was confirmed.</returns>
    public async Task<bool> SignUpAsync(string clientId, string userName, string password, String email)
    {
        var userAttrs = new AttributeType
        {
            Name = "email",
            Value = email,
        };

        var userAttrsList = new List<AttributeType>();

        userAttrsList.Add(userAttrs);

        var signUpRequest = new SignUpRequest
        {
            UserAttributes = userAttrsList,
            Username = userName,
            ClientId = clientId,
            Password = password
        };

        var response = await _cognitoService.SignUpAsync(signUpRequest);
        return response.UserConfirmed;
    }

}


namespace CognitoBasics;

/// <summary>
/// Some useful methods to make screen display easier.
/// </summary>
public class UiMethods
{
    public readonly string SepBar = new string('-', Console.WindowWidth);

    /// <summary>
    /// Show information about the scenario.
    /// </summary>
    public void DisplayOverview()
    {
        Console.Clear();
        DisplayTitle("Welcome to the Amazon Cognito Demo");

        Console.WriteLine("This example application does the following:");
        Console.WriteLine("\t 1. Signs up a user.");
        Console.WriteLine("\t 2. Gets the user's confirmation status.");
        Console.WriteLine("\t 3. Resends the confirmation code if the user requested another code.");
        Console.WriteLine("\t 4. Confirms that the user signed up.");
        Console.WriteLine("\t 5. Invokes the initiateAuth to sign in. This results in being prompted to set up TOTP (time-based one-time password). (The response is “ChallengeName”: “MFA_SETUP”).");
        Console.WriteLine("\t 6. Invokes the AssociateSoftwareToken method to generate a TOTP MFA private key. This can be used with Google Authenticator.");
        Console.WriteLine("\t 7. Invokes the VerifySoftwareToken method to verify the TOTP and register for MFA.");
        Console.WriteLine("\t 8. Invokes the AdminInitiateAuth to sign in again. This results in being prompted to submit a TOTP (Response: “ChallengeName”: “SOFTWARE_TOKEN_MFA”).");
        Console.WriteLine("\t 9. Invokes the AdminRespondToAuthChallenge to get back a token.");
    }

    /// <summary>
    /// Display a message and wait until the user presses enter.
    /// </summary>
    public void PressEnter()
    {
        Console.Write("\nPress <Enter> to continue.");
        _ = Console.ReadLine();
    }

    /// <summary>
    /// Pad a string with spaces to center it on the console display.
    /// </summary>
    /// <param name="strToCenter">The string to pad with spaces.</param>
    /// <returns>The padded string.</returns>
    public string CenterString(string strToCenter)
    {
        var padAmount = (Console.WindowWidth - strToCenter.Length) / 2;
        var leftPad = new string(' ', padAmount);
        return $"{leftPad}{strToCenter}";
    }

    /// <summary>
    /// Display a line of hyphens, the centered text of the title and another
    /// line of hyphens.
    /// </summary>
    /// <param name="strTitle">The string to be displayed.</param>
    public void DisplayTitle(string strTitle)
    {
        Console.WriteLine(SepBar);
        Console.WriteLine(CenterString(strTitle));
        Console.WriteLine(SepBar);
    }
}
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