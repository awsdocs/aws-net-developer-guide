--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Using Elastic IP Addresses in Amazon EC2<a name="ec2-example-elastic-ip-addresses"></a>

This \.NET example shows you how to:
+ Retrieve descriptions of your Elastic IP addresses
+ Allocate and associate an Elastic IP address with an Amazon EC2 instance
+ Release an Elastic IP address

## The Scenario<a name="the-scenario"></a>

An Elastic IP address is a static IP address designed for dynamic cloud computing\. An Elastic IP address is associated with your AWS account, and is a public IP address reachable from the Internet\.

If your Amazon EC2 instance doesn’t have a public IP address, you can associate an Elastic IP address with your instance to enable communication with the Internet\.

In this example, you use the AWS SDK for \.NET to manage Elastic IP addresses by using these methods of the Amazon EC2 client class:
+  [DescribeAddresses](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeAddressesDescribeAddressesRequest.html) 
+  [AllocateAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2AllocateAddressAllocateAddressRequest.html) 
+  [AssociateAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2AssociateAddressAssociateAddressRequest.html) 
+  [ReleaseAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2ReleaseAddressReleaseAddressRequest.html) 

For more information about Elastic IP addresses in Amazon EC2, see [Elastic IP Addresses](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/elastic-ip-addresses-eip.html) in the Amazon EC2 User Guide for Windows Instances\.

## Describe Elastic IP Addresses<a name="describe-elastic-ip-addresses"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) object\. Next, create a [DescribeAddressesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeAddressesRequest.html) object to pass as a parameter, filtering the addresses returned by those in your VPC\. To retrieve descriptions of all your Elastic IP addresses, omit the filter from the parameters\. Then call the [DescribeAddresses](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeAddressesDescribeAddressesRequest.html) method of the `AmazonEC2Client` object\.

```
public void DescribeElasticIps()
{
    using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
    {
        var addresses = client.DescribeAddresses(new DescribeAddressesRequest
        {
            Filters = new List<Filter>
            {
                new Filter
                {
                    Name = "domain",
                    Values = new List<string> { "vpc" }
                }
            }
        }).Addresses;

        foreach(var address in addresses)
        {
            Console.WriteLine(address.PublicIp);
            Console.WriteLine("\tAllocation Id: " + address.AllocationId);
            Console.WriteLine("\tPrivate IP Address: " + address.PrivateIpAddress);
            Console.WriteLine("\tAssociation Id: " + address.AssociationId);
            Console.WriteLine("\tInstance Id: " + address.InstanceId);
            Console.WriteLine("\tNetwork Interface Owner Id: " + address.NetworkInterfaceOwnerId);
        }
    }
}
```

## Allocate and Associate an Elastic IP Address<a name="allocate-and-associate-an-elastic-ip-address"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) object\. Next, create an [AllocateAddressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAllocateAddressRequest.html) object for the parameter used to allocate an Elastic IP address, which in this case specifies that the domain is a VPC\. Call the [AllocateAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2AllocateAddressAllocateAddressRequest.html) method of the `AmazonEC2Client` object\.

Upon success, the returned [AllocateAddressResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAllocateAddressResponse.html) object has an `AllocationId` property that identifies the allocated Elastic IP address\.

Create an [AssociateAddressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAssociateAddressRequest.html) object for the parameters used to associate an Elastic IP address to an Amazon EC2 instance\. Include the `AllocationId` from the newly allocated address and the `InstanceId` of the Amazon EC2 instance\. Then call the [AssociateAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2AssociateAddressAssociateAddressRequest.html) method of the `AmazonEC2Client` object\.

```
public void AllocateAndAssociate(string instanceId)
{
    using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
    {
        var allocationId = client.AllocateAddress(new AllocateAddressRequest
        {
            Domain = DomainType.Vpc
        }).AllocationId;

        Console.WriteLine("Allocation Id: " + allocationId);

        var associationId = client.AssociateAddress(new AssociateAddressRequest
        {
            AllocationId = allocationId,
            InstanceId = instanceId
        }).AssociationId;

        Console.WriteLine("Association Id: " + associationId);
    }
}
```

## Release an Elastic IP Address<a name="release-an-elastic-ip-address"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) object\. Next, create a [ReleaseAddressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TReleaseAddressRequest.html) object for the parameters used to release an Elastic IP address, which in this case specifies the `AllocationId` for the Elastic IP address\. Releasing an Elastic IP address also disassociates it from any Amazon EC2 instance\. Call the [ReleaseAddress](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2ReleaseAddressReleaseAddressRequest.html) method of the Amazon EC2 service object\.

```
public void Release(string allocationId)
{
    using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
    {
        client.ReleaseAddress(new ReleaseAddressRequest
        {
            AllocationId = allocationId
        });
    }
}
```