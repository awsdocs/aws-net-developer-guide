--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Using Regions and Availability Zones with Amazon EC2<a name="using-regions-and-availability-zones"></a>

This \.NET example shows you how to:
+ Get details about Availability Zones
+ Get details about regions

## The Scenario<a name="the-scenario"></a>

Amazon EC2 is hosted in multiple locations worldwide\. These locations are composed of regions and Availability Zones\. Each region is a separate geographic area that has multiple, isolated locations known as Availability Zones\. Amazon EC2 provides the ability to place instances and data in multiple locations\.

You can use the AWS SDK for \.NET to retrieve details about regions and Availability Zones by using the following methods of the [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) class:
+  [DescribeAvailabilityZones](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeAvailabilityZonesDescribeAvailabilityZonesRequest.html) 
+  [DescribeRegions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeRegionsDescribeRegionsRequest.html) 

For more information about regions and Availability Zones, see [Regions and Availability Zones](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-regions-availability-zones.html) in the Amazon EC2 User Guide for Windows Instances\.

## Describe Availability Zones<a name="describe-availability-zones"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance and call the [DescribeAvailabilityZones](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeAvailabilityZonesDescribeAvailabilityZonesRequest.html) method\. The [DescribeAvailabilityZonesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeAvailabilityZonesResponse.html) object that is returned contains a list of Availability Zones\.

```
public static void DescribeAvailabilityZones()
{
    Console.WriteLine("Describe Availability Zones");
    AmazonEC2Client client = new AmazonEC2Client();
    DescribeAvailabilityZonesResponse response = client.DescribeAvailabilityZones();
    var availZones = new List<AvailabilityZone>();
    availZones = response.AvailabilityZones;
    foreach (AvailabilityZone az in availZones)
    {
        Console.WriteLine(az.ZoneName);
    }
}
```

## Describe Regions<a name="describe-regions"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance and call the [DescribeRegions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeRegionsDescribeRegionsRequest.html) method\. The [DescribeRegionsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeRegionsResponse.html) object that is returned contains a list of regions\.

```
public static void DescribeRegions()
{
    Console.WriteLine("Describe Regions");
    AmazonEC2Client client = new AmazonEC2Client();
    DescribeRegionsResponse response = client.DescribeRegions();
    var regions = new List<Region>();
    regions = response.Regions;
    foreach (Region region in regions)
    {
        Console.WriteLine(region.RegionName);
    }
}
```