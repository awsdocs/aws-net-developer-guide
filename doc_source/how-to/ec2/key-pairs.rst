.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _create-key-pair:

############################
Working with |EC2| Key Pairs
############################

.. meta::
   :description: Use this .NET code example to learn how to use key pairs in Amazon EC2.
   :keywords: AWS SDK for .NET examples, EC2 key pairs, cryptography

Amazon EC2 uses public–key cryptography to encrypt and decrypt login information. Public–key cryptography
uses a public key to encrypt data, then the recipient uses the private key to decrypt the data. The
public and private keys are known as a key pair. You must specify a key pair when you launch an EC2
instance and specify the private key of the keypair when you connect to the instance. You can create
a key pair or use one you've used when launching other instances. For more information, see
:ec2-ug-win:`Amazon EC2 Key Pairs <ec2-key-pairs>` in
the |EC2-ug-win|.
This example shows how to create a key pair, describe key pairs and delete a key pair using these
:sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` methods:

* :sdk-net-api:`CreateKeyPair <EC2/MEC2CreateKeyPairCreateKeyPairRequest>`
* :sdk-net-api:`DeleteKeyPair <EC2/MEC2DeleteKeyPairDeleteKeyPairRequest>`
* :sdk-net-api:`DescribeKeyPairs <EC2/MEC2DescribeKeyPairsDescribeKeyPairsRequest>`

For information on creating an |EC2| client, see :ref:`init-ec2-client`.

.. _create-save-key-pair:

Create a Key Pair and Save the Private Key
==========================================

When you create a new key pair, you must save the private key that is returned. You cannot retrieve
the private key later.

Create and initialize a :sdk-net-api:`CreateKeyPairRequest <EC2/TCreateKeyPairRequest>` object. Set the
:code:`KeyName` property to the name of the key pair.

Pass the request object to the :sdk-net-api:`CreateKeyPair <EC2/MEC2CreateKeyPairCreateKeyPairRequest>`
method, which returns a :sdk-net-api:`CreateKeyPairResponse <EC2/TCreateKeyPairResponse>` object. If a key
pair with the specified name already exists, an :sdk-net-api:`AmazonEC2Exception <EC2/TEC2Exception>` is
thrown.

The response object includes a :sdk-net-api:`CreateKeyPairResponse <EC2/TCreateKeyPairResponse>` object that
contains the new key's :sdk-net-api:`KeyPair <EC2/TKeyPair>` object. The :sdk-net-api:`KeyPair <EC2/TKeyPair>`
object's :code:`KeyMaterial` property contains the unencrypted private key for the key pair. Save
the private key as a :file:`.pem` file in a safe location. You'll need this file when you connect to
your instance. This example saves the private key in the specified file name.

.. code-block:: csharp

    public static void CreateKeyPair(
      AmazonEC2Client ec2Client,
      string keyPairName,
      string privateKeyFile)
    {
      var request = new CreateKeyPairRequest();
      request.KeyName = keyPairName;

      try
      {
        var response = ec2Client.CreateKeyPair(request);
        Console.WriteLine();
        Console.WriteLine("New key: " + keyPairName);

        // Save the private key in a .pem file
        using (FileStream s = new FileStream(privateKeyFile, FileMode.Create))
        using (StreamWriter writer = new StreamWriter(s))
        {
          writer.WriteLine(response.KeyPair.KeyMaterial);
        }
      }
      catch (AmazonEC2Exception ex)
      {
        // Check the ErrorCode to see if the key already exists
        if("InvalidKeyPair.Duplicate" == ex.ErrorCode)
        {
          Console.WriteLine("The key pair \"{0}\" already exists.", keyPairName);
        }
        else
        {
          // The exception was thrown for another reason, so re-throw the exception.
          throw;
        }
      }
    }

    .. _enumerate-key-pairs:

Enumerate Your Key Pairs
========================

You can enumerate your key pairs and check whether a key pair exists.

Get the complete list of your key pairs using the :sdk-net-api:`DescribeKeyPairs <EC2/MEC2DescribeKeyPairs>`
method with no parameters.

.. code-block:: csharp

    public static void EnumerateKeyPairs(AmazonEC2Client ec2Client)
    {
      var request = new DescribeKeyPairsRequest();
      var response = ec2Client.DescribeKeyPairs(request);

      foreach (KeyPairInfo item in response.KeyPairs)
      {
        Console.WriteLine("Existing key pair: " + item.KeyName);
      }
    }

    .. _delete-key-pairs:

Delete Key Pairs
================

You can delete a key pair by calling the :sdk-net-api:`DeleteKeyPair <EC2/MEC2DeleteKeyPairDeleteKeyPairRequest>`
from your AmazonEC2Client instance.

Pass a :sdk-net-api:`DeleteKeyPairRequest <EC2/TDeleteKeyPairRequest>` containing the name of the
key pair to the :sdk-net-api:`DeleteKeyPair <EC2/MEC2DeleteKeyPairDeleteKeyPairRequest>` method of the
:sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` object.

.. code-block:: csharp

        public static void DeleteKeyPair(
                    AmazonEC2Client ec2Client,
                    KeyPair keyPair)
        {
            try
            {
                // Delete key pair created for sample
                ec2Client.DeleteKeyPair(new DeleteKeyPairRequest { KeyName = keyPair.KeyName });
            }
            catch (AmazonEC2Exception ex)
            {
                // Check the ErrorCode to see if the key already exists
                if ("InvalidKeyPair.NotFound" == ex.ErrorCode)
                {
                    Console.WriteLine("The key pair \"{0}\" was not found.", keyPair.KeyName);
                }
                else
                {
                    // The exception was thrown for another reason, so re-throw the exception
                    throw;
                }
            }
        }





