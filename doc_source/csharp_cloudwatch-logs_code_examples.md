--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools) for a simplified deployment experience\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

Read our [original blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) as well as the [update post](https://aws.amazon.com/blogs/developer/update-new-net-deployment-experience/) and the [post on deployment projects](https://aws.amazon.com/blogs/developer/dotnet-deployment-projects/)\. Submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\! For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# CloudWatch Logs examples using AWS SDK for \.NET<a name="csharp_cloudwatch-logs_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with CloudWatch Logs\.

*Actions* are code excerpts that show you how to call individual CloudWatch Logs functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple CloudWatch Logs functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w131aac23c18b9c13c13)

## Actions<a name="w131aac23c18b9c13c13"></a>

### Associate an AWS KMS key to log group<a name="cloudwatch-logs_AssociateKmsKey_csharp_topic"></a>

The following code example shows how to associate an AWS KMS key with an existing CloudWatch Logs log group\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // This client object will be associated with the same AWS Region
            // as the default user on this system. If you need to use a
            // different AWS Region, pass it as a parameter to the client
            // constructor.
            var client = new AmazonCloudWatchLogsClient();

            string kmsKeyId = "arn:aws:kms:us-west-2:<account-number>:key/7c9eccc2-38cb-4c4f-9db3-766ee8dd3ad4";
            string groupName = "cloudwatchlogs-example-loggroup";

            var request = new AssociateKmsKeyRequest
            {
                KmsKeyId = kmsKeyId,
                LogGroupName = groupName,
            };

            var response = await client.AssociateKmsKeyAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully associated KMS key ID: {kmsKeyId} with log group: {groupName}.");
            }
            else
            {
                Console.WriteLine("Could not make the association between: {kmsKeyId} and {groupName}.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [AssociateKmsKey](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/AssociateKmsKey) in *AWS SDK for \.NET API Reference*\. 

### Cancel an export task<a name="cloudwatch-logs_CancelExportTask_csharp_topic"></a>

The following code example shows how to cancel an existing CloudWatch Logs export task\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // This client object will be associated with the same AWS Region
            // as the default user on this system. If you need to use a
            // different AWS Region, pass it as a parameter to the client
            // constructor.
            var client = new AmazonCloudWatchLogsClient();
            string taskId = "exampleTaskId";

            var request = new CancelExportTaskRequest
            {
                TaskId = taskId,
            };

            var response = await client.CancelExportTaskAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{taskId} successfully canceled.");
            }
            else
            {
                Console.WriteLine($"{taskId} could not be canceled.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [CancelExportTask](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/CancelExportTask) in *AWS SDK for \.NET API Reference*\. 

### Create a log group<a name="cloudwatch-logs_CreateLogGroup_csharp_topic"></a>

The following code example shows how to create a new CloudWatch Logs log group\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // This client object will be associated with the same AWS Region
            // as the default user on this system. If you need to use a
            // different AWS Region, pass it as a parameter to the client
            // constructor.
            var client = new AmazonCloudWatchLogsClient();

            string logGroupName = "cloudwatchlogs-example-loggroup";

            var request = new CreateLogGroupRequest
            {
                LogGroupName = logGroupName,
            };

            var response = await client.CreateLogGroupAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully create log group with ID: {logGroupName}.");
            }
            else
            {
                Console.WriteLine("Could not create log group.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [CreateLogGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/CreateLogGroup) in *AWS SDK for \.NET API Reference*\. 

### Create a new log stream<a name="cloudwatch-logs_CreateLogStream_csharp_topic"></a>

The following code example shows how to create a new CloudWatch Logs log stream\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // This client object will be associated with the same AWS Region
            // as the default user on this system. If you need to use a
            // different AWS Region, pass it as a parameter to the client
            // constructor.
            var client = new AmazonCloudWatchLogsClient();
            string logGroupName = "cloudwatchlogs-example-loggroup";
            string logStreamName = "cloudwatchlogs-example-logstream";

            var request = new CreateLogStreamRequest
            {
                LogGroupName = logGroupName,
                LogStreamName = logStreamName,
            };

            var response = await client.CreateLogStreamAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{logStreamName} successfully created for {logGroupName}.");
            }
            else
            {
                Console.WriteLine("Could not create stream.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [CreateLogStream](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/CreateLogStream) in *AWS SDK for \.NET API Reference*\. 

### Delete a log group<a name="cloudwatch-logs_DeleteLogGroup_csharp_topic"></a>

The following code example shows how to delete an existing CloudWatch Logs log group\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            var client = new AmazonCloudWatchLogsClient();
            string logGroupName = "cloudwatchlogs-example-loggroup";

            var request = new DeleteLogGroupRequest
            {
                LogGroupName = logGroupName,
            };

            var response = await client.DeleteLogGroupAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully deleted CloudWatch log group, {logGroupName}.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [DeleteLogGroup](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/DeleteLogGroup) in *AWS SDK for \.NET API Reference*\. 

### Describe export tasks<a name="cloudwatch-logs_DescribeExportTasks_csharp_topic"></a>

The following code example shows how to describe CloudWatch Logs export tasks\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // This client object will be associated with the same AWS Region
            // as the default user on this system. If you need to use a
            // different AWS Region, pass it as a parameter to the client
            // constructor.
            var client = new AmazonCloudWatchLogsClient();

            var request = new DescribeExportTasksRequest
            {
                Limit = 5,
            };

            var response = new DescribeExportTasksResponse();

            do
            {
                response = await client.DescribeExportTasksAsync(request);
                response.ExportTasks.ForEach(t =>
                {
                   Console.WriteLine($"{t.TaskName} with ID: {t.TaskId} has status: {t.Status}");
                });
            }
            while (response.NextToken is not null);
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [DescribeExportTasks](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/DescribeExportTasks) in *AWS SDK for \.NET API Reference*\. 

### Describe log groups<a name="cloudwatch-logs_DescribeLogGroups_csharp_topic"></a>

The following code example shows how to describe CloudWatch Logs log groups\.

**AWS SDK for \.NET**  
  

```
        public static async Task Main()
        {
            // Creates a CloudWatch Logs client using the default
            // user. If you need to work with resources in another
            // AWS Region than the one defined for the default user,
            // pass the AWS Region as a parameter to the client constructor.
            var client = new AmazonCloudWatchLogsClient();

            var request = new DescribeLogGroupsRequest
            {
                Limit = 5,
            };

            var response = await client.DescribeLogGroupsAsync(request);

            if (response.LogGroups.Count > 0)
            {
                do
                {
                    response.LogGroups.ForEach(lg =>
                    {
                        Console.WriteLine($"{lg.LogGroupName} is associated with the key: {lg.KmsKeyId}.");
                        Console.WriteLine($"Created on: {lg.CreationTime.Date.Date}");
                        Console.WriteLine($"Date for this group will be stored for: {lg.RetentionInDays} days.\n");
                    });
                }
                while (response.NextToken is not null);
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/CloudWatchLogs#code-examples)\. 
+  For API details, see [DescribeLogGroups](https://docs.aws.amazon.com/goto/DotNetSDKV3/logs-2014-03-28/DescribeLogGroups) in *AWS SDK for \.NET API Reference*\. 