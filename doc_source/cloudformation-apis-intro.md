--------

End of support announcement: [https://aws\.amazon\.com/blogs/developer/announcing\-the\-end\-of\-support\-for\-the\-aws\-sdk\-for\-net\-version\-2/](https://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

 This documentation is for version 2\.0 of the AWS SDK for \.NET\. **For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide) of the AWS SDK for \.NET developer guide instead\.**

--------

# AWS CloudFormation Programming with the AWS SDK for \.NET<a name="cloudformation-apis-intro"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13b9b3b1"></a>

The AWS SDK for \.NET supports AWS CloudFormation, which creates and provision AWS infrastructure deployments predictably and repeatedly\. For more information, see [AWS CloudFormation Getting Started Guide](https://docs.aws.amazon.com/AWSCloudFormation/latest/GettingStartedGuide/)\.

The following information introduces you to the AWS CloudFormation programming models in the the SDK\.

### Programming Models<a name="cloudformation-apis-intro-models"></a>

The the SDK provides two programming models for working with AWS CloudFormation\. These programming models are known as the *low\-level* and *resource* models\. The following information describes these models, how to use them, and why you would want to use them\.

#### Low\-Level APIs<a name="cloudformation-apis-intro-low-level"></a>

The the SDK provides low\-level APIs for programming with AWS CloudFormation\. These low\-level APIs typically consist of sets of matching request\-and\-response objects that correspond to HTTP\-based API calls focusing on their corresponding service\-level constructs\.

The following example shows how to use the low\-level APIs to list accessible resources in AWS CloudFormation:

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

For related API reference information, see `Amazon.CloudFormation` and `Amazon.CloudFormation.Model` in the [sdk\-net\-api\-v2](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/)\.

#### Resource APIs<a name="cloudformation-apis-intro-resource-level"></a>

The the SDK provides the AWS Resource APIs for \.NET for programming with AWS CloudFormation\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with AWS CloudFormation resources as compared to their low\-level API counterparts\. \(For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.\)

The following example shows how to use the AWS Resource APIs for \.NET to list accessible resources in AWS CloudFormation:

```
// using Amazon.CloudFormation.Resources;

var cf = new CloudFormation();

foreach (var stack in cf.GetStacks())
{
  Console.WriteLine("Stack: {0}", stack.Name);
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

For related API reference information, see [Amazon\.CloudFormation\.Resources](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NCloudFormationResourcesNET45.html)\.