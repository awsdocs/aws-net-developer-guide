--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Create an Amazon SQS Client<a name="InitSQSClient"></a>

You will need an Amazon SQS client in order to create and use an Amazon SQS queue\. Before configuring your client, you should create an `App.Config` file to specify your AWS credentials\.

You specify your credentials by referencing the appropriate profile in the appSettings section of the file\. The following example specifies a profile named \{my\_profile\}\. For more information on credentials and profiles, see [Configuring Your AWS SDK for \.NET Application](net-dg-config.md)\.

```
<?xml version="1.0"?>
  <configuration>
    <startup>
      <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
    </startup>
    <configSections>
      <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
    </configSections> <aws profileName="{my_profile}"/>
  </configuration>
```

After you create this file, you are ready to create and initialize your Amazon SQS client\.

 **To create and initialize an Amazon SQS client** 

1. Create and initialize an [AmazonSQSConfig](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSSQSConfigNET45.html) instance, and set the [ServiceURL](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PRuntimeClientConfigServiceURLNET45.html) property with the protocol and service endpoint, as follows:

   ```
   AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();
   
   amazonSQSConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";
   ```

   ```
   AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();
   
   amazonSQSConfig.ServiceURL = "http://sqs.cn-north-1.amazonaws.com";
   ```

   The AWS SDK for \.NET uses US East \(N\. Virginia\) Region as the default region if you do not specify a region in your code\. However, the AWS Management Console uses US West \(Oregon\) Region as its default\. Therefore, when using the AWS Management Console in conjunction with your development, be sure to specify the same region in both your code and the console\.

   Go to [Regions and Endpoints](https://docs.aws.amazon.com/general/latest/gr/rande.html) for the current list of regions and corresponding endpoints for each of the services offered by AWS\.

1. Use the `AmazonSQSConfig` instance to create and initialize an [AmazonSQSClient](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TSQSSQSNET45.html) instance, as follows:

   ```
   amazonSQSClient = new AmazonSQSClient(amazonSQSConfig);
   ```

You can now use the client to create an Amazon SQS queue\. For information about creating a queue, see [Create an Amazon SQS Queue](CreateQueue.md#create-sqs-queue)\.