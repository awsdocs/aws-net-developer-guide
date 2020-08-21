--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Launching an Amazon EC2 Instance<a name="run-instance"></a>

Use the following procedure to launch one or more identically configured Amazon EC2 instances from the same Amazon Machine Image \(AMI\)\. After you create your EC2 instances, you can check their status\. When your EC2 instances are running, you can connect to them\.

For information on creating an Amazon EC2 client, see [Creating an Amazon EC2 Client](init-ec2-client.md)\.

## Launch an EC2 Instance in EC2\-Classic or in a VPC<a name="launch-instance"></a>

You can launch an instance in either EC2\-Classic or in a VPC\. For more information about EC2\-Classic and EC2\-VPC, see [Supported Platforms](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-supported-platforms.html) in the Amazon EC2 User Guide for Windows Instances\.

To get a list of your security groups and their `GroupId` properties, see [Enumerate Your Security Groups](security-groups.md#enumerate-security-groups)\.

**To launch an EC2 instance in EC2\-Classic**

1. Create and initialize a [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object\. Be sure the AMI, key pair, and security group you specify exist in the region you specified when you created the client object\.

   ```
   string amiID = "ami-e189c8d1";
   string keyPairName = "my-sample-key";
   
   List<string> groups = new List<string>() { mySG.GroupId };
   var launchRequest = new RunInstancesRequest()
   {
     ImageId = amiID,
     InstanceType = InstanceType.T1Micro,
     MinCount = 1,
     MaxCount = 1,
     KeyName = keyPairName,
     SecurityGroupIds = groups
   };
   ```  
** `ImageId` **  
The ID of the AMI\. For a list of public AMIs, see [Amazon Machine Images](https://aws.amazon.com/marketplace/search/results/&amp;searchTerms=AMISAWS?browse=1)\.  
** `InstanceType` **  
An instance type that is compatible with the specified AMI\. For more information, see [Instance Types](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/instance-types.html.html) in the Amazon EC2 User Guide for Windows Instances\.  
** `MinCount` **  
The minimum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches no instances\.  
** `MaxCount` **  
The maximum number of EC2 instances to launch\. If this is more instances than Amazon EC2 can launch in the target Availability Zone, Amazon EC2 launches the largest possible number of instances above `MinCount`\. You can launch between 1 and the maximum number of instances you’re allowed for the instance type\. For more information, see [How many instances can I run in Amazon EC2](https://aws.amazon.com/ec2/faqs/#How_many_instances_can_I_run_in_Amazon_EC2) in the Amazon EC2 General FAQ\.  
** `KeyName` **  
The name of the EC2 key pair\. If you launch an instance without specifying a key pair, you can’t connect to it\. For more information, see [Working with Amazon EC2 Key Pairs](key-pairs.md#create-key-pair)\.  
** `SecurityGroupIds` **  
The identifiers of one or more security groups\. For more information, see [Creating a Security Group in Amazon EC2](security-groups.md#create-security-group)\.

1. \(Optional\) To launch the instance with an [IAM role](net-dg-hosm.md), specify an IAM instance profile in the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object\.

   An IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

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

   For example, the following snippet instantiates and configures an [IamInstanceProfileSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIamInstanceProfileSpecification.html) object for an IAM role named `winapp-instance-role-1`\.

   ```
   var instanceProfile = new IamInstanceProfile();
   instanceProfile.Id  = "winapp-instance-role-1";
   ```

   To specify this instance profile in the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object, add the following line\.

   ```
   launchRequest.IamInstanceProfile = instanceProfile;
   ```

1. Launch the instance by passing the request object to the [RunInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2RunInstancesRunInstancesRequest.html) method\. Save the ID of the instance because you need it to manage the instance\.

   Use the returned [RunInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesResponse.html) object to get the instance IDs for the new instances\. The `Reservation.Instances` property contains a list of [Instance](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstance.html) objects, one for each EC2 instance you successfully launched\. You can retrieve the ID for each instance from the `InstanceId` property of the [Instance](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstance.html) object\.

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

1. Create and initialize an elastic network interface in a subnet of the VPC\.

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
The ID of the subnet where the instance will be launched\.  
** `Groups` **  
One or more security groups\. For more information, see [Creating a Security Group in Amazon EC2](security-groups.md#create-security-group)\.  
** `AssociatePublicIpAddress` **  
Indicates whether to auto\-assign a public IP address to an instance in a VPC\.

1. Create and initialize a [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object\. Be sure the AMI, key pair, and security group you specify exist in the region you specified when you created the client object\.

   ```
   string amiID = "ami-e189c8d1";
   string keyPairName = "my-sample-key";
   
   var launchRequest = new RunInstancesRequest()
   {
     ImageId = amiID,
     InstanceType = InstanceType.T1Micro,
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
The name of the EC2 key pair\. If you launch an instance without specifying a key pair, you can’t connect to it\. For more information, see [Working with Amazon EC2 Key Pairs](key-pairs.md#create-key-pair)\.  
** `NetworkInterfaces` **  
One or more network interfaces\.

1. \(Optional\) To launch the instance with an [IAM role](net-dg-hosm.md), specify an IAM instance profile in the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object\.

   An IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

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

   For example, the following snippet instantiates and configures an [IamInstanceProfileSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIamInstanceProfileSpecification.html) object for an IAM role named `winapp-instance-role-1`\.

   ```
   var instanceProfile = new IamInstanceProfileSpecification();
   instanceProfile.Name  = "winapp-instance-role-1";
   ```

   To specify this instance profile in the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object, add the following line\.

   ```
   launchRequest.IamInstanceProfile = instanceProfile;
   ```

1. Launch the instances by passing the request object to the [RunInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2RunInstancesRunInstancesRequest.html) method\. Save the IDs of the instances because you need them to manage the instances\.

   Use the returned [RunInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesResponse.html) object to get a list of instance IDs for the new instances\. The `Reservation.Instances` property contains a list of [Instance](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstance.html) objects, one for each EC2 instance you successfully launched\. You can retrieve the ID for each instance from the `InstanceId` property of the [Instance](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstance.html) object\.

   ```
   RunInstancesResponse launchResponse = ec2Client.RunInstances(launchRequest);
   
   List<String> instanceIds = new List<string>();
   foreach (Instance instance in launchResponse.Reservation.Instances)
   {
     Console.WriteLine(instance.InstanceId);
     instanceIds.Add(instance.InstanceId);
   }
   ```

## Check the State of Your Instance<a name="check-instance-state"></a>

Use the following procedure to get the current state of your instance\. Initially, your instance is in the `pending` state\. You can connect to your instance after it enters the `running` state\.

1. Create and configure a [DescribeInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeInstancesRequest.html) object and assign your instance’s instance ID to the `InstanceIds` property\. You can also use the `Filter` property to limit the request to certain instances, such as instances with a particular user\-specified tag\.

   ```
   var instanceRequest = new DescribeInstancesRequest();
   instanceRequest.InstanceIds = new List<string>();
   instanceRequest.InstanceIds.Add(instanceId);
   ```

1. Call the [DescribeInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeInstancesDescribeInstancesRequest.html) method, and pass it the request object from step 1\. The method returns a [DescribeInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeInstancesResponse.html) object that contains information about the instance\.

   ```
   var response = ec2Client.DescribeInstances(instanceRequest);
   ```

1. The `DescribeInstancesResponse.Reservations` property contains a list of reservations\. In this case, there is only one reservation\. Each reservation contains a list of `Instance` objects\. Again, in this case, there is only one instance\. You can get the instance’s status from the `State` property\.

   ```
   Console.WriteLine(response.Reservations[0].Instances[0].State.Name);
   ```

## Connect to Your Running Instance<a name="connect-to-instance"></a>

After an instance is running, you can remotely connect to it by using the appropriate remote client\.

For Linux instances, use an SSH client\. You must ensure that the instance’s SSH port \(22\) is open to traffic\. You will need the instance’s public IP address or public DNS name and the private portion of the key pair used to launch the instance\. For more information, see [Connecting to Your Linux Instance](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AccessingInstances.html) in the Amazon EC2 User Guide for Linux Instances\.

For Windows instances, use an RDP client\. You must ensure the instance’s RDP port \(3389\) is open to traffic\. You will need the instance’s public IP address or public DNS name and the administrator password\. The administrator password is obtained with the [GetPasswordData](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2GetPasswordDataGetPasswordDataRequest.html) and [GetPasswordDataResult\.GetDecryptedPassword](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MGetPasswordDataResponseGetDecryptedPasswordString.html) methods, which require the private portion of the key pair used to launch the instance\. For more information, see [Connecting to Your Windows Instance Using RDP](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html) in the Amazon EC2 User Guide for Windows Instances\. The following example demonstrates how to get the password for a Windows instance\.

```
public static string GetWindowsPassword(
  AmazonEC2Client ec2Client,
  string instanceId,
  FileInfo privateKeyFile)
{
  string password = "";

  var request = new GetPasswordDataRequest();
  request.InstanceId = instanceId;

  var response = ec2Client.GetPasswordData(request);
  if (null != response.PasswordData)
  {
    using (StreamReader sr = new StreamReader(privateKeyFile.FullName))
    {
      string privateKeyData = sr.ReadToEnd();
      password = response.GetDecryptedPassword(privateKeyData);
    }
  }
  else
  {
    Console.WriteLine("The password is not available. The password for " +
      "instance {0} is either not ready, or it is not a Windows instance.",
      instanceId);
  }

  return password;
}
```

When you no longer need your EC2 instance, see [Terminating an Amazon EC2 Instance](terminate-instance.md)\.