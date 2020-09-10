--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Deleting IAM users<a name="iam-users-delete"></a>

This example shows you how use the AWS SDK for \.NET to delete an IAM user\. It first removes resources such as access keys, attached policies, etc\., and then deletes the user\.

The following sections provide snippets of this example\. The [complete code for the example](#iam-users-delete-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Remove items from the user](#iam-users-delete-remove-items)
+ [Delete the user](#iam-users-delete-delete)
+ [Complete code](#iam-users-delete-complete-code)
+ [Additional considerations](#iam-users-delete-additional)

## Remove items from the user<a name="iam-users-delete-remove-items"></a>

The following snippets show examples of items that must be removed from a user before the user can be deleted, items such as managed policies and access keys\.

The example [at the end of this topic](#iam-users-delete-complete-code) shows this snippet in use\.

```
    //
    // Method to detach managed policies from a user
    private static async Task DetachPolicies(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      ListAttachedUserPoliciesResponse responseManagedPolicies =
        await iamClient.ListAttachedUserPoliciesAsync(
          new ListAttachedUserPoliciesRequest{UserName = userName});
      foreach(AttachedPolicyType policy in responseManagedPolicies.AttachedPolicies)
      {
        Console.WriteLine($"\tDetaching policy {policy.PolicyName}");
        await iamClient.DetachUserPolicyAsync(new DetachUserPolicyRequest{
          PolicyArn = policy.PolicyArn,
          UserName = userName});
      }
    }


    //
    // Method to delete access keys from a user
    private static async Task DeleteAccessKeys(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      ListAccessKeysResponse responseAccessKeys =
        await iamClient.ListAccessKeysAsync(
          new ListAccessKeysRequest{UserName = userName});
      foreach(AccessKeyMetadata accessKey in responseAccessKeys.AccessKeyMetadata)
      {
        Console.WriteLine($"\tDeleting Access key {accessKey.AccessKeyId}");
        await iamClient.DeleteAccessKeyAsync(new DeleteAccessKeyRequest{
          UserName = userName,
          AccessKeyId = accessKey.AccessKeyId});
      }
    }
```

## Delete the user<a name="iam-users-delete-delete"></a>

The following snippet calls methods to remove items from a user and then deletes the user\.

The example [at the end of this topic](#iam-users-delete-complete-code) shows this snippet in use\.

```
    //
    // Method to delete a user
    private static async Task DeleteUser(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      Console.WriteLine($"\nDeleting user {userName}...");
      //
      // Remove items from the user
      //
      // Detach any managed policies
      await DetachPolicies(iamClient, userName);

      // Delete any access keys
      await DeleteAccessKeys(iamClient, userName);

      // DeleteLoginProfileAsycn(), DeleteUserPolicyAsync(), etc.
      // See the description of DeleteUserAsync for a full list.

      //
      // Delete the user
      //
      await iamClient.DeleteUserAsync(new DeleteUserRequest(userName));
      Console.WriteLine("Done");
    }
```

## Complete code<a name="iam-users-delete-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c21c21c19b5b1"></a>

NuGet packages:
+ [AWSSDK\.IdentityManagement](https://www.nuget.org/packages/AWSSDK.IdentityManagement)

Programming elements:
+ Namespace [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html)

  Class [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html)
+ Namespace [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)

  Class [AccessKeyMetadata](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TAccessKeyMetadata.html)

  Class [AttachedPolicyType](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TAttachedPolicyType.html)

  Class [DeleteAccessKeyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDeleteAccessKeyRequest.html)

  Class [DeleteUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDeleteUserRequest.html)

  Class [DetachUserPolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDetachUserPolicyRequest.html)

  Class [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html)

  Class [ListAccessKeysResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysResponse.html)

  Class [ListAttachedUserPoliciesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAttachedUserPoliciesRequest.html)

  Class [ListAttachedUserPoliciesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAttachedUserPoliciesResponse.html)

### The code<a name="w4aac19c21c21c19b7b1"></a>

```
using System;
using System.Threading.Tasks;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

namespace IamDeleteUser
{
  class Program
  {
    static async Task Main(string[] args)
    {
      if(args.Length != 1)
      {
        Console.WriteLine("\nUsage: IamDeleteUser user-name");
        Console.WriteLine("   user-name - The name of the user you want to delete.");
        return;
      }

      // Create an IAM service client
      var iamClient = new AmazonIdentityManagementServiceClient();

      // Delete the given user
      await DeleteUser(iamClient, args[0]);

      // Could display a list of the users that are left.
    }

    //
    // Method to delete a user
    private static async Task DeleteUser(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      Console.WriteLine($"\nDeleting user {userName}...");
      //
      // Remove items from the user
      //
      // Detach any managed policies
      await DetachPolicies(iamClient, userName);

      // Delete any access keys
      await DeleteAccessKeys(iamClient, userName);

      // DeleteLoginProfileAsycn(), DeleteUserPolicyAsync(), etc.
      // See the description of DeleteUserAsync for a full list.

      //
      // Delete the user
      //
      await iamClient.DeleteUserAsync(new DeleteUserRequest(userName));
      Console.WriteLine("Done");
    }


    //
    // Method to detach managed policies from a user
    private static async Task DetachPolicies(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      ListAttachedUserPoliciesResponse responseManagedPolicies =
        await iamClient.ListAttachedUserPoliciesAsync(
          new ListAttachedUserPoliciesRequest{UserName = userName});
      foreach(AttachedPolicyType policy in responseManagedPolicies.AttachedPolicies)
      {
        Console.WriteLine($"\tDetaching policy {policy.PolicyName}");
        await iamClient.DetachUserPolicyAsync(new DetachUserPolicyRequest{
          PolicyArn = policy.PolicyArn,
          UserName = userName});
      }
    }


    //
    // Method to delete access keys from a user
    private static async Task DeleteAccessKeys(
      IAmazonIdentityManagementService iamClient, string userName)
    {
      ListAccessKeysResponse responseAccessKeys =
        await iamClient.ListAccessKeysAsync(
          new ListAccessKeysRequest{UserName = userName});
      foreach(AccessKeyMetadata accessKey in responseAccessKeys.AccessKeyMetadata)
      {
        Console.WriteLine($"\tDeleting Access key {accessKey.AccessKeyId}");
        await iamClient.DeleteAccessKeyAsync(new DeleteAccessKeyRequest{
          UserName = userName,
          AccessKeyId = accessKey.AccessKeyId});
      }
    }
  }
}
```

## Additional considerations<a name="iam-users-delete-additional"></a>
+ For information about the resources that must be removed from the user, see the description of the [DeleteUserAsync](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteUserAsyncDeleteUserRequestCancellationToken.html) method, but be sure to use the Async versions of the referenced methods\.
+ You can also see the list of users and the results of this example in the [IAM console](https://console.aws.amazon.com/iam/home#/users)\.