--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Creating an Amazon EC2 Client<a name="init-ec2-client"></a>

Create an Amazon EC2 client to manage your EC2 resources, such as instances and security groups\. This client is represented by an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) object, which you can create as follows\.

```
var ec2Client = new AmazonEC2Client();
```

The permissions for the client object are determined by the policy attached to the profile you specified in the `App.config` file\. By default, we use the region specified in `App.config`\. To use a different region, pass the appropriate [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpoint.html) value to the constructor\. For more information, see [Regions and Endpoints: EC2](https://docs.aws.amazon.com/general/latest/gr/rande.html#ec2_region) in the Amazon Web Services General Reference\.