# Amazon EC2 Auto Scaling examples using AWS SDK for \.NET<a name="csharp_auto-scaling_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon EC2 Auto Scaling\.

*Actions* are code excerpts that show you how to call individual Amazon EC2 Auto Scaling functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Amazon EC2 Auto Scaling functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w359aac21c17c13c25c13)
+ [Scenarios](#w359aac21c17c13c25c15)

## Actions<a name="w359aac21c17c13c25c13"></a>

### Create a group<a name="auto-scaling_CreateAutoScalingGroup_csharp_topic"></a>

The following code example shows how to create an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Creates a new Amazon EC2 Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name to use for the new Auto Scaling
        /// group.</param>
        /// <param name="launchTemplateName">The name of the Amazon EC2 launch template
        /// to use to create instances in the group.</param>
        /// <param name="serviceLinkedRoleARN">The AWS Identity and Access
        /// Management (IAM) service-linked role that provides the permissions
        /// to use with the Auso Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> CreateAutoScalingGroup(
            AmazonAutoScalingClient client,
            string groupName,
            string launchTemplateName,
            string serviceLinkedRoleARN)
        {
            var templateSpecification = new LaunchTemplateSpecification
            {
                LaunchTemplateName = launchTemplateName,
            };

            var zoneList = new List<string>
            {
                "us-east-2a",
            };

            var request = new CreateAutoScalingGroupRequest
            {
                AutoScalingGroupName = groupName,
                AvailabilityZones = zoneList,
                LaunchTemplate = templateSpecification,
                MaxSize = 1,
                MinSize = 1,
                ServiceLinkedRoleARN = serviceLinkedRoleARN,
            };

            var response = await client.CreateAutoScalingGroupAsync(request);
            Console.WriteLine(groupName + " Auto Scaling Group created");
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  For API details, see [CreateAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/CreateAutoScalingGroup) in *AWS SDK for \.NET API Reference*\. 

### Delete a group<a name="auto-scaling_DeleteAutoScalingGroup_csharp_topic"></a>

The following code example shows how to delete an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Deletes an Auto Scaling group.
        /// </summary>
        /// <param name="autoScalingClient">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> DeleteAutoScalingGroupAsync(
            AmazonAutoScalingClient autoScalingClient,
            string groupName)
        {
            var deleteAutoScalingGroupRequest = new DeleteAutoScalingGroupRequest
            {
                AutoScalingGroupName = groupName,
                ForceDelete = true,
            };

            var response = await autoScalingClient.DeleteAutoScalingGroupAsync(deleteAutoScalingGroupRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"You successfully deleted {groupName}");
                return true;
            }

            Console.WriteLine($"Couldn't delete {groupName}.");
            return false;
        }
```
+  For API details, see [DeleteAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DeleteAutoScalingGroup) in *AWS SDK for \.NET API Reference*\. 

### Disable metrics collection for a group<a name="auto-scaling_DisableMetricsCollection_csharp_topic"></a>

The following code example shows how to disable CloudWatch metrics collection for an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Disables the collection of metric data for an Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> DisableMetricsCollectionAsync(AmazonAutoScalingClient client, string groupName)
        {
            var request = new DisableMetricsCollectionRequest
            {
                AutoScalingGroupName = groupName,
            };

            var response = await client.DisableMetricsCollectionAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  For API details, see [DisableMetricsCollection](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DisableMetricsCollection) in *AWS SDK for \.NET API Reference*\. 

### Enable metrics collection for a group<a name="auto-scaling_EnableMetricsCollection_csharp_topic"></a>

The following code example shows how to enable CloudWatch metrics collection for an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Enables the collection of metric data for an Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> EnableMetricsCollectionAsync(AmazonAutoScalingClient client, string groupName)
        {
            var listMetrics = new List<string>
            {
                "GroupMaxSize",
            };

            var collectionRequest = new EnableMetricsCollectionRequest
            {
                AutoScalingGroupName = groupName,
                Metrics = listMetrics,
                Granularity = "1Minute",
            };

            var response = await client.EnableMetricsCollectionAsync(collectionRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  For API details, see [EnableMetricsCollection](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/EnableMetricsCollection) in *AWS SDK for \.NET API Reference*\. 

### Get information about groups<a name="auto-scaling_DescribeAutoScalingGroups_csharp_topic"></a>

The following code example shows how to get information about Auto Scaling groups\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Gets data about the instances in an Amazon EC2 Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A list of Auto Scaling details.</returns>
        public static async Task<List<AutoScalingInstanceDetails>> DescribeAutoScalingInstancesAsync(
            AmazonAutoScalingClient client,
            string groupName)
        {
            var groups = await DescribeAutoScalingGroupsAsync(client, groupName);
            var instanceIds = new List<string>();
            groups.ForEach(group =>
            {
                if (group.AutoScalingGroupName == groupName)
                {
                    group.Instances.ForEach(instance =>
                    {
                        instanceIds.Add(instance.InstanceId);
                    });
                }
            });

            var scalingGroupsRequest = new DescribeAutoScalingInstancesRequest
            {
                MaxRecords = 10,
                InstanceIds = instanceIds,
            };

            var response = await client.DescribeAutoScalingInstancesAsync(scalingGroupsRequest);
            var instanceDetails = response.AutoScalingInstances;

            return instanceDetails;
        }
```
+  For API details, see [DescribeAutoScalingGroups](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeAutoScalingGroups) in *AWS SDK for \.NET API Reference*\. 

### Get information about instances<a name="auto-scaling_DescribeAutoScalingInstances_csharp_topic"></a>

The following code example shows how to get information about Auto Scaling instances\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Gets data about the instances in an Amazon EC2 Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A list of Auto Scaling details.</returns>
        public static async Task<List<AutoScalingInstanceDetails>> DescribeAutoScalingInstancesAsync(
            AmazonAutoScalingClient client,
            string groupName)
        {
            var groups = await DescribeAutoScalingGroupsAsync(client, groupName);
            var instanceIds = new List<string>();
            groups.ForEach(group =>
            {
                if (group.AutoScalingGroupName == groupName)
                {
                    group.Instances.ForEach(instance =>
                    {
                        instanceIds.Add(instance.InstanceId);
                    });
                }
            });

            var scalingGroupsRequest = new DescribeAutoScalingInstancesRequest
            {
                MaxRecords = 10,
                InstanceIds = instanceIds,
            };

            var response = await client.DescribeAutoScalingInstancesAsync(scalingGroupsRequest);
            var instanceDetails = response.AutoScalingInstances;

            return instanceDetails;
        }
```
+  For API details, see [DescribeAutoScalingInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeAutoScalingInstances) in *AWS SDK for \.NET API Reference*\. 

### Get information about scaling activities<a name="auto-scaling_DescribeScalingActivities_csharp_topic"></a>

The following code example shows how to get information about Auto Scaling activities\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Retrieves a list of the Auto Scaling activities for an Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A list of Auto Scaling activities.</returns>
        public static async Task<List<Activity>> DescribeAutoScalingActivitiesAsync(
            AmazonAutoScalingClient client,
            string groupName)
        {
            var scalingActivitiesRequest = new DescribeScalingActivitiesRequest
            {
                AutoScalingGroupName = groupName,
                MaxRecords = 10,
            };

            var response = await client.DescribeScalingActivitiesAsync(scalingActivitiesRequest);
            return response.Activities;
        }
```
+  For API details, see [DescribeScalingActivities](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeScalingActivities) in *AWS SDK for \.NET API Reference*\. 

### Set the desired capacity of a group<a name="auto-scaling_SetDesiredCapacity_csharp_topic"></a>

The following code example shows how to set the desired capacity of an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Sets the desired capacity of an Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <param name="desiredCapacity">The desired capacity for the Auto
        /// Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> SetDesiredCapacityAsync(
            AmazonAutoScalingClient client,
            string groupName,
            int desiredCapacity)
        {
            var capacityRequest = new SetDesiredCapacityRequest
            {
                AutoScalingGroupName = groupName,
                DesiredCapacity = desiredCapacity,
            };

            var response = await client.SetDesiredCapacityAsync(capacityRequest);
            Console.WriteLine($"You have set the DesiredCapacity to {desiredCapacity}.");

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  For API details, see [SetDesiredCapacity](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/SetDesiredCapacity) in *AWS SDK for \.NET API Reference*\. 

### Terminate an instance in a group<a name="auto-scaling_TerminateInstanceInAutoScalingGroup_csharp_topic"></a>

The following code example shows how to terminate an instance in an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Terminates all instances in the Auto Scaling group in preparation for
        /// deleting the group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="instanceId">The instance Id of the instance to terminate.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> TerminateInstanceInAutoScalingGroupAsync(
            AmazonAutoScalingClient client,
            string instanceId)
        {
            var request = new TerminateInstanceInAutoScalingGroupRequest
            {
                InstanceId = instanceId,
                ShouldDecrementDesiredCapacity = false,
            };

            var response = await client.TerminateInstanceInAutoScalingGroupAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"You have terminated the instance {instanceId}");
                return true;
            }

            Console.WriteLine($"Could not terminate {instanceId}");
            return false;
        }
```
+  For API details, see [TerminateInstanceInAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/TerminateInstanceInAutoScalingGroup) in *AWS SDK for \.NET API Reference*\. 

### Update a group<a name="auto-scaling_UpdateAutoScalingGroup_csharp_topic"></a>

The following code example shows how to update the configuration for an Auto Scaling group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
        /// <summary>
        /// Updates the capacity of an Auto Scaling group.
        /// </summary>
        /// <param name="client">The  initialized Amazon EC2 Auto Scaling
        /// client object.</param>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <param name="launchTemplateName">The name of the EC2 launch template.</param>
        /// <param name="serviceLinkedRoleARN">The Amazon Resource Name (ARN)
        /// of the AWS Identity and Access Management (IAM) service-linked role.</param>
        /// <param name="maxSize">The maximum number of instances that can be
        /// created for the Auto Scaling group.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the update operation.</returns>
        public static async Task<bool> UpdateAutoScalingGroupAsync(
            AmazonAutoScalingClient client,
            string groupName,
            string launchTemplateName,
            string serviceLinkedRoleARN,
            int maxSize)
        {
            var templateSpecification = new LaunchTemplateSpecification
            {
                LaunchTemplateName = launchTemplateName,
            };

            var groupRequest = new UpdateAutoScalingGroupRequest
            {
                MaxSize = maxSize,
                ServiceLinkedRoleARN = serviceLinkedRoleARN,
                AutoScalingGroupName = groupName,
                LaunchTemplate = templateSpecification,
            };

            var response = await client.UpdateAutoScalingGroupAsync(groupRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"You successfully updated the Auto Scaling group {groupName}.");
                return true;
            }
            else
            {
                return false;
            }
        }
```
+  For API details, see [UpdateAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/UpdateAutoScalingGroup) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w359aac21c17c13c25c15"></a>

### Manage groups and instances<a name="auto-scaling_Scenario_GroupsAndInstances_csharp_topic"></a>

The following code example shows how to:
+ Create an Amazon EC2 Auto Scaling group and configure it with a launch template and Availability Zones\.
+ Get information about the group and running instances\.
+ Enable Amazon CloudWatch metrics collection on the group\.
+ Update the desired capacity of the group and wait for an instance to start\.
+ Terminate an instance in the group\.
+ List scaling activities that occur in response to user requests and capacity changes\.
+ Get statistics for CloudWatch metrics that are collected during the example\.
+ Stop collecting metrics, terminate all instances, and delete the group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

```
global using Amazon;
global using Amazon.AutoScaling;
global using Amazon.AutoScaling.Model;
global using AutoScale_Basics;


var imageId = "ami-05803413c51f242b7";
var instanceType = "t2.micro";
var launchTemplateName = "AutoScaleLaunchTemplate";

// The name of the Auto Scaling group.
var groupName = "AutoScaleExampleGroup";

// The Amazon Resource Name (ARN) of the AWS Identity and Access Management (IAM) service-linked role.
var serviceLinkedRoleARN = "<Enter Value>";

var client = new AmazonAutoScalingClient(RegionEndpoint.USEast2);

Console.WriteLine("Auto Scaling Basics");
DisplayDescription();

// Create the launch template and save the template Id to use when deleting the
// launch template at the end of the application.
var launchTemplateId = await EC2Methods.CreateLaunchTemplateAsync(imageId, instanceType, launchTemplateName);

// Confirm that the template was created by asking for a description of it.
await EC2Methods.DescribeLaunchTemplateAsync(launchTemplateName);

PressEnter();

Console.WriteLine($"--- Creating an Auto Scaling group named {groupName}. ---");
var success = await AutoScaleMethods.CreateAutoScalingGroup(
    client,
    groupName,
    launchTemplateName,
    serviceLinkedRoleARN);

// Keep checking the details of the new group until its lifecycle state
// is "InService".
Console.WriteLine($"Waiting for the Auto Scaling group to be active.");

List<AutoScalingInstanceDetails> instanceDetails;

do
{
    instanceDetails = await AutoScaleMethods.DescribeAutoScalingInstancesAsync(client, groupName);
}
while (instanceDetails.Count <= 0);

Console.WriteLine($"Auto scaling group {groupName} successfully created.");
Console.WriteLine($"{instanceDetails.Count} instances were created for the group.");

// Display the details of the Auto Scaling group.
instanceDetails.ForEach(detail =>
{
    Console.WriteLine($"Group name: {detail.AutoScalingGroupName}");
});

PressEnter();

Console.WriteLine($"\n--- Enable metrics collection for {groupName}");
await AutoScaleMethods.EnableMetricsCollectionAsync(client, groupName);

// Show the metrics that are collected for the group.

// Update the maximum size of the group to three instances.
Console.WriteLine("--- Update the Auto Scaling group to increase max size to 3 ---");
int maxSize = 3;
await AutoScaleMethods.UpdateAutoScalingGroupAsync(client, groupName, launchTemplateName, serviceLinkedRoleARN, maxSize);

Console.WriteLine("--- Describe all Auto Scaling groups to show the current state of the group ---");
var groups = await AutoScaleMethods.DescribeAutoScalingGroupsAsync(client, groupName);

DisplayGroupDetails(groups);

PressEnter();

Console.WriteLine("--- Describe account limits ---");
await AutoScaleMethods.DescribeAccountLimitsAsync(client);

Console.WriteLine("Wait 1 min for the resources, including the instance. Otherwise, an empty instance Id is returned");
System.Threading.Thread.Sleep(60000);

Console.WriteLine("--- Set desired capacity to 2 ---");
int desiredCapacity = 2;
await AutoScaleMethods.SetDesiredCapacityAsync(client, groupName, desiredCapacity);

Console.WriteLine("--- Get the two instance Id values and state ---");

// Empty the group before getting the details again.
groups.Clear();
groups = await AutoScaleMethods.DescribeAutoScalingGroupsAsync(client, groupName);
if (groups is not null)
{
    foreach (AutoScalingGroup group in groups)
    {
        Console.WriteLine($"The group name is {group.AutoScalingGroupName}");
        Console.WriteLine($"The group ARN is {group.AutoScalingGroupARN}");
        var instances = group.Instances;
        foreach (Instance instance in instances)
        {
            Console.WriteLine($"The instance id is {instance.InstanceId}");
            Console.WriteLine($"The lifecycle state is {instance.LifecycleState}");
        }
    }
}

Console.WriteLine("**** List the scaling activities that have occurred for the group");
var activities = await AutoScaleMethods.DescribeAutoScalingActivitiesAsync(client, groupName);
if (activities is not null)
{
    activities.ForEach(activity =>
    {
        Console.WriteLine($"The activity Id is {activity.ActivityId}");
        Console.WriteLine($"The activity details are {activity.Details}");
    });
}

// Display the Amazon CloudWatch metrics that have been collected.
var metrics = await CloudWatchMethods.GetCloudWatchMetricsAsync(groupName);
Console.WriteLine($"Metrics collected for {groupName}:");
metrics.ForEach(metric =>
{
    Console.Write($"Metric name: {metric.MetricName}\t");
    Console.WriteLine($"Namespace: {metric.Namespace}");
});

var dataPoints = await CloudWatchMethods.GetMetricStatisticsAsync(groupName);
Console.WriteLine("Details for the metrics collected:");
dataPoints.ForEach(detail =>
{
    Console.WriteLine(detail);
});

// Disable metrics collection.
Console.WriteLine("Disabling the collection of metrics for {groupName}.");
success = await AutoScaleMethods.DisableMetricsCollectionAsync(client, groupName);

if (success)
{
    Console.WriteLine($"Successfully stopped metrics collection for {groupName}.");
}
else
{
    Console.WriteLine($"Could not stop metrics collection for {groupName}.");
}

// Terminate all instances in the group.
Console.WriteLine("--- Now terminating all instances in the AWS Auto Scaling group ---");

if (groups is not null)
{
    groups.ForEach(group =>
    {
        // Only delete instances in the AutoScaling group we created.
        if (group.AutoScalingGroupName == groupName)
        {
            group.Instances.ForEach(async instance =>
            {
                await AutoScaleMethods.TerminateInstanceInAutoScalingGroupAsync(client, instance.InstanceId);
            });
        }
    });
}

// After all instances are terminated, delete the group.
Console.WriteLine("--- Deleting the Auto Scaling group ---");
await AutoScaleMethods.DeleteAutoScalingGroupAsync(client, groupName);

// Delete the launch template.
var deletedLaunchTemplateName = await EC2Methods.DeleteLaunchTemplateAsync(launchTemplateId);

if (deletedLaunchTemplateName == launchTemplateName)
{
    Console.WriteLine("Successfully deleted the launch template.");
}

Console.WriteLine("The demo is now concluded.");

void DisplayDescription()
{
    Console.WriteLine("This code example performs the following operations:");
    Console.WriteLine(" 1. Creates an Amazon EC2 launch template.");
    Console.WriteLine(" 2. Creates an Auto Scaling group.");
    Console.WriteLine(" 3. Shows the details of the new Auto Scaling group");
    Console.WriteLine("    to show that only one instance was created.");
    Console.WriteLine(" 4. Enables metrics collection.");
    Console.WriteLine(" 5. Updates the AWS Auto Scaling group to increase the");
    Console.WriteLine("    capacity to three.");
    Console.WriteLine(" 6. Describes Auto Scaling groups again to show the");
    Console.WriteLine("    current state of the group.");
    Console.WriteLine(" 7. Changes the desired capacity of the Auto Scaling");
    Console.WriteLine("    group to use an additional instance.");
    Console.WriteLine(" 8. Shows that there are now instances in the group.");
    Console.WriteLine(" 9. Lists the scaling activities that have occurred for the group.");
    Console.WriteLine("10. Displays the Amazon CloudWatch metrics that have");
    Console.WriteLine("    been collected.");
    Console.WriteLine("11. Disables metrics collection.");
    Console.WriteLine("12. Terminates all instances in the Auto Scaling group.");
    Console.WriteLine("13. Deletes the Auto Scaling group.");
    Console.WriteLine("14. Deletes the Amazon EC2 launch template.");
}

void DisplayGroupDetails(List<AutoScalingGroup> groups)
{
    if (groups is null)
        return;

    groups.ForEach(group =>
    {
        Console.WriteLine($"Group name:\t{group.AutoScalingGroupName}");
        Console.WriteLine($"Group created:\t{group.CreatedTime}");
        Console.WriteLine($"Maximum number of instances:\t{group.MaxSize}");
        Console.WriteLine($"Desired number of instances:\t{group.DesiredCapacity}");
    });
}

void PressEnter()
{
    Console.WriteLine("Press <Enter> to continue.");
    _ = Console.ReadLine();
    Console.WriteLine("\n\n");
}
```
Define functions that are called by the scenario to manage launch templates and metrics\. These functions wrap Amazon EC2 and CloudWatch actions\.  

```
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// The methods in this class create and delete an Amazon Elastic Compute
    /// Cloud (Amazon EC2) launch template for use by the Amazon EC2 Auto
    /// Scaling scenario.
    /// </summary>
    public class EC2Methods
    {
        /// <summary>
        /// Create a new Amazon EC2 launch template.
        /// </summary>
        /// <param name="imageId">The image Id to use for instances launched
        /// using the Amazon EC2 launch template.</param>
        /// <param name="instanceType">The type of EC2 instances to create.</param>
        /// <param name="launchTemplateName">The name of the launch template.</param>
        /// <returns>Returns the TemplaceID of the new launch template.</returns>
        public static async Task<string> CreateLaunchTemplateAsync(
            string imageId,
            string instanceType,
            string launchTemplateName)
        {
            var client = new AmazonEC2Client();

            var request = new CreateLaunchTemplateRequest
            {
                LaunchTemplateData = new RequestLaunchTemplateData
                {
                    ImageId = imageId,
                    InstanceType = instanceType,
                },
                LaunchTemplateName = launchTemplateName,
            };

            var response = await client.CreateLaunchTemplateAsync(request);

            return response.LaunchTemplate.LaunchTemplateId;
        }

        /// <summary>
        /// Deletes an Amazon EC2 launch template.
        /// </summary>
        /// <param name="launchTemplateId">The TemplateId of the launch template to
        /// delete.</param>
        /// <returns>The name of the EC2 launch template that was deleted.</returns>
        public static async Task<string> DeleteLaunchTemplateAsync(string launchTemplateId)
        {
            var client = new AmazonEC2Client();

            var request = new DeleteLaunchTemplateRequest
            {
                LaunchTemplateId = launchTemplateId,
            };

            var response = await client.DeleteLaunchTemplateAsync(request);
            return response.LaunchTemplate.LaunchTemplateName;
        }

        /// <summary>
        /// Retrieves a information about an EC2 launch template.
        /// </summary>
        /// <param name="launchTemplateName">The name of the EC2 launch template.</param>
        /// <returns>A Boolean value that indicates the success or failure of
        /// the operation.</returns>
        public static async Task<bool> DescribeLaunchTemplateAsync(string launchTemplateName)
        {
            var client = new AmazonEC2Client();

            var request = new DescribeLaunchTemplatesRequest
            {
                LaunchTemplateNames = new List<string> { launchTemplateName, },
            };

            var response = await client.DescribeLaunchTemplatesAsync(request);

            if (response.LaunchTemplates != null)
            {
                response.LaunchTemplates.ForEach(template =>
                {
                    Console.Write($"{template.LaunchTemplateName}\t");
                    Console.WriteLine(template.LaunchTemplateId);
                });

                return true;
            }

            return false;
        }
    }


    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// The method of this class display the metrics collected for the Amazon
    /// EC2 Auto Scaling group created by the Amazon EC2 Auto Scaling scenario.
    /// </summary>
    public class CloudWatchMethods
    {
        /// <summary>
        /// Retrieves the metrics information collection for the Auto Scaling group.
        /// </summary>
        /// <param name="groupName">The name of the Auto Scaling group.</param>
        /// <returns>A list of Metrics collected for the Auto Scaling group.</returns>
        public static async Task<List<Metric>> GetCloudWatchMetricsAsync(string groupName)
        {
            var client = new AmazonCloudWatchClient();

            var filter = new DimensionFilter
            {
                Name = "AutoScalingGroupName",
                Value = $"{groupName}",
            };

            var request = new ListMetricsRequest
            {
                MetricName = "AutoScalingGroupName",
                Dimensions = new List<DimensionFilter> { filter },
                Namespace = "AWS/AutoScaling",
            };

            var response = await client.ListMetricsAsync(request);

            return response.Metrics;
        }

        /// <summary>
        /// Retrieves the metric data collected for an Amazon EC2 Auto Scaling group.
        /// </summary>
        /// <param name="groupName">The name of the Amazon EC2 Auto Scaling group.</param>
        /// <returns>A list of data points.</returns>
        public static async Task<List<Datapoint>> GetMetricStatisticsAsync(string groupName)
        {
            var client = new AmazonCloudWatchClient();

            var metricDimensions = new List<Dimension>
            {
                new Dimension
                {
                    Name = "AutoScalingGroupName",
                    Value = $"{groupName}",
                },
            };

            // The start time will be yesterday.
            var startTime = DateTime.UtcNow.AddDays(-1);

            var request = new GetMetricStatisticsRequest
            {
                MetricName = "AutoScalingGroupName",
                Dimensions = metricDimensions,
                Namespace = "AWS/AutoScaling",
                Period = 60, // 60 seconds
                Statistics = new List<string>() { "Minimum" },
                StartTimeUtc = startTime,
                EndTimeUtc = DateTime.UtcNow,
            };

            var response = await client.GetMetricStatisticsAsync(request);

            return response.Datapoints;
        }
```
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [CreateAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/CreateAutoScalingGroup)
  + [DeleteAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DeleteAutoScalingGroup)
  + [DescribeAutoScalingGroups](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeAutoScalingGroups)
  + [DescribeAutoScalingInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeAutoScalingInstances)
  + [DescribeScalingActivities](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DescribeScalingActivities)
  + [DisableMetricsCollection](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/DisableMetricsCollection)
  + [EnableMetricsCollection](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/EnableMetricsCollection)
  + [SetDesiredCapacity](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/SetDesiredCapacity)
  + [TerminateInstanceInAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/TerminateInstanceInAutoScalingGroup)
  + [UpdateAutoScalingGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/autoscaling-2011-01-01/UpdateAutoScalingGroup)