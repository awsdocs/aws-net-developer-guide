--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# JSON Support in Amazon DynamoDB with the AWS SDK for \.NET<a name="dynamodb-json"></a>

The AWS SDK for \.NET supports JSON data when working with Amazon DynamoDB\. This enables you to more easily get JSON\-formatted data from, and insert JSON documents into, DynamoDB tables\.

**Topics**
+ [Get Data from a DynamoDB Table in JSON Format](#dynamodb-json-get-table-data)
+ [Insert JSON Format Data into a DynamoDB Table](#dynamodb-json-insert-table-data)
+ [DynamoDB Data Type Conversions to JSON](#dynamodb-json-datatypes)
+ [More Info](#dynamodb-json-more-info)

## Get Data from a DynamoDB Table in JSON Format<a name="dynamodb-json-get-table-data"></a>

The following example shows how to get data from a DynamoDB table in JSON format:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient();
var table = Table.LoadTable(client, "AnimalsInventory");
var item = table.GetItem(3, "Horse");

var jsonText = item.ToJson();
Console.Write(jsonText);
      
// Output:
//   {"Name":"Shadow","Type":"Horse","Id":3}

var jsonPrettyText = item.ToJsonPretty();
Console.WriteLine(jsonPrettyText);
      
// Output:
//   {
//     "Name" : "Shadow",
//     "Type" : "Horse",
//     "Id"   : 3
//   }
```

In the preceding example, the `ToJson` method of the `Document` class converts an item from the table into a JSON\-formatted string\. The item is retrieved through the `GetItem` method of the `Table` class\. To determine the item to get, in this example, the `GetItem` method uses the hash\-and\-range primary key of the target item\. To determine the table to get the item from, the `LoadTable` method of the `Table` class uses an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

## Insert JSON Format Data into a DynamoDB Table<a name="dynamodb-json-insert-table-data"></a>

The following example shows how to use JSON format to insert an item into a DynamoDB table:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient();
var table = Table.LoadTable(client, "AnimalsInventory");
var jsonText = "{\"Id\":6,\"Type\":\"Bird\",\"Name\":\"Tweety\"}";
var item = Document.FromJson(jsonText);

table.PutItem(item);
```

In the preceding example, the `FromJson` method of the `Document` class converts a JSON\-formatted string into an item\. The item is inserted into the table through the `PutItem` method of the `Table` class, which uses the instance of the `Document` class that contains the item\. To determine the table to insert the item into, the `LoadTable` method of the `Table` class is called, specifying an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

## DynamoDB Data Type Conversions to JSON<a name="dynamodb-json-datatypes"></a>

Whenever you call the `ToJson` method of the `Document` class, and then on the resulting JSON data you call the `FromJson` method to convert the JSON data back into an instance of a `Document` class, some DynamoDB data types will not be converted as expected\. Specifically:
+ DynamoDB sets \(the `SS`, `NS`, and `BS` types\) will be converted to JSON arrays\.
+ DynamoDB binary scalars and sets \(the `B` and `BS` types\) will be converted to base64\-encoded JSON strings or lists of strings\.

  In this scenario, you must call the `DecodeBase64Attributes` method of the `Document` class to replace the base64\-encoded JSON data with the correct binary representation\. The following example replaces a base64\-encoded binary scalar item attribute in an instance of a `Document` class, named `Picture`, with the correct binary representation\. This example also does the same for a base64\-encoded binary set item attribute in the same instance of the `Document` class, named `RelatedPictures`:

  ```
  item.DecodeBase64Attributes("Picture", "RelatedPictures");
  ```

## More Info<a name="dynamodb-json-more-info"></a>

For more information and examples of programming JSON with DynamoDB with the AWS SDK for \.NET, see:
+  [DynamoDB JSON Support](http://blogs.aws.amazon.com/net/post/Tx14U0PAQWWHGXM/DynamoDB-JSON-Support) 
+  [Amazon DynamoDB Update \- JSON, Expanded Free Tier, Flexible Scaling, Larger Items](http://aws.amazon.com/blogs/aws/dynamodb-update-json-and-more) 