--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# AWS asynchronous APIs for \.NET<a name="sdk-net-async-api"></a>

The AWS SDK for \.NET uses the *Task\-based Asynchronous Pattern \(TAP\)* for its asynchronous implementation\. To learn more about the TAP, see [Task\-based Asynchronous Pattern \(TAP\)](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap) on docs\.microsoft\.com\.

This topic gives you an overview of how to use TAP in your calls to AWS service clients\.

**Note**  
The information in this topic is centered around \.NET Core and ASP\.NET Core\. If you're maintaining \.NET Framework 4\.5 code, the information in this topic also applies to the SDK's implementation of \.NET Framework 4\.5, which also uses TAP\.  
If you're looking for legacy information about how the AWS SDK for \.NET implemented asynchronous operations for \.NET Framework 3\.5, see [version 3](../../v3/developer-guide/sdk-net-async-api.html) of the guide instead\.

The asynchronous methods in the AWS SDK for \.NET API are operations based on the `Task` class or the `Task<TResult>` class\. See docs\.microsoft\.com for information about these classes: [Task class](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task), [Task<TResult> class](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\.

When these API methods are called in your code, they must be called within a function that is declared with the `async` keyword, as shown in the following example\.

```
static async Task Main(string[] args)
{
  ...
  // Call the function that contains the asynchronous API method.
  // Could also call the asynchronous API method directly from Main
  //  because Main is declared async
  var response = await ListBucketsAsync();
  Console.WriteLine($"Number of buckets: {response.Buckets.Count}");
  ...
}

// Async method to get a list of Amazon S3 buckets.
private static async Task<ListBucketsResponse> ListBucketsAsync()
{
  ...
  var response = await s3Client.ListBucketsAsync();
  return response;
}
```

As shown in the preceding code snippet, the preferred scope for the `async` declaration is the `Main` function\. Setting this `async` scope ensures that all calls to AWS service clients are required to be asynchronous\. If you can't declare `Main` to be asynchronous for some reason, you can use the `async` keyword on functions other than `Main` and then call the API methods from there, as shown in the following example\. 

```
static void Main(string[] args)
{
  ...
  Task<ListBucketsResponse> response = ListBucketsAsync();
  Console.WriteLine($"Number of buckets: {response.Result.Buckets.Count}");
  ...
}

// Async method to get a list of Amazon S3 buckets.
private static async Task<ListBucketsResponse> ListBucketsAsync()
{
  ...
  var response = await s3Client.ListBucketsAsync();
  return response;
}
```

Notice the special `Task<>` syntax that's needed in `Main` when you use this pattern\. In addition, you must use the **`Result`** member of the response to get the data\.

You can see full examples of asynchronous calls to AWS service clients in the [Quick start](quick-start.md) section \([Simple cross\-platform app](quick-start-s3-1-cross.md) and [Simple Windows\-based app](quick-start-s3-1-winvs.md)\) and in [Working with AWS services](tutorials-examples.md)\.