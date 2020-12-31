--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Storing Archival Data Using Amazon S3 Glacier<a name="glacier-apis-intro"></a>

The AWS SDK for \.NET supports Amazon S3 Glacier, which is a storage service optimized for infrequently used data, or *cold data*\. The service provides durable and extremely low\-cost storage with security features for data archiving and backup\. For more information, see [Amazon S3 Glacier Developer Guide](https://docs.aws.amazon.com/amazonglacier/latest/dev/)\.

The following information introduces you to the S3 Glacier programming models in the AWS SDK for \.NET\.

## Programming Models<a name="glacier-apis-intro-models"></a>

The AWS SDK for \.NET provides two programming models for working with S3 Glacier\. The following information describes these models and why and how to use them\.

**Topics**
+ [Low\-Level APIs](#glacier-apis-intro-low-level)
+ [High\-Level APIs](#glacier-apis-intro-high-level)

### Low\-Level APIs<a name="glacier-apis-intro-low-level"></a>

The AWS SDK for \.NET provides low\-level APIs for programming with S3 Glacier\. These low\-level APIs map closely to the underlying REST API supported by S3 Glacier\. For each S3 Glacier REST operation, the low\-level APIs provide a corresponding method, a request object for you to provide request information, and a response object for you to process the S3 Glacier response\. The low\-level APIs are the most complete implementation of the underlying S3 Glacier operations\.

The following example shows how to use the low\-level APIs to list accessible vaults in S3 Glacier:

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

For more examples, see:
+  [Using the AWS SDK for \.NET](https://docs.aws.amazon.com/amazonglacier/latest/dev/using-aws-sdk-for-dot-net.html) 
+  [Creating a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/creating-vaults-dotnet-sdk.html#create-vault-dotnet-lowlevel.html) 
+  [Retrieving Vault Metadata](https://docs.aws.amazon.com/amazonglacier/latest/dev/retrieving-vault-info-sdk-dotnet.html) 
+  [Downloading a Vault Inventory](https://docs.aws.amazon.com/amazonglacier/latest/dev/retrieving-vault-inventory-sdk-dotnet.html) 
+  [Configuring Vault Notifications](https://docs.aws.amazon.com/amazonglacier/latest/dev/configuring-notifications-sdk-dotnet.html) 
+  [Deleting a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-vaults-sdk-dotnet.html) 
+  [Uploading an Archive in a Single Operation](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-single-op-using-dotnet.html) 
+  [Uploading Large Archives in Parts](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-mpu-using-dotnet.html) 
+  [Downloading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/downloading-an-archive-using-dotnet.html) 
+  [Deleting an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-an-archive-using-dot-net.html) 

For related API reference information, see [Amazon\.Glacier](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/NGlacier.html) and [Amazon\.Glacier](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/NGlacierModel.html)\.

### High\-Level APIs<a name="glacier-apis-intro-high-level"></a>

The AWS SDK for \.NET provides high\-level APIs for programming with S3 Glacier\. To further simplify application development, these high\-level APIs offer a higher\-level abstraction for some of the operations, including uploading an archive and downloading an archive or vault inventory\.

For examples, see the following topics in the [Amazon S3 Glacier Developer Guide](https://docs.aws.amazon.com/amazonglacier/latest/dev/):
+  [Using the AWS SDK for \.NET](https://docs.aws.amazon.com/amazonglacier/latest/dev/using-aws-sdk-for-dot-net.html) 
+  [Creating a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/creating-vaults-dotnet-sdk.html) 
+  [Deleting a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-vaults-sdk-dotnet.html) 
+  [Upload an Archive to a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-upload-archive-dotnet.html) 
+  [Uploading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-single-op-using-dotnet.html) 
+  [Uploading Large Archives in Parts](https://docs.aws.amazon.com/amazonglacier/latest/dev/uploading-an-archive-mpu-using-dotnet.html) 
+  [Download an Archive from a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-download-archive-dotnet.html) 
+  [Downloading an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/downloading-an-archive-using-dotnet.html) 
+  [Delete an Archive from a Vault](https://docs.aws.amazon.com/amazonglacier/latest/dev/getting-started-delete-archive-dotnet.html) 
+  [Deleting an Archive](https://docs.aws.amazon.com/amazonglacier/latest/dev/deleting-an-archive-using-dot-net.html) 

For related API reference information, see [Amazon\.Glacier\.Transfer](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/NGlacierTransfer.html) in the *AWS SDK for \.NET API Reference*\.