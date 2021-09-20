--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/BannerButton.png) ](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Configuring Other Application Parameters<a name="net-dg-config-other"></a>

**Note**  
The information in this topic is specific to projects based on \.NET Framework\. The `App.config` and `Web.config` files are not present by default in projects based on \.NET Core\.

## Open to view \.NET Framework content<a name="w8aac13c29b9b5b1"></a>

In addition to [configuring credentials](net-dg-config-creds.md), you can configure a number of other application parameters:
+  `AWSLogging` 
+  `AWSLogMetrics` 
+  `AWSRegion` 
+  `AWSResponseLogging` 
+  `AWS\.DynamoDBContext\.TableNamePrefix` 
+  `AWS\.S3\.UseSignatureVersion4` 
+  `AWSEndpointDefinition` 
+  [AWS Service\-Generated Endpoints](#config-setting-service-generated-awsendpointdefinition) 

These parameters can be configured in the application’s `App.config` or `Web.config` file\. Although you can also configure these with the AWS SDK for \.NET API, we recommend you use the application’s `.config` file\. Both approaches are described here\.

For more information about use of the `<aws>` element as described later in this topic, see [Configuration Files Reference for AWS SDK for \.NET](net-dg-config-ref.md)\.

### AWSLogging<a name="config-setting-awslogging"></a>

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

Alternatively, using the AWS SDK for \.NET API, combine the values of the [LoggingOptions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TLoggingOptions.html) enumeration and set the [AWSConfigs\.Logging](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property:

```
AWSConfigs.Logging = LoggingOptions.Log4Net | LoggingOptions.SystemDiagnostics;
```

Changes to this setting take effect only for new AWS client instances\.

### AWSLogMetrics<a name="config-setting-awslogmetrics"></a>

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

Alternatively, to set metrics logging with the AWS SDK for \.NET API, set the [AWSConfigs\.LogMetrics](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property:

```
AWSConfigs.LogMetrics = true;
```

This setting configures the default `LogMetrics` property for all clients/configs\. Changes to this setting take effect only for new AWS client instances\.

### AWSRegion<a name="config-setting-awsregion"></a>

Configures the default AWS region for clients that have not explicitly specified a region\. To set the region in the `.config` file, the recommended approach is to set the `region` attribute value in the `aws` element:

```
<aws region="us-west-2"/>
```

Alternatively, set the *AWSRegion* key in the `<appSettings>` section:

```
<add key="AWSRegion" value="us-west-2"/>
```

Alternatively, to set the region with the AWS SDK for \.NET API, set the [AWSConfigs\.AWSRegion](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property:

```
AWSConfigs.AWSRegion = "us-west-2";
```

For more information about creating an AWS client for a specific region, see [AWS Region Selection](net-dg-region-selection.md)\. Changes to this setting take effect only for new AWS client instances\.

### AWSResponseLogging<a name="config-setting-awsresponselogging"></a>

Configures when the SDK should log service responses\. The possible values are:

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

Alternatively, to set service logging with the AWS SDK for \.NET API, set the [AWSConfigs\.ResponseLogging](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property to one of the values of the [ResponseLoggingOption](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TResponseLoggingOption.html) enumeration:

```
AWSConfigs.ResponseLogging = ResponseLoggingOption.OnError;
```

Changes to this setting take effect immediately\.

### `AWS.DynamoDBContext.TableNamePrefix`<a name="config-setting-aws-dynamodbcontext-tablenameprefix"></a>

Configures the default `TableNamePrefix` the `DynamoDBContext` will use if not manually configured\.

To set the table name prefix in the `.config` file, the recommended approach is to set the `tableNamePrefix` attribute value in the `<dynamoDBContext>` element, which is a child element of the `<dynamoDB>` element, which itself is a child element of the `<aws>` element:

```
<dynamoDBContext tableNamePrefix="Test-"/>
```

Alternatively, set the `AWS.DynamoDBContext.TableNamePrefix` key in the `<appSettings>` section:

```
<add key="AWS.DynamoDBContext.TableNamePrefix" value="Test-"/>
```

Alternatively, to set the table name prefix with the AWS SDK for \.NET API, set the [AWSConfigs\.DynamoDBContextTableNamePrefix](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property:

```
AWSConfigs.DynamoDBContextTableNamePrefix = "Test-";
```

Changes to this setting will take effect only in newly constructed instances of `DynamoDBContextConfig` and `DynamoDBContext`\.

### `AWS.S3.UseSignatureVersion4`<a name="config-setting-aws-s3-usesignatureversion4"></a>

Configures whether or not the Amazon S3 client should use signature version 4 signing with requests\.

To set signature version 4 signing for Amazon S3 in the `.config` file, the recommended approach is to set the `useSignatureVersion4` attribute of the `<s3>` element, which is a child element of the `<aws>` element:

```
<aws>
  <s3 useSignatureVersion4="true"/>
</aws>
```

Alternatively, set the `AWS.S3.UseSignatureVersion4` key to `true` in the `<appSettings>` section:

```
<add key="AWS.S3.UseSignatureVersion4" value="true"/>
```

Alternatively, to set signature version 4 signing with the AWS SDK for \.NET API, set the [AWSConfigs\.S3UseSignatureVersion4](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property to `true`:

```
AWSConfigs.S3UseSignatureVersion4 = true;
```

By default, this setting is `false`, but signature version 4 may be used by default in some cases or with some regions\. When the setting is `true`, signature version 4 will be used for all requests\. Changes to this setting take effect only for new Amazon S3 client instances\.

### AWSEndpointDefinition<a name="config-setting-awsendpointdefinition"></a>

Configures whether the SDK should use a custom configuration file that defines the regions and endpoints\.

To set the endpoint definition file in the `.config` file, we recommend setting the `endpointDefinition` attribute value in the `<aws>` element\.

```
<aws endpointDefinition="c:\config\endpoints.json"/>
```

Alternatively, you can set the *AWSEndpointDefinition* key in the `<appSettings>` section:

```
<add key="AWSEndpointDefinition" value="c:\config\endpoints.json"/>
```

Alternatively, to set the endpoint definition file with the AWS SDK for \.NET API, set the [AWSConfigs\.EndpointDefinition](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TAWSConfigs.html) property:

```
AWSConfigs.EndpointDefinition = @"c:\config\endpoints.json";
```

If no file name is provided, then a custom configuration file will not be used\. Changes to this setting take effect only for new AWS client instances\. The endpoint\.json file is available from [https://github.com/aws/aws-sdk-net/blob/master/sdk/src/Core/endpoints.json](https://github.com/aws/aws-sdk-net/blob/master/sdk/src/Core/endpoints.json)\.

### AWS Service\-Generated Endpoints<a name="config-setting-service-generated-awsendpointdefinition"></a>

Some AWS services generate their own endpoints instead of consuming a region endpoint\. Clients for these services consume a service Url that is specific to that service and your resources\. Two examples of these services are Amazon CloudSearch and AWS IoT\. The following examples show how you can obtain the endpoints for those services\.

#### Amazon CloudSearch Endpoints Example<a name="cs-endpoints-example"></a>

The Amazon CloudSearch client is used for accessing the Amazon CloudSearch configuration service\. You use the Amazon CloudSearch configuration service to create, configure, and manage search domains\. To create a search domain, create a [CreateDomainRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearch/TCreateDomainRequest.html) object and provide the `DomainName` property\. Create an [AmazonCloudSearchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearch/TCloudSearchClient.html) object by using the request object\. Call the [CreateDomain](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearch/MCloudSearchCreateDomainCreateDomainRequest.html) method\. The [CreateDomainResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearch/TCreateDomainResponse.html) object returned from the call contains a `DomainStatus` property that has both the `DocService` and `SearchService` endpoints\. Create an [AmazonCloudSearchDomainConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearchDomain/TCloudSearchDomainConfig.html) object and use it to initialize `DocService` and `SearchService` instances of the [AmazonCloudSearchDomainClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudSearchDomain/TCloudSearchDomainClient.html) class\.

```
// Create domain and retrieve DocService and SearchService endpoints
DomainStatus domainStatus;
using (var searchClient = new AmazonCloudSearchClient())
{
    var request = new CreateDomainRequest
    {
        DomainName = "testdomain"
    };
    domainStatus = searchClient.CreateDomain(request).DomainStatus;
    Console.WriteLine(domainStatus.DomainName + " created");
}

// Test the DocService endpoint
var docServiceConfig = new AmazonCloudSearchDomainConfig
{
    ServiceURL = "https://" + domainStatus.DocService.Endpoint
};
using (var domainDocService = new AmazonCloudSearchDomainClient(docServiceConfig))
{
    Console.WriteLine("Amazon CloudSearchDomain DocService client instantiated using the DocService endpoint");
    Console.WriteLine("DocService endpoint = " + domainStatus.DocService.Endpoint);

    using (var docStream = new FileStream(@"C:\doc_source\XMLFile4.xml", FileMode.Open))
    {
        var upload = new UploadDocumentsRequest
        {
            ContentType = ContentType.ApplicationXml,
            Documents = docStream
        };
        domainDocService.UploadDocuments(upload);
    }
}

// Test the SearchService endpoint
var searchServiceConfig = new AmazonCloudSearchDomainConfig
{
    ServiceURL = "https://" + domainStatus.SearchService.Endpoint
};
using (var domainSearchService = new AmazonCloudSearchDomainClient(searchServiceConfig))
{
    Console.WriteLine("Amazon CloudSearchDomain SearchService client instantiated using the SearchService endpoint");
    Console.WriteLine("SearchService endpoint = " + domainStatus.SearchService.Endpoint);

    var searchReq = new SearchRequest
    {
        Query = "Gambardella",
        Sort = "_score desc",
        QueryParser = QueryParser.Simple
    };
    var searchResp = domainSearchService.Search(searchReq);
}
```

#### AWS IoT Endpoints Example<a name="iotlong-endpoints-example"></a>

To obtain the endpoint for AWS IoT, create an [AmazonIoTClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IoT/TIoTClient.html) object and call the [DescribeEndPoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IoT/MIoTDescribeEndpointDescribeEndpointRequest.html) method\. The returned [DescribeEndPointResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IoT/TDescribeEndpointResponse.html) object contains the `EndpointAddress`\. Create an [AmazonIotDataConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IotData/TIotDataConfig.html) object, set the `ServiceURL` property, and use the object to instantiate the [AmazonIotDataClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IotData/TIotDataClient.html) class\.

```
string iotEndpointAddress;
using (var iotClient = new AmazonIoTClient())
{
    var endPointResponse = iotClient.DescribeEndpoint();
    iotEndpointAddress = endPointResponse.EndpointAddress;
}

var ioTdocServiceConfig = new AmazonIotDataConfig
{
    ServiceURL = "https://" + iotEndpointAddress
};
using (var dataClient = new AmazonIotDataClient(ioTdocServiceConfig))
{
    Console.WriteLine("AWS IoTData client instantiated using the endpoint from the IotClient");
}nstantiated using the endpoint from the IoT client");
```