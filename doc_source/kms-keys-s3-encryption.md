# Using AWS Key Management Service keys for Amazon S3 encryption in the AWS SDK for \.NET<a name="kms-keys-s3-encryption"></a>

The [AmazonS3EncryptionClientV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.AmazonS3EncryptionClientV2.html) class implements the same interface as the standard `AmazonS3Client`\. This means it's easy to switch to the `AmazonS3EncryptionClientV2` class\. In fact, your application code won’t be aware of the encryption and decryption happening automatically in the client\.

You can use an AWS KMS key as your master key when you use the `AmazonS3EncryptionClientV2` class for client\-side encryption\. All you have to do is create an `EncryptionMaterials` object that contains a KMS key ID\. Then you pass the `EncryptionMaterials` object to the constructor of the `AmazonS3EncryptionClientV2`\.

One advantage of using an AWS KMS key as your master key is that you don't need to store and manage your own master keys; this is done by AWS\. A second advantage is that the `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET is interoperable with the `AmazonS3EncryptionClientV2` class of the AWS SDK for Java\. This means you can encrypt with the AWS SDK for Java and decrypt with the AWS SDK for \.NET, and vice versa\.

**Note**  
The `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET supports KMS master keys only when run in metadata mode\. The instruction file mode of the `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET is incompatible with the `AmazonS3EncryptionClientV2` class of the AWS SDK for Java\.

**Warning**  
The `AmazonS3EncryptionClient` class of the AWS SDK for \.NET is deprecated and is less secure than the `AmazonS3EncryptionClientV2` class\. To migrate existing code that uses `AmazonS3EncryptionClient`, see [S3 Encryption Client Migration](s3-encryption-migration.md)\.

For more information about client\-side encryption with the `AmazonS3EncryptionClientV2` class, and how envelope encryption works, see [Client Side Data Encryption with AWS SDK for \.NET and Amazon S3](http://aws.amazon.com/blogs/developer/client-side-data-encryption-with-aws-sdk-for-net-and-amazon-s3/)\.

The following example demonstrates how to use AWS KMS keys with the `AmazonS3EncryptionClientV2` class\.

Create a new console project\. Reference the appropriate versions of the following [NuGet packages](https://www.nuget.org/profiles/awsdotnet): **AWSSDK\.S3**, **AWSSDK\.KeyManagementService**, and **Amazon\.Extensions\.S3\.Encryption**\. When you run the application, include the Region code, the name of an existing Amazon S3 bucket, and a name for the new Amazon S3 object\.

The application creates an encrypted object in the given bucket, then decrypts and displays the contents of the object\.

```
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Amazon.Extensions.S3.Encryption;
using Amazon.Extensions.S3.Encryption.Primitives;
using Amazon.S3.Model;

namespace KmsS3Encryption
{
  class S3Sample
  {
    public static async Task Main(string[] args)
    {
      if (args.Length != 3)
      {
        Console.WriteLine("\nUsage: KmsS3Encryption REGION BUCKET ITEM");
        Console.WriteLine("  REGION: The AWS Region (for example, \"us-west-1\").");
        Console.WriteLine("  BUCKET: The name of an existing S3 bucket.");
        Console.WriteLine("  ITEM: The name you want to use for the item.");
        return;
      }
      string regionName = args[0];
      string bucketName = args[1];
      string itemName = args[2];

      // Create a customer master key (CMK) and store the result
      var createKeyResponse = await MyCreateKeyAsync(regionName);
      var kmsEncryptionContext = new Dictionary<string, string>();
      var kmsEncryptionMaterials = new EncryptionMaterialsV2(
        createKeyResponse.KeyMetadata.KeyId, KmsType.KmsContext, kmsEncryptionContext);

      // Create the object in the bucket, then display the content of the object
      var putObjectResponse =
        await CreateAndRetrieveObjectAsync(kmsEncryptionMaterials, bucketName, itemName);
      Stream stream = putObjectResponse.ResponseStream;
      StreamReader reader = new StreamReader(stream);
      Console.WriteLine(reader.ReadToEnd());
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }


    //
    // Method to create a customer master key
    static async Task<CreateKeyResponse> MyCreateKeyAsync(string regionName)
    {
      var kmsClient = new AmazonKeyManagementServiceClient(
        RegionEndpoint.GetBySystemName(regionName));
      return await kmsClient.CreateKeyAsync(new CreateKeyRequest());
    }


    //
    // Method to create and encrypt an object in an S3 bucket
    static async Task<GetObjectResponse> CreateAndRetrieveObjectAsync(
      EncryptionMaterialsV2 materials, string bucketName, string keyName)
    {
      // CryptoStorageMode.ObjectMetadata is required for KMS EncryptionMaterials
      var config = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
      {
        StorageMode = CryptoStorageMode.ObjectMetadata
      };
      var s3Client = new AmazonS3EncryptionClientV2(config, materials);

      // Create, encrypt, and put the object
      await s3Client.PutObjectAsync(new PutObjectRequest
      {
        BucketName = bucketName,
        Key = keyName,
        ContentBody = "Object content for KmsS3Encryption example."
      });

      // Get, decrypt, and return the object
      return await s3Client.GetObjectAsync(new GetObjectRequest
      {
        BucketName = bucketName,
        Key = keyName
      });
    }
  }
}
```