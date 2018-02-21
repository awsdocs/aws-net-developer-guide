.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-sending-events:


#################################
Sending Events to |CWlong| Events
#################################

.. meta::
   :description: Use this .NET code example to send events to Amazon CloudWatch Events.
   :keywords: AWS SDK for .NET examples, CloudWatch events


This .NET code example shows you how to:

* Create and update a scheduled rule to trigger an event
* Add a |LAMlong| function target to respond to an event
* Send events that are matched to targets

The Scenario
============

|CWElong| delivers a near real-time stream of system events that describe changes in AWS
resources to various targets. Using simple rules, you can match events and route them to one
or more target functions or streams. This .NET example shows you how to create and update a rule used
to trigger an event, define one or more targets to respond to an event, and send events that are matched
to targets for handling.

The code manages instances using these methods of the
:sdk-net-api:`AmazonCloudWatchEventsClient <CloudWatchEvents/TCloudWatchEventsCloudWatchEventsClient>` class:

* :sdk-net-api:`PutRule <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutRulePutRuleRequest>`
* :sdk-net-api:`PutTargets <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutTargetsPutTargetsRequest>`
* :sdk-net-api:`PutEvents <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutEventsPutEventsRequest>`

For more information about |CWElong|, see :cwe-dg:`Adding Events with PutEvents <AddEventsPutEvents>` in
the |CWE-ug|.

Prerequisite Tasks
==================

To set up and run this example, you must first:

* :cw-ug:`Get set up to use Amazon CloudWatch <GettingSetup>`.
* :sdk-net-dg:`Set up and configure <net-dg-setup>` the |sdk-net|.
*  Create a Lambda function using the hello-world blueprint to serve as the target for events. To 
   learn how, see `Step 1: Create an AWS Lambda function <http://docs.aws.amazon.com/lambda/latest/dg/tutorial-scheduled-events-create-function.html>`_ 
   in the |CWE-ug|.
   
Create an IAM Role to Run the Examples
======================================

The following examples require an IAM role whose policy grants permission to CloudWatch Events and 
that includes :code:`events.amazonaws.com` as a trusted entity. This example creates a role named 
CWEvents, setting it's trust relationship and role policy. 

.. code-block:: c#

        static void Main()
        {
            var client = new AmazonIdentityManagementServiceClient();
            // Create a role and it's trust relationship policy
            var role = client.CreateRole(new CreateRoleRequest
            {
                RoleName = "CWEvents",
                AssumeRolePolicyDocument = 
                @"{""Statement"":[{""Principal"":{""Service"":[""events.amazonaws.com""]}," + 
                @"""Effect"":""Allow"",""Action"":[""sts:AssumeRole""]}]}"
            }).Role;
            // Create a role policy and add it to the role
            string policy = GenerateRolePolicyDocument();
            var request = new CreatePolicyRequest
            {
                PolicyName = "DemoCWPermissions",
                PolicyDocument = policy
            };
            try
            {
                var createPolicyResponse = client.CreatePolicy(request);
            }
            catch (EntityAlreadyExistsException)
            {
                Console.WriteLine
                  ("Policy 'DemoCWPermissions' already exits.");
            }
            var request2 = new AttachRolePolicyRequest()
            {
                PolicyArn = "arn:aws:iam::192484417122:policy/DemoCWPermissions",
                RoleName = "CWEvents"
            };
            try
            {
                var response = client.AttachRolePolicy(request2);    //managedpolicy
                Console.WriteLine("Policy DemoCWPermissions attached to Role TestUser");
            }
            catch (NoSuchEntityException)
            {
                Console.WriteLine
                  ("Policy 'DemoCWPermissions' does not exist");
            }
            catch (InvalidInputException)
            {
                Console.WriteLine
                  ("One of the parameters is incorrect");
            }

        }
        public static string GenerateRolePolicyDocument()
        {
            /* This method produces the following managed policy:
               "Version": "2012-10-17",
               "Statement": [
                  {
                     "Sid": "CloudWatchEventsFullAccess",
                     "Effect": "Allow",
                     "Action": "events:*",
                     "Resource": "*"
                  },
                  {
                     "Sid": "IAMPassRoleForCloudWatchEvents",
                     "Effect": "Allow",
                     "Action": "iam:PassRole",
                     "Resource": "arn:aws:iam::*:role/AWS_Events_Invoke_Targets"
                  }      
               ]
            }
            */
            var actionList = new ActionIdentifier("events:*");
            var actions = new List<ActionIdentifier>();
            actions.Add(actionList);
            var resource = new Resource("*");
            var resources = new List<Resource>();
            resources.Add(resource);
            var statement = new Amazon.Auth.AccessControlPolicy.Statement
                (Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
            {
                Actions = actions,
                Id = "CloudWatchEventsFullAccess",
                Resources = resources
            };
            var statements = new List<Amazon.Auth.AccessControlPolicy.Statement>();
            statements.Add(statement);
            var actionList2 = new ActionIdentifier("iam:PassRole");
            var actions2 = new List<ActionIdentifier>();
            actions2.Add(actionList2);
            var resource2 = new Resource("arn:aws:iam::*:role/AWS_Events_Invoke_Targets");
            var resources2 = new List<Resource>();
            resources2.Add(resource2);
            var statement2 = new Amazon.Auth.AccessControlPolicy.Statement(Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
            {
                Actions = actions2,
                Id = "IAMPassRoleForCloudWatchEvents",
                Resources = resources2
            };

            statements.Add(statement2);
            var policy = new Policy
            {
                Id = "DemoEC2Permissions",
                Version = "2012-10-17",
                Statements = statements
            };
            return policy.ToJson();
        }

        
Create a Scheduled Rule
=======================

Create an :sdk-net-api:`AmazonCloudWatchEventsClient <CloudWatchEvents/TCloudWatchEventsCloudWatchEventsClient>`
instance and a :sdk-net-api:`PutRuleRequest <CloudWatchEvents/TCloudWatchEventsPutRuleRequest>` object
containing the parameters needed to specify the new scheduled rule, which include the following:

* A name for the rule
* The ARN of the |IAM| role you created previously
* An expression to schedule triggering of the rule every five minutes

Call the :sdk-net-api:`PutRule <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutRulePutRuleRequest>` method
to create the rule. The :sdk-net-api:`PutRuleResponse <CloudWatchEvents/TCloudWatchEventsPutRuleResponse>`
returns the ARN of the new or updated rule.

.. code-block:: c#

            AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

            var putRuleRequest = new PutRuleRequest
            {
                Name = "DEMO_EVENT",
                RoleArn = "IAM_ROLE_ARN",
                ScheduleExpression = "rate(5 minutes)",
                State = RuleState.ENABLED
            };

            var putRuleResponse = client.PutRule(putRuleRequest);
            Console.WriteLine("Successfully set the rule {0}", putRuleResponse.RuleArn);

Add a |LAM| Function Target
============================

Create an :sdk-net-api:`AmazonCloudWatchEventsClient <CloudWatchEvents/TCloudWatchEventsCloudWatchEventsClient>` instance
and a :sdk-net-api:`PutTargetsRequest <CloudWatchEvents/TCloudWatchEventsPutTargetsRequest>` object containing
the parameters needed to specify the rule to which you want to attach the target, including the ARN
of the |LAM| function you created. Call the :sdk-net-api:`PutTargets <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutTargetsPutTargetsRequest>`
method of the :code:`AmazonCloudWatchClient` instance.

.. code-block:: c#

            AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

            var putTargetRequest = new PutTargetsRequest
            {
                Rule = "DEMO_EVENT",
                Targets =
                {
                    new Target { Arn = "LAMBDA_FUNCTION_ARN", Id = "myCloudWatchEventsTarget"}
                }
            };
            client.PutTargets(putTargetRequest);


Send Events
===========

Create an :sdk-net-api:`AmazonCloudWatchEventsClient <CloudWatchEvents/TCloudWatchEventsCloudWatchEventsClient>`
instance and a :sdk-net-api:`PutEventsRequest <CloudWatchEvents/TCloudWatchEventsPutEventsRequest>` object
containing the parameters needed to send events. For each event, include the source of the event,
the ARNs of any resources affected by the event, and details for the event. Call the
:sdk-net-api:`PutEvents <CloudWatchEvents/MCloudWatchEventsCloudWatchEventsPutEventsPutEventsRequest>`
method of the :code:`AmazonCloudWatchClient` instance.

.. code-block:: c#


            AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

            var putEventsRequest = new PutEventsRequest
            {
                Entries = new List<PutEventsRequestEntry>
                {
                    new PutEventsRequestEntry
                    {
                        Detail = @"{ ""key1"" : ""value1"", ""key2"" : ""value2"" }",
                        DetailType = "appRequestSubmitted",
                        Resources =
                        {
                            "RESOURCE_ARN"
                        },
                        Source = "com.compnay.myapp"
                    }
                }
            };
            client.PutEvents(putEventsRequest);
