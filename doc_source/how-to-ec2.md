--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Tutorial: Creating Amazon EC2 Instances with the AWS SDK for \.NET<a name="how-to-ec2"></a>

You can access the features of Amazon EC2 using the [AWS SDK for \.NET](https://aws.amazon.com/sdk-for-net/)\. For example, you can create, start, and terminate EC2 instances\.

The sample code in this tutorial is written in C\#, but you can use the AWS SDK for \.NET with any compatible language\. The AWS SDK for \.NET installs a set of C\# project templates, so the simplest way to start this project is to open Visual Studio, select **New Project** from the **File** menu, and then select **AWS Empty Project**\.

 **Prerequisites** 

Before you begin, be sure that you have created an AWS account and that you have set up your AWS credentials\. For more information, see [Getting Started with the AWS SDK for \.NET](net-dg-setup.md)\.

## Tasks<a name="tasks"></a>

The following tasks demonstrate how to manage EC2 instances using the AWS SDK for \.NET\.
+  [Create an Amazon EC2 Client Using the the SDK](#init-ec2-client) 
+  [Create a Security Group Using the the SDK](#create-security-group) 
+  [Create a Key Pair Using the the SDK](#create-key-pair) 
+  [Launch an EC2 Instance Using the the SDK](run-instance.md) 
+  [Terminate an EC2 Instance Using the the SDK](terminate-instance.md) 

## Create an Amazon EC2 Client Using the the SDK<a name="init-ec2-client"></a>

Create an *Amazon EC2 client* to manage your EC2 resources, such as instances and security groups\. This client is represented by an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2EC2NET45.html) object, which you can create as follows:

```
var ec2Client = new AmazonEC2Client();
```

The permissions for the client object are determined by the policy that is attached to the profile that you specified in the `App.config` file\. By default, we use the region specified in `App.config`\. To use a different region, pass the appropriate [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRegionEndpointNET45.html) value to the constructor\. For more information, see [Regions and Endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html#ec2_region) in the Amazon Web Services General Reference\.

## Create a Security Group Using the the SDK<a name="create-security-group"></a>

Create a *security group*, which acts as a virtual firewall that controls the network traffic for one or more EC2 instances\. By default, Amazon EC2 associates your instances with a security group that allows no inbound traffic\. You can create a security group that allows your EC2 instances to accept certain traffic\. For example, if you need to connect to an EC2 Windows instance, you must configure the security group to allow RDP traffic\. You can create a security group using the Amazon EC2 console or the the SDK\.

You create a security group for use in either EC2\-Classic or EC2\-VPC\. For more information about EC2\-Classic and EC2\-VPC, see [Supported Platforms](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-supported-platforms.html) in the Amazon EC2 User Guide for Windows Instances\.

Alternatively, you can create a security group using the Amazon EC2 console\. For more information, see [Amazon EC2 Security Groups](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-network-security.html) in the Amazon EC2 User Guide for Windows Instances\.

**Topics**
+ [Enumerating Your Security Groups](#enumerate-security-groups)
+ [Creating a Security Group](#creating-security-group)
+ [Adding Rules to Your Security Group](#authorize-ingress)

### Enumerating Your Security Groups<a name="enumerate-security-groups"></a>

You can enumerate your security groups and check whether a particular security group exists\.

#### To enumerate your security groups for EC2\-Classic<a name="to-enumerate-your-security-groups-for-ec2-classic"></a>

Get the complete list of your security groups using [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeSecurityGroupsNET45.html) with no parameters\. The following example checks each security group to see whether its name is `my-sample-sg`\.

```
string secGroupName = "my-sample-sg";
SecurityGroup mySG = null;

var dsgRequest = new DescribeSecurityGroupsRequest();
var dsgResponse = ec2Client.DescribeSecurityGroups(dsgRequest);
List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
foreach (SecurityGroup item in mySGs)
{
    Console.WriteLine("Existing security group: " + item.GroupId);
    if (item.GroupName == secGroupName)
    {
        mySG = item;
    }
}
```

#### To enumerate your security groups for a VPC<a name="to-enumerate-your-security-groups-for-a-vpc"></a>

To enumerate the security groups for a particular VPC, use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeSecurityGroupsNET45.html) with a filter\. The following example checks each security group for a security group with the name `my-sample-sg-vpc`\.

```
string secGroupName = "my-sample-sg-vpc";
SecurityGroup mySG = null;
string vpcID = "vpc-f1663d98";

Filter vpcFilter = new Filter
{
    Name = "vpc-id",
    Values = new List<string>() {vpcID}
};
var dsgRequest = new DescribeSecurityGroupsRequest();
dsgRequest.Filters.Add(vpcFilter);
var dsgResponse = ec2Client.DescribeSecurityGroups(dsgRequest);
List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
foreach (SecurityGroup item in mySGs)
{
    Console.WriteLine("Existing security group: " + item.GroupId);
    if (item.GroupName == secGroupName)
    {
        mySG = item;
    }
}
```

### Creating a Security Group<a name="creating-security-group"></a>

The examples in this section follow from the examples in the previous section\. If the security group doesn’t already exist, create it\. Note that if you were to specify the same name as an existing security group, `CreateSecurityGroup` throws an exception\.

#### To create a security group for EC2\-Classic<a name="to-create-a-security-group-for-ec2-classic"></a>

Create and initialize a [CreateSecurityGroupRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateSecurityGroupRequestNET45.html) object\. Assign a name and description to the [GroupName](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateSecurityGroupRequestGroupNameNET45.html) and [Description](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateSecurityGroupRequestDescriptionNET45.html) properties, respectively\.

The [CreateSecurityGroup](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2CreateSecurityGroupCreateSecurityGroupRequestNET45.html) method returns a [CreateSecurityGroupResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateSecurityGroupRequestNET45.html) object\. You can get the ID of the new security group from the response and then use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeSecurityGroupsNET45.html) with the security group ID to get the `SecurityGroup` object for the security group\.

```
if (mySG == null)
{
    var newSGRequest = new CreateSecurityGroupRequest()
    {
        GroupName = secGroupName,
        Description = "My sample security group for EC2-Classic"
    };
    var csgResponse = ec2Client.CreateSecurityGroup(newSGRequest);
    Console.WriteLine();
    Console.WriteLine("New security group: " + csgResponse.GroupId);

    List<string> Groups = new List<string>() { csgResponse.GroupId };
    var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
    var newSgResponse = ec2Client.DescribeSecurityGroups(newSgRequest);
    mySG = newSgResponse.SecurityGroups[0];
}
```

#### To create a security group for EC2\-VPC<a name="to-create-a-security-group-for-ec2-vpc"></a>

Create and initialize a [CreateSecurityGroupRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateSecurityGroupRequestNET45.html) object\. Assign values to the [GroupName](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateSecurityGroupRequestGroupNameNET45.html), [Description](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateSecurityGroupRequestDescriptionNET45.html), and [VpcId](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateSecurityGroupRequestVpcIdNET45.html) properties\.

The [CreateSecurityGroup](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2CreateSecurityGroupCreateSecurityGroupRequestNET45.html) method returns a [CreateSecurityGroupResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateSecurityGroupRequestNET45.html) object\. You can get the ID of the new security group from the response and then use [DescribeSecurityGroups](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeSecurityGroupsNET45.html) with the security group ID to get the `SecurityGroup` object for the security group\.

```
if (mySG == null)
{
    var newSGRequest = new CreateSecurityGroupRequest()
    {
        GroupName = secGroupName,
        Description = "My sample security group for EC2-VPC",
        VpcId = vpcID
    };
    var csgResponse = ec2Client.CreateSecurityGroup(newSGRequest);
    Console.WriteLine();
    Console.WriteLine("New security group: " + csgResponse.GroupId);

    List<string> Groups = new List<string>() { csgResponse.GroupId };
    var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
    var newSgResponse = ec2Client.DescribeSecurityGroups(newSgRequest);
    mySG = newSgResponse.SecurityGroups[0];
}
```

### Adding Rules to Your Security Group<a name="authorize-ingress"></a>

Use the following procedure to add a rule to allow inbound traffic on TCP port 3389 \(RDP\)\. This enables you to connect to a Windows instance\. If you’re launching a Linux instance, use TCP port 22 \(SSH\) instead\.

**Note**  
You can get the public IP address of your local computer using a service\. For example, we provide the following service: [http://checkip\.amazonaws\.com/](http://checkip.amazonaws.com/)\. To locate another service that provides your IP address, use the search phrase “what is my IP address”\. If you are connecting through an ISP or from behind your firewall without a static IP address, you need to find out the range of IP addresses used by client computers\.

The examples in this section follow from the examples in the previous sections\. They assume that `mySG` is an existing security group\.

#### To add a rule to a security group<a name="to-add-a-rule-to-a-security-group"></a>

1. Create and initialize an [IpPermission](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2IpPermissionNET45.html) object\.

   ```
   string ipRange = "0.0.0.0/0";
   List<string> ranges = new List<string>() {ipRange};
   
   var ipPermission = new IpPermission()
   {
       IpProtocol = "tcp",
       FromPort = 3389,
       ToPort = 3389,
       IpRanges = ranges
   };
   ```  
** [IpProtocol](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2IpPermissionIpProtocolNET45.html) **  
The IP protocol\.  
** [FromPort](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2IpPermissionFromPortNET45.html) and [ToPort](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2IpPermissionToPortNET45.html) **  
The beginning and end of the port range\. This example specifies a single port, 3389, which is used to communicate with Windows over RDP\.  
** [IpRanges](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2IpPermissionIpRangesNET45.html) **  
The IP addresses or address ranges, in CIDR notation\. For convenience, this example uses `0.0.0.0/0`, which authorizes network traffic from all IP addresses\. This is acceptable for a short time in a test environment, but it’s unsafe in a production environment\.

1. Create and initialize an [AuthorizeSecurityGroupIngressRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2AuthorizeSecurityGroupIngressRequestNET45.html) object\.

   ```
   var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
   ingressRequest.GroupId = mySG.GroupId;
   ingressRequest.IpPermissions.Add(ipPermission);
   ```  
** [GroupId](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2AuthorizeSecurityGroupIngressRequestGroupIdNET45.html) **  
The ID of the security group\.  
** [IpPermissions](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2AuthorizeSecurityGroupIngressRequestIpPermissionsNET45.html) **  
The `IpPermission` object from step 1\.

1. \(Optional\) You can add additional rules to the `IpPermissions` collection before going to the next step\.

1. Pass the request object to the [AuthorizeSecurityGroupIngress](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2AuthorizeSecurityGroupIngressAuthorizeSecurityGroupIngressRequestNET45.html) method, which returns an [AuthorizeSecurityGroupIngressResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2AuthorizeSecurityGroupIngressResponseNET45.html) object\.

   ```
   var ingressResponse =  ec2Client.AuthorizeSecurityGroupIngress(ingressRequest);
   Console.WriteLine("New RDP rule for: " + ipRange);
   ```

## Create a Key Pair Using the the SDK<a name="create-key-pair"></a>

You must specify a key pair when you launch an EC2 instance and specify the private key of the key pair when you connect to the instance\. You can create a key pair or use an existing key pair that you’ve used when launching other instances\. For more information, see [Amazon EC2 Key Pairs](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-key-pairs.html) in the Amazon EC2 User Guide for Windows Instances\.

### Enumerating Your Key Pairs<a name="enumerate-key-pairs"></a>

You can enumerate your key pairs and check whether a particular key pair exists\.

 **To enumerate your key pairs** 

Get the complete list of your key pairs using [DescribeKeyPairs](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2DescribeKeyPairsNET45.html) with no parameters\. The following example checks each key pair to see whether its name is `my-sample-key`\.

```
string keyPairName = "my-sample-key";
KeyPairInfo myKeyPair = null;

var dkpRequest = new DescribeKeyPairsRequest();
var dkpResponse = ec2Client.DescribeKeyPairs(dkpRequest);
List<KeyPairInfo> myKeyPairs = dkpResponse.KeyPairs;

foreach (KeyPairInfo item in myKeyPairs)
{
    Console.WriteLine("Existing key pair: " + item.KeyName);
    if (item.KeyName == keyPairName)
    {
        myKeyPair = item;
    }
}
```

### Creating a Key Pair and Saving the Private Key<a name="create-save-key-pair"></a>

The example in this section follows from the example in the previous section\. If the key pair doesn’t already exist, create it\. Be sure to save the private key now, because you can’t retrieve it later\.

 **To create a key pair and save the private key** 

Create and initialize a [CreateKeyPairRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateKeyPairRequestNET45.html) object\. Set the [KeyName](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2CreateKeyPairRequestKeyNameNET45.html) property to the name of the key pair\.

Pass the request object to the [CreateKeyPair](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2CreateKeyPairCreateKeyPairRequestNET45.html) method, which returns a [CreateKeyPairResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateKeyPairResponseNET45.html) object\.

The response object includes a [CreateKeyPairResult](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2CreateKeyPairResultNET45.html) property that contains the new key’s [KeyPair](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2KeyPairNET45.html) object\. The `KeyPair` object’s [KeyMaterial](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2KeyPairKeyMaterialNET45.html) property contains the unencrypted private key for the key pair\. Save the private key as a `.pem` file in a safe location\. You’ll need this file when you connect to your instance\. This example saves the private key in the current directory, using the name of the key pair as the base file name of the `.pem` file\.

```
if (myKeyPair == null)
{
    var newKeyRequest = new CreateKeyPairRequest()
    {
        KeyName = keyPairName
    };
    var ckpResponse = ec2Client.CreateKeyPair(newKeyRequest);
    Console.WriteLine();
    Console.WriteLine("New key: " + keyPairName);

    // Save the private key in a .pem file
    using (FileStream s = new FileStream(keyPairName + ".pem", FileMode.Create))
    using (StreamWriter writer = new StreamWriter(s))
    {
        writer.WriteLine(ckpResponse.KeyPair.KeyMaterial);
    }
}
```

**Topics**