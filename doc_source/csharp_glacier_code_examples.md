# S3 Glacier examples using AWS SDK for \.NET<a name="csharp_glacier_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with S3 Glacier\.

*Actions* are code excerpts that show you how to call individual service functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple functions within the same service\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w2aac21c17c13c51c13)

## Actions<a name="w2aac21c17c13c51c13"></a>

### Add tags<a name="glacier_AddTagsToVault_csharp_topic"></a>

The following code example shows how to add tags to an Amazon S3 Glacier vault\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    public class AddTagsToVault
    {
        public static async Task Main(string[] args)
        {
            string vaultName = "example-vault";

            var client = new AmazonGlacierClient();
            var request = new AddTagsToVaultRequest
            {
                Tags = new Dictionary<string, string>
                {
                    { "examplekey1", "examplevalue1" },
                    { "examplekey2", "examplevalue2" },
                },
                AccountId = "-",
                VaultName = vaultName,
            };

            var response = await client.AddTagsToVaultAsync(request);
        }
    }
```
+  For API details, see [AddTagsToVault](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/AddTagsToVault) in *AWS SDK for \.NET API Reference*\. 

### Create a vault<a name="glacier_CreateVault_csharp_topic"></a>

The following code example shows how to create an Amazon S3 Glacier vault\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    public class CreateVault
    {
        static async Task Main(string[] args)
        {
            string vaultName = "example-vault";
            var client = new AmazonGlacierClient();
            var request = new CreateVaultRequest
            {
                // Setting the AccountId to "-" means that
                // the account associated with the default
                // client will be used.
                AccountId = "-",
                VaultName = vaultName,
            };

            var response = await client.CreateVaultAsync(request);

            Console.WriteLine($"Created {vaultName} at: {response.Location}");
        }
    }
```
+  For API details, see [CreateVault](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/CreateVault) in *AWS SDK for \.NET API Reference*\. 

### Describe a job<a name="glacier_DescribeJob_csharp_topic"></a>

The following code example shows how to describe an Amazon S3 Glacier job\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    public class DescribeVault
    {
        public static async Task Main(string[] args)
        {
            string vaultName = "example-vault";
            var client = new AmazonGlacierClient();
            var request = new DescribeVaultRequest
            {
                AccountId = "-",
                VaultName = vaultName,
            };

            var response = await client.DescribeVaultAsync(request);

            // Display the information about the vault.
            Console.WriteLine($"{response.VaultName}\tARN: {response.VaultARN}");
            Console.WriteLine($"Created on: {response.CreationDate}\tNumber of Archives: {response.NumberOfArchives}\tSize (in bytes): {response.SizeInBytes}");
            if (response.LastInventoryDate != DateTime.MinValue)
            {
                Console.WriteLine($"Last inventory: {response.LastInventoryDate}");
            }
        }
    }
```
+  For API details, see [DescribeJob](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/DescribeJob) in *AWS SDK for \.NET API Reference*\. 

### Download an archive<a name="glacier_DownloadArchive_csharp_topic"></a>

The following code example shows how to download an Amazon S3 Glacier archive\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using Amazon;
    using Amazon.Glacier;
    using Amazon.Glacier.Transfer;
    using Amazon.Runtime;

    class DownloadArchiveHighLevel
    {
        private static readonly string VaultName = "examplevault";
        private static readonly string ArchiveId = "*** Provide archive ID ***";
        private static readonly string DownloadFilePath = "*** Provide the file name and path to where to store the download ***";
        private static int currentPercentage = -1;

        static void Main()
        {
            try
            {
                var manager = new ArchiveTransferManager(RegionEndpoint.USEast2);

                var options = new DownloadOptions
                {
                    StreamTransferProgress = Progress,
                };

                // Download an archive.
                Console.WriteLine("Intiating the archive retrieval job and then polling SQS queue for the archive to be available.");
                Console.WriteLine("Once the archive is available, downloading will begin.");
                manager.DownloadAsync(VaultName, ArchiveId, DownloadFilePath, options);
                Console.WriteLine("To continue, press Enter");
                Console.ReadKey();
            }
            catch (AmazonGlacierException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("To continue, press Enter");
            Console.ReadKey();
        }

        static void Progress(object sender, StreamTransferProgressArgs args)
        {
            if (args.PercentDone != currentPercentage)
            {
                currentPercentage = args.PercentDone;
                Console.WriteLine("Downloaded {0}%", args.PercentDone);
            }
        }
    }
```

### List jobs<a name="glacier_ListJobs_csharp_topic"></a>

The following code example shows how to list Amazon S3 Glacier jobs\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    class ListJobs
    {
        static async Task Main(string[] args)
        {
            var client = new AmazonGlacierClient();
            var vaultName = "example-vault";

            var request = new ListJobsRequest
            {
                // Using a hyphen "=" for the Account Id will
                // cause the SDK to use the Account Id associated
                // with the default user.
                AccountId = "-",
                VaultName = vaultName,
            };

            var response = await client.ListJobsAsync(request);

            if (response.JobList.Count > 0)
            {
                response.JobList.ForEach(job => {
                    Console.WriteLine($"{job.CreationDate} {job.JobDescription}");
                });
            }
            else
            {
                Console.WriteLine($"No jobs were found for {vaultName}.");
            }
        }
    }
```
+  For API details, see [ListJobs](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/ListJobs) in *AWS SDK for \.NET API Reference*\. 

### List tags<a name="glacier_ListTagsForVault_csharp_topic"></a>

The following code example shows how to list tags for an Amazon S3 Glacier vault\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    class ListTagsForVault
    {
        static async Task Main(string[] args)
        {
            var client = new AmazonGlacierClient();
            var vaultName = "example-vault";

            var request = new ListTagsForVaultRequest
            {
                // Using a hyphen "=" for the Account Id will
                // cause the SDK to use the Account Id associated
                // with the default user.
                AccountId = "-",
                VaultName = vaultName,
            };

            var response = await client.ListTagsForVaultAsync(request);

            if (response.Tags.Count > 0)
            {
                foreach (KeyValuePair<string, string> tag in response.Tags)
                {
                    Console.WriteLine($"Key: {tag.Key}, value: {tag.Value}");
                }
            }
            else
            {
                Console.WriteLine($"{vaultName} has no tags.");
            }
        }
    }
```
+  For API details, see [ListTagsForVault](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/ListTagsForVault) in *AWS SDK for \.NET API Reference*\. 

### List vaults<a name="glacier_ListVaults_csharp_topic"></a>

The following code example shows how to list Amazon S3 Glacier vaults\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.Glacier;
    using Amazon.Glacier.Model;

    public class ListVaults
    {
        public static async Task Main(string[] args)
        {
            var client = new AmazonGlacierClient();
            var request = new ListVaultsRequest
            {
                AccountId = "-",
                Limit = 5,
            };

            var response = await client.ListVaultsAsync(request);

            List<DescribeVaultOutput> vaultList = response.VaultList;

            vaultList.ForEach(v => { Console.WriteLine($"{v.VaultName} ARN: {v.VaultARN}"); });
        }
    }
```
+  For API details, see [ListVaults](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/ListVaults) in *AWS SDK for \.NET API Reference*\. 

### Upload an archive to a vault<a name="glacier_UploadArchive_csharp_topic"></a>

The following code example shows how to upload an archive to an Amazon S3 Glacier vault\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Glacier#code-examples)\. 
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.Glacier;
    using Amazon.Glacier.Transfer;

    public class UploadArchiveHighLevel
    {
        private static readonly string VaultName = "example-vault";
        private static readonly string ArchiveToUpload = "*** Provide file name (with full path) to upload ***";

        public static async Task Main()
        {
            try
            {
                var manager = new ArchiveTransferManager(RegionEndpoint.USWest2);

                // Upload an archive.
                var response = await manager.UploadAsync(VaultName, "upload archive test", ArchiveToUpload);

                Console.WriteLine("Copy and save the ID for use in other examples.");
                Console.WriteLine($"Archive ID: {response.ArchiveId}");
                Console.WriteLine("To continue, press Enter");
                Console.ReadKey();
            }
            catch (AmazonGlacierException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
```
+  For API details, see [UploadArchive](https://docs.aws.amazon.com/goto/DotNetSDKV3/glacier-2012-06-01/UploadArchive) in *AWS SDK for \.NET API Reference*\. 