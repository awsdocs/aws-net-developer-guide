# Configuring the AWS SDK for \.NET with \.NET Core<a name="net-dg-config-netcore"></a>

One of the biggest changes in \.NET Core is the removal of `ConfigurationManager` and the standard `app.config` and `web.config` files that were used with \.NET Framework and ASP\.NET applications\.

Configuration in \.NET Core is based on key\-value pairs established by configuration providers\. Configuration providers read configuration data into key\-value pairs from a variety of configuration sources, including command\-line arguments, directory files, environment variables, and settings files\.

**Note**  
For further information, see [Configuration in ASP\.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)\.

To make it easy to use the AWS SDK for \.NET with \.NET Core, you can use the [AWSSDK\.Extensions\.NETCore\.Setup](https://www.nuget.org/packages/AWSSDK.Extensions.NETCore.Setup/) NuGet package\. Like many \.NET Core libraries, it adds extension methods to the `IConfiguration` interface to make getting the AWS configuration seamless\.

## Using AWSSDK\.Extensions\.NETCore\.Setup<a name="net-core-configuration-builder"></a>

When you create an ASP\.NET Core MVC application in Visual Studio, the constructor for `Startup.cs` handles configuration by reading in various input sources from configuration providers, such as reading *appsettings\.json*\.

```
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}
```

To use the `Configuration` object to get the AWS options, first add the `AWSSDK.Extensions.NETCore.Setup` NuGet package\. Then, add your options to the configuration file\. Notice one of the files added to the `ConfigurationBuilder` is called `$"appsettings.{env.EnvironmentName}.json"`\. If you look at the Debug tab in your project’s properties, you can see this file is set to **Development**\. This works great for local testing because you can put your configuration in the `appsettings.Development.json` file, which is read\-only during local testing\. When you deploy an Amazon EC2 instance that has `EnvironmentName` set to **Production**, this file is ignored and the AWS SDK for \.NET falls back to the IAM credentials and region configured for the Amazon EC2 instance\.

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

To access a setting in an *CSHTML* file, use the `Configuration` directive:

```
@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration

<h1>Contact</h1>

<p>
    <strong>Support:</strong> <a href='mailto:@Configuration["SupportEmail"]'>@Configuration["SupportEmail"]</a><br />
</p>
```

To access the AWS options set in the file from code, call the `GetAWSOptions` extension method added on `IConfiguration`\.

To construct a service client from these options, call `CreateServiceClient`\. The following example code shows how to create an Amazon S3 service client\. \(Be sure to add the [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3) NuGet package to your project\.\)

```
var options = Configuration.GetAWSOptions();
IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();
```

You can also create multiple service clients with incompatible settings using multiple entries in the `appsettings.Development.json` file, as shown in the following examples where the configuration for `service1` includes the `us-west-2` Region and the configuration for `service2` includes the special endpoint *URL*\.

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

You can then get the options for a specific service by using the entry in the JSON file\. For example, to get the settings for `service1`:

```
var options = Configuration.GetAWSOptions("service1");
```

### Allowed Values in appsettings File<a name="net-core-appsettings-values"></a>

The following app configuration values can be set in the `appsettings.Development.json` file\. The field names must use the casing shown in the list below\. For details on these settings, refer to the [AWS\.Runtime\.ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class\.
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

## ASP\.NET Core Dependency Injection<a name="net-core-dependency-injection"></a>

The *AWSSDK\.Extensions\.NETCore\.Setup* NuGet package also integrates with a new dependency injection system in ASP\.NET Core\. The `ConfigureServices` method in `Startup` is where the MVC services are added\. If the application is using Entity Framework, this is also where that is initialized\.

```
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();
}
```

**Note**  
Background on dependency injection in \.NET Core is available on the [\.NET Core documentation site](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)\.

The `AWSSDK.Extensions.NETCore.Setup` NuGet package adds new extension methods to `IServiceCollection` that you can use to add AWS services to the dependency injection\. The following code shows how to add the AWS options that are read from `IConfiguration` to add Amazon S3 and DynamoDB to our list of services\. \(Be sure to add the [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3) and [AWSSDK\.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2) NuGet packages to your project\.\)

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