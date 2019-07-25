.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-managing-users:


####################
Managing |IAM| Users
####################

.. meta::
   :description: Use this .NET code example to list, create, delete, and update IAM users.
   :keywords: AWS SDK for .NET examples, IAM users


This .NET example shows you how to retrieve a list of |IAM| users, create and delete |IAM| users,
and update an |IAM| user name.

You can create and manage users in |IAM| using these methods of the
:sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` class:

* :sdk-net-api:`CreateUser <IAM/MIAMServiceCreateUserCreateUserRequest>`
* :sdk-net-api:`ListUsers <IAM/MIAMServiceListUsersListUsersRequest>`
* :sdk-net-api:`UpdateUser <IAM/MIAMServiceUpdateUserUpdateUserRequest>`
* :sdk-net-api:`GetUser <IAM/MIAMServiceGetUserGetUserRequest>`
* :sdk-net-api:`DeleteUser <IAM/MIAMServiceDeleteUserDeleteUserRequest>`


For more information about |IAM| users, see :iam-ug:`IAM Users <id_users>`
in the |IAM-ug|.

For information about limitations on the number of IAM users you can create, see
:iam-ug:`Limitations on IAM Entities <iam-limits.html>`
in the |IAM-ug|.

Create a User for Your AWS Account
==================================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object. Next,
create a :sdk-net-api:`CreateUserRequest <IAM/TCreateUserRequest>` object containing the user
name you want to use for the new user. Call the
:sdk-net-api:`CreateUser <IAM/MIAMServiceCreateUserCreateUserRequest>`
method of the :code:`AmazonIAMClient` object. If the user name doesn't currently exist, display the name
and the ARN for the user on the console. If the name already exists, write a message to that
effect to the console.

.. code-block:: c#

            var client = new AmazonIdentityManagementServiceClient();
            var request = new CreateUserRequest
            {
                UserName = "DemoUser"
            };

            try
            {
                var response = client.CreateUser(request);

                Console.WriteLine("User Name = '{0}', ARN = '{1}'",
                  response.User.UserName, response.User.Arn);
            }
            catch (EntityAlreadyExistsException)
            {
                Console.WriteLine("User 'DemoUser' already exists.");
            }


List Users in Your AWS Account
==============================

This example lists the |IAM| users that have the specified path prefix. If no path prefix is specified,
the
action returns all users in the AWS account. If there are no users, the action returns an empty list.

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object. Next,
create a :sdk-net-api:`ListUsersRequest <IAM/TListUsersRequest>` object containing the
parameters needed to list your users. Limit the number returned by setting the :code:`MaxItems` parameter
to 10. Call the :sdk-net-api:`ListUsers <IAM/MIAMServiceListUsersListUsersRequest>` method of the
:code:`AmazonIdentityManagementServiceClient` object. Write each user's name and creation date to the console.


.. code-block:: c#

        public static void ListUsers()
        {
            var iamClient = new AmazonIdentityManagementServiceClient();
            var requestUsers = new ListUsersRequest() { MaxItems = 10 };
            var responseUsers = iamClient.ListUsers(requestUsers);

            foreach (var user in responseUsers.Users)
            {
                Console.WriteLine("User " + user.UserName  + " Created: " + user.CreateDate.ToShortDateString());
            }

        }



Update a User's Name
====================

This example shows how to update the name or the path of the specified |IAM| user. Be sure you understand
the implications of changing an |IAM| user's path or name. For more information, see
:iam-ug:`Renaming an IAM User <id_users_renaming>` in the |IAM-ug|.

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object.
Next, create an :sdk-net-api:`UpdateUserRequest <IAM/TUpdateUserRequest>` object, specifying both the
current and new user names as parameters. Call the :sdk-net-api:`UpdateUser <IAM/MIAMServiceUpdateUserUpdateUserRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

         public static void UpdateUser()
        {
            var client = new AmazonIdentityManagementServiceClient();
            var request = new UpdateUserRequest
            {
                UserName = "DemoUser",
                NewUserName = "NewUser"
            };

            try
            {
                var response = client.UpdateUser(request);

            }
            catch (EntityAlreadyExistsException)
            {
                Console.WriteLine("User 'NewUser' already exists.");
            }
        }



Get Information about a User
============================

This example shows how to retrieve information about the specified |IAM| user, including the user's creation
date, path, unique ID, and ARN. If you don't specify a user name, |IAM| determines
the user name implicitly based on the AWS access key ID used to sign the request to this API.

Create an  :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object.
Next, create a :sdk-net-api:`GetUserRequest <IAM/TGetUserRequest>` object containing the user name
you want to get information about. Call the
:sdk-net-api:`GetUser <IAM/MIAMServiceGetUserGetUserRequest>` method of the
:code:`AmazonIdentityManagementServiceClient` object to get the information. If the user doesn't exist,
an exception is thrown.

.. code-block:: c#

        public static void GetUser()
        {
            var client = new AmazonIdentityManagementServiceClient();
            var request = new GetUserRequest()
            {
                UserName = "DemoUser"
            };

            try
            {
                var response = client.GetUser(request);
                Console.WriteLine("Creation date: " + response.User.CreateDate.ToShortDateString());
                Console.WriteLine("Password last used: " + response.User.PasswordLastUsed.ToShortDateString());
                Console.WriteLine("UserId = " + response.User.UserId);

            }
            catch (NoSuchEntityException)
            {
                Console.WriteLine("User 'DemoUser' does not exist.");
            }
        }


Delete a User
=============

This example shows how to delete the specified |IAM| user. The user must not belong to any groups
or have any access keys, signing certificates, or attached policies.

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object.
Next, create a :sdk-net-api:`DeleteUserRequest <IAM/TDeleteUserRequest>` object containing the
parameters needed, which consists of the user name you want to delete. Call the
:sdk-net-api:`DeleteUser <IAM/MIAMServiceDeleteUserDeleteUserRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object to delete it.
If the user doesn't exist, an exception is thrown.


.. code-block:: c#

        public static void DeleteUser()
        {
            var client = new AmazonIdentityManagementServiceClient();
            var request = new DeleteUserRequest()
            {
                UserName = "DemoUser"
            };

            try
            {
                var response = client.DeleteUser(request);

            }
            catch (NoSuchEntityException)
            {
                Console.WriteLine("User DemoUser' does not exist.");
            }
        }
