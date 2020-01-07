# Retries and Timeouts<a name="retries-timeouts"></a>

The AWS SDK for \.NET allows you to configure the number of retries and the timeout values for HTTP requests to AWS services\. If the default values for retries and timeouts are not appropriate for your application, you can adjust them for your specific requirements, but it is important to understand how doing so will affect the behavior of your application\.

To determine which values to use for retries and timeouts, consider the following:
+ How should the AWS SDK for \.NET and your application respond when network connectivity degrades or an AWS service is unreachable? Do you want the call to fail fast, or is it appropriate for the call to keep retrying on your behalf?
+ Is your application a user\-facing application or website that must be responsive, or is it a background processing job that has more tolerance for increased latencies?
+ Is the application deployed on a reliable network with low latency, or it is deployed at a remote location with unreliable connectivity?

## Retries<a name="retries"></a>

The AWS SDK for \.NET will retry requests that fail due to server\-side throttling or dropped connections\. You can use the `MaxErrorRetry` property of the [ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class to specify the number of retries at the service client level\. the AWS SDK for \.NET will retry the operation the specified number of times before failing and throwing an exception\. By default, the `MaxErrorRetry` property is set to 4, except for the [AmazonDynamoDBConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/TDynamoDBConfig.html) class, which defaults to 10 retries\. When a retry occurs, it increases the latency of your request\. You should configure your retries based on your application limits for total request latency and error rates\.

## Timeouts<a name="timeouts"></a>

The AWS SDK for \.NET allows you to configure the request timeout and socket read/write timeout values at the service client level\. These values are specified in the `Timeout` and the `ReadWriteTimeout` properties of the [ClientConfig](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TClientConfig.html) class, respectively\. These values are passed on as the `Timeout` and `ReadWriteTimeout` properties of the [HttpWebRequest](https://msdn.microsoft.com/en-us/library/System.Net.HttpWebRequest%28v=vs.110%29.aspx) objects created by the AWS service client object\. By default, the `Timeout` value is 100 seconds and the `ReadWriteTimeout` value is 300 seconds\.

When your network has high latency, or conditions exist that cause an operation to be retried, using long timeout values and a high number of retries can cause some SDK operations to seem unresponsive\.

**Note**  
The version of the AWS SDK for \.NET that targets the portable class library \(PCL\) uses the [HttpClient](http://msdn.microsoft.com/en-us/library/system.net.http.httpclient%28v=vs.110%29.aspx) class instead of the `HttpWebRequest` class, and supports the [Timeout](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient.timeout%28v=vs.110%29.aspx) property only\.

The following are the exceptions to the default timeout values\. These values are overridden when you explicitly set the timeout values\.
+  `Timeout` and `ReadWriteTimeout` are set to the maximum values if the method being called uploads a stream, such as [AmazonS3Client\.PutObject\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3PutObjectPutObjectRequest.html), [AmazonS3Client\.UploadPart\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3UploadPartUploadPartRequest.html), [AmazonGlacierClient\.UploadArchive\(\)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/MGlacierUploadArchiveUploadArchiveRequest.html), and so on\.
+ The version of the AWS SDK for \.NET that targets the \.NET Framework 4\.5 sets `Timeout` and `ReadWriteTimeout` to the maximum values for all [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) and [AmazonGlacierClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/TGlacierClient.html) objects\.
+ The version of the AWS SDK for \.NET that targets the portable class library \(PCL\) sets `Timeout` to the maximum value for all [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) and [AmazonGlacierClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Glacier/TGlacierClient.html) objects\.

## Example<a name="retries-timeouts-example"></a>

The following example shows how to specify a maximum of 2 retries, a timeout of 10 seconds, and a read/write timeout of 10 seconds for an [AmazonS3Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3Client.html) object\.

```
var client =  new AmazonS3Client(
  new AmazonS3Config
  {
    Timeout = TimeSpan.FromSeconds(10),            // Default value is 100 seconds
    ReadWriteTimeout = TimeSpan.FromSeconds(10),   // Default value is 300 seconds
    MaxErrorRetry = 2                              // Default value is 4 retries
  });
```