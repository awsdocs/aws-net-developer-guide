--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# AWS Region Selection<a name="net-dg-region-selection"></a>

AWS Regions allow you to access AWS services that physically reside in a specific geographic region\. This can be useful for redundancy and to keep your data and applications running close to where you and your users will access them\. You can specify a region when creating the AWS service client by using the [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpoint.html) class\.

Here is an example that instantiates an Amazon EC2 client in a specific region\.

```
AmazonEC2Client ec2Client = new AmazonEC2Client(RegionEndpoint.USEast1);
```

Regions are isolated from each other\. For example, you can’t access US East \(N\. Virginia\) resources when using the Europe \(Ireland\) region\. If your code needs access to multiple AWS Regions, we recommend you create a separate client for each region\.

To use services in the China \(Beijing\) Region, you must have an account and credentials that are specific to the China \(Beijing\) Region\. Accounts and credentials for other AWS Regions won’t work for the China \(Beijing\) Region\. Likewise, accounts and credentials for the China \(Beijing\) Region won’t work for other AWS Regions\. For information about endpoints and protocols that are available in the China \(Beijing\) Region, see [China \(Beijing\) Region](http://docs.amazonaws.cn/en_us/general/latest/gr/rande.html#cnnorth_region)\.

New AWS services can be launched initially in a few regions and then supported in other regions\. In these cases you don’t need to install the latest SDK to access the new regions\. You can specify newly added regions on a per\-client basis or globally\.

## Per\-Client<a name="per-client"></a>

Setting the Region in a client takes precedence over any global setting\.

Construct the new region endpoint by using [GetBySystemName](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/MRegionEndpointGetBySystemNameString.html):

```
var newRegion = RegionEndpoint.GetBySystemName("us-west-new");
using (var ec2Client = new AmazonEC2Client(newRegion))
{
  // Make a request to EC2 using ec2Client
}
```

You can also use the `ServiceURL` property of the service client configuration class to specify the region\. This technique works even if the region endpoint does not follow the regular region endpoint pattern\.

```
var ec2ClientConfig = new AmazonEC2Config
{
    // Specify the endpoint explicitly
    ServiceURL = "https://ec2.us-west-new.amazonaws.com"
};

using (var ec2Client = new AmazonEC2Client(ec2ClientConfig))
{
  // Make a request to EC2 using ec2Client
}
```

## Globally<a name="globally"></a>

There are a number of ways you can specify a Region for all clients\. The AWS SDK for \.NET looks for a Region value in the following order:

Set as a `AWSConfigs.AWSRegion` property,

```
AWSConfigs.AWSRegion = "us-west-new";
using (var ec2Client = new AmazonEC2Client())
{
  // Make request to Amazon EC2 using ec2Client
}
```

Set as a `AWSRegion` key in the `appSettings` section of the `app.config` file\.

```
<configuration>
  <appSettings>
    <add key="AWSRegion" value="us-west-2"/>
  </appSettings>
</configuration>
```

Set as a `region` attribute in the `aws` section as described in [AWSRegion](net-dg-config-other.md#config-setting-awsregion)\.

```
<aws region="us-west-2"/>
```

To view the current list of all supported regions and endpoints for each AWS service, see [Regions and Endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html) in the Amazon Web Services General Reference\.