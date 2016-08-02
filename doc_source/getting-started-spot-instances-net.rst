.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _tutorial-spot-instances-net:

##############################
Tutorial: |EC2| Spot Instances
##############################

.. _tutor-spot-net-overview:

Overview
========

Spot Instances enable you to bid on unused |EC2| capacity and run any instances that you acquire
for as long as your bid exceeds the current *Spot Price*. |EC2| changes the Spot Price periodically
based on supply and demand, and customers whose bids meet or exceed it gain access to the available
Spot Instances. Like On-Demand Instances and Reserved Instances, Spot Instances provide another
option for obtaining more compute capacity.

Spot Instances can significantly lower your |EC2| costs for applications such as batch processing,
scientific research, image processing, video encoding, data and web crawling, financial analysis,
and testing. Additionally, Spot Instances are an excellent option when you need large amounts of
computing capacity but the need for that capacity is not urgent.

To use Spot Instances, place a Spot Instance request specifying the maximum price you are willing
to pay per instance hour; this is your bid. If your bid exceeds the current Spot Price, your request
is fulfilled and your instances will run until either you choose to terminate them or the Spot Price
increases above your bid (whichever is sooner). You can terminate a Spot Instance programmatically
as shown this tutorial or by using the :console:`AWS Console <ec2>` or by using the
|TVSlong|.

It's important to note two points:

1.  You will often pay less per hour than your bid. |EC2| adjusts the Spot Price periodically as
    requests come in and available supply changes. Everyone pays the same Spot Price for that period
    regardless of whether their bid was higher. Therefore, you might pay less than your bid, but you
    will never pay more than your bid.

2.  If you're running Spot Instances and your bid no longer meets or exceeds the current Spot Price,
    your instances will be terminated. This means that you will want to make sure that your
    workloads and applications are flexible enough to take advantage of this opportunistic |mdash|
    but potentially transient |mdash| capacity.

Spot Instances perform exactly like other |EC2| instances while running, and like other |EC2|
instances, Spot Instances can be terminated when you no longer need them. If you terminate your
instance, you pay for any partial hour used (as you would for On-Demand or Reserved Instances).
However, if your instance is terminated by |EC2| because the Spot Price goes above your bid, you
will not be charged for any partial hour of usage.

This tutorial provides an overview of how to use the .NET programming environment to do the
following.

* Submit a Spot Request

* Determine when the Spot Request becomes fulfilled

* Cancel the Spot Request

* Terminate associated instances


.. _tutor-spot-net-prereq:

Prerequisites
=============

This tutorial assumes that you have signed up for AWS, set up your .NET development environment,
and installed the |sdk-net|. If you use the Microsoft Visual Studio development environment, we
recommend that you also install the |TVSlong|. For instructions on setting up your environment, see
:ref:`net-dg-setup`.


.. _tutor-spot-net-credentials:

Step 1: Setting Up Your Credentials
===================================

To begin using this code sample, you need to populate the :file:`App.config` file with your AWS
credentials, which identify you to |AWSlong|. You specify your credentials in the files
:code:`appSettings` element. The preferred way to handle credentials is to create a profile in the
SDK Store, which encrypts your credentials and stores them separately from any project. You can then
specify the profile by name in the :file:`App.config` file, and the credentials are automatically
incorporated into the application. For more information, see :ref:`net-dg-config`.

Now that you have configured your settings, you can get started using the code in the example.


.. _tutor-spot-net-sg:

Step 2: Setting Up a Security Group
===================================

A *security group* acts as a firewall that controls the traffic allowed in and out of a group of
instances. By default, an instance is started without any security group, which means that all
incoming IP traffic, on any TCP port will be denied. So, before submitting your Spot Request, you
will set up a security group that allows the necessary network traffic. For the purposes of this
tutorial, we will create a new security group called "GettingStarted" that allows connection using
the Windows Remote Desktop Protocol (RDP) from the IP address of the local computer, that is, the
computer where you are running the application.

To set up a new security group, you need to include or run the following code sample that sets up
the security group programmatically. You only need to run this code once to create the new security
group. However, the code is designed so that it is safe to run even if the security group already
exists. In this case, the code catches and ignores the "InvalidGroup.Duplicate" exception.

In the code below, we first use *AWSClientFactoryClass* to create an *AmazonEC2* client object. We
then create a :code:`CreateSecurityGroupRequest` object with the name, "GettingStarted" and a
description for the security group. Finally, we call the :code:`ec2.createSecurityGroup` API to
create the group.

.. code-block:: csharp

    AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client();
    
    try
    {
        CreateSecurityGroupRequest securityGroupRequest = new CreateSecurityGroupRequest();
        securityGroupRequest.GroupName = "GettingStartedGroup";
        securityGroupRequest.GroupDescription = "Getting Started Security Group";
    
        ec2.CreateSecurityGroup(securityGroupRequest);
    }
    catch (AmazonEC2Exception ae)
    {
        if (string.Equals(ae.ErrorCode, "InvalidGroup.Duplicate", StringComparison.InvariantCulture))
        {
            Console.WriteLine(ae.Message);
        }
        else
        {
            throw;
        }
    }

To enable access to the group, we create an :code:`ipPermission` object with the IP address set to
the CIDR representation of the IP address of the local computer. The "/32" suffix on the IP address
indicates that the security group should accept traffic *only* from the local computer. We also
configure the :code:`ipPermission` object with the TCP protocol and port 3389 (RDP). You will need
to fill in the IP address of the local computer. If your connection to the Internet is mediated by a
firewall or some other type of proxy, you will need to determine the external IP address that the
proxy uses. One technique is to query a search engine such as Google or Bing with the string: "what
is my IP address".

.. code-block:: csharp

    // TODO - Change the code below to use your external IP address.
    String ipSource = "XXX.XXX.XXX.XX/32";
    
    List<String> ipRanges = new List<String>();
    ipRanges.Add(ipSource);
    
    List<IpPermissionSpecification> ipPermissions = new List<IpPermissionSpecification>();
    IpPermissionSpecification ipPermission = new IpPermissionSpecification();
    ipPermission.IpProtocol = "tcp";
    ipPermission.FromPort = 3389;
    ipPermission.ToPort = 3389;
    ipPermission.IpRanges = ipRanges;
    ipPermissions.Add(ipPermission);

The final step is to call :code:`ec2.authorizeSecurityGroupIngress` with the name of our security
group and the :code:`ipPermission` object.

.. code-block:: csharp

    try {
        // Authorize the ports to be used.
        AuthorizeSecurityGroupIngressRequest ingressRequest = new AuthorizeSecurityGroupIngressRequest();
        ingressRequest.IpPermissions = ipPermissions;
        ingressRequest.GroupName = "GettingStartedGroup";
        ec2.AuthorizeSecurityGroupIngress(ingressRequest);
    } catch (AmazonEC2Exception ae) {
        if (String.Equals(ae.ErrorCode, "InvalidPermission.Duplicate", StringComparison.InvariantCulture))
        {
            Console.WriteLine(ae.Message);
        }
        else
        {
            throw;
        }
    }

You can also create the security group using the |TVSlong|. Go to the |TVS-ug|_ for more information.


.. _tutor-spot-net-submit:

Step 3: Submitting Your Spot Request
====================================

To submit a Spot Request, you first need to determine the instance type, the Amazon Machine Image
(AMI), and the maximum bid price you want to use. You must also include the security group we
configured previously, so that you can log into the instance if you want to.

There are several instance types to choose from; go to :ec2-ug:`Amazon EC2 Instance Types<instance>` 
for a complete list. For this tutorial, we will use :code:`t1.micro`.
You'll also want to get the ID of a current Windows AMI. For more information, see 
:ec2-ug-win:`Finding an AMI <finding-an-ami>` in the |EC2-ug-win|.

There are many ways to approach bidding for Spot instances. To get a broad overview of the various
approaches, you should view the `Bidding for Spot Instances
<http://www.youtube.com/watch?v=WD9N73F3Fao&feature=player_embedded>`_ video. However, to get
started, we'll describe three common strategies: bid to ensure cost is less than on-demand pricing;
bid based on the value of the resulting computation; bid so as to acquire computing capacity as
quickly as possible.

* **Reduce Cost Below On-Demand**
  You have a batch processing job that will take a number of hours or
  days to run. However, you are flexible with respect to when it starts and when it completes. You
  want to see if you can complete it for less cost than with On-Demand Instances. You examine the
  Spot Price history for instance types using either the |console| or the |EC2| API. For more
  information, go to `Viewing Spot Price History <using-spot-instances-history.html>`_. After
  you've analyzed the price history for your desired instance type in a given Availability Zone,
  you have two alternative approaches for your bid:

  * You could bid at the upper end of the range of Spot Prices (which are still below the On-Demand
    price), anticipating that your one-time Spot Rrequest would most likely be fulfilled and run
    for enough consecutive compute time to complete the job.

  * Or, you could bid at the lower end of the price range, and plan to combine many instances launched
    over time through a persistent request. The instances would run long enough, in aggregate,
    to complete the job at an even lower total cost. (We will explain how to automate this task
    later in this tutorial.)

* **Pay No More than the Value of the Result**
  You have a data processing job to run. You understand the
  value of the job's results well enough to know how much they are worth in terms of computing
  costs. After you've analyzed the Spot Price history for your instance type, you choose a bid
  price at which the cost of the computing time is no more than the value of the job's results.
  You create a persistent bid and allow it to run intermittently as the Spot Price fluctuates at
  or below your bid.

* **Acquire Computing Capacity Quickly**
  You have an unanticipated, short-term need for additional
  capacity that is not available through On-Demand Instances. After you've analyzed the Spot Price
  history for your instance type, you bid above the highest historical price to provide a high
  likelihood that your request will be fulfilled quickly and continue computing until it
  completes.
    
After you choose your bid price, you are ready to request a Spot Instance. For the purposes of this
tutorial, we will set our bid price equal to the On-Demand price ($0.03) to maximize the chances
that the bid will be fulfilled. You can determine the types of available instances and the On-Demand
prices for instances by going to :ec2-ug:`Amazon EC2 Instance Types <instance-types>` .

To request a Spot Instance, you simply need to build your request with the parameters we have
specified so far. We start by creating a :code:`RequestSpotInstanceRequest` object. The request
object requires the number of instances you want to start (2) and the bid price ($0.03).
Additionally, you need to set the :code:`LaunchSpecification` for the request, which includes the
instance type, AMI ID, and security group you want to use. Once the request is populated, you call
the :code:`requestSpotInstances` method on the :code:`AmazonEC2Client` object. An example of how to
request a Spot Instance is shown below.

.. code-block:: csharp

     RequestSpotInstancesRequest requestRequest = new RequestSpotInstancesRequest();
    
     requestRequest.SpotPrice = "0.03";
     requestRequest.InstanceCount = 2;
    
     LaunchSpecification launchSpecification = new LaunchSpecification();
     launchSpecification.ImageId = "ami-fbf93092";   // latest Windows AMI as of this writing
     launchSpecification.InstanceType = "t1.micro";
    
     launchSpecification.SecurityGroup.Add("GettingStartedGroup");
    
     requestRequest.LaunchSpecification = launchSpecification;
    
     RequestSpotInstancesResponse requestResult = ec2.RequestSpotInstances(requestRequest);

There are other options you can use to configure your Spot Requests. To learn more, see
:sdk-net-api-v2:`RequestSpotInstances <TEC2RequestSpotInstancesRequestNET45>` in the |sdk-net|.

Running this code will launch a new Spot Instance Request.

.. note:: You will be charged for any Spot Instances that are actually launched, so make sure that you cancel
   any requests and terminate any instances you launch to reduce any associated fees.


.. _tutor-spot-net-request-state:

Step 4: Determining the State of Your Spot Request
==================================================

Next, we want to create code to wait until the Spot Request reaches the "active" state before
proceeding to the last step. To determine the state of our Spot Request, we poll the
:sdk-net-api-v2:`describeSpotInstanceRequests <TEC2DescribeSpotInstanceRequestsRequestNET45>` method for the
state of the Spot Request ID we want to monitor.

The request ID created in Step 2 is embedded in the result of our :code:`requestSpotInstances`
request. The following example code gathers request IDs from the :code:`requestSpotInstances` result
and uses them to populate the :code:`SpotInstanceRequestId` member of a :code:`describeRequest`
object. We will use this object in the next part of the sample.

.. code-block:: csharp

    // Call the RequestSpotInstance API.
    RequestSpotInstancesResponse requestResult = ec2.RequestSpotInstances(requestRequest);
    
    // Create the describeRequest object with all of the request ids
    // to monitor (e.g. that we started).
    DescribeSpotInstanceRequestsRequest describeRequest = new DescribeSpotInstanceRequestsRequest();
    foreach (SpotInstanceRequest spotInstanceRequest in requestResult.RequestSpotInstancesResult.SpotInstanceRequest)
    {
        describeRequest.SpotInstanceRequestId.Add(spotInstanceRequest.SpotInstanceRequestId);
    }

    
.. code-block:: csharp

     // Create a variable that will track whether there are any
     // requests still in the open state.
     bool anyOpen;
    
     // Create a list to store any instances that were activated.
     List<String> instanceIds = new List<String>();
    
     do
     {
         // Initialize the anyOpen variable to false, which assumes there
         // are no requests open unless we find one that is still open.
         anyOpen = false;
         instanceIds.Clear();
    
         try
         {
             // Retrieve all of the requests we want to monitor.
             DescribeSpotInstanceRequestsResponse describeResponse = ec2.DescribeSpotInstanceRequests(describeRequest);
    
             // Look through each request and determine if they are all in
             // the active state.
             foreach (SpotInstanceRequest spotInstanceRequest in describeResponse.DescribeSpotInstanceRequestsResult.SpotInstanceRequest)
             {
                 // If the state is open, it hasn't changed since we attempted
                 // to request it. There is the potential for it to transition
                 // almost immediately to closed or canceled, so we compare
                 // against open instead of active.
                 if (spotInstanceRequest.State.Equals("open", StringComparison.InvariantCulture))
                 {
                     anyOpen = true;
                     break;
                 }
                 else if (spotInstanceRequest.State.Equals("active", StringComparison.InvariantCulture))
                 {
                     // Add the instance id to the list we will
                     // eventually terminate.
                     instanceIds.Add(spotInstanceRequest.InstanceId);
                 }
             }
         }
         catch (AmazonEC2Exception e)
         {
             // If we have an exception, ensure we don't break out of
             // the loop. This prevents the scenario where there was
             // blip on the wire.
             anyOpen = true;
    
             Console.WriteLine(e.Message);
         }
    
         if (anyOpen)
         {
             // Wait for the requests to go active.
             Console.WriteLine("Requests still in open state, will retry in 60 seconds.");
             Thread.Sleep((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
         }
     } while (anyOpen); 

If you just ran the code up to this point, your Spot Instance Request would complete |mdash| or
possibly fail with an error. For the purposes of this tutorial, we'll add some code that cleans up
the requests after all of them have transitioned out of the open state.


.. _tutor-spot-net-cleaning-up:

Step 5: Cleaning up Your Spot Requests and Instances
====================================================

The final step is to clean up our requests and instances. It is important to both cancel any
outstanding requests *and* terminate any instances. Just canceling your requests will not terminate
your instances, which means that you will continue to pay for them. If you terminate your instances,
your Spot Requests may be canceled, but there are some scenarios |mdash| such as if you use
persistent bids |mdash| where terminating your instances is not sufficient to stop your request from
being re-fulfilled. Therefore, it is a best practice to both cancel any active bids and terminate
any running instances.

The following code demonstrates how to cancel your requests.

.. code-block:: csharp

    try
    {
        // Cancel requests.
        CancelSpotInstanceRequestsRequest cancelRequest = new CancelSpotInstanceRequestsRequest();
    
        foreach (SpotInstanceRequest spotInstanceRequest in requestResult.RequestSpotInstancesResult.SpotInstanceRequest)
        {
            cancelRequest.SpotInstanceRequestId.Add(spotInstanceRequest.SpotInstanceRequestId);
        }
    
        ec2.CancelSpotInstanceRequests(cancelRequest);
    }
    catch (AmazonEC2Exception e)
    {
        // Write out any exceptions that may have occurred.
        Console.WriteLine("Error cancelling instances");
        Console.WriteLine("Caught Exception: " + e.Message);
        Console.WriteLine("Reponse Status Code: " + e.StatusCode);
        Console.WriteLine("Error Code: " + e.ErrorCode);
        Console.WriteLine("Request ID: " + e.RequestId);
    }
    }

To terminate any outstanding instances, we use the instanceIds array, which we populated with the
instance IDs of those instances that transitioned to the active state. We terminate these instances
by assigning this array to the :code:`InstanceId` member of a :code:`TerminateInstancesRequest`
object, then passing that object to the :code:`ec2.TerminateInstances` API.

.. code-block:: csharp

    if (instanceIds.Count > 0)
    {
        try
        {
            TerminateInstancesRequest terminateRequest = new TerminateInstancesRequest();
            terminateRequest.InstanceId = instanceIds;
    
            ec2.TerminateInstances(terminateRequest);
        }
        catch (AmazonEC2Exception e)
        {
            Console.WriteLine("Error terminating instances");
            Console.WriteLine("Caught Exception: " + e.Message);
            Console.WriteLine("Reponse Status Code: " + e.StatusCode);
            Console.WriteLine("Error Code: " + e.ErrorCode);
            Console.WriteLine("Request ID: " + e.RequestId);
        }
    }


.. _tutor-spot-php-conclusion:

Conclusion
==========

Congratulations! You have just completed the getting started tutorial for developing Spot Instance
software with the |sdk-net|.



