.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cognito-authentication-extension:

#######################################################
Amazon CognitoAuthentication Extension Library Examples
#######################################################

.. meta::
   :description: Use Cognito to create user identities and authenticate identities.
   :keywords: AWS SDK for .NET examples, {Cognito}

The CognitoAuthentication extension library simplifies the authentication process of |COG| user pools
for .NET Core and Xamarin developers. The library is built on top of the Amazon Cognito Identity
Provider API to create and send user authentication API calls. You can get
**Amazon.Extensions.CognitoAuthentication** from the NuGet gallery.

Using the CognitoAuthentication Extension Library
=================================================

|COG| has some built-in :code:`AuthFlow` and :code:`ChallengeName` values for a standard authentication flow to validate username and password through the Secure Remote Password (SRP). For more information about authentication flow, see :COG-dg:`Amazon Cognito User Pool Authentication Flow <amazon-cognito-user-pools-authentication-flow>`.

The following examples require these :code:`using` statements:

.. literalinclude:: samples/cognito-extensions.cs
   :lines: 1-10
   :dedent: 8
   :language: csharp

Use Basic Authentication
------------------------

Create an :sdk-net-api:`AmazonCognitoIdentityProviderClient <CognitoIdentityProvider/TCognitoIdentityProviderClient>` using :sdk-net-api:`AnonymousAWSCredentials <Runtime/TAnonymousAWSCredentials>`, which do not require signed requests. You do not need to supply a region, the underlying code calls :code:`FallbackRegionFactory.GetRegionEndpoint()` if a region is not provided. Create :code:`CognitoUserPool` and  :code:`CognitoUser` objects. Call the :code:`StartWithSrpAuthAsync` method with an :code:`InitiateSrpAuthRequest` that contains the user password.

.. literalinclude:: samples/cognito-extensions.cs
   :dedent: 8
   :lines: 79-93
   :language: csharp

Authenticate with Challenges
------------------------------

Continuing the authentication flow with challenges, such as with NewPasswordRequired and Multi-Factor Authentication (MFA), is also simpler. The only requirements are the CognitoAuthentication objects, user's password for SRP, and the necessary information for the next challenge, which is acquired after prompting the user to enter it. The following code shows one way to check the challenge type and get the appropriate responses for MFA and NewPasswordRequired challenges during the authentication flow.

Do a basic authentication request as before, and :code:`await` an :code:`AuthFlowResponse`. When the response is received loop through the returned :code:`AuthenticationResult` object. If the :code:`ChallengeName` type is :code:`NEW_PASSWORD_REQUIRED`, call the :code:`RespondToNewPasswordRequiredAsync` method.

.. literalinclude:: samples/cognito-extensions.cs
   :dedent: 8
   :lines: 96-152
   :language: csharp

Use AWS Resources after Authentication
----------------------------------------

Once a user is authenticated using the CognitoAuthentication library, the next step is to allow the user  to access the appropriate AWS resources. To do this you must create an identity pool through the |COG| Federated Identities console. By specifying the |COG| user pool you created as a provider, using its poolID and clientID, you can allow your |COG| user pool users to access AWS resources connected to your account. You can also specify different roles to enable both unauthenticated and authenticated users to access different resources. You can change these rules in the IAM console, where you can add or remove permissions in the :guilabel:`Action` field of the role's attached policy. Then, using the appropriate identity pool, user pool, and |COG| user information, you can make calls to different AWS resources. The following example  shows a user authenticated with SRP accessing the different |S3| buckets permitted by the associated identity pool's role

.. literalinclude:: samples/cognito-extensions.cs
   :dedent: 8
   :lines: 154-180
   :language: csharp

More Authentication Options
===========================

In addition to SRP, NewPasswordRequired, and MFA, the CognitoAuthentication extension library offers an easier authentication flow for:

* Custom - Initiate with a call to :code:`StartWithCustomAuthAsync(InitiateCustomAuthRequest customRequest)`
* RefreshToken - Initiate with a call to :code:`StartWithRefreshTokenAuthAsync(InitiateRefreshTokenAuthRequest
  refreshTokenRequest)`
* RefreshTokenSRP - Initiate with a call to :code:`StartWithRefreshTokenAuthAsync(InitiateRefreshTokenAuthRequest
  refreshTokenRequest)`
* AdminNoSRP - Initiate with a call to :code:`StartWithAdminNoSrpAuthAsync(InitiateAdminNoSrpAuthRequest adminAuthRequest)`

Call the appropriate method depending on the flow you want. Then continue prompting the user with challenges as they are presented in the :code:`AuthFlowResponse` objects of each method call. Also call the appropriate response method, such as :code:`RespondToSmsMfaAuthAsync` for MFA challenges and :code:`RespondToCustomAuthAsync` for custom challenges.
