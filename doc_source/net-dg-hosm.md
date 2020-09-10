--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Granting access by using an AWS IAM role<a name="net-dg-hosm"></a>

This tutorial shows you how to use the AWS SDK for \.NET to enable IAM roles on Amazon EC2 instances\.

## Overview<a name="hosm-overview"></a>

All requests to AWS must be cryptographically signed by using credentials issued by AWS\. Therefore, you need a strategy to manage credentials for applications that run on Amazon EC2 instances\. You have to distribute, store, and rotate these credentials securely, but also keep them accessible to the applications\.

With IAM roles, you can effectively manage these credentials\. You create an IAM role and configure it with the permissions that an application requires, and then attach that role to an EC2 instance\. Read more about the benefits of using IAM roles in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/iam-roles-for-amazon-ec2.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/iam-roles-for-amazon-ec2.html)\. Also see the information about [IAM Roles](https://docs.aws.amazon.com/IAM/latest/UserGuide/WorkingWithRoles.html) in the IAM User Guide\.

For an application that is built using the AWS SDK for \.NET, when the application constructs a client object for an AWS service, the object searches for credentials from several potential sources\. The order in which it searches is shown in [Credential and profile resolution](creds-assign.md)\.

If the client object doesn't find credentials from any other source, it retrieves temporary credentials that have the same permissions as those that have been configured into the IAM role and are in the metadata of the EC2 instance\. These credentials are used to make calls to AWS from the client object\.

## About this tutorial<a name="about-hosm-tutorial"></a>

As you follow this tutorial, you use the AWS SDK for \.NET \(and other tools\) to launch an Amazon EC2 instance with an IAM role attached, and then see an application on the instance using the permissions of the IAM role\.

**Topics**
+ [Overview](#hosm-overview)
+ [About this tutorial](#about-hosm-tutorial)
+ [Create a sample Amazon S3 application](#net-dg-hosm-sample-s3-app)
+ [Create an IAM role](#net-dg-hosm-create-the-role)
+ [Launch an EC2 instance and attach the IAM role](#net-dg-hosm-launch-ec2-instance)
+ [Connect to the EC2 instance](#net-dg-hosm-connect)
+ [Run the sample application on the EC2 instance](#net-dg-hosm-run-the-app)
+ [Clean up](#net-dg-hosm-cleanup)

## Create a sample Amazon S3 application<a name="net-dg-hosm-sample-s3-app"></a>

This sample application retrieves an object from Amazon S3\. To run the application, you need the following:
+ An Amazon S3 bucket that contains a text file\.
+ AWS credentials on your development machine that allow you to access to the bucket\.

For information about creating an Amazon S3 bucket and uploading an object, see the [Amazon S3 Getting Started Guide](https://docs.aws.amazon.com/AmazonS3/latest/gsg/)\. For information about AWS credentials, see [Configure AWS credentials](net-dg-config-creds.md)\.

Create a \.NET Core project with the following code\. Then test the application on your development machine\.

**Note**  
On your development machine, the \.NET Core Runtime is installed, which enables you to run the application without publishing it\. When you create an EC2 instance later in this tutorial, you can choose to install the \.NET Core Runtime on the instance\. This gives you a similar experience and a smaller file transfer\.  
 However, you can also choose not to install the \.NET Core Runtime on the instance\. If you choose this course of action, you must publish the application so that all dependencies are included when you transfer it to the instance\.

### SDK references<a name="w4aac19c21c29c17c13b1"></a>

NuGet packages:
+ [AWSSDK\.S3](https://www.nuget.org/packages/AWSSDK.S3)

Programming elements:
+ Namespace [Amazon\.S3](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/NS3.html)

  Class [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html)
+ Namespace [Amazon\.S3\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/NS3Model.html)

  Class [GetObjectResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TGetObjectResponse.html)

### The code<a name="w4aac19c21c29c17c15b1"></a>

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3GetTextItem
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to retrieve a text file from an S3 bucket and write it to a local file
  class Program
  {
    static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if(parsedArgs.Count == 0)
      {
        PrintHelp();
        return;
      }

      // Get the application parameters from the parsed arguments
      string bucket =
        CommandLine.GetParameter(parsedArgs, null, "-b", "--bucket-name");
      string item =
        CommandLine.GetParameter(parsedArgs, null, "-t", "--text-object");
      string outFile =
        CommandLine.GetParameter(parsedArgs, null, "-o", "--output-filename");
      if(   string.IsNullOrEmpty(bucket)
         || string.IsNullOrEmpty(item)
         || string.IsNullOrEmpty(outFile))
        CommandLine.ErrorExit(
          "\nOne or more of the required arguments is missing or incorrect." +
          "\nRun the command with no arguments to see help.");

      // Create the S3 client object and get the file object from the bucket.
      var response = await GetObject(new AmazonS3Client(), bucket, item);

      // Write the contents of the file object to the given output file.
      var reader = new StreamReader(response.ResponseStream);
      string contents = reader.ReadToEnd();
      using (var s = new FileStream(outFile, FileMode.Create))
      using (var writer = new StreamWriter(s))
        writer.WriteLine(contents);
    }


    //
    // Method to get an object from an S3 bucket.
    private static async Task<GetObjectResponse> GetObject(
      IAmazonS3 s3Client, string bucket, string item)
    {
        Console.WriteLine($"Retrieving {item} from bucket {bucket}.");
        return await s3Client.GetObjectAsync(bucket, item);
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: S3GetTextItem -b <bucket-name> -t <text-object> -o <output-filename>" +
        "\n  -b, --bucket-name: The name of the S3 bucket." +
        "\n  -t, --text-object: The name of the text object in the bucket." +
        "\n  -o, --output-filename: The name of the file to write the text to.");
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

If you want, you can temporarily remove the credentials you use on your development machine to see how the application responds\. \(But be sure to restore the credentials when you're finished\.\)

## Create an IAM role<a name="net-dg-hosm-create-the-role"></a>

Create an IAM role that has the appropriate permissions to access Amazon S3\.

1. Open the [IAM console](https://console.aws.amazon.com/iam/)\.

1. In the navigation pane, choose **Roles**, and then choose **Create Role**\.

1. Select **AWS service**, find and choose **EC2**, and choose **Next: Permissions**\.

1. Under **Attach permissions policies**, find and select **AmazonS3ReadOnlyAccess**\. Review the policy if you want to, and then choose **Next: Tags**\.

1. Add tags if you want and then choose **Next: Review**\.

1. Type a name and description for the role, and then choose **Create role**\. Remember this name because you'll need it when you launch your EC2 instance\.

## Launch an EC2 instance and attach the IAM role<a name="net-dg-hosm-launch-ec2-instance"></a>

Launch an EC2 instance with the IAM role you created previously\. You can do so in the following ways\.
+ **Using the EC2 console**

  Follow the directions to launch an instance in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/launching-instance.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/launching-instance.html)\.

  As you go through the wizard you should at least visit the **Configure Instance Details** page so that you can select the **IAM role** you created earlier\.
+ **Using the AWS SDK for \.NET**

  For information about this, see [Launching an Amazon EC2 instance](run-instance.md), including the **Additional considerations** near the end of that topic\.

To launch an EC2 instance that has an IAM role attached, an IAM user's configuration must include certain permissions\. For more information about the required permissions, see the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/iam-roles-for-amazon-ec2.html#permission-to-pass-iam-roles) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/iam-roles-for-amazon-ec2.html#permission-to-pass-iam-roles)\.

## Connect to the EC2 instance<a name="net-dg-hosm-connect"></a>

Connect to the EC2 instance so that you can transfer the sample application to it and then run the application\. You'll need the file that contains the private portion of the key pair you used to launch the instance; that is, the PEM file\.

You can do this by following the connect procedure in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AccessingInstances.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html)\. When you connect, do so in such a way that you can transfer files from your development machine to your instance\.

If you're using Visual Studio on Windows, you can also connect to the instance by using the Toolkit for Visual Studio\. For more information, see [Connecting to an Amazon EC2 Instance](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/tkv-ec2-ami.html#connect-ec2) in the AWS Toolkit for Visual Studio User Guide\.

## Run the sample application on the EC2 instance<a name="net-dg-hosm-run-the-app"></a>

1. Copy the application files from your local drive to your instance\.

   Which files you transfer depends on how you built the application and whether your instance has the \.NET Core Runtime installed\. For information about how to transfer files to your instance see the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AccessingInstances.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html#AccessingInstancesWindowsFileTransfer)\.

1. Start the application and verify that it runs with the same results as on your development machine\.

1. Verify that the application uses the credentials provided by the IAM role\.

   1. Open the [Amazon EC2 console](https://console.aws.amazon.com/ec2/)\.

   1. Select the instance and detach the IAM role through **Actions**, **Instance Settings**, **Attach/Replace IAM Role**\.

   1. Run the application again and see that it returns an authorization error\.

## Clean up<a name="net-dg-hosm-cleanup"></a>

When you are finished with this tutorial, and if you no longer want the EC2 instance you created, be sure to terminate the instance to avoid unwanted cost\. You can do so in the [Amazon EC2 console](https://console.aws.amazon.com/ec2/) or programmatically, as described in [Terminating an Amazon EC2 instance](terminate-instance.md)\. If you want to, you can also delete other resources that you created for this tutorial\. These might include an IAM role, an EC2 keypair and PEM file, a security group, etc\.