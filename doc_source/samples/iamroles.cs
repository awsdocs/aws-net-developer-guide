using System;
using System.Collections.Specialized;
using System.IO;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace s3.amazon.com.docsamples.retrieveobject
{
  class S3Sample
  {
    public static void Main(string[] args)
    {
      ReadS3File("bucket-name", "s3-file-name", "output-file-name");

      Console.WriteLine("Press enter to continue");
      Console.ReadLine();
    }

    public static void ReadS3File(
      string bucketName, 
      string keyName, 
      string filename)
    {

      string responseBody = "";

      try
      {
        using (var s3Client = new AmazonS3Client())
        {
          Console.WriteLine("Retrieving (GET) an object");

          var request = new GetObjectRequest()
          {
            BucketName = bucketName,
            Key = keyName
          };

          using (var response = s3Client.GetObject(request))
          using (var responseStream = response.ResponseStream)
          using (var reader = new StreamReader(responseStream))
          {
            responseBody = reader.ReadToEnd();
          }
        }

        using (var s = new FileStream(filename, FileMode.Create))
        using (var writer = new StreamWriter(s))
        {
          writer.WriteLine(responseBody);
        }
      }
      catch (AmazonS3Exception s3Exception)
      {
        Console.WriteLine(s3Exception.Message, s3Exception.InnerException);
      }
    }
  }
}
