--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Launching an Amazon EC2 instance<a name="run-instance"></a>

This example shows you how to use the AWS SDK for \.NET to launch one or more identically configured Amazon EC2 instances from the same Amazon Machine Image \(AMI\)\. Using [several inputs](#run-instance-gather) that you supply, the application launches an EC2 instance and then monitors the instance until it's out of the "Pending" state\.

When your EC2 instance is running, you can connect to it remotely, as described in [\(optional\) Connect to the instance](#connect-to-instance)\.

You can launch an EC2 instance in a VPC or in EC2\-Classic \(if your AWS account supports this\)\. For more information about EC2 in a VPC versus EC2\-Classic, see the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-classic-platform.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-classic-platform.html)\.

The following sections provide snippets and other information for this example\. The [complete code for the example](#run-instance-complete-code) is shown after the snippets, and can be built and run as is\.

**Topics**
+ [Gather what you need](#run-instance-gather)
+ [Launch an instance](#run-instance-launch)
+ [Monitor the instance](#run-instance-monitor)
+ [Complete code](#run-instance-complete-code)
+ [\(optional\) Connect to the instance](#connect-to-instance)
+ [Clean up](#run-instance-cleanup)

## Gather what you need<a name="run-instance-gather"></a>

To launch an EC2 instance, you'll need several things\.
+ A [VPC](https://docs.aws.amazon.com/vpc/latest/userguide/) where the instance will be launched\. If it'll be a Windows instance and you'll be connecting to it through RDP, the VPC will most likely need to have an internet gateway attached to it, as well as an entry for the internet gateway in the route table\. For more information, see [Internet gateways](https://docs.aws.amazon.com/vpc/latest/userguide/VPC_Internet_Gateway.html) in the *Amazon VPC User Guide*\.
+ The ID of an existing subnet in the VPC where the instance will be launched\. An easy way to find or create this is to sign in to the [Amazon VPC console](https://console.aws.amazon.com/vpc/home#subnets), but you can also obtain it programmatically by using the [CreateSubnetAsync](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateSubnetAsyncCreateSubnetRequestCancellationToken.html) and [DescribeSubnetsAsync](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSubnetsAsyncDescribeSubnetsRequestCancellationToken.html) methods\.
**Note**  
If your AWS account supports EC2\-Classic and that's the type of instance you want to launch, this parameter isn't required\. However, if your account doesn't support EC2\-Classic and you don't supply this parameter, the new instance is launched in the default VPC for your account\.
+ The ID of an existing security group that belongs to the VPC where the instance will be launched\. For more information, see [Working with security groups in Amazon EC2](security-groups.md)\.
+ If you want to connect to the new instance, the security group mentioned earlier must have an appropriate inbound rule that allows SSH traffic on port 22 \(Linux instance\) or RDP traffic on port 3389 \(Windows instance\)\. For information about how to do this see [Updating security groups](authorize-ingress.md), including the **Additional considerations** near the end of that topic\.
+ The Amazon Machine Image \(AMI\) that will be used to create the instance\. See the information about AMIs in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AMIs.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/AMIs.html)\. For example, read about shared AMIs in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/sharing-amis.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/sharing-amis.html)\.
+ The name of an existing EC2 key pair, which is used to connect to the new instance\. For more information, see [Working with Amazon EC2 key pairs](key-pairs.md)\.
+ The name of the PEM file that contains the private key of the EC2 key pair mentioned earlier\. The PEM file is used when you [connect remotely](#connect-to-instance) to the instance\.

## Launch an instance<a name="run-instance-launch"></a>

The following snippet launches an EC2 instance\.

The example [near the end of this topic](#run-instance-complete-code) shows this snippet in use\.

```
    //
    // Method to launch the instances
    // Returns a list with the launched instance IDs
    private static async Task<List<string>> LaunchInstances(
      IAmazonEC2 ec2Client, RunInstancesRequest requestLaunch)
    {
      var instanceIds = new List<string>();
      RunInstancesResponse responseLaunch =
        await ec2Client.RunInstancesAsync(requestLaunch);

      Console.WriteLine("\nNew instances have been created.");
      foreach (Instance item in responseLaunch.Reservation.Instances)
      {
        instanceIds.Add(item.InstanceId);
        Console.WriteLine($"  New instance: {item.InstanceId}");
      }

      return instanceIds;
    }
```

## Monitor the instance<a name="run-instance-monitor"></a>

The following snippet monitors the instance until it's out of the "Pending" state\.

The example [near the end of this topic](#run-instance-complete-code) shows this snippet in use\.

See the [InstanceState](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstanceState.html) class for the valid values of the `Instance.State.Code` property\.

```
    //
    // Method to wait until the instances are running (or at least not pending)
    private static async Task CheckState(IAmazonEC2 ec2Client, List<string> instanceIds)
    {
      Console.WriteLine(
        "\nWaiting for the instances to start." +
        "\nPress any key to stop waiting. (Response might be slightly delayed.)");

      int numberRunning;
      DescribeInstancesResponse responseDescribe;
      var requestDescribe = new DescribeInstancesRequest{
        InstanceIds = instanceIds};

      // Check every couple of seconds
      int wait = 2000;
      while(true)
      {
        // Get and check the status for each of the instances to see if it's past pending.
        // Once all instances are past pending, break out.
        // (For this example, we are assuming that there is only one reservation.)
        Console.Write(".");
        numberRunning = 0;
        responseDescribe = await ec2Client.DescribeInstancesAsync(requestDescribe);
        foreach(Instance i in responseDescribe.Reservations[0].Instances)
        {
          // Check the lower byte of State.Code property
          // Code == 0 is the pending state
          if((i.State.Code & 255) > 0) numberRunning++;
        }
        if(numberRunning == responseDescribe.Reservations[0].Instances.Count)
          break;

        // Wait a bit and try again (unless the user wants to stop waiting)
        Thread.Sleep(wait);
        if(Console.KeyAvailable)
          break;
      }

      Console.WriteLine("\nNo more instances are pending.");
      foreach(Instance i in responseDescribe.Reservations[0].Instances)
      {
        Console.WriteLine($"For {i.InstanceId}:");
        Console.WriteLine($"  VPC ID: {i.VpcId}");
        Console.WriteLine($"  Instance state: {i.State.Name}");
        Console.WriteLine($"  Public IP address: {i.PublicIpAddress}");
        Console.WriteLine($"  Public DNS name: {i.PublicDnsName}");
        Console.WriteLine($"  Key pair name: {i.KeyName}");
      }
    }
```

## Complete code<a name="run-instance-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c19c19b9c27b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)

  Class [InstanceType](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstanceType.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [DescribeInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeInstancesRequest.html)

  Class [DescribeInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeInstancesResponse.html)

  Class [Instance](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstance.html)

  Class [InstanceNetworkInterfaceSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TInstanceNetworkInterfaceSpecification.html)

  Class [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html)

  Class [RunInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesResponse.html)

### The code<a name="w4aac19c19c19b9c27b7b1"></a>

```
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2LaunchInstance
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to launch an EC2 instance
  class Program
  {
    static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if(parsedArgs.Count == 0)
      {
        PrintHelp();
        return;
      }

      // Get the application parameters from the parsed arguments
      string groupID =
        CommandLine.GetParameter(parsedArgs, null, "-g", "--group-id");
      string ami =
        CommandLine.GetParameter(parsedArgs, null, "-a", "--ami-id");
      string keyPairName =
        CommandLine.GetParameter(parsedArgs, null, "-k", "--keypair-name");
      string subnetID =
        CommandLine.GetParameter(parsedArgs, null, "-s", "--subnet-id");
      if(   (string.IsNullOrEmpty(groupID) || !groupID.StartsWith("sg-"))
         || (string.IsNullOrEmpty(ami) || !ami.StartsWith("ami-"))
         || (string.IsNullOrEmpty(keyPairName))
         || (!string.IsNullOrEmpty(subnetID) && !subnetID.StartsWith("subnet-")))
        CommandLine.ErrorExit(
          "\nOne or more of the required arguments is missing or incorrect." +
          "\nRun the command with no arguments to see help.");

      // Create an EC2 client
      var ec2Client = new AmazonEC2Client();

      // Create an object with the necessary properties
      RunInstancesRequest request = GetRequestData(groupID, ami, keyPairName, subnetID);

      // Launch the instances and wait for them to start running
      var instanceIds = await LaunchInstances(ec2Client, request);
      await CheckState(ec2Client, instanceIds);
    }


    //
    // Method to put together the properties needed to launch the instance.
    private static RunInstancesRequest GetRequestData(
      string groupID, string ami, string keyPairName, string subnetID)
    {
      // Common properties
      var groupIDs = new List<string>() { groupID };
      var request = new RunInstancesRequest()
      {
        // The first three of these would be additional command-line arguments or similar.
        InstanceType = InstanceType.T1Micro,
        MinCount = 1,
        MaxCount = 1,
        ImageId = ami,
        KeyName = keyPairName
      };

      // Properties specifically for EC2 in a VPC.
      if(!string.IsNullOrEmpty(subnetID))
      {
        request.NetworkInterfaces =
          new List<InstanceNetworkInterfaceSpecification>() {
            new InstanceNetworkInterfaceSpecification() {
              DeviceIndex = 0,
              SubnetId = subnetID,
              Groups = groupIDs,
              AssociatePublicIpAddress = true
            }
          };
      }

      // Properties specifically for EC2-Classic
      else
      {
        request.SecurityGroupIds = groupIDs;
      }
      return request;
    }


    //
    // Method to launch the instances
    // Returns a list with the launched instance IDs
    private static async Task<List<string>> LaunchInstances(
      IAmazonEC2 ec2Client, RunInstancesRequest requestLaunch)
    {
      var instanceIds = new List<string>();
      RunInstancesResponse responseLaunch =
        await ec2Client.RunInstancesAsync(requestLaunch);

      Console.WriteLine("\nNew instances have been created.");
      foreach (Instance item in responseLaunch.Reservation.Instances)
      {
        instanceIds.Add(item.InstanceId);
        Console.WriteLine($"  New instance: {item.InstanceId}");
      }

      return instanceIds;
    }


    //
    // Method to wait until the instances are running (or at least not pending)
    private static async Task CheckState(IAmazonEC2 ec2Client, List<string> instanceIds)
    {
      Console.WriteLine(
        "\nWaiting for the instances to start." +
        "\nPress any key to stop waiting. (Response might be slightly delayed.)");

      int numberRunning;
      DescribeInstancesResponse responseDescribe;
      var requestDescribe = new DescribeInstancesRequest{
        InstanceIds = instanceIds};

      // Check every couple of seconds
      int wait = 2000;
      while(true)
      {
        // Get and check the status for each of the instances to see if it's past pending.
        // Once all instances are past pending, break out.
        // (For this example, we are assuming that there is only one reservation.)
        Console.Write(".");
        numberRunning = 0;
        responseDescribe = await ec2Client.DescribeInstancesAsync(requestDescribe);
        foreach(Instance i in responseDescribe.Reservations[0].Instances)
        {
          // Check the lower byte of State.Code property
          // Code == 0 is the pending state
          if((i.State.Code & 255) > 0) numberRunning++;
        }
        if(numberRunning == responseDescribe.Reservations[0].Instances.Count)
          break;

        // Wait a bit and try again (unless the user wants to stop waiting)
        Thread.Sleep(wait);
        if(Console.KeyAvailable)
          break;
      }

      Console.WriteLine("\nNo more instances are pending.");
      foreach(Instance i in responseDescribe.Reservations[0].Instances)
      {
        Console.WriteLine($"For {i.InstanceId}:");
        Console.WriteLine($"  VPC ID: {i.VpcId}");
        Console.WriteLine($"  Instance state: {i.State.Name}");
        Console.WriteLine($"  Public IP address: {i.PublicIpAddress}");
        Console.WriteLine($"  Public DNS name: {i.PublicDnsName}");
        Console.WriteLine($"  Key pair name: {i.KeyName}");
      }
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: EC2LaunchInstance -g <group-id> -a <ami-id> -k <keypair-name> [-s <subnet-id>]" +
        "\n  -g, --group-id: The ID of the security group." +
        "\n  -a, --ami-id: The ID of an Amazon Machine Image." +
        "\n  -k, --keypair-name - The name of a key pair." +
        "\n  -s, --subnet-id: The ID of a subnet. Required only for EC2 in a VPC.");
    }
  }


  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal.
  // (This is the same for all examples. When you have seen it once, you can ignore it.)
  static class CommandLine
  {
    // Method to parse a command line of the form: "--param value" or "-p value".
    // If "param" is found without a matching "value", Dictionary.Value is an empty string.
    // If "value" is found without a matching "param", Dictionary.Key is "--NoKeyN"
    //  where "N" represents sequential numbers.
    public static Dictionary<string,string> Parse(string[] args)
    {
      var parsedArgs = new Dictionary<string,string>();
      int i = 0, n = 0;
      while(i < args.Length)
      {
        // If the first argument in this iteration starts with a dash it's an option.
        if(args[i].StartsWith("-"))
        {
          var key = args[i++];
          var value = string.Empty;

          // Is there a value that goes with this option?
          if((i < args.Length) && (!args[i].StartsWith("-"))) value = args[i++];
          parsedArgs.Add(key, value);
        }

        // If the first argument in this iteration doesn't start with a dash, it's a value
        else
        {
          parsedArgs.Add("--NoKey" + n.ToString(), args[i++]);
          n++;
        }
      }

      return parsedArgs;
    }

    //
    // Method to get a parameter from the parsed command-line arguments
    public static string GetParameter(
      Dictionary<string,string> parsedArgs, string def, params string[] keys)
    {
      string retval = null;
      foreach(var key in keys)
        if(parsedArgs.TryGetValue(key, out retval)) break;
      return retval ?? def;
    }

    //
    // Exit with an error.
    public static void ErrorExit(string msg, int code=1)
    {
      Console.WriteLine("\nError");
      Console.WriteLine(msg);
      Environment.Exit(code);
    }
  }

}
```

**Additional considerations**
+ When checking the state of an EC2 instance, you can add a filter to the `Filter` property of the [DescribeInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeInstancesRequest.html) object\. Using this technique, you can limit the request to certain instances; for example, instances with a particular user\-specified tag\.
+ For brevity, some properties were given typical values\. Any or all of these properties can instead be determined programmatically or by user input\.
+ The values you can use for the `MinCount` and `MaxCount` properties of the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object are determined by the target Availability Zone and the maximum number of instances you’re allowed for the instance type\. For more information, see [How many instances can I run in Amazon EC2](https://aws.amazon.com/ec2/faqs/#How_many_instances_can_I_run_in_Amazon_EC2) in the Amazon EC2 General FAQ\.
+ If you want to use a different instance type than this example, there are several instance types to choose from, which you can see in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/instance-types.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/instance-types.html)\.
+ You can also attach an [IAM role](net-dg-hosm.md) to an instance when you launch it\. To do so, create an [IamInstanceProfileSpecification](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIamInstanceProfileSpecification.html) object whose `Name` property is set to the name of an IAM role\. Then add that object to the `IamInstanceProfile` property of the [RunInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TRunInstancesRequest.html) object\.
**Note**  
To launch an EC2 instance that has an IAM role attached, an IAM user's configuration must include certain permissions\. For more information about the required permissions, see the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/iam-roles-for-amazon-ec2.html#permission-to-pass-iam-roles) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/iam-roles-for-amazon-ec2.html#permission-to-pass-iam-roles)\.

## \(optional\) Connect to the instance<a name="connect-to-instance"></a>

After an instance is running, you can connect to it remotely by using the appropriate remote client\. For both Linux and Windows instances, you need the instance's public IP address or public DNS name\. You also need the following\.

**For Linux instances**

You can use an SSH client to connect to your Linux instance\. Make sure that the security group you used when you launched the instance allows SSH traffic on port 22, as described in [Updating security groups](authorize-ingress.md)\.

You also need the private portion of the key pair you used to launch the instance; that is, the PEM file\.

For more information, see [Connect to your Linux instance](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AccessingInstances.html) in the Amazon EC2 User Guide for Linux Instances\.

**For Windows instances**

You can use an RDP client to connect to your instance\. Make sure that the security group you used when you launched the instance allows RDP traffic on port 3389, as described in [Updating security groups](authorize-ingress.md)\.

You also need the Administrator password\. You can obtain this by using the following example code, which requires the instance ID and the private portion of the key pair used to launch the instance; that is, the PEM file\.

For more information, see [Connecting to your Windows instance](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html) in the Amazon EC2 User Guide for Windows Instances\.

**Warning**  
This example code returns the plaintext Administrator password for your instance\.

### SDK references<a name="w4aac19c19c19b9c29c23b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [GetPasswordDataRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TGetPasswordDataRequest.html)

  Class [GetPasswordDataResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TGetPasswordDataResponse.html)

### The code<a name="w4aac19c19c19b9c29c25b1"></a>

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2GetWindowsPassword
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to get the Administrator password of a Windows EC2 instance
  class Program
  {
    static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if(parsedArgs.Count == 0)
      {
        PrintHelp();
        return;
      }

      // Get the application parameters from the parsed arguments
      string instanceID =
        CommandLine.GetParameter(parsedArgs, null, "-i", "--instance-id");
      string pemFileName =
        CommandLine.GetParameter(parsedArgs, null, "-p", "--pem-filename");
      if(   (string.IsNullOrEmpty(instanceID) || !instanceID.StartsWith("i-"))
         || (string.IsNullOrEmpty(pemFileName) || !pemFileName.EndsWith(".pem")))
        CommandLine.ErrorExit(
          "\nOne or more of the required arguments is missing or incorrect." +
          "\nRun the command with no arguments to see help.");

      // Create the EC2 client
      var ec2Client = new AmazonEC2Client();

      // Get and display the password
      string password = await GetPassword(ec2Client, instanceID, pemFileName);
      Console.WriteLine($"\nPassword: {password}");
    }


    //
    // Method to get the administrator password of a Windows EC2 instance
    private static async Task<string> GetPassword(
      IAmazonEC2 ec2Client, string instanceID, string pemFilename)
    {
      string password = string.Empty;
      GetPasswordDataResponse response =
        await ec2Client.GetPasswordDataAsync(new GetPasswordDataRequest{
          InstanceId = instanceID});
      if(response.PasswordData != null)
      {
        password = response.GetDecryptedPassword(File.ReadAllText(pemFilename));
      }
      else
      {
        Console.WriteLine($"\nThe password is not available for instance {instanceID}.");
        Console.WriteLine($"If this is a Windows instance, the password might not be ready.");
      }
      return password;
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: EC2CreateKeyPair -i <instance-id> -p pem-filename" +
        "\n  -i, --instance-id: The name of the EC2 instance." +
        "\n  -p, --pem-filename: The name of the PEM file with the private key.");
    }
  }

  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal.
  // (This is the same for all examples. When you have seen it once, you can ignore it.)
  static class CommandLine
  {
    // Method to parse a command line of the form: "--param value" or "-p value".
    // If "param" is found without a matching "value", Dictionary.Value is an empty string.
    // If "value" is found without a matching "param", Dictionary.Key is "--NoKeyN"
    //  where "N" represents sequential numbers.
    public static Dictionary<string,string> Parse(string[] args)
    {
      var parsedArgs = new Dictionary<string,string>();
      int i = 0, n = 0;
      while(i < args.Length)
      {
        // If the first argument in this iteration starts with a dash it's an option.
        if(args[i].StartsWith("-"))
        {
          var key = args[i++];
          var value = string.Empty;

          // Is there a value that goes with this option?
          if((i < args.Length) && (!args[i].StartsWith("-"))) value = args[i++];
          parsedArgs.Add(key, value);
        }

        // If the first argument in this iteration doesn't start with a dash, it's a value
        else
        {
          parsedArgs.Add("--NoKey" + n.ToString(), args[i++]);
          n++;
        }
      }

      return parsedArgs;
    }

    //
    // Method to get a parameter from the parsed command-line arguments
    public static string GetParameter(
      Dictionary<string,string> parsedArgs, string def, params string[] keys)
    {
      string retval = null;
      foreach(var key in keys)
        if(parsedArgs.TryGetValue(key, out retval)) break;
      return retval ?? def;
    }

    //
    // Exit with an error.
    public static void ErrorExit(string msg, int code=1)
    {
      Console.WriteLine("\nError");
      Console.WriteLine(msg);
      Environment.Exit(code);
    }
  }

}
```

## Clean up<a name="run-instance-cleanup"></a>

When you no longer need your EC2 instance, be sure to terminate it, as described in [Terminating an Amazon EC2 instance](terminate-instance.md)\.