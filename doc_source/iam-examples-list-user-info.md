--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# List IAM Account Information<a name="iam-examples-list-user-info"></a>

The AWS SDK for \.NET supports IAM, which is a web service that enables AWS customers to manage users and user permissions in AWS\.

The following example shows how to list accessible user accounts in IAM\. For each user account, its associated groups, policies, and access key IDs are also listed\.

```
public static void ListUsersAndGroups()
{
  var iamClient = new AmazonIdentityManagementServiceClient();
  var requestUsers = new ListUsersRequest();
  var responseUsers = iamClient.ListUsers(requestUsers);

  foreach (var user in responseUsers.Users)
  {
    Console.WriteLine("For user {0}:", user.UserName);
    Console.WriteLine("  In groups:");

    var requestGroups = new ListGroupsForUserRequest
    {
      UserName = user.UserName
    };
    var responseGroups = iamClient.ListGroupsForUser(requestGroups);

    foreach (var group in responseGroups.Groups)
    {
      Console.WriteLine("    {0}", group.GroupName);
    }

    Console.WriteLine("  Policies:");

    var requestPolicies = new ListUserPoliciesRequest
    {
      UserName = user.UserName
    };
    var responsePolicies = iamClient.ListUserPolicies(requestPolicies);

    foreach (var policy in responsePolicies.PolicyNames)
    {
      Console.WriteLine("    {0}", policy);
    }

    var requestAccessKeys = new ListAccessKeysRequest
    {
      UserName = user.UserName
    };
    var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);

    Console.WriteLine("  Access keys:");

    foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
    {
      Console.WriteLine("    {0}", accessKey.AccessKeyId);
    }
  }
}
```

For related API reference information, see [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html) and [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)\.

**Topics**