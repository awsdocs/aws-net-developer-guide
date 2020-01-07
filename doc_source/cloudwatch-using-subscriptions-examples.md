# Using Subscription Filters in Amazon CloudWatch Logs<a name="cloudwatch-using-subscriptions-examples"></a>

These \.NET examples shows you how to:
+ List existing subscription filters in CloudWatch Logs
+ Create a subscription filter in CloudWatch Logs
+ Delete a subscription filter in CloudWatch Logs

## The Scenario<a name="the-scenario"></a>

Subscriptions provide access to a real\-time feed of log events from CloudWatch Logs and deliver that feed to other services such as an Amazon Kinesis Data Streams or AWS Lambda for custom processing, analysis, or loading to other systems\. A subscription filter defines the pattern to use for filtering which log events are delivered to your AWS resource\. This example shows how to list, create, and delete a subscription filter in CloudWatch Logs\. The destination for the log events is a Lambda function\.

This example uses the AWS SDK for \.NET to manage subscription filters using these methods of the [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) class:
+  [DescribeSubscriptionFilters](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsDescribeSubscriptionFiltersDescribeSubscriptionFiltersRequest.html) 
+  [PutSubscriptionFilter](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsPutSubscriptionFilterPutSubscriptionFilterRequest.html) 
+  [DeleteSubscriptionFilter](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsDeleteSubscriptionFilterDeleteSubscriptionFilterRequest.html) 

For more information about CloudWatch Logs subscriptions, see [Real\-time Processing of Log Data with Subscriptions](https://docs.aws.amazon.com/AmazonCloudWatch/latest/logs/Subscriptions.html) in the Amazon CloudWatch Logs User Guide\.

## Prerequisite Tasks<a name="prerequisite-tasks"></a>

To set up and run this example, you must first:
+  [Get set up to use Amazon CloudWatch](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/GettingSetup.html)\.
+  [Set up and configure](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/net-dg-setup.html) the AWS SDK for \.NET\.

## Describe Existing Subscription Filters<a name="describe-existing-subscription-filters"></a>

Create an [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) object\. Create a [DescribeSubscriptionFiltersRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TDescribeSubscriptionFiltersRequest.html) object containing the parameters needed to describe your existing filters\. Include the name of the log group and the maximum number of filters you want described\. Call the [DescribeSubscriptionFilters](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsDescribeSubscriptionFiltersDescribeSubscriptionFiltersRequest.html) method\.

```
public static void DescribeSubscriptionFilters()
{
    var client = new AmazonCloudWatchLogsClient();
    var request = new Amazon.CloudWatchLogs.Model.DescribeSubscriptionFiltersRequest()
    {
        LogGroupName = "GROUP_NAME",
        Limit = 5
    };
    try
    {
        var response = client.DescribeSubscriptionFilters(request);
    }
    catch (Amazon.CloudWatchLogs.Model.ResourceNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        client?.Dispose();
    }
}
```

## Create a Subscription Filter<a name="create-a-subscription-filter"></a>

Create an [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) object\. Create a [PutSubscriptionFilterRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TPutSubscriptionFilterRequest.html) object containing the parameters needed to create a filter, including the ARN of the destination Lambda function, the name of the filter, the string pattern for filtering, and the name of the log group\. Call the [PutSubscriptionFilter](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsPutSubscriptionFilterPutSubscriptionFilterRequest.html) method\.

```
public static void PutSubscriptionFilters()
{
    var client = new AmazonCloudWatchLogsClient();
    var request = new Amazon.CloudWatchLogs.Model.PutSubscriptionFilterRequest()
    {
        DestinationArn = "LAMBDA_FUNCTION_ARN",
        FilterName = "FILTER_NAME",
        FilterPattern = "ERROR",
        LogGroupName = "Log_Group"
    };
    try
    {
        var response = client.PutSubscriptionFilter(request);
    }
    catch (InvalidParameterException e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        client?.Dispose();
    }
}
```

## Delete a Subscription Filter<a name="delete-a-subscription-filter"></a>

Create an [AmazonCloudWatchLogsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) object\. Create a [DeleteSubscriptionFilterRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/TDeleteSubscriptionFilterRequest.html) object containing the parameters needed to delete a filter, including the names of the filter and the log group\. Call the [DeleteSubscriptionFilter](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchLogs/MCloudWatchLogsDeleteSubscriptionFilterDeleteSubscriptionFilterRequest.html) method\.

```
public static void DeleteSubscriptionFilter()
{
    var client = new AmazonCloudWatchLogsClient();
    var request = new Amazon.CloudWatchLogs.Model.DeleteSubscriptionFilterRequest()
    {
        LogGroupName = "GROUP_NAME",
        FilterName = "FILTER"
    };
    try
    {
        var response = client.DeleteSubscriptionFilter(request);
    }
    catch (Amazon.CloudWatchLogs.Model.ResourceNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        client?.Dispose();
    }
}
```