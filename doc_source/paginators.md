--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Paginators<a name="paginators"></a>

Some AWS services collect and store a large amount of data, which you can retrieve by using the API calls of the AWS SDK for \.NET\. If the amount of data you want to retrieve becomes too large for a single API call, you can break the results into more manageable pieces through the use of *pagination*\. 

To enable you to perform pagination, the request and response objects for many service clients in the SDK provide a *continuation token* \(typically named `NextToken`\)\. Some of these service clients also provide **paginators**\.

Paginators enable you to avoid the overhead of the continuation token, which might involve loops, state variables, multiple API calls, and so on\. When you use a paginator, you can retrieve data from an AWS service through a single line of code, a `foreach` loop's declaration\. If multiple API calls are needed to retrieve the data, the paginator handles this for you\.

## Where do I find paginators?<a name="paginators-find-them"></a>

Not all services provide paginators\. One way to determine whether a service provides a paginator for a particular API is to look at the definition of a service client class in the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/)\.

For example, if you examine the definition for the [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TCloudWatchLogsClient.html) class, you see a `Paginators` property\. This is the property that provides a paginator for Amazon CloudWatch Logs\.

## What do paginators give me?<a name="paginators-use-them"></a>

Paginators contain properties that enable you to see full responses\. They also typically contain one or more properties that enable to you access the most interesting portions of the responses, which we will call the *key results*\.

For example, in the `AmazonCloudWatchLogsClient` mentioned earlier, the `Paginator` object contains a `Responses` property with the full [DescribeLogGroupsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TDescribeLogGroupsResponse.html) object from the API call\. This `Responses` property contains, among other things, a collection of the log groups\.

The Paginator object also contains one key result named `LogGroups`\. This property holds just the log groups portion of the response\. Having this key result enables you to reduce and simplify your code in many circumstances\.

## Synchronous vs\. asynchronous pagination<a name="paginators-sync-async"></a>

Paginators provide both synchronous and asynchronous mechanisms for pagination\. Synchronous pagination is available in \.NET Framework 4\.5 \(or later\) projects\. Asynchronous pagination is available in \.NET Core projects\.

Because asynchronous operations and \.NET Core are recommended, the example that comes next shows you asynchronous pagination\. Information about how to perform the same tasks using synchronous pagination and \.NET Framework 4\.5 \(or later\) is shown after the example in [Additional considerations for paginators](#paginators-additional)\.

## Example<a name="paginators-example"></a>

The following example shows you how to use the AWS SDK for \.NET to display a list of log groups\. For contrast, the example shows how to do this both with and without paginators\. Before looking at the full code, shown later, consider the following snippets\.

**Getting CloudWatch log groups without paginators**

```
      // Loop as many times as needed to get all the log groups
      var request = new DescribeLogGroupsRequest{Limit = LogGroupLimit};
      do
      {
        Console.WriteLine($"Getting up to {LogGroupLimit} log groups...");
        var response = await cwClient.DescribeLogGroupsAsync(request);
        foreach(var logGroup in response.LogGroups)
        {
          Console.WriteLine($"{logGroup.LogGroupName}");
        }
        request.NextToken = response.NextToken;
      } while(!string.IsNullOrEmpty(request.NextToken));
```

**Getting CloudWatch log groups by using paginators**

```
      // No need to loop to get all the log groups--the SDK does it for us behind the scenes
      var paginatorForLogGroups =
        cwClient.Paginators.DescribeLogGroups(new DescribeLogGroupsRequest());
      await foreach(var logGroup in paginatorForLogGroups.LogGroups)
      {
        Console.WriteLine(logGroup.LogGroupName);
      }
```

The results of these two snippets are exactly the same, so the advantage in using paginators can clearly be seen\.

**Note**  
Before you try to build and run the full code, be sure you have [set up your environment](net-dg-setup.md)\. Also review the information in [Setting up your project](net-dg-config.md)\.  
You might also need the [Microsoft\.Bcl\.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/) NuGet package because asynchronous paginators use the `IAsyncEnumerable` interface\.

### Complete code<a name="paginators-complete-code"></a>

This section shows relevant references and the complete code for this example\.

#### SDK references<a name="w4aac15c17c23c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.CloudWatch](https://www.nuget.org/packages/AWSSDK.CloudWatch)

Programming elements:
+ Namespace [Amazon\.CloudWatch](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/NCloudWatch.html)

  Class [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TCloudWatchLogsClient.html)
+ Namespace [Amazon\.CloudWatchLogs\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/NCloudWatchLogsModel.html)

  Class [DescribeLogGroupsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TDescribeLogGroupsRequest.html)

  Class [DescribeLogGroupsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TDescribeLogGroupsResponse.html)

  Class [LogGroup](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TLogGroup.html)

#### Full code<a name="w4aac15c17c23c19b7b1"></a>

```
using System;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;

namespace CWGetLogGroups
{
  class Program
  {
    // A small limit for demonstration purposes
    private const int LogGroupLimit = 3;

    //
    // Main method
    static async Task Main(string[] args)
    {
      var cwClient = new AmazonCloudWatchLogsClient();
      await DisplayLogGroupsWithoutPaginators(cwClient);
      await DisplayLogGroupsWithPaginators(cwClient);
    }


    //
    // Method to get CloudWatch log groups without paginators
    private static async Task DisplayLogGroupsWithoutPaginators(IAmazonCloudWatchLogs cwClient)
    {
      Console.WriteLine("\nGetting list of CloudWatch log groups without using paginators...");
      Console.WriteLine("------------------------------------------------------------------");

      // Loop as many times as needed to get all the log groups
      var request = new DescribeLogGroupsRequest{Limit = LogGroupLimit};
      do
      {
        Console.WriteLine($"Getting up to {LogGroupLimit} log groups...");
        DescribeLogGroupsResponse response = await cwClient.DescribeLogGroupsAsync(request);
        foreach(LogGroup logGroup in response.LogGroups)
        {
          Console.WriteLine($"{logGroup.LogGroupName}");
        }
        request.NextToken = response.NextToken;
      } while(!string.IsNullOrEmpty(request.NextToken));
    }


    //
    // Method to get CloudWatch log groups by using paginators
    private static async Task DisplayLogGroupsWithPaginators(IAmazonCloudWatchLogs cwClient)
    {
      Console.WriteLine("\nGetting list of CloudWatch log groups by using paginators...");
      Console.WriteLine("-------------------------------------------------------------");

      // Access the key results; i.e., the log groups
      // No need to loop to get all the log groups--the SDK does it for us behind the scenes
      Console.WriteLine("\nFrom the key results...");
      Console.WriteLine("------------------------");
      IDescribeLogGroupsPaginator paginatorForLogGroups =
        cwClient.Paginators.DescribeLogGroups(new DescribeLogGroupsRequest());
      await foreach(LogGroup logGroup in paginatorForLogGroups.LogGroups)
      {
        Console.WriteLine(logGroup.LogGroupName);
      }

      // Access the full response
      // Create a new paginator, do NOT reuse the one from above
      Console.WriteLine("\nFrom the full response...");
      Console.WriteLine("--------------------------");
      IDescribeLogGroupsPaginator paginatorForResponses =
        cwClient.Paginators.DescribeLogGroups(new DescribeLogGroupsRequest());
      await foreach(DescribeLogGroupsResponse response in paginatorForResponses.Responses)
      {
        Console.WriteLine($"Content length: {response.ContentLength}");
        Console.WriteLine($"HTTP result: {response.HttpStatusCode}");
        Console.WriteLine($"Metadata: {response.ResponseMetadata}");
        Console.WriteLine("Log groups:");
        foreach(LogGroup logGroup in response.LogGroups) 
        {
          Console.WriteLine($"\t{logGroup.LogGroupName}");
        }
      }
    }
  }
}
```

## Additional considerations for paginators<a name="paginators-additional"></a>
+ **Paginators can't be used more than once**

  If you need the results of a particular AWS paginator in multiple locations in your code, you must not use a paginator object more than once\. Instead, create a new paginator each time you need it\. This concept is shown in the preceding example code in the `DisplayLogGroupsWithPaginators` method\.
+ **Synchronous pagination**

  Synchronous pagination is available for \.NET Framework 4\.5 \(or later\) projects\.

  To see this, create a \.NET Framework 4\.5 \(or later\) project and copy the preceding code to it\. Then simply remove the `await` keyword from the two `foreach` paginator calls, as shown in the following example\.

  ```
  /*await*/ foreach(var logGroup in paginatorForLogGroups.LogGroups)
  {
    Console.WriteLine(logGroup.LogGroupName);
  }
  ```

  Build and run the project to see the same results you saw with asynchronous pagination\.