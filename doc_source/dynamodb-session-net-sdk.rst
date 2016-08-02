.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-dynamodb-session:

#############################################
Managing ASP.NET Session State with |DDBlong|
#############################################

ASP.NET applications often store session-state data in memory. However, this approach doesn't scale
well. After the application grows beyond a single web server, the session state must be shared
between servers. A common solution is to set up a dedicated session-state server with Microsoft SQL
Server. But this approach also has drawbacks: you must administer another machine, the session-state
server is a single point of failure, and the session-state server itself can become a performance
bottleneck.

|DDBlong|_, a NoSQL database store from |AWSlong| (AWS), provides an
effective solution for sharing session state across web servers without incurring any of these
drawbacks.

.. note:: Regardless of the solution you choose, be aware that |DDBlong| enforces limits on the size of an
   item. None of the records you store in |DDB| can exceed this limit. For more information, see
   :ddb-dg:`Limits in DynamoDB <Limits>` in the |DDB-dg|.

The |sdk-net| includes :file:`AWS.SessionProvider.dll`, which contains an ASP.NET session state
provider. It also includes the *AmazonDynamoDBSessionProviderSample* sample, which demonstrates how
to use |DDBlong| as a session state provider.

For more information about using Session State with ASP.NET applications, go to the `MSDN
documentation <http://msdn.microsoft.com/en-us/library/ms178581.aspx>`_.

.. _asdf:

Create the *ASP.NET_SessionState* Table
=======================================

When your application starts, it looks for an |DDBlong| table named, by default,
:code:`ASP.NET_SessionState`. We recommend you create this table before you run your application for
the first time.

**To create the ASP.NET_SessionState table**

1. Choose :guilabel:`Create Table`. The :guilabel:`Create Table` wizard opens.

2. In the :guilabel:`Table name` text box, enter :code:`ASP.NET_SessionState`.

3. In the :guilabel:`Primary key` field, enter :code:`SessionId` and set the type to :code:`String`.

4. When all your options are entered as you want them, choose :guilabel:`Create`.

The :functionname:`ASP.NET_SessionState` table is ready for use when its status changes from
:code:`CREATING` to :code:`ACTIVE`.

.. note:: If you decide not to create the table beforehand, the session state provider will create the table
   during its initialization. See the :file:`web.config` options below for a list of attributes
   that act as configuration parameters for the session state table. If the provider creates the
   table, it will use these parameters.


.. _net-dg-ddb-config-sess-provider:

Configure the Session State Provider
====================================

**To configure an ASP.NET application to use DynamoDB as the session state server**

1. Add references to both :file:`AWSSDK.dll` and :file:`AWS.SessionProvider.dll` to your Visual Studio
   ASP.NET project. These assemblies are available by installing the :ref:`AWS SDK for .NET
   <net-dg-install-net-sdk>`. You can also install them by using :ref:`NuGet <net-dg-nuget>`.

   In earlier versions of the SDK, the functionality for the session state provider was contained
   in :file:`AWS.Extension.dll`. To improve usability, the functionality was moved to
   :file:`AWS.SessionProvider.dll`. For more information, see the blog post 
   :aws-blogs-net:`AWS.Extension Renaming <Tx27RWMCNAVWZN9/AWS-Extensions-renaming>`.

2. Edit your application's *Web.config* file. In the :code:`system.web` element, replace the existing
   :code:`sessionState` element with the following XML fragment:

   .. code-block:: xml

         <sessionState timeout="20" 
           mode="Custom" 
           customProvider="DynamoDBSessionStoreProvider"> 
             <providers> 
               <add 
                 name="DynamoDBSessionStoreProvider" 
                 type="Amazon.SessionProvider.DynamoDBSessionStateStore" 
                 AWSProfileName="{profile_name}"
                 Region="us-west-2" />
          </providers>
        </sessionState>

   The profile represents the AWS credentials used to communicate with |DDB| to store and retrieve
   the session state. If you are using the |sdk-net| and are specifying a profile in the
   :code:`appSettings` section of your application's :file:`Web.config` file, you do not need to
   specify a profile in the :code:`providers` section; the AWS .NET client code will discover it at
   run time. For more information, see :ref:`net-dg-config`.

   If the web server is running on an |EC2| instance that is configured to use IAM roles for EC2
   instances, then you do not need to specify any credentials in the :file:`web.config` file. In
   this case, the AWS .NET client will use the IAM roles' credentials. For more information, see
   :ref:`net-dg-roles` and :ref:`net-dg-ddb-sess-security`.

.. _net-dg-dd-config-opts:

Web.config Options
------------------

You can use the following configuration attributes in the :code:`providers` section of your
:file:`web.config` file:

*AWSAccessKey*
    Access key ID to use. This can be set either in the :code:`providers` or :code:`appSettings`
    section. We recommend not using this setting. Instead, specify credentials by using
    :code:`AWSProfileName` to specify a profile.

*AWSSecretKey*
    Secret key to use. This can be set either in the :code:`providers` or :code:`appSettings`
    section. We recommend not using this setting. Instead, specify credentials by using
    :code:`AWSProfileName` to specify a profile.

*AWSProfileName*
    The profile name associated with the credentials you want to use. For more information, see
    :ref:`net-dg-config`.

*Region*
    Required :code:`string` attribute. The AWS region in which to use |DDBlong|. For a list of AWS
    regions, see :rande:`Regions and Endpoints: DynamoDB <ddb>`.

*Application*
    Optional :code:`string` attribute. The value of the :code:`Application` attribute is used to
    partition the session data in the table so that the table can be used for more than one
    application.

*Table*
    Optional :code:`string` attribute. The name of the table used to store session data. The default
    is :code:`ASP.NET_SessionState`.

*ReadCapacityUnits*
    Optional :code:`int` attribute. The read capacity units to use if the provider creates the
    table. The default is 10.

*WriteCapacityUnits*
    Optional :code:`int` attribute. The write capacity units to use if the provider creates the
    table. The default is 5.

*CreateIfNotExist*
    Optional :code:`boolean` attribute. The :code:`CreateIfNotExist` attribute controls whether the
    provider will auto-create the table if it doesn't exist. The default is true. If this flag is
    set to false and the table doesn't exist, an exception will be thrown.



.. _net-dg-ddb-sess-security:

Security Considerations
=======================

After the |DDB| table is created and the application is configured, sessions can be used as with any
other session provider.

As a security best practice, we recommend you run your applications with the credentials of an |IAM|_ 
user. You can use either the :console:`AWS Management Console <iam>` or the 
:tvs-ug:`AWS Toolkit for Visual Studio` to create IAM users and define access policies.

The session state provider needs to be able to call the :ddb-dg:`DeleteItem <DeleteItem>`,
:ddb-dg:`DescribeTable <DescribeTable>`, :ddb-dg:`GetItem <GetItem>`, :ddb-dg:`PutItem <PutItem>`, 
and :ddb-dg:`UpdateItem <UpdateItem>` operations for the table that stores
the session data. The sample policy below can be used to restrict the IAM user to only the
operations needed by the provider for an instance of |DDB| running in |region_api_default|:

.. code-block:: json

    { "Version" : "2012-10-17", 
      "Statement" : [ 
        { 
          "Sid" : "1", 
          "Effect" : "Allow", 
          "Action" : [ 
            "dynamodb:DeleteItem", 
            "dynamodb:DescribeTable", 
            "dynamodb:GetItem", 
            "dynamodb:PutItem", 
            "dynamodb:UpdateItem" ], 
          "Resource" : "arn:aws:dynamodb:us-west-2{<YOUR-AWS-ACCOUNT-ID>}:table/ASP.NET_SessionState"
        }
      ]
    }



