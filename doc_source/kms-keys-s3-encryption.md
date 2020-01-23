# Using AWS KMS Keys with the AmazonS3EncryptionClient in the AWS SDK for \.NET<a name="kms-keys-s3-encryption"></a>

The [AmazonS3EncryptionClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3EncryptionClient.html) class implements the same interface as the standard `AmazonS3Client`\. This means it’s easy to switch to the `AmazonS3EncryptionClient` class\. In fact, your application code won’t be aware of the encryption and decryption happening automatically in the client\.

You can use an AWS KMS key as your master key when you use the `AmazonS3EncryptionClient` class for client\-side encryption\. All you have to do is create an `EncryptionMaterials` object that contains a KMS key ID\. Then you pass the `EncryptionMaterials` object to the constructor of the `AmazonS3EncryptionClient`\.

The main advantage of using an AWS KMS key as your master key is that you don’t need to store and manage your own master keys\. It’s done by AWS\. A second advantage is that it makes the AWS SDK for \.NET’s `AmazonS3EncryptionClient` class interoperable with the AWS SDK for Java’s `AmazonS3EncryptionClient` class\. This means you can encrypt with the AWS SDK for Java and decrypt with the AWS SDK for \.NET, and vice versa\.

**Note**  
The AWS SDK for \.NET’s `AmazonS3EncryptionClient` supports KMS master keys only when run in metadata mode\. The instruction file mode of the AWS SDK for \.NET’s `AmazonS3EncryptionClient` is still incompatible with the AWS SDK for Java’s `AmazonS3EncryptionClient`\.

For more information about client\-side encryption with the AmazonS3EncryptionClient class, and how envelope encryption works, see [Client Side Data Encryption with AWS SDK for \.NET and Amazon S3](http://aws.amazon.com/blogs/developer/client-side-data-encryption-with-aws-sdk-for-net-and-amazon-s3/)\.

The following example demonstrates how to use AWS KMS keys with the AmazonS3EncryptionClient class\.

In Visual Studio, create a **Console App \(\.NET Framework\)** project using \.NET Framework 4\.5 or later and reference the appropriate versions of the following NuGet packages: **AWSSDK\.S3** and **AWSSDK\.KeyManagementService**\. When you run the application, include the Region code, the name of an existing Amazon S3 bucket, and a name for the new Amazon S3 object\.

```
using System;
using System.IO;
using System.Threading.Tasks;

using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Amazon.S3.Encryption;
using Amazon.S3.Model;

namespace KmsS3Encryption
{
    class S3Sample
    {
        static async Task<CreateKeyResponse> MyCreateKeyAsync(string regionName)
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(regionName);

            AmazonKeyManagementServiceClient kmsClient = new AmazonKeyManagementServiceClient(region);

            CreateKeyResponse response = await kmsClient.CreateKeyAsync(new CreateKeyRequest());

            return response;
        }

        static async Task<GetObjectResponse> MyPutObjectAsync(EncryptionMaterials materials, string bucketName, string keyName)
        {
            // CryptoStorageMode.ObjectMetadata is required for KMS EncryptionMaterials
            var config = new AmazonS3CryptoConfiguration()
            {
                StorageMode = CryptoStorageMode.ObjectMetadata
            };

            AmazonS3EncryptionClient s3Client = new AmazonS3EncryptionClient(config, materials);

            // encrypt and put object
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                ContentBody = "object content"
            };

            await s3Client.PutObjectAsync(putRequest);

            // get object and decrypt
            var getRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            GetObjectResponse response = await s3Client.GetObjectAsync(getRequest);

            return response;
        }
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You must supply a Region, bucket name, and item name:");
                Console.WriteLine("Usage: KmsS3Encryption REGION BUCKET ITEM");
                return;
            }

            string regionName = args[0];
            string bucketName = args[1];
            string itemName = args[2];

            Task<CreateKeyResponse> response = MyCreateKeyAsync(regionName);

            KeyMetadata keyMetadata = response.Result.KeyMetadata;
            string kmsKeyId = keyMetadata.KeyId;

            // An object that contains information about the CMK created by this operation.
            EncryptionMaterials kmsEncryptionMaterials = new EncryptionMaterials(kmsKeyId);

            Task<GetObjectResponse> goResponse = MyPutObjectAsync(kmsEncryptionMaterials, bucketName, itemName);

            Stream stream = goResponse.Result.ResponseStream;
            StreamReader reader = new StreamReader(stream);

            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
```

See the [complete example](https://github.com/awsdocs/aws-doc-sdk-examples/blob/master/dotnet/example_code/KMS/KmsS3Encryption.cs) on GitHub\.