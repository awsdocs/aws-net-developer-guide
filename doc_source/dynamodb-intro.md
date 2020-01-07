# Using Amazon DynamoDB NoSQL Databases<a name="dynamodb-intro"></a>

The AWS SDK for \.NET supports Amazon DynamoDB, which is a fast NoSQL database service offered by AWS\. The SDK provides three programming models for communicating with DynamoDB: the *low\-level* model, the *document* model, and the *object persistence* model\.

The following information introduces these models and their APIs, provides examples for how and when to use them, and gives you links to additional DynamoDB programming resources in the AWS SDK for \.NET\.

**Topics**
+ [Low\-Level Model](#dynamodb-intro-apis-low-level)
+ [Document Model](#dynamodb-intro-apis-document)
+ [Object Persistence Model](#dynamodb-intro-apis-object-persistence)
+ [More Info](#dynamodb-intro-more-info)
+ [Using Expressions with Amazon DynamoDB and the AWS SDK for \.NET](dynamodb-expressions.md)
+ [JSON Support in Amazon DynamoDB with the AWS SDK for \.NET](dynamodb-json.md)
+ [Managing ASP\.NET Session State with Amazon DynamoDB](dynamodb-session-net-sdk.md)

## Low\-Level Model<a name="dynamodb-intro-apis-low-level"></a>

The low\-level programming model wraps direct calls to the DynamoDB service\. You access this model through the [Amazon\.DynamoDBv2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/NDynamoDBv2.html) namespace\.

Of the three models, the low\-level model requires you to write the most code\. For example, you must convert \.NET data types to their equivalents in DynamoDB\. However, this model gives you access to the most features\.

The following examples show you how to use the low\-level model to create a table, modify a table, and insert items into a table in DynamoDB\.

### Creating a Table<a name="dynamodb-intro-apis-low-level-create-table"></a>

In the following example, you create a table by using the `CreateTable` method of the `AmazonDynamoDBClient` class\. The `CreateTable` method uses an instance of the `CreateTableRequest` class that contains characteristics such as required item attribute names, primary key definition, and throughput capacity\. The `CreateTable` method returns an instance of the `CreateTableResponse` class\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();

Console.WriteLine("Getting list of tables");
List<string> currentTables = client.ListTables().TableNames;
Console.WriteLine("Number of tables: " + currentTables.Count);
if (!currentTables.Contains("AnimalsInventory"))
{
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
}
```

### Verifying That a Table is Ready to Modify<a name="dynamodb-intro-apis-low-level-verify-table"></a>

Before you can change or modify a table, the table has to be ready for modification\. The following example shows how to use the low\-level model to verify that a table in DynamoDB is ready\. In this example, the target table to check is referenced through the `DescribeTable` method of the `AmazonDynamoDBClient` class\. Every five seconds, the code checks the value of the table’s `TableStatus` property\. When the status is set to `ACTIVE`, the table is ready to be modified\.

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

### Inserting an Item into a Table<a name="dynamodb-intro-apis-low-level-insert-item"></a>

In the following example, you use the low\-level model to insert two items into a table in DynamoDB\. Each item is inserted through the `PutItem` method of the `AmazonDynamoDBClient` class, using an instance of the `PutItemRequest` class\. Each of the two instances of the `PutItemRequest` class takes the name of the table that the items will be inserted in, with a series of item attribute values\.

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

## Document Model<a name="dynamodb-intro-apis-document"></a>

The document programming model provides an easier way to work with data in DynamoDB\. This model is specifically intended for accessing tables and items in tables\. You access this model through the [Amazon\.DynamoDBv2\.DocumentModel](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/NDynamoDBv2DocumentModel.html) namespace\.

Compared to the low\-level programming model, the document model is easier to code against DynamoDB data\. For example, you don’t have to convert as many \.NET data types to their equivalents in DynamoDB\. However, this model doesn’t provide access to as many features as the low\-level programming model\. For example, you can use this model to create, retrieve, update, and delete items in tables\. However, to create the tables, you must use the low\-level model\. Compared to the object persistence model, this model requires you to write more code to store, load, and query \.NET objects\.

The following examples show you how to use the document model to insert items and get items in tables in DynamoDB\.

### Inserting an Item into a Table<a name="dynamodb-intro-apis-document-insert-item"></a>

In the following example, an item is inserted into the table through the `PutItem` method of the `Table` class\. The `PutItem` method takes an instance of the `Document` class; the `Document` class is simply a collection of initialized attributes\. To determine the table to insert the item into, call the `LoadTable` method of the `Table` class, specifying an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

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

### Getting an Item from a Table<a name="dynamodb-intro-apis-document-get-item"></a>

In the following example, the item is retrieved through the `GetItem` method of the `Table` class\. To determine the item to get, the `GetItem` method uses the hash\-and\-range primary key of the target item\. To determine the table to get the item from, the `LoadTable` method of the `Table` class uses an instance of the `AmazonDynamoDBClient` class and the name of the target table in DynamoDB\.

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

The preceding example implicitly converts the attribute values for `Id`, `Type`, and `Name` to strings for the `WriteLine` method\. You can do explicit conversions by using the various `AsType` methods of the `DynamoDBEntry` class\. For example, you could explicitly convert the attribute value for `Id` from a `Primitive` data type to an integer through the `AsInt` method:

```
int id = item["Id"].AsInt();
```

Or, you could simply perform an explicit cast here by using `(int)`:

```
int id = (int)item["Id"];
```

## Object Persistence Model<a name="dynamodb-intro-apis-object-persistence"></a>

The object persistence programming model is specifically designed for storing, loading, and querying \.NET objects in DynamoDB\. You access this model through the [Amazon\.DynamoDBv2\.DataModel](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/DynamoDBv2/NDynamoDBv2DataModel.html) namespace\.

Of the three models, the object persistence model is the easiest to code against whenever you are storing, loading, or querying DynamoDB data\. For example, you work with DynamoDB data types directly\. However, this model provides access only to operations that store, load, and query \.NET objects in DynamoDB\. For example, you can use this model to create, retrieve, update, and delete items in tables\. However, you must first create your tables using the low\-level model, and then use this model to map your \.NET classes to the tables\.

The following examples show you how to define a \.NET class that represents an item, use an instance of the \.NET class to insert an item, and use an instance of a \.NET object to get an item from a table in DynamoDB\.

### Defining a \.NET Class that Represents an Item in a Table<a name="dynamodb-intro-apis-object-persistence-net-class-item"></a>

In the following example, the `DynamoDBTable` attribute specifies the table name, while the `DynamoDBHashKey` and `DynamoDBRangeKey` attributes model the table’s hash\-and\-range primary key\.

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

### Using an Instance of the \.NET Class to Insert an Item into a Table<a name="dynamodb-intro-apis-object-persistence-net-insert-item"></a>

In this example, the item is inserted through the `Save` method of the `DynamoDBContext` class, which takes an initialized instance of the \.NET class that represents the item\. \(The instance of the `DynamoDBContext` class is initialized with an instance of the `AmazonDynamoDBClient` class\.\)

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

### Using an Instance of a \.NET Object to Get an Item from a Table<a name="dynamodb-intro-apis-object-persistence-net-get-item"></a>

In this example, the item is retrieved through the `Load` method of the `DynamoDBContext` class, which takes a partially initialized instance of the \.NET class that represents the hash\-and\-range primary key of the item to be retrieved\. \(As shown previously, the instance of the `DynamoDBContext` class is initialized with an instance of the `AmazonDynamoDBClient` class\.\)

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

## More Info<a name="dynamodb-intro-more-info"></a>

 **Using the AWS SDK for \.NET to program DynamoDB** information and examples\*\*
+  [DynamoDB APIs](http://blogs.aws.amazon.com/net/post/Tx17SQHVEMW8MXC/DynamoDB-APIs) 
+  [DynamoDB Series Kickoff](http://blogs.aws.amazon.com/net/post/Tx2XQOCY08QMTKO/DynamoDB-Series-Kickoff) 
+  [DynamoDB Series \- Document Model](http://blogs.aws.amazon.com/net/post/Tx2R0WG46GQI1JI/DynamoDB-Series-Document-Model) 
+  [DynamoDB Series \- Conversion Schemas](http://blogs.aws.amazon.com/net/post/Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas) 
+  [DynamoDB Series \- Object Persistence Model](http://blogs.aws.amazon.com/net/post/Tx20L86FLMBW51P/DynamoDB-Series-Object-Persistence-Model) 
+  [DynamoDB Series \- Expressions](http://blogs.aws.amazon.com/net/post/TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions) 
+  [Using Expressions with Amazon DynamoDB and the AWS SDK for \.NET](dynamodb-expressions.md) 
+  [JSON Support in Amazon DynamoDB with the AWS SDK for \.NET](dynamodb-json.md) 
+  [Managing ASP\.NET Session State with Amazon DynamoDB](dynamodb-session-net-sdk.md#net-dg-dynamodb-session) 

 **Low\-Level model information and examples** 
+  [Working with Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html) 
+  [Working with Items Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetItemCRUD.html) 
+  [Querying Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetQuerying.html) 
+  [Scanning Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetScanning.html) 
+  [Working with Local Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LSILowLevelDotNet.html) 
+  [Working with Global Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GSILowLevelDotNet.html) 

 **Document model information and examples** 
+  [DynamoDB Data Types](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html#DataModel.DataTypes) 
+  [DynamoDBEntry](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TDynamoDBv2DocumentModelDynamoDBEntry.html) 
+  [\.NET: Document Model](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKMidLevel.html) 

 **Object persistance model information and examples** 
+  [\.NET: Object Persistence Model](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKHighLevel.html) 