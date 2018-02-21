.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudwatch-examples-intro:

############################################
Monitoring Your AWS Resources Using |CWlong|
############################################

.. meta::
   :description: .NET code examples for Amazon CloudWatch
   :keywords: AWS SDK for .NET examples, CloudWatch


|CWlong| is a web service that monitors your AWS resources
and the applications you run on AWS in real time. You can use |CW| to collect and track metrics,
which are variables you can measure for your resources and applications. |CW| alarms send
notifications or automatically make changes to the resources you're monitoring based on rules that
you define.

The code for these examples is written in C#, but you can use the |sdk-net| with any compatible
language. When you install the |TVSlong|, a set of C# project templates are installed. The simplest way to start this
project is to open Visual Studio, and then choose :guilabel:`File`, :guilabel:`New Project`,
:guilabel:`AWS Sample Projects`, :guilabel:`Deployment and Management`,
:guilabel:`AWS CloudWatch Example`.

**Prerequisite Tasks**

Before you begin, be sure that you have created an AWS account and set up your AWS credentials. For
more information, see :ref:`net-dg-setup`.

.. toctree::
    :titlesonly:
    :maxdepth: 1

    how-to/cloudwatch/cloudwatch-creating-alarms-examples
    how-to/cloudwatch/cloudwatch-using-alarms-examples
    how-to/cloudwatch/cloudwatch-getting-metrics-examples
    how-to/cloudwatch/cloudwatch-examples-sending-events
    how-to/cloudwatch/cloudwatch-using-subscriptions-examples


