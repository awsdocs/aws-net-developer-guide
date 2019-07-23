.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-using-subscriptions:


#######################################
Using Subscription Filters in |CWLlong|
#######################################

.. meta::
   :description: Use this .NET code example to learn how to use subscription filters in Amazon CloudWatch Logs.
   :keywords: AWS SDK for .NET examples, CloudWatch Logs subscription filters

These .NET examples shows you how to:

* List existing subscription filters in |CWL|
* Create a subscription filter in |CWL|
* Delete a subscription filter in |CWL|

The Scenario
============

Subscriptions provide access to a real-time feed of log events from |CW| Logs and deliver that feed
to other services such as an |AKSlong| or |LAMlong| for custom processing, analysis,
or loading to other systems. A subscription filter defines the pattern to use for filtering which log
events are delivered to your AWS resource. This example shows how to list, create, and delete a
subscription filter in |CWL|. The destination for the log events is a |LAM| function.

This example uses the |sdk-net| to manage subscription filters using these methods of the
:sdk-net-api:`AmazonCloudWatchLogsClient <CloudWatchLogs/TCloudWatchLogsCloudWatchLogsClient>` class:

* :sdk-net-api:`DescribeSubscriptionFilters <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsDescribeSubscriptionFiltersDescribeSubscriptionFiltersRequest>`
* :sdk-net-api:`PutSubscriptionFilter <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsPutSubscriptionFilterPutSubscriptionFilterRequest>`
* :sdk-net-api:`DeleteSubscriptionFilter <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsDeleteSubscriptionFilterDeleteSubscriptionFilterRequest>`

For more information about |CWL| subscriptions, see
:cwl-ug:`Real-time Processing of Log Data with Subscriptions <Subscriptions>`
in the |CWL-ug|.

Prerequisite Tasks
==================

To set up and run this example, you must first:

* :cw-ug:`Get set up to use Amazon CloudWatch <GettingSetup>`.
* :sdk-net-dg:`Set up and configure <net-dg-setup>` the |sdk-net|.

Describe Existing Subscription Filters
======================================

Create an :sdk-net-api:`AmazonCloudWatchLogsClient <CloudWatchLogs/TCloudWatchLogsCloudWatchLogsClient>`
object. Create a :sdk-net-api:`DescribeSubscriptionFiltersRequest <CloudWatchLogs/TCloudWatchLogsDescribeSubscriptionFiltersRequest>`
object containing the parameters needed to describe your existing filters. Include the name of the
log group and the maximum number of filters you want described. Call the
:sdk-net-api:`DescribeSubscriptionFilters <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsDescribeSubscriptionFiltersDescribeSubscriptionFiltersRequest>`
method.

.. code-block:: c#

        public static void DescribeSubscriptionFilters()
        {
            var client = new AmazonCloudWatchLogsClient();
            var request = new Amazon.CloudWatchLogs.Model.DescribeSubscriptionFiltersRequest()
            {
                LogGroupName = "GROUP_NAME",
                Limit = 5
            };
            try
            {
                var response = client.DescribeSubscriptionFilters(request);
            }
            catch (Amazon.CloudWatchLogs.Model.ResourceNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                client?.Dispose();
            }
        }

Create a Subscription Filter
============================

Create an :sdk-net-api:`AmazonCloudWatchLogsClient <CloudWatchLogs/TCloudWatchLogsCloudWatchLogsClient>`
object. Create a :sdk-net-api:`PutSubscriptionFilterRequest <CloudWatchLogs/TCloudWatchLogsPutSubscriptionFilterRequest>`
object containing the parameters needed to create a filter, including the ARN of the destination |LAM|
function, the name of the filter, the string pattern for filtering, and the name of the log group.
Call the :sdk-net-api:`PutSubscriptionFilter <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsPutSubscriptionFilterPutSubscriptionFilterRequest>`
method.

.. code-block:: c#

        public static void PutSubscriptionFilters()
        {
            var client = new AmazonCloudWatchLogsClient();
            var request = new Amazon.CloudWatchLogs.Model.PutSubscriptionFilterRequest()
            {
                DestinationArn = "LAMBDA_FUNCTION_ARN",
                FilterName = "FILTER_NAME",
                FilterPattern = "ERROR",
                LogGroupName = "Log_Group"
            };
            try
            {
                var response = client.PutSubscriptionFilter(request);
            }
            catch (InvalidParameterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                client?.Dispose();
            }
        }

Delete a Subscription Filter
============================

Create an :sdk-net-api:`AmazonCloudWatchLogsClient <CloudWatchLogs/TCloudWatchLogsCloudWatchLogsClient>`
object. Create a :sdk-net-api:`DeleteSubscriptionFilterRequest <CloudWatchLogs/TCloudWatchLogsDeleteSubscriptionFilterRequest>`
object containing the parameters needed to delete a filter, including the names of the filter and the
log group. Call the :sdk-net-api:`DeleteSubscriptionFilter <CloudWatchLogs/MCloudWatchLogsCloudWatchLogsDeleteSubscriptionFilterDeleteSubscriptionFilterRequest>`
method.

.. code-block:: c#

        public static void DeleteSubscriptionFilter()
        {
            var client = new AmazonCloudWatchLogsClient();
            var request = new Amazon.CloudWatchLogs.Model.DeleteSubscriptionFilterRequest()
            {
                LogGroupName = "GROUP_NAME",
                FilterName = "FILTER"
            };
            try
            {
                var response = client.DeleteSubscriptionFilter(request);
            }
            catch (Amazon.CloudWatchLogs.Model.ResourceNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                client?.Dispose();
            }
        }
