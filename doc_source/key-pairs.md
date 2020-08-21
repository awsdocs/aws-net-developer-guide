--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Working with Amazon EC2 Key Pairs<a name="key-pairs"></a>

Amazon EC2 uses public–key cryptography to encrypt and decrypt login information\. Public–key cryptography uses a public key to encrypt data, then the recipient uses the private key to decrypt the data\. The public and private keys are known as a key pair\. You must specify a key pair when you launch an EC2 instance and specify the private key of the keypair when you connect to the instance\. You can create a key pair or use one you’ve used when launching other instances\. For more information, see [Amazon EC2 Key Pairs](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-key-pairs.html) in the Amazon EC2 User Guide for Windows Instances\. This example shows how to create a key pair, describe key pairs and delete a key pair using these [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) methods:
+  [CreateKeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateKeyPairCreateKeyPairRequest.html) 
+  [DeleteKeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DeleteKeyPairDeleteKeyPairRequest.html) 
+  [DescribeKeyPairs](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeKeyPairsDescribeKeyPairsRequest.html) 

For information on creating an Amazon EC2 client, see [Creating an Amazon EC2 Client](init-ec2-client.md)\.

## Create a Key Pair and Save the Private Key<a name="create-save-key-pair"></a>

When you create a new key pair, you must save the private key that is returned\. You cannot retrieve the private key later\.

Create and initialize a [CreateKeyPairRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateKeyPairRequest.html) object\. Set the `KeyName` property to the name of the key pair\.

Pass the request object to the [CreateKeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateKeyPairCreateKeyPairRequest.html) method, which returns a [CreateKeyPairResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateKeyPairResponse.html) object\. If a key pair with the specified name already exists, an [AmazonEC2Exception](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Exception.html) is thrown\.

The response object includes a [CreateKeyPairResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateKeyPairResponse.html) object that contains the new key’s [KeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TKeyPair.html) object\. The [KeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TKeyPair.html) object’s `KeyMaterial` property contains the unencrypted private key for the key pair\. Save the private key as a `.pem` file in a safe location\. You’ll need this file when you connect to your instance\. This example saves the private key in the specified file name\.

```
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
```

## Enumerate Your Key Pairs<a name="enumerate-your-key-pairs"></a>

You can enumerate your key pairs and check whether a key pair exists\.

Get the complete list of your key pairs using the [DescribeKeyPairs](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeKeyPairs.html) method with no parameters\.

```
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
```

## Delete Key Pairs<a name="delete-key-pairs"></a>

You can delete a key pair by calling the [DeleteKeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DeleteKeyPairDeleteKeyPairRequest.html) from your AmazonEC2Client instance\.

Pass a [DeleteKeyPairRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDeleteKeyPairRequest.html) containing the name of the key pair to the [DeleteKeyPair](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DeleteKeyPairDeleteKeyPairRequest.html) method of the [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) object\.

```
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
```