# Listing AWS Resources using AWS CloudFormation<a name="cloudformation-apis-intro"></a>

The AWS SDK for \.NET supports AWS CloudFormation, which creates and provisions AWS infrastructure deployments predictably and repeatedly\. For more information, see [AWS CloudFormation Getting Started Guide](https://docs.aws.amazon.com/AWSCloudFormation/latest/GettingStartedGuide/)\.

The following example shows how to use the low\-level APIs to list accessible resources in AWS CloudFormation\.

```
// using Amazon.CloudFormation;
// using Amazon.CloudFormation.Model;

var client = new AmazonCloudFormationClient();
var request = new DescribeStacksRequest();
var response = client.DescribeStacks(request);

foreach (var stack in response.Stacks)
{
  Console.WriteLine("Stack: {0}", stack.StackName);
  Console.WriteLine("  Status: {0}", stack.StackStatus);
  Console.WriteLine("  Created: {0}", stack.CreationTime);

  var ps = stack.Parameters;

  if (ps.Any())
  {
    Console.WriteLine("  Parameters:");

    foreach (var p in ps)
    {
      Console.WriteLine("    {0} = {1}", 
        p.ParameterKey, p.ParameterValue);
    }

  }
  
}
```

For related API reference information, see [Amazon\.CloudFormation](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/NCloudFormation.html) and [Amazon\.CloudFormation\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/NCloudFormation.html) in the [AWS SDK for \.NET API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/)\.