# Amazon EC2 Auto Scaling examples using AWS SDK for \.NET<a name="csharp_auto-scaling_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon EC2 Auto Scaling\.

*Actions* are code excerpts that show you how to call individual Amazon EC2 Auto Scaling functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Amazon EC2 Auto Scaling functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w198aac21c17b9c19c13)

## Actions<a name="w198aac21c17b9c19c13"></a>

### Create a group<a name="auto-scaling_CreateAutoScalingGroup_csharp_topic"></a>

The following code example shows how to create an Auto Scaling group\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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

### Update a group<a name="auto-scaling_UpdateAutoScalingGroup_csharp_topic"></a>

The following code example shows how to update the configuration for an Auto Scaling group\.

**AWS SDK for \.NET**  
 To learn how to set up and run this example, see [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/AutoScaling#code-examples)\. 
  

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