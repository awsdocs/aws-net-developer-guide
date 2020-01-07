--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Tutorial: Grant Access Using an IAM Role and the AWS SDK for \.NET<a name="net-dg-hosm"></a>

All requests to AWS must be cryptographically signed using credentials issued by AWS\. Therefore, you need a strategy for managing credentials for software that runs on Amazon EC2 instances\. You must distribute, store, and rotate these credentials in a way that keeps them secure but also accessible to the software\.

We designed IAM roles so that you can effectively manage AWS credentials for software running on EC2 instances\. You create an IAM role and configure it with the permissions that the software requires\. For more information about the benefits of this approach, see [IAM Roles for Amazon EC2](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/iam-roles-for-amazon-ec2.html) in the Amazon EC2 User Guide for Windows Instances and [Roles \(Delegation and Federation\)](https://docs.aws.amazon.com/IAM/latest/UserGuide/WorkingWithRoles.html) in the IAM User Guide\.

To use the permissions, the software constructs a client object for the AWS service\. The constructor searches the credentials provider chain for credentials\. For \.NET, the credentials provider chain is as follows:
+ The `App.config` file
+ The instance metadata associated with the IAM role for the EC2 instance

If the client does not find credentials in `App.config`, it retrieves temporary credentials that have the same permissions as those associated with the IAM role\. The credentials are retrieved from instance metadata\. The credentials are stored by the constructor on behalf of the customer software and are used to make calls to AWS from that client object\. Although the credentials are temporary and eventually expire, the SDK client periodically refreshes them so that they continue to enable access\. This periodic refresh is completely transparent to the application software\.

The following walkthrough uses a sample program that retrieves an object from Amazon S3 using the AWS credentials that you’ve configured\. Next, we create an IAM role to provide the AWS credentials\. Finally, we launch an instance with an IAM role that provides the AWS credentials to the sample program running on the instance\.

**Topics**
+ [Create a Sample that Retrieves an Object from Amazon S3](#net-dg-using-hosm-to-retrieve-an-object-from-ec2)
+ [Create an IAM Role](#net-dg-create-the-role)
+ [Launch an EC2 Instance and Specify the IAM Role](#net-dg-launch-ec2-instance-with-instance-profile)
+ [Run the Sample Program on the EC2 Instance](#net-dg-run-the-program)

## Create a Sample that Retrieves an Object from Amazon S3<a name="net-dg-using-hosm-to-retrieve-an-object-from-ec2"></a>

The following sample code retrieves an object from Amazon S3\. It requires a text file in an Amazon S3 bucket that you have access to\. For more information about creating an Amazon S3 bucket and uploading an object, see the [Amazon S3 Getting Started Guide](https://docs.aws.amazon.com/AmazonS3/latest/gsg/)\. It also requires AWS credentials that provide you with access to the Amazon S3 bucket\. For more information, see [Configuring AWS Credentials](net-dg-config-creds.md)\.

```
using System;
using System.Collections.Specialized;
using System.IO;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace s3.amazon.com.docsamples.retrieveobject
{
  class S3Sample
  {
    public static void Main(string[] args)
    {
      ReadS3File("bucket-name", "s3-file-name", "output-file-name");

      Console.WriteLine("Press enter to continue");
      Console.ReadLine();
    }

    public static void ReadS3File(
      string bucketName, 
      string keyName, 
      string filename)
    {

      string responseBody = "";

      try
      {
        using (var s3Client = new AmazonS3Client())
        {
          Console.WriteLine("Retrieving (GET) an object");

          var request = new GetObjectRequest()
          {
            BucketName = bucketName,
            Key = keyName
          };

          using (var response = s3Client.GetObject(request))
          using (var responseStream = response.ResponseStream)
          using (var reader = new StreamReader(responseStream))
          {
            responseBody = reader.ReadToEnd();
          }
        }

        using (var s = new FileStream(filename, FileMode.Create))
        using (var writer = new StreamWriter(s))
        {
          writer.Write(responseBody);
        }
      }
      catch (AmazonS3Exception s3Exception)
      {
        Console.WriteLine(s3Exception.Message, s3Exception.InnerException);
      }
    }
  }
}
```

 **To test the sample code** 

1. Open Visual Studio and create an AWS Console project\.

1. Replace the code in the `Program.cs` file with the sample code\.

1. Replace `bucket-name` with the name of your Amazon S3 bucket and `folder/file-name.txt` with the name of a text file in the bucket\.

1. Compile and run the sample program\. If the program succeeds, it displays the following output and creates a file named `s3Object.txt` on your local drive that contains the text it retrieved from the text file in Amazon S3\.

   ```
   Retrieving (GET) an object
   ```

   If the program fails, ensure that you are using credentials that provide you with access to the bucket\.

1. \(Optional\) Transfer the sample program to a running Windows instance on which you haven’t set up credentials\. Run the program and verify that it fails because it can’t locate credentials\.

## Create an IAM Role<a name="net-dg-create-the-role"></a>

Create an IAM role that has the appropriate permissions to access Amazon S3\.

 **To create the IAM role** 

1. Open the IAM console\.

1. In the navigation pane, click **Roles**, and then click **Create New Role**\.

1. Enter a name for the role, and then click **Next Step**\. Remember this name, as you’ll need it when you launch your EC2 instance\.

1. Under **AWS Service Roles**, select **Amazon EC2**\. Under **Select Policy Template**, select **Amazon S3 Read Only Access**\. Review the policy and then click **Next Step**\.

1. Review the role information and then click **Create Role**\.

## Launch an EC2 Instance and Specify the IAM Role<a name="net-dg-launch-ec2-instance-with-instance-profile"></a>

You can launch an EC2 instance with an IAM role using the Amazon EC2 console or the the SDK\.
+ To launch an EC2 instance using the console, follow the directions in [Launching a Windows Instance](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/EC2Win_GetStarted.html) in the Amazon EC2 User Guide for Windows Instances\. When you reach the **Review Instance Launch** page, click **Edit instance details**\. In **IAM role**, specify the IAM role that you created previously\. Complete the procedure as directed\. Notice that you’ll need to create or use an existing security group and key pair in order to connect to the instance\.
+ To launch an EC2 instance with an IAM role using the the SDK, see [Launch an EC2 Instance Using the the SDK](run-instance.md)\.

Note that an IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

```
{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Allow",
    "Action": [
      "iam:PassRole",
      "iam:ListInstanceProfiles",
      "ec2:*"
    ],
    "Resource": "*"
  }]
}
```

## Run the Sample Program on the EC2 Instance<a name="net-dg-run-the-program"></a>

To transfer the sample program to your EC2 instance, connect to the instance using the AWS Management Console as described in the following procedure\.

**Note**  
Alternatively, connect using the Toolkit for Visual Studio \(as described in [Connecting to an Amazon EC2 Instance](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/managing-ec2.html) in the AWS Toolkit for Visual Studio User Guide\) and then copy the files from your local drive to the instance\. The Remote Desktop session is automatically configured so that your local drives are available to the instance\.

 **To run the sample program on the EC2 instance** 

1. Open the Amazon EC2 console\.

1. Get the password for your EC2 instance as follows:

1. In the navigation pane, click **Instances**\. Select the instance, and then click **Connect**\.

1. In the **Connect To Your Instance** dialog box, click **Get Password**\. \(It will take a few minutes after the instance is launched before the password is available\.\)

1. Click **Browse** and navigate to the private key file you created when you launched the instance\. Select the file and click **Open** to copy the entire contents of the file into contents box\.

1. Click **Decrypt Password**\. The console displays the default administrator password for the instance in the **Connect To Your Instance** dialog box, replacing the link to **Get Password** shown previously with the actual password\.

1. Record the default administrator password, or copy it to the clipboard\. You need this password to connect to the instance\.

1. Connect to your EC2 instance as follows:

1. Click **Download Remote Desktop File**\. When your browser prompts you to do so, save the `.rdp` file\. When you have finished, you can click **Close** to dismiss the **Connect To Your Instance** dialog box\.

1. Navigate to your downloads directory, right\-click the `.rdp` file, and then select **Edit**\. On the **Local Resources** tab, under **Local devices and resources**, click **More**\. Select **Drives** to make your local drives available to your instance, and then click **OK**\.

1. Click **Connect** to connect to your instance\. You may get a warning that the publisher of the remote connection is unknown\.

1. Log in to the instance as prompted, using the default **Administrator** account and the default administrator password that you recorded or copied previously\.

   Sometimes copying and pasting content can corrupt data\. If you encounter a “Password Failed” error when you log in, try typing in the password manually\. For more information, see [Connecting to Your Windows Instance Using RDP](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html) and [Troubleshooting Windows Instances](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/troubleshooting-windows-instances.html) in the Amazon EC2 User Guide for Windows Instances\.

1. Copy both the program and the AWS assembly \(`AWSSDK.dll`\) from your local drive to the instance\.

1. Run the program and verify that it succeeds because it uses the credentials provided by the IAM role\.

   ```
   Retrieving (GET) an object
   ```