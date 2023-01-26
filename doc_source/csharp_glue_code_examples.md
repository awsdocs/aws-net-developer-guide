# AWS Glue examples using AWS SDK for \.NET<a name="csharp_glue_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with AWS Glue\.

*Actions* are code excerpts that show you how to call individual service functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple functions within the same service\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w2aac21c17c13c27c13)
+ [Scenarios](#w2aac21c17c13c27c15)

## Actions<a name="w2aac21c17c13c27c13"></a>

### Create a crawler<a name="glue_CreateCrawler_csharp_topic"></a>

The following code example shows how to create an AWS Glue crawler\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Creates an AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="iam">The Amazon Resource Name (ARN) of the IAM role
        /// that is used by the crawler.</param>
        /// <param name="s3Path">The path to the Amazon S3 bucket where
        /// data is stored.</param>
        /// <param name="cron">The name of the CRON job that runs the crawler.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="crawlerName">The name of the AWS Glue crawler.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler was
        /// created successfully.</returns>
        public static async Task<bool> CreateGlueCrawlerAsync(
            AmazonGlueClient glueClient,
            string iam,
            string s3Path,
            string cron,
            string dbName,
            string crawlerName)
        {
            var s3Target = new S3Target
            {
                Path = s3Path,
            };

            var targetList = new List<S3Target>
            {
                s3Target,
            };

            var targets = new CrawlerTargets
            {
                S3Targets = targetList,
            };

            var crawlerRequest = new CreateCrawlerRequest
            {
                DatabaseName = dbName,
                Name = crawlerName,
                Description = "Created by the AWS Glue .NET API",
                Targets = targets,
                Role = iam,
                Schedule = cron,
            };

            var response = await glueClient.CreateCrawlerAsync(crawlerRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was successfully created");
                return true;
            }
            else
            {
                Console.WriteLine($"Could not create {crawlerName}.");
                return false;
            }
        }
```
+  For API details, see [CreateCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/CreateCrawler) in *AWS SDK for \.NET API Reference*\. 

### Create a job definition<a name="glue_CreateJob_csharp_topic"></a>

The following code example shows how to create an AWS Glue job definition\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Creates an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The name of the job to create.</param>
        /// <param name="iam">The Amazon Resource Name (ARN) of the IAM role
        /// that will be used by the job.</param>
        /// <param name="scriptLocation">The location where the script is stored.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job was
        /// created successfully.</returns>
        public static async Task<bool> CreateJobAsync(AmazonGlueClient glueClient, string jobName, string iam, string scriptLocation)
        {
            var command = new JobCommand
            {
                PythonVersion = "3",
                Name = "MyJob1",
                ScriptLocation = scriptLocation,
            };

            var jobRequest = new CreateJobRequest
            {
                Description = "A Job created by using the AWS SDK for .NET",
                GlueVersion = "2.0",
                WorkerType = WorkerType.G1X,
                NumberOfWorkers = 10,
                Name = jobName,
                Role = iam,
                Command = command,
            };

            var response = await glueClient.CreateJobAsync(jobRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} was successfully created.");
                return true;
            }

            Console.WriteLine($"{jobName} could not be created.");
            return false;
        }
```
+  For API details, see [CreateJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/CreateJob) in *AWS SDK for \.NET API Reference*\. 

### Delete a crawler<a name="glue_DeleteCrawler_csharp_topic"></a>

The following code example shows how to delete an AWS Glue crawler\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Deletes the named AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawler to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var deleteCrawlerRequest = new DeleteCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.DeleteCrawlerAsync(deleteCrawlerRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was deleted");
                return true;
            }

            Console.WriteLine($"Could not create {crawlerName}.");
            return false;
        }
```
+  For API details, see [DeleteCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteCrawler) in *AWS SDK for \.NET API Reference*\. 

### Delete a database from the Data Catalog<a name="glue_DeleteDatabase_csharp_topic"></a>

The following code example shows how to delete a database from the AWS Glue Data Catalog\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Deletes an AWS Glue database.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="databaseName">The name of the database to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue database was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteDatabaseAsync(AmazonGlueClient glueClient, string databaseName)
        {
            var request = new DeleteDatabaseRequest
            {
                Name = databaseName,
            };

            var response = await glueClient.DeleteDatabaseAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{databaseName} was successfully deleted");
                return true;
            }

            Console.WriteLine($"{databaseName} could not be deleted.");
            return false;
        }
```
+  For API details, see [DeleteDatabase](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteDatabase) in *AWS SDK for \.NET API Reference*\. 

### Delete a job definition<a name="glue_DeleteJob_csharp_topic"></a>

The following code example shows how to delete an AWS Glue job definition and all associated runs\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Deletes the named job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The name of the job to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteJobAsync(AmazonGlueClient glueClient, string jobName)
        {
            var jobRequest = new DeleteJobRequest
            {
                JobName = jobName,
            };

            var response = await glueClient.DeleteJobAsync(jobRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} was successfully deleted");
                return true;
            }

            Console.WriteLine($"{jobName} could not be deleted.");
            return false;
        }
```
+  For API details, see [DeleteJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteJob) in *AWS SDK for \.NET API Reference*\. 

### Get a crawler<a name="glue_GetCrawler_csharp_topic"></a>

The following code example shows how to get an AWS Glue crawler\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Retrieves information about a specific AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawer.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue crawler was retrieved successfully.</returns>
        public static async Task<bool> GetSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var crawlerRequest = new GetCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.GetCrawlerAsync(crawlerRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var databaseName = response.Crawler.DatabaseName;
                Console.WriteLine($"{crawlerName} has the database {databaseName}");
                return true;
            }

            Console.WriteLine($"No information regarding {crawlerName} could be found.");
            return false;
        }
```
+  For API details, see [GetCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetCrawler) in *AWS SDK for \.NET API Reference*\. 

### Get a database from the Data Catalog<a name="glue_GetDatabase_csharp_topic"></a>

The following code example shows how to get a database from the AWS Glue Data Catalog\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Gets information about the database created for this Glue
        /// example.
        /// </summary>
        /// <param name="glueClient">The initialized Glue client.</param>
        /// <param name="databaseName">The name of the AWS Glue database.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue database was retrieved successfully.</returns>
        public static async Task<bool> GetSpecificDatabaseAsync(
            AmazonGlueClient glueClient,
            string databaseName)
        {
            var databasesRequest = new GetDatabaseRequest
            {
                Name = databaseName,
            };

            var response = await glueClient.GetDatabaseAsync(databasesRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"The Create Time is {response.Database.CreateTime}");
                return true;
            }

            Console.WriteLine($"No informaton about {databaseName}.");
            return false;
        }
```
+  For API details, see [GetDatabase](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetDatabase) in *AWS SDK for \.NET API Reference*\. 

### Get runs of a job<a name="glue_GetJobRuns_csharp_topic"></a>

The following code example shows how to get runs of an AWS Glue job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Retrieves information about an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The AWS Glue object for which to retrieve run
        /// information.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue job runs was retrieved successfully.</returns>
        public static async Task<bool> GetJobRunsAsync(AmazonGlueClient glueClient, string jobName)
        {
            var runsRequest = new GetJobRunsRequest
            {
                JobName = jobName,
                MaxResults = 20,
            };

            var response = await glueClient.GetJobRunsAsync(runsRequest);
            var jobRuns = response.JobRuns;

            if (jobRuns.Count > 0)
            {
                foreach (JobRun jobRun in jobRuns)
                {
                    Console.WriteLine($"Job run state is {jobRun.JobRunState}");
                    Console.WriteLine($"Job run Id is {jobRun.Id}");
                    Console.WriteLine($"The Glue version is {jobRun.GlueVersion}");
                }

                return true;
            }
            else
            {
                Console.WriteLine("No jobs found.");
                return false;
            }
        }
```
+  For API details, see [GetJobRuns](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetJobRuns) in *AWS SDK for \.NET API Reference*\. 

### Get tables from a database<a name="glue_GetTables_csharp_topic"></a>

The following code example shows how to get tables from a database in the AWS Glue Data Catalog\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Gets the tables used by the database for an AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue tables was retrieved successfully.</returns>
        public static async Task<bool> GetGlueTablesAsync(
            AmazonGlueClient glueClient,
            string dbName)
        {
            var tableRequest = new GetTablesRequest
            {
                DatabaseName = dbName,
            };

            // Get the list of AWS Glue databases.
            var response = await glueClient.GetTablesAsync(tableRequest);
            var tables = response.TableList;

            if (tables.Count > 0)
            {
                // Displays the list of table names.
                tables.ForEach(table => { Console.WriteLine($"Table name is: {table.Name}"); });
                return true;
            }
            else
            {
                Console.WriteLine("No tables found.");
                return false;
            }
        }
```
+  For API details, see [GetTables](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetTables) in *AWS SDK for \.NET API Reference*\. 

### Start a crawler<a name="glue_StartCrawler_csharp_topic"></a>

The following code example shows how to start an AWS Glue crawler\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Starts the named AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawler to start.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler
        /// was started successfully.</returns>
        public static async Task<bool> StartSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var crawlerRequest = new StartCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.StartCrawlerAsync(crawlerRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was successfully started!");
                return true;
            }

            Console.WriteLine($"Could not start AWS Glue crawler, {crawlerName}.");
            return false;
        }
```
+  For API details, see [StartCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/StartCrawler) in *AWS SDK for \.NET API Reference*\. 

### Start a job run<a name="glue_StartJobRun_csharp_topic"></a>

The following code example shows how to start an AWS Glue job run\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
  

```
        /// <summary>
        /// Starts an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized Glue client.</param>
        /// <param name="jobName">The name of the AWS Glue job to start.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job
        /// was started successfully.</returns>
        public static async Task<bool> StartJobAsync(AmazonGlueClient glueClient, string jobName)
        {
            var runRequest = new StartJobRunRequest
            {
                WorkerType = WorkerType.G1X,
                NumberOfWorkers = 10,
                JobName = jobName,
            };

            var response = await glueClient.StartJobRunAsync(runRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} successfully started. The job run id is {response.JobRunId}.");
                return true;
            }

            Console.WriteLine($"Could not start {jobName}.");
            return false;
        }
```
+  For API details, see [StartJobRun](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/StartJobRun) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w2aac21c17c13c27c15"></a>

### Get started running crawlers and jobs<a name="glue_Scenario_GetStartedCrawlersJobs_csharp_topic"></a>

The following code example shows how to:
+ Create and run a crawler that crawls a public Amazon Simple Storage Service \(Amazon S3\) bucket and generates a metadata database that describes the CSV\-formatted data it finds\.
+ List information about databases and tables in your AWS Glue Data Catalog\.
+ Create and run a job that extracts CSV data from the source Amazon S3 bucket, transforms it by removing and renaming fields, and loads JSON\-formatted output into another Amazon S3 bucket\.
+ List information about job runs and view some of the transformed data\.
+ Delete all resources created by the demo\.

For more information, see [Tutorial: Getting started with AWS Glue Studio](https://docs.aws.amazon.com/glue/latest/ug/tutorial-create-job.html)\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glue#code-examples)\. 
Create a class that wraps AWS Glue functions that are used in the scenario\.  

```
namespace Glue_Basics
{
    /// <summary>
    /// Methods for working the AWS Glue by using the AWS SDK for .NET (v3.7).
    /// </summary>
    public static class GlueMethods
    {

        /// <summary>
        /// Creates a database for use by an AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="dbName">The name of the new database.</param>
        /// <param name="locationUri">The location of scripts that will be
        /// used by the AWS Glue crawler.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue database
        /// was created successfully.</returns>
        public static async Task<bool> CreateDatabaseAsync(AmazonGlueClient glueClient, string dbName, string locationUri)
        {
            try
            {
                var dataBaseInput = new DatabaseInput
                {
                    Description = "Built with the AWS SDK for .NET (v3)",
                    Name = dbName,
                    LocationUri = locationUri,
                };

                var request = new CreateDatabaseRequest
                {
                    DatabaseInput = dataBaseInput,
                };

                var response = await glueClient.CreateDatabaseAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("The database was successfully created");
                    return true;
                }
                else
                {
                    Console.WriteLine("Could not create the database.");
                    return false;
                }
            }
            catch (AmazonGlueException ex)
            {
                Console.WriteLine($"Error occurred: '{ex.Message}'");
                return false;
            }
        }



        /// <summary>
        /// Creates an AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="iam">The Amazon Resource Name (ARN) of the IAM role
        /// that is used by the crawler.</param>
        /// <param name="s3Path">The path to the Amazon S3 bucket where
        /// data is stored.</param>
        /// <param name="cron">The name of the CRON job that runs the crawler.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="crawlerName">The name of the AWS Glue crawler.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler was
        /// created successfully.</returns>
        public static async Task<bool> CreateGlueCrawlerAsync(
            AmazonGlueClient glueClient,
            string iam,
            string s3Path,
            string cron,
            string dbName,
            string crawlerName)
        {
            var s3Target = new S3Target
            {
                Path = s3Path,
            };

            var targetList = new List<S3Target>
            {
                s3Target,
            };

            var targets = new CrawlerTargets
            {
                S3Targets = targetList,
            };

            var crawlerRequest = new CreateCrawlerRequest
            {
                DatabaseName = dbName,
                Name = crawlerName,
                Description = "Created by the AWS Glue .NET API",
                Targets = targets,
                Role = iam,
                Schedule = cron,
            };

            var response = await glueClient.CreateCrawlerAsync(crawlerRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was successfully created");
                return true;
            }
            else
            {
                Console.WriteLine($"Could not create {crawlerName}.");
                return false;
            }
        }



        /// <summary>
        /// Creates an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The name of the job to create.</param>
        /// <param name="iam">The Amazon Resource Name (ARN) of the IAM role
        /// that will be used by the job.</param>
        /// <param name="scriptLocation">The location where the script is stored.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job was
        /// created successfully.</returns>
        public static async Task<bool> CreateJobAsync(AmazonGlueClient glueClient, string jobName, string iam, string scriptLocation)
        {
            var command = new JobCommand
            {
                PythonVersion = "3",
                Name = "MyJob1",
                ScriptLocation = scriptLocation,
            };

            var jobRequest = new CreateJobRequest
            {
                Description = "A Job created by using the AWS SDK for .NET",
                GlueVersion = "2.0",
                WorkerType = WorkerType.G1X,
                NumberOfWorkers = 10,
                Name = jobName,
                Role = iam,
                Command = command,
            };

            var response = await glueClient.CreateJobAsync(jobRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} was successfully created.");
                return true;
            }

            Console.WriteLine($"{jobName} could not be created.");
            return false;
        }



        /// <summary>
        /// Deletes the named AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawler to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var deleteCrawlerRequest = new DeleteCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.DeleteCrawlerAsync(deleteCrawlerRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was deleted");
                return true;
            }

            Console.WriteLine($"Could not create {crawlerName}.");
            return false;
        }



        /// <summary>
        /// Deletes an AWS Glue database.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="databaseName">The name of the database to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue database was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteDatabaseAsync(AmazonGlueClient glueClient, string databaseName)
        {
            var request = new DeleteDatabaseRequest
            {
                Name = databaseName,
            };

            var response = await glueClient.DeleteDatabaseAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{databaseName} was successfully deleted");
                return true;
            }

            Console.WriteLine($"{databaseName} could not be deleted.");
            return false;
        }



        /// <summary>
        /// Deletes the named job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The name of the job to delete.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job was
        /// deleted successfully.</returns>
        public static async Task<bool> DeleteJobAsync(AmazonGlueClient glueClient, string jobName)
        {
            var jobRequest = new DeleteJobRequest
            {
                JobName = jobName,
            };

            var response = await glueClient.DeleteJobAsync(jobRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} was successfully deleted");
                return true;
            }

            Console.WriteLine($"{jobName} could not be deleted.");
            return false;
        }



        /// <summary>
        /// Gets a list of AWS Glue jobs.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <returns>A Boolean value indicating whether information about the
        /// AWS Glue jobs was retrieved successfully.</returns>
        /// <returns>A Boolean value indicating whether information about
        /// all AWS Glue jobs was retrieved.</returns>
        public static async Task<bool> GetAllJobsAsync(AmazonGlueClient glueClient)
        {
            var jobsRequest = new GetJobsRequest
            {
                MaxResults = 10,
            };

            var response = await glueClient.GetJobsAsync(jobsRequest);
            var jobs = response.Jobs;
            if (jobs.Count > 0)
            {
                jobs.ForEach(job => { Console.WriteLine($"The job name is: {job.Name}"); });
                return true;
            }
            else
            {
                Console.WriteLine("Didn't find any jobs.");
                return false;
            }
        }



        /// <summary>
        /// Gets the tables used by the database for an AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue tables was retrieved successfully.</returns>
        public static async Task<bool> GetGlueTablesAsync(
            AmazonGlueClient glueClient,
            string dbName)
        {
            var tableRequest = new GetTablesRequest
            {
                DatabaseName = dbName,
            };

            // Get the list of AWS Glue databases.
            var response = await glueClient.GetTablesAsync(tableRequest);
            var tables = response.TableList;

            if (tables.Count > 0)
            {
                // Displays the list of table names.
                tables.ForEach(table => { Console.WriteLine($"Table name is: {table.Name}"); });
                return true;
            }
            else
            {
                Console.WriteLine("No tables found.");
                return false;
            }
        }



        /// <summary>
        /// Retrieves information about an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="jobName">The AWS Glue object for which to retrieve run
        /// information.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue job runs was retrieved successfully.</returns>
        public static async Task<bool> GetJobRunsAsync(AmazonGlueClient glueClient, string jobName)
        {
            var runsRequest = new GetJobRunsRequest
            {
                JobName = jobName,
                MaxResults = 20,
            };

            var response = await glueClient.GetJobRunsAsync(runsRequest);
            var jobRuns = response.JobRuns;

            if (jobRuns.Count > 0)
            {
                foreach (JobRun jobRun in jobRuns)
                {
                    Console.WriteLine($"Job run state is {jobRun.JobRunState}");
                    Console.WriteLine($"Job run Id is {jobRun.Id}");
                    Console.WriteLine($"The Glue version is {jobRun.GlueVersion}");
                }

                return true;
            }
            else
            {
                Console.WriteLine("No jobs found.");
                return false;
            }
        }



        /// <summary>
        /// Retrieves information about a specific AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawer.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue crawler was retrieved successfully.</returns>
        public static async Task<bool> GetSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var crawlerRequest = new GetCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.GetCrawlerAsync(crawlerRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var databaseName = response.Crawler.DatabaseName;
                Console.WriteLine($"{crawlerName} has the database {databaseName}");
                return true;
            }

            Console.WriteLine($"No information regarding {crawlerName} could be found.");
            return false;
        }



        /// <summary>
        /// Gets information about the database created for this Glue
        /// example.
        /// </summary>
        /// <param name="glueClient">The initialized Glue client.</param>
        /// <param name="databaseName">The name of the AWS Glue database.</param>
        /// <returns>A Boolean value indicating whether information about
        /// the AWS Glue database was retrieved successfully.</returns>
        public static async Task<bool> GetSpecificDatabaseAsync(
            AmazonGlueClient glueClient,
            string databaseName)
        {
            var databasesRequest = new GetDatabaseRequest
            {
                Name = databaseName,
            };

            var response = await glueClient.GetDatabaseAsync(databasesRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"The Create Time is {response.Database.CreateTime}");
                return true;
            }

            Console.WriteLine($"No informaton about {databaseName}.");
            return false;
        }



        /// <summary>
        /// Starts an AWS Glue job.
        /// </summary>
        /// <param name="glueClient">The initialized Glue client.</param>
        /// <param name="jobName">The name of the AWS Glue job to start.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue job
        /// was started successfully.</returns>
        public static async Task<bool> StartJobAsync(AmazonGlueClient glueClient, string jobName)
        {
            var runRequest = new StartJobRunRequest
            {
                WorkerType = WorkerType.G1X,
                NumberOfWorkers = 10,
                JobName = jobName,
            };

            var response = await glueClient.StartJobRunAsync(runRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{jobName} successfully started. The job run id is {response.JobRunId}.");
                return true;
            }

            Console.WriteLine($"Could not start {jobName}.");
            return false;
        }



        /// <summary>
        /// Starts the named AWS Glue crawler.
        /// </summary>
        /// <param name="glueClient">The initialized AWS Glue client.</param>
        /// <param name="crawlerName">The name of the crawler to start.</param>
        /// <returns>A Boolean value indicating whether the AWS Glue crawler
        /// was started successfully.</returns>
        public static async Task<bool> StartSpecificCrawlerAsync(AmazonGlueClient glueClient, string crawlerName)
        {
            var crawlerRequest = new StartCrawlerRequest
            {
                Name = crawlerName,
            };

            var response = await glueClient.StartCrawlerAsync(crawlerRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"{crawlerName} was successfully started!");
                return true;
            }

            Console.WriteLine($"Could not start AWS Glue crawler, {crawlerName}.");
            return false;
        }

    }
}
```
Create a class that runs the scenario\.  

```
global using Amazon.Glue;
global using Amazon.Glue.Model;
global using Glue_Basics;


// This example uses .NET Core 6 and the AWS SDK for .NET (v3.7)
// Before running the code, set up your development environment,
// including your credentials. For more information, see the
// following topic:
//    https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config.html
//
// To set up the resources you need, see the following topic:
//    https://docs.aws.amazon.com/glue/latest/ug/tutorial-add-crawler.html
//
// This example performs the following tasks:
//    1. CreateDatabase
//    2. CreateCrawler
//    3. GetCrawler
//    4. StartCrawler
//    5. GetDatabase
//    6. GetTables
//    7. CreateJob
//    8. StartJobRun
//    9. ListJobs
//   10. GetJobRuns
//   11. DeleteJob
//   12. DeleteDatabase
//   13. DeleteCrawler

// Initialize the values that we need for the scenario.
// The Amazon Resource Name (ARN) of the service role used by the crawler.
var iam = "arn:aws:iam::012345678901:role/AWSGlueServiceRole-CrawlerTutorial";

// The path to the Amazon S3 bucket where the comma-delimited file is stored.
var s3Path = "s3://crawler-public-us-east-1/flight/2016/csv";

var cron = "cron(15 12 * * ? *)";

// The name of the database used by the crawler.
var dbName = "example-flights-db";

var crawlerName = "Flight Data Crawler";
var jobName = "glue-job34";
var scriptLocation = "s3://aws-glue-scripts-012345678901-us-west-1/GlueDemoUser";
var locationUri = "s3://crawler-public-us-east-1/flight/2016/csv/";

var glueClient = new AmazonGlueClient();
await GlueMethods.DeleteDatabaseAsync(glueClient, dbName);

Console.WriteLine("Creating the database and crawler for the AWS Glue example.");
var success = await GlueMethods.CreateDatabaseAsync(glueClient, dbName, locationUri);
success = await GlueMethods.CreateGlueCrawlerAsync(glueClient, iam, s3Path, cron, dbName, crawlerName);

// Get information about the AWS Glue crawler.
Console.WriteLine("Showing information about the newly created AWS Glue crawler.");
success = await GlueMethods.GetSpecificCrawlerAsync(glueClient, crawlerName);

Console.WriteLine("Starting the new AWS Glue crawler.");
success = await GlueMethods.StartSpecificCrawlerAsync(glueClient, crawlerName);

Console.WriteLine("Displaying information about the database used by the crawler.");
success = await GlueMethods.GetSpecificDatabaseAsync(glueClient, dbName);
success = await GlueMethods.GetGlueTablesAsync(glueClient, dbName);

Console.WriteLine("Creating a new AWS Glue job.");
success = await GlueMethods.CreateJobAsync(glueClient, jobName, iam, scriptLocation);

Console.WriteLine("Starting the new AWS Glue job.");
success = await GlueMethods.StartJobAsync(glueClient, jobName);

Console.WriteLine("Getting information about the AWS Glue job.");
success = await GlueMethods.GetAllJobsAsync(glueClient);
success = await GlueMethods.GetJobRunsAsync(glueClient, jobName);

Console.WriteLine("Deleting the AWS Glue job used by the exmple.");
success = await GlueMethods.DeleteJobAsync(glueClient, jobName);

Console.WriteLine("\n*** Waiting 5 MIN for the " + crawlerName + " to stop. ***");
System.Threading.Thread.Sleep(300000);

Console.WriteLine("Clean up the resources created for the example.");
success = await GlueMethods.DeleteDatabaseAsync(glueClient, dbName);
success = await GlueMethods.DeleteSpecificCrawlerAsync(glueClient, crawlerName);

Console.WriteLine("Successfully completed the AWS Glue Scenario ");
```
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [CreateCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/CreateCrawler)
  + [CreateJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/CreateJob)
  + [DeleteCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteCrawler)
  + [DeleteDatabase](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteDatabase)
  + [DeleteJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteJob)
  + [DeleteTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/DeleteTable)
  + [GetCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetCrawler)
  + [GetDatabase](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetDatabase)
  + [GetDatabases](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetDatabases)
  + [GetJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetJob)
  + [GetJobRun](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetJobRun)
  + [GetJobRuns](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetJobRuns)
  + [GetTables](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/GetTables)
  + [ListJobs](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/ListJobs)
  + [StartCrawler](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/StartCrawler)
  + [StartJobRun](https://docs.aws.amazon.com/goto/DotNetSDKV3/glue-2017-03-31/StartJobRun)