--------

End of support announcement: [http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/](http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

This documentation is for version 2\.0 of the AWS SDK for \.NET\.** For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/) of the AWS SDK for \.NET developer guide instead\.**

--------

# AWS Identity and Access Management Programming with the AWS SDK for \.NET<a name="iam-apis-intro"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c17b3b1"></a>

The AWS SDK for \.NET supports AWS Identity and Access Management \(IAM\), which is a web service that enables Amazon Web Services customers to manage users and user permissions in AWS\.

The following information introduces you to the IAM programming models in the the SDK\. There are also links to additional IAM programming resources within the the SDK\.

### Programming Models<a name="iam-apis-intro-models"></a>

The the SDK provides two programming models for working with IAM\. These programming models are known as the *low\-level* model and the *resource* model\. The following information describes these models and how to use them\.

#### Low\-Level APIs<a name="iam-apis-intro-low-level"></a>

The the SDK provides low\-level APIs for programming with IAM\. These low\-level APIs typically consist of sets of matching request\-and\-response objects that correspond to HTTP\-based API calls focusing on their corresponding service\-level constructs\.

The following example shows how to use the low\-level APIs to list accessible user accounts in IAM\. For each user account, its associated groups, policies, and access key IDs are also listed:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
var requestUsers = new ListUsersRequest();
var responseUsers = client.ListUsers(requestUsers);

foreach (var user in responseUsers.Users)
{
  Console.WriteLine("For user {0}:", user.UserName);
  Console.WriteLine("  In groups:");

  var requestGroups = new ListGroupsForUserRequest
  {
    UserName = user.UserName
  };
  var responseGroups = client.ListGroupsForUser(requestGroups);

  foreach (var group in responseGroups.Groups)
  {
    Console.WriteLine("    {0}", group.GroupName);
  }

  Console.WriteLine("  Policies:");

  var requestPolicies = new ListUserPoliciesRequest
  {
    UserName = user.UserName
  };
  var responsePolicies = client.ListUserPolicies(requestPolicies);

  foreach (var policy in responsePolicies.PolicyNames)
  {
    Console.WriteLine("    {0}", policy);
  }

  var requestAccessKeys = new ListAccessKeysRequest
  {
    UserName = user.UserName
  };
  var responseAccessKeys = client.ListAccessKeys(requestAccessKeys);

  Console.WriteLine("  Access keys:");

  foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
  {
    Console.WriteLine("    {0}", accessKey.AccessKeyId);
  }
}
```

For additional examples, see [Tutorial: Grant Access Using an IAM Role and the AWS SDK for \.NET](net-dg-hosm.md)\.

For related API reference information, see [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NIAMNET45.html) and [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NIAMNET45.html)\.

#### Resource APIs<a name="iam-apis-intro-resource-level"></a>

The the SDK provides the AWS Resource APIs for \.NET for programming with IAM\. These resource APIs provide a resource\-level programming model that enables you to write code to work more directly with IAM resources as compared to their low\-level API counterparts\. \(For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.\)

The AWS Resource APIs for \.NET are currently provided as a preview\. This means that these resource APIs may frequently change in response to customer feedback, and these changes may happen without advance notice\. Until these resource APIs exit the preview stage, please be cautious about writing and distributing production\-quality code that relies on them\.

The following example shows how to use the AWS Resource APIs for \.NET to list accessible user accounts in IAM\. For each user account, its associated groups, policies, and access key IDs are also listed:

```
// using Amazon.IdentityManagement.Resources;

var iam = new IdentityManagementService();  
var users = iam.GetUsers();

foreach (var user in users)
{
  Console.WriteLine("For user {0}:", user.Name);
  Console.WriteLine("  In groups:");

  foreach (var group in user.GetGroups()) 
  {
    Console.WriteLine("    {0}", group.Name);
  }

  Console.WriteLine("  Policies:");

  foreach (var policy in user.GetPolicies())
  {
    Console.WriteLine("    {0}", policy.Name);
  }

  Console.WriteLine("  Access keys:");

  foreach (var accessKey in user.GetAccessKeys())
  {
    Console.WriteLine("    {0}", accessKey.Id);
  }
}
```

For additional examples, see [AWS Identity and Access Management Code Examples with the AWS Resource APIs for \.NET](iam-resource-api-examples.md)\.

For related API reference information, see [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/NIAMNET45.html)\.

**Topics**
+ [AWS Identity and Access Management Code Examples with the AWS Resource APIs for \.NET](iam-resource-api-examples.md)
+ [Tutorial: Grant Access Using an IAM Role and the AWS SDK for \.NET](net-dg-hosm.md)