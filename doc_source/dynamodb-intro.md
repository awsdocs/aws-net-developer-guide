--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Amazon DynamoDB Programming with the AWS SDK for \.NET<a name="dynamodb-intro"></a>

The AWS SDK for \.NET supports Amazon DynamoDB, which is a fast NoSQL database service offered by AWS\.

The following information introduces you to the DynamoDB programming models and their APIs\. There are also links to additional DynamoDB programming resources within the the AWS SDK for \.NET\.

**Topics**
+ [Programming Models](#dynamodb-intro-apis)
+ [Additional Resources](#dynamodb-intro-more-info)
+ [Amazon DynamoDB Programming with Expressions by Using the AWS SDK for \.NET](dynamodb-expressions.md)
+ [JSON Support in Amazon DynamoDB with the AWS SDK for \.NET](dynamodb-json.md)
+ [Managing ASP\.NET Session State with Amazon DynamoDB](dynamodb-session-net-sdk.md)

## Programming Models<a name="dynamodb-intro-apis"></a>

The the SDK provides three different programming models for communicating with DynamoDB\. These programming models include the *low\-level* model, the *document* model, and the *object persistence* model\. The following information describes these models, how to use them, and when you might want to use them\.

**Topics**
+ [Low\-Level](#dynamodb-intro-apis-low-level)
+ [Document](#dynamodb-intro-apis-document)
+ [Object Persistence](#dynamodb-intro-apis-object-persistence)

### Low\-Level<a name="dynamodb-intro-apis-low-level"></a>

The low\-level programming model wraps direct calls to the DynamoDB service\. You access this model through the [Amazon\.DynamoDBv2](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NDynamoDBv2NET45.html) namespace\.

Of the three models, the low\-level model requires you to write the most code\. For example, you must convert \.NET data types to their equivalents in DynamoDB\. However, this model gives you access to the most features\.

The following example shows how to use the low\-level model to create a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();

var request = new CreateTableRequest
{
  TableName = "AnimalsInventory",
  AttributeDefinitions = new List<AttributeDefinition>
  {
    new AttributeDefinition
    {
      AttributeName = "Id",
      // "S" = string, "N" = number, and so on.
      AttributeType = "N"
    },
    new AttributeDefinition
    {
      AttributeName = "Type",
      AttributeType = "S"
    }
  },
  KeySchema = new List<KeySchemaElement>
  {
    new KeySchemaElement
    {
      AttributeName = "Id",
      // "HASH" = hash key, "RANGE" = range key.
      KeyType = "HASH"
    },
    new KeySchemaElement
    {
      AttributeName = "Type",
      KeyType = "RANGE"
    },
  },
  ProvisionedThroughput = new ProvisionedThroughput
  {
    ReadCapacityUnits = 10,
    WriteCapacityUnits = 5
  },  
};

var response = client.CreateTable(request);

Console.WriteLine("Table created with request ID: " + 
  response.ResponseMetadata.RequestId);
```

In the preceding example, the table is created through the `AmazonDynamoDBClient` class’s `CreateTable` method\. The `CreateTable` method uses an instance of the `CreateTableRequest` class containing characteristics such as required item attribute names, primary key definition, and throughput capacity\. The `CreateTable` method returns an instance of the `CreateTableResponse` class\.

Before you begin modifying a table, you should make sure that the table is ready\. The following example shows how to use the low\-level model to verify that a table in DynamoDB is ready:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();      
var status = "";

do
{
  // Wait 5 seconds before checking (again).
  System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
        
  try
  {
    var response = client.DescribeTable(new DescribeTableRequest
    {
      TableName = "AnimalsInventory"
    });

    Console.WriteLine("Table = {0}, Status = {1}",
      response.Table.TableName,
      response.Table.TableStatus);

    status = response.Table.TableStatus;
  }
  catch (ResourceNotFoundException)
  {
    // DescribeTable is eventually consistent. So you might
    //   get resource not found. 
  }

} while (status != TableStatus.ACTIVE);
```

In the preceding example, the target table to check is referenced through the `AmazonDynamoDBClient` class’s `DescribeTable` method\. Every 5 seconds, the code checks the value of the table’s `TableStatus` property\. When the status is set to `ACTIVE`, then the table is ready to be modified\.

The following example shows how to use the low\-level model to insert two items into a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();

var request1 = new PutItemRequest
{
  TableName = "AnimalsInventory",
  Item = new Dictionary<string, AttributeValue>
  {
    { "Id", new AttributeValue { N = "1" }},
    { "Type", new AttributeValue { S = "Dog" }},
    { "Name", new AttributeValue { S = "Fido" }}
  }
};

var request2 = new PutItemRequest
{
  TableName = "AnimalsInventory",
  Item = new Dictionary<string, AttributeValue>
  {
    { "Id", new AttributeValue { N = "2" }},
    { "Type", new AttributeValue { S = "Cat" }},
    { "Name", new AttributeValue { S = "Patches" }}
  }
};
        
client.PutItem(request1);
client.PutItem(request2);
```

In the preceding example, each item is inserted through the `AmazonDynamoDBClient` class’s `PutItem` method, using an instance of the `PutItemRequest` class\. Each of the two instances of the `PutItemRequest` class takes the name of the table to be inserted into, along with a series of item attribute values\.

For more information and examples, see:
+  [Working with Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html) 
+  [Working with Items Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetItemCRUD.html) 
+  [Querying Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetQuerying.html) 
+  [Scanning Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetScanning.html) 
+  [Working with Local Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LSILowLevelDotNet.html) 
+  [Working with Global Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GSILowLevelDotNet.html) 

### Document<a name="dynamodb-intro-apis-document"></a>

The document programming model provides an easier way to work with data in DynamoDB\. This model is specifically intended for accessing tables and items in tables\. You access this model through the [Amazon\.DynamoDBv2\.DocumentModel](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NDynamoDBv2DocumentModel.html) namespace\.

Of the three models, the document model is easier to code against DynamoDB data compared to the low\-level programming model\. For example, you don’t have to convert as many \.NET data types to their equivalents in DynamoDB\. However, this model doesn’t provide access to as many features as the low\-level programming model\. For example, you can use this model to create, retrieve, update, and delete items in tables\. However, to create tables, you must use the low\-level model\. Finally, this model requires you to write more code to store, load, and query \.NET objects compared to the object persistence model\.

The following example shows how to use the document model to insert an item into a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient();
var table = Table.LoadTable(client, "AnimalsInventory");
var item = new Document();

item["Id"] = 3;
item["Type"] = "Horse";
item["Name"] = "Shadow";

table.PutItem(item);
```

In the preceding example, the item is inserted into the table through the `Table` class’s `PutItem` method\. The `PutItem` method takes an instance of the `Document` class; the `Document` class is simply a collection of initialized attributes\. To determine the table to insert the item into, the `Table` class’s `LoadTable` method is called, specifying an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

The following example shows how to use the document model to get an item from a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient();
var table = Table.LoadTable(client, "AnimalsInventory");
var item = table.GetItem(3, "Horse");

Console.WriteLine("Id = " + item["Id"]);
Console.WriteLine("Type = " + item["Type"]);
Console.WriteLine("Name = " + item["Name"]);
```

In the preceding example, the item is retrieved through the `Table` class’s `GetItem` method\. To determine the item to get, in this example, the `GetItem` method uses the hash\-and\-range primary key of the target item\. To determine the table to get the item from, the `Table` class’s `LoadTable` method uses an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

The preceding example implicitly converts the attribute values for `Id`, `Type`, `Name` to strings for the `WriteLine` method\. You can do explicit conversions by using the various `AsType` methods of the `DynamoDBEntry` class\. For example, you could explicitly convert the attribute value for `Id` from a `Primitive` data type to an integer through the `AsInt` method:

```
int id = item["Id"].AsInt();
```

Or, you could simply perform an explicit cast here by using `(int)`:

```
int id = (int)item["Id"];
```

For more information about data type conversions with DynamoDB, see [DynamoDB Data Types](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html#DataModel.DataTypes) and [DynamoDBEntry](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TDynamoDBv2DocumentModelDynamoDBEntry.html)\.

For more information and examples about the DynamoDB document model, see [\.NET: Document Model](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKMidLevel.html)\.

### Object Persistence<a name="dynamodb-intro-apis-object-persistence"></a>

The object persistence programming model is specifically designed for storing, loading, and querying \.NET objects in DynamoDB\. You access this model through the [Amazon\.DynamoDBv2\.DataModel](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NDynamoDBv2DataModel.html) namespace\.

Of the three models, the object persistence model is easiest to code against whenever you are storing, loading, or querying DynamoDB data\. For example, you work with DynamoDB data types directly\. However, this model provides access only to operations that store, load, and query \.NET objects in DynamoDB\. For example, you can use this model to create, retrieve, update and delete items in tables\. However, you must first create your tables using the low\-level model, and then you can use this model to map your \.NET classes to the tables\.

The following example shows how to define a \.NET class that represents an item in a table in DynamoDB:

```
// using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("AnimalsInventory")]
class Item
{
  [DynamoDBHashKey]
  public int Id { get; set; }
  [DynamoDBRangeKey]
  public string Type { get; set; }
  public string Name { get; set; }
}
```

In the preceding example, the `DynamoDBTable` attribute specifies the table name, while the `DynamoDBHashKey` and `DynamoDBRangeKey` attributes model the table’s hash\-and\-range primary key\.

The following example shows how to use an instance of this \.NET class to insert an item into a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DataModel;
    
var client = new AmazonDynamoDBClient();
var context = new DynamoDBContext(client);
var item = new Item
{
  Id = 4,
  Type = "Fish",
  Name = "Goldie"
};

context.Save(item);
```

In the preceding example, the item is inserted through the `DynamoDBContext` class’s `Save` method, which takes an initialized instance of the \.NET class that represents the item\. \(The instance of the `DynamoDBContext` class is initialized with an instance of the `AmazonDynamoDBClient` class\.\)

The following example shows how to use an instance of this \.NET object to get an item from a table in DynamoDB:

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.DataModel;

var client = new AmazonDynamoDBClient();
var context = new DynamoDBContext(client);
var item = context.Load<Item>(4, "Fish");

Console.WriteLine("Id = {0}", item.Id);
Console.WriteLine("Type = {0}", item.Type);
Console.WriteLine("Name = {0}", item.Name);
```

In the preceding example, the item is retrieved through the `DynamoDBContext` class’s `Load` method, which takes a partially\-initialized instance of the \.NET class that represents the hash\-and\-range primary key of the item to be retrieved\. \(As before, the instance of the `DynamoDBContext` class is initialized with an instance of the `AmazonDynamoDBClient` class\.\)

For more information and examples, see [\.NET: Object Persistence Model](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKHighLevel.html)\.

## Additional Resources<a name="dynamodb-intro-more-info"></a>

For additional information and examples of programming DynamoDB with the the SDK, see:
+  [DynamoDB APIs](http://blogs.aws.amazon.com/net/post/Tx17SQHVEMW8MXC/DynamoDB-APIs) 
+  [DynamoDB Series Kickoff](http://blogs.aws.amazon.com/net/post/Tx2XQOCY08QMTKO/DynamoDB-Series-Kickoff) 
+  [DynamoDB Series \- Document Model](http://blogs.aws.amazon.com/net/post/Tx2R0WG46GQI1JI/DynamoDB-Series-Document-Model) 
+  [DynamoDB Series \- Conversion Schemas](http://blogs.aws.amazon.com/net/post/Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas) 
+  [DynamoDB Series \- Object Persistence Model](http://blogs.aws.amazon.com/net/post/Tx20L86FLMBW51P/DynamoDB-Series-Object-Persistence-Model) 
+  [DynamoDB Series \- Expressions](http://blogs.aws.amazon.com/net/post/TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions) 
+  [Amazon DynamoDB Programming with Expressions by Using the AWS SDK for \.NET](dynamodb-expressions.md) 
+  [JSON Support in Amazon DynamoDB with the AWS SDK for \.NET](dynamodb-json.md) 
+  [Managing ASP\.NET Session State with Amazon DynamoDB](dynamodb-session-net-sdk.md#net-dg-dynamodb-session) 