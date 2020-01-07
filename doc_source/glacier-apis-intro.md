--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Amazon S3 Glacier Programming with the AWS SDK for \.NET<a name="glacier-apis-intro"></a>

The AWS SDK for \.NET supports Amazon S3 Glacier, which is a storage service optimized for infrequently used data, or “cold data\.” The service provides durable and extremely low\-cost storage with security features for data archiving and backup\. For more information, see [S3 Glacier](https://aws.amazon.com/glacier)\.

The following information introduces you to the Glacier programming models in the the SDK\.

## Programming Models<a name="glacier-apis-intro-models"></a>

The the SDK provides three programming models for working with Glacier\. These programming models are known as the *low\-level*, *high\-level*, and *resource* models\. The following information describes these models, why you would want to use them, and how to use them\.

### Low\-Level APIs<a name="glacier-apis-intro-low-level"></a>

The the SDK provides low\-level APIs for programming with Glacier\. These low\-level APIs map closely the underlying REST API supported by Glacier\. For each Glacier REST operation, the low\-level APIs provide a corresponding method, a request object for you to provide request information, and a response object for you to process the Glacier response\. The low\-level APIs are the most complete implementation of the underlying Glacier operations\.

The following example shows how to use the low\-level APIs to list accessible vaults in Glacier:

```
// using Amazon.Glacier;
// using Amazon.Glacier.Model;

var client = new AmazonGlacierClient();
var request = new ListVaultsRequest();
var response = client.ListVaults(request);

foreach (var vault in response.VaultList)
{
  Console.WriteLine("Vault: {0}", vault.VaultName);
  Console.WriteLine("  Creation date: {0}", vault.CreationDate);
  Console.WriteLine("  Size in bytes: {0}", vault.SizeInBytes);
  Console.WriteLine("  Number of archives: {0}", vault.NumberOfArchives);
  
  try 
  {
    var requestNotifications = new GetVaultNotificationsRequest
    {
      VaultName = vault.VaultName
    };
    var responseNotifications = 
      client.GetVaultNotifications(requestNotifications);

    Console.WriteLine("  Notifications:");
    Console.WriteLine("    Topic: {0}", 
      responseNotifications.VaultNotificationConfig.SNSTopic);

    var events = responseNotifications.VaultNotificationConfig.Events;

    if (events.Any())
    {
      Console.WriteLine("    Events:");

      foreach (var e in events)
      {
        Console.WriteLine("{0}", e);
      }
    }
    else
    {
      Console.WriteLine("    No events set.");
    }
    
  }
  catch (ResourceNotFoundException)
  {
    Console.WriteLine("  No notifications set.");
  }
    
  var requestJobs = new ListJobsRequest{
    VaultName = vault.VaultName
  };
  var responseJobs = client.ListJobs(requestJobs);
  var jobs = responseJobs.JobList;
  
  if (jobs.Any())
  {
    Console.WriteLine("  Jobs:");

    foreach (var job in jobs)
    {
      Console.WriteLine("    For job ID: {0}", 
        job.JobId);
      Console.WriteLine("Archive ID: {0}", 
        job.ArchiveId);
      Console.WriteLine("Archive size in bytes: {0}", 
        job.ArchiveSizeInBytes.ToString());
      Console.WriteLine("Completed: {0}", 
        job.Completed);
      Console.WriteLine("Completion date: {0}", 
        job.CompletionDate);
      Console.WriteLine("Creation date: {0}", 
        job.CreationDate);
      Console.WriteLine("Inventory size in bytes: {0}", 
        job.InventorySizeInBytes);
      Console.WriteLine("Job description: {0}", 
        job.JobDescription);
      Console.WriteLine("Status code: {0}", 
        job.StatusCode.Value);
      Console.WriteLine("Status message: {0}", 
        job.StatusMessage);
    }

  }
  else
  {
    Console.WriteLine("  No jobs.");
  }

}
```

For additional examples, see:
+  [Using the AWS SDK for \.NET](https://docs.aws.amazon.com/amazonglacier/latest/dev/using-aws-sdk-for-dot-net.html.html) 
+  [Creating a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/creating-vaults-dotnet-sdk.html#create-vault-dotnet-lowlevel.html.html) 
+  [Retrieving Vault Metadata](https://docs.aws.amazon.com/amazonglacier/latest/dev/retrieving-vault-info-sdk-dotnet.html.html) 
+  [Downloading a Vault Inventory](https://docs.aws.amazon.com/amazonglacier/latest/dev/retrieving-vault-inventory-sdk-dotnet.html.html) 
+  [Configuring Vault Notifications](https://docs.aws.amazon.com/amazonglacier/latest/dev/configuring-notifications-sdk-dotnet.html.html) 
+  [Deleting a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-vaults-sdk-dotnet.html#deleting-vault-sdk-dotnet-low-level.html) 
+  [Uploading an Archive in a Single Operation](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-single-op-using-dotnet.html#uploading-an-archive-single-op-lowlevel-using-dotnet.html) 
+  [Uploading Large Archives in Parts](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-mpu-using-dotnet.html#uploading-an-archive-in-parts-lowlevel-using-dotnet.html) 
+  [Downloading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/downloading-an-archive-using-dotnet.html#downloading-an-archive-using-dotnet-lowlevel-api.html) 
+  [Deleting an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-an-archive-using-dot-net.html#delete-archive-using-dot-net-low-level.html) 

For related API reference information, see `Amazon.Glacier` and `Amazon.Glacier.Model` in the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/)\.

### High\-Level APIs<a name="glacier-apis-intro-high-level"></a>

The the SDK provides high\-level APIs for programming with Glacier\. To further simplify application development, these high\-level APIs offer a higher\-level abstraction for some of the operations, including uploading an archive and downloading an archive or vault inventory\.

For examples, see:
+  [Using the AWS SDK for \.NET](https://docs.aws.amazon.com/amazonglacier/latest/dev/using-aws-sdk-for-dot-net.html.html) 
+  [Creating a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/creating-vaults-dotnet-sdk.html#create-vault-dotnet-highlevel.html) 
+  [Deleting a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-vaults-sdk-dotnet.html#deleting-vault-sdk-dotnet-high-level.html) 
+  [Upload an Archive to a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-upload-archive-dotnet.html.html) 
+  [Uploading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-single-op-using-dotnet.html#uploading-an-archive-single-op-highlevel-using-dotnet.html) 
+  [Uploading Large Archives in Parts](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-mpu-using-dotnet.html#uploading-an-archive-in-parts-highlevel-using-dotnet.html) 
+  [Download an Archive from a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-download-archive-dotnet.html.html) 
+  [Downloading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/downloading-an-archive-using-dotnet.html#downloading-an-archive-using-dotnet-highlevel-api.html) 
+  [Delete an Archive from a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-delete-archive-dotnet.html.html) 
+  [Deleting an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-an-archive-using-dot-net.html#delete-archive-using-dot-net-high-level.html) 

For related API reference information, see [Amazon\.Glacier\.Transfer](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NGlacierTransfer.html) in the AWS SDK for \.NET API Reference\.

### Resource APIs<a name="glacier-apis-intro-resource-level"></a>

The the SDK provides the AWS Resource APIs for \.NET for programming with Glacier\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with Glacier resources as compared to their low\-level and high\-level API counterparts\. \(For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.\)

The following example shows how to use the AWS Resource APIs for \.NET to list accessible vaults in Glacier:

```
// using Amazon.Glacier.Resources;
// using Amazon.Runtime.Resources;

var g = new Glacier();

foreach (var vault in g.GetVaults())
{
  Console.WriteLine("Vault: {0}", vault.Name);
  Console.WriteLine("  Creation date: {0}", vault.CreationDate);
  Console.WriteLine("  Size in bytes: {0}", vault.SizeInBytes);
  Console.WriteLine("  Number of archives: {0}", vault.NumberOfArchives);

  try
  {
    var n = vault.GetNotification();

    Console.WriteLine("  Notifications:");
    Console.WriteLine("    Topic: {0}", n.SNSTopic);

    var events = n.Events;

    if (events.Any())
    {
      Console.WriteLine("    Events:");

      foreach (var e in events)
      {
        Console.WriteLine("{0}", e);
      }
    }
    else
    {
      Console.WriteLine("    No events set.");
    }

  }
  catch (ResourceLoadException)
  {
    Console.WriteLine("    No notifications set.");
  }

  var jobs = vault.GetJobs();

  if (jobs.Any())
  {
    Console.WriteLine("  Jobs:");

    foreach (var job in jobs)
    {
      Console.WriteLine("    For job ID: {0}",
        job.Id);
      Console.WriteLine("Archive ID: {0}",
        job.ArchiveId);
      Console.WriteLine("Archive size in bytes: {0}",
        job.ArchiveSizeInBytes.ToString());
      Console.WriteLine("Completed: {0}",
        job.Completed);
      Console.WriteLine("Completion date: {0}",
        job.CompletionDate);
      Console.WriteLine("Creation date: {0}",
        job.CreationDate);
      Console.WriteLine("Inventory size in bytes: {0}",
        job.InventorySizeInBytes);
      Console.WriteLine("Job description: {0}",
        job.JobDescription);
      Console.WriteLine("Status code: {0}",
        job.StatusCode.Value);
      Console.WriteLine("Status message: {0}",
        job.StatusMessage);
    }

  }
  else
  {
    Console.WriteLine("  No jobs.");
  }

}
```

For related API reference information, see [Amazon\.Glacier\.Resources](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NGlacierResourcesNET45.html)\.