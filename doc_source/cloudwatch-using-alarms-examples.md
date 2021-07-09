--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Using Alarms in Amazon CloudWatch<a name="cloudwatch-using-alarms-examples"></a>

This \.NET example shows you how to change the state of your Amazon EC2 instances automatically based on a CloudWatch alarm\.

## The Scenario<a name="the-scenario"></a>

Using alarm actions, you can create alarms that automatically stop, terminate, reboot, or recover your Amazon EC2 instances\. You can use the stop or terminate actions when you no longer need an instance to be running\. You can use the reboot and recover actions to automatically reboot those instances\.

In this example, \.NET is used to define an alarm action in CloudWatch that triggers the reboot of an Amazon EC2 instance\. The methods use the AWS SDK for \.NET to manage Amazon EC2 instances using these methods of the [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) class:
+  [PutMetricAlarm](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricAlarmPutMetricAlarmRequest.html) 
+  [EnableAlarmActions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchEnableAlarmActionsEnableAlarmActionsRequest.html) 
+  [DisableAlarmActions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDisableAlarmActionsDisableAlarmActionsRequest.html) 

For more information about CloudWatch alarm actions, see [Create Alarms to Stop, Terminate, Reboot, or Recover an Instance](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/UsingAlarmActions.html) in the Amazon CloudWatch User Guide\.

## Prerequisite Tasks<a name="prerequisite-tasks"></a>

To set up and run this example, you must first:
+  [Get set up to use Amazon CloudWatch](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/GettingSetup.html)\.
+ [Set up and configure the AWS SDK for \.NET\.](net-dg-setup.md)

## Create and Enable Actions on an Alarm<a name="create-and-enable-actions-on-an-alarm"></a>

1. Create an [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance and a [PutMetricAlarmRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TPutMetricAlarmRequest.html) object to hold the parameters for creating an alarm, specifying `ActionsEnabled` as true and an array of ARNs for the actions the alarm will trigger\. Call the [PutMetricAlarm](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricAlarmPutMetricAlarmRequest.html) method of the `AmazonCloudWatchClient` object, which creates the alarm if it doesn’t exist or updates it if the alarm does exist\.

   ```
   using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
   {
       client.PutMetricAlarm(new PutMetricAlarmRequest
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
               new Dimension { Name = "InstanceId", Value = "instanceId" }
           }
       });
   }
   ```

1. When `PutMetricAlarm` completes successfully, create an [EnableAlarmActionsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TEnableAlarmActionsRequest.html) object containing the name of the CloudWatch alarm\. Call the [EnableAlarmActions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchEnableAlarmActionsEnableAlarmActionsRequest.html) method to enable the alarm action\.

   ```
   client.EnableAlarmActions(new EnableAlarmActionsRequest
   {
       AlarmNames = new List<string> { "Web_Server_CPU_Utilization" }
   });
   ```

1. Create a [MetricDatum](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TMetricDatum.html) object containing the CPUUtilization custom metric\. Create a [PutMetricDataRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TPutMetricDataRequest.html) object containing the `MetricData` parameter needed to submit a data point for the CPUUtilization metric\. Call the [PutMetricData](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchPutMetricDataPutMetricDataRequest.html) method\.

   ```
   MetricDatum metricDatum = new MetricDatum
   { MetricName = "CPUUtilization" };
   PutMetricDataRequest putMetricDatarequest = new PutMetricDataRequest
   {
       MetricData = new List<MetricDatum> { metricDatum }
   };
   client.PutMetricData(putMetricDatarequest);
   ```

## Disable Actions on an Alarm<a name="disable-actions-on-an-alarm"></a>

Create an [AmazonCloudWatchClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TCloudWatchClient.html) instance and a [DisableAlarmActionsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/TDisableAlarmActionsRequest.html) object containing the name of the CloudWatch alarm\. Call the [DisableAlarmActions](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatch/MCloudWatchDisableAlarmActionsDisableAlarmActionsRequest.html) method to disable the actions for this alarm\.

```
using (var client = new AmazonCloudWatchClient(RegionEndpoint.USWest2))
{
    client.DisableAlarmActions(new DisableAlarmActionsRequest
    {
        AlarmNames = new List<string> { "Web_Server_CPU_Utilization" }
    });
}
```