--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Using Expressions with Amazon DynamoDB and the AWS SDK for \.NET<a name="dynamodb-expressions"></a>

The following code examples demonstrate how to use the AWS SDK for \.NET to program DynamoDB with expressions\. *Expressions* denote the attributes you want to read from an item in a DynamoDB table\. You also use expressions when writing an item, to indicate any conditions that must be met \(also known as a *conditional update*\) and how the attributes are to be updated\. Some update examples are replacing the attribute with a new value, or adding new data to a list or a map\. For more information, see [Reading and Writing Items Using Expressions](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.html)\.

**Topics**
+ [Sample Data](#dynamodb-expressions-sample-data)
+ [Get a Single Item by Using Expressions and the Item’s Primary Key](#dynamodb-expressions-get-item)
+ [Get Multiple Items by Using Expressions and the Table’s Primary Key](#dynamodb-expressions-query)
+ [Get Multiple Items by Using Expressions and Other Item Attributes](#dynamodb-expressions-scan)
+ [Print an Item](#dynamodb-expressions-print-item)
+ [Create or Replace an Item by Using Expressions](#dynamodb-expressions-put-item)
+ [Update an Item by Using Expressions](#dynamodb-expressions-update-item)
+ [Delete an Item by Using Expressions](#dynamodb-expressions-delete-item)
+ [More Info](#dynamodb-expressions-resources)

## Sample Data<a name="dynamodb-expressions-sample-data"></a>

The code examples in this topic rely on the following two example items in a DynamoDB table named `ProductCatalog`\. These items describe information about product entries in a fictitious bicycle store catalog\. These items are based on the example provided in [Case Study: A ProductCatalog Item](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.CaseStudy.html)\. The data type descriptors such as `BOOL`, `L`, `M`, `N`, `NS`, `S`, and `SS` correspond to those in the [JSON Data Format](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html)\.

```
{
  "Id": {
    "N": "205"
  },
  "Title": {
    "S": "20-Bicycle 205"
  },
  "Description": {
    "S": "205 description"
  },
  "BicycleType": {
    "S": "Hybrid"
  },
  "Brand": {
    "S": "Brand-Company C"
  },
  "Price": {
    "N": "500"
  },
  "Gender": {
    "S": "B"
  },
  "Color": {
    "SS": [
      "Red",
      "Black"
    ]
  },
  "ProductCategory": {
    "S": "Bike"
  },
  "InStock": {
    "BOOL": true
  },
  "QuantityOnHand": {
    "N": "1"
  },
  "RelatedItems": {
    "NS": [
      "341",
      "472",
      "649"
    ]
  },
  "Pictures": {
    "L": [
      {
        "M": {
          "FrontView": {
            "S": "http://example/products/205_front.jpg"
          }
        }
      },
      {
        "M": {
          "RearView": {
            "S": "http://example/products/205_rear.jpg"
          }
        }
      },
      {
        "M": {
          "SideView": {
            "S": "http://example/products/205_left_side.jpg"
          }
        }
      }
    ]
  },
  "ProductReviews": {
    "M": {
      "FiveStar": {
        "SS": [
          "Excellent! Can't recommend it highly enough! Buy it!",
          "Do yourself a favor and buy this."
        ]
      },
      "OneStar": {
        "SS": [
          "Terrible product! Do not buy this."
        ]
      }
    }
  }
},
{
  "Id": {
    "N": "301"
  },
  "Title": {
    "S": "18-Bicycle 301"
  },
  "Description": {
    "S": "301 description"
  },
  "BicycleType": {
    "S": "Road"
  },
  "Brand": {
    "S": "Brand-Company C"
  },
  "Price": {
    "N": "185"
  },
  "Gender": {
    "S": "F"
  },
  "Color": {
    "SS": [
      "Blue",
      "Silver"
    ]
  },
  "ProductCategory": {
    "S": "Bike"
  },
  "InStock": {
    "BOOL": true
  },
  "QuantityOnHand": {
    "N": "3"
  },
  "RelatedItems": {
    "NS": [
      "801",
      "822",
      "979"
    ]
  },
  "Pictures": {
    "L": [
      {
        "M": {
          "FrontView": {
            "S": "http://example/products/301_front.jpg"
          }
        }
      },
      {
        "M": {
          "RearView": {
            "S": "http://example/products/301_rear.jpg"
          }
        }
      },
      {
        "M": {
          "SideView": {
            "S": "http://example/products/301_left_side.jpg"
          }
        }
      }
    ]
  },
  "ProductReviews": {
    "M": {
      "FiveStar": {
        "SS": [
          "My daughter really enjoyed this bike!"
        ]
      },
      "ThreeStar": {
        "SS": [
          "This bike was okay, but I would have preferred it in my color.",
	      "Fun to ride."
        ]
      }
    }
  }
}
```

## Get a Single Item by Using Expressions and the Item’s Primary Key<a name="dynamodb-expressions-get-item"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.GetItem` method and a set of expressions to get and then print the item that has an `Id` of `205`\. Only the following attributes of the item are returned: `Id`, `Title`, `Description`, `Color`, `RelatedItems`, `Pictures`, and `ProductReviews`\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new GetItemRequest
{
  TableName = "ProductCatalog",
  ProjectionExpression = "Id, Title, Description, Color, #ri, Pictures, #pr",
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#pr", "ProductReviews" },
    { "#ri", "RelatedItems" }
  },
  Key = new Dictionary<string, AttributeValue>
  {
    { "Id", new AttributeValue { N = "205" } }
  },
};
var response = client.GetItem(request);

// PrintItem() is a custom function.
PrintItem(response.Item);
```

In the preceding example, the `ProjectionExpression` property specifies the attributes to be returned\. The `ExpressionAttributeNames` property specifies the placeholder `#pr` to represent the `ProductReviews` attribute and the placeholder `#ri` to represent the `RelatedItems` attribute\. The call to `PrintItem` refers to a custom function as described in [Print an Item](#dynamodb-expressions-print-item)\.

## Get Multiple Items by Using Expressions and the Table’s Primary Key<a name="dynamodb-expressions-query"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.Query` method and a set of expressions to get and then print the item that has an `Id` of `301`, but only if the value of `Price` is greater than `150`\. Only the following attributes of the item are returned: `Id`, `Title`, and all of the `ThreeStar` attributes in `ProductReviews`\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new QueryRequest
{
  TableName = "ProductCatalog",
  KeyConditions = new Dictionary<string,Condition>
  {
    { "Id", new Condition()
      {
        ComparisonOperator = ComparisonOperator.EQ,
        AttributeValueList = new List<AttributeValue>
        {
          new AttributeValue { N = "301" }
        }
      }
    }
  },
  ProjectionExpression = "Id, Title, #pr.ThreeStar",
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#pr", "ProductReviews" },
    { "#p", "Price" }
  },
  ExpressionAttributeValues = new Dictionary<string,AttributeValue>
  {
    { ":val", new AttributeValue { N = "150" } }
  },
  FilterExpression = "#p > :val"
};
var response = client.Query(request);

foreach (var item in response.Items)
{
  // Write out the first page of an item's attribute keys and values.
  // PrintItem() is a custom function.
  PrintItem(item);
  Console.WriteLine("=====");
}
```

In the preceding example, the `ProjectionExpression` property specifies the attributes to be returned\. The `ExpressionAttributeNames` property specifies the placeholder `#pr` to represent the `ProductReviews` attribute and the placeholder `#p` to represent the `Price` attribute\. `#pr.ThreeStar` specifies to return only the `ThreeStar` attribute\. The `ExpressionAttributeValues` property specifies the placeholder `:val` to represent the value `150`\. The `FilterExpression` property specifies that `#p` \(`Price`\) must be greater than `:val` \(`150`\)\. The call to `PrintItem` refers to a custom function as described in [Print an Item](#dynamodb-expressions-print-item)\.

## Get Multiple Items by Using Expressions and Other Item Attributes<a name="dynamodb-expressions-scan"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.Scan` method and a set of expressions to get and then print all items that have a `ProductCategory` of `Bike`\. Only the following attributes of the item are returned: `Id`, `Title`, and all of the attributes in `ProductReviews`\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new ScanRequest
{
  TableName = "ProductCatalog",
  ProjectionExpression = "Id, Title, #pr",
  ExpressionAttributeValues = new Dictionary<string,AttributeValue>
  {
    { ":catg", new AttributeValue { S = "Bike" } }
  },
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#pr", "ProductReviews" },
    { "#pc", "ProductCategory" }
  },
  FilterExpression = "#pc = :catg",  
};
var response = client.Scan(request);

foreach (var item in response.Items)
{
  // Write out the first page/scan of an item's attribute keys and values.
  // PrintItem() is a custom function.
  PrintItem(item);
  Console.WriteLine("=====");
}
```

In the preceding example, the `ProjectionExpression` property specifies the attributes to be returned\. The `ExpressionAttributeNames` property specifies the placeholder `#pr` to represent the `ProductReviews` attribute and the placeholder `#pc` to represent the `ProductCategory` attribute\. The `ExpressionAttributeValues` property specifies the placeholder `:catg` to represent the value `Bike`\. The `FilterExpression` property specifies that `#pc` \(`ProductCategory`\) must be equal to `:catg` \(`Bike`\)\. The call to `PrintItem` refers to a custom function as described in [Print an Item](#dynamodb-expressions-print-item)\.

## Print an Item<a name="dynamodb-expressions-print-item"></a>

The following example shows how to print an item’s attributes and values\. This example is used in the preceding examples that show how to [Get a Single Item by Using Expressions and the Item’s Primary Key](#dynamodb-expressions-get-item), [Get Multiple Items by Using Expressions and the Table’s Primary Key](#dynamodb-expressions-query), and [Get Multiple Items by Using Expressions and Other Item Attributes](#dynamodb-expressions-scan)\.

```
// using Amazon.DynamoDBv2.Model;

// Writes out an item's attribute keys and values.
public static void PrintItem(Dictionary<string, AttributeValue> attrs)
{
  foreach (KeyValuePair<string, AttributeValue> kvp in attrs)
  {
    Console.Write(kvp.Key + " = ");
    PrintValue(kvp.Value);
  }
}

// Writes out just an attribute's value.
public static void PrintValue(AttributeValue value)
{
  // Binary attribute value.
  if (value.B != null)
  {
    Console.Write("Binary data");
  }
  // Binary set attribute value.
  else if (value.BS.Count > 0)
  {
    foreach (var bValue in value.BS)
    {
      Console.Write("\n  Binary data");
    }
  }
  // List attribute value.
  else if (value.L.Count > 0)
  {
    foreach (AttributeValue attr in value.L)
    {
      PrintValue(attr);
    }
  }
  // Map attribute value.
  else if (value.M.Count > 0)
  {
    Console.Write("\n");
    PrintItem(value.M);
  }
  // Number attribute value.
  else if (value.N != null)
  {
    Console.Write(value.N);
  }
  // Number set attribute value.
  else if (value.NS.Count > 0)
  {
    Console.Write("{0}", string.Join("\n", value.NS.ToArray()));
  }
  // Null attribute value.
  else if (value.NULL)
  {
    Console.Write("Null");
  }
  // String attribute value.
  else if (value.S != null)
  {
    Console.Write(value.S);
  }
  // String set attribute value.
  else if (value.SS.Count > 0)
  {
    Console.Write("{0}", string.Join("\n", value.SS.ToArray()));
  }
  // Otherwise, boolean value.
  else
  {
    Console.Write(value.BOOL);
  }
 
  Console.Write("\n");
}
```

In the preceding example, each attribute value has several data\-type\-specific properties that can be evaluated to determine the correct format to print the attribute\. These properties include `B`, `BOOL`, `BS`, `L`, `M`, `N`, `NS`, `NULL`, `S`, and `SS`, which correspond to those in the [JSON Data Format](DataFormat.html)\. For properties such as `B`, `N`, `NULL`, and `S`, if the corresponding property is not `null`, then the attribute is of the corresponding non\-`null` data type\. For properties such as `BS`, `L`, `M`, `NS`, and `SS`, if `Count` is greater than zero, then the attribute is of the corresponding non\-zero\-value data type\. If all of the attribute’s data\-type\-specific properties are either `null` or the `Count` equals zero, then the attribute corresponds to the `BOOL` data type\.

## Create or Replace an Item by Using Expressions<a name="dynamodb-expressions-put-item"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.PutItem` method and a set of expressions to update the item that has a `Title` of `18-Bicycle 301`\. If the item doesn’t already exist, a new item is added\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new PutItemRequest
{
  TableName = "ProductCatalog",
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#title", "Title" }
  },
  ExpressionAttributeValues = new Dictionary<string, AttributeValue>
  {
    { ":product", new AttributeValue { S = "18-Bicycle 301" } }
  },
  ConditionExpression = "#title = :product", 
  // CreateItemData() is a custom function.
  Item = CreateItemData()
};
client.PutItem(request);
```

In the preceding example, the `ExpressionAttributeNames` property specifies the placeholder `#title` to represent the `Title` attribute\. The `ExpressionAttributeValues` property specifies the placeholder `:product` to represent the value `18-Bicycle 301`\. The `ConditionExpression` property specifies that `#title` \(`Title`\) must be equal to `:product` \(`18-Bicycle 301`\)\. The call to `CreateItemData` refers to the following custom function:

```
// using Amazon.DynamoDBv2.Model;

// Provides a sample item that can be added to a table.
public static Dictionary<string, AttributeValue> CreateItemData()
{
  var itemData = new Dictionary<string, AttributeValue>
  {
    { "Id", new AttributeValue { N = "301" } },
    { "Title", new AttributeValue { S = "18\" Girl's Bike" } },
    { "BicycleType", new AttributeValue { S = "Road" } },
    { "Brand" , new AttributeValue { S = "Brand-Company C" } },
    { "Color", new AttributeValue { SS = new List<string>{ "Blue", "Silver" } } },
    { "Description", new AttributeValue { S = "301 description" } },
    { "Gender", new AttributeValue { S = "F" } },
    { "InStock", new AttributeValue { BOOL = true } },
    { "Pictures", new AttributeValue { L = new List<AttributeValue>{ 
      { new AttributeValue { M = new Dictionary<string,AttributeValue>{
        { "FrontView", new AttributeValue { S = "http://example/products/301_front.jpg" } } } } },
      { new AttributeValue { M = new Dictionary<string,AttributeValue>{
        { "RearView", new AttributeValue {S = "http://example/products/301_rear.jpg" } } } } },
      { new AttributeValue { M = new Dictionary<string,AttributeValue>{
        { "SideView", new AttributeValue { S = "http://example/products/301_left_side.jpg" } } } } }
    } } },
    { "Price", new AttributeValue { N = "185" } },
    { "ProductCategory", new AttributeValue { S = "Bike" } },
    { "ProductReviews", new AttributeValue { M = new Dictionary<string,AttributeValue>{
      { "FiveStar", new AttributeValue { SS = new List<string>{
        "My daughter really enjoyed this bike!" } } },
      { "OneStar", new AttributeValue { SS = new List<string>{
        "Fun to ride.",
        "This bike was okay, but I would have preferred it in my color." } } }
    } } },
    { "QuantityOnHand", new AttributeValue { N = "3" } },
    { "RelatedItems", new AttributeValue { NS = new List<string>{ "979", "822", "801" } } }
  };

  return itemData;
}
```

In the preceding example, an example item with sample data is returned to the caller\. A series of attributes and corresponding values are constructed, using data types such as `BOOL`, `L`, `M`, `N`, `NS`, `S`, and `SS`, which correspond to those in the [JSON Data Format](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html)\.

## Update an Item by Using Expressions<a name="dynamodb-expressions-update-item"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.UpdateItem` method and a set of expressions to change the `Title` to `18" Girl's Bike` for the item with `Id` of `301`\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new UpdateItemRequest
{
  TableName = "ProductCatalog",
  Key = new Dictionary<string,AttributeValue>
  {
     { "Id", new AttributeValue { N = "301" } }
  },
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#title", "Title" }
  },
  ExpressionAttributeValues = new Dictionary<string, AttributeValue>
  {
    { ":newproduct", new AttributeValue { S = "18\" Girl's Bike" } }
  },
  UpdateExpression = "SET #title = :newproduct"
};
client.UpdateItem(request);
```

In the preceding example, the `ExpressionAttributeNames` property specifies the placeholder `#title` to represent the `Title` attribute\. The `ExpressionAttributeValues` property specifies the placeholder `:newproduct` to represent the value `18" Girl's Bike`\. The `UpdateExpression` property specifies to change `#title` \(`Title`\) to `:newproduct` \(`18" Girl's Bike`\)\.

## Delete an Item by Using Expressions<a name="dynamodb-expressions-delete-item"></a>

The following example features the `Amazon.DynamoDBv2.AmazonDynamoDBClient.DeleteItem` method and a set of expressions to delete the item with `Id` of `301`, but only if the item’s `Title` is `18-Bicycle 301`\.

```
// using Amazon.DynamoDBv2;
// using Amazon.DynamoDBv2.Model;

var client = new AmazonDynamoDBClient();
var request = new DeleteItemRequest
{
  TableName = "ProductCatalog",
  Key = new Dictionary<string,AttributeValue>
  {
     { "Id", new AttributeValue { N = "301" } }
  },
  ExpressionAttributeNames = new Dictionary<string, string>
  {
    { "#title", "Title" }
  },
  ExpressionAttributeValues = new Dictionary<string, AttributeValue>
  {
    { ":product", new AttributeValue { S = "18-Bicycle 301" } }
  },
  ConditionExpression = "#title = :product"
};
client.DeleteItem(request);
```

In the preceding example, the `ExpressionAttributeNames` property specifies the placeholder `#title` to represent the `Title` attribute\. The `ExpressionAttributeValues` property specifies the placeholder `:product` to represent the value `18-Bicycle 301`\. The `ConditionExpression` property specifies that `#title` \(`Title`\) must equal `:product` \(`18-Bicycle 301`\)\.

## More Info<a name="dynamodb-expressions-resources"></a>

For more information and code examples, see:
+  [DynamoDB Series \- Expressions](http://blogs.aws.amazon.com/net/post/TxZQM7VA9AUZ9L/DynamoDB-Series-Expressions) 
+  [Accessing Item Attributes with Projection Expressions](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html) 
+  [Using Placeholders for Attribute Names and Values](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ExpressionPlaceholders.html) 
+  [Specifying Conditions with Condition Expressions](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.SpecifyingConditions.html) 
+  [Modifying Items and Attributes with Update Expressions](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.Modifying.html) 
+  [Working with Items Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetItemCRUD.html) 
+  [Querying Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetQuerying.html) 
+  [Scanning Tables Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetScanning.html) 
+  [Working with Local Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LSILowLevelDotNet.html) 
+  [Working with Global Secondary Indexes Using the AWS SDK for \.NET Low\-Level API](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GSILowLevelDotNet.html) 