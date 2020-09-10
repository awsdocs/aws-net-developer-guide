--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Amazon S3 Encryption Client Migration<a name="s3-encryption-migration"></a>

This topic shows how to migrate your applications from Version 1 \(V1\) of the Amazon Simple Storage Service \(Amazon S3\) encryption client to Version 2 \(V2\), and ensure application availability throughout the migration process\.

Objects that are encrypted with the V2 client can't be decrypted with the V1 client\. In order to ease migration to the new client without having to re\-encrypt all objects at once, a "V1\-transitional" client has been provided\. This client can *decrypt* both V1\- and V2\-encrypted objects, but *encrypts* objects only in V1\-compatible format\. The V2 client can *decrypt* both V1\- and V2\-encrypted objects \(when enabled for V1 objects\), but *encrypts* objects only in V2\-compatible format\.

## Migration Overview<a name="s3-encryption-migration-overview"></a>

This migration happens in three phases\. These phases are introduced here and described in detail later\. Each phase must be completed for *all* clients that use shared objects before the next phase is started\.

1. **Update existing clients to V1\-transitional clients to read new formats\.** First, update your applications to take a dependency on the V1\-transitional client instead of the V1 client\. The V1\-transitional client enables your existing code to decrypt objects written by the new V2 clients and objects written in V1\-compatible format\.
**Note**  
The V1\-transitional client is provided for migration purposes only\. Proceed to upgrading to the V2 client after moving to the V1\-transitional client\.

1. **Migrate V1\-transitional clients to V2 clients to write new formats\.** Next, replace all V1\-transitional clients in your applications with V2 clients, and set the security profile to `V2AndLegacy`\. Setting this security profile on V2 clients enables those clients to decrypt objects that were encrypted in V1\-compatible format\.

1. **Update V2 clients to no longer read V1 formats\.** Finally, after all clients have been migrated to V2 and all objects have been encrypted or re\-encrypted in V2\-compatible format, set the V2 security profile to `V2` instead of `V2AndLegacy`\. This prevents the decryption of objects that are in V1\-compatible format\.

## Update Existing Clients to V1\-transitional Clients to Read New Formats<a name="s3-encryption-migration-to-v1n"></a>

The V2 encryption client uses encryption algorithms that older versions of the client don't support\. The first step in the migration is to update your V1 decryption clients so that they can read the new format\.

The V1\-transitional client enables your applications to decrypt both V1\- and V2\-encrypted objects\. This client is a part of the [Amazon\.Extensions\.S3\.Encryption](https://www.nuget.org/packages/Amazon.Extensions.S3.Encryption) NuGet package\. Perform the following steps on each of your applications to use the V1\-transitional client\.

1. Take a new dependency on the [Amazon\.Extensions\.S3\.Encryption](https://www.nuget.org/packages/Amazon.Extensions.S3.Encryption) package\. If your project depends directly on the **AWSSDK\.S3** or **AWSSDK\.KeyManagementService** packages, you must either update those dependencies or remove them so that their updated versions will be pulled in with this new package\.

1. Change the appropriate `using` statement from `Amazon.S3.Encryption` to `Amazon.Extensions.S3.Encryption`, as follows: 

   ```
   // using Amazon.S3.Encryption;
     using Amazon.Extensions.S3.Encryption;
   ```

1. Rebuild and redeploy your application\.

The V1\-transitional client is fully API\-compatible with the V1 client, so no other code changes are required\.

## Migrate V1\-transitional Clients to V2 Clients to Write New Formats<a name="s3-encryption-migration-v1n-to-v2"></a>

The V2 client is a part of the [Amazon\.Extensions\.S3\.Encryption](https://www.nuget.org/packages/Amazon.Extensions.S3.Encryption) NuGet package\. It enables your applications to decrypt both V1\- and V2\-encrypted objects \(if configured to do so\), but encrypts objects only in V2\-compatible format\.

After updating your existing clients to read the new encryption format, you can proceed to safely update your applications to the V2 encryption and decryption clients\. Perform the following steps on each of your applications to use the V2 client:

1. Change `EncryptionMaterials` to `EncryptionMaterialsV2`\.

   1. When using KMS:

      1. Provide a KMS key ID\.

      1. Declare the encryption method that you are using; that is, `KmsType.KmsContext`\.

      1. Provide an encryption context to KMS to associate with this data key\. You can send an empty dictionary \(Amazon encryption context will still be merged in\), but providing additional context is encouraged\.

   1. When using user\-provided key wrap methods \(symmetric or asymmetric encryption\):

      1. Provide an `AES` or an `RSA` instance that contains the encryption materials\.

      1. Declare which encryption algorithm to use; that is, `SymmetricAlgorithmType.AesGcm` or `AsymmetricAlgorithmType.RsaOaepSha1`\.

1. Change `AmazonS3CryptoConfiguration` to `AmazonS3CryptoConfigurationV2` with the `SecurityProfile` property set to `SecurityProfile.V2AndLegacy`\.

1. Change `AmazonS3EncryptionClient` to `AmazonS3EncryptionClientV2`\. This client takes the newly converted `AmazonS3CryptoConfigurationV2` and `EncryptionMaterialsV2` objects from the previous steps\.

### Example: KMS to KMS\+Context<a name="s3-encryption-migration-ex-kms"></a>

**Pre\-migration**

```
using System.Security.Cryptography;
using Amazon.S3.Encryption;

var encryptionMaterial = new EncryptionMaterials("1234abcd-12ab-34cd-56ef-1234567890ab");
var configuration = new AmazonS3CryptoConfiguration()
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClient(configuration, encryptionMaterial);
```

**Post\-migration**

```
using System.Security.Cryptography;
using Amazon.Extensions.S3.Encryption;
using Amazon.Extensions.S3.Encryption.Primitives;

var encryptionContext = new Dictionary<string, string>();
var encryptionMaterial = new EncryptionMaterialsV2("1234abcd-12ab-34cd-56ef-1234567890ab", KmsType.KmsContext, encryptionContext);
var configuration = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClientV2(configuration, encryptionMaterial);
```

### Example: Symmetric Algorithm \(AES\-CBC to AES\-GCM Key Wrap\)<a name="s3-encryption-migration-ex-aes"></a>

`StorageMode` can be either `ObjectMetadata` or `InstructionFile`\.

**Pre\-migration**

```
using System.Security.Cryptography;
using Amazon.S3.Encryption;

var symmetricAlgorithm = Aes.Create();
var encryptionMaterial = new EncryptionMaterials(symmetricAlgorithm);
var configuration = new AmazonS3CryptoConfiguration()
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClient(configuration, encryptionMaterial);
```

**Post\-migration**

```
using System.Security.Cryptography;
using Amazon.Extensions.S3.Encryption;
using Amazon.Extensions.S3.Encryption.Primitives;

var symmetricAlgorithm = Aes.Create();
var encryptionMaterial = new EncryptionMaterialsV2(symmetricAlgorithm, SymmetricAlgorithmType.AesGcm);
var configuration = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClientV2(configuration, encryptionMaterial);
```

**Note**  
When decrypting with AES\-GCM, read the entire object to the end before you start using the decrypted data\. This is to verify that the object hasn't been modified since it was encrypted\.

### Example: Asymmetric Algorithm \(RSA to RSA\-OAEP\-SHA1 Key Wrap\)<a name="s3-encryption-migration-ex-rsa"></a>

`StorageMode` can be either `ObjectMetadata` or `InstructionFile`\.

**Pre\-migration**

```
using System.Security.Cryptography;
using Amazon.S3.Encryption;

var asymmetricAlgorithm = RSA.Create();
var encryptionMaterial = new EncryptionMaterials(asymmetricAlgorithm);
var configuration = new AmazonS3CryptoConfiguration()
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClient(configuration, encryptionMaterial);
```

**Post\-migration**

```
using System.Security.Cryptography;
using Amazon.Extensions.S3.Encryption;
using Amazon.Extensions.S3.Encryption.Primitives;

var asymmetricAlgorithm = RSA.Create();
var encryptionMaterial = new EncryptionMaterialsV2(asymmetricAlgorithm, AsymmetricAlgorithmType.RsaOaepSha1);
var configuration = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy)
{
    StorageMode = CryptoStorageMode.ObjectMetadata
};
var encryptionClient = new AmazonS3EncryptionClientV2(configuration, encryptionMaterial);
```

## Update V2 Clients to No Longer Read V1 Formats<a name="s3-encryption-migration-v2-cleanup"></a>

Eventually, all objects will have been encrypted or re\-encrypted using a V2 client\. *After this conversion is complete*, you can disable V1 compatibility in the V2 clients by setting the `SecurityProfile` property to `SecurityProfile.V2`, as shown in the following snippet\.

```
//var configuration = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2AndLegacy);
var configuration = new AmazonS3CryptoConfigurationV2(SecurityProfile.V2);
```