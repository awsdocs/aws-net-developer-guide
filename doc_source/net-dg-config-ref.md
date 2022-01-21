--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Configuration Files Reference for AWS SDK for \.NET<a name="net-dg-config-ref"></a>

**Note**  
The information in this topic is specific to projects based on \.NET Framework\. The `App.config` and `Web.config` files are not present by default in projects based on \.NET Core\.

## Open to view \.NET Framework content<a name="w8aac13c29c11b5b1"></a>

You can use a \.NET project's `App.config` or `Web.config` file to specify AWS settings, such as AWS credentials, logging options, AWS service endpoints, and AWS regions, as well as some settings for AWS services, such as Amazon DynamoDB, Amazon EC2, and Amazon S3\. The following information describes how to properly format an `App.config` or `Web.config` file to specify these types of settings\.

**Note**  
Although you can continue to use the `<appSettings>` element in an `App.config` or `Web.config` file to specify AWS settings, we recommend you use the `<configSections>` and `<aws>` elements as described later in this topic\. For more information about the `<appSettings>` element, see the `<appSettings>` element examples in [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

**Note**  
Although you can continue to use the following [AWSConfigs](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) class properties in a code file to specify AWS settings, the following properties are deprecated and may not be supported in future releases:  
 `DynamoDBContextTableNamePrefix` 
 `EC2UseSignatureVersion4` 
 `LoggingOptions` 
 `LogMetrics` 
 `ResponseLoggingOption` 
 `S3UseSignatureVersion4` 
In general, we recommend that instead of using `AWSConfigs` class properties in a code file to specify AWS settings, you should use the `<configSections>` and `<aws>` elements in an `App.config` or `Web.config` file to specify AWS settings, as described later in this topic\. For more information about the preceding properties, see the `AWSConfigs` code examples in [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

**Topics**
+ [Declaring an AWS Settings Section](#net-dg-config-ref-declaring)
+ [Allowed Elements](#net-dg-config-ref-elements)
+ [Elements Reference](#net-dg-config-ref-elements-ref)

### Declaring an AWS Settings Section<a name="net-dg-config-ref-declaring"></a>

You specify AWS settings in an `App.config` or `Web.config` file from within the `<aws>` element\. Before you can begin using the `<aws>` element, you must create a `<section>` element \(which is a child element of the `<configSections>` element\) and set its `name` attribute to `aws` and its `type` attribute to `Amazon.AWSSection, AWSSDK.Core`, as shown in the following example:

```
<?xml version="1.0"?>
<configuration>
  ...
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
  </configSections>
  <aws>
    <!-- Add your desired AWS settings declarations here. -->
  </aws>
  ...
</configuration>
```

The Visual Studio Editor does not provide automatic code completion \(IntelliSense\) for the `<aws>` element or its child elements\.

To assist you in creating a correctly formatted version of the `<aws>` element, call the `Amazon.AWSConfigs.GenerateConfigTemplate` method\. This outputs a canonical version of the `<aws>` element as a pretty\-printed string, which you can adapt to your needs\. The following sections describe the `<aws>` element's attributes and child elements\.

### Allowed Elements<a name="net-dg-config-ref-elements"></a>

The following is a list of the logical relationships among the allowed elements in an AWS settings section\. You can generate the latest version of this list by calling the `Amazon.AWSConfigs.GenerateConfigTemplate` method, which outputs a canonical version of the `<aws>` element as a string you can adapt to your needs\.

```
<aws
  endpointDefinition="string value"
  region="string value"
  profileName="string value"
  profilesLocation="string value">
  <logging
    logTo="None, Log4Net, SystemDiagnostics"
    logResponses="Never | OnError | Always"
    logMetrics="true | false"
    logMetricsFormat="Standard | JSON"
    logMetricsCustomFormatter="NameSpace.Class, Assembly" />
  <dynamoDB
    conversionSchema="V1 | V2">
    <dynamoDBContext
      tableNamePrefix="string value">
      <tableAliases>
        <alias
          fromTable="string value"
          toTable="string value" />
      </tableAliases>
      <map
        type="NameSpace.Class, Assembly"
        targetTable="string value">
        <property
          name="string value"
          attribute="string value"
          ignore="true | false"
          version="true | false"
          converter="NameSpace.Class, Assembly" />
      </map>
    </dynamoDBContext>
  </dynamoDB>
  <s3
    useSignatureVersion4="true | false" />
  <ec2
    useSignatureVersion4="true | false" />
  <proxy
    host="string value"
    port="1234"
    username="string value"
    password="string value" />
</aws>
```

### Elements Reference<a name="net-dg-config-ref-elements-ref"></a>

The following is a list of the elements that are allowed in an AWS settings section\. For each element, its allowed attributes and parent\-child elements are listed\.

**Topics**
+ [alias](#net-dg-config-ref-elements-alias)
+ [aws](#net-dg-config-ref-elements-aws)
+ [dynamoDB](#net-dg-config-ref-elements-dynamodb)
+ [dynamoDBContext](#net-dg-config-ref-elements-ddbcontext)
+ [ec2](#net-dg-config-ref-elements-ec2)
+ [logging](#net-dg-config-ref-elements-logging)
+ [map](#net-dg-config-ref-elements-map)
+ [property](#net-dg-config-ref-elements-property)
+ [proxy](#net-dg-config-ref-elements-proxy)
+ [s3](#net-dg-config-ref-elements-s3)

#### alias<a name="net-dg-config-ref-elements-alias"></a>

The `<alias>` element represents a single item in a collection of one or more from\-table to to\-table mappings that specifies a different table than one configured for a type\. This element maps to an instance of the `Amazon.Util.TableAlias` class from the `Amazon.AWSConfigs.DynamoDBConfig.Context.TableAliases` property in the AWS SDK for \.NET\. Remapping is done before applying a table name prefix\.

This element can include the following attributes:

** `fromTable` **  
The from\-table portion of the from\-table to to\-table mapping\. This attribute maps to the `Amazon.Util.TableAlias.FromTable` property in the AWS SDK for \.NET\.

** `toTable` **  
The to\-table portion of the from\-table to to\-table mapping\. This attribute maps to the `Amazon.Util.TableAlias.ToTable` property in the AWS SDK for \.NET\.

The parent of the `<alias>` element is the `<tableAliases>` element\.

The `<alias>` element contains no child elements\.

The following is an example of the `<alias>` element in use:

```
<alias
  fromTable="Studio"
  toTable="Studios" />
```

#### aws<a name="net-dg-config-ref-elements-aws"></a>

The `<aws>` element represents the top\-most element in an AWS settings section\. This element can include the following attributes:

** `endpointDefinition` **  
The absolute path to a custom configuration file that defines the AWS regions and endpoints to use\. This attribute maps to the `Amazon.AWSConfigs.EndpointDefinition` property in the AWS SDK for \.NET\.

** `profileName` **  
The profile name for stored AWS credentials that will be used to make service calls\. This attribute maps to the `Amazon.AWSConfigs.AWSProfileName` property in the AWS SDK for \.NET\.

** `profilesLocation` **  
The absolute path to the location of the credentials file shared with other AWS SDKs\. By default, the credentials file is stored in the `.aws` directory in the current user's home directory\. This attribute maps to the `Amazon.AWSConfigs.AWSProfilesLocation` property in the AWS SDK for \.NET\.

** `region` **  
The default AWS region ID for clients that have not explicitly specified a region\. This attribute maps to the `Amazon.AWSConfigs.AWSRegion` property in the AWS SDK for \.NET\.

The `<aws>` element has no parent element\.

The `<aws>` element can include the following child elements:
+  `<dynamoDB>` 
+  `<ec2>` 
+  `<logging>` 
+  `<proxy>` 
+  `<s3>` 

The following is an example of the `<aws>` element in use:

```
<aws
  endpointDefinition="C:\Configs\endpoints.xml"
  region="us-west-2"
  profileName="development"
  profilesLocation="C:\Configs">
  <!-- ... -->
</aws>
```

#### dynamoDB<a name="net-dg-config-ref-elements-dynamodb"></a>

The `<dynamoDB>` element represents a collection of settings for Amazon DynamoDB\. This element can include the *conversionSchema* attribute, which represents the version to use for converting between \.NET and DynamoDB objects\. Allowed values include V1 and V2\. This attribute maps to the `Amazon.DynamoDBv2.DynamoDBEntryConversion` class in the AWS SDK for \.NET\. For more information, see [DynamoDB Series \- Conversion Schemas](http://blogs.aws.amazon.com/net/post/Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas)\.

The parent of the `<dynamoDB>` element is the `<aws>` element\.

The `<dynamoDB>` element can include the `<dynamoDBContext>` child element\.

The following is an example of the `<dynamoDB>` element in use:

```
<dynamoDB
  conversionSchema="V2">
  <!-- ... -->
</dynamoDB>
```

#### dynamoDBContext<a name="net-dg-config-ref-elements-ddbcontext"></a>

The `<dynamoDBContext>` element represents a collection of Amazon DynamoDB context\-specific settings\. This element can include the *tableNamePrefix* attribute, which represents the default table name prefix the DynamoDB context will use if it is not manually configured\. This attribute maps to the `Amazon.Util.DynamoDBContextConfig.TableNamePrefix` property from the `Amazon.AWSConfigs.DynamoDBConfig.Context.TableNamePrefix` property in the AWS SDK for \.NET\. For more information, see [Enhancements to the DynamoDB SDK](http://blogs.aws.amazon.com/net/post/Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK)\.

The parent of the `<dynamoDBContext>` element is the `<dynamoDB>` element\.

The `<dynamoDBContext>` element can include the following child elements:
+  `<alias>` \(one or more instances\)
+  `<map>` \(one or more instances\)

The following is an example of the `<dynamoDBContext>` element in use:

```
<dynamoDBContext
  tableNamePrefix="Test-">
  <!-- ... -->
</dynamoDBContext>
```

#### ec2<a name="net-dg-config-ref-elements-ec2"></a>

The `<ec2>` element represents a collection of Amazon EC2 settings\. This element can include the *useSignatureVersion4* attribute, which specifies whether signature version 4 signing will be used for all requests \(true\) or whether signature version 4 signing will not be used for all requests \(false, the default\)\. This attribute maps to the `Amazon.Util.EC2Config.UseSignatureVersion4` property from the `Amazon.AWSConfigs.EC2Config.UseSignatureVersion4` property in the AWS SDK for \.NET\.

The parent of the `<ec2>` element is the element\.

The `<ec2>` element contains no child elements\.

The following is an example of the `<ec2>` element in use:

```
<ec2
  useSignatureVersion4="true" />
```

#### logging<a name="net-dg-config-ref-elements-logging"></a>

The `<logging>` element represents a collection of settings for response logging and performance metrics logging\. This element can include the following attributes:

** `logMetrics` **  
Whether performance metrics will be logged for all clients and configurations \(true\); otherwise, false\. This attribute maps to the `Amazon.Util.LoggingConfig.LogMetrics` property from the `Amazon.AWSConfigs.LoggingConfig.LogMetrics` property in the AWS SDK for \.NET\.

** `logMetricsCustomFormatter` **  
The data type and assembly name of a custom formatter for logging metrics\. This attribute maps to the `Amazon.Util.LoggingConfig.LogMetricsCustomFormatter` property from the `Amazon.AWSConfigs.LoggingConfig.LogMetricsCustomFormatter` property in the AWS SDK for \.NET\.

** `logMetricsFormat` **  
The format in which the logging metrics are presented \(maps to the `Amazon.Util.LoggingConfig.LogMetricsFormat` property from the `Amazon.AWSConfigs.LoggingConfig.LogMetricsFormat` property in the AWS SDK for \.NET\)\.  
Allowed values include:    
** `JSON` **  
Use JSON format\.  
** `Standard` **  
Use the default format\.

** `logResponses` **  
When to log service responses \(maps to the `Amazon.Util.LoggingConfig.LogResponses` property from the `Amazon.AWSConfigs.LoggingConfig.LogResponses` property in the AWS SDK for \.NET\)\.  
Allowed values include:    
** `Always` **  
Always log service responses\.  
** `Never` **  
Never log service responses\.  
** `OnError` **  
Only log service responses when there are errors\.

** `logTo` **  
Where to log to \(maps to the `LogTo` property from the `Amazon.AWSConfigs.LoggingConfig.LogTo` property in the AWS SDK for \.NET\)\.  
Allowed values include one or more of:    
** `Log4Net` **  
Log to log4net\.  
** `None` **  
Disable logging\.  
** `SystemDiagnostics` **  
Log to System\.Diagnostics\.

The parent of the `<logging>` element is the `<aws>` element\.

The `<logging>` element contains no child elements\.

The following is an example of the `<logging>` element in use:

```
<logging
  logTo="SystemDiagnostics"
  logResponses="OnError"
  logMetrics="true"
  logMetricsFormat="JSON"
  logMetricsCustomFormatter="MyLib.Util.MyMetricsFormatter, MyLib" />
```

#### map<a name="net-dg-config-ref-elements-map"></a>

The `<map>` element represents a single item in a collection of type\-to\-table mappings from \.NET types to DynamoDB tables \(maps to an instance of the `TypeMapping` class from the `Amazon.AWSConfigs.DynamoDBConfig.Context.TypeMappings` property in the AWS SDK for \.NET\)\. For more information, see [Enhancements to the DynamoDB SDK](http://blogs.aws.amazon.com/net/post/Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK)\.

This element can include the following attributes:

** `targetTable` **  
The DynamoDB table to which the mapping applies\. This attribute maps to the `Amazon.Util.TypeMapping.TargetTable` property in the AWS SDK for \.NET\.

** `type` **  
The type and assembly name to which the mapping applies\. This attribute maps to the `Amazon.Util.TypeMapping.Type` property in the AWS SDK for \.NET\.

The parent of the `<map>` element is the `<dynamoDBContext>` element\.

The `<map>` element can include one or more instances of the `<property>` child element\.

The following is an example of the `<map>` element in use:

```
<map
  type="SampleApp.Models.Movie, SampleDLL"
  targetTable="Movies">
  <!-- ... -->
</map>
```

#### property<a name="net-dg-config-ref-elements-property"></a>

The `<property>` element represents a DynamoDB property\. \(This element maps to an instance of the Amazon\.Util\.PropertyConfig class from the `AddProperty` method in the AWS SDK for \.NET\) For more information, see [Enhancements to the DynamoDB SDK](http://blogs.aws.amazon.com/net/post/Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK) and [DynamoDB Attributes](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DeclarativeTagsList.html)\.

This element can include the following attributes:

** `attribute` **  
The name of an attribute for the property, such as the name of a range key\. This attribute maps to the `Amazon.Util.PropertyConfig.Attribute` property in the AWS SDK for \.NET\.

** `converter` **  
The type of converter that should be used for this property\. This attribute maps to the `Amazon.Util.PropertyConfig.Converter` property in the AWS SDK for \.NET\.

** `ignore` **  
Whether the associated property should be ignored \(true\); otherwise, false\. This attribute maps to the `Amazon.Util.PropertyConfig.Ignore` property in the AWS SDK for \.NET\.

** `name` **  
The name of the property\. This attribute maps to the `Amazon.Util.PropertyConfig.Name` property in the AWS SDK for \.NET\.

** `version` **  
Whether this property should store the item version number \(true\); otherwise, false\. This attribute maps to the `Amazon.Util.PropertyConfig.Version` property in the AWS SDK for \.NET\.

The parent of the `<property>` element is the `<map>` element\.

The `<property>` element contains no child elements\.

The following is an example of the `<property>` element in use:

```
<property
  name="Rating"
  converter="SampleApp.Models.RatingConverter, SampleDLL" />
```

#### proxy<a name="net-dg-config-ref-elements-proxy"></a>

The `<proxy>` element represents settings for configuring a proxy for the AWS SDK for \.NET to use\. This element can include the following attributes:

**host**  
The host name or IP address of the proxy server\. This attributes maps to the `Amazon.Util.ProxyConfig.Host` property from the `Amazon.AWSConfigs.ProxyConfig.Host` property in the AWS SDK for \.NET\.

**password**  
The password to authenticate with the proxy server\. This attributes maps to the `Amazon.Util.ProxyConfig.Password` property from the `Amazon.AWSConfigs.ProxyConfig.Password` property in the AWS SDK for \.NET\.

**port**  
The port number of the proxy\. This attributes maps to the `Amazon.Util.ProxyConfig.Port` property from the `Amazon.AWSConfigs.ProxyConfig.Port` property in the AWS SDK for \.NET\.

**username**  
The user name to authenticate with the proxy server\. This attributes maps to the `Amazon.Util.ProxyConfig.Username` property from the `Amazon.AWSConfigs.ProxyConfig.Username` property in the AWS SDK for \.NET\.

The parent of the `<proxy>` element is the `<aws>` element\.

The `<proxy>` element contains no child elements\.

The following is an example of the `<proxy>` element in use:

```
<proxy
  host="192.0.2.0"
  port="1234"
  username="My-Username-Here"
  password="My-Password-Here" />
```

#### s3<a name="net-dg-config-ref-elements-s3"></a>

The `<s3>` element represents a collection of Amazon S3 settings\. This element can include the *useSignatureVersion4* attribute, which specifies whether signature version 4 signing will be used for all requests \(true\) or whether signature version 4 signing will not be used for all requests \(false, the default\)\. This attribute maps to the `Amazon.AWSConfigs.S3Config.UseSignatureVersion4` property in the AWS SDK for \.NET\.

The parent of the `<s3>` element is the `<aws>` element\.

The `<s3>` element contains no child elements\.

The following is an example of the `<s3>` element in use:

```
<s3 useSignatureVersion4="true" />
```