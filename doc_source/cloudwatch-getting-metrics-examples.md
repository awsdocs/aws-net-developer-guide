--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Getting Metrics from Amazon CloudWatch<a name="cloudwatch-getting-metrics-examples"></a>

This example shows you how to:
+ Retrieve a list of CloudWatch metrics
+ Publish CloudWatch custom metrics

## The Scenario<a name="the-scenario"></a>

Metrics are data about the performance of your systems\. You can enable detailed monitoring of some resources such as your Amazon EC2 instances or your own application metrics\. In this example, you use \.NET to retrieve a list of published CloudWatch metrics and publish data points to CloudWatch metrics using these methods of the [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) class:
+  [ListMetrics](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchListMetricsListMetricsRequest.html) 
+  [PutMetricData](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricDataPutMetricDataRequest.html) 

For more information about CloudWatch metrics, see [Using Amazon CloudWatch Metrics](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/working_with_metrics.html) in the Amazon CloudWatch User Guide\.

## Prerequisite Tasks<a name="prerequisite-tasks"></a>

To set up and run this example, you must first:
+  [Get set up to use Amazon CloudWatch](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/GettingSetup.html)\.
+ [Set up and configure the AWS SDK for \.NET\.](net-dg-setup.md)

## List Metrics<a name="list-metrics"></a>

Create a [ListMetricsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TListMetricsRequest.html) object containing the parameters needed to list metrics within the `AWS/Logs` namespace\. Call the [ListMetrics](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchListMetricsListMetricsRequest.html) method from a [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance to list the `IncomingLogEvents` metric\.

```
var logGroupName = "LogGroupName";
DimensionFilter dimensionFilter = new DimensionFilter()
{
    Name = logGroupName
};
var dimensionFilterList = new List<DimensionFilter>();
dimensionFilterList.Add(dimensionFilter);

var dimension = new Dimension
{
    Name = "UniquePages",
    Value = "URLs"
};
using (var cw = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
{
    var listMetricsResponse = cw.ListMetrics(new ListMetricsRequest
    {
        Dimensions = dimensionFilterList,
        MetricName = "IncomingLogEvents",
        Namespace = "AWS/Logs"
    });
    Console.WriteLine(listMetricsResponse.Metrics);
}
```

## Submit Custom Metrics<a name="submit-custom-metrics"></a>

Create a [PutMetricDataRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TPutMetricDataRequest.html) object containing the parameters needed to submit a data point for the `PAGES_VISITED` custom metric\. Call the [PutMetricData](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricDataPutMetricDataRequest.html) method from the [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance\.

```
using (var cw = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
{
    cw.PutMetricData(new PutMetricDataRequest
    {
        MetricData = new List<MetricDatum>{new MetricDatum
        {
            MetricName = "PagesVisited",
            Dimensions = new List<Dimension>{dimension},
            Unit = "None",
            Value = 1.0
        }},
        Namespace = "SITE/TRAFFIC"
    });
}
```