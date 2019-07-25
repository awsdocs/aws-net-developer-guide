.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _kms-keys-s3-encryption:

###################################################################
Using |KMS| Keys with the AmazonS3EncryptionClient in the |sdk-net|
###################################################################

The :sdk-net-api:`AmazonS3EncryptionClient <S3/TS3EncryptionClient>` class implements the
same interface as the standard :code:`AmazonS3Client`. This means it's easy to switch to the :code:`AmazonS3EncryptionClient`
class. In fact, your application code won't be aware of the encryption and decryption happening automatically
in the client.

You can use an |KMS| key as your master key when you use the :code:`AmazonS3EncryptionClient` class for
client-side encryption. All you have to do is create an :code:`EncryptionMaterials` object that contains
a KMS key ID. Then you pass the :code:`EncryptionMaterials` object to the constructor of the
:code:`AmazonS3EncryptionClient`.

The main advantage of using an |KMS| key as your master key is that you don't need to store and manage
your own master keys. It's done by AWS. A second advantage is that it makes the |sdk-net|'s :code:`AmazonS3EncryptionClient`
class interoperable with the |sdk-java|'s :code:`AmazonS3EncryptionClient` class.  This means you can
encrypt with the |sdk-java| and decrypt with the |sdk-net|, and vice versa.

.. note::

    The |sdk-net|'s :code:`AmazonS3EncryptionClient` supports KMS master keys only when run in metadata
    mode. The instruction file mode of the |sdk-net|'s :code:`AmazonS3EncryptionClient` is still incompatible
    with the |sdk-java|'s :code:`AmazonS3EncryptionClient`.

For more information about client-side encryption with the AmazonS3EncryptionClient class, and how envelope encryption works, see `Client Side Data Encryption with AWS SDK for .NET and Amazon S3 <https://aws.amazon.com/blogs/developer/client-side-data-encryption-with-aws-sdk-for-net-and-amazon-s3/>`_.

The following example demonstrates how to use |KMS| keys with the AmazonS3EncryptionClient class. Your project must reference the latest version of the :code:`AWSSDK.KeyManagementService` Nuget package to use this feature.

.. literalinclude:: samples/kms_s3_encryption.cs
   :language: csharp

