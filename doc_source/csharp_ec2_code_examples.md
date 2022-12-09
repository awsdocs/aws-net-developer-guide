# Amazon EC2 examples using AWS SDK for \.NET<a name="csharp_ec2_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Amazon EC2\.

*Actions* are code excerpts that show you how to call individual Amazon EC2 functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Amazon EC2 functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w2aac21c17c13c23c13)

## Actions<a name="w2aac21c17c13c23c13"></a>

### Create a VPC<a name="ec2_CreateVpc_csharp_topic"></a>

The following code example shows how to create an Amazon EC2 VPC\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to create an Amazon Elastic Compute Cloud (Amazon EC2) VPC
    /// using the AWS SDK for .NET and .NET Core 5.0.
    /// </summary>
    public class CreateVPC
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then calls the
        /// CreateVpcAsync method to create the VPC.
        /// </summary>
        public static async Task Main()
        {
            // If you do not want to create the VPC in the same AWS Region as
            // the default users on your system, you need to supply the AWS
            // Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var response = await client.CreateVpcAsync(new CreateVpcRequest
            {
                CidrBlock = "10.0.0.0/16",
            });

            Vpc vpc = response.Vpc;

            if (vpc is not null)
            {
                Console.WriteLine($"Created VPC with ID: {vpc.VpcId}.");
            }
        }
    }
```
+  For API details, see [CreateVpc](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/CreateVpc) in *AWS SDK for \.NET API Reference*\. 

### Create a security group<a name="ec2_CreateSecurityGroup_csharp_topic"></a>

The following code example shows how to create an Amazon EC2 security group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to create a security group for an Amazon Elastic Compute
    /// Cloud (Amazon EC2) VPC using the AWS SDK for .NET version 3.7 and
    /// .NET Core 5.0.
    /// </summary>
    public class CreateSecurityGroup
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and uses the
        /// CreateSecurityGroupAsync method to create the security group.
        /// </summary>
        public static async Task Main()
        {
            string vpcId = "vpc-1234567890abcdefa";
            string vpcDescription = "Sample security group";
            string groupName = "sample-security-group";

            var client = new AmazonEC2Client();
            var response = await client.CreateSecurityGroupAsync(new CreateSecurityGroupRequest
            {
                Description = vpcDescription,
                GroupName = groupName,
                VpcId = vpcId,
            });

            string groupId = response.GroupId;

            Console.WriteLine($"Successfully created security group: {groupName} with ID: {groupId}");
        }
    }
```
+  For API details, see [CreateSecurityGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/CreateSecurityGroup) in *AWS SDK for \.NET API Reference*\. 

### Create a security key pair<a name="ec2_CreateKeyPair_csharp_topic"></a>

The following code example shows how to create a security key pair for Amazon EC2\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to create a new Amazon Elastic Compute Cloud (Amazon EC2)
    /// key pair. The example was uses the AWS SDK for .NET version 3.7 and
    /// .NET Core 5.0.
    /// </summary>
    public class CreateKeyPair
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then calls the
        /// CreateKeyPairAsync method to create the new key pair.
        /// </summary>
        public static async Task Main()
        {
            string keyName = "sdk-example-key-pair";

            // If the default user on your system is not the same as
            // the Region where you want to create the key pair, you
            // need to supply the AWS Region as a parameter to the
            // client constructor.
            var client = new AmazonEC2Client();

            var request = new CreateKeyPairRequest
            {
                KeyName = keyName,
            };

            var response = await client.CreateKeyPairAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var kp = response.KeyPair;
                Console.WriteLine($"{kp.KeyName} with the ID: {kp.KeyPairId}.");
            }
            else
            {
                Console.WriteLine("Could not create key pair.");
            }
        }
    }
```
+  For API details, see [CreateKeyPair](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/CreateKeyPair) in *AWS SDK for \.NET API Reference*\. 

### Delete a VPC<a name="ec2_DeleteVPC_csharp_topic"></a>

The following code example shows how to delete an Amazon EC2 VPC\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to delete an existing Amazon Elastic Compute Cloud
    /// (Amazon EC2) VPC. The example was created using the AWS SDK for .NET
    /// version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DeleteVPC
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then calls the
        /// DeleteVpcAsync method to delete the VPC.
        /// </summary>
        public static async Task Main()
        {
            string vpcId = "vpc-0123456789abc";

            // If your Amazon EC2 VPC is not defined in the same AWS Region as
            // the default AWS user on your system, you need to supply the AWS
            // Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var request = new DeleteVpcRequest
            {
                VpcId = vpcId,
            };

            var response = await client.DeleteVpcAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully deleted VPC with ID: {vpcId}.");
            }
        }
    }
```
+  For API details, see [DeleteVpc](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/DeleteVpc) in *AWS SDK for \.NET API Reference*\. 

### Delete a security group<a name="ec2_DeleteSecurityGroup_csharp_topic"></a>

The following code example shows how to delete an Amazon EC2 security group\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to use Amazon Elastic Compute Cloud (Amazon EC2) and the
    /// AWS SDK for .NET to delete an existing security group. This example
    /// was created using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DeleteSecurityGroup
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then uses it to call
        /// the DeleteSecurityGroupAsync method to delete the security group.
        /// </summary>
        public static async Task Main()
        {
            string secGroupId = "sg-1234567890abcd";
            string groupName = "sample-security-group";

            // If your Amazon EC2 security grup is not defined in the same AWS
            // Region as the default user on your system, you need to supply
            // the AWS Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var request = new DeleteSecurityGroupRequest
            {
                GroupId = secGroupId,
                GroupName = groupName,
            };

            var response = await client.DeleteSecurityGroupAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully deleted {groupName}.");
            }
            else
            {
                Console.WriteLine($"Could not delete {groupName}.");
            }
        }
    }
```
+  For API details, see [DeleteSecurityGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/DeleteSecurityGroup) in *AWS SDK for \.NET API Reference*\. 

### Delete a security key pair<a name="ec2_DeleteKeyPair_csharp_topic"></a>

The following code example shows how to delete an Amazon EC2 security key pair\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to delete an existing Amazon Elastic Compute Cloud
    /// (Amazon EC2) key pair. The example uses the AWS SDK for .NET
    /// version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DeleteKeyPair
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then calls the
        /// DeleteKeyPairAsync method to delete the key pair.
        /// </summary>
        public static async Task Main()
        {
            string keyName = "sdk-example-key-pair";

            // If the key pair was not created in the same AWS Region as
            // the default user on your system, you need to supply
            // the AWS Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var request = new DeleteKeyPairRequest
            {
                KeyName = keyName,
            };

            var response = await client.DeleteKeyPairAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully deleted the key pair: {keyName}.");
            }
            else
            {
                Console.WriteLine("Could not delete the key pair.");
            }
        }
    }
```
+  For API details, see [DeleteKeyPair](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/DeleteKeyPair) in *AWS SDK for \.NET API Reference*\. 

### Describe instances<a name="ec2_DescribeInstances_csharp_topic"></a>

The following code example shows how to describe Amazon EC2 instances\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// This example shows how to list your Amazon Elastic Compute Cloud
    /// (Amazon EC2) instances. It was created using the AWS SDK for .NET
    /// version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DescribeInstances
    {
        /// <summary>
        /// The Main method creates the Amazon EC2 client object and then calls
        /// first GetInstanceDescriptions and then GetInstanceDescriptionsFiltered
        /// to display the list of Amazon EC2 Instances attached to the default
        /// account.
        /// </summary>
        public static async Task Main()
        {
            // If the Region of the EC2 instances you want to list is different
            // from the default user's Region, you need to specify the Region
            // when you create the client object.
            // For example: RegionEndpoint.USWest1.
            var eC2Client = new AmazonEC2Client();

            // List all EC2 instances.
            await GetInstanceDescriptions(eC2Client);

            string tagName = "IncludeInList";
            string tagValue = "Yes";
            await GetInstanceDescriptionsFiltered(eC2Client, tagName, tagValue);
        }

        /// <summary>
        /// This method uses a paginator to list all of the EC2 Instances
        /// attached to the default account.
        /// </summary>
        /// <param name="client">The Amazon EC2 client object used to call
        /// the DescribeInstances method.</param>
        public static async Task GetInstanceDescriptions(AmazonEC2Client client)
        {
            var request = new DescribeInstancesRequest();

            Console.WriteLine("Showing all instances:");
            var paginator = client.Paginators.DescribeInstances(request);

            await foreach (var response in paginator.Responses)
            {
                foreach (var reservation in response.Reservations)
                {
                    foreach (var instance in reservation.Instances)
                    {
                        Console.Write($"Instance ID: {instance.InstanceId}");
                        Console.WriteLine($"\tCurrent State: {instance.State.Name}");
                    }
                }
            }
        }

        /// <summary>
        /// This method lists the EC2 instances for this account which have set
        /// the tag named in the tagName parameter with the value in the tagValue
        /// parameter.
        /// </summary>
        /// <param name="client">The Amazon EC2 client object used to call
        /// the DescribeInstances method.</param>
        /// <param name="tagName">A string representing the name of the tag to
        /// filter on.</param>
        /// <param name="tagValue">A string representing the value of the tag
        /// to filter on.</param>
        public static async Task GetInstanceDescriptionsFiltered(AmazonEC2Client client,
           string tagName, string tagValue)
        {
            // This is the tag we want to use to filter
            // the results of our list of instances.
            var filters = new List<Filter>
            {
                new Filter
                {
                    Name = $"tag:{tagName}",
                    Values = new List<string>
                    {
                        tagValue,
                    },
                },
            };
            var request = new DescribeInstancesRequest
            {
                Filters = filters,
            };

            Console.WriteLine("\nShowing instances with tag: \"IncludeInList\" set to \"Yes\".");
            var paginator = client.Paginators.DescribeInstances(request);

            await foreach (var response in paginator.Responses)
            {
                foreach (var reservation in response.Reservations)
                {
                    foreach (var instance in reservation.Instances)
                    {
                        Console.Write($"Instance ID: {instance.InstanceId} ");
                        Console.WriteLine($"\tCurrent State: {instance.State.Name}");
                    }
                }
            }
        }
    }
```
+  For API details, see [DescribeInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/DescribeInstances) in *AWS SDK for \.NET API Reference*\. 

### Reboot an instance<a name="ec2_RebootInstances_csharp_topic"></a>

The following code example shows how to reboot an Amazon EC2 instance\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to reboot Amazon Elastic Compute Cloud (Amazon EC2) instances
    /// using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class RebootInstances
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and then calls
        /// RebootInstancesAsync to reboot the instance(s) in the ec2InstanceId
        /// list.
        /// </summary>
        public static async Task Main()
        {
            string ec2InstanceId = "i-0123456789abcdef0";

            // If your EC2 instances are not in the same AWS Region as
            // the default users on your system, you need to supply
            // the AWS Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var request = new RebootInstancesRequest
            {
                InstanceIds = new List<string> { ec2InstanceId },
            };

            var response = await client.RebootInstancesAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Instance(s) successfully rebooted.");
            }
            else
            {
                Console.WriteLine("Could not reboot one or more instances.");
            }
        }
    }
```
+  For API details, see [RebootInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/RebootInstances) in *AWS SDK for \.NET API Reference*\. 

### Start an instance<a name="ec2_StartInstances_csharp_topic"></a>

The following code example shows how to start an Amazon EC2 instance\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to start a list of Amazon Elastic Compute Cloud (Amazon EC2)
    /// instances using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class StartInstances
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and uses it to call the
        /// StartInstancesAsync method to start the listed Amazon EC2 instances.
        /// </summary>
        public static async Task Main()
        {
            string ec2InstanceId = "i-0123456789abcdef0";

            // If your EC2 instances are not in the same AWS Region as
            // the default users on your system, you need to supply
            // the AWS Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            var request = new StartInstancesRequest
            {
                InstanceIds = new List<string> { ec2InstanceId },
            };

            var response = await client.StartInstancesAsync(request);

            if (response.StartingInstances.Count > 0)
            {
                var instances = response.StartingInstances;
                instances.ForEach(i =>
                {
                    Console.WriteLine($"Successfully started the EC2 Instance with InstanceID: {i.InstanceId}.");
                });
            }
        }
    }
```
+  For API details, see [StartInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/StartInstances) in *AWS SDK for \.NET API Reference*\. 

### Stop an instance<a name="ec2_StopInstances_csharp_topic"></a>

The following code example shows how to stop an Amazon EC2 instance\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/EC2#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.EC2;
    using Amazon.EC2.Model;

    /// <summary>
    /// Shows how to stop a list of Amazon Elastic Compute Cloud (Amazon EC2)
    /// instances using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class StopInstances
    {
        /// <summary>
        /// Initializes the Amazon EC2 client object and uses it to call the
        /// StartInstancesAsync method to stop the listed Amazon EC2 instances.
        /// </summary>
        public static async Task Main()
        {
            string ec2InstanceId = "i-0123456789abcdef0";

            // If your EC2 instances are not in the same AWS Region as
            // the default user on your system, you need to supply
            // the AWS Region as a parameter to the client constructor.
            var client = new AmazonEC2Client();

            // In addition to the list of instance Ids, the
            // request can also include the following properties:
            //     Force      When true forces the instances to
            //                stop but you have to check the integrity
            //                of the file system. Not recommended on
            //                Windows instances.
            //     Hibernate  When true, hibernates the instance if the
            //                instance was enabled for hibernation when
            //                it was launched.
            var request = new StopInstancesRequest
            {
                InstanceIds = new List<string> { ec2InstanceId },
            };

            var response = await client.StopInstancesAsync(request);

            if (response.StoppingInstances.Count > 0)
            {
                var instances = response.StoppingInstances;
                instances.ForEach(i =>
                {
                    Console.WriteLine($"Successfully stopped the EC2 Instance " +
                                      $"with InstanceID: {i.InstanceId}.");
                });
            }
        }
    }
```
+  For API details, see [StopInstances](https://docs.aws.amazon.com/goto/DotNetSDKV3/ec2-2016-11-15/StopInstances) in *AWS SDK for \.NET API Reference*\. 