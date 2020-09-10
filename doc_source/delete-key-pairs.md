--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Deleting key pairs<a name="delete-key-pairs"></a>

This example shows you how to use the AWS SDK for \.NET to delete a key pair\. The application takes the name of a key pair\. It deletes the key pair and then displays all available key pairs\. If you provide no command\-line arguments, the application simply displays all available key pairs\.

The following sections provide snippets of this example\. The [complete code for the example](#delete-key-pairs-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Delete the key pair](#delete-key-pairs-create)
+ [Display available key pairs](#delete-key-pairs-display)
+ [Complete code](#delete-key-pairs-complete-code)

## Delete the key pair<a name="delete-key-pairs-create"></a>

The following snippet deletes a key pair\.

The example [at the end of this topic](#delete-key-pairs-complete-code) shows this snippet in use\.

```
    //
    // Method to delete a key pair
    private static async Task DeleteKeyPair(IAmazonEC2 ec2Client, string keyName)
    {
      await ec2Client.DeleteKeyPairAsync(new DeleteKeyPairRequest{
        KeyName = keyName});
      Console.WriteLine($"\nKey pair {keyName} has been deleted (if it existed).");
    }
```

## Display available key pairs<a name="delete-key-pairs-display"></a>

The following snippet displays a list of the available key pairs\.

The example [at the end of this topic](#delete-key-pairs-complete-code) shows this snippet in use\.

```
    //
    // Method to show the key pairs that are available
    private static async Task EnumerateKeyPairs(IAmazonEC2 ec2Client)
    {
      DescribeKeyPairsResponse response = await ec2Client.DescribeKeyPairsAsync();
      Console.WriteLine("Available key pairs:");
      foreach (KeyPairInfo item in response.KeyPairs)
        Console.WriteLine($"  {item.KeyName}");
    }
```

## Complete code<a name="delete-key-pairs-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c19c15c13c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [https://docs.aws.amazon.com/sdkfornet/v3/apidocs/](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/)

  Class [DescribeKeyPairsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeKeyPairsResponse.html)

  Class [KeyPairInfo](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TKeyPairInfo.html)

### The code<a name="w4aac19c19c15c13c19b7b1"></a>

```
using System;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2DeleteKeyPair
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Create the EC2 client
      var ec2Client = new AmazonEC2Client();

      if(args.Length == 1)
      {
        // Delete a key pair (if it exists)
        await DeleteKeyPair(ec2Client, args[0]);

        // Display the key pairs that are left
        await EnumerateKeyPairs(ec2Client);
      }
      else
      {
        Console.WriteLine("\nUsage: EC2DeleteKeyPair keypair-name");
        Console.WriteLine("  keypair-name - The name of the key pair you want to delete.");
        Console.WriteLine("\nNo arguments specified.");
        Console.Write(
          "Do you want to see a list of the existing key pairs? ((y) or n): ");
        string response = Console.ReadLine();
        if((string.IsNullOrEmpty(response)) || (response.ToLower() == "y"))
          await EnumerateKeyPairs(ec2Client);
      }
    }


    //
    // Method to delete a key pair
    private static async Task DeleteKeyPair(IAmazonEC2 ec2Client, string keyName)
    {
      await ec2Client.DeleteKeyPairAsync(new DeleteKeyPairRequest{
        KeyName = keyName});
      Console.WriteLine($"\nKey pair {keyName} has been deleted (if it existed).");
    }


    //
    // Method to show the key pairs that are available
    private static async Task EnumerateKeyPairs(IAmazonEC2 ec2Client)
    {
      DescribeKeyPairsResponse response = await ec2Client.DescribeKeyPairsAsync();
      Console.WriteLine("Available key pairs:");
      foreach (KeyPairInfo item in response.KeyPairs)
        Console.WriteLine($"  {item.KeyName}");
    }
  }
}
```