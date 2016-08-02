.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _dynamodb-expressions:

#############################################################
|DDBlong| Programming with Expressions by Using the |sdk-net|
#############################################################

The following code examples demonstrate how to use the the SDK to program |DDB| with expressions.
*Expressions* denote the attributes that you want to read from an item in a |DDB| table. You also
use expressions when writing an item, to indicate any conditions that must be met (also known as a
*conditional update*) and to indicate how the attributes are to be updated. Some update examples are
replacing the attribute with a new value, or adding new data to a list or a map. For more
information see :ddb-dg:`Reading and Writing Items Using Expressions <Expressions>`.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _dynamodb-expressions-sample-data:

Sample Data
===========

The code examples in this topic rely on the following two example items in a |DDB| table named
:code:`ProductCatalog`. These items describe information about product entries in a fictitious
bicycle store catalog. These items are based on the example that is provided in :ddb-dg:`Case Study: A
ProductCatalog Item <Expressions.CaseStudy>`. The data type descriptors such as :code:`BOOL`,
:code:`L`, :code:`M`, :code:`N`, :code:`NS`, :code:`S`, and :code:`SS` correspond to those in the
:ddb-dg:`JSON Data Format <DataFormat>`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-sample-data.txt
   :language: csharp


.. _dynamodb-expressions-get-item:

Get a Single Item by Using Expressions and the Item's Primary Key
=================================================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.GetItem` method and
a set of expressions to get and then print the item that has an :code:`Id` of :code:`205`. Only the
following attributes of the item are returned: :code:`Id`, :code:`Title`, :code:`Description`,
:code:`Color`, :code:`RelatedItems`, :code:`Pictures`, and :code:`ProductReviews`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-get-item.txt
   :language: csharp

In the preceding example, the :code:`ProjectionExpression` property specifies the attributes to be
returned. The :code:`ExpressionAttributeNames` property specifies the placeholder :code:`#pr` to
represent the :code:`ProductReviews` attribute and the placeholder :code:`#ri` to represent the
:code:`RelatedItems` attribute. The call to :code:`PrintItem` refers to a custom function as
described in :ref:`Print an Item <dynamodb-expressions-print-item>`.


.. _dynamodb-expressions-query:

Get Multiple Items by Using Expressions and the Table's Primary Key
===================================================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.Query` method and a
set of expressions to get and then print the item that has an :code:`Id` of :code:`301`, but only if
the value of :code:`Price` is greater than :code:`150`. Only the following attributes of the item
are returned: :code:`Id`, :code:`Title`, and all of the :code:`ThreeStar` attributes in
:code:`ProductReviews`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-query.txt
   :language: csharp
   
In the preceding example, the :code:`ProjectionExpression` property specifies the attributes to be
returned. The :code:`ExpressionAttributeNames` property specifies the placeholder :code:`#pr` to
represent the :code:`ProductReviews` attribute and the placeholder :code:`#p` to represent the
:code:`Price` attribute. :code:`#pr.ThreeStar` specifies to return only the :code:`ThreeStar`
attribute. The :code:`ExpressionAttributeValues` property specifies the placeholder :code:`:val` to
represent the value :code:`150`. The :code:`FilterExpression` property specifies that :code:`#p`
(:code:`Price`) must be greater than :code:`:val` (:code:`150`). The call to :code:`PrintItem`
refers to a custom function as described in :ref:`Print an Item <dynamodb-expressions-print-item>`.


.. _dynamodb-expressions-scan:

Get Multiple Items by Using Expressions and Other Item Attributes
=================================================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.Scan` method and a
set of expressions to get and then print all items that have a :code:`ProductCategory` of
:code:`Bike`. Only the following attributes of the item are returned: :code:`Id`, :code:`Title`, and
all of the attributes in :code:`ProductReviews`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-scan.txt
   :language: csharp

In the preceding example, the :code:`ProjectionExpression` property specifies the attributes to be
returned. The :code:`ExpressionAttributeNames` property specifies the placeholder :code:`#pr` to
represent the :code:`ProductReviews` attribute and the placeholder :code:`#pc` to represent the
:code:`ProductCategory` attribute. The :code:`ExpressionAttributeValues` property specifies the
placeholder :code:`:catg` to represent the value :code:`Bike`. The :code:`FilterExpression` property
specifies that :code:`#pc` (:code:`ProductCategory`) must be equal to :code:`:catg` (:code:`Bike`).
The call to :code:`PrintItem` refers to a custom function as described in :ref:`Print an Item
<dynamodb-expressions-print-item>`.


.. _dynamodb-expressions-print-item:

Print an Item
=============

The following example shows how to print an item's attributes and values. This example is used in
the preceding examples that show how to :ref:`dynamodb-expressions-get-item`,
:ref:`dynamodb-expressions-query`, and :ref:`dynamodb-expressions-scan`.

.. literalinclude:: how-to/dynamodb/dynamodb-print-item.txt
   :language: csharp

In the preceding example, each attribute value has several data-type-specific properties that can be
evaluated to determine the correct format to print the attribute. These properties include
properties such as :code:`B`, :code:`BOOL`, :code:`BS`, :code:`L`, :code:`M`, :code:`N`, :code:`NS`,
:code:`NULL`, :code:`S`, and :code:`SS`, which correspond to those in the `JSON Data Format
<DataFormat.html>`_. For properties such as :code:`B`, :code:`N`, :code:`NULL`, and :code:`S`, if
the corresponding property is not :code:`null`, then the attribute is of the corresponding
non-:code:`null` data type. For properties such as :code:`BS`, :code:`L`, :code:`M`, :code:`NS`, and
:code:`SS`, if :code:`Count` is greater than zero, then the attribute is of the corresponding
non-zero-value data type. If all of the attribute's data-type-specific properties are either
:code:`null` or the :code:`Count` equals zero, then the attribute corresponds to the :code:`BOOL`
data type.


.. _dynamodb-expressions-put-item:

Create or Replace an Item by Using Expressions
==============================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.PutItem` method and
a set of expressions to update the item that has a :code:`Title` of :code:`18-Bicycle 301`. If the
item doesn't already exist, a new item is added.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-put-item.txt
   :language: csharp

In the preceding example, the :code:`ExpressionAttributeNames` property specifies the placeholder
:code:`#title` to represent the :code:`Title` attribute. The :code:`ExpressionAttributeValues`
property specifies the placeholder :code:`:product` to represent the value :code:`18-Bicycle 301`.
The :code:`ConditionExpression` property specifies that :code:`#title` (:code:`Title`) must be equal
to :code:`:product` (:code:`18-Bicycle 301`). The call to :code:`CreateItemData` refers to the
following custom function:

.. literalinclude:: how-to/dynamodb/dynamodb-create-item-data.txt
   :language: csharp

In the preceding example, an example item with sample data is returned to the caller. A series of
attributes and corresponding values are constructed, using data types such as :code:`BOOL`,
:code:`L`, :code:`M`, :code:`N`, :code:`NS`, :code:`S`, and :code:`SS`, which correspond to those in
the `JSON Data Format <DataFormat.html>`_.


.. _dynamodb-expressions-update-item:

Update an Item by Using Expressions
===================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.UpdateItem` method
and a set of expressions to change the :code:`Title` to :code:`18" Girl's Bike` for the item with
:code:`Id` of :code:`301`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-update-item.txt
   :language: csharp

In the preceding example, the :code:`ExpressionAttributeNames` property specifies the placeholder
:code:`#title` to represent the :code:`Title` attribute. The :code:`ExpressionAttributeValues`
property specifies the placeholder :code:`:newproduct` to represent the value :code:`18" Girl's
Bike`. The :code:`UpdateExpression` property specifies to change :code:`#title` (:code:`Title`) to
:code:`:newproduct` (:code:`18" Girl's Bike`).


.. _dynamodb-expressions-delete-item:

Delete an Item by Using Expressions
===================================

The following example features the :code:`Amazon.DynamoDBv2.AmazonDynamoDBClient.DeleteItem` method
and a set of expressions to delete the item with :code:`Id` of :code:`301`, but only if the item's
:code:`Title` is :code:`18-Bicycle 301`.

.. literalinclude:: how-to/dynamodb/dynamodb-expressions-delete-item.txt
   :language: csharp

In the preceding example, the :code:`ExpressionAttributeNames` property specifies the placeholder
:code:`#title` to represent the :code:`Title` attribute. The :code:`ExpressionAttributeValues`
property specifies the placeholder :code:`:product` to represent the value :code:`18-Bicycle 301`.
The :code:`ConditionExpression` property specifies that :code:`#title` (:code:`Title`) must equal
:code:`:product` (:code:`18-Bicycle 301`).


.. _dynamodb-expressions-resources:

Additional Resources
====================

For additonal information and code examples, see:

* :aws-blogs-net:`DynamoDB Series - Expressions <TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions>`

* :ddb-dg:`Accessing Item Attributes with Projection Expressions <Expressions.AccessingItemAttributes>`

* :ddb-dg:`Using Placeholders for Attribute Names and Values <ExpressionPlaceholders>`

* :ddb-dg:`Specifying Conditions with Condition Expressions <Expressions.SpecifyingConditions>`

* :ddb-dg:`Modifying Items and Attributes with Update Expressions <Expressions.Modifying>`

* :ddb-dg:`Working with Items Using the AWS SDK for .NET Low-Level API <LowLevelDotNetItemCRUD>`

* :ddb-dg:`Querying Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetQuerying>`

* :ddb-dg:`Scanning Tables Using the AWS SDK for .NET Low-Level API <LowLevelDotNetScanning>`

* :ddb-dg:`Working with Local Secondary Indexes Using the AWS SDK for .NET Low-Level API <LSILowLevelDotNet>`

* :ddb-dg:`Working with Global Secondary Indexes Using the AWS SDK for .NET Low-Level API <GSILowLevelDotNet>`


