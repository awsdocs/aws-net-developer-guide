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
The SDK provides three programming models for communicating with |DDB|: the *low-level* model,
the *document* model, and the *object persistence* model. 

The following information introduces these models and their APIs, provides examples for how and when 
to use them, and gives you links to additional |DDB|programmibng resources in the |sdk-net|.

.. contents:: Topics
    :local:
    :depth: 1
    
.. toctree::
    :hidden:
    :maxdepth: 1

    dynamodb-expressions
    dynamodb-json
    dynamodb-session-net-sdk

.. _dynamodb-intro-apis-low-level:

Low-Level Model
===============

The low-level programming model wraps direct calls to the |DDB| service. You access this model
through the :sdk-net-api:`Amazon.DynamoDBv2 <DynamoDBv2/NDynamoDBv2>` namespace.

Of the three models, the low-level model requires you to write the most code. For example, you must
convert .NET data types to their equivalents in |DDB|. However, this model gives you access to the
most features.

The following examples show you how to use the low-level model to create a table, modify a table, 
and insert items into a table in |DDB|.

.. _dynamodb-intro-apis-low-level_create-table:

Creating a Table
~~~~~~~~~~~~~~~~

In the following example, you create a table by using the :code:`CreateTable` method of the
:code:`AmazonDynamoDBClient` class. The :code:`CreateTable` method uses an instance of the
:code:`CreateTableRequest` class that contains characteristics such as required item attribute names,
primary key definition, and throughput capacity. The :code:`CreateTable` method returns an instance
of the :code:`CreateTableResponse` class.

.. literalinclude:: how-to/dynamodb/dynamodb-create-table-low-level.txt
   :language: csharp


.. _dynamodb-intro-apis-low-level-verify-table:
   
Verifying That a Table is Ready to Modify
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Before you can change or modify a table, the table has to be ready for modification. 
The following example shows how to use the low-level model to verify that a table in |DDB| is ready. 
In this example, the target table to check is referenced through the :code:`DescribeTable`
method of the :code:`AmazonDynamoDBClient` class. Every five seconds, the code checks the value of
the table's :code:`TableStatus` property. When the status is set to :code:`ACTIVE`, the table is
ready to be modified.

.. literalinclude:: how-to/dynamodb/dynamodb-check-table-low-level.txt
   :language: csharp

   
.. _dynamodb-intro-apis-low-level_insert-item:
   
Inserting an Item into a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the following example, you use the low-level model to insert two items into a table in
|DDB|. Each item is inserted through the :code:`PutItem` method of the
:code:`AmazonDynamoDBClient` class, using an instance of the :code:`PutItemRequest` class. Each of
the two instances of the :code:`PutItemRequest` class takes the name of the table that the items will be 
inserted in, with a series of item attribute values.

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-low-level.txt
   :language: csharp


.. _dynamodb-intro-apis-document:

Document Model
==============

The document programming model provides an easier way to work with data in |DDB|. This model is
specifically intended for accessing tables and items in tables. You access this model through the
:sdk-net-api:`Amazon.DynamoDBv2.DocumentModel <DynamoDBv2/NDynamoDBv2DocumentModel>` namespace.

Compared to the low-level programming model, the document model is easier to code against |DDB|
data. For example, you don't have to convert as many .NET data types to their equivalents in |DDB|.
However, this model doesn't provide access to as many features as the low-level programming model.
For example, you can use this model to create, retrieve, update, and delete items in tables.
However, to create the tables, you must use the low-level model. Compared to the object persistence
model, this model requires you to write more code to store, load, and query .NET objects.

The following examples show you how to use the document model to insert items and get items 
in tables in |DDB|.


.. _dynamodb-intro-apis-document-insert-item:

Inserting an Item into a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the following example, an item is inserted into the table through the :code:`PutItem` method of
the :code:`Table` class. The :code:`PutItem` method takes an instance of the :code:`Document` class;
the :code:`Document` class is simply a collection of initialized attributes. To determine the table
to insert the item into, call the :code:`LoadTable` method of the :code:`Table` class,
specifying an instance of the :code:`AmazonDynamoDBClient` class and the name of the target table in
|DDB|.

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-doc-model.txt
   :language: csharp

.. _dynamodb-intro-apis-document-get-item:
   
Getting an Item from a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the following example, the item is retrieved through the :code:`GetItem` method of the
:code:`Table` class. To determine the item to get, the :code:`GetItem` method uses
the hash-and-range primary key of the target item. To determine the table to get the item from, the
:code:`LoadTable` method of the :code:`Table` class uses an instance of the
:code:`AmazonDynamoDBClient` class and the name of the target table in |DDB|.

.. literalinclude:: how-to/dynamodb/dynamodb-get-item-doc-model.txt
   :language: csharp

The preceding example implicitly converts the attribute values for :code:`Id`, :code:`Type`, and
:code:`Name` to strings for the :code:`WriteLine` method. You can do explicit conversions by using
the various :code:`AsType` methods of the :code:`DynamoDBEntry` class. For example, you could
explicitly convert the attribute value for :code:`Id` from a :code:`Primitive` data type to an
integer through the :code:`AsInt` method:

.. code-block:: csharp

    int id = item["Id"].AsInt();


Or, you could simply perform an explicit cast here by using :code:`(int)`:

.. code-block:: csharp

    int id = (int)item["Id"];


.. _dynamodb-intro-apis-object-persistence:

Object Persistence Model
========================

The object persistence programming model is specifically designed for storing, loading, and querying
.NET objects in |DDB|. You access this model through the 
:sdk-net-api:`Amazon.DynamoDBv2.DataModel <DynamoDBv2/NDynamoDBv2DataModel>` namespace.

Of the three models, the object persistence model is the easiest to code against whenever you are
storing, loading, or querying |DDB| data. For example, you work with |DDB| data types directly.
However, this model provides access only to operations that store, load, and query .NET objects in
|DDB|. For example, you can use this model to create, retrieve, update, and delete items in tables.
However, you must first create your tables using the low-level model, and then use this model to map
your .NET classes to the tables.

The following examples show you how to define a .NET class that represents an item, use an
instance of the .NET class to insert an item, and use an instance of a .NET object to get an item
from a table in |DDB|. 

.. _dynamodb-intro-apis-object-persistence-net-class-item:

Defining a .NET Class that Represents an Item in a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the following example, the :code:`DynamoDBTable` attribute specifies the table name, while the
:code:`DynamoDBHashKey` and :code:`DynamoDBRangeKey` attributes model the table's hash-and-range
primary key.

.. literalinclude:: how-to/dynamodb/dynamodb-create-class-obj-model.txt


.. _dynamodb-intro-apis-object-persistence-net-insert-item:

Using an Instance of the .NET Class to Insert an Item into a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In this example, the item is inserted through the :code:`Save` method of the
:code:`DynamoDBContext` class, which takes an initialized instance of the .NET class that represents
the item. (The instance of the :code:`DynamoDBContext` class is initialized with an instance of the
:code:`AmazonDynamoDBClient` class.)

.. literalinclude:: how-to/dynamodb/dynamodb-insert-item-obj-model.txt
   :language: csharp


.. _dynamodb-intro-apis-object-persistence-net-get-item:
   
Using an Instance of a .NET Object to Get an Item from a Table
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In this example, the item is retrieved through the :code:`Load` method of the
:code:`DynamoDBContext` class, which takes a partially initialized instance of the .NET class that
represents the hash-and-range primary key of the item to be retrieved. (As shown previously, the 
instance of the :code:`DynamoDBContext` class is initialized with an instance of the
:code:`AmazonDynamoDBClient` class.)

.. literalinclude:: how-to/dynamodb/dynamodb-get-item-obj-model.txt
   :language: csharp

   
.. _dynamodb-intro-more-info:

More Info
=========

**Using the |sdk-net| to program |DDB|** information and examples**

* :aws-blogs-net:`DynamoDB APIs <Tx17SQHVEMW8MXC/DynamoDB-APIs>`

* :aws-blogs-net:`DynamoDB Series Kickoff <Tx2XQOCY08QMTKO/DynamoDB-Series-Kickoff>`

* :aws-blogs-net:`DynamoDB Series - Document Model <Tx2R0WG46GQI1JI/DynamoDB-Series-Document-Model>`

* :aws-blogs-net:`DynamoDB Series - Conversion Schemas <Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas>`

* :aws-blogs-net:`DynamoDB Series - Object Persistence Model <Tx20L86FLMBW51P/DynamoDB-Series-Object-Persistence-Model>`

* :aws-blogs-net:`DynamoDB Series - Expressions <TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions>`

* :ref:`dynamodb-expressions`

* :ref:`dynamodb-json`

* :ref:`net-dg-dynamodb-session`


**Low-Level model information and examples**

* :ddb-dg:`Working with Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetWorkingWithTables>`

* :ddb-dg:`Working with Items Using the AWS SDK for .NET Low-Level API <LowLevelDotNetItemCRUD>`

* :ddb-dg:`Querying Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetQuerying>`

* :ddb-dg:`Scanning Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetScanning>`

* :ddb-dg:`Working with Local Secondary Indexes Using the AWS SDK for .NET Low-Level API <LSILowLevelDotNet>`

* :ddb-dg:`Working with Global Secondary Indexes Using the AWS SDK for .NET Low-Level API <GSILowLevelDotNet>`


**Document model information and examples**

* :ddb-dg-deep:`DynamoDB Data Types <DataModel.html#DataModel.DataTypes>`

* :ddb-dg:`DynamoDBEntry <TDynamoDBv2DocumentModelDynamoDBEntry>`

* :ddb-dg:`.NET: Document Model <DotNetSDKMidLevel>`


**Object persistance model information and examples**

* :ddb-dg:`.NET: Object Persistence Model <DotNetSDKHighLevel>`
