--------

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Listing AWS resources using AWS CloudFormation<a name="cfn-list-resources"></a>

This example shows you how to use the AWS SDK for \.NET to list the resources in AWS CloudFormation stacks\. The example uses the low\-level API\. The application takes no arguments, but simply gathers information for all stacks that are accessible to the user's credentials and then displays information about those stacks\.

## SDK references<a name="w8aac19c17c13b5b1"></a>

NuGet packages:
+ [AWSSDK\.CloudFormation](https://www.nuget.org/packages/AWSSDK.CloudFormation/)

Programming elements:
+ Namespace [Amazon\.CloudFormation](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/NCloudFormation.html)

  Class [AmazonCloudFormationClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TCloudFormationClient.html)
+ Namespace [Amazon\.CloudFormation\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/NCloudFormationModel.html)

  Class [DescribeStackResourcesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TDescribeStackResourcesRequest.html)

  Class [DescribeStackResourcesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TDescribeStackResourcesResponse.html)

  Class [DescribeStacksResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TDescribeStacksResponse.html)

  Class [Stack](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TStack.html)

  Class [StackResource](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TStackResource.html)

  Class [Tag](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudFormation/TTag.html)

```
using System;
using System.Threading.Tasks;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;

namespace CFNListResources
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Create the CloudFormation client
      var cfnClient = new AmazonCloudFormationClient();

      // List the resources for each stack
      await ListResources(cfnClient, await cfnClient.DescribeStacksAsync());
    }

    //
    // Method to list stack resources and other information
    private static async Task ListResources(
      IAmazonCloudFormation cfnClient, DescribeStacksResponse responseDescribeStacks)
    {
      Console.WriteLine("Getting CloudFormation stack information...");

      foreach (Stack stack in responseDescribeStacks.Stacks)
      {
        // Basic information for each stack
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"\nStack: {stack.StackName}");
        Console.WriteLine($"  Status: {stack.StackStatus.Value}");
        Console.WriteLine($"  Created: {stack.CreationTime}");

        // The tags of each stack (etc.)
        if(stack.Tags.Count > 0)
        {
          Console.WriteLine("  Tags:");
          foreach (Tag tag in stack.Tags)
            Console.WriteLine($"    {tag.Key}, {tag.Value}");
        }

        // The resources of each stack
        DescribeStackResourcesResponse responseDescribeResources =
          await cfnClient.DescribeStackResourcesAsync(new DescribeStackResourcesRequest{
            StackName = stack.StackName});
        if(responseDescribeResources.StackResources.Count > 0)
        {
          Console.WriteLine("  Resources:");
          foreach(StackResource resource in responseDescribeResources.StackResources)
            Console.WriteLine($"    {resource.LogicalResourceId}: {resource.ResourceStatus}");
        }
      }
      Console.WriteLine("\n------------------------------------------------");
    }
  }
}
```