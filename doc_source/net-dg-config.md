--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Configuring Your AWS SDK for \.NET Application<a name="net-dg-config"></a>

You can configure your AWS SDK for \.NET application to specify logging options, endpoints, or signature version 4 support with Amazon S3\.

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

Although you can configure an AWS SDK for \.NET application programmatically by setting property values in the [AWSConfigs](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) class, we recommend you use the `aws` element instead\. The following example specifies the [AWSRegion](net-dg-config-other.md#config-setting-awsregion) and [AWSLogging](net-dg-config-other.md#config-setting-awslogging) parameters:

```
AWSConfigs.AWSRegion = "us-west-2";
AWSConfigs.Logging = LoggingOptions.Log4Net;
```

Programmatically defined parameters override any values that were specified in an `App.config` or `Web.config` file\. Some programmatically defined parameter values take effect immediately; others take effect only after you create a new client object\.

**Topics**
+ [Configuring the AWS SDK for \.NET with \.NET Core](net-dg-config-netcore.md)
+ [Configuring AWS Credentials](net-dg-config-creds.md)
+ [AWS Region Selection](net-dg-region-selection.md)
+ [Configuring Other Application Parameters](net-dg-config-other.md)
+ [Configuration Files Reference for AWS SDK for \.NET](net-dg-config-ref.md)
+ [Enabling SDK Metrics](sdk-metrics.md)