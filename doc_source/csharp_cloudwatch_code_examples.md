--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# CloudWatch examples using AWS SDK for \.NET<a name="csharp_cloudwatch_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with CloudWatch\.

*Actions* are code excerpts that show you how to call individual CloudWatch functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple CloudWatch functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w99aac23c18b9c11c13)

## Actions<a name="w99aac23c18b9c11c13"></a>

### Delete alarms<a name="cloudwatch_DeleteAlarms_csharp_topic"></a>

The following code example shows how to delete Amazon CloudWatch alarms\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example shows how to delete Amazon CloudWatch alarms. The example
    /// was created using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DeleteAlarms
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();

            var alarmNames = CreateAlarmNameList();
            await DeleteAlarmsAsyncExample(cwClient, alarmNames);
        }

        /// <summary>
        /// Delete the alarms whose names are listed in the alarmNames parameter.
        /// </summary>
        /// <param name="client">The initialized Amazon CloudWatch client.</param>
        /// <param name="alarmNames">A list of names for the alarms to be
        /// deleted.</param>
        public static async Task DeleteAlarmsAsyncExample(IAmazonCloudWatch client, List<string> alarmNames)
        {
            var request = new DeleteAlarmsRequest
            {
                AlarmNames = alarmNames,
            };

            try
            {
                var response = await client.DeleteAlarmsAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Alarms successfully deleted:");
                    alarmNames
                        .ForEach(name => Console.WriteLine($"{name}"));
                }
            }
            catch (ResourceNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Defines and returns the list of alarm names to delete.
        /// </summary>
        /// <returns>A list of alarm names.</returns>
        public static List<string> CreateAlarmNameList()
        {
            // The list of alarm names passed to DeleteAlarmsAsync
            // can contain up to 100 alarm names.
            var theList = new List<string>
            {
                "ALARM_NAME_1",
                "ALARM_NAME_2",
            };

            return theList;
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [DeleteAlarms](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/DeleteAlarms) in *AWS SDK for \.NET API Reference*\. 

### Describe alarm history<a name="cloudwatch_DescribeAlarmHistory_csharp_topic"></a>

The following code example shows how to describe Amazon CloudWatch alarm history\.

**AWS SDK for \.NET**  
Get a list of CloudWatch alarms, then retrieve the history for each alarm\.  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example retrieves a list of Amazon CloudWatch alarms and, for
    /// each one, displays its history. The example was created using the
    /// AWS SDK for .NET 3.7 and .NET Core 5.0.
    /// </summary>
    public class DescribeAlarmHistories
    {
        /// <summary>
        /// Retrieves a list of alarms and then passes each name to the
        /// DescribeAlarmHistoriesAsync method to retrieve its history.
        /// </summary>
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
            var response = await cwClient.DescribeAlarmsAsync();

            foreach (var alarm in response.MetricAlarms)
            {
                await DescribeAlarmHistoriesAsync(cwClient, alarm.AlarmName);
            }
        }

        /// <summary>
        /// Retrieves the CloudWatch alarm history for the alarm name passed
        /// to the method.
        /// </summary>
        /// <param name="client">An initialized CloudWatch client object.</param>
        /// <param name="alarmName">The CloudWatch alarm for which to retrieve
        /// history information.</param>
        public static async Task DescribeAlarmHistoriesAsync(IAmazonCloudWatch client, string alarmName)
        {
            var request = new DescribeAlarmHistoryRequest
            {
                AlarmName = alarmName,
                EndDateUtc = DateTime.Today,
                HistoryItemType = HistoryItemType.Action,
                MaxRecords = 1,
                StartDateUtc = DateTime.Today.Subtract(TimeSpan.FromDays(30)),
            };

            var response = new DescribeAlarmHistoryResponse();

            do
            {
                response = await client.DescribeAlarmHistoryAsync(request);

                foreach (var item in response.AlarmHistoryItems)
                {
                    Console.WriteLine(item.AlarmName);
                    Console.WriteLine(item.HistorySummary);
                    Console.WriteLine();
                }

                request.NextToken = response.NextToken;
            }
            while (!string.IsNullOrEmpty(response.NextToken));
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [DescribeAlarmHistory](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/DescribeAlarmHistory) in *AWS SDK for \.NET API Reference*\. 

### Disable alarm actions<a name="cloudwatch_DisableAlarmActions_csharp_topic"></a>

The following code example shows how to disable Amazon CloudWatch alarm actions\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example shows how to disable the Amazon CloudWatch actions for
    /// one or more CloudWatch alarms. The example was created using the
    /// AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class DisableAlarmActions
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
            var alarmNames = new List<string>
            {
                "ALARM_NAME",
                "ALARM_NAME_2",
            };

            var success = await DisableAlarmsActionsAsync(cwClient, alarmNames);

            if (success)
            {
                Console.WriteLine("Alarm action(s) successfully disabled.");
            }
            else
            {
                Console.WriteLine("Alarm action(s) were not disabled.")
            }
        }

        /// <summary>
        /// Disable the actions for the list of CloudWatch alarm names passed
        /// in the alarmNames parameter.
        /// </summary>
        /// <param name="client">An initialized CloudWatch client object.</param>
        /// <param name="alarmNames">The list of CloudWatch alarms to disable.</param>
        /// <returns>A Boolean value indicating the success of the call.</returns>
        public static async Task<bool> DisableAlarmsActionsAsync(
            IAmazonCloudWatch client,
            List<string> alarmNames)
        {
            var request = new DisableAlarmActionsRequest
            {
                AlarmNames = alarmNames,
            };

            var response = await client.DisableAlarmActionsAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [DisableAlarmActions](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/DisableAlarmActions) in *AWS SDK for \.NET API Reference*\. 

### Enable alarm actions<a name="cloudwatch_EnableAlarmActions_csharp_topic"></a>

The following code example shows how to enable Amazon CloudWatch alarm actions\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example shows how to enable the Amazon CloudWatch actions for
    /// one or more CloudWatch alarms. The example was created using the
    /// AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class EnableAlarmActions
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
            var alarmNames = new List<string>
            {
                "ALARM_NAME",
                "ALARM_NAME_2",
            };

            var success = await EnableAlarmActionsAsync(cwClient, alarmNames);

            if (success)
            {
                Console.WriteLine("Alarm action(s) successfully enabled.");
            }
            else
            {
                Console.WriteLine("Alarm action(s) were not enabled.")
            }
        }

        /// <summary>
        /// Enable the actions for the list of CloudWatch alarm names passed
        /// in the alarmNames parameter.
        /// </summary>
        /// <param name="client">An initialized CloudWatch client object.</param>
        /// <param name="alarmNames">The list of CloudWatch alarms to enable.</param>
        /// <returns>A Boolean value indicating the success of the call.</returns>
        public static async Task<bool> EnableAlarmActionsAsync(IAmazonCloudWatch client, List<string> alarmNames)
        {
            var request = new EnableAlarmActionsRequest
            {
                AlarmNames = alarmNames,
            };

            var response = await client.EnableAlarmActionsAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [EnableAlarmActions](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/EnableAlarmActions) in *AWS SDK for \.NET API Reference*\. 

### Get dashboard details<a name="cloudwatch_GetDashboard_csharp_topic"></a>

The following code example shows how to get Amazon CloudWatch dashboard details\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example shows how to retrieve the details of an Amazon CloudWatch
    /// dashboard. The return value from the call to GetDashboard is a json
    /// object representing the widgets in the dashboard. The example was
    /// created using the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class GetDashboard
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
            string dashboardName = "CloudWatch-Default";

            var body = await GetDashboardAsync(cwClient, dashboardName);

            Console.WriteLine(body);
        }

        /// <summary>
        /// Get the json that represents the dashboard.
        /// </summary>
        /// <param name="client">An initialized CloudWatch client.</param>
        /// <param name="dashboardName">The name of the dashboard.</param>
        /// <returns>The string containing the json value describing the
        /// contents and layout of the CloudWatch dashboard.</returns>
        public static async Task<string> GetDashboardAsync(IAmazonCloudWatch client, string dashboardName)
        {

            var request = new GetDashboardRequest
            {
                DashboardName = dashboardName,
            };

            var response = await client.GetDashboardAsync(request);

            return response.DashboardBody;
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [GetDashboard](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/GetDashboard) in *AWS SDK for \.NET API Reference*\. 

### List dashboards<a name="cloudwatch_ListDashboards_csharp_topic"></a>

The following code example shows how to list Amazon CloudWatch dashboards\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// Shows how to retrieve a list of Amazon CloudWatch dashboards. This
    /// example was written using AWSSDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class ListDashboards
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
            var dashboards = await ListDashboardsAsync(cwClient);

            DisplayDashboardList(dashboards);
        }

        /// <summary>
        /// Get the list of available dashboards.
        /// </summary>
        /// <param name="client">The initialized CloudWatch client used to
        /// retrieve a list of defined dashboards.</param>
        /// <returns>A list of DashboardEntry objects.</returns>
        public static async Task<List<DashboardEntry>> ListDashboardsAsync(IAmazonCloudWatch client)
        {
            var response = await client.ListDashboardsAsync(new ListDashboardsRequest());
            return response.DashboardEntries;
        }

        /// <summary>
        /// Displays the name of each CloudWatch Dashboard in the list passed
        /// to the method.
        /// </summary>
        /// <param name="dashboards">A list of DashboardEntry objects.</param>
        public static void DisplayDashboardList(List<DashboardEntry> dashboards)
        {
            if (dashboards.Count > 0)
            {
                Console.WriteLine("The following dashboards are defined:");
                foreach (var dashboard in dashboards)
                {
                    Console.WriteLine($"Name: {dashboard.DashboardName} Last modified: {dashboard.LastModified}");
                }
            }
            else
            {
                Console.WriteLine("No dashboards found.");
            }
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [ListDashboards](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/ListDashboards) in *AWS SDK for \.NET API Reference*\. 

### List metrics<a name="cloudwatch_ListMetrics_csharp_topic"></a>

The following code example shows how to list Amazon CloudWatch metrics\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.CloudWatch;
    using Amazon.CloudWatch.Model;

    /// <summary>
    /// This example demonstrates how to list metrics for Amazon CloudWatch.
    /// The example was created using the AWS SDK for .NET version 3.7 and
    /// .NET Core 5.0.
    /// </summary>
    public class ListMetrics
    {
        public static async Task Main()
        {
            IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();

            var filter = new DimensionFilter
            {
                Name = "InstanceType",
                Value = "t1.micro",
            };
            string metricName = "CPUUtilization";
            string namespaceName = "AWS/EC2";

            await ListMetricsAsync(cwClient, filter, metricName, namespaceName);
        }

        /// <summary>
        /// Retrieve CloudWatch metrics using the supplied filter, metrics name,
        /// and namespace.
        /// </summary>
        /// <param name="client">An initialized CloudWatch client.</param>
        /// <param name="filter">The filter to apply in retrieving metrics.</param>
        /// <param name="metricName">The metric name for which to retrieve
        /// information.</param>
        /// <param name="nameSpaceName">The name of the namespace from which
        /// to retrieve metric information.</param>
        public static async Task ListMetricsAsync(
            IAmazonCloudWatch client,
            DimensionFilter filter,
            string metricName,
            string nameSpaceName)
        {
            var request = new ListMetricsRequest
            {
                Dimensions = new List<DimensionFilter>() { filter },
                MetricName = metricName,
                Namespace = nameSpaceName,
            };

            var response = new ListMetricsResponse();
            do
            {
                response = await client.ListMetricsAsync(request);

                if (response.Metrics.Count > 0)
                {
                    foreach (var metric in response.Metrics)
                    {
                        Console.WriteLine(metric.MetricName +
                          " (" + metric.Namespace + ")");

                        foreach (var dimension in metric.Dimensions)
                        {
                            Console.WriteLine("  " + dimension.Name + ": "
                              + dimension.Value);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No metrics found.");
                }

                request.NextToken = response.NextToken;
            }
            while (!string.IsNullOrEmpty(response.NextToken));
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatch#code-examples)\. 
+  For API details, see [ListMetrics](https://docs.aws.amazon.com/goto/DotNetSDKV3/monitoring-2010-08-01/ListMetrics) in *AWS SDK for \.NET API Reference*\. 