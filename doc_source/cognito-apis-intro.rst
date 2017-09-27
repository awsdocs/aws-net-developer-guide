.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cognito-apis-intro:

########################################
Authenticating Users with Amazon Cognito
########################################


.. meta::
   :description: Use Cognito to create user identities and authenticate identities.
   :keywords: AWS SDK for .NET examples, {Cognito}

Using |COGID|, you can create unique identities for your users and authenticate them for secure access 
to your AWS resources such as |S3| or |DDBlong|. |COGID| supports public identity providers such as Amazon, 
Facebook, Twitter/Digits, Google, or any OpenID Connect-compatible provider as well as unauthenticated identities. |COG| also supports `developer authenticated identities <https://aws.amazon.com/blogs/mobile/amazon-cognito-announcing-developer-authenticated-identities/>`_, which let you register and authenticate users using your own backend authentication process, while still using |COGSYNC| to synchronize user data and access AWS resources.

For more information on `Amazon Cognito <https://aws.amazon.com/cognito/>`_, see the :COG-dg:`Amazon Cognito Developer Guide <what-is-amazon-cognito>`

The following code examples show how to easily use |COGID|. The *Amazon Cognito Credentials Provider* example shows how to create and authenticate user identities. The *Amazon CognitoAuthentication Extension Library* example shows how to use the CognitoAuthentication extension library to authenticate |COG| user pools.

.. toctree::
    :titlesonly:
    :maxdepth: 1
    
    cognito-creds-provider
    cognito-authentication-extension

