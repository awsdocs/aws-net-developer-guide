.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-access-keys:


##########################
Managing |IAM| Access Keys
##########################

.. meta::
   :description: Use this .NET code example to learn how to manage access keys in IAM.
   :keywords: AWS SDK for .NET examples, IAM access keys

These .NET examples shows you how to:

* Create an access key for a user
* Get the date that an access key was last used
* Update the status for an access key
* Delete an access key

The Scenario
============

Users need their own access keys to make programmatic calls to AWS from the |sdk-net|. To meet this need,
you can create, modify, view, or rotate access keys (access key IDs and secret access keys) for |IAM| users.
When you create an access key, its status is Active by default, which means the user can use the access
key for API calls.

The C# code uses the |sdk-net| to manage |IAM| access keys
using these methods of the :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` class:

* :sdk-net-api:`CreateAccessKey <IAM/MIAMIAMServiceCreateAccessKeyCreateAccessKeyRequest>`
* :sdk-net-api:`ListAccessKeys <IAM/MIAMIAMServiceListAccessKeysListAccessKeysRequest>`
* :sdk-net-api:`GetAccessKeyLastUsed <IAM/MIAMIAMServiceGetAccessKeyLastUsedGetAccessKeyLastUsedRequest>`
* :sdk-net-api:`UpdateAccessKey <IAM/MIAMIAMServiceUpdateAccessKeyUpdateAccessKeyRequest>`
* :sdk-net-api:`DeleteAccessKey <IAM/MIAMIAMServiceDeleteAccessKeyDeleteAccessKeyRequest>`

For more information about |IAM| access keys, see :iam-ug:`Managing Access Keys for IAM Users <id_credentials_access-keys>` in the |IAM-ug|.

Create Access Keys for a User
=============================

Call the :code:`CreateAccessKey` method to create an access key named :code:`S3UserReadOnlyAccess` for 
the |IAM| access keys examples. The :code:`CreateAccessKey method first creates a user named 
:code:`S3UserReadOnlyAccess` with read only access rights by calling the :code:`CreateUser` method. 
It then creates an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` 
object and a :sdk-net-api:`CreateAccessKeyRequest <IAM/TIAMCreateAccessKeyRequest>` object containing 
the :code:`UserName` parameter needed to create new access keys. It then calls the 
:sdk-net-api:`CreateAccessKey <IAM/MIAMIAMServiceCreateAccessKeyCreateAccessKeyRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

        public static void CreateAccessKey()
        {
            try
            {
                CreateUser();
                var iamClient = new AmazonIdentityManagementServiceClient();
                // Create an access key for the IAM user that can be used by the SDK
                var accessKey = iamClient.CreateAccessKey(new CreateAccessKeyRequest
                {
                    // Use the user created in the CreateUser example
                    UserName = "S3UserReadOnlyAccess"
                }).AccessKey;

            }
            catch (LimitExceededException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static User CreateUser()
        {
            var iamClient = new AmazonIdentityManagementServiceClient();
            try
            {
                // Create the IAM user
                var readOnlyUser = iamClient.CreateUser(new CreateUserRequest
                {
                    UserName = "S3UserReadOnlyAccess"
                }).User;

                // Assign the read-only policy to the new user
                iamClient.PutUserPolicy(new PutUserPolicyRequest
                {
                    UserName = readOnlyUser.UserName,
                    PolicyName = "S3ReadOnlyAccess",
                    PolicyDocument = S3_READONLY_POLICY
                });
                return readOnlyUser;
            }
            catch (EntityAlreadyExistsException e)
            {
                Console.WriteLine(e.Message);
                var request = new GetUserRequest()
                {
                    UserName = "S3UserReadOnlyAccess"
                };

                return iamClient.GetUser(request).User;

            }
        }


List a User's Access Keys
=========================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object and 
a :sdk-net-api:`ListAccessKeysRequest <IAM/TIAMListAccessKeysRequest>` object containing the parameters needed to
retrieve the user's access keys. This includes the |IAM| user's name and, optionally, the maximum number
of access key pairs you want to list. Call the :sdk-net-api:`ListAccessKeys <IAM/MIAMIAMServiceListAccessKeysListAccessKeysRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

        public static void ListAccessKeys()
        {

            var iamClient = new AmazonIdentityManagementServiceClient();
            var requestAccessKeys = new ListAccessKeysRequest
            {
                // Use the user created in the CreateAccessKey example
                UserName = "S3UserReadOnlyAccess",
                MaxItems = 10
            };
            var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
            Console.WriteLine("  Access keys:");

            foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
            {
                Console.WriteLine("    {0}", accessKey.AccessKeyId);
             }
        }




Get the Last Used Date for Access Keys
======================================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object and a
:sdk-net-api:`ListAccessKeysRequest <IAM/TIAMListAccessKeysRequest>` object containing the :code:`UserName`
parameter needed to list the access keys. Call the :sdk-net-api:`ListAccessKeys <IAM/MIAMIAMServiceListAccessKeysListAccessKeysRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object. Loop through the access keys returned,
displaying the :code:`AccessKeyId` of each key and using it to create a :sdk-net-api:`GetAccessKeyLastUsedRequest <IAM/TIAMGetAccessKeyLastUsedRequest>`
object. Call the :sdk-net-api:`GetAccessKeyLastUsed <IAM/MIAMIAMServiceGetAccessKeyLastUsedGetAccessKeyLastUsedRequest>`
method and display the time that the key was last used on the console.

.. code-block:: c#

        public static void GetAccessKeysLastUsed()
        {

            var iamClient = new AmazonIdentityManagementServiceClient();
            var requestAccessKeys = new ListAccessKeysRequest
            {
                // Use the user we created in the CreateUser example
                UserName = "S3UserReadOnlyAccess"
            };
            var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
            Console.WriteLine("  Access keys:");

            foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
            {
                Console.WriteLine("    {0}", accessKey.AccessKeyId);
                GetAccessKeyLastUsedRequest request = new GetAccessKeyLastUsedRequest()
                    { AccessKeyId = accessKey.AccessKeyId };
                var response = iamClient.GetAccessKeyLastUsed(request);
                Console.WriteLine("Key last used " + response.AccessKeyLastUsed.LastUsedDate.ToLongDateString());
            }
        }




Update the Status of an Access Key
==================================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object and a
:sdk-net-api:`ListAccessKeysRequest <IAM/TIAMListAccessKeysRequest>` object containing the user name to
list the keys for. The user name in this example is the user created for the other examples. Call
the :sdk-net-api:`ListAccessKeys <IAM/MIAMIAMServiceListAccessKeysListAccessKeysRequest>` method of the
:code:`AmazonIdentityManagementServiceClient`. The :sdk-net-api:`ListAccessKeysResponse <IAM/TIAMListAccessKeysResponse>`
that is returned contains a list of the access keys for that user. Use the first access key in the list.
Create an :sdk-net-api:`UpdateAccessKeyRequest <IAM/TIAMUpdateAccessKeyRequest>` object, providing 
the :code:`UserName`, :code:`AccessKeyId`, and :code:`Status` parameters. Call the 
:sdk-net-api:`UpdateAccessKey <IAM/MIAMIAMServiceUpdateAccessKeyUpdateAccessKeyRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

        public static void UpdateKeyStatus()
        {
            // This example changes the status of the key specified by its index in the list of access keys
            // Optionally, you could change the keynumber parameter to be an AccessKey ID
            var iamClient = new AmazonIdentityManagementServiceClient();
            var requestAccessKeys = new ListAccessKeysRequest
            {
                UserName = "S3UserReadOnlyAccess"
            };
            var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
            UpdateAccessKeyRequest updateRequest = new UpdateAccessKeyRequest
                {
                    UserName = "S3UserReadOnlyAccess",
                    AccessKeyId = responseAccessKeys.AccessKeyMetadata[0].AccessKeyId,
                    Status = StatusType.Active
                };
            iamClient.UpdateAccessKey(updateRequest);
            Console.WriteLine("  Access key " + updateRequest.AccessKeyId + " updated");
        }


Delete Access Keys
==================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object and a
:sdk-net-api:`ListAccessKeysRequest <IAM/TIAMListAccessKeysRequest>` object containing the name of the 
user as a parameter. Call the :sdk-net-api:`ListAccessKeys <IAM/MIAMIAMServiceListAccessKeysListAccessKeysRequest>` 
method of the :code:`AmazonIdentityManagementServiceClient`. The 
:sdk-net-api:`ListAccessKeysResponse <IAM/TIAMListAccessKeysResponse>` that is returned contains a list 
of the access keys for that user. Delete each access key in the list by 
calling the :sdk-net-api:`DeleteAccessKey <IAM/MIAMIAMServiceDeleteAccessKeyDeleteAccessKeyRequest>` 
method of the :code:`AmazonIdentityManagementServiceClient`.

.. code-block:: c#

        public static void DeleteAccessKeys()
        {
        // Delete all the access keys created for the examples
            var iamClient = new AmazonIdentityManagementServiceClient();
            var requestAccessKeys = new ListAccessKeysRequest
            {
                // Use the user created in the CreateUser example
                UserName = "S3UserReadOnlyAccess"
            };
            var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
            Console.WriteLine("  Access keys:");

            foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
            {
                Console.WriteLine("    {0}", accessKey.AccessKeyId);
                iamClient.DeleteAccessKey(new DeleteAccessKeyRequest
                {
                    UserName = "S3UserReadOnlyAccess",
                    AccessKeyId = accessKey.AccessKeyId
                });
                Console.WriteLine("Access Key " + accessKey.AccessKeyId + " deleted");
            }

        }
