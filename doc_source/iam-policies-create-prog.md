--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Creating IAM managed policies programmatically<a name="iam-policies-create-prog"></a>

This example shows you how to use the AWS SDK for \.NET to create an [IAM managed policy](https://docs.aws.amazon.com/IAM/latest/UserGuide/access_policies_managed-vs-inline.html#aws-managed-policies) programmatically\. The application constructs a policy document by using action and statement classes, creates an IAM client object, and then creates the policy\.

The following sections provide snippets of this example\. The [complete code for the example](#iam-policies-create-prog-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Generate the policy document](#iam-policies-create-prog-doc)
+ [Create the policy](#iam-policies-create-prog-create)
+ [Complete code](#iam-policies-create-prog-complete-code)
+ [Additional considerations](#iam-policies-create-prog-additional)

## Generate the policy document<a name="iam-policies-create-prog-doc"></a>

The following snippet generates the policy document programmatically\.

The example [at the end of this topic](#iam-policies-create-prog-complete-code) shows this snippet in use\.

```
    //
    // Method to generate a policy document in JSON
    private static string GeneratePolicyDocumentForTutorial()
    {
      //
      // The S3 part of the policy
      //
      var s3Statement = new Amazon.Auth.AccessControlPolicy.Statement(
        Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
      {
        Id = "DotnetTutorialPolicyS3",
        Actions = new List<ActionIdentifier>{
          new ActionIdentifier("s3:Get*"),
          new ActionIdentifier("s3:List*")},
        Resources = new List<Resource>{
          new Resource("*")}
      };

      //
      // The Polly part of the policy
      //
      var pollyStatement = new Amazon.Auth.AccessControlPolicy.Statement(
        Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
      {
        Id = "DotnetTutorialPolicyPolly",
        Actions = new List<ActionIdentifier>{
          new ActionIdentifier("polly:DescribeVoices"),
          new ActionIdentifier("polly:SynthesizeSpeech")},
        Resources = new List<Resource>{
          new Resource("*")}
      };

      //
      // Putting it all together
      //
      var policy = new Policy
      {
          Id = "DotnetTutorialPolicy",
          Version = "2012-10-17",
          Statements = new List<Amazon.Auth.AccessControlPolicy.Statement>{
            s3Statement,
            pollyStatement}
      };
      return policy.ToJson();
    }
```

## Create the policy<a name="iam-policies-create-prog-create"></a>

The following snippet creates an IAM managed policy with the given name and the generated policy document\.

The example [at the end of this topic](#iam-policies-create-prog-complete-code) shows this snippet in use\.

```
    //
    // Method to create an IAM policy from a given string
    private static async Task<CreatePolicyResponse> CreateManagedPolicy(
      IAmazonIdentityManagementService iamClient, string policyName, string policyDoc)
    {
      return await iamClient.CreatePolicyAsync(new CreatePolicyRequest{
        PolicyName = policyName,
        PolicyDocument = policyDoc});
    }
```

## Complete code<a name="iam-policies-create-prog-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c21c25c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.IdentityManagement](https://www.nuget.org/packages/AWSSDK.IdentityManagement)

Programming elements:
+ Namespace [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html)

  Class [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html)
+ Namespace [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)

  Class [CreatePolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreatePolicyRequest.html)

  Class [CreatePolicyResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreatePolicyResponse.html)
+ Namespace [Amazon\.Auth\.AccessControlPolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Auth/NAuthAccessControlPolicy.html)

  Class [ActionIdentifier](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Auth/TActionIdentifier.html)

  Class [Policy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Auth/TPolicy.html)

  Class [Resource](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Auth/TResource.html)

  Class [Statement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Auth/TStatement.html)

### The code<a name="w4aac19c21c25c19b7b1"></a>

```
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Auth.AccessControlPolicy;

namespace IamCreatePolicyProgrammatically
{
  class Program
  {
    static async Task Main(string[] args)
    {
      if(args.Length != 1)
      {
        Console.WriteLine("\nUsage: IamCreatePolicyProgrammatically policy-name");
        Console.WriteLine("  policy-name: The name you want the new policy to have.");
        return;
      }

      // Create the policy document
      string policyDoc = GeneratePolicyDocumentForTutorial();

      // Create an IAM service client
      var iamClient = new AmazonIdentityManagementServiceClient();

      // Create the new policy
      var response = await CreateManagedPolicy(iamClient, args[0], policyDoc);
      Console.WriteLine($"Policy {response.Policy.PolicyName} has been created.");
      Console.WriteLine($"  Arn: {response.Policy.Arn}");
    }


    //
    // Method to generate a policy document in JSON
    private static string GeneratePolicyDocumentForTutorial()
    {
      //
      // The S3 part of the policy
      //
      var s3Statement = new Amazon.Auth.AccessControlPolicy.Statement(
        Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
      {
        Id = "DotnetTutorialPolicyS3",
        Actions = new List<ActionIdentifier>{
          new ActionIdentifier("s3:Get*"),
          new ActionIdentifier("s3:List*")},
        Resources = new List<Resource>{
          new Resource("*")}
      };

      //
      // The Polly part of the policy
      //
      var pollyStatement = new Amazon.Auth.AccessControlPolicy.Statement(
        Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
      {
        Id = "DotnetTutorialPolicyPolly",
        Actions = new List<ActionIdentifier>{
          new ActionIdentifier("polly:DescribeVoices"),
          new ActionIdentifier("polly:SynthesizeSpeech")},
        Resources = new List<Resource>{
          new Resource("*")}
      };

      //
      // Putting it all together
      //
      var policy = new Policy
      {
          Id = "DotnetTutorialPolicy",
          Version = "2012-10-17",
          Statements = new List<Amazon.Auth.AccessControlPolicy.Statement>{
            s3Statement,
            pollyStatement}
      };
      return policy.ToJson();
    }


    //
    // Method to create an IAM policy from a given string
    private static async Task<CreatePolicyResponse> CreateManagedPolicy(
      IAmazonIdentityManagementService iamClient, string policyName, string policyDoc)
    {
      return await iamClient.CreatePolicyAsync(new CreatePolicyRequest{
        PolicyName = policyName,
        PolicyDocument = policyDoc});
    }
  }
}
```

## Additional considerations<a name="iam-policies-create-prog-additional"></a>
+ You can verify that the policy was created by looking in the [IAM console](https://console.aws.amazon.com/iam/home#/policies)\. In the **Filter policies** drop\-down list, select **Customer managed**\. Delete the policy when you no longer need it\.
+ When creating the policy, this example creates the following policy document programmatically:

  ```
  {
    "Version" : "2012-10-17",
    "Id"  : "DotnetTutorialPolicy",
    "Statement" : [
      {
        "Sid" : "DotnetTutorialPolicyS3",
        "Effect" : "Allow",
        "Action" : [
          "s3:Get*",
          "s3:List*"
        ],
        "Resource" : "*"
      },
      {
        "Sid" : "DotnetTutorialPolicyPolly",
        "Effect": "Allow",
        "Action": [
          "polly:DescribeVoices",
          "polly:SynthesizeSpeech"
        ],
        "Resource": "*"
      }
    ]
  }
  ```

  To contrast this example with the one that [requires a JSON policy document as input](iam-policies-create-json.md), you can use this JSON definition as the input for that example\.