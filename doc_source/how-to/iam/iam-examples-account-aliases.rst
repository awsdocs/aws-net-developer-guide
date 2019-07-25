.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-aliases:


##############################
Managing |IAM| Account Aliases
##############################

.. meta::
   :description: Use this .NET code example to learn how to create, list, and delete an IAM account alias.
   :keywords: AWS SDK for .NET examples, IAM account aliases


These .NET examples show you how to:

* Create an account alias for your AWS account ID
* List an account alias for your AWS account ID
* Delete an account alias for your AWS account ID

The Scenario
============

If you want the URL for your sign-in page to contain your company name or other friendly identifier
instead of your AWS account ID, you can create an alias for your AWS account ID. If you create an AWS
account alias, your sign-in page URL changes to incorporate the alias.

The following examples demonstrate how to manage your AWS account alias by using these methods of the
:sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` class:

* :sdk-net-api:`CreateAccountAlias <IAM/MIAMServiceCreateAccountAliasCreateAccountAliasRequest>`
* :sdk-net-api:`ListAccountAliases <IAM/MIAMServiceListAccountAliasesListAccountAliasesRequest>`
* :sdk-net-api:`DeleteAccountAlias <IAM/MIAMServiceDeleteAccountAliasDeleteAccountAliasRequest>`

For more information about |IAM| account aliases, see :iam-ug:`Your AWS Account ID and Its Alias <console_account-alias>`
in the |IAM-ug|.

Create an Account Alias
=======================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object. Next,
create a
:sdk-net-api:`CreateAccountAliasRequest <IAM/TCreateAccountAliasRequest>` object containing the new
account alias you want to use. Call the :sdk-net-api:`CreateAccountAlias <IAM/MIAMServiceCreateAccountAliasCreateAccountAliasRequest>`
method of the :code:`AmazonIAMClient` object. If the account alias is created, display the new alias on
the console.
If the name already exists, write the exception message to the console.

.. code-block:: c#

        public static void CreateAccountAlias()
        {
            try
            {
                var iamClient = new AmazonIdentityManagementServiceClient();
                var request = new CreateAccountAliasRequest();
                request.AccountAlias = "my-aws-account-alias-2017";
                var response = iamClient.CreateAccountAlias(request);
                if (response.HttpStatusCode.ToString() == "OK")
                    Console.WriteLine(request.AccountAlias + " created.");
                else
                    Console.WriteLine("HttpStatusCode returned = " + response.HttpStatusCode.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



List Account Aliases
====================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object. Next,
create a
:sdk-net-api:`ListAccountAliasesRequest <IAM/TCreateAccountAliasRequest>` object. Call the
:sdk-net-api:`ListAccountAliases <IAM/MIAMServiceListAccountAliasesListAccountAliasesRequest>`
method of the :code:`AmazonIAMClient` object. If there is an account alias, display it on the console.

If there is no account alias, write the exception message to the console.

.. Suggest rewrite for note: An AWS account can have only one alias.

.. note:: There can be only one account alias.

.. code-block:: c#

        public static void ListAccountAliases()
        {
            try
            {
                var iamClient = new AmazonIdentityManagementServiceClient();
                var request = new ListAccountAliasesRequest();
                var response = iamClient.ListAccountAliases(request);
                List<string> aliases = response.AccountAliases;
                foreach (string account in aliases)
                {
                    Console.WriteLine("The account alias is: " + account);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

Delete an Account Alias
=======================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMServiceClient>` object. Next,
create a :sdk-net-api:`DeleteAccountAliasRequest <IAM/TDeleteAccountAliasRequest>` object containing the
account alias you want to delete. Call the :sdk-net-api:`DeleteAccountAlias <IAM/MIAMServiceDeleteAccountAliasDeleteAccountAliasRequest>`
method of the :code:`AmazonIAMClient` object. If the account alias is deleted, display the delete information
on the console. If the name doesn't exist, write the exception message to the console.

.. code-block:: c#

        public static void DeleteAccountAlias()
        {
            try
            {
                var iamClient = new AmazonIdentityManagementServiceClient();
                var request = new DeleteAccountAliasRequest();
                request.AccountAlias = "my-aws-account-alias-2017";
                var response = iamClient.DeleteAccountAlias(request);
                if (response.HttpStatusCode.ToString() == "OK")
                    Console.WriteLine(request.AccountAlias + " deleted.");
                else
                    Console.WriteLine("HttpStatusCode returned = " + response.HttpStatusCode.ToString());
            }
            catch (NoSuchEntityException e)
            {
                Console.WriteLine(e.Message);
            }
        }

