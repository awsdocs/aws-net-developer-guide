.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _tutorial-spot-instances-net:

#################################
Amazon EC2 Spot Instance Examples
#################################

This topic describes how to use the |sdk-net| to create, cancel, and terminate an Amazon EC2 Spot Instance.

.. contents:: **Topics**
    :local:
    :depth: 1

.. _tutor-spot-net-overview:

Overview
========

Spot Instances enable you to request unused |EC2| capacity and run any instances that you acquire for
as long as your request exceeds the current *Spot Price*. |EC2| changes the Spot Price periodically
based on supply and demand, but will never exceed 90% of the On-Demand Instance price;
customers whose requests meet or exceed it gain access to the available Spot Instances.
Like On-Demand Instances and Reserved Instances, Spot Instances provide another option
for obtaining more compute capacity.

Spot Instances can significantly lower your |EC2| costs for applications such as batch processing,
scientific research, image processing, video encoding, data and web crawling, financial analysis,
and testing. Additionally, Spot Instances are an excellent option when you need large amounts of
computing capacity, but the need for that capacity is not urgent.

To use Spot Instances, place a Spot Instance request specifying the maximum price you are willing to
pay per instance hour; this is your request. If your request exceeds the current Spot Price, your request is
fulfilled and your instances will run until either you choose to terminate them or the Spot Price
increases above your request (whichever is sooner). You can terminate a Spot Instance programmatically,
as shown this tutorial, or by using the :console:`AWS Management Console <ec2>` or by using the
|TVSlong|.

You can also specify the amount you are willing to pay for Spot Instances as a precentage of the On-Demand Instance price.
If the specified price is exceeded, then the Spot Instance will terminate.

Spot Instances perform exactly like other |EC2| instances while running, and like other |EC2|
instances, Spot Instances can be terminated when you no longer need them. 

You pay the Spot price that's in effect for the time period your instances are running.
Spot Instance prices are set by |EC2| and adjust gradually based on long-term trends in supply and demand for Spot Instance capacity.
You can also specify the amount you are willing to pay for a Spot Instance as a percentage of the On-Demand Instance price.

This tutorial provides an overview of how to use the .NET programming environment to do the
following.

* Submit a Spot request

* Determine when the Spot request becomes fulfilled

* Cancel the Spot request

* Terminate associated instances

.. _tutor-spot-net-prereq:

Prerequisites
=============

This tutorial assumes you have signed up for AWS, set up your .NET development environment, and
installed the |sdk-net|. If you use the Microsoft Visual Studio development environment, we
recommend you also install the |TVSlong|. For instructions on setting up your environment, see
:ref:`net-dg-setup`.


.. _tutor-spot-net-credentials:

Setting Up Your Credentials
===========================

For information about how to use your AWS credentials with the SDK, see
:ref:`net-dg-config-creds`.

.. _tutor-spot-net-submit:

Submitting Your Spot Request
============================

To submit a Spot request, you first need to determine the instance type, the Amazon Machine Image
(AMI), and the maximum request you want to offer. You must also include a security group, so that
you can log into the instance if you want to. For more information about creating security groups,
see :ref:`create-security-group`.

There are several instance types to choose from; go to 
:ec2-ug:`Amazon EC2 Instance Types <instance-types>` for a complete list. For this tutorial, we will 
use :code:`t1.micro`. You'll also want to get the ID of a current Windows AMI. For more information, 
see :ec2-ug-win:`Finding an AMI <finding-an-ami>` in the |EC2-ug-win|.

There are many ways to approach requesting Spot Instances. To get a broad overview of the various
approaches, you should view the 
`Deciding on Your Spot Bidding Strategy <http://www.youtube.com/watch?v=WD9N73F3Fao&feature=player_embedded>`_ 
video. However, to get started, we'll describe three common strategies: request to ensure that the cost is less 
than on-demand pricing; request based on the value of the resulting computation; request so as to acquire 
computing capacity as quickly as possible.

*Reduce Cost Below On-Demand*
  You have a batch processing job that will take a number of hours or days to run. However, you
  are flexible with respect to when it starts and ends. You want to see if you can complete it for
  less than the cost of On-Demand Instances. You examine the Spot Price history for instance types
  using either the |console| or the |EC2| API. For more information, go to 
  :ec2-ug:`Viewing Spot Price History <using-spot-instances-history>`. After you've analyzed the 
  price history for your desired instance type in a given Availability Zone, you have two 
  alternative approaches for your request: 

  * Specify a request at the upper end of the range of Spot Prices, which are still below the On-Demand
    price, anticipating that your one-time Spot request would most likely be fulfilled and run
    for enough consecutive compute time to complete the job.

  * Specify a request at the lower end of the price range, and plan to combine many instances launched
    over time through a persistent request. The instances would run long enough, in aggregate,
    to complete the job at an even lower total cost. (We will explain how to automate this task
    later in this tutorial.)

*Pay No More than the Value of the Result*
  You have a data processing job to run. You understand the value of the job's results well enough
  to know how much they are worth in terms of computing costs. After you've analyzed the Spot
  Price history for your instance type, you choose a request at which the cost of the computing
  time is no more than the value of the job's results. You create a persistent request and allow it to
  run intermittently as the Spot Price fluctuates at or below your request.

*Acquire Computing Capacity Quickly*
  You have an unanticipated, short-term need for additional capacity that is not available through
  On-Demand Instances. After you've analyzed the Spot Price history for your instance type, you
  request above the highest historical price to greatly improve the likelihood your request will be
  fulfilled quickly and continue computing until it is complete.

After you have performed your analysis, you are ready to request a Spot Instance. In this
tutorial the request is equal to the On-Demand price ($0.03) to maximize the chances the
request will be fulfilled. You can determine the types of available instances and the On-Demand prices
for instances by going to `Amazon EC2 Pricing page <http://aws.amazon.com/ec2/pricing/>`_.

First specify the .NET namespaces used in the application.

.. literalinclude:: ../../ec2.dotnet.spot_instance_using.txt
   :language: csharp

For information on creating an |EC2| client, see :ref:`init-ec2-client`.

Next, to request a Spot Instance, you need to build your request with the parameters we have specified so
far. Start by creating a :sdk-net-api:`RequestSpotInstanceRequest <EC2/TRequestSpotInstancesRequest>`
object. The request object requires the request amount and the number of instances you want to start.
Additionally, you need to set the :sdk-net-api:`LaunchSpecification <EC2/TLaunchSpecification>` for the
request, which includes the instance type, AMI ID, and the name of the security group you want to
use for the Spot Instances. After the request is populated, call the :sdk-net-api:`RequestSpotInstances
<EC2/MEC2RequestSpotInstancesRequestSpotInstancesRequest>` method to create the Spot Instance
request. The following example demonstrates how to request a Spot Instance.

.. literalinclude:: ec2.dotnet.spot_instance_request_spot_instance.txt
   :language: csharp
   :dedent: 8

The Spot request ID is contained in the :code:`SpotInstanceRequestId` member of the
:sdk-net-api:`SpotInstanceRequest <EC2/TSpotInstanceRequest>` object.

Running this code will launch a new Spot Instance request.

.. note:: You will be charged for any Spot Instances that are launched, so make sure you cancel any requests
   and terminate any instances you launch to reduce any associated fees.

There are other options you can use to configure your Spot requests. To learn more, see
:sdk-net-api:`RequestSpotInstances <EC2/MEC2RequestSpotInstancesRequestSpotInstancesRequest>` in the
|sdk-net|.

.. _tutor-spot-net-request-state:

Determining the State of Your Spot Request
==========================================

Next, we need to wait until the Spot request reaches the :code:`Active` state before proceeding to
the last step. To determine the state of your Spot request, we use the 
:sdk-net-api:`DescribeSpotInstanceRequests <EC2/TDescribeSpotInstanceRequestsRequest>` method to 
obtain the state of the Spot request ID we want to monitor.

.. literalinclude:: ec2.dotnet.spot_instance_get_spot_request_state.txt
   :language: csharp
   :dedent: 8

.. _tutor-spot-net-cleaning-up:

Cleaning Up Your Spot Requests and Instances
============================================

The final step is to clean up your requests and instances. It is important to both cancel any
outstanding requests and terminate any instances. Just canceling your requests will not terminate
your instances, which means that you will continue to be charged for them. If you terminate your
instances, your Spot requests may be canceled, but there are some scenarios, such as if you use
persistent requests, where terminating your instances is not sufficient to stop your request from being
re-fulfilled. Therefore, it is a best practice to both cancel any active requests and terminate any
running instances.

You use the :sdk-net-api:`CancelSpotInstanceRequests
<EC2/MEC2CancelSpotInstanceRequestsCancelSpotInstanceRequestsRequest>` method to cancel a Spot
request. The following example demonstrates how to cancel a Spot request.

.. literalinclude:: ec2.dotnet.spot_instance_cancel_spot_request.txt
   :language: csharp
   :dedent: 8

You use the :sdk-net-api:`TerminateInstances <EC2/MEC2TerminateInstancesTerminateInstancesRequest>` method
to terminate an instance. The following example demonstrates how to obtain the instance identifier
for an active Spot Instance and terminate the instance.

.. literalinclude:: ec2.dotnet.spot_instance_terminate_spot_request.txt
   :language: csharp
   :dedent: 8

For more information about terminating active instances, see :ref:`terminate-instance`.

.. _tutor-spot-net-main:

Putting it all Together
=======================

The following `main` routine calls these methods in the shown order
to create, cancel, and terminate a spot instance request.
As the comment states, it takes one argument, the AMI.

.. literalinclude:: ec2.dotnet.spot_instance_main.txt
   :language: csharp
   :dedent: 8

See the
`complete example
<https://github.com/awsdocs/aws-doc-sdk-examples/blob/master/dotnet/example_code/ec2/Ec2SpotCRUD.cs>`_,
including information on how to build and run the example from the command line,
on GitHub.
