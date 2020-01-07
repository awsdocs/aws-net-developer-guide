# Using AWS KMS Keys with the AmazonS3EncryptionClient in the AWS SDK for \.NET<a name="kms-keys-s3-encryption"></a>

The [AmazonS3EncryptionClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3EncryptionClient.html) class implements the same interface as the standard `AmazonS3Client`\. This means it’s easy to switch to the `AmazonS3EncryptionClient` class\. In fact, your application code won’t be aware of the encryption and decryption happening automatically in the client\.

You can use an AWS KMS key as your master key when you use the `AmazonS3EncryptionClient` class for client\-side encryption\. All you have to do is create an `EncryptionMaterials` object that contains a KMS key ID\. Then you pass the `EncryptionMaterials` object to the constructor of the `AmazonS3EncryptionClient`\.

The main advantage of using an AWS KMS key as your master key is that you don’t need to store and manage your own master keys\. It’s done by AWS\. A second advantage is that it makes the AWS SDK for \.NET’s `AmazonS3EncryptionClient` class interoperable with the AWS SDK for Java’s `AmazonS3EncryptionClient` class\. This means you can encrypt with the AWS SDK for Java and decrypt with the AWS SDK for \.NET, and vice versa\.

**Note**  
The AWS SDK for \.NET’s `AmazonS3EncryptionClient` supports KMS master keys only when run in metadata mode\. The instruction file mode of the AWS SDK for \.NET’s `AmazonS3EncryptionClient` is still incompatible with the AWS SDK for Java’s `AmazonS3EncryptionClient`\.

For more information about client\-side encryption with the AmazonS3EncryptionClient class, and how envelope encryption works, see [Client Side Data Encryption with AWS SDK for \.NET and Amazon S3](http://aws.amazon.com/blogs/developer/client-side-data-encryption-with-aws-sdk-for-net-and-amazon-s3/)\.

The following example demonstrates how to use AWS KMS keys with the AmazonS3EncryptionClient class\. Your project must reference the latest version of the `AWSSDK.KeyManagementService` Nuget package to use this feature\. Don’t forget to set the `region`, `bucketName`, and `objectKey` variables to appropriate values\.

```
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Encryption;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace S3Sample1
{
    class S3Sample
    {
        public static void Main(string[] args)
        {
            string kmsKeyID = null;
            using (var kmsClient = new AmazonKeyManagementServiceClient())
            {
                var response = kmsClient.CreateKey(new CreateKeyRequest());
                kmsKeyID = response.KeyMetadata.KeyId;

                var keyMetadata = response.KeyMetadata; // An object that contains information about the CMK created by this operation.
                var bucketName = "<s3bucket>";
                var objectKey = "key";

                var kmsEncryptionMaterials = new EncryptionMaterials(kmsKeyID);
                // CryptoStorageMode.ObjectMetadata is required for KMS EncryptionMaterials
                var config = new AmazonS3CryptoConfiguration()
                {
                    StorageMode = CryptoStorageMode.ObjectMetadata
                };

                using (var s3Client = new AmazonS3EncryptionClient(config, kmsEncryptionMaterials))
                {
                    // encrypt and put object
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        ContentBody = "object content"
                    };
                    s3Client.PutObject(putRequest);

                    // get object and decrypt
                    var getRequest = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey
                    };

                    using (var getResponse = s3Client.GetObject(getRequest))
                    using (var stream = getResponse.ResponseStream)
                    using (var reader = new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }

}
```

See the [complete example](https://github.com/awsdocs/aws-net-developer-guide/tree/master/doc_source/samples/kms_s3_encryption.cs) on GitHub\.