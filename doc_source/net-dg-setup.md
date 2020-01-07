--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Getting Started with the AWS SDK for \.NET<a name="net-dg-setup"></a>

To get started with the AWS SDK for \.NET, complete the following tasks:

**Topics**
+ [Create an AWS Account and Credentials](#net-dg-signup)
+ [Install the \.NET Development Environment](#net-dg-dev-env)
+ [Install the AWS SDK for \.NET](#net-dg-install-net-sdk)
+ [Start a New Project](#net-dg-start-new-project)

## Create an AWS Account and Credentials<a name="net-dg-signup"></a>

To access AWS, you need an AWS account\.

 **To sign up for an AWS account** 

1. Open [http://aws\.amazon\.com/](https://aws.amazon.com/), and then choose **Create an AWS Account**\.

1. Follow the instructions\. Part of the sign\-up procedure involves receiving a phone call and entering a PIN using the phone keypad\.

AWS sends you a confirmation email after the sign\-up process is complete\. At any time, you can view your current account activity and manage your account by going to [http://aws\.amazon\.com](http://aws.amazon.com) and clicking **My Account/Console**\.

To use the SDK, you must have a set of valid AWS credentials, which consist of an access key and a secret key\. These keys are used to sign programmatic web service requests and enable AWS to verify that the request comes from an authorized source\. You can obtain a set of account credentials when you create your account\. However, we recommend that you do not use these credentials with the SDK\. Instead, [create one or more IAM users](https://docs.aws.amazon.com/IAM/latest/UserGuide/Using_SettingUpUser.html), and use those credentials\. For applications that run on EC2 instances, you can use [IAM roles](https://docs.aws.amazon.com/IAM/latest/UserGuide/WorkingWithRoles.html) to provide temporary credentials\.

The preferred approach for handling credentials is to create a profile for each set of credentials in the SDK Store\. You can create and manage profiles with the AWS Toolkit for Visual Studio, PowerShell cmdlets, or programmatically with the SDK\. These credentials are encrypted and stored separately from any project\. You then reference the profile by name in your application, and the credentials are inserted at build time\. This approach ensures that your credentials are not unintentionally exposed with your project on a public site\. For more information, see [Setting Up the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/tkv_setup.html) and [Configuring AWS Credentials](net-dg-config-creds.md)\.

For more information about managing your credentials, see [Best Practices for Managing AWS Access Keys](https://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html)\.

## Install the \.NET Development Environment<a name="net-dg-dev-env"></a>

To use the SDK, you must have the following installed\.

### Requirements<a name="requirements"></a>
+ \(Required\) Microsoft \.NET Framework 3\.5 or later
+ \(Required\) Microsoft Visual Studio 2010 or later
+ \(Required\) The SDK
+ \(Recommended\) AWS Toolkit for Visual Studio, a plugin that provides a user interface for managing your AWS resources from Visual Studio, and includes the SDK\. For more information, see [Using the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/welcome.html)\.

**Note**  
We recommend using Visual Studio Professional 2010 or higher to implement your applications\.

## Install the AWS SDK for \.NET<a name="net-dg-install-net-sdk"></a>

The following procedure describes how to install the AWS Tools for Windows, which contains the AWS SDK for \.NET\.

 **To install the SDK** 

1. Go to [AWS SDK for \.NET](https://aws.amazon.com/sdk-for-net/)\. Click the **Download** button in the upper right corner of the page\. Your browser will prompt you to save the install file\.
**Note**  
The AWS SDK for \.NET is also available on [GitHub](https://github.com/aws/aws-sdk-net)\.

1. To begin the install process, open the saved install file and follow the on\-screen instructions\. Version 2 of the SDK can be found in the `past-releases` folder of the SDK installation directory\.
**Note**  
By default, the AWS Tools for Windows is installed in the *Program Files* directory, which requires administrator privileges\. To install the AWS Tools for Windows as a non\-administrator, specify a different installation directory\.

1. \(Optional\) You can install extensions for the SDK, which include a session state provider and a trace listener\. For more information, see [Install AWS Assemblies with NuGet](net-dg-nuget.md)\.

## Start a New Project<a name="net-dg-start-new-project"></a>

If you have installed the Toolkit for Visual Studio on Visual Studio Professional, it includes C\# project templates for a variety of AWS services, including the following basic templates:

**AWS Console Project**  
A console application that makes basic requests to Amazon S3, Amazon SimpleDB, and Amazon EC2\.

**AWS Empty Project**  
A console application that does not include any code\.

**AWS Web Project**  
An ASP\.NET application that makes basic requests to Amazon S3, Amazon SimpleDB, and Amazon EC2\.

You can also base your application on one of the standard Visual Studio project templates\. Just add a reference to the AWS \.NET library \(`AWSSDK.dll`\), which is located in the `past-releases` folder of the SDK installation directory\.

The following procedure gets you started by creating and running a new AWS Console project for Visual Studio 2012; the process is similar for other project types and Visual Studio versions\. For more information on how to configure an AWS application, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

 **To start a new project** 

1. In Visual Studio, on the **File** menu, select **New**, and then click **Project** to open the **New Project** dialog box\.

1. Select **AWS** from the list of installed templates and select the **AWS Console Project** project template\. Enter a project name, and then click **OK**\.  
![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v2/developer-guide/images/new-proj-dlg-net-dg.png)

1. Use the **AWS Access Credentials** dialog box to configure your application\.
+ Specify which account profile your code should use to access AWS\. To use an existing profile, click **Use existing profile** and select the profile from the list\. To add a new profile, click **Use a new profile** and enter the credentials information\. For more information about profiles, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.
+ Specify a default AWS region\.

![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v2/developer-guide/images/creds-new-proj-net-dg.png)

1. Click **OK** to accept the configuration, which opens the project\. Examine the project’s `App.config` file, which will contain something like the following:

   ```
   <configuration>
       <appSettings>
           <add key="AWSProfileName" value="development"/>
           <add key="AWSRegion" value="us-west-2"/>
       </appSettings>
   </configuration>
   ```

   The Toolkit for Visual Studio puts the values you specified in the **AWS Access Credentials** dialog box into the two key\-value pairs in `appSettings`\.
**Note**  
Although using the `appSettings` element is still supported, we recommend that you move to using the `aws` element instead, for example:  

   ```
   <configuration>
     <configSections>
       <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
     </configSections>
     <aws region="us-west-2" profileName="development"/>
   </configuration>
   ```
For more information on use of the `aws` element, see [Configuration Files Reference for AWS SDK for \.NET](net-dg-config-ref.md)\.

1. Click `F5` to compile and run the application, which prints the number of EC2 instances, Amazon SimpleDB tables, and Amazon S3 buckets in your account\.

For more information about configuring an AWS application, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.