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
