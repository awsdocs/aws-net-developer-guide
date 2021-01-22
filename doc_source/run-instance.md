--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.

--------

# Launch an EC2 Instance Using the SDK<a name="run-instance"></a>

Use the following procedure to launch one or more identically configured EC2 instances from the same Amazon Machine Image \(AMI\)\. After you create your EC2 instances, you can check their status\. After your EC2 instances are running, you can connect to them\.

**Topics**
+ [Launching an EC2 Instance](#launch-instance)
+ [Checking the State of Your Instance](#check-instance-state)
+ [Connecting to Your Running Instance](#connect-to-instance)

## Launching an EC2 Instance<a name="launch-instance"></a>

You launch an instance in either EC2\-Classic or in a VPC\. For more information about EC2\-Classic and EC2\-VPC, see [Supported Platforms](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-supported-platforms.html) in the Amazon EC2 User Guide for Windows Instances\.

 **To launch an EC2 instance in EC2\-Classic** 

1. Create and initialize a [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2RunInstancesRequestNET45.html) object\. Make sure that the AMI, key pair, and security group that you specify exist in the region that you specified when you created the client object\.

   ```
   string amiID = "ami-e189c8d1";
   string keyPairName = "my-sample-key";
   
   List<string> groups = new List<string>() { mySG.GroupId };
   var launchRequest = new RunInstancesRequest()
   {
       ImageId = amiID,
       InstanceType = "t1.micro",
       MinCount = 1,
       MaxCount = 1,
       KeyName = keyPairName,
       SecurityGroupIds = groups
   };
   ```  
** `ImageId` **  
The ID of the AMI\. For a list of public AMIs provided by Amazon, see [Amazon Machine Images](https://aws.amazon.com/marketplace/search/results/&amp;searchTerms=AMISAWS?browse=1)\.  
** `InstanceType` **  
An instance type that is compatible with the specified AMI\. For more information, see [Instance Types](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/instance-types.html) in the Amazon EC2 User Guide for Windows Instances\.  
** `MinCount` **  
The minimum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches no instances\.  
** `MaxCount` **  
The maximum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches the largest possible number of instances above `MinCount`\. You can launch between 1 and the maximum number of instances you’re allowed for the instance type\. For more information, see [How many instances can I run in Amazon EC2](https://aws.amazon.com/ec2/faqs/#How_many_instances_can_I_run_in_Amazon_EC2) in the Amazon EC2 General FAQ\.  
** `KeyName` **  
The name of the EC2 key pair\. If you launch an instance without specifying a key pair, you can’t connect to it\. For more information, see [Create a Key Pair Using the the SDK](how-to-ec2.md#create-key-pair)\.  
** `SecurityGroupIds` **  
The identifiers of one or more security groups\. For more information, see [Create a Security Group Using the the SDK](how-to-ec2.md#create-security-group)\.

1. \(Optional\) To launch the instance with an [IAM role](net-dg-hosm.md#net-dg-roles), specify an IAM instance profile in the `RunInstancesRequest` object\.

   Note that an IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

   ```
   {
     "Version": "2012-10-17",
     "Statement": [{
       "Effect": "Allow",
       "Action": [
         "iam:PassRole",
         "iam:ListInstanceProfiles",
         "ec2:*"
       ],
       "Resource": "*"
     }]
   }
   ```

   For example, the following snippet instantiates and configures an [IamInstanceProfileSpecification](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2IamInstanceProfileSpecificationNET45.html) object for an IAM role named `winapp-instance-role-1`\.

   ```
   var instanceProfile = new IamInstanceProfile();
   instanceProfile.Id  = "winapp-instance-role-1";
   instanceProfile.Arn = "arn:aws:iam::|ExampleAWSAccountNo2H|:instance-profile/winapp-instance-role-1";
   ```

   To specify this instance profile in the `RunInstancesRequest` object, add the following line\.

   ```
   launchRequest.IamInstanceProfile = instanceProfile;
   ```

1. Launch the instance by passing the request object to the [RunInstances](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2RunInstancesRunInstancesRequestNET45.html) method\. Save the ID of the instances, as you need it to manage the instance\.

   Use the returned [RunInstancesResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2RunInstancesResponseNET45.html) object to get the instance IDs for the new instances\. The `Reservation.Instances` property contains a list of [Instance](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2InstanceNET45.html) objects, one for each EC2 instance that you successfully launched\. You can retrieve the ID for each instance from the `Instance` object’s `InstanceId` property\.

   ```
   var launchResponse = ec2Client.RunInstances(launchRequest);
   var instances = launchResponse.Reservation.Instances;
   var instanceIds = new List<string>();
   foreach (Instance item in instances)
   {
       instanceIds.Add(item.InstanceId);
       Console.WriteLine();
       Console.WriteLine("New instance: " + item.InstanceId);
       Console.WriteLine("Instance state: " + item.State.Name);
   }
   ```

 **To launch an EC2 instance in a VPC** 

1. Create and initialize a network interface\.

   ```
   string subnetID = "subnet-cb663da2";
   
   List<string> groups = new List<string>() { mySG.GroupId };
   var eni = new InstanceNetworkInterfaceSpecification()
   {
       DeviceIndex = 0,
       SubnetId = subnetID,
       Groups = groups,
       AssociatePublicIpAddress = true
   };
   List<InstanceNetworkInterfaceSpecification> enis = new List<InstanceNetworkInterfaceSpecification>() {eni};
   ```  
** `DeviceIndex` **  
The index of the device on the instance for the network interface attachment\.  
** `SubnetId` **  
The ID of the subnet to launch the instance into\.  
** `GroupIds` **  
One or more security groups\. For more information, see [Create a Security Group Using the the SDK](how-to-ec2.md#create-security-group)\.  
** `AssociatePublicIpAddress` **  
Indicates whether to auto\-assign a public IP address to an instance in a VPC\.

1. Create and initialize a [RunInstancesRequest](TEC2RunInstancesRequestNET45.html) object\. Make sure that the AMI, key pair, and security group that you specify exist in the region that you specified when you created the client object\.

   ```
   string amiID = "ami-e189c8d1";
   string keyPairName = "my-sample-key";
   
   var launchRequest = new RunInstancesRequest()
   {
       ImageId = amiID,
       InstanceType = "t1.micro",
       MinCount = 1,
       MaxCount = 1,
       KeyName = keyPairName,
       NetworkInterfaces = enis
   };
   ```  
** `ImageId` **  
The ID of the AMI\. For a list of public AMIs provided by Amazon, see [Amazon Machine Images](https://aws.amazon.com/marketplace/search/results/&amp;searchTerms=AMISAWS?browse=1)\.  
** `InstanceType` **  
An instance type that is compatible with the specified AMI\. For more information, see [Instance Types](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/instance-types.html) in the Amazon EC2 User Guide for Windows Instances\.  
** `MinCount` **  
The minimum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches no instances\.  
** `MaxCount` **  
The maximum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches the largest possible number of instances above `MinCount`\. You can launch between 1 and the maximum number of instances you’re allowed for the instance type\. For more information, see [How many instances can I run in Amazon EC2](https://aws.amazon.com/ec2/faqs/#How_many_instances_can_I_run_in_Amazon_EC2) in the Amazon EC2 General FAQ\.  
** `KeyName` **  
The name of the EC2 key pair\. If you launch an instance without specifying a key pair, you can’t connect to it\. For more information, see [Create a Key Pair Using the the SDK](how-to-ec2.md#create-key-pair)\.  
** `NetworkInterfaces` **  
One or more network interfaces\.

1. \(Optional\) To launch the instance with an [IAM role](net-dg-hosm.md#net-dg-roles), specify an [IAM instance profile](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2RunInstancesRequestIamInstanceProfileNET45.html) in the `RunInstancesRequest` object\.

   Note that an IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

   ```
   {
     "Version": "2012-10-17",
     "Statement": [{
       "Effect": "Allow",
       "Action": [
         "iam:PassRole",
         "iam:ListInstanceProfiles",
         "ec2:*"
       ],
       "Resource": "*"
     }]
   }
   ```

   For example, the following snippet instantiates and configures an [IamInstanceProfileSpecification](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2IamInstanceProfileSpecificationNET45.html) object for an IAM role named `winapp-instance-role-1`\.

   ```
   var instanceProfile = new IamInstanceProfile();
   instanceProfile.Id  = "winapp-instance-role-1";
   instanceProfile.Arn = "arn:aws:iam::|ExampleAWSAccountNo2H|:instance-profile/winapp-instance-role-1";
   ```

   To specify this instance profile in the `RunInstancesRequest` object, add the following line\.

   ```
   InstanceProfile = instanceProfile
   ```

1. Launch the instances by passing the request object to the [RunInstances](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2RunInstancesRunInstancesRequestNET45.html) method\. Save the IDs of the instances, as you need them to manage the instances\.

   Use the returned [RunInstancesResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2RunInstancesResponseNET45.html) object to get a list of instance IDs for the new instances\. The `Reservation.Instances` property contains a list of [Instance](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2InstanceNET45.html) objects, one for each EC2 instance that you successfully launched\. You can retrieve the ID for each instance from the `Instance` object’s `InstanceId` property\.

   ```
   RunInstancesResponse launchResponse = ec2Client.RunInstances(launchRequest);
   
   List<String> instanceIds = new List<string>();
   foreach (Instance instance in launchResponse.Reservation.Instances)
   {
       Console.WriteLine(instance.InstanceId);
       instanceIds.Add(instance.InstanceId);
   }
   ```

## Checking the State of Your Instance<a name="check-instance-state"></a>

Use the following procedure to get the current state of your instance\. Initially, your instance is in the `pending` state\. You can connect to your instance after it enters the `running` state\.

 **To check the state of your instance** 

1. Create and configure a [DescribeInstancesRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2DescribeInstancesRequestNET45.html) object and assign your instance’s instance ID to the `InstanceIds` property\. You can also use the `Filter` property to limit the request to certain instances, such as instances with a particular user\-specified tag\.

   ```
   var instanceRequest = new DescribeInstancesRequest();
   instanceRequest.InstanceIds = new List<string>();
   instanceRequest.InstanceIds.Add(instanceId);
   ```

1. Call the EC2 client’s [DescribeInstances](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeInstancesDescribeInstancesRequestNET45.html) method, and pass it the request object from step 1\. The method returns a [DescribeInstancesResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2DescribeInstancesResponseNET45.html) object that contains information about the instance\.

   ```
   var response = ec2Client.DescribeInstances(instanceRequest);
   ```

1. The `DescribeInstancesResponse.Reservations` property contains a list of reservations\. In this case, there is only one reservation\. Each reservation contains a list of `Instance` objects\. Again, in this case, there is only one instance\. You can get the instance’s status from the `State` property\.

   ```
   Console.WriteLine(response.Reservations[0].Instances[0].State.Name);
   ```

## Connecting to Your Running Instance<a name="connect-to-instance"></a>

After an instance is running, you can remotely connect to it using an RDP client on your computer\. Before connecting to your instance, you must ensure that the instance’s RDP port is open to traffic\. To connect, you need the instance ID and the private key for instance’s key pair\. For more information, see [Connecting to Your Windows Instance Using RDP](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html) in the Amazon EC2 User Guide for Windows Instances\.

When you have finished with your EC2 instance, see [Terminate an EC2 Instance Using the the SDK](terminate-instance.md)\.
