# Lambda examples using AWS SDK for \.NET<a name="csharp_lambda_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with Lambda\.

*Actions* are code excerpts that show you how to call individual Lambda functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple Lambda functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w2aac21c17c13c35c13)
+ [Scenarios](#w2aac21c17c13c35c15)

## Actions<a name="w2aac21c17c13c35c13"></a>

### Create a function<a name="lambda_CreateFunction_csharp_topic"></a>

The following code example shows how to create a Lambda function\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
        /// <summary>
        /// Creates a new Lambda function.
        /// </summary>
        /// <param name="client">The initialized Lambda client object.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="s3Bucket">The S3 bucket where the zip file containing
        /// the code is located.</param>
        /// <param name="s3Key">The Amazon S3 key of the zip file.</param>
        /// <param name="role">A role with the appropriate Lambda
        /// permissions.</param>
        /// <param name="handler">The name of the handler function.</param>
        /// <returns>The Amazon Resource Name (ARN) of the newly created
        /// Lambda function.</returns>
        public async Task<string> CreateLambdaFunction(
            AmazonLambdaClient client,
            string functionName,
            string s3Bucket,
            string s3Key,
            string role,
            string handler)
        {
            var functionCode = new FunctionCode
            {
                S3Bucket = s3Bucket,
                S3Key = s3Key,
            };

            var createFunctionRequest = new CreateFunctionRequest
            {
                FunctionName = functionName,
                Description = "Created by the Lambda .NET API",
                Code = functionCode,
                Handler = handler,
                Runtime = Runtime.Dotnet6,
                Role = role,
            };

            var reponse = await client.CreateFunctionAsync(createFunctionRequest);
            return reponse.FunctionArn;
        }
```
+  For API details, see [CreateFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/CreateFunction) in *AWS SDK for \.NET API Reference*\. 

### Delete a function<a name="lambda_DeleteFunction_csharp_topic"></a>

The following code example shows how to delete a Lambda function\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
        /// <summary>
        /// Deletes an AWS Lambda function.
        /// </summary>
        /// <param name="client">An initialized Lambda client object.</param>
        /// <param name="functionName">The name of the Lambda function to
        /// delete.</param>
        /// <returns>A Boolean value that indicates where the function was
        /// successfully deleted.</returns>
        public async Task<bool> DeleteLambdaFunction(AmazonLambdaClient client, string functionName)
        {
            var request = new DeleteFunctionRequest
            {
                FunctionName = functionName,
            };

            var response = await client.DeleteFunctionAsync(request);

            // A return value of NoContent means that the request was processed
            // (in this case, the function was deleted, and the return value
            // is intentionally blank.
            return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }
```
+  For API details, see [DeleteFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/DeleteFunction) in *AWS SDK for \.NET API Reference*\. 

### Get a function<a name="lambda_GetFunction_csharp_topic"></a>

The following code example shows how to get a Lambda function\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
        /// <summary>
        /// Gets information about a Lambda function.
        /// </summary>
        /// <param name="client">The initialized Lambda client object.</param>
        /// <param name="functionName">The name of the Lambda function for
        /// which to retrieve information.</param>
        /// <returns>A System Threading Task.</returns>
        public async Task<FunctionConfiguration> GetFunction(AmazonLambdaClient client, string functionName)
        {
            var functionRequest = new GetFunctionRequest
            {
                FunctionName = functionName,
            };

            var response = await client.GetFunctionAsync(functionRequest);
            return response.Configuration;
        }
```
+  For API details, see [GetFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/GetFunction) in *AWS SDK for \.NET API Reference*\. 

### Invoke a function<a name="lambda_Invoke_csharp_topic"></a>

The following code example shows how to invoke a Lambda function\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
    using System.Threading.Tasks;
    using Amazon.Lambda;
    using Amazon.Lambda.Model;

    /// <summary>
    /// Shows how to invoke an existing Amazon Lambda Function from a C#
    /// application. The example was created using the AWS SDK for .NET and
    /// .NET Core 5.0.
    /// </summary>
    public class InvokeFunction
    {
        /// <summary>
        /// Initializes the Lambda client and then invokes the Lambda Function
        /// called "CreateDDBTable" with the parameter "\"DDBWorkTable\"" to
        /// create the table called DDBWorkTable.
        /// </summary>
        public static async Task Main()
        {
            IAmazonLambda client = new AmazonLambdaClient();
            string functionName = "CreateDDBTable";
            string invokeArgs = "\"DDBWorkTable\"";

            var response = await client.InvokeAsync(
                new InvokeRequest
                {
                    FunctionName = functionName,
                    Payload = invokeArgs,
                    InvocationType = "Event",
                });
        }
    }


        /// <summary>
        /// Invokes a Lambda function.
        /// </summary>
        /// <param name="client">An initialized Lambda client object.</param>
        /// <param name="functionName">The name of the Lambda function to
        /// invoke.</param>
        /// <returns>A System Threading Task.</returns>
        public async Task<string> InvokeFunctionAsync(
            AmazonLambdaClient client,
            string functionName,
            string parameters)
        {
            var payload = parameters;
            var request = new InvokeRequest
            {
                FunctionName = functionName,
                Payload = payload,
            };

            var response = await client.InvokeAsync(request);
            MemoryStream stream = response.Payload;
            string returnValue = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            return returnValue;
        }
```
+  For API details, see [Invoke](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/Invoke) in *AWS SDK for \.NET API Reference*\. 

### List functions<a name="lambda_ListFunctions_csharp_topic"></a>

The following code example shows how to list Lambda functions\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.Lambda;
    using Amazon.Lambda.Model;

    /// <summary>
    /// This example shows two ways to list the AWS Lambda functions you have
    /// created for your account. It will only list the functions within one
    /// AWS Region at a time, however, so you need to pass the AWS Region you
    /// are interested in to the Lambda client object constructor. This example
    /// was created with the AWS SDK for .NET version 3.7 and .NET Core 5.0.
    /// </summary>
    public class ListFunctions
    {
        public static async Task Main()
        {
            // If the AWS Region you are interested in listing is the same as
            // the AWS Region defined for the default user, you don't have to
            // supply the RegionEndpoint constant to the constructor.
            IAmazonLambda client = new AmazonLambdaClient(RegionEndpoint.USEast2);

            // First use the ListFunctionsAsync method.
            var functions1 = await ListFunctionsAsync(client);

            DisplayFunctionList(functions1);

            // Get the list again useing a Lambda client paginator.
            var functions2 = await ListFunctionsPaginatorAsync(client);

            DisplayFunctionList(functions2);
        }

        /// <summary>
        /// Calls the asynchronous ListFunctionsAsync method of the Lambda
        /// client to retrieve the list of functions in the AWS Region with
        /// which the Lambda client was initialized.
        /// </summary>
        /// <param name="client">The initialized Lambda client which will be
        /// used to retrieve the list of Lambda functions.</param>
        /// <returns>A list of Lambda functions configuration information.</returns>
        public static async Task<List<FunctionConfiguration>> ListFunctionsAsync(IAmazonLambda client)
        {
            // Get the list of functions. The response will have a property
            // called Functions, a list of information about the Lambda
            // functions defined on your account in the specified Region.
            var response = await client.ListFunctionsAsync();

            return response.Functions;
        }


        /// <summary>
        /// Uses a Lambda paginator to retrieve the list of functions in the
        /// AWS Region with which the Lambda client was initialized.
        /// </summary>
        /// <param name="client">The initialized Lambda client which will be
        /// used to retrieve the list of Lambda functions.</param>
        /// <returns>A list of Lambda functions configuration information.</returns>
        public static async Task<List<FunctionConfiguration>> ListFunctionsPaginatorAsync(IAmazonLambda client)
        {
            Console.WriteLine("\nNow let's show the list using a paginator.\n");

            // Get the list of functions using a paginator.
            var paginator = client.Paginators.ListFunctions(new ListFunctionsRequest());

            // Defined return a list of function information to the caller
            // for display using the DisplayFunctionList method.
            var functions = new List<FunctionConfiguration>();

            await foreach (var resp in paginator.Responses)
            {
                resp.Functions
                    .ForEach(f => functions.Add(f));
            }

            return functions;
        }


        /// <summary>
        /// Displays the details of each function in the list of functions
        /// passed to the method.
        /// </summary>
        /// <param name="functions">A list of FunctionConfiguration objects.</param>
        public static void DisplayFunctionList(List<FunctionConfiguration> functions)
        {
            // Display a list of the Lambda functions on the console.
            functions
                .ForEach(f => Console.WriteLine($"{f.FunctionName}\t{f.Handler}"));
        }
    }


        /// <summary>
        /// Gets a list of Lambda functions.
        /// </summary>
        /// <param name="client">The initialized Lambda client object.</param>
        /// <returns>A list of FunctionConfiguration objects.</returns>
        public async Task<List<FunctionConfiguration>> ListFunctions(AmazonLambdaClient client)
        {
            var reponse = await client.ListFunctionsAsync();
            var functionList = reponse.Functions;
            return functionList;
        }
```
List functions using a paginator\.  

```
        /// <summary>
        /// Uses a Lambda paginator to retrieve the list of functions in the
        /// AWS Region with which the Lambda client was initialized.
        /// </summary>
        /// <param name="client">The initialized Lambda client which will be
        /// used to retrieve the list of Lambda functions.</param>
        /// <returns>A list of Lambda functions configuration information.</returns>
        public static async Task<List<FunctionConfiguration>> ListFunctionsPaginatorAsync(IAmazonLambda client)
        {
            Console.WriteLine("\nNow let's show the list using a paginator.\n");

            // Get the list of functions using a paginator.
            var paginator = client.Paginators.ListFunctions(new ListFunctionsRequest());

            // Defined return a list of function information to the caller
            // for display using the DisplayFunctionList method.
            var functions = new List<FunctionConfiguration>();

            await foreach (var resp in paginator.Responses)
            {
                resp.Functions
                    .ForEach(f => functions.Add(f));
            }

            return functions;
        }
```
+  For API details, see [ListFunctions](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/ListFunctions) in *AWS SDK for \.NET API Reference*\. 

### Update function code<a name="lambda_UpdateFunctionCode_csharp_topic"></a>

The following code example shows how to update Lambda function code\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
        /// <summary>
        /// Updates an existing Lambda function.
        /// </summary>
        /// <param name="client">An initialized Lambda client object.</param>
        /// <param name="functionName">The name of the Lambda function to update.</param>
        /// <param name="bucketName">The bucket where the zip file containing
        /// the Lambda function code is stored.</param>
        /// <param name="key">The key name of the source code file.</param>
        /// <returns>A System Threading Task.</returns>
        public async Task UpdateFunctionCode(
            AmazonLambdaClient client,
            string functionName,
            string bucketName,
            string key)
        {
            var functionCodeRequest = new UpdateFunctionCodeRequest
            {
                FunctionName = functionName,
                Publish = true,
                S3Bucket = bucketName,
                S3Key = key,
            };

            var response = await client.UpdateFunctionCodeAsync(functionCodeRequest);
            Console.WriteLine($"The Function was last modified at {response.LastModified}.");
        }
```
+  For API details, see [UpdateFunctionCode](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/UpdateFunctionCode) in *AWS SDK for \.NET API Reference*\. 

### Update function configuration<a name="lambda_UpdateFunctionConfiguration_csharp_topic"></a>

The following code example shows how to update Lambda function configuration\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
        public async Task<bool> UpdateFunctionConfigurationAsync(
            AmazonLambdaClient client,
            string functionName,
            string functionHandler,
            Dictionary<string, string> environmentVariables)
        {
            var request = new UpdateFunctionConfigurationRequest
            {
                Handler = functionHandler,
                FunctionName = functionName,
                Environment = new Amazon.Lambda.Model.Environment { Variables = environmentVariables },
            };

            var response = await client.UpdateFunctionConfigurationAsync(request);

            Console.WriteLine(response.LastModified);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
```
+  For API details, see [UpdateFunctionConfiguration](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/UpdateFunctionConfiguration) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w2aac21c17c13c35c15"></a>

### Get started with functions<a name="lambda_Scenario_GettingStartedFunctions_csharp_topic"></a>

The following code example shows how to:
+ Create an AWS Identity and Access Management \(IAM\) role that grants Lambda permission to write to logs\.
+ Create a Lambda function and upload handler code\.
+ Invoke the function with a single parameter and get results\.
+ Update the function code and configure its Lambda environment with an environment variable\.
+ Invoke the function with new parameters and get results\. Display the execution log that's returned from the invocation\.
+ List the functions for your account\.
+ Delete the IAM role and the Lambda function\.

For more information, see [Create a Lambda function with the console](https://docs.aws.amazon.com/lambda/latest/dg/getting-started-create-function.html)\.

**AWS SDK for \.NET**  
 There's more on GitHub\. Find the complete example and learn how to set up and run in the [AWS Code Examples Repository](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/Lambda#code-examples)\. 
  

```
global using Amazon;
global using Amazon.Lambda;
global using Amazon.Lambda.Model;
global using Amazon.IdentityManagement;
global using Amazon.IdentityManagement.Model;
global using Lambda_Basics;
global using Microsoft.Extensions.Configuration;


// The following variables will be loaded from a configuration file:
//
//   functionName - The name of the Lambda function.
//   roleName - The IAM service role that has Lambda permissions.
//   handler - The fully qualified method name (for example,
//       example.Handler::handleRequest).
//   bucketName - The Amazon Simple Storage Service (Amazon S3) bucket name
//       that contains the .zip or .jar used to update the Lambda function's code.
//   key - The Amazon S3 key name that represents the .zip or .jar (for
//       example, LambdaHello-1.0-SNAPSHOT.jar).
//   keyUpdate - The Amazon S3 key name that represents the updated .zip (for
//      example, "updated-function.zip").

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("settings.json") // Load test settings from JSON file.
    .AddJsonFile("settings.local.json",
    true) // Optionally load local settings.
.Build();

string functionName = configuration["FunctionName"];
string roleName = configuration["RoleName"];
string policyDocument = "{" +
    " \"Version\": \"2012-10-17\"," +
    " \"Statement\": [ " +
    "    {" +
    "        \"Effect\": \"Allow\"," +
    "        \"Principal\": {" +
    "            \"Service\": \"lambda.amazonaws.com\" " +
    "    }," +
    "        \"Action\": \"sts:AssumeRole\" " +
    "    }" +
    "]" +
"}";

var incrementHandler = configuration["IncrementHandler"];
var calculatorHandler = configuration["CalculatorHandler"];
var bucketName = configuration["BucketName"];
var key = configuration["Key"];
var updateKey = configuration["UpdateKey"];

string sepBar = new('-', 80);

var lambdaClient = new AmazonLambdaClient();
var lambdaMethods = new LambdaMethods();
var lambdaRoleMethods = new LambdaRoleMethods();

ShowOverview();

// Create the policy to use with the Lambda functions and then attach the
// policy to a new role.
var roleArn = await lambdaRoleMethods.CreateLambdaRole(roleName, policyDocument);

Console.WriteLine("Waiting for role to become active.");
System.Threading.Thread.Sleep(10000);

// Create the Lambda function using a zip file stored in an S3 bucket.
Console.WriteLine(sepBar);
Console.WriteLine($"Creating the AWS Lambda function: {functionName}.");
var lambdaArn = await lambdaMethods.CreateLambdaFunction(
    lambdaClient,
    functionName,
    bucketName,
    key,
    roleArn,
    incrementHandler);

Console.WriteLine(sepBar);
Console.WriteLine($"The AWS Lambda ARN is {lambdaArn}");

// Get the Lambda function.
Console.WriteLine($"Getting the {functionName} AWS Lambda function.");
FunctionConfiguration config;
do
{
    config = await lambdaMethods.GetFunction(lambdaClient, functionName);
    Console.Write(".");
}
while (config.State != State.Active);

Console.WriteLine($"\nThe function, {functionName} has been created.");
Console.WriteLine($"The runtime of this Lambda function is {config.Runtime}.");

PressEnter();

// List the Lambda functions.
Console.WriteLine(sepBar);
Console.WriteLine("Listing all Lambda functions.");
var functions = await lambdaMethods.ListFunctions(lambdaClient);
DisplayFunctionList(functions);
Console.WriteLine(sepBar);

Console.WriteLine(sepBar);
Console.WriteLine("Invoke the Lambda increment function.");
string? value;
do
{
    Console.Write("Enter a value to increment: ");
    value = Console.ReadLine();
}
while (value == string.Empty);

string functionParameters = "{" +
    "\"action\": \"increment\", " +
    "\"x\": \"" + value + "\"" +
"}";
var answer = await lambdaMethods.InvokeFunctionAsync(lambdaClient, functionName, functionParameters);
Console.WriteLine($"{value} + 1 = {answer}.");

Console.WriteLine(sepBar);
Console.WriteLine("Now update the Lambda function code.");
await lambdaMethods.UpdateFunctionCode(lambdaClient, functionName, bucketName, updateKey);

do
{
    config = await lambdaMethods.GetFunction(lambdaClient, functionName);
    Console.Write(".");
}
while (config.LastUpdateStatus == LastUpdateStatus.InProgress);

await lambdaMethods.UpdateFunctionConfigurationAsync(
    lambdaClient,
    functionName,
    configuration["CalculatorHandler"],
    new Dictionary<string, string> { { "LOG_LEVEL", "DEBUG" } });

do
{
    config = await lambdaMethods.GetFunction(lambdaClient, functionName);
    Console.Write(".");
}
while (config.LastUpdateStatus == LastUpdateStatus.InProgress);

Console.WriteLine();
Console.WriteLine(sepBar);
Console.WriteLine("Now call the updated function...");

// Get two numbers and an action from the user.
value = string.Empty;
do
{
    Console.Write("Enter the first value: ");
    value = Console.ReadLine();
}
while (value == string.Empty);

string? value2;
do
{
    Console.Write("Enter a second value: ");
    value2 = Console.ReadLine();
}
while (value2 == string.Empty);

string? opSelected;

Console.WriteLine("Select the operation to perform:");
Console.WriteLine("\t1. add");
Console.WriteLine("\t2. subtract");
Console.WriteLine("\t3. multiply");
Console.WriteLine("\t4. divide");
Console.WriteLine("Enter the number (1, 2, 3, or 4) of the operation you want to perform: ");
do
{
    Console.Write("Your choice? ");
    opSelected = Console.ReadLine();
}
while (opSelected == string.Empty);

var operation = (opSelected) switch
{
    "1" => "add",
    "2" => "subtract",
    "3" => "multiply",
    "4" => "divide",
    _ => "add",
};

functionParameters = "{" +
    "\"action\": \"" + operation + "\", " +
    "\"x\": \"" + value + "\"," +
    "\"y\": \"" + value2 + "\"" +
"}";

answer = await lambdaMethods.InvokeFunctionAsync(lambdaClient, functionName, functionParameters);
Console.WriteLine($"The answer when we {operation} the two numbers is: {answer}.");

PressEnter();

// Delete the function created earlier.
Console.WriteLine(sepBar);
Console.WriteLine("Delete the AWS Lambda function.");
var success = await lambdaMethods.DeleteLambdaFunction(lambdaClient, functionName);
if (success)
{
    Console.WriteLine($"The {functionName} function was deleted.");
}
else
{
    Console.WriteLine($"Could not remove the function {functionName}");
}

// Now delete the IAM role created for use with the functions
// created by the application.
success = await lambdaRoleMethods.DeleteLambdaRole(roleName);
if (success)
{
    Console.WriteLine("The role has been successfully removed.");
}
else
{
    Console.WriteLine("Couldn't delete the role.");
}

Console.WriteLine("The Lambda Scenario is now complete.");
PressEnter();

// Displays a formatted list of existing functions returned by the
// LambdaMethods.ListFunctions.
void DisplayFunctionList(List<FunctionConfiguration> functions)
{
    functions.ForEach(functionConfig =>
    {
        Console.WriteLine($"{functionConfig.FunctionName}\t{functionConfig.Description}");
    });
}

// Displays an overview of the application.
void ShowOverview()
{
    Console.WriteLine("Welcome to the AWS Lambda Basics Example");
    Console.WriteLine("Getting started with functions");
    Console.WriteLine(sepBar);
    Console.WriteLine("This scenario performs the following operations:");
    Console.WriteLine("\t 1. Creates an IAM policy that will be used by AWS Lambda.");
    Console.WriteLine("\t 2. Attaches the policy to a new IAM role.");
    Console.WriteLine("\t 3. Creates an AWS Lambda function.");
    Console.WriteLine("\t 4. Gets a specific AWS Lambda function.");
    Console.WriteLine("\t 5. Lists all Lambda functions.");
    Console.WriteLine("\t 6. Invokes the Lambda function.");
    Console.WriteLine("\t 7. Updates the Lambda function's code.");
    Console.WriteLine("\t 8. Updates the Lambda function's configuration.");
    Console.WriteLine("\t 9. Invokes the updated function.");
    Console.WriteLine("\t10. Deletes the Lambda function.");
    Console.WriteLine("\t11. Deletes the IAM role.");
    PressEnter();
}

// Wait for the user to press the Enter key.
void PressEnter()
{
    Console.Write("Press <Enter> to continue.");
    _ = Console.ReadLine();
    Console.WriteLine();
}
```
Define a Lambda handler that increments a number\.  

```
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaIncrement;

public class Function
{

    /// <summary>
    /// A simple function increments the integer parameter.
    /// </summary>
    /// <param name="input">A JSON string containing an action, which must be
    /// "increment" and a string representing the value to increment.</param>
    /// <param name="context">The context object passed by Lambda containing
    /// information about invocation, function, and execution environment.</param>
    /// <returns>A string representing the incremented value of the parameter.</returns>
    public int FunctionHandler(Dictionary<string, string> input, ILambdaContext context)
    {
        if (input["action"] == "increment")
        {
            int inputValue = Convert.ToInt32(input["x"]);
            return inputValue + 1;
        }
        else
        {
            return 0;
        }
    }
}
```
Define a second Lambda handler that performs arithmetic operations\.  

```
using Amazon.Lambda.Core;
using System.Diagnostics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaCalculator;

public class Function
{

    /// <summary>
    /// A simple function that takes two number in string format and performs
    /// the requested arithmetic function.
    /// </summary>
    /// <param name="input">JSON data containing an action, and x and y values.
    /// Valid actions include: add, subtract, multiply, and divide.</param>
    /// <param name="context">The context object passed by Lambda containing
    /// information about invocation, function, and execution environment.</param>
    /// <returns>A string representing the results of the calculation.</returns>
    public int FunctionHandler(Dictionary<string, string> input, ILambdaContext context)
    {
        var action = input["action"];
        int x = Convert.ToInt32(input["x"]);
        int y = Convert.ToInt32(input["y"]);
        int result;
        switch (action)
        {
            case "add":
                result = x + y;
                break;
            case "subtract":
                result = x - y;
                break;
            case "multiply":
                result = x * y;
                break;
            case "divide":
                if (y == 0)
                {
                    Console.Error.WriteLine("Divide by zero error.");
                    result = 0;
                }
                else
                    result = x / y;
                break;
            default:
                Console.Error.WriteLine($"{action} is not a valid operation.");
                result = 0;
                break;
        }
        return result;
    }
}
```
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [CreateFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/CreateFunction)
  + [DeleteFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/DeleteFunction)
  + [GetFunction](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/GetFunction)
  + [Invoke](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/Invoke)
  + [ListFunctions](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/ListFunctions)
  + [UpdateFunctionCode](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/UpdateFunctionCode)
  + [UpdateFunctionConfiguration](https://docs.aws.amazon.com/goto/DotNetSDKV3/lambda-2015-03-31/UpdateFunctionConfiguration)