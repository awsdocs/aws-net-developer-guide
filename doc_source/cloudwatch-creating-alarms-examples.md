--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Describing, Creating, and Deleting Alarms in Amazon CloudWatch<a name="cloudwatch-creating-alarms-examples"></a>

This \.NET example show you how to:
+ Describe a CloudWatch alarm
+ Create a CloudWatch alarm based on a metric
+ Delete a CloudWatch alarm

## The Scenario<a name="cloudwatch-creating-alarms-examples"></a>

An alarm watches a single metric over a time period you specify\. It performs one or more actions based on the value of the metric, relative to a given threshold over a number of time periods\. The following examples show how to describe, create, and delete alarms in CloudWatch using these methods of the [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) class:
+  [DescribeAlarms](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDescribeAlarmsDescribeAlarmsRequest.html) 
+  [PutMetricAlarm](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricAlarmPutMetricAlarmRequest.html) 
+  [DeleteAlarms](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDeleteAlarmsDeleteAlarmsRequest.html) 

For more information about CloudWatch alarms, see [Creating Amazon CloudWatch Alarms](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/AlarmThatSendsEmail.html) in the Amazon CloudWatch User Guide\.

## Prerequisite Tasks<a name="cloudwatch-describe-alarms-prerequisites"></a>

To set up and run this example, you must first:
+  [Get set up to use Amazon CloudWatch](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/GettingSetup.html)\.
+ [Set up and configure the AWS SDK for \.NET\.](net-dg-setup.md)

## Describing an Alarm<a name="cloudwatch-example-describing-alarms"></a>

Create an [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance and a [DescribeAlarmsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TDescribeAlarmsRequest.html) object, limiting the alarms that are returned to those with a state of INSUFFICIENT\_DATA\. Then call the [DescribeAlarms](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDescribeAlarmsDescribeAlarmsRequest.html) method of the `AmazonCloudWatchClient` object\.

```
using (var cloudWatch = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
{
    var request = new DescribeAlarmsRequest();
    request.StateValue = "INSUFFICIENT_DATA";
    request.AlarmNames = new List<string> { "Alarm1", "Alarm2" };
    do
    {
        var response = cloudWatch.DescribeAlarms(request);
        foreach(var alarm in response.MetricAlarms)
        {
            Console.WriteLine(alarm.AlarmName);
        }
        request.NextToken = response.NextToken;
    } while (request.NextToken != null);
}
```

## Creating an Alarm Based on a Metric<a name="cloudwatch-example-creating-alarms-metric"></a>

Create an [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance and a [PutMetricAlarmRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TPutMetricAlarmRequest.html) object for the parameters needed to create an alarm that is based on a metric, in this case, the CPU utilization of an Amazon EC2 instance\.

The remaining parameters are set to trigger the alarm when the metric exceeds a threshold of 70 percent\.

Then call the [PutMetricAlarm](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricAlarmPutMetricAlarmRequest.html) method of the `AmazonCloudWatchClient` object\.

```
var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2);
client.PutMetricAlarm(
    new PutMetricAlarmRequest
    {
        AlarmName = "Web_Server_CPU_Utilization",
        ComparisonOperator = ComparisonOperator.GreaterThanThreshold,
        EvaluationPeriods = 1,
        MetricName = "CPUUtilization",
        Namespace = "AWS/EC2",
        Period = 60,
        Statistic = Statistic.Average,
        Threshold = 70.0,
        ActionsEnabled = true,
        AlarmActions = new List<string> { "arn:aws:swf:us-west-2:" + "customerAccount" + ":action/actions/AWS_EC2.InstanceId.Reboot/1.0" },
        AlarmDescription = "Alarm when server CPU exceeds 70%",
        Dimensions = new List<Dimension>
            {
                new Dimension { Name = "InstanceId", Value = "INSTANCE_ID" }
            },
        Unit = StandardUnit.Seconds
    }
);
```

## Deleting an Alarm<a name="cloudwatch-example-deleting-alarms"></a>

Create an [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance and a [DeleteAlarmsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TDeleteAlarmsRequest.html) object to hold the names of the alarms you want to delete\. Then call the [DeleteAlarms](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDeleteAlarmsDeleteAlarmsRequest.html) method of the `AmazonCloudWatchClient` object\.

```
using (var cloudWatch = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
{
    var response = cloudWatch.DeleteAlarms(
        new DeleteAlarmsRequest
        {
            AlarmNames = new List<string> { "Alarm1", "Alarm2" };
        });
}
```