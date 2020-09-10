--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Amazon EC2 Spot Instance tutorial<a name="how-to-spot-instances"></a>

This tutorial shows you how to use the AWS SDK for \.NET to manage Amazon EC2 Spot Instances\.

## Overview<a name="tutor-spot-net-overview"></a>

Spot Instances enable you to request unused Amazon EC2 capacity for less than the On\-Demand price\. This can significantly lower your EC2 costs for applications that can be interrupted\.

The following is a high\-level summary of how Spot Instances are requested and used\.

1. Create a Spot Instance request, specifying the maximum price you are willing to pay\.

1. When the request is fulfilled, run the instance as you would any other Amazon EC2 instance\.

1. Run the instance as long as you want and then terminate it, unless the *Spot Price* changes such that the instance is terminated for you\.

1. Clean up the Spot Instance request when you no longer need it so that Spot Instances are no longer created\.

This has been a very high level overview of Spot Instances\. You can gain a better understanding of Spot Instances by reading about them in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-spot-instances.html)\.

## About this tutorial<a name="about-spot-instances-tutorial"></a>

As you follow this tutorial, you use the AWS SDK for \.NET to do the following:
+ Create a Spot Instance request
+ Determine when the Spot Instance request has been fulfilled
+ Cancel the Spot Instance request
+ Terminate associated instances

The following sections provide snippets and other information for this example\. The [complete code for the example](#tutor-spot-net-main) is shown after the snippets, and can be built and run as is\.

**Topics**
+ [Overview](#tutor-spot-net-overview)
+ [About this tutorial](#about-spot-instances-tutorial)
+ [Prerequisites](#tutor-spot-net-prereq)
+ [Gather what you need](#tutor-spot-net-gather)
+ [Creating a Spot Instance request](#tutor-spot-net-submit)
+ [Determine the state of your Spot Instance request](#tutor-spot-net-request-state)
+ [Clean up your Spot Instance requests](#tutor-spot-net-clean-up-request)
+ [Clean up your Spot Instances](#tutor-spot-net-clean-up-instance)
+ [Complete code](#tutor-spot-net-main)

## Prerequisites<a name="tutor-spot-net-prereq"></a>

For information about the APIs and prerequisites, see the parent section \([Working with Amazon EC2](ec2-apis-intro.md)\)\.

## Gather what you need<a name="tutor-spot-net-gather"></a>

To create a Spot Instance request, you'll need several things\. 
+ The number of instances and their instance type\. There are several instance types to choose from, which you can see in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/instance-types.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/instance-types.html)\. The default number for this tutorial is 1\.
+ The Amazon Machine Image \(AMI\) that will be used to create the instance\. See the information about AMIs in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AMIs.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/AMIs.html)\. For example, read about shared AMIs in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/sharing-amis.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/sharing-amis.html)\.
+ The maximum price that you're willing to pay per instance hour\. You can see the prices for all instance types \(for both On\-Demand Instances and Spot Instances\) on the [Amazon EC2 pricing page](https://aws.amazon.com/ec2/pricing/)\. The default price for this tutorial is explained later\.
+ If you want to connect remotely to an instance, a security group with the appropriate configuration and resources\. This is described in [Working with security groups in Amazon EC2](security-groups.md) and the information about [gathering what you need](run-instance.md#run-instance-gather) and [connecting to an instance](run-instance.md#connect-to-instance) in [Launching an Amazon EC2 instance](run-instance.md)\. For simplicity, this tutorial uses the security group named **default** that all newer AWS accounts have\.

There are many ways to approach requesting Spot Instances\. The following are common strategies:
+ Make requests that are sure to cost less than on\-demand pricing\.
+ Make requests based on the value of the resulting computation\.
+ Make requests so as to acquire computing capacity as quickly as possible\.

The following explanations refer to the Spot Price history in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances-history.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-spot-instances-history.html)\.

### Reduce cost below On\-Demand<a name="reduce-cost"></a>

You have a batch processing job that will take a number of hours or days to run\. However, you are flexible with respect to when it starts and ends\. You want to see if you can complete it for less than the cost of On\-Demand Instances\.

You examine the Spot Price history for instance types by using either the Amazon EC2 console or the Amazon EC2 API\. After you've analyzed the price history for your desired instance type in a given Availability Zone, you have two alternative approaches for your request:
+ Specify a request at the upper end of the range of Spot Prices, which are still below the On\-Demand price, anticipating that your one\-time Spot Instance request would most likely be fulfilled and run for enough consecutive compute time to complete the job\.
+ Specify a request at the lower end of the price range, and plan to combine many instances launched over time through a persistent request\. The instances would run long enough, in aggregate, to complete the job at an even lower total cost\.

### Pay no more than the value of the result<a name="value-of-result"></a>

You have a data processing job to run\. You understand the value of the job's results well enough to know how much they're worth in terms of computing costs\.

After you've analyzed the Spot Price history for your instance type, you choose a price at which the cost of the computing time is no more than the value of the job's results\. You create a persistent request and allow it to run intermittently as the Spot Price fluctuates at or below your request\.

### Acquire computing capacity quickly<a name="acquire-quickly"></a>

You have an unanticipated, short\-term need for additional capacity that's not available through On\-Demand Instances\. After you've analyzed the Spot Price history for your instance type, you choose a price above the highest historical price to greatly improve the likelihood that your request will be fulfilled quickly and continue computing until it's complete\.

After you have gathered what you need and chosen a strategy, you are ready to request a Spot Instance\. For this tutorial the default maximum spot\-instance price is set to be the same as the On\-Demand price \(which is $0\.003 for this tutorial\)\. Setting the price in this way maximizes the chances that the request will be fulfilled\.

## Creating a Spot Instance request<a name="tutor-spot-net-submit"></a>

The following snippet shows you how to create a Spot Instance request with the elements you gathered earlier\.

The example [at the end of this topic](#tutor-spot-net-main) shows this snippet in use\.

```
    //
    // Method to create a Spot Instance request
    private static async Task<SpotInstanceRequest> CreateSpotInstanceRequest(
      IAmazonEC2 ec2Client, string amiId, string securityGroupName,
      InstanceType instanceType, string spotPrice, int instanceCount)
    {
      var launchSpecification = new LaunchSpecification{
        ImageId = amiId,
        InstanceType = instanceType
      };
      launchSpecification.SecurityGroups.Add(securityGroupName);
      var request = new RequestSpotInstancesRequest{
        SpotPrice = spotPrice,
        InstanceCount = instanceCount,
        LaunchSpecification = launchSpecification
      };

      RequestSpotInstancesResponse result =
        await ec2Client.RequestSpotInstancesAsync(request);
      return result.SpotInstanceRequests[0];
    }
```

The important value returned from this method is the Spot Instance request ID, which is contained in the `SpotInstanceRequestId` member of the returned [SpotInstanceRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSpotInstanceRequest.html) object\.

**Note**  
You will be charged for any Spot Instances that are launched\. To avoid unnecessary costs be sure to [cancel any requests](#tutor-spot-net-clean-up-request) and [terminate any instances](#tutor-spot-net-clean-up-instance)\.

## Determine the state of your Spot Instance request<a name="tutor-spot-net-request-state"></a>

The following snippet shows you how to get information about your Spot Instance request\. You can use that information to make certain decisions in your code, such as whether to continue waiting for a Spot Instance request to be fulfilled\.

The example [at the end of this topic](#tutor-spot-net-main) shows this snippet in use\.

```
    //
    // Method to get information about a Spot Instance request, including the status,
    // instance ID, etc.
    // It gets the information for a specific request (as opposed to all requests).
    private static async Task<SpotInstanceRequest> GetSpotInstanceRequestInfo(
      IAmazonEC2 ec2Client, string requestId)
    {
      var describeRequest = new DescribeSpotInstanceRequestsRequest();
      describeRequest.SpotInstanceRequestIds.Add(requestId);

      DescribeSpotInstanceRequestsResponse describeResponse =
        await ec2Client.DescribeSpotInstanceRequestsAsync(describeRequest);
      return describeResponse.SpotInstanceRequests[0];
    }
```

The method returns information about the Spot Instance request such as the instance ID, it's state, and the status code\. You can see the status codes for Spot Instance requests in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/spot-bid-status.html#spot-instance-bid-status-understand) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/spot-bid-status.html#spot-instance-bid-status-understand)\.

## Clean up your Spot Instance requests<a name="tutor-spot-net-clean-up-request"></a>

When you no longer need to request Spot Instances, it's important to cancel any outstanding requests to prevent those requests from being re\-fulfilled\. The following snippet shows you how to cancel a Spot Instance request\.

The example [at the end of this topic](#tutor-spot-net-main) shows this snippet in use\.

```
    //
    // Method to cancel a Spot Instance request
    private static async Task CancelSpotInstanceRequest(
      IAmazonEC2 ec2Client, string requestId)
    {
      var cancelRequest = new CancelSpotInstanceRequestsRequest();
      cancelRequest.SpotInstanceRequestIds.Add(requestId);

      await ec2Client.CancelSpotInstanceRequestsAsync(cancelRequest);
    }
```

## Clean up your Spot Instances<a name="tutor-spot-net-clean-up-instance"></a>

To avoid unnecessary costs, it's important that you terminate any instances that were started from Spot Instance requests; simply canceling Spot Instance requests will not terminate your instances, which means that you'll continue to be charged for them\. The following snippet shows you how to terminate an instance after you obtain the instance identifier for an active Spot Instance\.

The example [at the end of this topic](#tutor-spot-net-main) shows this snippet in use\.

```
    //
    // Method to terminate a Spot Instance
    private static async Task TerminateSpotInstance(
      IAmazonEC2 ec2Client, string requestId)
    {
      var describeRequest = new DescribeSpotInstanceRequestsRequest();
      describeRequest.SpotInstanceRequestIds.Add(requestId);

      // Retrieve the Spot Instance request to check for running instances.
      DescribeSpotInstanceRequestsResponse describeResponse =
        await ec2Client.DescribeSpotInstanceRequestsAsync(describeRequest);

      // If there are any running instances, terminate them
      if(   (describeResponse.SpotInstanceRequests[0].Status.Code
              == "request-canceled-and-instance-running")
         || (describeResponse.SpotInstanceRequests[0].State == SpotInstanceState.Active))
      {
        TerminateInstancesResponse response =
          await ec2Client.TerminateInstancesAsync(new TerminateInstancesRequest{
            InstanceIds = new List<string>(){
              describeResponse.SpotInstanceRequests[0].InstanceId } });
        foreach (InstanceStateChange item in response.TerminatingInstances)
        {
          Console.WriteLine($"\n  Terminated instance: {item.InstanceId}");
          Console.WriteLine($"  Instance state: {item.CurrentState.Name}\n");
        }
      }
    }
```

## Complete code<a name="tutor-spot-net-main"></a>

The following code example calls the methods described earlier to create and cancel a Spot Instance request and terminate a Spot Instance\.

### SDK references<a name="w4aac19c19c21c43b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)

  Class [InstanceType](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstanceType.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [CancelSpotInstanceRequestsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCancelSpotInstanceRequestsRequest.html)

  Class [DescribeSpotInstanceRequestsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSpotInstanceRequestsRequest.html)

  Class [DescribeSpotInstanceRequestsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSpotInstanceRequestsResponse.html)

  Class [InstanceStateChange](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstanceStateChange.html)

  Class [LaunchSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TLaunchSpecification.html)

  Class [RequestSpotInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRequestSpotInstancesRequest.html)

  Class [RequestSpotInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRequestSpotInstancesResponse.html)

  Class [SpotInstanceRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSpotInstanceRequest.html)

  Class [TerminateInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TTerminateInstancesRequest.html)

  Class [TerminateInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TTerminateInstancesResponse.html)

### The code<a name="w4aac19c19c21c43b7b1"></a>

```
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2SpotInstanceRequests
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Some default values.
      // These could be made into command-line arguments instead.
      var instanceType = InstanceType.T1Micro;
      string securityGroupName = "default";
      string spotPrice = "0.003";
      int instanceCount = 1;

      // Parse the command line arguments
      if((args.Length != 1) || (!args[0].StartsWith("ami-")))
      {
        Console.WriteLine("\nUsage: EC2SpotInstanceRequests ami");
        Console.WriteLine("  ami: the Amazon Machine Image to use for the Spot Instances.");
        return;
      }

      // Create the Amazon EC2 client.
      var ec2Client = new AmazonEC2Client();

      // Create the Spot Instance request and record its ID
      Console.WriteLine("\nCreating spot instance request...");
      var req = await CreateSpotInstanceRequest(
        ec2Client, args[0], securityGroupName, instanceType, spotPrice, instanceCount);
      string requestId = req.SpotInstanceRequestId;

      // Wait for an EC2 Spot Instance to become active
      Console.WriteLine(
        $"Waiting for Spot Instance request with ID {requestId} to become active...");
      int wait = 1;
      var start = DateTime.Now;
      while(true)
      {
        Console.Write(".");

        // Get and check the status to see if the request has been fulfilled.
        var requestInfo = await GetSpotInstanceRequestInfo(ec2Client, requestId);
        if(requestInfo.Status.Code == "fulfilled")
        {
          Console.WriteLine($"\nSpot Instance request {requestId} " +
            $"has been fulfilled by instance {requestInfo.InstanceId}.\n");
          break;
        }

        // Wait a bit and try again, longer each time (1, 2, 4, ...)
        Thread.Sleep(wait);
        wait = wait * 2;
      }

      // Show the user how long it took to fulfill the Spot Instance request.
      TimeSpan span = DateTime.Now.Subtract(start);
      Console.WriteLine($"That took {span.TotalMilliseconds} milliseconds");

      // Perform actions here as needed.
      // For this example, simply wait for the user to hit a key.
      // That gives them a chance to look at the EC2 console to see
      // the running instance if they want to.
      Console.WriteLine("Press any key to start the cleanup...");
      Console.ReadKey(true);

      // Cancel the request.
      // Do this first to make sure that the request can't be re-fulfilled
      // once the Spot Instance has been terminated.
      Console.WriteLine("Canceling Spot Instance request...");
      await CancelSpotInstanceRequest(ec2Client, requestId);

      // Terminate the Spot Instance that's running.
      Console.WriteLine("Terminating the running Spot Instance...");
      await TerminateSpotInstance(ec2Client, requestId);

      Console.WriteLine("Done. Press any key to exit...");
      Console.ReadKey(true);
    }


    //
    // Method to create a Spot Instance request
    private static async Task<SpotInstanceRequest> CreateSpotInstanceRequest(
      IAmazonEC2 ec2Client, string amiId, string securityGroupName,
      InstanceType instanceType, string spotPrice, int instanceCount)
    {
      var launchSpecification = new LaunchSpecification{
        ImageId = amiId,
        InstanceType = instanceType
      };
      launchSpecification.SecurityGroups.Add(securityGroupName);
      var request = new RequestSpotInstancesRequest{
        SpotPrice = spotPrice,
        InstanceCount = instanceCount,
        LaunchSpecification = launchSpecification
      };

      RequestSpotInstancesResponse result =
        await ec2Client.RequestSpotInstancesAsync(request);
      return result.SpotInstanceRequests[0];
    }


    //
    // Method to get information about a Spot Instance request, including the status,
    // instance ID, etc.
    // It gets the information for a specific request (as opposed to all requests).
    private static async Task<SpotInstanceRequest> GetSpotInstanceRequestInfo(
      IAmazonEC2 ec2Client, string requestId)
    {
      var describeRequest = new DescribeSpotInstanceRequestsRequest();
      describeRequest.SpotInstanceRequestIds.Add(requestId);

      DescribeSpotInstanceRequestsResponse describeResponse =
        await ec2Client.DescribeSpotInstanceRequestsAsync(describeRequest);
      return describeResponse.SpotInstanceRequests[0];
    }


    //
    // Method to cancel a Spot Instance request
    private static async Task CancelSpotInstanceRequest(
      IAmazonEC2 ec2Client, string requestId)
    {
      var cancelRequest = new CancelSpotInstanceRequestsRequest();
      cancelRequest.SpotInstanceRequestIds.Add(requestId);

      await ec2Client.CancelSpotInstanceRequestsAsync(cancelRequest);
    }


    //
    // Method to terminate a Spot Instance
    private static async Task TerminateSpotInstance(
      IAmazonEC2 ec2Client, string requestId)
    {
      var describeRequest = new DescribeSpotInstanceRequestsRequest();
      describeRequest.SpotInstanceRequestIds.Add(requestId);

      // Retrieve the Spot Instance request to check for running instances.
      DescribeSpotInstanceRequestsResponse describeResponse =
        await ec2Client.DescribeSpotInstanceRequestsAsync(describeRequest);

      // If there are any running instances, terminate them
      if(   (describeResponse.SpotInstanceRequests[0].Status.Code
              == "request-canceled-and-instance-running")
         || (describeResponse.SpotInstanceRequests[0].State == SpotInstanceState.Active))
      {
        TerminateInstancesResponse response =
          await ec2Client.TerminateInstancesAsync(new TerminateInstancesRequest{
            InstanceIds = new List<string>(){
              describeResponse.SpotInstanceRequests[0].InstanceId } });
        foreach (InstanceStateChange item in response.TerminatingInstances)
        {
          Console.WriteLine($"\n  Terminated instance: {item.InstanceId}");
          Console.WriteLine($"  Instance state: {item.CurrentState.Name}\n");
        }
      }
    }
  }
}
```

**Additional considerations**
+ After you run the tutorial, it's a good idea to sign in to the [Amazon EC2 console](https://console.aws.amazon.com/ec2/) to verify that the [Spot Instance request](https://console.aws.amazon.com/ec2sp/v1/spot/home) has been canceled and that the [Spot Instance](https://console.aws.amazon.com/ec2/v2/home#Instances) has been terminated\.