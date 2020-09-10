--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Creating and displaying key pairs<a name="create-save-key-pair"></a>

This example shows you how to use the AWS SDK for \.NET to create a key pair\. The application takes the name for the new key pair and the name of a PEM file \(with a "\.pem" extension\)\. It creates the keypair, writes the private key to the PEM file, and then displays all available key pairs\. If you provide no command\-line arguments, the application simply displays all available key pairs\.

The following sections provide snippets of this example\. The [complete code for the example](#create-save-key-pair-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Create the key pair](#create-save-key-pair-create)
+ [Display available key pairs](#create-save-key-pair-display)
+ [Complete code](#create-save-key-pair-complete-code)

## Create the key pair<a name="create-save-key-pair-create"></a>

The following snippet creates a key pair and then stores the private key to the given PEM file\.

The example [at the end of this topic](#create-save-key-pair-complete-code) shows this snippet in use\.

```
    //
    // Method to create a key pair and save the key material in a PEM file
    private static async Task CreateKeyPair(
      IAmazonEC2 ec2Client, string keyPairName, string pemFileName)
    {
      // Create the key pair
      CreateKeyPairResponse response =
        await ec2Client.CreateKeyPairAsync(new CreateKeyPairRequest{
          KeyName = keyPairName
        });
      Console.WriteLine($"\nCreated new key pair: {response.KeyPair.KeyName}");

      // Save the private key in a PEM file
      using (var s = new FileStream(pemFileName, FileMode.Create))
      using (var writer = new StreamWriter(s))
      {
        writer.WriteLine(response.KeyPair.KeyMaterial);
      }
    }
```

## Display available key pairs<a name="create-save-key-pair-display"></a>

The following snippet displays a list of the available key pairs\.

The example [at the end of this topic](#create-save-key-pair-complete-code) shows this snippet in use\.

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

## Complete code<a name="create-save-key-pair-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c19c15c11c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [CreateKeyPairRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateKeyPairRequest.html)

  Class [CreateKeyPairResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateKeyPairResponse.html)

  Class [DescribeKeyPairsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeKeyPairsResponse.html)

  Class [KeyPairInfo](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TKeyPairInfo.html)

### The code<a name="w4aac19c19c15c11c19b7b1"></a>

```
using System;
using System.Threading.Tasks;
using System.IO;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Collections.Generic;

namespace EC2CreateKeyPair
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to create and store a key pair
  class Program
  {
    static async Task Main(string[] args)
    {
      // Create the EC2 client
      var ec2Client = new AmazonEC2Client();

      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if(parsedArgs.Count == 0)
      {
        // In the case of no command-line arguments,
        // just show help and the existing key pairs
        PrintHelp();
        Console.WriteLine("\nNo arguments specified.");
        Console.Write(
          "Do you want to see a list of the existing key pairs? ((y) or n): ");
        string response = Console.ReadLine();
        if((string.IsNullOrEmpty(response)) || (response.ToLower() == "y"))
          await EnumerateKeyPairs(ec2Client);
        return;
      }

      // Get the application parameters from the parsed arguments
      string keyPairName =
        CommandLine.GetParameter(parsedArgs, null, "-k", "--keypair-name");
      string pemFileName =
        CommandLine.GetParameter(parsedArgs, null, "-p", "--pem-filename");
      if(string.IsNullOrEmpty(keyPairName))
        CommandLine.ErrorExit("\nNo key pair name specified." +
          "\nRun the command with no arguments to see help.");
      if(string.IsNullOrEmpty(pemFileName) || !pemFileName.EndsWith(".pem"))
        CommandLine.ErrorExit("\nThe PEM filename is missing or incorrect." +
          "\nRun the command with no arguments to see help.");

      // Create the key pair
      await CreateKeyPair(ec2Client, keyPairName, pemFileName);
      await EnumerateKeyPairs(ec2Client);
    }


    //
    // Method to create a key pair and save the key material in a PEM file
    private static async Task CreateKeyPair(
      IAmazonEC2 ec2Client, string keyPairName, string pemFileName)
    {
      // Create the key pair
      CreateKeyPairResponse response =
        await ec2Client.CreateKeyPairAsync(new CreateKeyPairRequest{
          KeyName = keyPairName
        });
      Console.WriteLine($"\nCreated new key pair: {response.KeyPair.KeyName}");

      // Save the private key in a PEM file
      using (var s = new FileStream(pemFileName, FileMode.Create))
      using (var writer = new StreamWriter(s))
      {
        writer.WriteLine(response.KeyPair.KeyMaterial);
      }
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


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: EC2CreateKeyPair -k <keypair-name> -p <pem-filename>" +
        "\n  -k, --keypair-name: The name you want to assign to the key pair." +
        "\n  -p, --pem-filename: The name of the PEM file to create, with a \".pem\" extension.");
    }
  }


  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal.
  // (This is the same for all examples. When you have seen it once, you can ignore it.)
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
+ After you run the example, you can see the new key pair in the [Amazon EC2 console](https://console.aws.amazon.com/ec2/#KeyPairs)\.
+ When you create a key pair, you must save the private key that is returned because you can't retrieve the private key later\.