--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Granting Access Using an IAM Role<a name="net-dg-hosm"></a>

This \.NET example shows you how to:
+ Create a sample program that retrieves an object from Amazon S3
+ Create an IAM role
+ Launch an Amazon EC2 instance and specify the IAM role
+ Run the sample on the Amazon EC2 instance

## The Scenario<a name="the-scenario"></a>

All requests to AWS must be cryptographically signed by using credentials issued by AWS\. Therefore, you need a strategy to manage credentials for software that runs on Amazon EC2 instances\. You have to distribute, store, and rotate these credentials securely, but also keep them accessible to the software\.

IAM roles enable you to effectively manage AWS credentials for software running on EC2 instances\. You create an IAM role and configure it with the permissions the software requires\. For more information about the benefits of using IAM roles, see [IAM Roles for Amazon EC2](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/iam-roles-for-amazon-ec2.html) in the Amazon EC2 User Guide for Windows Instances and [Roles \(Delegation and Federation\)](https://docs.aws.amazon.com/IAM/latest/UserGuide/WorkingWithRoles.html) in the IAM User Guide\.

To use the permissions, the software constructs a client object for the AWS service\. The constructor searches the credentials provider chain for credentials\. For \.NET, the credentials provider chain is as follows:
+ The `App.config` file
+ The instance metadata associated with the IAM role for the EC2 instance

If the client doesn’t find credentials in `App.config`, it retrieves temporary credentials that have the same permissions as those associated with the IAM role from instance metadata\. The credentials are stored by the constructor on behalf of the application software, and are used to make calls to AWS from that client object\. Although the credentials are temporary and eventually expire, the SDK client periodically refreshes them so that they continue to enable access\. This periodic refresh is completely transparent to the application software\.

The following examples show a sample program that retrieves an object from Amazon S3 using the AWS credentials you configure\. You create an IAM role to provide the AWS credentials\. Finally, you launch an instance with an IAM role that provides the AWS credentials to the sample program running on the instance\.

## Create a Sample that Retrieves an Object from Amazon S3<a name="net-dg-using-hosm-to-retrieve-an-object-from-ec2"></a>

The following sample code requires a text file in an Amazon S3 bucket that you have access to, and AWS credentials that provide you with access to the Amazon S3 bucket\.

For more information about creating an Amazon S3 bucket and uploading an object, see the [Amazon S3 Getting Started Guide](https://docs.aws.amazon.com/AmazonS3/latest/gsg/)\. For more information about AWS credentials, see [Configuring AWS Credentials](net-dg-config-creds.md)\.

```
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3ShowTextItem
{
    class S3Sample
    {
        static async Task<GetObjectResponse> MyGetObjectAsync(string region, string bucket, string item)
        {
            RegionEndpoint reg = RegionEndpoint.GetBySystemName(region);
            AmazonS3Client s3Client = new AmazonS3Client(reg);

            Console.WriteLine("Retrieving (GET) an object");

            GetObjectResponse response = await s3Client.GetObjectAsync(bucket, item, new CancellationToken());

            return response;
        }

        public static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("You must supply a region, bucket name, text file name, and output file name");
                return;
            }

            try
            {
                Task<GetObjectResponse> response = MyGetObjectAsync(args[0], args[1], args[2]);

                Stream responseStream = response.Result.ResponseStream;
                StreamReader reader = new StreamReader(responseStream);

                string responseBody = reader.ReadToEnd();

                using(FileStream s = new FileStream(args[3], FileMode.Create))
                using(StreamWriter writer = new StreamWriter(s))
                {
                    writer.WriteLine(responseBody);
                }
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message, s3Exception.InnerException);
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
```

**To test the sample code**

1. Open Visual Studio and create a **Console App \(\.NET Framework\)** project using \.NET Framework 4\.5 or later\.

1. Add the [AWSSDK\.S3](http://www.nuget.org/packages/AWSSDK.S3) NuGet package to your project\.

1. Replace the code in the `Program.cs` file with the sample code shown above\.

1. Compile and run the sample program\. If the program succeeds, it displays the following output and creates a file on your local drive that contains the text it retrieved from the text file in Amazon S3\.

   ```
   Retrieving (GET) an object
   ```

   If the program fails, be sure you’re using credentials that provide you with access to the bucket\.

1. \(Optional\) Transfer the sample program to a running Windows instance on which you haven’t set up credentials\. Run the program and verify that it fails because it can’t locate credentials\.

## Create an IAM Role<a name="net-dg-create-the-role"></a>

Create an IAM role that has the appropriate permissions to access Amazon S3\.

**To create the IAM role**

1. Open the IAM console\.

1. In the navigation pane, choose **Roles**, and then choose **Create New Role**\.

1. Type a name for the role, and then choose **Next Step**\. Remember this name because you’ll need it when you launch your EC2 instance\.

1. Under **AWS Service Roles**, choose **Amazon EC2**\. Under **Select Policy Template**, choose **Amazon S3 Read Only Access**\. Review the policy, and then choose **Next Step**\.

1. Review the role information, and then choose **Create Role**\.

## Launch an EC2 Instance and Specify the IAM Role<a name="net-dg-launch-ec2-instance-with-instance-profile"></a>

You can use the Amazon EC2 console or the AWS SDK for \.NET to launch an EC2 instance with an IAM role\.
+ Using the console: Follow the directions in [Launching a Windows Instance](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/EC2Win_GetStarted.html#EC2Win_LaunchInstance.html) in the Amazon EC2 User Guide for Windows Instances\. When you reach the **Review Instance Launch** page, choose **Edit instance details**\. In **IAM role**, specify the IAM role you created previously\. Complete the procedure as directed\. You’ll need to create or use an existing security group and key pair to connect to the instance\.
+ Using the AWS SDK for \.NET: See [Launching an Amazon EC2 Instance](run-instance.md)\.

An IAM user can’t launch an instance with an IAM role without the permissions granted by the following policy\.

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
Alternatively, connect using the Toolkit for Visual Studio \(see [Connecting to an Amazon EC2 Instance](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/tkv-ec2-ami.html#connect-ec2) in the AWS Toolkit for Visual Studio\) and then copy the files from your local drive to the instance\. The Remote Desktop session is automatically configured so that your local drives are available to the instance\.

**To run the sample program on the EC2 instance**

1. Open the Amazon EC2 console\.

1. Get the password for your EC2 instance:

   1. In the navigation pane, choose **Instances**\. Choose the instance, and then choose **Connect**\.

   1. In the **Connect To Your Instance** dialog box, choose **Get Password**\. \(It will take a few minutes after the instance is launched before the password is available\.\)

   1. Choose **Browse** and navigate to the private key file you created when you launched the instance\. Choose the file, and then choose **Open** to copy the file’s contents into the contents box\.

   1. Choose **Decrypt Password**\. The console displays the default administrator password for the instance in the **Connect To Your Instance** dialog box, replacing the link to **Get Password** shown earlier with the password\.

   1. Record the default administrator password or copy it to the clipboard\. You need this password to connect to the instance\.

1. Connect to your EC2 instance:

   1. Choose **Download Remote Desktop File**\. When your browser prompts you, save the `.rdp` file\. When you finish, choose **Close** to close the **Connect To Your Instance** dialog box\.

   1. Navigate to your downloads directory, right\-click the `.rdp` file, and then choose **Edit**\. On the **Local Resources** tab, under **Local devices and resources**, choose **More**\. Choose **Drives** to make your local drives available to your instance\. Then choose **OK**\.

   1. Choose **Connect** to connect to your instance\. You may get a warning that the publisher of the remote connection is unknown\.

   1. Sign in to the instance when prompted, using the default **Administrator** account and the default administrator password you recorded or copied previously\.

      Sometimes copying and pasting content can corrupt data\. If you encounter a “Password Failed” error when you sign in, try typing in the password manually\. For more information, see [Connecting to Your Windows Instance Using RDP](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/connecting_to_windows_instance.html) and [Troubleshooting Windows Instances](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/troubleshooting-windows-instances.html) in the Amazon EC2 User Guide for Windows Instances\.

1. Copy the program and the AWS assemblies \(`AWSSDK.Core.dll` and `AWSSDK.S3.dll`\) from your local drive to the instance\.

1. Run the program and verify that it succeeds using the credentials provided by the IAM role\.

   ```
   Retrieving (GET) an object
   ```