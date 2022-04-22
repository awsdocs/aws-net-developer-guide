--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Migrating to Version 3 of the AWS SDK for \.NET<a name="migration-v3"></a>

This topic describes changes in version 3 of the AWS SDK for \.NET and how to migrate your code to this version of the SDK\.

## About the AWS SDK for \.NET Versions<a name="net-dg-migrate-v3-intro"></a>

The AWS SDK for \.NET, originally released in November 2009, was designed for \.NET Framework 2\.0\. Since that release, \.NET has improved with \.NET Framework 4\.0 and \.NET Framework 4\.5, and added new target platforms: WinRT and Windows Phone\.

AWS SDK for \.NET version 2 was updated to take advantage of the new features of the \.NET platform and to target WinRT and Windows Phone\.

AWS SDK for \.NET version 3 has been updated to make the assemblies modular\.

## Architecture Redesign for the SDK<a name="net-dg-migrate-v3-arch"></a>

The entire version 3 of the AWS SDK for \.NET is redesigned to be modular\. Each service is now implemented in its own assembly, instead of in one global assembly\. You no longer have to add the entire AWS SDK for \.NET to your application\. You can now add assemblies only for the AWS services your application uses\.

## Breaking Changes<a name="net-dg-migrate-v3-breaking"></a>

The following sections describe changes to version 3 of the AWS SDK for \.NET\.

### AWSClientFactory Removed<a name="awsclientfactory-removed"></a>

The `Amazon.AWSClientFactory` class was removed\. Now, to create a service client, use the constructor of the service client\. For example, to create an `AmazonEC2Client`:

```
var ec2Client = new Amazon.EC2.AmazonEC2Client();
```

### Amazon\.Runtime\.AssumeRoleAWSCredentials Removed<a name="assumeroleawscredentials-removed"></a>

The `Amazon.Runtime.AssumeRoleAWSCredentials` class was removed because it was in a core namespace but had a dependency on the AWS Security Token Service, and because it has been obsolete in the SDK for some time\. Use the `Amazon.SecurityToken.AssumeRoleAWSCredentials` class instead\.

### SetACL Method Removed from S3Link<a name="setacl-removed"></a>

The `S3Link` class is part of the `Amazon.DynamoDBv2` package and is used for storing objects in Amazon S3 that are references in a DynamoDB item\. This is a useful feature, but we didn’t want to create a compile dependency on the `Amazon.S3` package for DynamoDB\. Consequently, we simplified the exposed `Amazon.S3` methods from the `S3Link` class, replacing the `SetACL` method with the `MakeS3ObjectPublic` method\. For more control over the access control list \(ACL\) on the object, use the `Amazon.S3` package directly\.

### Removal of Obsolete Result Classes<a name="result-classes-removed"></a>

For most services in the AWS SDK for \.NET, operations return a response object that contains metadata for the operation, such as the request ID and a result object\. Having a separate response and result class was redundant and created extra typing for developers\. In version 2 of the AWS SDK for \.NET, we put all the information in the result class into the response class\. We also marked the result classes obsolete to discourage their use\. In version 3 of the AWS SDK for \.NET, we removed these obsolete result classes to help reduce the SDK’s size\.

### AWS Config Section Changes<a name="configs-changes"></a>

It is possible to do advanced configuration of the AWS SDK for \.NET through the `App.config` or `Web.config` file\. You do this through an `<aws>` config section like the following, which references the SDK assembly name\.

```
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
  </configSections>
  <aws region="us-west-2">
    <logging logTo="Log4Net"/>
  </aws>
</configuration>
```

In version 3 of the AWS SDK for \.NET, the `AWSSDK` assembly no longer exists\. We put the common code into the `AWSSDK.Core` assembly\. As a result, you will need to change the references to the `AWSSDK` assembly in your `App.config` or `Web.config` file to the `AWSSDK.Core` assembly, as follows\.

```
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
  </configSections>
  <aws region="us-west-2">
    <logging logTo="Log4Net"/>
  </aws>
</configuration>
```

You can also manipulate the config settings with the `Amazon.AWSConfigs` class\. In version 3 of the AWS SDK for \.NET, we moved the config settings for DynamoDB from the `Amazon.AWSConfigs` class to the `Amazon.AWSConfigsDynamoDB` class\.