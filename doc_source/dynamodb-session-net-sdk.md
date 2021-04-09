--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Managing ASP\.NET Session State with Amazon DynamoDB<a name="dynamodb-session-net-sdk"></a>

ASP\.NET applications often store session state data in memory\. However, this approach doesn’t scale well\. After the application grows beyond a single web server, the session state must be shared between servers\. A common solution is to set up a dedicated session\-state server with Microsoft SQL Server, but this approach also has drawbacks: you must administer another machine; the session\-state server is a single point of failure; and the session\-state server itself can become a performance bottleneck\.

 [DynamoDB](https://aws.amazon.com/dynamodb/), a NoSQL database store from AWS, provides an effective solution for sharing session state across web servers without incurring any of these drawbacks\.

**Note**  
Regardless of the solution you choose, be aware that Amazon DynamoDB enforces limits on the size of an item\. None of the records you store in DynamoDB can exceed this limit\. For more information, see [Limits in DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Limits.html) in the Amazon DynamoDB Developer Guide\.

The AWS SDK for \.NET includes `AWS.SessionProvider.dll`, which contains an ASP\.NET session state provider\. It also includes the *AmazonDynamoDBSessionProviderSample* sample, which demonstrates how to use Amazon DynamoDB as a session state provider\.

For more information about using session state with ASP\.NET applications, go to the [Microsoft documentation](https://docs.microsoft.com/en-us/previous-versions/ms178581(v=vs.140))\.

## Create the *ASP\.NET\_SessionState* Table<a name="asdf"></a>

When your application starts, it looks for an Amazon DynamoDB table named, by default, `ASP.NET_SessionState`\. We recommend you create this table before you run your application for the first time\.

 **To create the ASP\.NET\_SessionState table** 

1. Choose **Create Table**\. The **Create Table** wizard opens\.

1. In the **Table name** text box, enter `ASP.NET_SessionState`\.

1. In the **Primary key** field, enter `SessionId` and set the type to `String`\.

1. When all your options are entered as you want them, choose **Create**\.

The `ASP.NET_SessionState` table is ready for use when its status changes from `CREATING` to `ACTIVE`\.

**Note**  
If you decide not to create the table beforehand, the session state provider will create the table during its initialization\. See the `web.config` options below for a list of attributes that act as configuration parameters for the session state table\. If the provider creates the table, it will use these parameters\.

## Configure the Session State Provider<a name="net-dg-ddb-config-sess-provider"></a>

 **To configure an ASP\.NET application to use DynamoDB as the session\-state server** 

1. Add references to both `AWSSDK.dll` and `AWS.SessionProvider.dll` to your Visual Studio ASP\.NET project\. These assemblies are available by installing the [AWS SDK for \.NET](net-dg-install-assemblies.md#net-dg-install-net-sdk)\. You can also install them by using [NuGet](net-dg-install-assemblies.md#net-dg-nuget)\.

   In earlier versions of the SDK, the functionality for the session state provider was contained in `AWS.Extension.dll`\. To improve usability, the functionality was moved to `AWS.SessionProvider.dll`\. For more information, see the blog post [AWS\.Extension Renaming](http://blogs.aws.amazon.com/net/post/Tx27RWMCNAVWZN9/AWS-Extensions-renaming)\.

1. Edit your application’s `Web.config` file\. In the `system.web` element, replace the existing `sessionState` element with the following XML fragment:

   ```
   <sessionState timeout="20" mode="Custom" customProvider="DynamoDBSessionStoreProvider">
     <providers>
       <add name="DynamoDBSessionStoreProvider"
            type="Amazon.SessionProvider.DynamoDBSessionStateStore"
            AWSProfileName="{profile_name}"
            Region="us-west-2" />
     </providers>
   </sessionState>
   ```

   The profile represents the AWS credentials that are used to communicate with DynamoDB to store and retrieve the session state\. If you are using the AWS SDK for \.NET and are specifying a profile in the `appSettings` section of your application’s `Web.config` file, you do not need to specify a profile in the `providers` section; the AWS \.NET client code will discover it at run time\. For more information, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

   If the web server is running on an Amazon EC2 instance configured to use IAM roles for EC2 instances, then you do not need to specify any credentials in the `Web.config` file\. In this case, the AWS \.NET client will use the IAM role credentials\. For more information, see [Granting Access Using an IAM Role](net-dg-hosm.md) and [Security Considerations](#net-dg-ddb-sess-security)\.

### Web\.config Options<a name="net-dg-dd-config-opts"></a>

You can use the following configuration attributes in the `providers` section of your `Web.config` file:

** *AWSAccessKey* **  
Access key ID to use\. This can be set either in the `providers` or `appSettings` section\. We recommend not using this setting\. Instead, specify credentials by using `AWSProfileName` to specify a profile\.

** *AWSSecretKey* **  
Secret key to use\. This can be set either in the `providers` or `appSettings` section\. We recommend not using this setting\. Instead, specify credentials by using `AWSProfileName` to specify a profile\.

** *AWSProfileName* **  
The profile name associated with the credentials you want to use\. For more information, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

** *Region* **  
Required `string` attribute\. The AWS region in which to use Amazon DynamoDB\. For a list of AWS regions, see [Regions and Endpoints: DynamoDB](https://docs.aws.amazon.com/general/latest/gr/rande.html#ddb_region)\.

** *Application* **  
Optional `string` attribute\. The value of the `Application` attribute is used to partition the session data in the table so that the table can be used for more than one application\.

** *Table* **  
Optional `string` attribute\. The name of the table used to store session data\. The default is `ASP.NET_SessionState`\.

** *ReadCapacityUnits* **  
Optional `int` attribute\. The read capacity units to use if the provider creates the table\. The default is 10\.

** *WriteCapacityUnits* **  
Optional `int` attribute\. The write capacity units to use if the provider creates the table\. The default is 5\.

** *CreateIfNotExist* **  
Optional `boolean` attribute\. The `CreateIfNotExist` attribute controls whether the provider will auto\-create the table if it doesn’t exist\. The default is true\. If this flag is set to false and the table doesn’t exist, an exception will be thrown\.

## Security Considerations<a name="net-dg-ddb-sess-security"></a>

After the DynamoDB table is created and the application is configured, sessions can be used as with any other session provider\.

As a security best practice, we recommend you run your applications with the credentials of an [IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/) user\. You can use either the [IAM Management Console](https://console.aws.amazon.com/iam/home) or the [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/) to create IAM users and define access policies\.

The session state provider needs to be able to call the [DeleteItem](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DeleteItem.html), [DescribeTable](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DescribeTable.html), [GetItem](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GetItem.html), [PutItem](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/PutItem.html), and [UpdateItem](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/UpdateItem.html) operations for the table that stores the session data\. The sample policy below can be used to restrict the IAM user to only the operations needed by the provider for an instance of DynamoDB running in us\-west\-2:

```
{ "Version" : "2012-10-17",
"Statement" : [
  {
    "Sid" : "1",
    "Effect" : "Allow",
    "Action" : [
        "dynamodb:DeleteItem",
        "dynamodb:DescribeTable",
        "dynamodb:GetItem",
        "dynamodb:PutItem",
        "dynamodb:UpdateItem"
    ],
    "Resource" : "arn:aws:dynamodb:us-west-2:{<YOUR-AWS-ACCOUNT-ID>}:table/ASP.NET_SessionState"
    }
  ]
}
```