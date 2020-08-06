--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.

--------

# Configuring Other Application Parameters<a name="net-dg-config-other"></a>

In addition to [configuring credentials](net-dg-config-creds.md), you can configure a number of other application parameters:

**Topics**
+ [**AWSEndpointDefinition**](#config-setting-awsendpointdefinition)
+ [**AWSLogging**](#config-setting-awslogging)
+ [**AWSLogMetrics**](#config-setting-awslogmetrics)
+ [**AWSRegion**](#config-setting-awsregion)
+ [**AWSResponseLogging**](#config-setting-awsresponselogging)
+ [**AWS\.DynamoDBContext\.TableNamePrefix**](#config-setting-aws-dynamodbcontext-tablenameprefix)
+ [**AWS\.S3\.UseSignatureVersion4**](#config-setting-aws-s3-usesignatureversion4)

These parameters can be configured in the application’s `App.config` or `Web.config` file\. Although you can also configure these with the AWS SDK for \.NET API, we recommend you use the application’s `.config` file\. Both approaches are described here\.

For more information about use of the `<aws>` element as described later in this topic, see [Configuration Files Reference for AWS SDK for \.NET](net-dg-config-ref.md)\.

## **AWSEndpointDefinition**<a name="config-setting-awsendpointdefinition"></a>

Configures whether the SDK should use a custom configuration file that defines the regions and endpoints\. To set the endpoint definition file in the `.config` file, we recommend setting the `endpointDefinition` attribute value in the `<aws>` element\.

```
<aws endpointDefinition="c:\config\endpoints.xml"/>
```

Alternatively, you can set the *AWSEndpointDefinition* key in the `<appSettings>` section:

```
<add key="AWSEndpointDefinition" value="c:\config\endpoints.xml"/>
```

Alternatively, to set the endpoint definition file with the AWS SDK for \.NET API, set the [AWSConfigs\.EndpointDefinition](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property:

```
AWSConfigs.EndpointDefinition = @"c:\config\endpoints.xml";
```

If no file name is provided, then a custom configuration file will not be used\. Changes to this setting take effect only for new AWS client instances\.

## **AWSLogging**<a name="config-setting-awslogging"></a>

Configures how the SDK should log events, if at all\. For example, the recommended approach is to use the `<logging>` element, which is a child element of the `<aws>` element:

```
<aws>
  <logging logTo="Log4Net"/>
</aws>
```

Alternatively:

```
<add key="AWSLogging" value="log4net"/>
```

The possible values are:

** `None` **  
Turn off event logging\. This is the default\.

** `log4net` **  
Log using log4net\.

** `SystemDiagnostics` **  
Log using the `System.Diagnostics` class\.

You can set multiple values for the `logTo` attribute, separated by commas\. The following example sets both `log4net` and `System.Diagnostics` logging in the `.config` file:

```
<logging logTo="Log4Net, SystemDiagnostics"/>
```

Alternatively:

```
<add key="AWSLogging" value="log4net, SystemDiagnostics"/>
```

Alternatively, using the AWS SDK for \.NET API, combine the values of the [LoggingOptions](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TLoggingOptionsNET45.html) enumeration and set the [AWSConfigs\.Logging](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property:

```
AWSConfigs.Logging = LoggingOptions.Log4Net | LoggingOptions.SystemDiagnostics;
```

Changes to this setting take effect only for new AWS client instances\.

## **AWSLogMetrics**<a name="config-setting-awslogmetrics"></a>

Specifies whether or not the SDK should log performance metrics\. To set the metrics logging configuration in the `.config` file, set the `logMetrics` attribute value in the `<logging>` element, which is a child element of the `<aws>` element:

```
<aws>
  <logging logMetrics="true"/>
</aws>
```

Alternatively, set the `AWSLogMetrics` key in the `<appSettings>` section:

```
<add key="AWSLogMetrics" value="true">
```

Alternatively, to set metrics logging with the AWS SDK for \.NET API, set the [AWSConfigs\.LogMetrics](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property:

```
AWSConfigs.LogMetrics = true;
```

This setting configures the default `LogMetrics` property for all clients/configs\. Changes to this setting take effect only for new AWS client instances\.

## **AWSRegion**<a name="config-setting-awsregion"></a>

Configures the default AWS region for clients that have not explicitly specified a region\. To set the region in the `.config` file, the recommended approach is to set the `region` attribute value in the `aws` element:

```
<aws region="us-west-2"/>
```

Alternatively, set the *AWSRegion* key in the `<appSettings>` section:

```
<add key="AWSRegion" value="us-west-2"/>
```

lternatively, to set the region with the AWS SDK for \.NET API, set the [AWSConfigs\.AWSRegion](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property:

```
AWSConfigs.AWSRegion = "us-west-2";
```

For more information about creating an AWS client for a specific region, see [AWS Region Selection](net-dg-region-selection.md)\. Changes to this setting take effect only for new AWS client instances\.

## **AWSResponseLogging**<a name="config-setting-awsresponselogging"></a>

Configures when the SDK should log service responses\.

The possible values are:

** `Never` **  
Never log service responses\. This is the default\.

** `Always` **  
Always log service responses\.

** `OnError` **  
Only log service responses when an error occurs\.

To set the service logging configuration in the `.config` file, the recommended approach is to set the `logResponses` attribute value in the `<logging>` element, which is a child element of the `<aws>` element:

```
<aws>
  <logging logResponses="OnError"/>
</aws>
```

Alternatively, set the *AWSResponseLogging* key in the `<appSettings>` section:

```
<add key="AWSResponseLogging" value="OnError"/>
```

Alternatively, to set service logging with the AWS SDK for \.NET API, set the [AWSConfigs\.ResponseLogging](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property to one of the values of the [ResponseLoggingOption](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TResponseLoggingOptionNET45.html) enumeration:

```
AWSConfigs.ResponseLogging = ResponseLoggingOption.OnError;
```

Changes to this setting take effect immediately\.

## **AWS\.DynamoDBContext\.TableNamePrefix**<a name="config-setting-aws-dynamodbcontext-tablenameprefix"></a>

Configures the default `TableNamePrefix` the `DynamoDBContext` will use if not manually configured\. To set the table name prefix in the `.config` file, the recommended approach is to set the `tableNamePrefix` attribute value in the `<dynamoDBContext>` element, which is a child element of the `<dynamoDB>` element, which itself is a child element of the `<aws>` element:

```
<dynamoDBContext tableNamePrefix="Test-"/>
```

Alternatively, set the `AWS.DynamoDBContext.TableNamePrefix` key in the `<appSettings>` section:

```
<add key="AWS.DynamoDBContext.TableNamePrefix" value="Test-"/>
```

```
AWSConfigs.DynamoDBContextTableNamePrefix = "Test-";
```

Changes to this setting will take effect only in newly constructed instances of `DynamoDBContextConfig` and `DynamoDBContext`\.

## **AWS\.S3\.UseSignatureVersion4**<a name="config-setting-aws-s3-usesignatureversion4"></a>

Configures whether or not the Amazon S3 client should use signature version 4 signing with requests\. To set signature version 4 signing for Amazon S3 in the `.config` file, the recommended approach is to set the `useSignatureVersion4` attribute of the `<s3>` element, which is a child element of the `<aws>` element:

```
<aws>
  <s3 useSignatureVersion4="true"/>
</aws>
```

Alternatively, set the *AWS\.S3\.UseSignatureVersion4* key to *true* in the `<appSettings>` section:

```
<add key="AWS.S3.UseSignatureVersion4" value="true"/>
```

Alternatively, to set signature version 4 signing with the AWS SDK for \.NET API, set the [AWSConfigs\.S3UseSignatureVersion4](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TAWSConfigsNET45.html) property to `true`:

```
AWSConfigs.S3UseSignatureVersion4 = true;
```

By default, this setting is `false`, but signature version 4 may be used by default in some cases or with some regions\. When the setting is `true`, signature version 4 will be used for all requests\. Changes to this setting take effect only for new Amazon S3 client instances\.