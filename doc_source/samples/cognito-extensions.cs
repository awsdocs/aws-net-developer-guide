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

namespace AwsConsoleApp1
{
    class Program
    {
        public static string accessToken;
        public static void Main(string[] args)

        {

            //Set up CognitoAWSCredentials

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

            //Use AWS as an Unautheticated User

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

            // One more example

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
        }

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
    }
}