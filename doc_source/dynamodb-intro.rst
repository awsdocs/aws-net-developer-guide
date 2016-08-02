.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _dynamodb-intro:

########################################
|DDBlong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |DDBlong|, which is a fast NoSQL database service offered by AWS.

The following information introduces you to the |DDB| programming models and their APIs. There are
also links to additional |DDB| programming resources within the the |sdk-net|.

.. toctree::
    :titlesonly:
    :maxdepth: 1

    dynamodb-expressions
    dynamodb-json
    dynamodb-session-net-sdk

.. _dynamodb-intro-apis:

Programming Models
==================

The the SDK provides three different programming models for communicating with |DDB|. These
programming models include the *low-level* model, the *document* model, and the *object persistence*
model. The following information describes these models, how to use them, and when you might want to
use them.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _dynamodb-intro-apis-low-level:

Low-Level
---------

The low-level programming model wraps direct calls to the |DDB| service. You access this model
through the :sdk-net-api-v2:`Amazon.DynamoDBv2 <NDynamoDBv2NET45>` namespace.

Of the three models, the low-level model requires you to write the most code. For example, you must
convert .NET data types to their equivalents in |DDB|. However, this model gives you access to the
most features.

The following example shows how to use the low-level model to create a table in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-create-table-low-level.txt
   :language: csharp

In the preceding example, the table is created through the :code:`AmazonDynamoDBClient` class's
:code:`CreateTable` method. The :code:`CreateTable` method uses an instance of the
:code:`CreateTableRequest` class containing characteristics such as required item attribute names,
primary key definition, and throughput capacity. The :code:`CreateTable` method returns an instance
of the :code:`CreateTableResponse` class.

Before you begin modifying a table, you should make sure that the table is ready. The following
example shows how to use the low-level model to verify that a table in |DDB| is ready:

.. literalinclude:: how-to/dynamodb/dynamodb-check-table-low-level.txt
   :language: csharp

In the preceding example, the target table to check is referenced through the
:code:`AmazonDynamoDBClient` class's :code:`DescribeTable` method. Every 5 seconds, the code checks
the value of the table's :code:`TableStatus` property. When the status is set to :code:`ACTIVE`,
then the table is ready to be modified.

The following example shows how to use the low-level model to insert two items into a table in
|DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-low-level.txt
   :language: csharp

In the preceding example, each item is inserted through the :code:`AmazonDynamoDBClient` class's
:code:`PutItem` method, using an instance of the :code:`PutItemRequest` class. Each of the two
instances of the :code:`PutItemRequest` class takes the name of the table to be inserted into, along
with a series of item attribute values.

For more information and examples, see:

* :ddb-dg:`Working with Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetWorkingWithTables>`

* :ddb-dg:`Working with Items Using the AWS SDK for .NET Low-Level API <LowLevelDotNetItemCRUD>`

* :ddb-dg:`Querying Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetQuerying>`

* :ddb-dg:`Scanning Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetScanning>`

* :ddb-dg:`Working with Local Secondary Indexes Using the AWS SDK for .NET Low-Level API <LSILowLevelDotNet>`

* :ddb-dg:`Working with Global Secondary Indexes Using the AWS SDK for .NET Low-Level API <GSILowLevelDotNet>`

.. _dynamodb-intro-apis-document:

Document
--------

The document programming model provides an easier way to work with data in |DDB|. This model is
specifically intended for accessing tables and items in tables. You access this model through the
:sdk-net-api-v2:`Amazon.DynamoDBv2.DocumentModel <NDynamoDBv2DocumentModel>` namespace.

Of the three models, the document model is easier to code against |DDB| data compared to the
low-level programming model. For example, you don't have to convert as many .NET data types to their
equivalents in |DDB|. However, this model doesn't provide access to as many features as the
low-level programming model. For example, you can use this model to create, retrieve, update, and
delete items in tables. However, to create tables, you must use the low-level model. Finally, this
model requires you to write more code to store, load, and query .NET objects compared to the object
persistence model.

The following example shows how to use the document model to insert an item into a table in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-doc-model.txt
   :language: csharp

In the preceding example, the item is inserted into the table through the :code:`Table` class's
:code:`PutItem` method. The :code:`PutItem` method takes an instance of the :code:`Document` class;
the :code:`Document` class is simply a collection of initialized attributes. To determine the table
to insert the item into, the :code:`Table` class's :code:`LoadTable` method is called, specifying an
instance of the :code:`AmazonDynamoDBClient` class and the name of the target table in |DDB|.

The following example shows how to use the document model to get an item from a table in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-get-item-doc-model.txt
   :language: csharp

In the preceding example, the item is retrieved through the :code:`Table` class's :code:`GetItem`
method. To determine the item to get, in this example, the :code:`GetItem` method uses the
hash-and-range primary key of the target item. To determine the table to get the item from, the
:code:`Table` class's :code:`LoadTable` method uses an instance of the :code:`AmazonDynamoDBClient`
class and the name of the target table in |DDB|.

The preceding example implicitly converts the attribute values for :code:`Id`, :code:`Type`,
:code:`Name` to strings for the :code:`WriteLine` method. You can do explicit conversions by using
the various :code:`AsType` methods of the :code:`DynamoDBEntry` class. For example, you could
explicitly convert the attribute value for :code:`Id` from a :code:`Primitive` data type to an
integer through the :code:`AsInt` method:

.. code-block:: csharp

    int id = item["Id"].AsInt();

Or, you could simply perform an explicit cast here by using :code:`(int)`:

.. code-block:: csharp

    int id = (int)item["Id"];

For more information about data type conversions with |DDB|, see 
:ddb-dg-deep:`DynamoDB Data Types <DataModel.html#DataModel.DataTypes>` and 
:ddb-dg:`DynamoDBEntry <TDynamoDBv2DocumentModelDynamoDBEntry>`.

For more information and examples about the |DDB| document model, see 
:ddb-dg:`.NET: Document Model <DotNetSDKMidLevel>`.



.. _dynamodb-intro-apis-object-persistence:

Object Persistence
------------------

The object persistence programming model is specifically designed for storing, loading, and querying
.NET objects in |DDB|. You access this model through the :sdk-net-api-v2:`Amazon.DynamoDBv2.DataModel <NDynamoDBv2DataModel>` namespace.

Of the three models, the object persistence model is easiest to code against whenever you are
storing, loading, or querying |DDB| data. For example, you work with |DDB| data types directly.
However, this model provides access only to operations that store, load, and query .NET objects in
|DDB|. For example, you can use this model to create, retrieve, update and delete items in tables.
However, you must first create your tables using the low-level model, and then you can use this
model to map your .NET classes to the tables.

The following example shows how to define a .NET class that represents an item in a table in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-create-class-obj-model.txt
   :language: csharp

In the preceding example, the :code:`DynamoDBTable` attribute specifies the table name, while the
:code:`DynamoDBHashKey` and :code:`DynamoDBRangeKey` attributes model the table's hash-and-range
primary key.

The following example shows how to use an instance of this .NET class to insert an item into a table
in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-obj-model.txt
   :language: csharp

In the preceding example, the item is inserted through the :code:`DynamoDBContext` class's
:code:`Save` method, which takes an initialized instance of the .NET class that represents the item.
(The instance of the :code:`DynamoDBContext` class is initialized with an instance of the
:code:`AmazonDynamoDBClient` class.)

The following example shows how to use an instance of this .NET object to get an item from a table
in |DDB|:

.. literalinclude:: how-to/dynamodb/dynamodb-get-item-obj-model.txt
   :language: csharp

In the preceding example, the item is retrieved through the :code:`DynamoDBContext` class's
:code:`Load` method, which takes a partially-initialized instance of the .NET class that represents
the hash-and-range primary key of the item to be retrieved. (As before, the instance of the
:code:`DynamoDBContext` class is initialized with an instance of the :code:`AmazonDynamoDBClient`
class.)

For more information and examples, see :ddb-dg:`.NET: Object Persistence Model <DotNetSDKHighLevel>`.



.. _dynamodb-intro-more-info:

Additional Resources
====================

For additional information and examples of programming |DDB| with the the SDK, see:

* :aws-blogs-net:`DynamoDB APIs <Tx17SQHVEMW8MXC/DynamoDB-APIs>`

* :aws-blogs-net:`DynamoDB Series Kickoff <Tx2XQOCY08QMTKO/DynamoDB-Series-Kickoff>`

* :aws-blogs-net:`DynamoDB Series - Document Model <Tx2R0WG46GQI1JI/DynamoDB-Series-Document-Model>`

* :aws-blogs-net:`DynamoDB Series - Conversion Schemas <Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas>`

* :aws-blogs-net:`DynamoDB Series - Object Persistence Model <Tx20L86FLMBW51P/DynamoDB-Series-Object-Persistence-Model>`

* :aws-blogs-net:`DynamoDB Series - Expressions <TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions>`

* :ref:`dynamodb-expressions`

* :ref:`dynamodb-json`

* :ref:`net-dg-dynamodb-session`




