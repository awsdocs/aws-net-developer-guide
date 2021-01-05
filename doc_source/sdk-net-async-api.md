--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# AWS asynchronous APIs for \.NET<a name="sdk-net-async-api"></a>

## Asynchronous API for \.NET Framework 4\.5, Windows Store, and Windows Phone 8<a name="async-api-45"></a>

The AWS SDK for \.NET uses the new task\-based asynchronous pattern for \.NET Framework version 4\.5, Windows Store, and Windows Phone 8\. You can use the `async` and `await` keywords to perform and manage asynchronous operations for all AWS products without blocking\.

To learn more about the task\-based asynchronous pattern, see [Task\-based Asynchronous Pattern \(TAP\)](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap) on docs\.microsoft\.com\. To see how TAP is used in the AWS SDK for \.NET, for both \.NET Framework 4\.5 projects and \.NET Core projects, see the [latest version of the documentation](../../latest/developer-guide/sdk-net-async-api.html)\.

## Asynchronous API for \.NET Framework 3\.5<a name="async-api-35"></a>

The AWS SDK for \.NET supports asynchronous \(async\) versions of most of the method calls exposed by the \.NET client classes\. The async methods enable you to call AWS services without having your code block on the response from the service\. For example, you can make a request to write data to Amazon S3 or DynamoDB and then have your code continue to do other work while AWS processes the requests\.

### Syntax of Async Request Methods<a name="sdk-net-async-request-syntax"></a>

There are two phases to making an asynchronous request to an AWS service\. The first is to call the `Begin` method for the request\. This method initiates the asynchronous operation\. The corresponding `End` method retrieves the response from the service and also provides an opportunity to handle exceptions that might have occurred during the operation\.

**Note**  
Calling the `End` method is not required\. Assuming no errors are encountered, the asynchronous operation will complete whether or not you call `End`\.

#### Begin Method Syntax<a name="sdk-net-async-begin-request"></a>

In addition to taking a request object parameter, such as [PutItemRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/TPutItemRequest.html), the async `Begin` methods take two additional parameters: a callback function and a state object\. Instead of returning a [service response object](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TWebServiceResponse.html), the `Begin` methods return a result of type `IAsyncResult`\. For the definition of this type, go to the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)\.

 *Synchronous Method* 

```
PutItemResponse PutItem(
  PutItemRequest putItemRequest
)
```

 *Asynchronous Method* 

```
IAsyncResult BeginPutItem(
  GetSessionTokenRequest getSessionTokenRequest, {AsyncCallback callback}, {Object state}
)
```

 *AsyncCallback Callback* 

The callback function is called when the asynchronous operation is complete\. When the function is called, it receives a single parameter of type [IAsyncResult](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncresult)\. The callback function has the following signature\.

```
void Callback(IAsyncResult asyncResult)
```

 *Object State* 

The third parameter, `state`, is a user\-defined object that is made available to the callback function as the `AsyncState` property of the `asyncResult` parameter, that is, `asyncResult.AsyncState`\.

 *Calling Patterns* 
+ Passing a callback function and a state object\.
+ Passing a callback function, but passing null for the state object\.
+ Passing null for both the callback function and the state object\.

This topic provides an example of each of these patterns\.

##### Using IAsyncResult\.AsyncWaitHandle<a name="sdk-net-async-waithandle"></a>

In some circumstances, the code that calls the `Begin` method might need to enable another method that it calls to wait on the completion of the asynchronous operation\. In these situations, it can pass the method the `WaitHandle` returned by the `IAsyncResult.AsyncWaitHandle` property of the `IAsyncResult` return value\. The method can then wait for the asynchronous operation to complete by calling `WaitOne` on this `WaitHandle`\.

### Examples<a name="sdk-net-async-examples"></a>

For the complete code example, see [Complete Example](#sdk-net-async-complete-code) below or view it on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/master/dotnet/example_code_legacy/S3/S3Async.cs)\.

All of the following snippets assume the following initialization code\.

```
        public static void TestPutObjectAsync(string bucket)
        {
            // Create a client
            AmazonS3Client client = new AmazonS3Client();

            PutObjectResponse response;
            IAsyncResult asyncResult;

            //
            // Create a PutObject request object using the supplied bucket name.
            //
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucket,
                Key = "Item0-Synchronous",
                ContentBody = "Put S3 object synchronously."
            };
```

#### No Callback Specified<a name="no-callback-specified"></a>

The following example code calls `BeginPutObject`, performs some work, and then calls `EndPutObject` to retrieve the service response\. The call to `EndPutObject` is enclosed in a `try` block to catch any exceptions that might have been thrown during the operation\.

```
            asyncResult = client.BeginPutObject(request, null, null);
            while (!asyncResult.IsCompleted)
            {
                //
                // Do some work here
                //
            }
            try
            {
                response = client.EndPutObject(asyncResult);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }
```

#### Simple Callback<a name="simple-callback"></a>

This example assumes the following callback function has been defined\.

```
        public static void SimpleCallback(IAsyncResult asyncResult)
        {
            Console.WriteLine("Finished PutObject operation with simple callback.");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("asyncResult.IsCompleted: {0}\n\n", asyncResult.IsCompleted.ToString());
        }
```

The following line of code calls `BeginPutObject` and specifies the above callback function\. When the `PutObject` operation is complete, the callback function is called\. The call to `BeginPutObject` specifies `null` for the `state` parameter because the simple callback function does not access the `AsyncState` property of the `asyncResult` parameter\. Neither the calling code or the callback function call `EndPutObject`\. Therefore, the service response is effectively discarded and any exceptions that occur during the operation are ignored\.

```
            asyncResult = client.BeginPutObject(request, SimpleCallback, null);
```

#### Callback with Client<a name="callback-with-client"></a>

This example assumes the following callback function has been defined\.

```
        public static void CallbackWithClient(IAsyncResult asyncResult)
        {
            try
            {
                AmazonS3Client s3Client = (AmazonS3Client)asyncResult.AsyncState;
                PutObjectResponse response = s3Client.EndPutObject(asyncResult);
                Console.WriteLine("Finished PutObject operation with client callback. Service Version: {0}", s3Client.Config.ServiceVersion);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Service Response:");
                Console.WriteLine("-----------------");
                Console.WriteLine("Request ID: {0}\n\n", response.ResponseMetadata.RequestId);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }
        }
```

The following line of code calls `BeginPutObject` and specifies the preceding callback function\. When the `PutObject` operation is complete, the callback function is called\. In this example, the call to `BeginPutObject` specifies the Amazon S3 client object for the `state` parameter\. The callback function uses the client to call the `EndPutObject` method to retrieve the server response\. Because any exceptions that occurred during the operation will be received when the callback calls `EndPutObject`, this call is placed within a `try` block\.

```
            asyncResult = client.BeginPutObject(request_client, CallbackWithClient, client);
```

#### Callback with State Object<a name="callback-with-state-object"></a>

This example assumes the following class and callback function have been defined\.

```
    class ClientState
    {
        public AmazonS3Client Client { get; set; }
        public DateTime Start { get; set; }
    }
```

```
        public static void CallbackWithState(IAsyncResult asyncResult)
        {
            try
            {
                ClientState state = asyncResult.AsyncState as ClientState;
                AmazonS3Client s3Client = (AmazonS3Client)state.Client;
                PutObjectResponse response = state.Client.EndPutObject(asyncResult);
                Console.WriteLine("Finished PutObject operation with state callback that started at {0}",
                    state.Start.ToString());
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Service Response:");
                Console.WriteLine("-----------------");
                Console.WriteLine("Request ID: {0}\n\n", response.ResponseMetadata.RequestId);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }
        }
```

The following line of code calls `BeginPutObject` and specifies the above callback function\. When the `PutObject` operation is complete, the callback function is called\. In this example, the call to `BeginPutObject` specifies, for the `state` parameter, an instance of the `ClientState` class defined previously\. This class embeds the Amazon S3 client as well as the time at which `BeginPutObject` is called\. The callback function uses the Amazon S3 client object to call the `EndPutObject` method to retrieve the server response\. The callback also extracts the start time for the operation and uses it to print the time it took for the asynchronous operation to complete\.

As in the previous examples, because exceptions that occur during the operation are received when `EndPutObject` is called, this call is placed within a `try` block\.

```
            asyncResult = client.BeginPutObject(request_state, CallbackWithState,
               new ClientState { Client = client, Start = DateTime.Now });
```

### Complete Example<a name="sdk-net-async-complete-code"></a>

The following code example demonstrates the patterns you can use when calling the asynchronous request methods\.

```
using System;
using System.Threading;

using Amazon.S3;
using Amazon.S3.Model;

namespace async_aws_net
{
    class ClientState
    {
        public AmazonS3Client Client { get; set; }
        public DateTime Start { get; set; }
    }

    class Program
    {
        //
        // Function Main().
        // Parse the command line and call the worker function.
        //
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("You must supply the name of an existing Amazon S3 bucket.");
                return;
            }

            TestPutObjectAsync(args[0]);
        }

        //
        // Function SimpleCallback().
        // A very simple callback function.
        //
        public static void SimpleCallback(IAsyncResult asyncResult)
        {
            Console.WriteLine("Finished PutObject operation with simple callback.");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("asyncResult.IsCompleted: {0}\n\n", asyncResult.IsCompleted.ToString());
        }

        //
        // Function CallbackWithClient().
        // A callback function that provides access to a given S3 client.
        //
        public static void CallbackWithClient(IAsyncResult asyncResult)
        {
            try
            {
                AmazonS3Client s3Client = (AmazonS3Client)asyncResult.AsyncState;
                PutObjectResponse response = s3Client.EndPutObject(asyncResult);
                Console.WriteLine("Finished PutObject operation with client callback. Service Version: {0}", s3Client.Config.ServiceVersion);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Service Response:");
                Console.WriteLine("-----------------");
                Console.WriteLine("Request ID: {0}\n\n", response.ResponseMetadata.RequestId);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }
        }

        //
        // Function CallbackWithState().
        // A callback function that provides access to a given S3 client as well as state information.
        //
        public static void CallbackWithState(IAsyncResult asyncResult)
        {
            try
            {
                ClientState state = asyncResult.AsyncState as ClientState;
                AmazonS3Client s3Client = (AmazonS3Client)state.Client;
                PutObjectResponse response = state.Client.EndPutObject(asyncResult);
                Console.WriteLine("Finished PutObject operation with state callback that started at {0}",
                    state.Start.ToString());
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Service Response:");
                Console.WriteLine("-----------------");
                Console.WriteLine("Request ID: {0}\n\n", response.ResponseMetadata.RequestId);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }
        }

        //
        // Function TestPutObjectAsync().
        // Test synchronous and asynchronous variations of PutObject().
        //
        public static void TestPutObjectAsync(string bucket)
        {
            // Create a client
            AmazonS3Client client = new AmazonS3Client();

            PutObjectResponse response;
            IAsyncResult asyncResult;

            //
            // Create a PutObject request object using the supplied bucket name.
            //
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucket,
                Key = "Item0-Synchronous",
                ContentBody = "Put S3 object synchronously."
            };

            //
            // Perform a synchronous PutObject operation.
            //
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Performing synchronous PutObject operation for {0}.", request.Key);
            response = client.PutObject(request);
            Console.WriteLine("Finished PutObject operation for {0}.", request.Key);
            Console.WriteLine("Service Response:");
            Console.WriteLine("-----------------");
            Console.WriteLine("Request ID: {0}", response.ResponseMetadata.RequestId);
            Console.Write("\n");

            //
            // Perform an async PutObject operation and wait for the response.
            //
            // (Re-use the existing PutObject request object since it isn't being used for another async request.)
            //
            request.Key = "Item1-Async-wait";
            request.ContentBody = "Put S3 object asynchronously; wait for response.";
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Performing async PutObject operation and waiting for response (Key: {0}).", request.Key);

            asyncResult = client.BeginPutObject(request, null, null);
            while (!asyncResult.IsCompleted)
            {
                //
                // Do some work here
                //
            }
            try
            {
                response = client.EndPutObject(asyncResult);
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine("Caught exception calling EndPutObject:");
                Console.WriteLine(s3Exception);
            }

            Console.WriteLine("Finished Async PutObject operation for {0}.", request.Key);
            Console.WriteLine("Service Response:");
            Console.WriteLine("-----------------");
            Console.WriteLine("Request ID: {0}\n", response.ResponseMetadata.RequestId);

            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Performing the following async PutObject operations:");
            Console.WriteLine("\"simple callback\", \"callback with client\", and \"callback with state\"...\n");

            //
            // Perform an async PutObject operation with a simple callback.
            //
            // (Re-use the existing PutObject request object since it isn't being used for another async request.)
            //
            request.Key = "Item2-Async-simple";
            request.ContentBody = "Put S3 object asynchronously; use simple callback.";

            Console.WriteLine("PutObject with simple callback (Key: {0}).", request.Key);
            asyncResult = client.BeginPutObject(request, SimpleCallback, null);

            //
            // Perform an async PutObject operation with a client callback.
            //
            // Create a PutObject request object for this call using the supplied bucket name.
            //
            PutObjectRequest request_client = new PutObjectRequest
            {
                BucketName = bucket,
                Key = "Item3-Async-client",
                ContentBody = "Put S3 object asynchronously; use callback with client."
            };

            Console.WriteLine("PutObject with client callback (Key: {0}).", request_client.Key);
            asyncResult = client.BeginPutObject(request_client, CallbackWithClient, client);

            //
            // Perform an async PutObject operation with a state callback.
            //
            // Create a PutObject request object for this call using the supplied bucket name.
            //
            PutObjectRequest request_state = new PutObjectRequest
            {
                BucketName = bucket,
                Key = "Item3-Async-state",
                ContentBody = "Put S3 object asynchronously; use callback with state."
            };

            Console.WriteLine("PutObject with state callback (Key: {0}).\n", request_state.Key);
            asyncResult = client.BeginPutObject(request_state, CallbackWithState,
               new ClientState { Client = client, Start = DateTime.Now });

            //
            // Finished with async calls. Wait a bit for them to finish.
            //
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}
```

You can also view it on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/master/dotnet/example_code_legacy/S3/S3Async.cs)\.

### See Also<a name="sdk-net-async-see-also"></a>
+  [Setting up the AWS SDK for \.NET](net-dg-setup.md) 
+  [Programming with the AWS SDK for \.NET](net-dg-programming-techniques.md) 