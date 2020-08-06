--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.

--------

# AWS Region Selection<a name="net-dg-region-selection"></a>

AWS regions allow you to access AWS services that reside physically in a specific geographic region\. This can be useful both for redundancy and to keep your data and applications running close to where you and your users will access them\. To select a particular region, configure the AWS client object with an endpoint that corresponds to that region\.

For example:

```
AmazonEC2Config config = new AmazonEC2Config();
config.ServiceURL = "https://us-west-2.amazonaws.com";
Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials("profile_name");
AmazonEC2Client ec2 = new AmazonEC2Client(credentials, config);
```

You can also specify the region using the [RegionEndpoint](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRegionEndpointNET45.html) class\. Here is an example that instantiates an Amazon EC2 client using [AWSClientFactory](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSClientFactoryNET45.html) and specifies the region:

```
Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials("profile_name");
AmazonEC2Client ec2 = AWSClientFactory.CreateAmazonEC2Client(
   credentials, RegionEndpoint.USEast1 );
```

Regions are isolated from each other\. For example, you can’t access *US East* resources when using the *EU West* region\. If your code needs access to multiple AWS regions, we recommend that you create a client specific to each region\.

Regions are logically isolated from each other; you can’t access another region’s resources when communicating with the China \(Beijing\) Region endpoint\.

Go to [Regions and Endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html) in the Amazon Web Services General Reference to view the current list of regions and corresponding endpoints for each of the services offered by AWS\.