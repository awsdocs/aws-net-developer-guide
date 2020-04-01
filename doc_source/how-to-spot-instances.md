# Amazon EC2 Spot Instance Examples<a name="how-to-spot-instances"></a>

This topic describes how to use the AWS SDK for \.NET to create, cancel, and terminate an Amazon EC2 Spot Instance\.

**Topics**
+ [Overview](#tutor-spot-net-overview)
+ [Prerequisites](#tutor-spot-net-prereq)
+ [Setting Up Your Credentials](#tutor-spot-net-credentials)
+ [Submitting Your Spot Request](#tutor-spot-net-submit)
+ [Determining the State of Your Spot Request](#tutor-spot-net-request-state)
+ [Cleaning Up Your Spot Requests and Instances](#tutor-spot-net-cleaning-up)
+ [Putting it all Together](#tutor-spot-net-main)

## Overview<a name="tutor-spot-net-overview"></a>

Spot Instances enable you to request unused Amazon EC2 capacity and run any instances that you acquire for as long as your request exceeds the current *Spot Price*\. Amazon EC2 changes the Spot Price periodically based on supply and demand, but will never exceed 90% of the On\-Demand Instance price; customers whose requests meet or exceed the Spot Price gain access to the available Spot Instances\. Like On\-Demand Instances and Reserved Instances, Spot Instances provide another option for obtaining more compute capacity\.

Spot Instances can significantly lower your Amazon EC2 costs for applications such as batch processing, scientific research, image processing, video encoding, data and web crawling, financial analysis, and testing\. Additionally, Spot Instances are an excellent option when you need large amounts of computing capacity, but the need for that capacity is not urgent\.

For more information about Spot Instances, see [https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances.html](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances.html) in the *[Amazon EC2 User Guide for Linux Instances](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/)*\.

To use Spot Instances, place a Spot Instance request specifying the maximum price you are willing to pay per instance hour; this is your request\. If your request exceeds the current Spot Price, your request is fulfilled and your instances will run until either you choose to terminate them or the Spot Price increases above your request \(whichever is sooner\)\. You can terminate a Spot Instance programmatically, as shown in this tutorial, by using the [AWS Management Console](https://console.aws.amazon.com/ec2/home), or by using the [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\.

You can also specify the amount you are willing to pay for Spot Instances as a percentage of the On\-Demand Instance price\. If the specified price is exceeded, then the Spot Instance will terminate\.

Spot Instances perform exactly like other Amazon EC2 instances while running, and like other Amazon EC2 instances, Spot Instances can be terminated when you no longer need them\.

You pay the Spot price that’s in effect for the time period your instances are running\. Spot Instance prices are set by Amazon EC2 and adjust gradually based on long\-term trends in supply and demand for Spot Instance capacity\. You can also specify the amount you are willing to pay for a Spot Instance as a percentage of the On\-Demand Instance price\.

This tutorial provides an overview of how to use the \.NET programming environment to do the following\.
+ Submit a Spot request
+ Determine when the Spot request becomes fulfilled
+ Cancel the Spot request
+ Terminate associated instances

## Prerequisites<a name="tutor-spot-net-prereq"></a>

This tutorial assumes you have signed up for AWS, set up your \.NET development environment, and installed the AWS SDK for \.NET\. If you use the Microsoft Visual Studio development environment, we recommend you also install the AWS Toolkit for Visual Studio\. For instructions on setting up your environment, see [Getting Started with the AWS SDK for \.NET](net-dg-setup.md)\.

## Setting Up Your Credentials<a name="tutor-spot-net-credentials"></a>

For information about how to use your AWS credentials with the SDK, see [Configuring AWS Credentials](net-dg-config-creds.md)\.

## Submitting Your Spot Request<a name="tutor-spot-net-submit"></a>

To submit a Spot request, you first need to determine the instance type, the Amazon Machine Image \(AMI\), and the maximum request you want to offer\. You must also include a security group, so that you can log into the instance if you want to\. For more information about creating security groups, see [Creating a Security Group in Amazon EC2](security-groups.md#create-security-group)\.

There are several instance types to choose from; go to [Amazon EC2 Instance Types](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/instance-types.html) for a complete list\. For this tutorial, we will use `t1.micro`\. You’ll also want to get the ID of a current Windows AMI\. For more information, see [Finding an AMI](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/finding-an-ami.html) in the Amazon EC2 User Guide for Windows Instances\.

There are many ways to approach requesting Spot Instances\. To get started, we’ll describe three common strategies:
+ Request to ensure that the cost is less than on\-demand pricing\.
+ Request based on the value of the resulting computation\.
+ Request so as to acquire computing capacity as quickly as possible\.

** *Reduce Cost Below On\-Demand* **  
You have a batch processing job that will take a number of hours or days to run\. However, you are flexible with respect to when it starts and ends\. You want to see if you can complete it for less than the cost of On\-Demand Instances\. You examine the Spot Price history for instance types using either the AWS Management Console or the Amazon EC2 API\. For more information, go to [Viewing Spot Price History](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-spot-instances-history.html)\. After you’ve analyzed the price history for your desired instance type in a given Availability Zone, you have two alternative approaches for your request:  
+ Specify a request at the upper end of the range of Spot Prices, which are still below the On\-Demand price, anticipating that your one\-time Spot request would most likely be fulfilled and run for enough consecutive compute time to complete the job\.
+ Specify a request at the lower end of the price range, and plan to combine many instances launched over time through a persistent request\. The instances would run long enough, in aggregate, to complete the job at an even lower total cost\. \(We will explain how to automate this task later in this tutorial\.\)

** *Pay No More than the Value of the Result* **  
You have a data processing job to run\. You understand the value of the job’s results well enough to know how much they are worth in terms of computing costs\. After you’ve analyzed the Spot Price history for your instance type, you choose a request at which the cost of the computing time is no more than the value of the job’s results\. You create a persistent request and allow it to run intermittently as the Spot Price fluctuates at or below your request\.

** *Acquire Computing Capacity Quickly* **  
You have an unanticipated, short\-term need for additional capacity that is not available through On\-Demand Instances\. After you’ve analyzed the Spot Price history for your instance type, you request above the highest historical price to greatly improve the likelihood your request will be fulfilled quickly and continue computing until it is complete\.

After you have performed your analysis, you are ready to request a Spot Instance\. For this tutorial the default maximum spot\-instance price is set to be the same as the On\-Demand price \(which is $0\.003 for this tutorial\)\. Setting the price in this way maximizes the chances that the request will be fulfilled\. You can determine the types of available instances and the On\-Demand prices for instances by going to [Amazon EC2 Pricing page](https://aws.amazon.com/ec2/pricing/)\.

First specify the \.NET namespaces used in the application\.

```
using System;
using System.Collections.Generic;
using System.Threading;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
```

For information on creating an Amazon EC2 client, see [Creating an Amazon EC2 Client](init-ec2-client.md)\.

Next, to request a Spot Instance, you need to build your request with the parameters we have specified so far\. Start by creating a [RequestSpotInstanceRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRequestSpotInstancesRequest.html) object\. The request object requires the request amount and the number of instances you want to start\. Additionally, you need to set the [LaunchSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TLaunchSpecification.html) for the request, which includes the instance type, AMI ID, and the name of the security group you want to use for the Spot Instances\. After the request is populated, call the [RequestSpotInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2RequestSpotInstancesRequestSpotInstancesRequest.html) method to create the Spot Instance request\. The following example demonstrates how to request a Spot Instance\.

```
        /* Creates a spot instance
         *
         * Takes six args:
         *   AmazonEC2Client ec2Client is the EC2 client through which the spot instance request is made
         *   string amiId is the AMI of the instance to request
         *   string securityGroupName is the name of the security group of the instance to request
         *   InstanceType instanceType is the type of the instance to request
         *   string spotPrice is the price of the instance to request
         *   int instanceCount is the number of instances to request
         *
         * See https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2RequestSpotInstancesRequestSpotInstancesRequest.html
         */
        private static SpotInstanceRequest RequestSpotInstance(
            AmazonEC2Client ec2Client,
            string amiId,
            string securityGroupName,
            InstanceType instanceType,
            string spotPrice,
            int instanceCount)
        {
            RequestSpotInstancesRequest request = new RequestSpotInstancesRequest
            {
                SpotPrice = spotPrice,
                InstanceCount = instanceCount
            };

            LaunchSpecification launchSpecification = new LaunchSpecification
            {
                ImageId = amiId,
                InstanceType = instanceType
            };

            launchSpecification.SecurityGroups.Add(securityGroupName);

            request.LaunchSpecification = launchSpecification;

            var result = ec2Client.RequestSpotInstancesAsync(request);

            return result.Result.SpotInstanceRequests[0];
        }
```

The Spot request ID is contained in the `SpotInstanceRequestId` member of the [SpotInstanceRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSpotInstanceRequest.html) object\.

Running this code will launch a new Spot Instance request\.

**Note**  
You will be charged for any Spot Instances that are launched, so make sure you cancel any requests and terminate any instances you launch to reduce any associated fees\.

There are other options you can use to configure your Spot requests\. To learn more, see [RequestSpotInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2RequestSpotInstancesRequestSpotInstancesRequest.html) in the AWS SDK for \.NET\.

## Determining the State of Your Spot Request<a name="tutor-spot-net-request-state"></a>

Next, we need to wait until the Spot request reaches the `Active` state before proceeding to the last step\. To determine the state of your Spot request, we use the [DescribeSpotInstanceRequests](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSpotInstanceRequestsRequest.html) method to obtain the state of the Spot request ID we want to monitor\.

```
        /* Gets the state of a spot instance request.
         * Takes two args:
         *   AmazonEC2Client ec2Client is the EC2 client through which information about the state of the spot instance is made
         *   string spotRequestId is the ID of the spot instance
         *
         * See https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSpotInstanceRequests.html
         */
        private static SpotInstanceState GetSpotRequestState(
            AmazonEC2Client ec2Client,
            string spotRequestId)
        {
            // Create the describeRequest object with all of the request ids
            // to monitor (e.g. that we started).
            var request = new DescribeSpotInstanceRequestsRequest();
            request.SpotInstanceRequestIds.Add(spotRequestId);

            // Retrieve the request we want to monitor.
            var describeResponse = ec2Client.DescribeSpotInstanceRequestsAsync(request);

            SpotInstanceRequest req = describeResponse.Result.SpotInstanceRequests[0];

            return req.State;
        }
```

## Cleaning Up Your Spot Requests and Instances<a name="tutor-spot-net-cleaning-up"></a>

The final step is to clean up your requests and instances\. It is important to both cancel any outstanding requests and terminate any instances\. Just canceling your requests will not terminate your instances, which means that you will continue to be charged for them\. If you terminate your instances, your Spot requests may be canceled, but there are some scenarios, such as if you use persistent requests, where terminating your instances is not sufficient to stop your request from being re\-fulfilled\. Therefore, it is a best practice to both cancel any active requests and terminate any running instances\.

You use the [CancelSpotInstanceRequests](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CancelSpotInstanceRequestsCancelSpotInstanceRequestsRequest.html) method to cancel a Spot request\. The following example demonstrates how to cancel a Spot request\.

```
        /* Cancels a spot instance request
         * Takes two args:
         *   AmazonEC2Client ec2Client is the EC2 client through which the spot instance is cancelled
         *   string spotRequestId is the ID of the spot instance
         *
         * See https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CancelSpotInstanceRequestsCancelSpotInstanceRequestsRequest.html
         */
        private static void CancelSpotRequest(
            AmazonEC2Client ec2Client,
            string spotRequestId)
        {
            var cancelRequest = new CancelSpotInstanceRequestsRequest();

            cancelRequest.SpotInstanceRequestIds.Add(spotRequestId);

            ec2Client.CancelSpotInstanceRequestsAsync(cancelRequest);
        }
```

You use the [TerminateInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2TerminateInstancesTerminateInstancesRequest.html) method to terminate an instance\. The following example demonstrates how to obtain the instance identifier for an active Spot Instance and terminate the instance\.

```
        /* Terminates a spot instance request
         * Takes two args:
         *   AmazonEC2Client ec2Client is the EC2 client through which the spot instance is terminated
         *   string spotRequestId is the ID of the spot instance
         *
         * See https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2TerminateInstancesTerminateInstancesRequest.html
         */
        private static void TerminateSpotInstance(
            AmazonEC2Client ec2Client,
            string spotRequestId)
        {
            var describeRequest = new DescribeSpotInstanceRequestsRequest();
            describeRequest.SpotInstanceRequestIds.Add(spotRequestId);

            // Retrieve the request we want to monitor.
            var describeResponse = ec2Client.DescribeSpotInstanceRequestsAsync(describeRequest);

            if (SpotInstanceState.Active == describeResponse.Result.SpotInstanceRequests[0].State)
            {
                string instanceId = describeResponse.Result.SpotInstanceRequests[0].InstanceId;

                var terminateRequest = new TerminateInstancesRequest();
                terminateRequest.InstanceIds = new List<string>() { instanceId };

                try
                {
                    ec2Client.TerminateInstancesAsync(terminateRequest);
                }
                catch (AmazonEC2Exception ex)
                {
                    // Check the ErrorCode to see if the instance does not exist.
                    if ("InvalidInstanceID.NotFound" == ex.ErrorCode)
                    {
                        Console.WriteLine("Instance {0} does not exist.", instanceId);
                    }
                    else
                    {
                        // The exception was thrown for another reason, so re-throw the exception.
                        throw;
                    }
                }
            }
        }
```

For more information about terminating active instances, see [Terminating an Amazon EC2 Instance](terminate-instance.md)\.

## Putting it all Together<a name="tutor-spot-net-main"></a>

The following *main* routine calls these methods in the shown order to create, cancel, and terminate a spot instance request\. As the comment states, it takes one argument, the AMI\.

```
        /* Creates, cancels, and terminates a spot instance request
         * 
         *   AmazonEC2Client ec2Client is the EC2 client through which the spot instance is manipulated
         *   string spotRequestId is the ID of the spot instance
         *
         * See https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2TerminateInstancesTerminateInstancesRequest.html
         */

        // Displays information about the command-line args
        private static void Usage()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("");
            Console.WriteLine("Ec2SpotCrud.exe AMI [-s SECURITY_GROUP] [-p SPOT_PRICE] [-c INSTANCE_COUNT] [-h]");
            Console.WriteLine("  where:");
            Console.WriteLine("  AMI is the AMI to use. No default value. Cannot be an empty string.");
            Console.WriteLine("  SECURITY_GROUP is the name of a security group. Default is default. Cannot be an empty string.");
            Console.WriteLine("  SPOT_PRICE is the spot price. Default is 0.003. Must be > 0.001.");
            Console.WriteLine("  INSTANCE_COUNT is the number of instances. Default is 1. Must be > 0.");
            Console.WriteLine("  -h displays this message and quits");
            Console.WriteLine();
        }
        
        /* Creates, cancels, and terminates a spot instance request
         * See Usage() for information about the command-line args
         */
        static void Main(string[] args)
        {
            // Values that aren't easy to pass on the command line            
            RegionEndpoint region = RegionEndpoint.USWest2;
            InstanceType instanceType = InstanceType.T1Micro;
            
            // Default values for optional command-line args
            string securityGroupName = "default";
            string spotPrice = "0.003";
            int instanceCount = 1;

            // Placeholder for the only required command-line arg
            string amiId = "";

            // Parse command-line args
            int i = 0;
            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-s":
                        i++;
                        securityGroupName = args[i];
                        if (securityGroupName == "")
                        {
                            Console.WriteLine("The security group name cannot be blank");
                            Usage();
                            return;
                        }
                        break;
                    case "-p":
                        i++;
                        spotPrice = args[i];
                        double price;
                        double.TryParse(spotPrice, out price);
                        if (price < 0.001)
                        {
                            Console.WriteLine("The spot price must be > 0.001");
                            Usage();
                            return;
                        }
                        break;
                    case "-c":
                        i++;
                        int.TryParse(args[i], out instanceCount);
                        if (instanceCount < 1)
                        {
                            Console.WriteLine("The instance count must be > 0");
                            Usage();
                            return;
                        }
                        break;
                    case "-h":
                        Usage();
                        return;
                    default:
                        amiId = args[i];
                        break;
                }

                i++;
            }

            // Make sure we have an AMI
            if (amiId == "")
            {
                Console.WriteLine("You must supply an AMI");
                Usage();
                return;
            }

            AmazonEC2Client ec2Client = new AmazonEC2Client(region: region);

            Console.WriteLine("Creating spot instance request");

            SpotInstanceRequest req = RequestSpotInstance(ec2Client, amiId, securityGroupName, instanceType, spotPrice, instanceCount);

            string id = req.SpotInstanceRequestId;

            // Wait for it to become active
            Console.WriteLine("Waiting for spot instance request with ID " + id + " to become active");

            int wait = 1;
            int totalTime = 0;

            while (true)
            {
                totalTime += wait;
                Console.Write(".");

                SpotInstanceState state = GetSpotRequestState(ec2Client, id);

                if (state == SpotInstanceState.Active)
                {
                    Console.WriteLine("");
                    break;
                }

                // wait a bit and try again
                Thread.Sleep(wait);

                // wait longer next time
                // 1, 2, 4, ...
                wait = wait * 2;
            }

            // Should be around 1000 (one second)
            Console.WriteLine("That took " + totalTime + " milliseconds");

            // Cancel the request
            Console.WriteLine("Canceling spot instance request");

            CancelSpotRequest(ec2Client, id);

            // Clean everything up
            Console.WriteLine("Terminating spot instance request");

            TerminateSpotInstance(ec2Client, id);

            Console.WriteLine("Done. Press enter to quit");

            Console.ReadLine();
        }
```

See the [complete example](https://github.com/awsdocs/aws-doc-sdk-examples/tree/master/dotnet/example_code_legacy/ec2/Ec2SpotCRUD.cs), including information on how to build and run the example from the command line, on GitHub\.