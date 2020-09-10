--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Using AWSSDK\.Extensions\.NETCore\.Setup and the IConfiguration interface<a name="net-dg-config-netcore"></a>

\(This topic was formerly titled, "Configuring the AWS SDK for \.NET with \.NET Core"\)

One of the biggest changes in \.NET Core is the removal of `ConfigurationManager` and the standard `app.config` and `web.config` files that were used with \.NET Framework and ASP\.NET applications\.

Configuration in \.NET Core is based on key\-value pairs established by configuration providers\. Configuration providers read configuration data into key\-value pairs from a variety of configuration sources, including command\-line arguments, directory files, environment variables, and settings files\.

**Note**  
For further information, see [Configuration in ASP\.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)\.

To make it easy to use the AWS SDK for \.NET with \.NET Core, you can use the [AWSSDK\.Extensions\.NETCore\.Setup](https://www.nuget.org/packages/AWSSDK.Extensions.NETCore.Setup/) NuGet package\. Like many \.NET Core libraries, it adds extension methods to the `IConfiguration` interface to make getting the AWS configuration seamless\.

## Using AWSSDK\.Extensions\.NETCore\.Setup<a name="net-core-configuration-builder"></a>

Suppose that you create an ASP\.NET Core Model\-View\-Controller \(MVC\) application, which can be accomplished with the **ASP\.NET Core Web Application** template in Visual Studio or by running `dotnet new mvc ...` in the \.NET Core CLI\. When you create such an application, the constructor for `Startup.cs` handles configuration by reading in various input sources from configuration providers such as `appsettings.json`\.

```
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}
```

To use the `Configuration` object to get the *AWS* options, first add the `AWSSDK.Extensions.NETCore.Setup` NuGet package\. Then, add your options to the configuration file as described next\.

Notice that one of the files added to your project is `appsettings.Development.json`\. This corresponds to an `EnvironmentName` set to **Development**\. During development, you put your configuration in this file, which is only read during local testing\. When you deploy an Amazon EC2 instance that has `EnvironmentName` set to **Production**, this file is ignored and the AWS SDK for \.NET falls back to the IAM credentials and Region that are configured for the Amazon EC2 instance\.

The following configuration settings show examples of the values you can add in the `appsettings.Development.json` file in your project to supply AWS settings\.

```
{
  "AWS": {
    "Profile": "local-test-profile",
    "Region": "us-west-2"
  },
  "SupportEmail": "TechSupport@example.com"
}
```

To access a setting in a *CSHTML* file, use the `Configuration` directive\.

```
@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration

<h1>Contact</h1>

<p>
    <strong>Support:</strong> <a href='mailto:@Configuration["SupportEmail"]'>@Configuration["SupportEmail"]</a><br />
</p>
```

To access the AWS options set in the file from code, call the `GetAWSOptions` extension method added to `IConfiguration`\.

To construct a service client from these options, call `CreateServiceClient`\. The following example shows how to create an Amazon S3 service client\. \(Be sure to add the [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3) NuGet package to your project\.\)

```
var options = Configuration.GetAWSOptions();
IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();
```

You can also create multiple service clients with incompatible settings by using multiple entries in the `appsettings.Development.json` file, as shown in the following examples where the configuration for `service1` includes the `us-west-2` Region and the configuration for `service2` includes the special endpoint *URL*\.

```
{
  "service1": {
    "Profile": "default",
    "Region": "us-west-2"
  },
  "service2": {
    "Profile": "default",
    "ServiceURL": "URL"
  }
}
```

You can then get the options for a specific service by using the entry in the JSON file\. For example, to get the settings for `service1` use the following\.

```
var options = Configuration.GetAWSOptions("service1");
```

### Allowed values in appsettings file<a name="net-core-appsettings-values"></a>

The following app configuration values can be set in the `appsettings.Development.json` file\. The field names must use the casing shown\. For details on these settings, see the [AWS\.Runtime\.ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class\.
+ Region
+ Profile
+ ProfilesLocation
+ SignatureVersion
+ RegionEndpoint
+ UseHttp
+ ServiceURL
+ AuthenticationRegion
+ AuthenticationServiceName
+ MaxErrorRetry
+ LogResponse
+ BufferSize
+ ProgressUpdateInterval
+ ResignRetries
+ AllowAutoRedirect
+ LogMetrics
+ DisableLogging
+ UseDualstackEndpoint

## ASP\.NET Core dependency injection<a name="net-core-dependency-injection"></a>

The *AWSSDK\.Extensions\.NETCore\.Setup* NuGet package also integrates with a new dependency injection system in ASP\.NET Core\. The `ConfigureServices` method in your application's `Startup` class is where the MVC services are added\. If the application is using Entity Framework, this is also where that is initialized\.

```
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();
}
```

**Note**  
Background on dependency injection in \.NET Core is available on the [\.NET Core documentation site](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)\.

The `AWSSDK.Extensions.NETCore.Setup` NuGet package adds new extension methods to `IServiceCollection` that you can use to add AWS services to the dependency injection\. The following code shows you how to add the AWS options that are read from `IConfiguration` to add Amazon S3 and DynamoDB to the list of services\. \(Be sure to add the [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3) and [AWSSDK\.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2) NuGet packages to your project\.\)

```
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();

    services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
    services.AddAWSService<IAmazonS3>();
    services.AddAWSService<IAmazonDynamoDB>();
}
```

Now, if your MVC controllers use either `IAmazonS3` or `IAmazonDynamoDB` as parameters in their constructors, the dependency injection system passes in those services\.

```
public class HomeController : Controller
{
    IAmazonS3 S3Client { get; set; }

    public HomeController(IAmazonS3 s3Client)
    {
        this.S3Client = s3Client;
    }

    ...

}
```