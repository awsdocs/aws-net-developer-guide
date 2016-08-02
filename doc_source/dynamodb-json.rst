.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _dynamodb-json:

############################################
JSON Support in |DDBlong| with the |sdk-net|
############################################

The |sdk-net| supports JSON data when working with |DDBlong|. This enables you to more easily get
JSON-formatted data from, and insert JSON documents into, |DDB| tables.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _dynamodb-json-get-table-data:

Get Data from a |DDB| Table in JSON Format
==========================================

The following example shows how to get data from a |DDB| table in JSON format:

.. literalinclude:: how-to/dynamodb/dynamodb-doc-to-json.txt
   :language: csharp

In the preceding example, the :code:`Document` class's :code:`ToJson` method converts an item from
the table into a JSON-formatted string. The item is retrieved through the :code:`Table` class's
:code:`GetItem` method. To determine the item to get, in this example, the :code:`GetItem` method
uses the hash-and-range primary key of the target item. To determine the table to get the item from,
the :code:`Table` class's :code:`LoadTable` method uses an instance of the
:code:`AmazonDynamoDBClient` class and the name of the target table in |DDB|.


.. _dynamodb-json-insert-table-data:

Insert JSON Format Data into a |DDB| Table
==========================================

The following example shows how to use JSON format to insert an item into a |DDB| table:

.. literalinclude:: how-to/dynamodb/dynamodb-json-to-doc.txt
    :language: csharp

In the preceding example, the :code:`Document` class's :code:`FromJson` method converts a
JSON-formatted string into an item. The item is inserted into the table through the :code:`Table`
class's :code:`PutItem` method, which uses the instance of the :code:`Document` class that contains
the item. To determine the table to insert the item into, the :code:`Table` class's
:code:`LoadTable` method is called, specifying an instance of the :code:`AmazonDynamoDBClient` class
and the name of the target table in |DDB|.


.. _dynamodb-json-datatypes:

|DDB| Data Type Conversions to JSON
===================================

Whenever you call the :code:`Document` class's :code:`ToJson` method, and then on the resulting JSON
data you call the :code:`FromJson` method to convert the JSON data back into an instance of a
:code:`Document` class, some |DDB| data types will not convert as expected. Specifically:

* |DDB| sets (the :code:`SS`, :code:`NS`, and :code:`BS` types) will be converted to JSON arrays.

* |DDB| binary scalars and sets (the :code:`B` and :code:`BS` types) will be converted to
  base64-encoded JSON strings or lists of strings.

In this scenario, you must call the :code:`Document` class's :code:`DecodeBase64Attributes`
method to replace the base64-encoded JSON data with the correct binary representation. The
following example replaces a base64-encoded binary scalar item attribute in an instance of a
:code:`Document` class, named :code:`Picture`, with the correct binary representation. This
example also does the same for a base64-encoded binary set item attribute in the same instance
of the :code:`Document` class, named :code:`RelatedPictures`:

.. code-block:: csharp

    item.DecodeBase64Attributes("Picture", "RelatedPictures");


.. _dynamodb-json-more-info:

Additional Resources
====================

For additional information and examples of programming JSON with |DDB| with the the SDK, see:

* :aws-blogs-net:`DynamoDB JSON Support <Tx14U0PAQWWHGXM/DynamoDB-JSON-Support>`

* :aws-blogs:`Amazon DynamoDB Update - JSON, Expanded Free Tier, Flexible Scaling, Larger Items <aws/dynamodb-update-json-and-more>`



