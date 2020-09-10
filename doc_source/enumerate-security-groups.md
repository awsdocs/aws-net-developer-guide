--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Enumerating security groups<a name="enumerate-security-groups"></a>

This example shows you how to use the AWS SDK for \.NET to enumerate security groups\. If you supply an [Amazon Virtual Private Cloud](https://docs.aws.amazon.com/vpc/latest/userguide/) ID, the application enumerates the security groups for that particular VPC\. Otherwise, the application simply displays a list of all available security groups\.

The following sections provide snippets of this example\. The [complete code for the example](#enum-sec-groups-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Enumerate security groups](#enum-sec-groups-enum)
+ [Complete code](#enum-sec-groups-complete-code)

## Enumerate security groups<a name="enum-sec-groups-enum"></a>

The following snippet enumerates your security groups\. It enumerates all groups or the groups for a particular VPC if one is given\.

The example [at the end of this topic](#enum-sec-groups-complete-code) shows this snippet in use\.

```
    //
    // Method to enumerate the security groups
    private static async Task EnumerateGroups(IAmazonEC2 ec2Client, string vpcID)
    {
      // A request object, in case we need it.
      var request = new DescribeSecurityGroupsRequest();

      // Put together the properties, if needed
      if(!string.IsNullOrEmpty(vpcID))
      {
        // We have a VPC ID. Find the security groups for just that VPC.
        Console.WriteLine($"\nGetting security groups for VPC {vpcID}...\n");
        request.Filters.Add(new Filter
        {
          Name = "vpc-id",
          Values = new List<string>() { vpcID }
        });
      }

      // Get the list of security groups
      DescribeSecurityGroupsResponse response =
        await ec2Client.DescribeSecurityGroupsAsync(request);

      // Display the list of security groups.
      foreach (SecurityGroup item in response.SecurityGroups)
      {
        Console.WriteLine("Security group: " + item.GroupId);
        Console.WriteLine("\tGroupId: " + item.GroupId);
        Console.WriteLine("\tGroupName: " + item.GroupName);
        Console.WriteLine("\tVpcId: " + item.VpcId);
        Console.WriteLine();
      }
    }
```

## Complete code<a name="enum-sec-groups-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c19c13c13c15b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [DescribeSecurityGroupsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSecurityGroupsRequest.html)

  Class [DescribeSecurityGroupsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSecurityGroupsResponse.html)

  Class [Filter](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TFilter.html)

  Class [SecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSecurityGroup.html)

### The Code<a name="w4aac19c19c13c13c15b7b1"></a>

```
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2EnumerateSecGroups
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Parse the command line
       string vpcID = string.Empty;
      if(args.Length == 0)
      {
        Console.WriteLine("\nEC2EnumerateSecGroups [vpc_id]");
        Console.WriteLine("  vpc_id - The ID of the VPC for which you want to see security groups.");
        Console.WriteLine("\nSince you specified no arguments, showing all available security groups.");
      }
      else
      {
        vpcID = args[0];
      }

      if(vpcID.StartsWith("vpc-") || string.IsNullOrEmpty(vpcID))
      {
        // Create an EC2 client object
        var ec2Client = new AmazonEC2Client();

        // Enumerate the security groups
        await EnumerateGroups(ec2Client, vpcID);
      }
      else
      {
        Console.WriteLine("Could not find a valid VPC ID in the command-line arguments:");
        Console.WriteLine($"{args[0]}");
      }
    }


    //
    // Method to enumerate the security groups
    private static async Task EnumerateGroups(IAmazonEC2 ec2Client, string vpcID)
    {
      // A request object, in case we need it.
      var request = new DescribeSecurityGroupsRequest();

      // Put together the properties, if needed
      if(!string.IsNullOrEmpty(vpcID))
      {
        // We have a VPC ID. Find the security groups for just that VPC.
        Console.WriteLine($"\nGetting security groups for VPC {vpcID}...\n");
        request.Filters.Add(new Filter
        {
          Name = "vpc-id",
          Values = new List<string>() { vpcID }
        });
      }

      // Get the list of security groups
      DescribeSecurityGroupsResponse response =
        await ec2Client.DescribeSecurityGroupsAsync(request);

      // Display the list of security groups.
      foreach (SecurityGroup item in response.SecurityGroups)
      {
        Console.WriteLine("Security group: " + item.GroupId);
        Console.WriteLine("\tGroupId: " + item.GroupId);
        Console.WriteLine("\tGroupName: " + item.GroupName);
        Console.WriteLine("\tVpcId: " + item.VpcId);
        Console.WriteLine();
      }
    }
  }
}
```

**Additional considerations**
+ Notice for the VPC case that the filter is constructed with the `Name` part of the name\-value pair set to "vpc\-id"\. This name comes from the description for the `Filters` property of the [DescribeSecurityGroupsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeSecurityGroupsRequest.html) class\.
+ To get the complete list of your security groups, you can also use [ DescribeSecurityGroupsAsync with no parameters](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSecurityGroupsAsyncCancellationToken.html)\.
+ You can verify the results by checking the list of security groups in the [Amazon EC2 console](https://console.aws.amazon.com/ec2/v2/home#SecurityGroups)\.