--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.

--------

# Configuring Your AWS SDK for \.NET Application<a name="net-dg-config"></a>

You can configure your AWS SDK for \.NET application to specify AWS credentials, logging options, endpoints, or signature version 4 support with Amazon S3\.

The recommended way to configure an application is to use the `<aws>` element in the project’s `App.config` or `Web.config` file\. The following example specifies the [AWSRegion](net-dg-config-other.md#config-setting-awsregion) and [AWSLogging](net-dg-config-other.md#config-setting-awslogging) parameters\.

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

Another way to configure an application is to edit the `<appSettings>` element in the project’s `App.config` or `Web.config` file\. The following example specifies the [AWSRegion](net-dg-config-other.md#config-setting-awsregion) and [AWSLogging](net-dg-config-other.md#config-setting-awslogging) parameters\.

```
<configuration>
  <appSettings>
    <add key="AWSRegion" value="us-west-2"/>
    <add key="AWSLogging" value="log4net"/>
  </appSettings>
</configuration>
```

These settings take effect only after the application has been rebuilt\.

Although you can configure an AWS SDK for \.NET application programmatically by setting property values in the [AWSConfigs](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) class, we recommend you use the `aws` element instead\. The following example specifies the [AWSRegion](net-dg-config-other.md#config-setting-awsregion) and [AWSLogging](net-dg-config-other.md#config-setting-awslogging) parameters:

```
AWSConfigs.AWSRegion = "us-west-2";
AWSConfigs.Logging = LoggingOptions.Log4Net;
```

Programmatically defined parameters override any values that were specified in an `App.config` or `Web.config` file\. Some programmatically defined parameter values take effect immediately; others take effect only after you create a new client object\. For more information, see [Configuring AWS Credentials](net-dg-config-creds.md)\.

**Topics**
+ [Configuring AWS Credentials](net-dg-config-creds.md)
+ [AWS Region Selection](net-dg-region-selection.md)
+ [Configuring Other Application Parameters](net-dg-config-other.md)
+ [Configuration Files Reference for AWS SDK for \.NET](net-dg-config-ref.md)