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
            // Set these to the appropriate value:
            var region = RegionEndpoint.USWest-2;
            var bucketName = "BUCKET";
            var objectKey = "KEY";
            
            string kmsKeyID = null;
            using (var kmsClient = new AmazonKeyManagementServiceClient(region))
            {
                var response = kmsClient.CreateKey(new CreateKeyRequest());
                kmsKeyID = response.KeyMetadata.KeyId;

                // An object that contains information about the CMK created by this operation.
                var keyMetadata = response.KeyMetadata;
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
