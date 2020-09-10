--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Display the policy document of an IAM managed policy<a name="iam-policies-display"></a>

This example shows you how to use the AWS SDK for \.NET to display a policy document\. The application creates an IAM client object, finds the default version of the given IAM managed policy, and then displays the policy document in JSON\.

The following sections provide snippets of this example\. The [complete code for the example](#iam-policies-display-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Find the default version](#iam-policies-display-version)
+ [Display the policy document](#iam-policies-display-doc)
+ [Complete code](#iam-policies-display-complete-code)

## Find the default version<a name="iam-policies-display-version"></a>

The following snippet finds the default version of the given IAM policy\.

The example [at the end of this topic](#iam-policies-display-complete-code) shows this snippet in use\.

```
    //
    // Method to determine the default version of an IAM policy
    // Returns a string with the version
    private static async Task<string> GetDefaultVersion(
      IAmazonIdentityManagementService iamClient, string policyArn)
    {
      // Retrieve all the versions of this policy
      string defaultVersion = string.Empty;
      ListPolicyVersionsResponse reponseVersions =
        await iamClient.ListPolicyVersionsAsync(new ListPolicyVersionsRequest{
          PolicyArn = policyArn});

      // Find the default version
      foreach(PolicyVersion version in reponseVersions.Versions)
      {
        if(version.IsDefaultVersion)
        {
          defaultVersion = version.VersionId;
          break;
        }
      }

      return defaultVersion;
    }
```

## Display the policy document<a name="iam-policies-display-doc"></a>

The following snippet displays the policy document in JSON of the given IAM policy\.

The example [at the end of this topic](#iam-policies-display-complete-code) shows this snippet in use\.

```
    //
    // Method to retrieve and display the policy document of an IAM policy
    private static async Task ShowPolicyDocument(
      IAmazonIdentityManagementService iamClient, string policyArn, string defaultVersion)
    {
      // Retrieve the policy document of the default version
      GetPolicyVersionResponse responsePolicy =
        await iamClient.GetPolicyVersionAsync(new GetPolicyVersionRequest{
          PolicyArn = policyArn,
          VersionId = defaultVersion});

      // Display the policy document (in JSON)
      Console.WriteLine($"Version {defaultVersion} of the policy (in JSON format):");
      Console.WriteLine(
        $"{HttpUtility.UrlDecode(responsePolicy.PolicyVersion.Document)}");
    }
```

## Complete code<a name="iam-policies-display-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c21c27c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.IdentityManagement](https://www.nuget.org/packages/AWSSDK.IdentityManagement)

Programming elements:
+ Namespace [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html)

  Class [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html)
+ Namespace [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)

  Class [GetPolicyVersionRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetPolicyVersionRequest.html)

  Class [GetPolicyVersionResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetPolicyVersionResponse.html)

  Class [ListPolicyVersionsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListPolicyVersionsRequest.html)

  Class [ListPolicyVersionsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListPolicyVersionsResponse.html)

  Class [PolicyVersion](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TPolicyVersion.html)

### The code<a name="w4aac19c21c27c19b7b1"></a>

```
using System;
using System.Web;
using System.Threading.Tasks;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

namespace IamDisplayPolicyJson
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      if(args.Length != 1)
      {
        Console.WriteLine("\nUsage: IamDisplayPolicyJson policy-arn");
        Console.WriteLine("   policy-arn: The ARN of the policy to retrieve.");
        return;
      }
      if(!args[0].StartsWith("arn:"))
      {
        Console.WriteLine("\nCould not find policy ARN in the command-line arguments:");
        Console.WriteLine($"{args[0]}");
        return;
      }

      // Create an IAM service client
      var iamClient = new AmazonIdentityManagementServiceClient();

      // Retrieve and display the policy document of the given policy
      string defaultVersion = await GetDefaultVersion(iamClient, args[0]);
      if(string.IsNullOrEmpty(defaultVersion))
        Console.WriteLine($"Could not find the default version for policy {args[0]}.");
      else
        await ShowPolicyDocument(iamClient, args[0], defaultVersion);
    }


    //
    // Method to determine the default version of an IAM policy
    // Returns a string with the version
    private static async Task<string> GetDefaultVersion(
      IAmazonIdentityManagementService iamClient, string policyArn)
    {
      // Retrieve all the versions of this policy
      string defaultVersion = string.Empty;
      ListPolicyVersionsResponse reponseVersions =
        await iamClient.ListPolicyVersionsAsync(new ListPolicyVersionsRequest{
          PolicyArn = policyArn});

      // Find the default version
      foreach(PolicyVersion version in reponseVersions.Versions)
      {
        if(version.IsDefaultVersion)
        {
          defaultVersion = version.VersionId;
          break;
        }
      }

      return defaultVersion;
    }


    //
    // Method to retrieve and display the policy document of an IAM policy
    private static async Task ShowPolicyDocument(
      IAmazonIdentityManagementService iamClient, string policyArn, string defaultVersion)
    {
      // Retrieve the policy document of the default version
      GetPolicyVersionResponse responsePolicy =
        await iamClient.GetPolicyVersionAsync(new GetPolicyVersionRequest{
          PolicyArn = policyArn,
          VersionId = defaultVersion});

      // Display the policy document (in JSON)
      Console.WriteLine($"Version {defaultVersion} of the policy (in JSON format):");
      Console.WriteLine(
        $"{HttpUtility.UrlDecode(responsePolicy.PolicyVersion.Document)}");
    }
  }
}
```