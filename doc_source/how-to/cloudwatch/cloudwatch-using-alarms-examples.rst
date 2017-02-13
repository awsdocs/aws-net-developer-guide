.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-using-alarms:


########################
Using Alarms in |CWlong|
########################

.. meta::
   :description: Use this .NET code example to learn how to use alarms in Amazon Cloudwatch.
   :keywords: AWS SDK for .NET examples, CloudWatch alarms


This .NET example shows you how to change the state of your |EC2| instances automatically
based on a |CW| alarm.

The Scenario
============

Using alarm actions, you can create alarms that automatically stop, terminate, reboot, or recover your
|EC2| instances. You can use the stop or terminate actions when you no longer need an instance
to be running. You can use the reboot and recover actions to automatically reboot those instances.

In this example, .NET is used to define an alarm action in |CW| that triggers
the reboot of an |EC2| instance. The methods use the |sdk-net| to manage |EC2| instances
using these methods of the AmazonCloudWatchClient class:

* :sdk-net-api:`EnableAlarmActions <CloudWatch/MCloudWatchCloudWatchEnableAlarmActionsEnableAlarmActionsRequest>`
* :sdk-net-api:`DisableAlarmActions <CloudWatch/MCloudWatchCloudWatchDisableAlarmActionsDisableAlarmActionsRequest>`

For more information about |CW| alarm actions, see
:cw-ug:`Create Alarms to Stop, Terminate, Reboot, or Recover an Instance <UsingAlarmActions>`
in the |CW-ug|.

Prerequisite Tasks
==================

To set up and run this example, you must first:

* :cw-ug:`Get set up to use Amazon CloudWatch <GettingSetup>`.
* :sdk-net-dg:`Set up and configure <net-dg-setup>` the |sdk-net|.

Create and Enable Actions on an Alarm
=====================================

#. Create an :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance
   and a :sdk-net-api:`PutMetricAlarmRequest <CloudWatch/TCloudWatchPutMetricAlarmRequest>`
   object to hold the parameters for creating an alarm, specifying :code:`ActionsEnabled` as true and
   an array of ARNs for the actions the alarm  will trigger. Call the 
   :sdk-net-api:`PutMetricAlarm <CloudWatch/MCloudWatchCloudWatchPutMetricAlarmPutMetricAlarmRequest>` 
   method of the :code:`AmazonCloudWatchClient` object, which creates the alarm if it doesn't exist 
   or updates it if the alarm does exist.

    .. code-block:: c#

            using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
            {
                client.PutMetricAlarm(new PutMetricAlarmRequest
                {
                    AlarmName = "Web_Server_CPU_Utilization",
                    ComparisonOperator = ComparisonOperator.GreaterThanThreshold,
                    EvaluationPeriods = 1,
                    MetricName = "CPUUtilization",
                    Namespace = "AWS/EC2",
                    Period = 60,
                    Statistic = Statistic.Average,
                    Threshold = 70.0,
                    ActionsEnabled = true,
                    AlarmActions = new List<string> { "arn:aws:swf:us-west-2:" + "customerAccount" + ":action/actions/AWS_EC2.InstanceId.Reboot/1.0" },
                    AlarmDescription = "Alarm when server CPU exceeds 70%",
                    Dimensions = new List<Dimension>
                    {
                        new Dimension { Name = "InstanceId", Value = "instanceId" }
                    },
                    Unit = StandardUnit.Seconds
                });
            }

#. When :code:`PutMetricAlarm` completes successfully, create an
   :sdk-net-api:`EnableAlarmActionsRequest <CloudWatch/TCloudWatchEnableAlarmActionsRequest>`
   object containing the name of the |CW| alarm. Call the
   :sdk-net-api:`EnableAlarmActions <CloudWatch/MCloudWatchCloudWatchEnableAlarmActionsEnableAlarmActionsRequest>`
   method to enable the alarm action.

    .. code-block:: c#

                client.EnableAlarmActions(new EnableAlarmActionsRequest
                {
                    AlarmNames = new List<string> { "Web_Server_CPU_Utilization" }
                });


#. Create a :sdk-net-api:`MetricDatum <CloudWatch/TCloudWatchMetricDatum>` object containing
   the CPUUtilization custom metric. Create a
   :sdk-net-api:`PutMetricDataRequest <CloudWatch/TCloudWatchPutMetricDataRequest>`
   object containing the :code:`MetricData` parameter needed to submit a data point for the CPUUtilization metric. Call the :sdk-net-api:`PutMetricData <CloudWatch/MCloudWatchCloudWatchPutMetricDataPutMetricDataRequest>` method.

    .. code-block:: c#

                MetricDatum metricDatum = new MetricDatum
                { MetricName = "CPUUtilization" };
                PutMetricDataRequest putMetricDatarequest = new PutMetricDataRequest
                {
                    MetricData = new List<MetricDatum> { metricDatum }
                };
                client.PutMetricData(putMetricDatarequest);

Disable Actions on an Alarm
===========================

Create an :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance and a
:sdk-net-api:`DisableAlarmActionsRequest <CloudWatch/TCloudWatchDisableAlarmActionsRequest>` object containing the name of the |CW| alarm. Call the :sdk-net-api:`DisableAlarmActionsRequest <CloudWatch/TCloudWatchDisableAlarmActionsRequest>`
method to disable the actions for this alarm.

    .. code-block:: c#

            using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
            {
                client.DisableAlarmActions(new DisableAlarmActionsRequest
                {
                    AlarmNames = new List<string> { "Web_Server_CPU_Utilization" }
                });
            }


