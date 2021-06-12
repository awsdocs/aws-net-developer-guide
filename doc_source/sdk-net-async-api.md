--------

End of support announcement: [http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/](http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

This documentation is for version 2\.0 of the AWS SDK for \.NET\.** For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/) of the AWS SDK for \.NET developer guide instead\.**

--------

# Amazon Web Services Asynchronous APIs for \.NET<a name="sdk-net-async-api"></a>

## Version 2 content \(see announcement above\)<a name="w3aac11b9b3b1"></a>

**Topics**
+ [Asynchronous API for \.NET 4\.5, Windows Store, and Windows Phone 8](#async-api-45)
+ [Asynchronous API for \.NET 3\.5](#async-api-35)

### Asynchronous API for \.NET 4\.5, Windows Store, and Windows Phone 8<a name="async-api-45"></a>

The AWS SDK for \.NET uses the new task\-based asynchronous pattern for \.NET 4\.5, Windows Store, and Windows Phone 8\. You can use the `async` and `await` keywords to perform and manage asynchronous operations for all AWS products without blocking\.

To learn more about the task\-based asynchronous pattern, see [Task\-based Asynchronous Pattern \(TAP\)](http://msdn.microsoft.com/en-us/library/hh873175(v=vs.110).aspx) on MSDN\.

### Asynchronous API for \.NET 3\.5<a name="async-api-35"></a>

The AWS SDK for \.NET supports asynchronous \(async\) versions of most of the method calls exposed by the \.NET client classes\. The async methods enable you to call AWS services without having your code block on the response from the service\. For example, you could make a request to write data to Amazon S3 or DynamoDB and then have your code continue to do other work while AWS processes the requests\.

#### Syntax of Async Request Methods<a name="sdk-net-async-request-syntax"></a>

There are two phases to making an asynchronous request to an AWS service\. The first is to call the `Begin` method for the request\. This method initiates the asynchronous operation\. Then, after some period of time, you would call the corresponding `End` method\. This method retrieves the response from the service and also provides an opportunity to handle exceptions that might have occurred during the operation\.

**Note**  
It is not required that you call the `End` method\. Assuming that no errors are encountered, the asynchronous operation will complete whether or not you call `End`\.

##### Begin Method Syntax<a name="sdk-net-async-begin-request"></a>

In addition to taking a request object parameter, such as [PutItemRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TDynamoDBv2PutItemRequestNET35.html), the async `Begin` methods take two additional parameters: a callback function, and a state object\. Instead of returning a [service response object](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRuntimeWebServiceResponseNET35.html), the `Begin` methods return a result of type `IAsyncResult`\. For the definition of this type, go to the [MSDN documentation](http://msdn.microsoft.com/en-us/library/bkbsbb9x.aspx)\.

##### Synchronous Method<a name="synchronous-method"></a>

```
PutItemResponse PutItem(
  PutItemRequest putItemRequest
)
```

##### Asynchronous Method<a name="asynchronous-method"></a>

```
IAsyncResult BeginPutItem( GetSessionTokenRequest getSessionTokenRequest, {AsyncCallback callback}, {Object state}
)
```

##### AsyncCallback callback<a name="asynccallback-callback"></a>

The callback function is called when the asynchronous operation completes\. When the function is called, it receives a single parameter of type [IAsyncResult](http://msdn.microsoft.com/en-us/library/bkbsbb9x.aspx)\. The callback function has the following signature\.

```
void Callback(IAsyncResult asyncResult)
```

##### Object state<a name="object-state"></a>

The third parameter, `state`, is a user\-defined object that is made available to the callback function as the `AsyncState` property of the `asyncResult` parameter, that is, `asyncResult.AsyncState`\.

#### Calling Patterns<a name="calling-patterns"></a>
+ Passing a callback function and a state object\.
+ Passing a callback function, but passing null for the state object\.
+ Passing null for both the callback function and the state object\.

This topic provides an example of each of these patterns\.

##### Examples<a name="examples"></a>

All of the following examples assume the following initialization code\.

```
public static void TestPutObjectAsync() {
  // Create a client
  AmazonS3Client client = new AmazonS3Client();
  PutObjectResponse response;
  IAsyncResult asyncResult;

  //
  // Create a PutObject request
  //
  // You will need to use your own bucket name below in order
  // to run this sample code.
  //
  PutObjectRequest request = new PutObjectRequest { BucketName = "{PUT YOUR OWN EXISTING BUCKET NAME HERE}",
    Key = "Item",
    ContentBody = "This is sample content..."
  };

  //
  // additional example code
  //
}
```

##### Using IAsyncResult\.AsyncWaitHandle<a name="sdk-net-async-waithandle"></a>

In some circumstances, the code that calls the `Begin` method might need to enable another method that it calls to wait on the completion of the asynchronous operation\. In these situations, it can pass the method the `WaitHandle` returned by the `IAsyncResult.AsyncWaitHandle` property of the `IAsyncResult` return value\. The method can then wait for the asynchronous operation to complete by calling `WaitOne` on this `WaitHandle`\.

##### No Callback Specified<a name="sdk-net-async-examples"></a>

The following example code calls `BeginPutObject`, performs some work, then calls `EndPutObject` to retrieve the service response\. The call to `EndPutObject` is enclosed in a `try` block to catch any exceptions that might have been thrown during the operation\.

```
asyncResult = client.BeginPutObject(request, null, null);
while ( ! asyncResult.IsCompleted ) {
  //
  // Do some work here
  //
}
try {
  response = client.EndPutObject(asyncResult);
}
catch (AmazonS3Exception s3Exception) {
  //
  // Code to process exception
  //
}
```

##### Simple Callback<a name="simple-callback"></a>

This example assumes that the following callback function has been defined\.

```
public static void SimpleCallback(IAsyncResult asyncResult)
{
  Console.WriteLine("Finished PutObject operation with simple callback");
}
```

The following line of code calls `BeginPutObject` and specifies the above callback function\. When the `PutObject` operation completes, the callback function is called\. The call to `BeginPutObject` specifies `null` for the `state` parameter because the simple callback function does not access the `AsyncState` property of the `asyncResult` parameter\. Neither the calling code or the callback function call `EndPutObject`\. Therefore, the service response is effectively discarded and any exceptions that occur during the operation are ignored\.

```
asyncResult = client.BeginPutObject(request, SimpleCallback, null);
```

##### Callback with Client<a name="callback-with-client"></a>

This example assumes that the following callback function has been defined\.

```
public static void CallbackWithClient(IAsyncResult asyncResult)
{
  try {
    AmazonS3Client s3Client = (AmazonS3Client) asyncResult.AsyncState;
    PutObjectResponse response = s3Client.EndPutObject(asyncResult);
    Console.WriteLine("Finished PutObject operation with client callback");
  }
  catch (AmazonS3Exception s3Exception) {
    //
    // Code to process exception
    //
  }
}
```

The following line of code calls `BeginPutObject` and specifies the preceding callback function\. When the `PutObject` operation completes, the callback function is called\. In this example, the call to `BeginPutObject` specifies the Amazon S3 client object for the `state` parameter\. The callback function uses the client to call the `EndPutObject` method to retrieve the server response\. Because any exceptions that occurred during the operation will be received when the callback calls `EndPutObject`, this call is placed within a `try` block\.

```
asyncResult = client.BeginPutObject(request, CallbackWithClient, client);
```

##### Callback with State Object<a name="callback-with-state-object"></a>

This example assumes that the following class and callback function have been defined\.

```
class ClientState
{
  AmazonS3Client client;
  DateTime startTime;

  public AmazonS3Client Client
  {
    get { return client; }
    set { client = value; }
  }

  public DateTime Start
  {
    get { return startTime; }
    set { startTime = value; }
  }
}
```

```
public static void CallbackWithState(IAsyncResult asyncResult)
{
  try {
    ClientState state = asyncResult.AsyncState as ClientState;
    AmazonS3Client s3Client = (AmazonS3Client)state.Client;
    PutObjectResponse response = state.Client.EndPutObject(asyncResult);
    Console.WriteLine("Finished PutObject. Elapsed time: {0}",
      (DateTime.Now - state.Start).ToString());
  }
  catch (AmazonS3Exception s3Exception) {
    //
    // Code to process exception
    //
  }
}
```

The following line of code calls `BeginPutObject` and specifies the above callback function\. When the `PutObject` operation completes, the callback function is called\. In this example, the call to `BeginPutObject` specifies, for the `state` parameter, an instance of the `ClientState` class defined previously\. This class embeds the Amazon S3 client as well as the time at which `BeginPutObject` is called\. The callback function uses the Amazon S3 client object to call the `EndPutObject` method to retrieve the server response\. The callback also extracts the start time for the operation and uses it to print the time it took for the asynchronous operation to complete\.

As in the previous examples, because exceptions that occur during the operation are received when `EndPutObject` is called, this call is placed within a `try` block\.

```
asyncResult = client.BeginPutObject(
  request, CallbackWithState, new ClientState { Client = client, Start = DateTime.Now } );
```

#### Complete Sample<a name="sdk-net-async-complete-code"></a>

The following code sample demonstrates the various patterns that you can use when calling the asynchronous request methods\.

```
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace async_aws_net
{
   class ClientState
   {
      AmazonS3Client client;
      DateTime startTime;

      public AmazonS3Client Client
      {
         get { return client; }
         set { client = value; }
      }

      public DateTime Start
      {
         get { return startTime; }
         set { startTime = value; }
      }
   }

   class Program
   {
      public static void Main(string[] args)
      {
         TestPutObjectAsync();
      }

      public static void SimpleCallback(IAsyncResult asyncResult)
      {
         Console.WriteLine("Finished PutObject operation with simple callback");
         Console.Write("\n\n");
      }

      public static void CallbackWithClient(IAsyncResult asyncResult)
      {
         try {
            AmazonS3Client s3Client = (AmazonS3Client) asyncResult.AsyncState;
            PutObjectResponse response = s3Client.EndPutObject(asyncResult);
            Console.WriteLine("Finished PutObject operation with client callback");
            Console.WriteLine("Service Response:");
            Console.WriteLine("-----------------");
            Console.WriteLine(response);
            Console.Write("\n\n");
         }
         catch (AmazonS3Exception s3Exception) {
            //
            // Code to process exception
            //
         }
      }

      public static void CallbackWithState(IAsyncResult asyncResult)
      {
         try {
            ClientState state = asyncResult.AsyncState as ClientState;
            AmazonS3Client s3Client = (AmazonS3Client)state.Client;
            PutObjectResponse response = state.Client.EndPutObject(asyncResult);
            Console.WriteLine(
               "Finished PutObject operation with state callback that started at {0}",
               (DateTime.Now - state.Start).ToString() + state.Start);
            Console.WriteLine("Service Response:");
            Console.WriteLine("-----------------");
            Console.WriteLine(response);
            Console.Write("\n\n");
         }
         catch (AmazonS3Exception s3Exception) {
            //
            // Code to process exception
            //
         }
      }

      public static void TestPutObjectAsync()
      {
         // Create a client
         AmazonS3Client client = new AmazonS3Client();

         PutObjectResponse response;
         IAsyncResult asyncResult;

         //
         // Create a PutObject request
         //
         // You will need to change the BucketName below in order to run this
         // sample code.
         //
         PutObjectRequest request = new PutObjectRequest
         {
           BucketName = "PUT-YOUR-OWN-EXISTING-BUCKET-NAME-HERE",
           Key = "Item",
           ContentBody = "This is sample content..."
         };

         response = client.PutObject(request);
         Console.WriteLine("Finished PutObject operation for {0}.", request.Key);
         Console.WriteLine("Service Response:");
         Console.WriteLine("-----------------");
         Console.WriteLine("{0}", response);
         Console.Write("\n\n");

         request.Key = "Item1";
         asyncResult = client.BeginPutObject(request, null, null);
         while ( ! asyncResult.IsCompleted ) {
           //
           // Do some work here
           //
         }
         try {
           response = client.EndPutObject(asyncResult);
         }
         catch (AmazonS3Exception s3Exception) {
           //
           // Code to process exception
           //
         }

         Console.WriteLine("Finished Async PutObject operation for {0}.", request.Key );
         Console.WriteLine("Service Response:");
         Console.WriteLine("-----------------");
         Console.WriteLine(response);
         Console.Write("\n\n");

         request.Key = "Item2";
         asyncResult = client.BeginPutObject(request, SimpleCallback, null);

         request.Key = "Item3";
         asyncResult = client.BeginPutObject(request, CallbackWithClient, client);

         request.Key = "Item4";
         asyncResult = client.BeginPutObject(request, CallbackWithState,
            new ClientState { Client = client, Start = DateTime.Now } );

         Thread.Sleep( TimeSpan.FromSeconds(5) );
      }
   }
}
```

#### See Also<a name="sdk-net-async-see-also"></a>
+  [Getting Started with the AWS SDK for \.NET](net-dg-setup.md) 
+  [Programming with the AWS SDK for \.NET](net-dg-programming-techniques.md) 