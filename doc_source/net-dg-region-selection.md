--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Configure the AWS Region<a name="net-dg-region-selection"></a>

AWS Regions allow you to access AWS services that physically reside in a specific geographic region\. This can be useful for redundancy and to keep your data and applications running close to where you and your users will access them\.

To view the current list of all supported Regions and endpoints for each AWS service, see [Service endpoints and quotas](https://docs.aws.amazon.com/general/latest/gr/aws-service-information.html) in the *AWS General Reference*\. To view a list of existing Regional endpoints, see [AWS service endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html)\. To see detailed information about Regions, see [Managing AWS Regions](https://docs.aws.amazon.com/general/latest/gr/rande-manage.html)\.

You can create an AWS service client that goes to a [particular Region](#per-client)\. You can also configure your application with a Region that will be used for [all AWS service clients](#globally)\. These two cases are explained next\.

## Create a service client with a particular Region<a name="per-client"></a>

You can specify the Region for any of the AWS service clients in your application\. Setting the Region in this way takes precedence over any global setting for that particular service client\.

### Existing Region<a name="w4aac13c25c11b5"></a>

This example shows you how to instantiate an [Amazon EC2 client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) in an existing Region\. It uses defined [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpoint.html) fields\.

```
using (AmazonEC2Client ec2Client = new AmazonEC2Client(RegionEndpoint.USWest2))
{
  // Make a request to EC2 in the us-west-2 Region using ec2Client
}
```

### New Region using RegionEndpoint class<a name="w4aac13c25c11b7"></a>

This example shows you how to construct a new Region endpoint by using [RegionEndpoint\.GetBySystemName](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/MRegionEndpointGetBySystemNameString.html)\.

```
var newRegion = RegionEndpoint.GetBySystemName("us-west-new");
using (var ec2Client = new AmazonEC2Client(newRegion))
{
  // Make a request to EC2 in the new Region using ec2Client
}
```

### New Region using the service client configuration class<a name="w4aac13c25c11b9"></a>

This example shows you how to use the `ServiceURL` property of the service client configuration class to specify the Region; in this case, using the [AmazonEC2Config](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Config.html) class\.

This technique works even if the Region endpoint doesn't follow the regular Region endpoint pattern\.

```
var ec2ClientConfig = new AmazonEC2Config
{
    // Specify the endpoint explicitly
    ServiceURL = "https://ec2.us-west-new.amazonaws.com"
};

using (var ec2Client = new AmazonEC2Client(ec2ClientConfig))
{
  // Make a request to EC2 in the new Region using ec2Client
}
```

## Specify a Region for all service clients<a name="globally"></a>

There are several ways you can specify a Region for all of the AWS service clients that your application creates\. This Region is used for service clients that aren't created with a particular Region\.

The AWS SDK for \.NET looks for a Region value in the following order\.

### Profiles<a name="w4aac13c25c15b7"></a>

Set in a profile that your application or the SDK has loaded\. For more information, see [Credential and profile resolution](creds-assign.md)\.

### Environment variables<a name="w4aac13c25c15b9"></a>

Set in the `AWS_REGION` environment variable\.

On Linux or macOS:

```
AWS_REGION='us-west-2'
```

On Windows:

```
set AWS_REGION=us-west-2
```

**Note**  
If you set this environment variable for the whole system \(using `export` or `setx`\), it affects all SDKs and toolkits, not just the AWS SDK for \.NET\.

### AWSConfigs class<a name="w4aac13c25c15c11"></a>

Set as an [AWSConfigs\.AWSRegion](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property\.

```
AWSConfigs.AWSRegion = "us-west-2";
using (var ec2Client = new AmazonEC2Client())
{
  // Make request to Amazon EC2 in us-west-2 Region using ec2Client
}
```

## Special information about the China \(Beijing\) Region<a name="net-dg-region-cn-north-1"></a>

To use services in the China \(Beijing\) Region, you must have an account and credentials that are specific to the China \(Beijing\) Region\. Accounts and credentials for other AWS Regions won't work for the China \(Beijing\) Region\. Likewise, accounts and credentials for the China \(Beijing\) Region won't work for other AWS Regions\. For information about endpoints and protocols that are available in the China \(Beijing\) Region, see [China \(Beijing\) Region](https://docs.amazonaws.cn/en_us/general/latest/gr/cnnorth_region.html)\.

## Special information about new AWS services<a name="net-dg-region-new-services"></a>

New AWS services can be launched initially in a few Regions and then supported in other Regions\. In these cases you don't need to install the latest SDK to access the new Regions for that service\. You can specify newly added Regions on a per\-client basis or globally, as shown earlier\.