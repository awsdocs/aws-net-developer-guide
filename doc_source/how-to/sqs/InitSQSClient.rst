.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _init-sqs-client:

######################
Create an |SQS| Client
######################

You will need an |SQS| client in order to create and use an |SQS| queue. Before configuring your
client, you should create an :file:`App.Config` file to specify your AWS credentials.

You specify your credentials by referencing the appropriate profile in the appSettings section of
the file. The following example specifies a profile named {my_profile}. For more information on
credentials and profiles, see :ref:`net-dg-config`.

.. code-block:: xml

    <?xml version="1.0"?> 
      <configuration> 
        <startup> 
          <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/> 
        </startup> 
        <configSections> 
          <section name="aws" type="Amazon.AWSSection, AWSSDK"/> 
        </configSections> <aws profileName="{my_profile}"/>
      </configuration>

After you create this file, you are ready to create and initialize your |SQS| client.

**To create and initialize an Amazon SQS client**

1. Create and initialize an :sdk-net-api-v2:`AmazonSQSConfig <TSQSSQSConfigNET45>` instance, and set the
   :sdk-net-api-v2:`ServiceURL <PRuntimeClientConfigServiceURLNET45>` property with the protocol and service
   endpoint, as follows:

   .. code-block:: csharp

       AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();
           
       amazonSQSConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";

   .. code-block:: csharp

       AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();
           
       amazonSQSConfig.ServiceURL = "http://sqs.cn-north-1.amazonaws.com";

   The |sdk-net| uses |useast1-name| as the default region if you do not specify a region in your
   code. However, the |console| uses |uswest2-name| as its default. Therefore, when using the
   |console| in conjunction with your development, be sure to specify the same region in both your
   code and the console.

   The |sdk-net| uses |cnnorth1-name| as the default region if you do not specify a region in your
   code. Therefore, when using the |console| in conjunction with your development, be sure to
   specify that region in both your code and the console.

   Go to |regions-and-endpoints|_ for the current list of regions and corresponding endpoints
   for each of the services offered by AWS.

2. Use the :code:`AmazonSQSConfig` instance to create and initialize an :sdk-net-api-v2:`AmazonSQSClient
   <TSQSSQSNET45>` instance, as follows:

   .. code-block:: csharp

       amazonSQSClient = new AmazonSQSClient(amazonSQSConfig);

You can now use the client to create an |SQS| queue. For information about creating a queue, see
:ref:`create-sqs-queue`.


