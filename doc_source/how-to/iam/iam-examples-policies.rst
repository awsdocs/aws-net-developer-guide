.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-policies:


###########################
Working with |IAM| Policies
###########################

.. meta::
   :description: Use this .NET code example to use IAM policies.
   :keywords: AWS SDK for .NET examples, IAM policies

The following examples show you how to:

* Create and delete |IAM| policies
* Attach and detach |IAM| policies from roles

The Scenario
============

You grant permissions to a user by creating a policy, which is a document that lists the actions that
a user can perform and the resources those actions can affect. Any actions or resources that are not
explicitly allowed are denied by default. You can create policies and attach them to users,
groups of users, roles assumed by users, and resources.

Use the |sdk-net| to create and delete policies and attach and detach role policies
by using these methods of the :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>`
class:

* :sdk-net-api:`CreatePolicy <IAM/MIAMIAMServiceCreatePolicyCreatePolicyRequest>`
* :sdk-net-api:`GetPolicy <IAM/MIAMIAMServiceGetPolicyGetPolicyRequest>`
* :sdk-net-api:`ListAttachedRolePolicies <IAM/MIAMIAMServiceListAttachedRolePoliciesListAttachedRolePoliciesRequest>`
* :sdk-net-api:`AttachRolePolicy <IAM/MIAMIAMServiceAttachRolePolicyAttachRolePolicyRequest>`
* :sdk-net-api:`DetachRolePolicy <IAM/MIAMIAMServiceDetachRolePolicyDetachRolePolicyRequest>`

For more information about |IAM| users, see :iam-ug:`Overview of Access Management: Permissions and Policies <introduction_access-management.html>`
in the |IAM-ug|.

Create an IAM Policy
====================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object. Next,
create a :sdk-net-api:`CreatePolicy <IAM/MIAMIAMServiceCreatePolicyCreatePolicyRequest>` object
containing the parameters needed to create a new policy, which consists of the name you want
to use for the new policy and a policy document.
You create the policy document by calling the provided :code:`GenerateRolePolicyDocument` method. Upon
returning from the :code:`CreatePolicy` method call, the :sdk-net-api:`CreatePolicyResponse <IAM/TIAMCreatePolicyResponse>`
contains the policy ARN, which is displayed on the console. Please make a note of it so you can use it
in the following examples.

.. code-block:: c#

        public static void CreatePolicyExample()
        {
            var client = new AmazonIdentityManagementServiceClient();
            // GenerateRolePolicyDocument is a custom method
            string policyDoc = GenerateRolePolicyDocument();

            var request = new CreatePolicyRequest
            {
                PolicyName = "DemoEC2Permissions",
                PolicyDocument = policyDoc
            };

            try
            {
                var createPolicyResponse = client.CreatePolicy(request);
                Console.WriteLine("Make a note, Policy named " + createPolicyResponse.Policy.PolicyName +
                    " has Arn: : " + createPolicyResponse.Policy.Arn);
            }
            catch (EntityAlreadyExistsException)
            {
                Console.WriteLine
                  ("Policy 'DemoEC2Permissions' already exits.");
            }

        }

        public static string GenerateRolePolicyDocument()
        {
            // using Amazon.Auth.AccessControlPolicy;

            // Create a policy that looks like this:
            /*
            {
              "Version" : "2012-10-17",
              "Id"  : "DemoEC2Permissions",
              "Statement" : [
                {
                  "Sid" : "DemoEC2PermissionsStatement",
                  "Effect" : "Allow",
                  "Action" : [
                    "s3:Get*",
                    "s3:List*"
                  ],
                  "Resource" : "*"
                }
              ]
            }
            */

            var actionGet = new ActionIdentifier("s3:Get*");
            var actionList = new ActionIdentifier("s3:List*");
            var actions = new List<ActionIdentifier>();

            actions.Add(actionGet);
            actions.Add(actionList);

            var resource = new Resource("*");
            var resources = new List<Resource>();

            resources.Add(resource);

            var statement = new Amazon.Auth.AccessControlPolicy.Statement(Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
            {
                Actions = actions,
                Id = "DemoEC2PermissionsStatement",
                Resources = resources
            };
            var statements = new List<Amazon.Auth.AccessControlPolicy.Statement>();

            statements.Add(statement);

            var policy = new Policy
            {
                Id = "DemoEC2Permissions",
                Version = "2012-10-17",
                Statements = statements
            };

            return policy.ToJson();
        }

Get an IAM Policy
=================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object. Next,
create a :sdk-net-api:`GetPolicyRequest <IAM/TIAMGetPolicyRequest>` object containing the parameter needed
to get the policy, the policy ARN, which was returned by the :code:`CreatePolicy` method in the previous
example.

Call the :sdk-net-api:`GetPolicy <IAM/MIAMIAMServiceGetPolicyGetPolicyRequest>` method.

.. code-block:: c#

        public static void GetPolicy()
        {
            var client = new AmazonIdentityManagementServiceClient();
            var request = new GetPolicyRequest
            {
                PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions"
            };

            try
            {
                var response = client.GetPolicy(request);
                Console.WriteLine("Policy " + response.Policy.PolicyName + "successfully retrieved");

            }
            catch (NoSuchEntityException)
            {
                Console.WriteLine
                  ("Policy 'DemoEC2Permissions' does not exist.");
            }

        }

Attach a Managed Role Policy
============================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object. Next,
create an :sdk-net-api:`AttachRolePolicy <IAM/MIAMIAMServiceAttachRolePolicyAttachRolePolicyRequest>` object containing the
parameters needed to attach the policy to the role, the role name, and the Jason policy returned by the
:code:`GenerateRolePolicyDocument` method. Be sure to use a valid role from the roles associated with your
AWS account.


.. code-block:: c#

        public static void AttachRolePolicy()
        {
            var client = new AmazonIdentityManagementServiceClient();
            string policy = GenerateRolePolicyDocument();
            CreateRoleRequest roleRequest = new CreateRoleRequest()
            {
                RoleName = "tester",
                AssumeRolePolicyDocument = policy
            };

            var request = new AttachRolePolicyRequest()
            {
                PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions",
                RoleName = "tester"
            };
            try
            {
                var response = client.AttachRolePolicy(request);
                Console.WriteLine("Policy DemoEC2Permissions attached to Role TestUser");
            }
            catch (NoSuchEntityException)
            {
                Console.WriteLine
                  ("Policy 'DemoEC2Permissions' does not exist");
            }
            catch (InvalidInputException)
            {
                Console.WriteLine
                  ("One of the parameters is incorrect");
            }
        }





Detach a Managed Role Policy
============================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object. Next,
create a :sdk-net-api:`DetachRolePolicy <IAM/MIAMIAMServiceDetachRolePolicyDetachRolePolicyRequest>` object containing the
parameters needed to attach the policy to the role, the role name, and the Jason policy returned by the
:code:`GenerateRolePolicyDocument` method. Be sure to use the role you used to attach the policy in the
previous example.

.. code-block:: c#

        public static void DetachRolePolicy()
        {
            var client = new AmazonIdentityManagementServiceClient();
            string policy = GenerateRolePolicyDocument();
            CreateRoleRequest roleRequest = new CreateRoleRequest()
            {
                RoleName = "tester",
                AssumeRolePolicyDocument = policy
            };

            var request = new DetachRolePolicyRequest()
            {
                PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions",
                RoleName = "tester"
            };
            try
            {
                var response = client.DetachRolePolicy(request);
                Console.WriteLine("Policy DemoEC2Permissions detached from Role 'tester'");
            }
            catch (NoSuchEntityException e)
            {
                Console.WriteLine
                  (e.Message);
            }
            catch (InvalidInputException i)
            {
                Console.WriteLine
                  (i.Message);
            }
        }
