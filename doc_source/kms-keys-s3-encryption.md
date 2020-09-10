--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Using AWS KMS keys for Amazon S3 encryption in the AWS SDK for \.NET<a name="kms-keys-s3-encryption"></a>

This example shows you how to use AWS Key Management Service keys to encrypt Amazon S3 objects\. The application creates a customer master key \(CMK\) and uses it to create a [AmazonS3EncryptionClientV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.AmazonS3EncryptionClientV2.html) object for client\-side encryption\. The application uses that client to create an encrypted object from a given text file in an existing Amazon S3 bucket\. It then decrypts the object and displays its contents\.

**Warning**  
A similar class called `AmazonS3EncryptionClient` is deprecated and is less secure than the `AmazonS3EncryptionClientV2` class\. To migrate existing code that uses `AmazonS3EncryptionClient`, see [S3 Encryption Client Migration](s3-encryption-migration.md)\.

**Topics**
+ [Create encryption materials](#kms-s3-enc-mat)
+ [Create and encrypt an Amazon S3 object](#kms-s3-create-ojbect)
+ [Complete code](#kms-s3-complete-code)

## Create encryption materials<a name="kms-s3-enc-mat"></a>

The following snippet creates an `EncryptionMaterials` object that contains a KMS key ID\.

The example [at the end of this topic](#kms-s3-complete-code) shows this snippet in use\.

```
      // Create a customer master key (CMK) and store the result
      CreateKeyResponse createKeyResponse =
        await new AmazonKeyManagementServiceClient().CreateKeyAsync(new CreateKeyRequest());
      var kmsEncryptionContext = new Dictionary<string, string>();
      var kmsEncryptionMaterials = new EncryptionMaterialsV2(
        createKeyResponse.KeyMetadata.KeyId, KmsType.KmsContext, kmsEncryptionContext);
```

## Create and encrypt an Amazon S3 object<a name="kms-s3-create-ojbect"></a>

The following snippet creates an `AmazonS3EncryptionClientV2` object that uses the encryption materials created earlier\. It then uses the client to create and encrypt a new Amazon S3 object\.

The example [at the end of this topic](#kms-s3-complete-code) shows this snippet in use\.

```
    //
    // Method to create and encrypt an object in an S3 bucket
    static async Task<GetObjectResponse> CreateAndRetrieveObjectAsync(
      EncryptionMaterialsV2 materials, string bucketName,
      string fileName, string itemName)
    {
      // CryptoStorageMode.ObjectMetadata is required for KMS EncryptionMaterials
      var config = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
      {
        StorageMode = CryptoStorageMode.ObjectMetadata
      };
      var s3EncClient = new AmazonS3EncryptionClientV2(config, materials);

      // Create, encrypt, and put the object
      await s3EncClient.PutObjectAsync(new PutObjectRequest
      {
        BucketName = bucketName,
        Key = itemName,
        ContentBody = File.ReadAllText(fileName)
      });

      // Get, decrypt, and return the object
      return await s3EncClient.GetObjectAsync(new GetObjectRequest
      {
        BucketName = bucketName,
        Key = itemName
      });
    }
```

## Complete code<a name="kms-s3-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c23c13c15b5b1"></a>

NuGet packages:
+ [Amazon\.Extensions\.S3\.Encryption](https://www.nuget.org/packages/Amazon.Extensions.S3.Encryption)

Programming elements:
+ Namespace [Amazon\.Extensions\.S3\.Encryption](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.html)

  Class [AmazonS3EncryptionClientV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.AmazonS3EncryptionClientV2.html)

  Class [AmazonS3CryptoConfigurationV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.AmazonS3CryptoConfigurationV2.html)

  Class [CryptoStorageMode](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.CryptoStorageMode.html)

  Class [EncryptionMaterialsV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.EncryptionMaterialsV2.html)
+ Namespace [Amazon\.Extensions\.S3\.Encryption\.Primitives](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.Primitives.html)

  Class [KmsType](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.Primitives.KmsType.html)
+ Namespace [Amazon\.S3\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/NS3Model.html)

  Class [GetObjectRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TGetObjectRequest.html)

  Class [GetObjectResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TGetObjectResponse.html)

  Class [PutObjectRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TPutObjectRequest.html)
+ Namespace [Amazon\.KeyManagementService](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/KeyManagementService/NKeyManagementService.html)

  Class [AmazonKeyManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/KeyManagementService/TKeyManagementServiceClient.html)
+ Namespace [Amazon\.KeyManagementService\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/KeyManagementService/NKeyManagementServiceModel.html)

  Class [CreateKeyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/KeyManagementService/TCreateKeyRequest.html)

  Class [CreateKeyResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/KeyManagementService/TCreateKeyResponse.html)

### The code<a name="w4aac19c23c13c15b7b1"></a>

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.Extensions.S3.Encryption;
using Amazon.Extensions.S3.Encryption.Primitives;
using Amazon.S3.Model;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace KmsS3Encryption
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to store text in an encrypted S3 object.
  class Program
  {
    private const int MaxArgs = 3;

    public static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if((parsedArgs.Count == 0) || (parsedArgs.Count > MaxArgs))
      {
        PrintHelp();
        return;
      }

      // Get the application parameters from the parsed arguments
      string bucketName =
        CommandLine.GetParameter(parsedArgs, null, "-b", "--bucket-name");
      string fileName =
        CommandLine.GetParameter(parsedArgs, null, "-f", "--file-name");
      string itemName =
        CommandLine.GetParameter(parsedArgs, null, "-i", "--item-name");
      if(string.IsNullOrEmpty(bucketName) || (string.IsNullOrEmpty(fileName)))
        CommandLine.ErrorExit(
          "\nOne or more of the required arguments is missing or incorrect." +
          "\nRun the command with no arguments to see help.");
      if(!File.Exists(fileName))
        CommandLine.ErrorExit($"\nThe given file {fileName} doesn't exist.");
      if(string.IsNullOrEmpty(itemName))
        itemName = Path.GetFileName(fileName);

      // Create a customer master key (CMK) and store the result
      CreateKeyResponse createKeyResponse =
        await new AmazonKeyManagementServiceClient().CreateKeyAsync(new CreateKeyRequest());
      var kmsEncryptionContext = new Dictionary<string, string>();
      var kmsEncryptionMaterials = new EncryptionMaterialsV2(
        createKeyResponse.KeyMetadata.KeyId, KmsType.KmsContext, kmsEncryptionContext);

      // Create the object in the bucket, then display the content of the object
      var putObjectResponse =
        await CreateAndRetrieveObjectAsync(kmsEncryptionMaterials, bucketName, fileName, itemName);
      Stream stream = putObjectResponse.ResponseStream;
      StreamReader reader = new StreamReader(stream);
      Console.WriteLine(reader.ReadToEnd());
    }


    //
    // Method to create and encrypt an object in an S3 bucket
    static async Task<GetObjectResponse> CreateAndRetrieveObjectAsync(
      EncryptionMaterialsV2 materials, string bucketName,
      string fileName, string itemName)
    {
      // CryptoStorageMode.ObjectMetadata is required for KMS EncryptionMaterials
      var config = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
      {
        StorageMode = CryptoStorageMode.ObjectMetadata
      };
      var s3EncClient = new AmazonS3EncryptionClientV2(config, materials);

      // Create, encrypt, and put the object
      await s3EncClient.PutObjectAsync(new PutObjectRequest
      {
        BucketName = bucketName,
        Key = itemName,
        ContentBody = File.ReadAllText(fileName)
      });

      // Get, decrypt, and return the object
      return await s3EncClient.GetObjectAsync(new GetObjectRequest
      {
        BucketName = bucketName,
        Key = itemName
      });
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: KmsS3Encryption -b <bucket-name> -f <file-name> [-i <item-name>]" +
        "\n  -b, --bucket-name: The name of an existing S3 bucket." +
        "\n  -f, --file-name: The name of a text file with content to encrypt and store in S3." +
        "\n  -i, --item-name: The name you want to use for the item." +
        "\n      If item-name isn't given, file-name will be used.");
    }

  }

  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal
  // (This is the same for all examples. When you have seen it once, you can ignore it)
  static class CommandLine
  {
    // Method to parse a command line of the form: "--param value" or "-p value".
    // If "param" is found without a matching "value", Dictionary.Value is an empty string.
    // If "value" is found without a matching "param", Dictionary.Key is "--NoKeyN"
    //  where "N" represents sequential numbers.
    public static Dictionary<string,string> Parse(string[] args)
    {
      var parsedArgs = new Dictionary<string,string>();
      int i = 0, n = 0;
      while(i < args.Length)
      {
        // If the first argument in this iteration starts with a dash it's an option.
        if(args[i].StartsWith("-"))
        {
          var key = args[i++];
          var value = string.Empty;

          // Is there a value that goes with this option?
          if((i < args.Length) && (!args[i].StartsWith("-"))) value = args[i++];
          parsedArgs.Add(key, value);
        }

        // If the first argument in this iteration doesn't start with a dash, it's a value
        else
        {
          parsedArgs.Add("--NoKey" + n.ToString(), args[i++]);
          n++;
        }
      }

      return parsedArgs;
    }

    //
    // Method to get a parameter from the parsed command-line arguments
    public static string GetParameter(
      Dictionary<string,string> parsedArgs, string def, params string[] keys)
    {
      string retval = null;
      foreach(var key in keys)
        if(parsedArgs.TryGetValue(key, out retval)) break;
      return retval ?? def;
    }

    //
    // Exit with an error.
    public static void ErrorExit(string msg, int code=1)
    {
      Console.WriteLine("\nError");
      Console.WriteLine(msg);
      Environment.Exit(code);
    }
  }

}
```

**Additional considerations**
+ You can check the results of this example\. To do so, go to the [Amazon S3 console](https://console.aws.amazon.com/s3) and open the bucket you provided to the application\. Then find the new object, download it, and open it in a text editor\.
+ The [AmazonS3EncryptionClientV2](https://aws.github.io/amazon-s3-encryption-client-dotnet/api/Amazon.Extensions.S3.Encryption.AmazonS3EncryptionClientV2.html) class implements the same interface as the standard `AmazonS3Client` class\. This makes it easier to port your code to the `AmazonS3EncryptionClientV2` class so that encryption and decryption happen automatically and transparently in the client\.
+ One advantage of using an AWS KMS key as your master key is that you don't need to store and manage your own master keys; this is done by AWS\. A second advantage is that the `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET is interoperable with the `AmazonS3EncryptionClientV2` class of the AWS SDK for Java\. This means you can encrypt with the AWS SDK for Java and decrypt with the AWS SDK for \.NET, and vice versa\.
**Note**  
The `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET supports KMS master keys only when run in metadata mode\. The instruction file mode of the `AmazonS3EncryptionClientV2` class of the AWS SDK for \.NET is incompatible with the `AmazonS3EncryptionClientV2` class of the AWS SDK for Java\.
+ For more information about client\-side encryption with the `AmazonS3EncryptionClientV2` class, and how envelope encryption works, see [Client Side Data Encryption with AWS SDK for \.NET and Amazon S3](http://aws.amazon.com/blogs/developer/client-side-data-encryption-with-aws-sdk-for-net-and-amazon-s3/)\.