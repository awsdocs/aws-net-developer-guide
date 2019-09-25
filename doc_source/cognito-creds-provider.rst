.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cognito-cred-provider:

###################################
Amazon Cognito Credentials Provider
###################################

.. meta::
   :description: Use Cognito to create user identities and authenticate identities.
   :keywords: AWS SDK for .NET examples, {Cognito}

:code:`Amazon.CognitoIdentity.CognitoAWSCredentials` is a credentials object that uses |COG| and the AWS Security Token Service (|STS|) to retrieve credentials to make AWS calls.

The first step in setting up :code:`CognitoAWSCredentials` is to create an "identity pool". (An identity pool is a store of user identity information that is specific to your account. The information is retrievable across client platforms, devices, and operating systems, so that if a user starts using the app on a phone and later switches to a tablet, the persisted app information is still available for that user. You can create a new identity pool from the |COG| console. If you are using the console, it will also provide you the other pieces of information you need:

* Your account number- A 12-digit number, such as 123456789012, that is unique to your account.
* The unauthenticated role ARN- A role that unauthenticated users will assume. For example, this role can  provide read-only permissions to your data.
* The authenticated role ARN- A role that authenticated users will assume. This role can provide more extensive permissions to your data.

Set up CognitoAWSCredentials
============================

The following code example shows how to set up :code:`CognitoAWSCredentials`, which you can then use to make a call to |S3| as an unauthenticated user. This enables you to make calls with just a minimum amount of data required to authenticate the user. User permissions are controlled by the role, so you can  configure access as you need.
 
.. literalinclude:: samples/cognito-extensions.cs
   :lines: 23-32
   :dedent: 12
   :language: csharp

Use AWS as an Unauthenticated User
==================================
    
The following code example shows how you can start using AWS as an unauthenticated user, then authenticate through Facebook and update the credentials to use Facebook credentials. Using this approach, you can grant different capabilities to authenticated users via the authenticated role. For instance, you might have a  phone application that permits users to view content anonymously, but allows them to post if they are logged on with one or more of the configured providers.

.. literalinclude:: samples/cognito-extensions.cs
   :lines: 36-55
   :dedent: 12
   :language: csharp

The :code:`CognitoAWSCredentials` object provides even more functionality if you use it with the :code:`AmazonCognitoSyncClient` that is part of the |sdk-net|. If you're using both :code:`AmazonCognitoSyncClient` and :code:`CognitoAWSCredentials`, you don’t have to specify the :code:`IdentityPoolId` and :code:`IdentityId` properties when making calls with the :code:`AmazonCognitoSyncClient`. These properties are automatically filled in from :code:`CognitoAWSCredentials`. The next code example illustrates this, as well as an event that notifies you whenever the :code:`IdentityId` for :code:`CognitoAWSCredentials` changes. The :code:`IdentityId` can change in some cases, such as when changing from an unauthenticated user to an authenticated one.

.. literalinclude:: samples/cognito-extensions.cs
   :lines: 59-76
   :dedent: 12
   :language: csharp
