.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _retries-timeouts:

####################
Retries and Timeouts
####################

The |sdk-net| allows you to configure the number of retries and the timeout values for HTTP requests
to AWS services. If the default values for retries and timeouts are not appropriate for your
application, you can adjust them for your specific requirements, but it is important to understand
how doing so will affect the behavior of your application.

To determine which values to use for retries and timeouts, consider the following:

* How should the |sdk-net| and your application respond when network connectivity degrades or an AWS
  service is unreachable? Do you want the call to fail fast, or is it appropriate for the call to
  keep retrying on your behalf?

* Is your application a user-facing application or website that must be responsive, or is it a
  background processing job that has more tolerance for increased latencies?

* Is the application deployed on a reliable network with low latency, or it is deployed at a remote
  location with unreliable connectivity?


.. _retries:

Retries
=======

The |sdk-net| will retry requests that fail due to server-side throttling or dropped connections. You
can use the :code:`MaxErrorRetry` property of the :sdk-net-api:`ClientConfig <Runtime/TClientConfig>` class
to specify the number of retries at the service client level. the |sdk-net| will retry the operation
the specified number of times before failing and throwing an exception. By default, the
:code:`MaxErrorRetry` property is set to 4, except for the 
:sdk-net-api:`AmazonDynamoDBConfig <DynamoDBv2/TDynamoDBConfig>` class, which defaults to 10 
retries. When a retry occurs, it increases the latency of your request. You should configure your 
retries based on your application limits for total request latency and error rates.


.. _timeouts:

Timeouts
========

The |sdk-net| allows you to configure the request timeout and socket read/write timeout values at the
service client level. These values are specified in the :code:`Timeout` and the
:code:`ReadWriteTimeout` properties of the :sdk-net-api:`ClientConfig <Runtime/TClientConfig>` class,
respectively. These values are passed on as the :code:`Timeout` and :code:`ReadWriteTimeout`
properties of the `HttpWebRequest
<https://msdn.microsoft.com/en-us/library/System.Net.HttpWebRequest%28v=vs.110%29.aspx>`_ objects
created by the AWS service client object. By default, the :code:`Timeout` value is 100 seconds and
the :code:`ReadWriteTimeout` value is 300 seconds.

When your network has high latency, or conditions exist that cause an operation to be retried, using
long timeout values and a high number of retries can cause some SDK operations to seem unresponsive.

.. note:: The version of the |sdk-net| that targets the portable class library (PCL) uses the `HttpClient
   <http://msdn.microsoft.com/en-us/library/system.net.http.httpclient%28v=vs.110%29.aspx>`_ class
   instead of the :classname:`HttpWebRequest` class, and supports the `Timeout
   <https://msdn.microsoft.com/en-us/library/system.net.http.httpclient.timeout%28v=vs.110%29.aspx>`_
   property only.

The following are the exceptions to the default timeout values. These values are overridden when
you explicitly set the timeout values.

* :code:`Timeout` and :code:`ReadWriteTimeout` are set to the maximum values if the method being
  called uploads a stream, such as :sdk-net-api:`AmazonS3Client.PutObject() <S3/MS3PutObjectPutObjectRequest>`, 
  :sdk-net-api:`AmazonS3Client.UploadPart() <S3/MS3UploadPartUploadPartRequest>`, 
  :sdk-net-api:`AmazonGlacierClient.UploadArchive() <Glacier/MGlacierUploadArchiveUploadArchiveRequest>`, 
  and so on.

* The version of the |sdk-net| that targets the .NET Framework 4.5 sets :code:`Timeout` and
  :code:`ReadWriteTimeout` to the maximum values for all :sdk-net-api:`AmazonS3Client <S3/TS3S3Client>` and
  :sdk-net-api:`AmazonGlacierClient <Glacier/TGlacierClient>` objects.

* The version of the |sdk-net| that targets the portable class library (PCL) sets :code:`Timeout` to the
  maximum value for all :sdk-net-api:`AmazonS3Client <S3/TS3S3Client>` and 
  :sdk-net-api:`AmazonGlacierClient <Glacier/TGlacierGlacierClient>` objects.


.. _retries-timeouts-example:

Example
=======

The following example shows how to specify a maximum of 2 retries, a timeout of 10 seconds, and a
read/write timeout of 10 seconds for an :sdk-net-api:`AmazonS3Client <S3/TS3S3Client>` object.

.. code-block:: csharp

    var client =  new AmazonS3Client(
      new AmazonS3Config 
      {
        Timeout = TimeSpan.FromSeconds(10),            // Default value is 100 seconds
        ReadWriteTimeout = TimeSpan.FromSeconds(10),   // Default value is 300 seconds
        MaxErrorRetry = 2                              // Default value is 4 retries
      });
