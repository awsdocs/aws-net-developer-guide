# Using annotations to write AWS Lambda functions<a name="aws-lambda-annotations"></a>


|  | 
| --- |
| This is prerelease documentation for a service in preview release\. It is subject to change\. | 

When writing Lambda functions, you sometimes need to write a large amount of handler code and update AWS CloudFormation templates, among other tasks\. Lambda Annotations is a framework to help ease these burdens for \.NET 6 Lambda functions, thereby making the experience of writing Lambda feel more natural in C\#\.

As an example of the benefit of using the Lambda Annotations framework, consider the following snippets of code that add two numbers\.

**Without Lambda Annotations**

```
public class Functions
{
    public APIGatewayProxyResponse LambdaMathPlus(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.TryGetValue("x", out var xs))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        if (!request.PathParameters.TryGetValue("y", out var ys))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var x = int.Parse(xs);
        var y = int.Parse(ys);

        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = (x + y).ToString(),
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
    } 
}
```

**With Lambda Annotations**

```
public class Functions
{
    [LambdaFunction]
    [RestApi("/plus/{x}/{y}")]
    public int Plus(int x, int y)
    {
        return x + y;
    }
}
```

As is shown in the example, Lambda Annotations can remove the need for certain boiler plate code\.

For details about how to use the framework as well as additional information, see the following resources:
+ The [aws/aws\-lambda\-dotnet](https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md) GitHub repository\.
+ The [preview blog](http://aws.amazon.com/blogs/developer/introducing-net-annotations-lambda-framework-preview/) for Lambda Annotations\.
+ The [https://www.nuget.org/packages/Amazon.Lambda.Annotations](https://www.nuget.org/packages/Amazon.Lambda.Annotations) NuGet package\.