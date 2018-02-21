.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-getting-metrics:


#############################
Getting Metrics from |CWlong|
#############################

.. meta::
   :description: Use this >NET code example to learn how to get metrics from Amazon Cloudwatch.
   :keywords: AWS SDK for .NET examples, CloudWatch metrics

This example shows you how to:

* Retrieve a list of |CW| metrics
* Publish |CW| custom metrics

The Scenario
============

Metrics are data about the performance of your systems. You can enable detailed monitoring of some
resources such as your |EC2| instances or your own application metrics. In this example, you use
.NET to retrieve a list of published |CW| metrics and publish data points to |CW| metrics using
these methods of the :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` class:

* :sdk-net-api:`ListMetrics <CloudWatch/MCloudWatchCloudWatchListMetricsListMetricsRequest>`
* :sdk-net-api:`PutMetricData <CloudWatch/MCloudWatchCloudWatchPutMetricDataPutMetricDataRequest>`

For more information about |CW| metrics, see
:cw-ug:`Using Amazon CloudWatch Metrics <working_with_metrics>` in the |CW-ug|.

Prerequisite Tasks
==================

To set up and run this example, you must first:

* :cw-ug:`Get set up to use Amazon CloudWatch <GettingSetup>`.
* :sdk-net-dg:`Set up and configure <net-dg-setup>` the |sdk-net|.

List Metrics
============

Create a :sdk-net-api:`ListMetricsRequest <CloudWatch/TCloudWatchListMetricsRequest>` object containing
the parameters needed to list metrics within the :code:`AWS/Logs` namespace. Call the
:sdk-net-api:`ListMetrics <CloudWatch/MCloudWatchCloudWatchListMetricsListMetricsRequest>` method from
a :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance to list the
:code:`IncomingLogEvents` metric.

.. code-block:: c#

        var logGroupName = "LogGroupName";
        DimensionFilter dimensionFilter = new DimensionFilter()
        {
            Name = logGroupName
        };
        var dimensionFilterList = new List<DimensionFilter>();
        dimensionFilterList.Add(dimensionFilter);

        var dimension = new Dimension
        {
            Name = "UniquePages",
            Value = "URLs"
        };
        using (var cw = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
        {
            var listMetricsResponse = cw.ListMetrics(new ListMetricsRequest
            {
                Dimensions = dimensionFilterList,
                MetricName = "IncomingLogEvents",
                Namespace = "AWS/Logs"
            });
            Console.WriteLine(listMetricsResponse.Metrics);
        }


Submit Custom Metrics
=====================

Create a :sdk-net-api:`PutMetricDataRequest <CloudWatch/TCloudWatchPutMetricDataRequest>` object
containing the parameters needed to submit a data point for the :code:`PAGES_VISITED` custom metric. Call
the :sdk-net-api:`PutMetricData <CloudWatch/MCloudWatchCloudWatchPutMetricDataPutMetricDataRequest>` method
from the :sdk-net-api:`AmazonCloudWatchClient <CloudWatch/TCloudWatchCloudWatchClient>` instance.

.. code-block:: c#

        using (var cw = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
        {
            cw.PutMetricData(new PutMetricDataRequest
            {
                MetricData = new List<MetricDatum>{new MetricDatum
                {
                    MetricName = "PagesVisited",
                    Dimensions = new List<Dimension>{dimension},
                    Unit = "None",
                    Value = 1.0
                }},
                Namespace = "SITE/TRAFFIC"
            });
        }


