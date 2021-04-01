--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# Programming with the AWS Resource APIs for \.NET<a name="resource-level-apis-intro"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13b7b3b1"></a>

The AWS SDK for \.NET provides the AWS Resource APIs for \.NET\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with resources that are managed by AWS services\. A resource is a logical object that is exposed by an AWS service’s APIs\. For example, AWS Identity and Access Management \(IAM\) exposes users and groups as resources that can be programmatically accessed more directly by these resource APIs than by other means\.

The AWS Resource APIs for \.NET are currently provided as a preview\. This means that these resource APIs may frequently change in response to customer feedback, and these changes may happen without advance notice\. Until these resource APIs exit the preview stage, please be cautious about writing and distributing production\-quality code that relies on them\.

Using the AWS Resource APIs for \.NET provide these benefits:
+ The resource APIs in the the SDK are easier to understand conceptually than their low\-level API counterparts\. The low\-level APIs in the the SDK typically consist of sets of matching request\-and\-response objects that correspond to HTTP\-based API calls focusing on somewhat isolated AWS service constructs\. In contrast, these resource APIs represent logical relationships among resources within AWS services and intuitively use familiar \.NET programming constructs\.
+ Code that you write with the resource APIs is easier for you and others to comprehend when compared to their low\-level API equivalents\. Instead of writing somewhat complex request\-and\-response style code with the low\-level APIs to access resources, you can get directly to resources with the resource APIs\. If you’re working with a team of developers in the same code base, it’s typically easier to understand what has already been coded and to start contributing quickly to existing code\.
+ You will typically write less code with the resource APIs than with equivalent low\-level API code\. Request\-and\-response style code with the low\-level APIs can sometimes be quite long\. Equivalent resource APIs code is typically much shorter, more compact, and easer to debug\.

Here’s a brief example of using C\# and the AWS Resource APIs for \.NET to create a new IAM user account:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var user = iam.CreateUser("DemoUser");

  Console.WriteLine("User Name = '{0}', ARN = '{1}'", 
    user.Name, user.Arn);
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine("User 'DemoUser' already exists.");
}
```

Compare this to an equivalent example of using the low\-level APIs:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
var request = new CreateUserRequest 
{ 
  UserName = "DemoUser" 
};

try
{
  var response = client.CreateUser(request);

  Console.WriteLine("User Name = '{0}', ARN = '{1}'", 
    response.User.UserName, response.User.Arn);
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine("User 'DemoUser' already exists.");
}
```

Even with this brief code example, you’ll see that the resource APIs code is a bit easier to comprehend than the low\-level code, and the resource APIs code is a bit shorter and more compact than its low\-level counterpart\.

There are a few limitations to note when using the resource APIs as compared to the low\-level APIs in the the SDK:
+ Not all of the AWS services currently have resource APIs \(although this number is growing\)\. Currently, the following AWS services have resource APIs in the the SDK:
  + AWS CloudFormation
  + Amazon S3 Glacier
  + AWS Identity and Access Management \(IAM\)
  + Amazon Simple Notification Service \(Amazon SNS\)
  + Amazon Simple Queue Service \(Amazon SQS\)
+ The resource APIs are currently provided as a preview\. Please be cautious about writing and distributing production\-quality code that relies on these resource APIs, especially as the resource APIs may undergo frequent changes during the preview stage\.

The following information describes how to download and reference the resource APIs\. Links to code examples and related programming concepts for supported AWS services are also provided\.

### Download and Reference the AWS Resource APIs for \.NET<a name="resource-level-apis-intro-setup"></a>

1. If you have an existing project in Visual Studio that you want to use the resource APIs with, and that project is already referencing the AWS \.NET library file \(`AWSSDK.dll`\), you must remove this reference\. This reference is set by default if you have the AWS Toolkit for Visual Studio installed and you have created a project based upon one of the AWS project templates \(for example, the Visual C\# AWS Console Project template\)\. Or, you may have previously set a reference to the library explicitly, which the the SDK typically installs to `drive:\Program Files (x86)\AWS SDK for .NET\bin`\. To remove the reference for example in **Solution Explorer** in Visual Studio, in the **References** folder, right\-click **AWSSDK** and then click **Remove**\.

1. Download the AWS Resource APIs for \.NET library file from the [resourceAPI\-preview](https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview) branch of the `aws-sdk-net` GitHub repository onto your development machine\. To do this, in the [binaries](https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries) folder at that location, download and then unzip the file named [dotnet35\.zip](https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries/dotnet35.zip) \(for projects that rely on the \.NET Framework 3\.5\) or [dotnet45\.zip](https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries/dotnet45.zip) \(for projects that rely on the \.NET Framework 4\.5\)\. Note that because these zip files contains a file that is also named `AWSSDK.dll`, make sure to unzip the file to a location *other* than where your AWS \.NET library file is already installed\. For example, unzip the file to any location *other* than `drive:\Program Files (x86)\AWS SDK for .NET\bin`\. The unzipped contents contain both \.NET Framework 3\.5 and 4\.5 versions of the AWS Resource APIs for \.NET library file \(`AWSSDK.dll`\), which you can set a reference to from your projects\.

   Note that after unzipping, there will be three files: `AWSSDK.dll`, `AWSSDK.pdb`, and `AWSSDK.xml`\. To enable robust debugging and help within Visual Studio, make sure that these three files remain together in the same folder\.

1. From the project in Visual Studio that you want to use the resource APIs with, set a reference to the AWS Resource APIs for \.NET library file that you just unzipped\. To do this for example in **Solution Explorer** in Visual Studio, right\-click the **References** folder; click **Add Reference**; click **Browse**; browse to and select the `AWSSDK.dll` file that you just unzipped; click **Add** and then click **OK**\.

1. Import the specific resource APIs in the AWS Resource APIs for \.NET that you want to use in your project’s code\. These APIs typically take the format `Amazon.ServiceName.Resources`, where \{ServiceName\} is typically some recognizable phrase that corresponds to the specific service\. For example for the AWS Identity and Access Management resource APIs, in C\# you would include the following `using` directive at the top of a class file:

   ```
   using Amazon.IdentityManagement.Resources;
   ```

1. As needed, import any corresponding low\-level APIs that the specific resource APIs rely upon\. These APIs typically take the format `Amazon.ServiceName.Model` and sometimes also `Amazon.ServiceName`, where \{ServiceName\} is typically some recognizable phrase that corresponds to the specific service\. For example for the AWS Identity and Access Management low\-level APIs, in C\# you would include the following `using` directives at the top of a class file:

   ```
   using Amazon.IdentityManagement.Model;
   // Possibly also the following, depending on which of the resource APIs that you use:
   using Amazon.IdentityManagement
   ```

1. Because the resource APIs are currently provided as a preview, you should be cautious about writing production\-quality code that relies on them, especially as the resource APIs may undergo frequent changes during the preview stage\. However, if you choose to distribute the project anyway, make sure to include a copy of the AWS Resource APIs for \.NET library file\. To do this for example in **Solution Explorer** in Visual Studio, within the **References** folder, click **AWSSDK**; in the **Properties** window, next to **Copy Local**, select **True** if it is not already selected\.
**Note**  
If you distribute a project that has a copy of the resource APIs library file included, and then the resource library APIs change, the only way for your project to include the new changes is to redistribute your project with an updated resource APIs library file copied locally\.

### Code Examples for Resource APIs<a name="resource-level-apis-intro-examples"></a>

The following links provide code examples for AWS services that support resource\-level APIs in the the SDK\.
+  [CloudFormation](cloudformation-apis-intro.md#cloudformation-apis-intro-resource-level) 
+  [Amazon Glacier](glacier-apis-intro.md#glacier-apis-intro-resource-level) 
+  [AWS Identity and Access Management \(IAM\)](iam-resource-api-examples.md) 
+  [Amazon Simple Notification Service \(Amazon SNS\)](sns-apis-intro.md#sns-apis-intro-resource-level) 
+  [Amazon Simple Queue Service \(Amazon SQS\)](sqs-apis-intro.md#sqs-apis-intro-resource-level) 