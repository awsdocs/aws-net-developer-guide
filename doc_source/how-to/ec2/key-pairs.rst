.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _create-key-pair:

###################################
Create a Key Pair Using the SDK
###################################

You must specify a key pair when you launch an EC2 instance and specify the private key of the key
pair when you connect to the instance. You can create a key pair or use one you've used when
launching other instances. For more information, see :ec2-ug-win:`Amazon EC2 Key Pairs <ec2-key-pairs>` in
the |EC2-ug-win|.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _enumerate-key-pairs:

Enumerating Your Key Pairs
==========================

You can enumerate your key pairs and check whether a key pair exists.

**To enumerate your key pairs**

Get the complete list of your key pairs using the :sdk-net-api:`DescribeKeyPairs <EC2/MEC2EC2DescribeKeyPairs>`
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


.. _create-save-key-pair:

Creating a Key Pair and Saving the Private Key
==============================================

When you create a new key pair, you must save the private key that is returned. You cannot retrieve
the private key later.

**To create a key pair and save the private key**

Create and initialize a :sdk-net-api:`CreateKeyPairRequest <EC2/TEC2CreateKeyPairRequest>` object. Set the
:code:`KeyName` property to the name of the key pair.

Pass the request object to the :sdk-net-api:`CreateKeyPair <EC2/MEC2EC2CreateKeyPairCreateKeyPairRequest>`
method, which returns a :sdk-net-api:`CreateKeyPairResponse <EC2/TEC2CreateKeyPairResponse>` object. If a key
pair with the specified name already exists, an :sdk-net-api:`AmazonEC2Exception <EC2/TEC2EC2Exception>` is
thrown.

The response object includes a :sdk-net-api:`CreateKeyPairResult <EC2/TEC2CreateKeyPairResult>` object that
contains the new key's `KeyPair <TEC2KeyPair.html>`_ object. The :sdk-net-api:`KeyPair <EC2/TEC2KeyPair>`
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
        // Check the ErrorCode to see if the key already exists.
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



