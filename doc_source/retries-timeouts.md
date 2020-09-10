--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Retries and timeouts<a name="retries-timeouts"></a>

The AWS SDK for \.NET enables you to configure the number of retries and the timeout values for HTTP requests to AWS services\. If the default values for retries and timeouts are not appropriate for your application, you can adjust them for your specific requirements, but it is important to understand how doing so will affect the behavior of your application\.

To determine which values to use for retries and timeouts, consider the following:
+ How should the AWS SDK for \.NET and your application respond when network connectivity degrades or an AWS service is unreachable? Do you want the call to fail fast, or is it appropriate for the call to keep retrying on your behalf?
+ Is your application a user\-facing application or website that must be responsive, or is it a background processing job that has more tolerance for increased latencies?
+ Is the application deployed on a reliable network with low latency, or is it deployed at a remote location with unreliable connectivity?

## Retries<a name="retries"></a>

### Overview<a name="w4aac15c15c11b5"></a>

The AWS SDK for \.NET can retry requests that fail due to server\-side throttling or dropped connections\. There are two properties of service configuration classes that you can use to specify the retry behavior of a service client\. Service configuration classes inherit these properties from the abstract [Amazon\.Runtime\.ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class of the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/):
+ `RetryMode` specifies one of three retry modes, which are defined in the [Amazon\.Runtime\.RequestRetryMode](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TRequestRetryMode.html) enumeration\.

  The default value for your application can be controlled by using the `AWS_RETRY_MODE` environment variable or the *retry\_mode* setting in the shared AWS config file\.
+ `MaxErrorRetry` specifies the number of retries allowed at the service client level; the SDK retries the operation the specified number of times before failing and throwing an exception\.

  The default value for your application can be controlled by using the `AWS_MAX_ATTEMPTS` environment variable or the *max\_attempts* setting in the shared AWS config file\.

Detailed descriptions for these properties can be found in the abstract [Amazon\.Runtime\.ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class of the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/)\. Each value of `RetryMode` corresponds by default to a particular value of `MaxErrorRetry`, as shown in the following table\.

|  |  |  | 
| --- |--- |--- |
| Legacy | 10 | 4 | 
| Standard | 10 | 2 | 
| Adaptive \(experimental\) | 10 | 2 | 

### Behavior<a name="w4aac15c15c11b9"></a>

**When your application starts**

When your application starts, default values for `RetryMode` and `MaxErrorRetry` are configured by the SDK\. These default values are used when you create a service client unless you specify other values\.
+ If the properties aren't set in your environment, the default for `RetryMode` is configured as *Legacy* and the default for `MaxErrorRetry` is configured with the corresponding value from the preceding table\.
+ If the retry mode has been set in your environment, that value is used as the default for `RetryMode`\. The default for `MaxErrorRetry` is configured with the corresponding value from the preceding table unless the value for maximum errors has also been set in your environment \(described next\)\.
+ If the value for maximum errors has been set in your environment, that value is used as the default for `MaxErrorRetry`\. Amazon DynamoDB is the exception to this rule; the default DynamoDB value for `MaxErrorRetry` is always the value from the preceding table\.

**As your application runs**

When you create a service client, you can use the default values for `RetryMode` and `MaxErrorRetry`, as described earlier, or you can specify other values\. To specify other values, create and include a service configuration object such as [AmazonDynamoDBConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/TDynamoDBConfig.html) or [AmazonSQSConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSQSConfig.html) when you create the service client\.

These values can't be changed for a service client after it has been created\.

**Considerations**

When a retry occurs, the latency of your request is increased\. You should configure your retries based on your application limits for total request latency and error rates\.

## Timeouts<a name="timeouts"></a>

The AWS SDK for \.NET enables you to configure the request timeout and socket read/write timeout values at the service client level\. These values are specified in the `Timeout` and the `ReadWriteTimeout` properties of the abstract [Amazon\.Runtime\.ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class\. These values are passed on as the `Timeout` and `ReadWriteTimeout` properties of the [HttpWebRequest](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpwebrequest) objects created by the AWS service client object\. By default, the `Timeout` value is 100 seconds and the `ReadWriteTimeout` value is 300 seconds\.

When your network has high latency, or conditions exist that cause an operation to be retried, using long timeout values and a high number of retries can cause some SDK operations to seem unresponsive\.

**Note**  
The version of the AWS SDK for \.NET that targets the portable class library \(PCL\) uses the [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient) class instead of the `HttpWebRequest` class, and supports the [Timeout](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout) property only\.

The following are the exceptions to the default timeout values\. These values are overridden when you explicitly set the timeout values\.
+ `Timeout` and `ReadWriteTimeout` are set to the maximum values if the method being called uploads a stream, such as [AmazonS3Client\.PutObjectAsync\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3PutObjectAsyncPutObjectRequestCancellationToken.html), [AmazonS3Client\.UploadPartAsync\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3UploadPartAsyncUploadPartRequestCancellationToken.html), [AmazonGlacierClient\.UploadArchiveAsync\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/MGlacierUploadArchiveAsyncUploadArchiveRequestCancellationToken.html), and so on\.
+ The versions of the AWS SDK for \.NET that target \.NET Framework set `Timeout` and `ReadWriteTimeout` to the maximum values for all [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) and [AmazonGlacierClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/TGlacierClient.html) objects\.
+ The versions of the AWS SDK for \.NET that target the portable class library \(PCL\) and \.NET Core set `Timeout` to the maximum value for all [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) and [AmazonGlacierClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/TGlacierClient.html) objects\.

## Example<a name="retries-timeouts-example"></a>

The following example shows you how to specify *Standard* retry mode, a maximum of 3 retries, a timeout of 10 seconds, and a read/write timeout of 10 seconds \(if applicable\)\. The [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) constructor is given an [AmazonS3Config](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Config.html) object\.

```
var s3Client = new AmazonS3Client(
  new AmazonS3Config
  {
    Timeout = TimeSpan.FromSeconds(10),
    // NOTE: The following property is obsolete for
    //       versions of the AWS SDK for .NET that target .NET Core.
    ReadWriteTimeout = TimeSpan.FromSeconds(10),
    RetryMode = RequestRetryMode.Standard,
    MaxErrorRetry = 3
  });
```