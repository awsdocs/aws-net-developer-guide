.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _init-sqs-client:

########################
Creating an |SQS| Client
########################

You need an |SQS| client in order to create and use an |SQS| queue. Before configuring your
client, you should create an :file:`App.Config` file to specify your AWS credentials.

You specify your credentials by referencing the appropriate profile in the :code:`appSettings` section
of the file.

The following example specifies a profile named :code:`my_profile`. For more information
about credentials and profiles, see :ref:`net-dg-config`.

.. code-block:: xml

    <?xml version="1.0"?>
    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws profileName="my_profile"/>
      <startup>
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
      </startup>
    </configuration>


After you create this file, you're ready to create and initialize your |SQS| client.

.. topic:: To create and initialize an |SQS| client

    #. Create and initialize an :sdk-net-api:`AmazonSQSConfig <SQS/TSQSSQSConfig>` instance, and then set the
       :code:`ServiceURL` property with the protocol and service endpoint, as follows.

       .. code-block:: csharp

           var sqsConfig = new AmazonSQSConfig();

           sqsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";


    #. Use the :classname:`AmazonSQSConfig` instance to create and initialize an
       :sdk-net-api:`AmazonSQSClient <SQS/TSQSSQSClient>` instance, as follows.

       .. code-block:: csharp

          var sqsClient = new AmazonSQSClient(sqsConfig);

    You can now use the client to create an |SQS| queue. For information about creating a queue, see
    :ref:`create-sqs-queue`.
