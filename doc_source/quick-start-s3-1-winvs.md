# Simple Windows\-based application using the AWS SDK for \.NET<a name="quick-start-s3-1-winvs"></a>

This tutorial uses the AWS SDK for \.NET on Windows along with Visual Studio and \.NET Core\. The tutorial shows you how to use the SDK to list the [Amazon S3 buckets](https://docs.aws.amazon.com/AmazonS3/latest/gsg/) that you own and optionally create a new bucket\.

## Steps<a name="s3-1-winvs-steps"></a>
+ [Setup for this tutorial](#s3-1-winvs-setup)
+ [Create the project](#s3-1-winvs-create-project)
+ [Create the code](#s3-1-winvs-code)
+ [Run the application](#s3-1-winvs-run)
+ [Clean up](#s3-1-winvs-clean-up)

## Setup for this tutorial<a name="s3-1-winvs-setup"></a>

This section provides the minimal setup needed to complete this tutorial\. You shouldn't consider this to be a full setup\. For that, see [Setting up the AWS SDK for \.NET](net-dg-setup.md)\.

**Note**  
If you've already completed any of the following steps through other tutorials or existing configuration, skip those steps\.

### Create an AWS account<a name="s3-1-winvs-setup-account"></a>

To create an AWS account, see [How do I create and activate a new Amazon Web Services account?](https://aws.amazon.com/premiumsupport/knowledge-center/create-and-activate-aws-account)\.

### Create AWS credentials and a profile<a name="s3-1-winvs-setup-creds"></a>

To perform these tutorials, you need to create an IAM user and obtain credentials for that user\. Once you have those credentials, you make them available to the SDK in your development environment\. Here's how\.

**To create and use credentials**

1. Sign in to the AWS Management Console and open the IAM console at [https://console\.aws\.amazon\.com/iam/](https://console.aws.amazon.com/iam/)\.

1. Choose **Users** and then choose **Add user**\.

1. Provide a user name\. For this tutorial, we'll use *Dotnet\-Tutorial\-User*\.

1. Under **Select AWS access type**, select **Programmatic access** and choose **Next: Permissions**\.

1. Choose **Attach existing policies directly**\.

1. In the **Search** field, enter "s3" and select *AmazonS3FullAccess*\.

1. Choose **Next: Tags**, **Next: Review**, and **Create user**\.

1. Record the credentials for *Dotnet\-Tutorial\-User*\. You can do so by downloading the **`.csv`** file or by copying and pasting the *Access key ID* and *Secret access key*\.
**Warning**  
Use appropriate security measures to keep these credentials safe and rotated\.

1. Create or open the shared AWS credentials file\. This file is `~/.aws/credentials` on Linux and macOS systems and `%HOME%\.aws\credentials` on Windows\.

1. Add the following text to the shared AWS credentials file, but replace the example ID and example key with the ones you obtained earlier\. Remember to save the file\.

   ```
   [dotnet-tutorials]
   aws_access_key_id = AKIAIOSFODNN7EXAMPLE
   aws_secret_access_key = wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
   ```

The preceding procedure is the simplest of several possibilities for authentication and authorization\. For complete information, see the topic [Configuring AWS Credentials](net-dg-config-creds.md)\.

### Install other tools<a name="s3-1-winvs-setup-tools"></a>

You'll perform this tutorial on Windows using Visual Studio and \.NET Core\. For other ways to configure your development environment, see the topic [Set Up the \.NET Development Environment](net-dg-dev-env.md)\.

**For development on Windows with Visual Studio and \.NET Core:**
+ [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/)
+ Microsoft \.NET Core 2\.1, 3\.1 or later

  This is typically included by default when installing a modern version of Visual Studio\.

## Create the project<a name="s3-1-winvs-create-project"></a>

1. Open Visual Studio and create a new project that uses the C\# version of the **Console App \(\.NET Core\)** template\. Name the project `S3CreateAndList`\.

1. With the newly created project loaded, open **Tools**, **NuGet Package Manager**, **Manage NuGet Packages for Solution**\.

1. Browse for the **AWSSDK\.S3** NuGet package and install it into the project\.

   This process installs the **AWSSDK\.S3** NuGet package from the [NuGet package manager](https://www.nuget.org/profiles/awsdotnet)\. Since we know exactly what NuGet packages we need for this tutorial, we can perform this step now\. It's also quite common that the required packages become known during development\. When this happens, follow a similar process to install them at that time\.

1. If you intend to run the application from the command line, open a command line now and navigate to the folder that will contain the build output\. This is typically something like `S3CreateAndList\S3CreateAndList\bin\Debug\netcoreapp3.1`, but will depend on our environment\.

1. Add the following temporary environment variables to the environment\.

   From the command line:

   ```
   set AWS_PROFILE=dotnet-tutorials
   set AWS_REGION=us-west-2
   ```

   Or, if you intend to run the application in the IDE, go to **Project**, **S3CreateAndList Properties**, **Debug** and set them there\.

## Create the code<a name="s3-1-winvs-code"></a>

1. In the `S3CreateAndList` project, find and open `Program.cs` in the IDE\.

1. Replace the contents with the following code and save the file\.

   ```
   using System;
   using System.Threading.Tasks;
   
   // To interact with Amazon S3.
   using Amazon.S3;
   using Amazon.S3.Model;
   
   namespace S3CreateAndList
   {
       class Program
       {
           // Function Main
           static async Task Main(string[] args)
           {
               // Before running this app, credentials must be specified either in the [default] profile
               //   or in another profile and then by setting the AWS_PROFILE environment variable.
               // A region must be specified either in the [default] profile or by setting the AWS_REGION environment variable.
   
               // Create an S3 client object.
               var client = new AmazonS3Client();
   
               // Parse the command line arguments for the bucket name.
               // If a bucket name was supplied, create the bucket.
               if(GetBucketName(args, out String bucketName))
               {
                   try
                   {
                       Console.WriteLine();
                       Console.WriteLine($"Creating bucket {bucketName}...");
                       var response = await client.PutBucketAsync(bucketName);
                       Console.WriteLine($"Result: {response.HttpStatusCode.ToString()}");
                   }
                   catch(Exception e)
                   {
                       Console.WriteLine("Caught exception when creating a bucket:");
                       Console.WriteLine(e.Message);
                   }
               }
   
               // List the buckets owned by the user.
               try
               {
                   Console.WriteLine();
                   Console.WriteLine("Getting a list of your buckets...");
                   var response = await client.ListBucketsAsync();
                   Console.WriteLine($"Number of buckets: {response.Buckets.Count}");
                   foreach(S3Bucket b in response.Buckets)
                   {
                       Console.WriteLine(b.BucketName);
                   }
               }
               catch(Exception e)
               {
                   Console.WriteLine("Caught exception when getting the list of buckets:");
                   Console.WriteLine(e.Message);
               }
           }
   
           // Function GetBucketName.
           // Parses the command line arguments to find the bucket name.
           private static Boolean GetBucketName(string[] args, out String bucketName)
           {
               Boolean retval = false;
               bucketName = String.Empty;
               if(args.Length == 0)
               {
                   Console.WriteLine();
                   Console.WriteLine("No arguments specified. Will simply list your Amazon S3 buckets.");
                   Console.WriteLine("If you wish to create a bucket, supply a valid, globally unique bucket name.");
                   bucketName = String.Empty;
                   retval = false;
               }
               else if(args.Length == 1)
               {
                   bucketName = args[0];
                   retval = true;
               }
               else
               {
                   Console.WriteLine();
                   Console.WriteLine("Too many arguments specified.");
                   Console.WriteLine();
                   Console.WriteLine("dotnet_tutorials - A utility to list your Amazon S3 buckets and optionally create a new one.");
                   Console.WriteLine();
                   Console.WriteLine("Usage: S3CreateAndList [bucket_name]");
                   Console.WriteLine(" - bucket_name, if supplied, must be a valid, globally unique bucket name");
                   Console.WriteLine(" - If bucket_name is not supplied, this utility will simply list your buckets.");
                   Environment.Exit(1);
               }
   
               return retval;
           }
       }
   }
   ```

1. Build the application\.

## Run the application<a name="s3-1-winvs-run"></a>

1. Run the application with no command line arguments\. Do this either in the command line \(if you opened one earlier\) or from the IDE\.

1. Examine the output to see the number of Amazon S3 buckets that you own, if any, and their names\.

1. Choose a name for a new Amazon S3 bucket\. Use "dotnet\-quickstart\-s3\-1\-winvs\-" as a base and add something unique to it like a GUID or your name, etc\. Be sure to follow the rules for bucket names, as described in [Rules for Bucket Naming](https://docs.aws.amazon.com/AmazonS3/latest/dev/BucketRestrictions.html#bucketnamingrules) in the *[Amazon Simple Storage Service Developer Guide](https://docs.aws.amazon.com/AmazonS3/latest/dev/)*\.

1. Run the application again, this time supplying the bucket name\.

   In the command line, replace *BUCKET\-NAME* in the following command with the name of the bucket that you chose:

   ```
   S3CreateAndList BUCKET-NAME
   ```

   Or, if you are running the application in the IDE, go to **Project**, **S3CreateAndList Properties**, **Debug** and enter the bucket name there\.

1. Examine the output to see the new bucket that was created\.

## Clean up<a name="s3-1-winvs-clean-up"></a>

While performing this tutorial, you created a few resources that you can choose to clean up at this time\.
+ If you don't want to keep the bucket that the application created in an earlier step, you can delete it by using the Amazon S3 console at [https://console\.aws\.amazon\.com/s3/](https://console.aws.amazon.com/s3/)\.
+ If you don't want to keep the user you created during tutorial setup earlier in this topic, you can delete it by using the IAM console at [https://console\.aws\.amazon\.com/iam/home\#/users](https://console.aws.amazon.com/iam/home#/users)\.

  If you do choose to delete the user, you should also remove the **`dotnet-tutorials`** profile that you created in the shared AWS credentials file\. You created this profile during tutorial setup earlier in this topic\.
+ If you don't want to keep your \.NET project, remove the `S3CreateAndList` folder from your development environment\.

## Next\.\.\.<a name="s3-1-winvs-next"></a>

Go back to the [quick\-start menu](quick-start.md) or go straight to the [end of this quick start](quick-start-next-steps.md)\.