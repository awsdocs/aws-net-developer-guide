.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-creating-alarms:


#####################################################
Describing, Creating, and Deleting Alarms in |CWlong|
#####################################################

.. meta::
   :description: Use this .NET code example to learn how to describe alarms in Amazon CloudWatch.
   :keywords: AWS SDK for .NET examples, CloudWatch alarms


This .NET example show you how to:

* Describe a |CW| alarm
* Create a |CW| alarm based on a metric
* Delete a |CW| alarm

.. _cloudwatch-creating-alarms-examples:

The Scenario
============

An alarm watches a single metric over a time period you specify. It performs one or more actions
based on the value of the metric, relative to a given threshold over a number of time periods. The
following examples show how to describe, create, and delete alarms
in |CW| using these methods of the ``AmazonCloudWatchClient``
class:

* :sdk-net-api:`DescribeAlarms <CloudWatch/MCloudWatchCloudWatchDescribeAlarmsDescribeAlarmsRequest>`
* :sdk-net-api:`PutMetricAlarm <CloudWatch/MCloudWatchCloudWatchPutMetricAlarmPutMetricAlarmRequest>`
* :sdk-net-api:`DeleteAlarms <CloudWatch/MCloudWatchCloudWatchDeleteAlarmsDeleteAlarmsRequest>`

For more information about |CW| alarms, see
:cw-ug:`Creating Amazon CloudWatch Alarms <AlarmThatSendsEmail>`
in the |CW-ug|.

.. _cloudwatch-describe-alarms-prerequisites:

Prerequisite Tasks
==================

To set up and run this example, you must first:

* :cw-ug:`Get set up to use Amazon CloudWatch <GettingSetup>`.
* :sdk-net-dg:`Set up and configure <net-dg-setup>` the |sdk-net|.

.. _cloudwatch-example-describing-alarms:

Describing an Alarm
===================

Create an :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance and a
:sdk-net-api:`DescribeAlarmsRequest <CloudWatch/TCloudWatchDescribeAlarmsRequest>` object,
limiting the alarms that are returned to those with a state of INSUFFICIENT_DATA. Then call the
:sdk-net-api:`DescribeAlarms <CloudWatch/MCloudWatchCloudWatchDescribeAlarmsDescribeAlarmsRequest>`
method of the ``AmazonCloudWatchClient`` object.

.. code-block:: c#

        using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
        {
            var request = new DescribeAlarmsRequest();
            request.StateValue = "INSUFFICIENT_DATA";
            request.AlarmNames = new List<string> { "Alarm1", "Alarm2" };
            do
            {
                var response = client.DescribeAlarms(request);
                foreach(var alarm in response.MetricAlarms)
                {
                    Console.WriteLine(alarm.AlarmName);
                }
                request.NextToken = response.NextToken;
            } while (request.NextToken != null);
        }

.. _cloudwatch-example-creating-alarms-metric:

Creating an Alarm Based on a Metric
===================================

Create an :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance and a
:sdk-net-api:`PutMetricAlarmRequest <CloudWatch/TCloudWatchPutMetricAlarmRequest>` object for the parameters
needed to create an alarm that is based on a metric, in this case, the CPU utilization of an |EC2| instance.

The remaining parameters are set to trigger the alarm when the metric exceeds a threshold of 70 percent.

Then call the :sdk-net-api:`PutMetricAlarm <CloudWatch/MCloudWatchCloudWatchPutMetricAlarmPutMetricAlarmRequest>`
method of the ``AmazonCloudWatchClient`` object.

.. code-block:: C#

            using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
            {
                client.PutMetricAlarm(
                    new PutMetricAlarmRequest
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
                                new Dimension { Name = "InstanceId", Value = "INSTANCE_ID" }
                            },
                        Unit = StandardUnit.Seconds
                    } 
                );
            }

.. _cloudwatch-example-deleting-alarms:

Deleting an Alarm
=================

Create an :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance and
a :sdk-net-api:`DeleteAlarmsRequest <CloudWatch/TCloudWatchDeleteAlarmsRequest>` object to hold the
names of the alarms you want to delete. Then call the :sdk-net-api:`DeleteAlarms <CloudWatch/MCloudWatchCloudWatchDeleteAlarmsDeleteAlarmsRequest>`
method of the ``AmazonCloudWatchClient`` object.

.. code-block:: c#

            using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
            {
                var response = client.DeleteAlarms(
                    new DeleteAlarmsRequest
                    {
                        AlarmNames = new List<string> { "Alarm1", "Alarm2" };
                    });
            }

