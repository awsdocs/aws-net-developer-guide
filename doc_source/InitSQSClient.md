--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Creating an Amazon SQS Client<a name="InitSQSClient"></a>

You need an Amazon SQS client in order to create and use an Amazon SQS queue\. Before configuring your client, you should create an `App.Config` file to specify your AWS credentials\.

You specify your credentials by referencing the appropriate profile in the `appSettings` section of the file\.

The following example specifies a profile named `my_profile`\. For more information about credentials and profiles, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

```
<?xml version="1.0"?>
<configuration>
  <configSections>
    <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
  </configSections>
  <aws profileName="my_profile"/>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
  </startup>
</configuration>
```

After you create this file, you’re ready to create and initialize your Amazon SQS client\.

**To create and initialize an Amazon SQS client**

1. Create and initialize an [AmazonSQSConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSConfig.html) instance, and then set the `ServiceURL` property with the protocol and service endpoint, as follows\.

   ```
   var sqsConfig = new AmazonSQSConfig();
   
   sqsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";
   ```

1. Use the `AmazonSQSConfig` instance to create and initialize an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSClient.html) instance, as follows\.

   ```
   var sqsClient = new AmazonSQSClient(sqsConfig);
   ```

You can now use the client to create an Amazon SQS queue\. For information about creating a queue, see [Creating an Amazon SQS Queue](CreateQueue.md#create-sqs-queue)\.