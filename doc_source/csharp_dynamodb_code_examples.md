# DynamoDB examples using AWS SDK for \.NET<a name="csharp_dynamodb_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with DynamoDB\.

*Actions* are code excerpts that show you how to call individual DynamoDB functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple DynamoDB functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w150aac21c18b9c17c13)
+ [Scenarios](#w150aac21c18b9c17c15)

## Actions<a name="w150aac21c18b9c17c13"></a>

### Create a table<a name="dynamodb_CreateTable_csharp_topic"></a>

The following code example shows how to create a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Creates a new Amazon DynamoDB table and then waits for the new
        /// table to become active.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="tableName">The name of the table to create.</param>
        /// <returns>A Boolean value indicating the success of the operation.</returns>
        public static async Task<bool> CreateMovieTableAsync(AmazonDynamoDBClient client, string tableName)
        {
            var response = await client.CreateTableAsync(new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "title",
                        AttributeType = "S",
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "year",
                        AttributeType = "N",
                    },
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName = "year",
                        KeyType = "HASH",
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "title",
                        KeyType = "RANGE",
                    },
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5,
                },
            });

            // Wait until the table is ACTIVE and then report success.
            Console.Write("Waiting for table to become active...");

            var request = new DescribeTableRequest
            {
                TableName = response.TableDescription.TableName,
            };

            TableStatus status;

            int sleepDuration = 2000;

            do
            {
                System.Threading.Thread.Sleep(sleepDuration);

                var describeTableResponse = await client.DescribeTableAsync(request);
                status = describeTableResponse.Table.TableStatus;

                Console.Write(".");
            }
            while (status != "ACTIVE");

            return status == TableStatus.ACTIVE;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [CreateTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/CreateTable) in *AWS SDK for \.NET API Reference*\. 

### Delete a table<a name="dynamodb_DeleteTable_csharp_topic"></a>

The following code example shows how to delete a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        public static async Task<bool> DeleteTableAsync(AmazonDynamoDBClient client, string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName,
            };

            var response = await client.DeleteTableAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Table {response.TableDescription.TableName} successfully deleted.");
                return true;
            }
            else
            {
                Console.WriteLine("Could not delete table.");
                return false;
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [DeleteTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/DeleteTable) in *AWS SDK for \.NET API Reference*\. 

### Delete an item from a table<a name="dynamodb_DeleteItem_csharp_topic"></a>

The following code example shows how to delete an item from a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Deletes a single item from a DynamoDB table.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="tableName">The name of the table from which the item
        /// will be deleted.</param>
        /// <param name="movieToDelete">A movie object containing the title and
        /// year of the movie to delete.</param>
        /// <returns>A Boolean value indicating the success or failure of the
        /// delete operation.</returns>
        public static async Task<bool> DeleteItemAsync(
            AmazonDynamoDBClient client,
            string tableName,
            Movie movieToDelete)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = movieToDelete.Title },
                ["year"] = new AttributeValue { N = movieToDelete.Year.ToString() },
            };

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = key,
            };

            var response = await client.DeleteItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [DeleteItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/DeleteItem) in *AWS SDK for \.NET API Reference*\. 

### Get a batch of items<a name="dynamodb_BatchGetItem_csharp_topic"></a>

The following code example shows how to get a batch of DynamoDB items\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Gets information about an existing movie from the table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing information about
        /// the movie to retrieve.</param>
        /// <param name="tableName">The name of the table containing the movie.</param>
        /// <returns>A Dictionary object containing information about the item
        /// retrieved.</returns>
        public static async Task<Dictionary<string, AttributeValue>> GetItemAsync(AmazonDynamoDBClient client, Movie newMovie, string tableName)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };

            var request = new GetItemRequest
            {
                Key = key,
                TableName = tableName,
            };

            var response = await client.GetItemAsync(request);
            return response.Item;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [BatchGetItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/BatchGetItem) in *AWS SDK for \.NET API Reference*\. 

### Put an item in a table<a name="dynamodb_PutItem_csharp_topic"></a>

The following code example shows how to put an item in a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Adds a new item to the table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing informtation for
        /// the movie to add to the table.</param>
        /// <param name="tableName">The name of the table where the item will be added.</param>
        /// <returns>A Boolean value that indicates the results of adding the item.</returns>
        public static async Task<bool> PutItemAsync(AmazonDynamoDBClient client, Movie newMovie, string tableName)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };

            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item,
            };

            var response = await client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [PutItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/PutItem) in *AWS SDK for \.NET API Reference*\. 

### Query a table<a name="dynamodb_Query_csharp_topic"></a>

The following code example shows how to query a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Queries the table for movies released in a particular year and
        /// then displays the information for the movies returned.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="tableName">The name of the table to query.</param>
        /// <param name="year">The release year for which we want to
        /// view movies.</param>
        /// <returns>The number of movies that match the query.</returns>
        public static async Task<int> QueryMoviesAsync(AmazonDynamoDBClient client, string tableName, int year)
        {
            var movieTable = Table.LoadTable(client, tableName);
            var filter = new QueryFilter("year", QueryOperator.Equal, year);

            Console.WriteLine("\nFind movies released in: {year}:");

            var config = new QueryOperationConfig()
            {
                Limit = 10, // 10 items per page.
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string>
                {
                  "title",
                  "year",
                },
                ConsistentRead = true,
                Filter = filter,
            };

            // Value used to track how many movies match the
            // supplied criteria.
            var moviesFound = 0;

            Search search = movieTable.Query(config);
            do
            {
                var movieList = await search.GetNextSetAsync();
                moviesFound += movieList.Count;

                foreach (var movie in movieList)
                {
                    DisplayDocument(movie);
                }
            }
            while (!search.IsDone);

            return moviesFound;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [Query](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/Query) in *AWS SDK for \.NET API Reference*\. 

### Scan a table<a name="dynamodb_Scan_csharp_topic"></a>

The following code example shows how to scan a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        public static async Task<int> ScanTableAsync(
            AmazonDynamoDBClient client,
            string tableName,
            int startYear,
            int endYear)
        {
            var request = new ScanRequest
            {
                TableName = tableName,
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                  { "#yr", "year" },
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":y_a", new AttributeValue { N = startYear.ToString() } },
                    { ":y_z", new AttributeValue { N = endYear.ToString() } },
                },
                FilterExpression = "#yr between :y_a and :y_z",
                ProjectionExpression = "#yr, title, info.actors[0], info.directors, info.running_time_secs",
            };

            // Keep track of how many movies were found.
            int foundCount = 0;

            var response = new ScanResponse();
            do
            {
                response = await client.ScanAsync(request);
                foundCount += response.Items.Count;
                response.Items.ForEach(i => DisplayItem(i));
            }
            while (response.LastEvaluatedKey.Count > 1);
            return foundCount;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [Scan](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/Scan) in *AWS SDK for \.NET API Reference*\. 

### Update an item in a table<a name="dynamodb_UpdateItem_csharp_topic"></a>

The following code example shows how to update an item in a DynamoDB table\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Updates an existing item in the movies table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing information for
        /// the movie to update.</param>
        /// <param name="newInfo">A MovieInfo object that contains the
        /// information that will be changed.</param>
        /// <param name="tableName">The name of the table that contains the movie.</param>
        /// <returns>A Boolean value that indicates the success of the operation.</returns>
        public static async Task<bool> UpdateItemAsync(
            AmazonDynamoDBClient client,
            Movie newMovie,
            MovieInfo newInfo,
            string tableName)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };
            var updates = new Dictionary<string, AttributeValueUpdate>
            {
                ["info.plot"] = new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = newInfo.Plot },
                },

                ["info.rating"] = new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { N = newInfo.Rank.ToString() },
                },
            };

            var request = new UpdateItemRequest
            {
                AttributeUpdates = updates,
                Key = key,
                TableName = tableName,
            };

            var response = await client.UpdateItemAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [UpdateItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/UpdateItem) in *AWS SDK for \.NET API Reference*\. 

### Write a batch of items<a name="dynamodb_BatchWriteItem_csharp_topic"></a>

The following code example shows how to write a batch of DynamoDB items\.

**AWS SDK for \.NET**  
Writes a batch of items to the movie table\.  

```
        /// <summary>
        /// Loads the contents of a JSON file into a list of movies to be
        /// added to the DynamoDB table.
        /// </summary>
        /// <param name="movieFileName">The full path to the JSON file.</param>
        /// <returns>A generic list of movie objects.</returns>
        public static List<Movie> ImportMovies(string movieFileName)
        {
            if (!File.Exists(movieFileName))
            {
                return null;
            }

            using var sr = new StreamReader(movieFileName);
            string json = sr.ReadToEnd();
            var allMovies = JsonConvert.DeserializeObject<List<Movie>>(json);

            // Now return the first 250 entries.
            return allMovies.GetRange(0, 250);
        }

        /// <summary>
        /// Writes 250 items to the movie table.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="movieFileName">A string containing the full path to
        /// the JSON file containing movie data.</param>
        /// <returns>A long integer value representing the number of movies
        /// imported from the JSON file.</returns>
        public static async Task<long> BatchWriteItemsAsync(
            AmazonDynamoDBClient client,
            string movieFileName)
        {
            var movies = ImportMovies(movieFileName);
            if (movies is null)
            {
                Console.WriteLine("Couldn't find the JSON file with movie data.");
                return 0;
            }

            var context = new DynamoDBContext(client);

            var bookBatch = context.CreateBatchWrite<Movie>();
            bookBatch.AddPutItems(movies);

            Console.WriteLine("Adding imported movies to the table.");
            await bookBatch.ExecuteAsync();

            return movies.Count;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+  For API details, see [BatchWriteItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/BatchWriteItem) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w150aac21c18b9c17c15"></a>

### Get started using tables, items, and queries<a name="dynamodb_Scenario_GettingStartedMovies_csharp_topic"></a>

The following code example shows how to:
+ Create a table that can hold movie data\.
+ Put, get, and update a single movie in the table\.
+ Write movie data to the table from a sample JSON file\.
+ Query for movies that were released in a given year\.
+ Scan for movies that were released in a range of years\.
+ Delete a movie from the table\.
+ Delete the table\.

**AWS SDK for \.NET**  
  

```
/// <summary>
/// This example application performs the following basic Amazon DynamoDB
/// functions:
///
///     CreateTableAsync
///     PutItemAsync
///     UpdateItemAsync
///     BatchWriteItemAsync
///     GetItemAsync
///     DeleteItemAsync
///     Query
///     Scan
///     DeleteItemAsync
///
/// The code in this example uses the AWS SDK for .NET version 3.7 and .NET 5.
/// Before you run this example, download 'movies.json' from
/// https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.Js.02.html,
/// and put it in the same folder as the example.
/// </summary>
namespace DynamoDB_Basics_Scenario
{
    using System;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;

    public class DynamoDB_Basics
    {
        // Separator for the console display.
        private static readonly string SepBar = new('-', 80);

        public static async Task Main()
        {
            var client = new AmazonDynamoDBClient();

            var tableName = "movie_table";
            var movieFileName = "moviedata.json";

            DisplayInstructions();

            // Create a new table and wait for it to be active.
            Console.WriteLine($"Creating the new table: {tableName}");

            var success = await DynamoDbMethods.CreateMovieTableAsync(client, tableName);

            if (success)
            {
                Console.WriteLine($"\nTable: {tableName} successfully created.");
            }
            else
            {
                Console.WriteLine($"\nCould not create {tableName}.");
            }

            WaitForEnter();

            // Add a single new movie to the table.
            var newMovie = new Movie
            {
                Year = 2021,
                Title = "Spider-Man: No Way Home",
            };

            success = await DynamoDbMethods.PutItemAsync(client, newMovie, tableName);
            if (success)
            {
                Console.WriteLine($"Added {newMovie.Title} to the table.");
            }
            else
            {
                Console.WriteLine("Could not add movie to table.");
            }

            WaitForEnter();

            // Update the new movie by adding a plot and rank.
            var newInfo = new MovieInfo
            {
                Plot = "With Spider-Man's identity now revealed, Peter asks" +
                "Doctor Strange for help. When a spell goes wrong, dangerous" +
                "foes from other worlds start to appear, forcing Peter to" +
                "discover what it truly means to be Spider-Man.",
                Rank = 9,
            };

            success = await DynamoDbMethods.UpdateItemAsync(client, newMovie, newInfo, tableName);
            if (success)
            {
                Console.WriteLine($"Successfully updated the movie: {newMovie.Title}");
            }
            else
            {
                Console.WriteLine("Could not update the movie.");
            }

            WaitForEnter();

            // Add a batch of movies to the DynamoDB table from a list of
            // movies in a JSON file.
            var itemCount = await DynamoDbMethods.BatchWriteItemsAsync(client, movieFileName);
            Console.WriteLine($"Added {itemCount} movies to the table.");

            WaitForEnter();

            // Get a movie by key. (partition + sort)
            var lookupMovie = new Movie
            {
                Title = "Jurassic Park",
                Year = 1993,
            };

            Console.WriteLine("Looking for the movie \"Spider-Man: No Way Home\".");
            var item = await DynamoDbMethods.GetItemAsync(client, lookupMovie, tableName);
            if (item.Count > 0)
            {
                DynamoDbMethods.DisplayItem(item);
            }
            else
            {
                Console.WriteLine($"Couldn't find {lookupMovie.Title}");
            }

            WaitForEnter();

            // Delete a movie.
            var movieToDelete = new Movie
            {
                Title = "The Town",
                Year = 2010,
            };

            success = await DynamoDbMethods.DeleteItemAsync(client, tableName, movieToDelete);

            if (success)
            {
                Console.WriteLine($"Successfully deleted {movieToDelete.Title}.");
            }
            else
            {
                Console.WriteLine($"Could not delete {movieToDelete.Title}.");
            }

            WaitForEnter();

            // Use Query to find all the movies released in 2010.
            int findYear = 2010;
            Console.WriteLine($"Movies released in {findYear}");
            var queryCount = await DynamoDbMethods.QueryMoviesAsync(client, tableName, findYear);
            Console.WriteLine($"Found {queryCount} movies released in {findYear}");

            WaitForEnter();

            // Use Scan to get a list of movies from 2001 to 2011.
            int startYear = 2001;
            int endYear = 2011;
            var scanCount = await DynamoDbMethods.ScanTableAsync(client, tableName, startYear, endYear);
            Console.WriteLine($"Found {scanCount} movies released between {startYear} and {endYear}");

            WaitForEnter();

            // Delete the table.
            success = await DynamoDbMethods.DeleteTableAsync(client, tableName);

            if (success)
            {
                Console.WriteLine($"Successfully deleted {tableName}");
            }
            else
            {
                Console.WriteLine($"Could not delete {tableName}");
            }

            Console.WriteLine("The DynamoDB Basics example application is done.");

            WaitForEnter();
        }

        /// <summary>
        /// Displays the description of the application on the console.
        /// </summary>
        private static void DisplayInstructions()
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write(new string(' ', 28));
            Console.WriteLine("DynamoDB Basics Example");
            Console.WriteLine(SepBar);
            Console.WriteLine("This demo application shows the basics of using DynamoDB with the AWS SDK for");
            Console.WriteLine(".NET version 3.7 and .NET Core 5.");
            Console.WriteLine(SepBar);
            Console.WriteLine("The application does the following:");
            Console.WriteLine("\t1. Creates a table with partition: year and sort:title.");
            Console.WriteLine("\t2. Adds a single movie to the table.");
            Console.WriteLine("\t3. Adds movies to the table from moviedata.json.");
            Console.WriteLine("\t4. Updates the rating and plot of the movie that was just added.");
            Console.WriteLine("\t5. Gets a movie using its key (partition + sort).");
            Console.WriteLine("\t6. Deletes a movie.");
            Console.WriteLine("\t7. Uses QueryAsync to return all movies released in a given year.");
            Console.WriteLine("\t8. Uses ScanAsync to return all movies released within a range of years.");
            Console.WriteLine("\t9. Finally, it deletes the table that was just created.");
            WaitForEnter();
        }

        /// <summary>
        /// Simple method to wait for the Enter key to be pressed.
        /// </summary>
        private static void WaitForEnter()
        {
            Console.WriteLine("\nPress <Enter> to continue.");
            Console.WriteLine(SepBar);
            _ = Console.ReadLine();
        }
    }
}
```
Creates a table to contain movie data\.  

```
        /// <summary>
        /// Creates a new Amazon DynamoDB table and then waits for the new
        /// table to become active.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="tableName">The name of the table to create.</param>
        /// <returns>A Boolean value indicating the success of the operation.</returns>
        public static async Task<bool> CreateMovieTableAsync(AmazonDynamoDBClient client, string tableName)
        {
            var response = await client.CreateTableAsync(new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "title",
                        AttributeType = "S",
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "year",
                        AttributeType = "N",
                    },
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName = "year",
                        KeyType = "HASH",
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "title",
                        KeyType = "RANGE",
                    },
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5,
                },
            });

            // Wait until the table is ACTIVE and then report success.
            Console.Write("Waiting for table to become active...");

            var request = new DescribeTableRequest
            {
                TableName = response.TableDescription.TableName,
            };

            TableStatus status;

            int sleepDuration = 2000;

            do
            {
                System.Threading.Thread.Sleep(sleepDuration);

                var describeTableResponse = await client.DescribeTableAsync(request);
                status = describeTableResponse.Table.TableStatus;

                Console.Write(".");
            }
            while (status != "ACTIVE");

            return status == TableStatus.ACTIVE;
        }
```
Adds a single movie to the table\.  

```
        /// <summary>
        /// Adds a new item to the table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing informtation for
        /// the movie to add to the table.</param>
        /// <param name="tableName">The name of the table where the item will be added.</param>
        /// <returns>A Boolean value that indicates the results of adding the item.</returns>
        public static async Task<bool> PutItemAsync(AmazonDynamoDBClient client, Movie newMovie, string tableName)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };

            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item,
            };

            var response = await client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
Updates a single item in a table\.  

```
        /// <summary>
        /// Updates an existing item in the movies table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing information for
        /// the movie to update.</param>
        /// <param name="newInfo">A MovieInfo object that contains the
        /// information that will be changed.</param>
        /// <param name="tableName">The name of the table that contains the movie.</param>
        /// <returns>A Boolean value that indicates the success of the operation.</returns>
        public static async Task<bool> UpdateItemAsync(
            AmazonDynamoDBClient client,
            Movie newMovie,
            MovieInfo newInfo,
            string tableName)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };
            var updates = new Dictionary<string, AttributeValueUpdate>
            {
                ["info.plot"] = new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = newInfo.Plot },
                },

                ["info.rating"] = new AttributeValueUpdate
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { N = newInfo.Rank.ToString() },
                },
            };

            var request = new UpdateItemRequest
            {
                AttributeUpdates = updates,
                Key = key,
                TableName = tableName,
            };

            var response = await client.UpdateItemAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
Retrieves a single item from the movie table\.  

```
        /// <summary>
        /// Gets information about an existing movie from the table.
        /// </summary>
        /// <param name="client">An initialized Amazon DynamoDB client object.</param>
        /// <param name="newMovie">A Movie object containing information about
        /// the movie to retrieve.</param>
        /// <param name="tableName">The name of the table containing the movie.</param>
        /// <returns>A Dictionary object containing information about the item
        /// retrieved.</returns>
        public static async Task<Dictionary<string, AttributeValue>> GetItemAsync(AmazonDynamoDBClient client, Movie newMovie, string tableName)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };

            var request = new GetItemRequest
            {
                Key = key,
                TableName = tableName,
            };

            var response = await client.GetItemAsync(request);
            return response.Item;
        }
```
Writes a batch of items to the movie table\.  

```
        /// <summary>
        /// Loads the contents of a JSON file into a list of movies to be
        /// added to the DynamoDB table.
        /// </summary>
        /// <param name="movieFileName">The full path to the JSON file.</param>
        /// <returns>A generic list of movie objects.</returns>
        public static List<Movie> ImportMovies(string movieFileName)
        {
            if (!File.Exists(movieFileName))
            {
                return null;
            }

            using var sr = new StreamReader(movieFileName);
            string json = sr.ReadToEnd();
            var allMovies = JsonConvert.DeserializeObject<List<Movie>>(json);

            // Now return the first 250 entries.
            return allMovies.GetRange(0, 250);
        }

        /// <summary>
        /// Writes 250 items to the movie table.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="movieFileName">A string containing the full path to
        /// the JSON file containing movie data.</param>
        /// <returns>A long integer value representing the number of movies
        /// imported from the JSON file.</returns>
        public static async Task<long> BatchWriteItemsAsync(
            AmazonDynamoDBClient client,
            string movieFileName)
        {
            var movies = ImportMovies(movieFileName);
            if (movies is null)
            {
                Console.WriteLine("Couldn't find the JSON file with movie data.");
                return 0;
            }

            var context = new DynamoDBContext(client);

            var bookBatch = context.CreateBatchWrite<Movie>();
            bookBatch.AddPutItems(movies);

            Console.WriteLine("Adding imported movies to the table.");
            await bookBatch.ExecuteAsync();

            return movies.Count;
        }
```
Deletes a single item from the table\.  

```
        /// <summary>
        /// Deletes a single item from a DynamoDB table.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="tableName">The name of the table from which the item
        /// will be deleted.</param>
        /// <param name="movieToDelete">A movie object containing the title and
        /// year of the movie to delete.</param>
        /// <returns>A Boolean value indicating the success or failure of the
        /// delete operation.</returns>
        public static async Task<bool> DeleteItemAsync(
            AmazonDynamoDBClient client,
            string tableName,
            Movie movieToDelete)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = movieToDelete.Title },
                ["year"] = new AttributeValue { N = movieToDelete.Year.ToString() },
            };

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = key,
            };

            var response = await client.DeleteItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
Queries the table for movies released in a particular year\.  

```
        /// <summary>
        /// Queries the table for movies released in a particular year and
        /// then displays the information for the movies returned.
        /// </summary>
        /// <param name="client">The initialized DynamoDB client object.</param>
        /// <param name="tableName">The name of the table to query.</param>
        /// <param name="year">The release year for which we want to
        /// view movies.</param>
        /// <returns>The number of movies that match the query.</returns>
        public static async Task<int> QueryMoviesAsync(AmazonDynamoDBClient client, string tableName, int year)
        {
            var movieTable = Table.LoadTable(client, tableName);
            var filter = new QueryFilter("year", QueryOperator.Equal, year);

            Console.WriteLine("\nFind movies released in: {year}:");

            var config = new QueryOperationConfig()
            {
                Limit = 10, // 10 items per page.
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string>
                {
                  "title",
                  "year",
                },
                ConsistentRead = true,
                Filter = filter,
            };

            // Value used to track how many movies match the
            // supplied criteria.
            var moviesFound = 0;

            Search search = movieTable.Query(config);
            do
            {
                var movieList = await search.GetNextSetAsync();
                moviesFound += movieList.Count;

                foreach (var movie in movieList)
                {
                    DisplayDocument(movie);
                }
            }
            while (!search.IsDone);

            return moviesFound;
        }
```
Scans the table for movies released in a range of years\.  

```
        public static async Task<int> ScanTableAsync(
            AmazonDynamoDBClient client,
            string tableName,
            int startYear,
            int endYear)
        {
            var request = new ScanRequest
            {
                TableName = tableName,
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                  { "#yr", "year" },
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":y_a", new AttributeValue { N = startYear.ToString() } },
                    { ":y_z", new AttributeValue { N = endYear.ToString() } },
                },
                FilterExpression = "#yr between :y_a and :y_z",
                ProjectionExpression = "#yr, title, info.actors[0], info.directors, info.running_time_secs",
            };

            // Keep track of how many movies were found.
            int foundCount = 0;

            var response = new ScanResponse();
            do
            {
                response = await client.ScanAsync(request);
                foundCount += response.Items.Count;
                response.Items.ForEach(i => DisplayItem(i));
            }
            while (response.LastEvaluatedKey.Count > 1);
            return foundCount;
        }
```
Deletes the movie table\.  

```
        public static async Task<bool> DeleteTableAsync(AmazonDynamoDBClient client, string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName,
            };

            var response = await client.DeleteTableAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Table {response.TableDescription.TableName} successfully deleted.");
                return true;
            }
            else
            {
                Console.WriteLine("Could not delete table.");
                return false;
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/dynamodb#code-examples)\. 
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [BatchWriteItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/BatchWriteItem)
  + [CreateTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/CreateTable)
  + [DeleteItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/DeleteItem)
  + [DeleteTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/DeleteTable)
  + [DescribeTable](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/DescribeTable)
  + [GetItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/GetItem)
  + [PutItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/PutItem)
  + [Query](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/Query)
  + [Scan](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/Scan)
  + [UpdateItem](https://docs.aws.amazon.com/goto/DotNetSDKV3/dynamodb-2012-08-10/UpdateItem)