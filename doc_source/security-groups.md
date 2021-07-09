--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Creating a Security Group in Amazon EC2<a name="security-groups"></a>

In Amazon EC2, a security group acts as a virtual firewall that controls the network traffic for one or more EC2 instances\. By default, Amazon EC2 associates your instances with a security group that allows no inbound traffic\. You can create a security group that allows your EC2 instances to accept certain traffic\. For example, if you need to connect to an EC2 Windows instance, you must configure the security group to allow RDP traffic\. You can create a security group by using the Amazon EC2 console or the AWS SDK for \.NET\.

You create a security group for use in either EC2\-Classic or EC2\-VPC\. For more information about EC2\-Classic and EC2\-VPC, see [Supported Platforms](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-supported-platforms.html) in the Amazon EC2 User Guide for Windows Instances\.

Alternatively, you can create a security group using the Amazon EC2 console\. For more information, see [Amazon EC2 Security Groups](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-network-security.html) in the Amazon EC2 User Guide for Windows Instances\.

For information on creating an Amazon EC2 client, see [Creating an Amazon EC2 Client](init-ec2-client.md)\.

## Enumerate Your Security Groups<a name="enumerate-security-groups"></a>

You can enumerate your security groups and check whether a security group exists\.

### To enumerate your security groups<a name="w8aac15c21c15c19c11b5"></a>

Get the complete list of your security groups using [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSecurityGroupsDescribeSecurityGroupsRequest.html) with no parameters\.

The following example enumerates all of the security groups in the region\.

```
static void EnumerateSecurityGroups(AmazonEC2Client ec2Client)
{
  var request = new DescribeSecurityGroupsRequest();
  var response = ec2Client.DescribeSecurityGroups(request);
  List<SecurityGroup> mySGs = response.SecurityGroups;
  foreach (SecurityGroup item in mySGs)
  {
    Console.WriteLine("Security group: " + item.GroupId);
    Console.WriteLine("\tGroupId: " + item.GroupId);
    Console.WriteLine("\tGroupName: " + item.GroupName);
    Console.WriteLine("\tVpcId: " + item.VpcId);

    Console.WriteLine();
  }
}
```

### To enumerate your security groups for a particular VPC<a name="w8aac15c21c15c19c11b7"></a>

Use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSecurityGroupsDescribeSecurityGroupsRequest.html) with a filter\.

The following example retrieves only the security groups that belong to the specified VPC\.

```
static void EnumerateVpcSecurityGroups(AmazonEC2Client ec2Client, string vpcID)
{
  Filter vpcFilter = new Filter
  {
    Name = "vpc-id",
    Values = new List<string>() { vpcID }
  };

  var request = new DescribeSecurityGroupsRequest();
  request.Filters.Add(vpcFilter);
  var response = ec2Client.DescribeSecurityGroups(request);
  List<SecurityGroup> mySGs = response.SecurityGroups;
  foreach (SecurityGroup item in mySGs)
  {
    Console.WriteLine("Security group: " + item.GroupId);
    Console.WriteLine("\tGroupId: " + item.GroupId);
    Console.WriteLine("\tGroupName: " + item.GroupName);
    Console.WriteLine("\tVpcId: " + item.VpcId);

    Console.WriteLine();
  }
}
```

## Create a Security Group<a name="creating-security-group"></a>

If you attempt to create a security group with a name of an existing security group, [CreateSecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateSecurityGroupCreateSecurityGroupRequest.html) will throw an exception\. To avoid this, the following examples search for a security group with the specified name, and return the appropriate [SecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSecurityGroup.html) object if one is found\.

### To create a security group for EC2\-Classic<a name="w8aac15c21c15c19c13b5"></a>

Create and initialize a [CreateSecurityGroupRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateSecurityGroupRequest.html) object\. Assign a name and description to the `GroupName` and `Description` properties, respectively\.

The [CreateSecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateSecurityGroupCreateSecurityGroupRequest.html) method returns a [CreateSecurityGroupResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateSecurityGroupResponse.html) object\. You can get the identifier of the new security group from the response and then use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSecurityGroupsDescribeSecurityGroupsRequest.html) with the security group identifier to get the [SecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSecurityGroup.html) object for the security group\.

```
static SecurityGroup CreateEc2SecurityGroup(
  AmazonEC2Client ec2Client,
  string secGroupName)
{
  // See if a security group with the specified name already exists
  Filter nameFilter = new Filter();
  nameFilter.Name = "group-name";
  nameFilter.Values= new List<string>() { secGroupName };

  var describeRequest = new DescribeSecurityGroupsRequest();
  describeRequest.Filters.Add(nameFilter);
  var describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);

  // If a match was found, return the SecurityGroup object for the security group
  if(describeResponse.SecurityGroups.Count > 0)
  {
    return describeResponse.SecurityGroups[0];
  }

  // Create the security group
  var createRequest = new CreateSecurityGroupRequest();
  createRequest.GroupName = secGroupName;
  createRequest.Description = "My sample security group for EC2-Classic";

  var createResponse = ec2Client.CreateSecurityGroup(createRequest);

  var Groups = new List<string>() { createResponse.GroupId };
  describeRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
  describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
  return describeResponse.SecurityGroups[0];
}
```

### To create a security group for EC2\-VPC<a name="w8aac15c21c15c19c13b7"></a>

Create and initialize a [CreateSecurityGroupRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateSecurityGroupRequest.html) object\. Assign values to the `GroupName`, `Description`, and `VpcId` properties\.

The [CreateSecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateSecurityGroupCreateSecurityGroupRequest.html) method returns a [CreateSecurityGroupResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateSecurityGroupResponse.html) object\. You can get the identifier of the new security group from the response and then use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeSecurityGroupsDescribeSecurityGroupsRequest.html) with the security group identifier to get the [SecurityGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TSecurityGroup.html) object for the security group\.

```
static SecurityGroup CreateVpcSecurityGroup(
  AmazonEC2Client ec2Client,
  string vpcId,
  string secGroupName)
{
  // See if a security group with the specified name already exists
  Filter nameFilter = new Filter();
  nameFilter.Name = "group-name";
  nameFilter.Values = new List<string>() { secGroupName };

  var describeRequest = new DescribeSecurityGroupsRequest();
  describeRequest.Filters.Add(nameFilter);
  var describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);

  // If a match was found, return the SecurityGroup object for the security group
  if (describeResponse.SecurityGroups.Count > 0)
  {
    return describeResponse.SecurityGroups[0];
  }

  // Create the security group
  var createRequest = new CreateSecurityGroupRequest();
  createRequest.GroupName = secGroupName;
  createRequest.Description = "My sample security group for EC2-VPC";
  createRequest.VpcId = vpcId;

  var createResponse = ec2Client.CreateSecurityGroup(createRequest);

  var Groups = new List<string>() { createResponse.GroupId };
  describeRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
  describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
  return describeResponse.SecurityGroups[0];
}
```

## Add Rules to Your Security Group<a name="authorize-ingress"></a>

Use the following procedure to add a rule to allow inbound traffic on TCP port 3389 \(RDP\)\. This enables you to connect to a Windows instance\. If you’re launching a Linux instance, use TCP port 22 \(SSH\) instead\.

**Note**  
You can use a service to get the public IP address of your local computer\. For example, we provide the following service: [http://checkip\.amazonaws\.com/](http://checkip.amazonaws.com/)\. To locate another service that provides your IP address, use the search phrase “what is my IP address”\. If you are connecting through an ISP or from behind your firewall without a static IP address, you need to find out the range of IP addresses used by client computers\.

The examples in this section follow from the examples in the previous sections\. They assume `secGroup` is an existing security group\.

**To add a rule to a security group**

1. Create and initialize an [IpPermission](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIpPermission.html) object\.

   ```
   string ipRange = "1.1.1.1/1";
   List<string> ranges = new List<string>() { ipRange };
   
   var ipPermission = new IpPermission();
   ipPermission.IpProtocol = "tcp";
   ipPermission.FromPort = 3389;
   ipPermission.ToPort = 3389;
   ipPermission.IpRanges = ranges;
   ```  
** `IpProtocol` **  
The IP protocol\.  
** `FromPort` and `ToPort` **  
The beginning and end of the port range\. This example specifies a single port, 3389, which is used to communicate with Windows over RDP\.  
** `IpRanges` **  
The IP addresses or address ranges, in CIDR notation\. For convenience, this example uses `72.21.198.64/24`, which authorizes network traffic for a single IP address\. You can use [http://checkip\.amazonaws\.com/](http://checkip.amazonaws.com/) to determine your own IP addcress\.

1. Create and initialize an [AuthorizeSecurityGroupIngressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAuthorizeSecurityGroupIngressRequest.html) object\.

   ```
   var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
   ingressRequest.GroupId = secGroup.GroupId;
   ingressRequest.IpPermissions.Add(ipPermission);
   ```  
** `GroupId` **  
The identifier of the security group\.  
** `IpPermissions` **  
The `IpPermission` object from step 1\.

1. \(Optional\) You can add additional rules to the `IpPermissions` collection before going to the next step\.

1. Pass the [AuthorizeSecurityGroupIngressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAuthorizeSecurityGroupIngressRequest.html) object to the [AuthorizeSecurityGroupIngress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2AuthorizeSecurityGroupIngressAuthorizeSecurityGroupIngressRequest.html) method, which returns an [AuthorizeSecurityGroupIngressResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAuthorizeSecurityGroupIngressResponse.html) object\. If a matching rule already exists, an [AmazonEC2Exception](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Exception.html) is thrown\.

   ```
   try
   {
     var ingressResponse = ec2Client.AuthorizeSecurityGroupIngress(ingressRequest);
     Console.WriteLine("New RDP rule for: " + ipRange);
   }
   catch (AmazonEC2Exception ex)
   {
     // Check the ErrorCode to see if the rule already exists
     if ("InvalidPermission.Duplicate" == ex.ErrorCode)
     {
       Console.WriteLine("An RDP rule for: {0} already exists.", ipRange);
     }
     else
     {
       // The exception was thrown for another reason, so re-throw the exception
       throw;
     }
   }
   ```