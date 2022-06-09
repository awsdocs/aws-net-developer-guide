# IAM examples using AWS SDK for \.NET<a name="csharp_iam_code_examples"></a>

The following code examples show you how to perform actions and implement common scenarios by using the AWS SDK for \.NET with IAM\.

*Actions* are code excerpts that show you how to call individual IAM functions\.

*Scenarios* are code examples that show you how to accomplish a specific task by calling multiple IAM functions\.

Each example includes a link to GitHub, where you can find instructions on how to set up and run the code in context\.

**Topics**
+ [Actions](#w150aac21c18b9c19c13)
+ [Scenarios](#w150aac21c18b9c19c15)

## Actions<a name="w150aac21c18b9c19c13"></a>

### Attach a policy to a role<a name="iam_AttachRolePolicy_csharp_topic"></a>

The following code example shows how to attach an IAM policy to a role\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Attach the policy to the role so that the user can assume it.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="policyArn">The ARN of the policy to attach.</param>
        /// <param name="roleName">The name of the role to attach the policy to.</param>
        public static async Task AttachRoleAsync(
            AmazonIdentityManagementServiceClient client,
            string policyArn,
            string roleName)
        {
            var request = new AttachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            };

            var response = await client.AttachRolePolicyAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Successfully attached the policy to the role.");
            }
            else
            {
                Console.WriteLine("Could not attach the policy.");
            }
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [AttachRolePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/AttachRolePolicy) in *AWS SDK for \.NET API Reference*\. 

### Create a policy<a name="iam_CreatePolicy_csharp_topic"></a>

The following code example shows how to create an IAM policy\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Create a policy to allow a user to list the buckets in an account.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="policyName">The name of the poicy to create.</param>
        /// <param name="policyDocument">The permissions policy document.</param>
        /// <returns>The newly created ManagedPolicy object.</returns>
        public static async Task<ManagedPolicy> CreatePolicyAsync(
            AmazonIdentityManagementServiceClient client,
            string policyName,
            string policyDocument)
        {
            var request = new CreatePolicyRequest
            {
                PolicyName = policyName,
                PolicyDocument = policyDocument,
            };

            var response = await client.CreatePolicyAsync(request);

            return response.Policy;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [CreatePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreatePolicy) in *AWS SDK for \.NET API Reference*\. 

### Create a role<a name="iam_CreateRole_csharp_topic"></a>

The following code example shows how to create an IAM role\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Create a new IAM role which we can attach to a user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="roleName">The name of the IAM role to create.</param>
        /// <param name="rolePermissions">The permissions which the role will have.</param>
        /// <returns>A Role object representing the newly created role.</returns>
        public static async Task<Role> CreateRoleAsync(
            AmazonIdentityManagementServiceClient client,
            string roleName,
            string rolePermissions)
        {
            var request = new CreateRoleRequest
            {
                RoleName = roleName,
                AssumeRolePolicyDocument = rolePermissions,
            };

            var response = await client.CreateRoleAsync(request);

            return response.Role;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [CreateRole](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateRole) in *AWS SDK for \.NET API Reference*\. 

### Create a user<a name="iam_CreateUser_csharp_topic"></a>

The following code example shows how to create an IAM user\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Create a new IAM user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="userName">A string representing the user name of the
        /// new user.</param>
        /// <returns>The newly created user.</returns>
        public static async Task<User> CreateUserAsync(
            AmazonIdentityManagementServiceClient client,
            string userName)
        {
            var request = new CreateUserRequest
            {
                UserName = userName,
            };

            var response = await client.CreateUserAsync(request);

            return response.User;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [CreateUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateUser) in *AWS SDK for \.NET API Reference*\. 

### Create an access key<a name="iam_CreateAccessKey_csharp_topic"></a>

The following code example shows how to create an IAM access key\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Create a new AccessKey for the user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="userName">The name of the user for whom to create the key.</param>
        /// <returns>A new IAM access key for the user.</returns>
        public static async Task<AccessKey> CreateAccessKeyAsync(
            AmazonIdentityManagementServiceClient client,
            string userName)
        {
            var request = new CreateAccessKeyRequest
            {
                UserName = userName,
            };

            var response = await client.CreateAccessKeyAsync(request);

            if (response.AccessKey is not null)
            {
                Console.WriteLine($"Successfully created Access Key for {userName}.");
            }

            return response.AccessKey;
        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [CreateAccessKey](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateAccessKey) in *AWS SDK for \.NET API Reference*\. 

### Delete a role policy<a name="iam_DeleteRolePolicy_csharp_topic"></a>

The following code example shows how to delete an IAM role policy\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.Threading.Tasks;
    using Amazon.IdentityManagement;
    using Amazon.IdentityManagement.Model;

    public class DeleteRolePolicy
    {
        /// <summary>
        /// Initializes the IAM client object and then calls DeleteRolePolicyAsync
        /// to delete the Policy attached to the Role.
        /// </summary>
        public static async Task Main()
        {
            var client = new AmazonIdentityManagementServiceClient();
            var response = await client.DeleteRolePolicyAsync(new DeleteRolePolicyRequest
            {
                PolicyName = "ExamplePolicy",
                RoleName = "Test-Role",
            });

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Policy successfully deleted.");
            }
            else
            {
                Console.WriteLine("Could not delete pollicy.");
            }
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [DeleteRolePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteRolePolicy) in *AWS SDK for \.NET API Reference*\. 

### Delete a user<a name="iam_DeleteUser_csharp_topic"></a>

The following code example shows how to delete an IAM user\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Delete the user, and other resources created for this example.
        /// </summary>
        /// <param name="client">The initialized client object.</param>
        /// <param name=accessKeyId">The Id of the user's access key.</param>"
        /// <param name="userName">The user name of the user to delete.</param>
        /// <param name="policyName">The name of the policy to delete.</param>
        /// <param name="policyArn">The Amazon Resource Name ARN of the Policy to delete.</param>
        /// <param name="roleName">The name of the role that will be deleted.</param>
        public static async Task DeleteResourcesAsync(
            AmazonIdentityManagementServiceClient client,
            string accessKeyId,
            string userName,
            string policyArn,
            string roleName)
        {
            var detachPolicyResponse = await client.DetachRolePolicyAsync(new DetachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            });

            var delPolicyResponse = await client.DeletePolicyAsync(new DeletePolicyRequest
            {
                PolicyArn = policyArn,
            });

            var delRoleResponse = await client.DeleteRoleAsync(new DeleteRoleRequest
            {
                RoleName = roleName,
            });

            var delAccessKey = await client.DeleteAccessKeyAsync(new DeleteAccessKeyRequest
            {
                AccessKeyId = accessKeyId,
                UserName = userName,
            });

            var delUserResponse = await client.DeleteUserAsync(new DeleteUserRequest
            {
                UserName = userName,
            });

        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [DeleteUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteUser) in *AWS SDK for \.NET API Reference*\. 

### Delete an access key<a name="iam_DeleteAccessKey_csharp_topic"></a>

The following code example shows how to delete an IAM access key\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Delete the user, and other resources created for this example.
        /// </summary>
        /// <param name="client">The initialized client object.</param>
        /// <param name=accessKeyId">The Id of the user's access key.</param>"
        /// <param name="userName">The user name of the user to delete.</param>
        /// <param name="policyName">The name of the policy to delete.</param>
        /// <param name="policyArn">The Amazon Resource Name ARN of the Policy to delete.</param>
        /// <param name="roleName">The name of the role that will be deleted.</param>
        public static async Task DeleteResourcesAsync(
            AmazonIdentityManagementServiceClient client,
            string accessKeyId,
            string userName,
            string policyArn,
            string roleName)
        {
            var detachPolicyResponse = await client.DetachRolePolicyAsync(new DetachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            });

            var delPolicyResponse = await client.DeletePolicyAsync(new DeletePolicyRequest
            {
                PolicyArn = policyArn,
            });

            var delRoleResponse = await client.DeleteRoleAsync(new DeleteRoleRequest
            {
                RoleName = roleName,
            });

            var delAccessKey = await client.DeleteAccessKeyAsync(new DeleteAccessKeyRequest
            {
                AccessKeyId = accessKeyId,
                UserName = userName,
            });

            var delUserResponse = await client.DeleteUserAsync(new DeleteUserRequest
            {
                UserName = userName,
            });

        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [DeleteAccessKey](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteAccessKey) in *AWS SDK for \.NET API Reference*\. 

### Detach a policy from a role<a name="iam_DetachRolePolicy_csharp_topic"></a>

The following code example shows how to detach an IAM policy from a role\.

**AWS SDK for \.NET**  
  

```
        /// <summary>
        /// Delete the user, and other resources created for this example.
        /// </summary>
        /// <param name="client">The initialized client object.</param>
        /// <param name=accessKeyId">The Id of the user's access key.</param>"
        /// <param name="userName">The user name of the user to delete.</param>
        /// <param name="policyName">The name of the policy to delete.</param>
        /// <param name="policyArn">The Amazon Resource Name ARN of the Policy to delete.</param>
        /// <param name="roleName">The name of the role that will be deleted.</param>
        public static async Task DeleteResourcesAsync(
            AmazonIdentityManagementServiceClient client,
            string accessKeyId,
            string userName,
            string policyArn,
            string roleName)
        {
            var detachPolicyResponse = await client.DetachRolePolicyAsync(new DetachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            });

            var delPolicyResponse = await client.DeletePolicyAsync(new DeletePolicyRequest
            {
                PolicyArn = policyArn,
            });

            var delRoleResponse = await client.DeleteRoleAsync(new DeleteRoleRequest
            {
                RoleName = roleName,
            });

            var delAccessKey = await client.DeleteAccessKeyAsync(new DeleteAccessKeyRequest
            {
                AccessKeyId = accessKeyId,
                UserName = userName,
            });

            var delUserResponse = await client.DeleteUserAsync(new DeleteUserRequest
            {
                UserName = userName,
            });

        }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [DetachRolePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DetachRolePolicy) in *AWS SDK for \.NET API Reference*\. 

### Get a policy<a name="iam_GetPolicy_csharp_topic"></a>

The following code example shows how to get an IAM policy\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
var request = new GetPolicyRequest
{
    PolicyArn = "POLICY_ARN",
};

var response = await client.GetPolicyAsync(request);

Console.Write($"{response.Policy.PolicyName} was created on ");
Console.WriteLine($"{response.Policy.CreateDate}");
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [GetPolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/GetPolicy) in *AWS SDK for \.NET API Reference*\. 

### Get a role<a name="iam_GetRole_csharp_topic"></a>

The following code example shows how to get an IAM role\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();

var response = await client.GetRoleAsync(new GetRoleRequest
{
    RoleName = "LambdaS3Role",
});

if (response.Role is not null)
{
    Console.WriteLine($"{response.Role.RoleName} with ARN: {response.Role.Arn}");
    Console.WriteLine($"{response.Role.Description}");
    Console.WriteLine($"Created: {response.Role.CreateDate} Last used on: { response.Role.RoleLastUsed}");
}
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [GetRole](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/GetRole) in *AWS SDK for \.NET API Reference*\. 

### Get the account password policy<a name="iam_GetAccountPasswordPolicy_csharp_topic"></a>

The following code example shows how to get the IAM account password policy\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();

try
{
    var request = new GetAccountPasswordPolicyRequest();
    var response = await client.GetAccountPasswordPolicyAsync(request);

    Console.WriteLine($"{response.PasswordPolicy}");
}
catch (NoSuchEntityException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [GetAccountPasswordPolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/GetAccountPasswordPolicy) in *AWS SDK for \.NET API Reference*\. 

### List SAML providers<a name="iam_ListSAMLProviders_csharp_topic"></a>

The following code example shows how to list SAML providers for IAM\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();

var response = await client.ListSAMLProvidersAsync(new ListSAMLProvidersRequest());

response.SAMLProviderList.ForEach(samlProvider =>
{
    Console.WriteLine($"{samlProvider.Arn} created on: {samlProvider.CreateDate}");
});
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListSAMLProviders](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListSAMLProviders) in *AWS SDK for \.NET API Reference*\. 

### List groups<a name="iam_ListGroups_csharp_topic"></a>

The following code example shows how to list IAM groups\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();

var request = new ListGroupsRequest
{
    MaxItems = 10,
};

var response = await client.ListGroupsAsync(request);

do
{
    response.Groups.ForEach(group =>
    {
        Console.WriteLine($"{group.GroupName} created on: {group.CreateDate}");
    });

    if (response.IsTruncated)
    {
        request.Marker = response.Marker;
        response = await client.ListGroupsAsync(request);
    }
} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListGroups](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListGroups) in *AWS SDK for \.NET API Reference*\. 

### List inline policies for a role<a name="iam_ListRolePolicies_csharp_topic"></a>

The following code example shows how to list inline policies for an IAM role\.

**AWS SDK for \.NET**  
  

```
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using System;

var client = new AmazonIdentityManagementServiceClient();
var request = new ListRolePoliciesRequest
{
    RoleName = "LambdaS3Role",
};

var response = new ListRolePoliciesResponse();

do
{
    response = await client.ListRolePoliciesAsync(request);

    if (response.PolicyNames.Count > 0)
    {
        response.PolicyNames.ForEach(policyName =>
        {
            Console.WriteLine($"{policyName}");
        });
    }

    // As long as response.IsTruncated is true, set request.Marker equal
    // to response.Marker and call ListRolesAsync again.
    if (response.IsTruncated)
    {
        request.Marker = response.Marker;
    }
} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListRolePolicies](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListRolePolicies) in *AWS SDK for \.NET API Reference*\. 

### List policies<a name="iam_ListPolicies_csharp_topic"></a>

The following code example shows how to list IAM policies\.

**AWS SDK for \.NET**  
  

```
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using System;

var client = new AmazonIdentityManagementServiceClient();

var request = new ListPoliciesRequest
{
    MaxItems = 10,
};

var response = new ListPoliciesResponse();

do
{
    response = await client.ListPoliciesAsync(request);
    response.Policies.ForEach(policy =>
    {
        Console.Write($"{policy.PolicyName} ");
        Console.Write($"with ID: {policy.PolicyId} ");
        Console.Write($"and ARN: {policy.Arn}. ");
        Console.WriteLine($"It was created on {policy.CreateDate}.");
    });

    if (response.IsTruncated)
    {
        request.Marker = response.Marker;
    }
} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListPolicies](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListPolicies) in *AWS SDK for \.NET API Reference*\. 

### List policies attached to a role<a name="iam_ListAttachedRolePolicies_csharp_topic"></a>

The following code example shows how to list policies attached to an IAM role\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
var request = new ListAttachedRolePoliciesRequest
{
    MaxItems = 10,
    RoleName = "testAssumeRole",
};

var response = await client.ListAttachedRolePoliciesAsync(request);

do
{
    response.AttachedPolicies.ForEach(policy =>
    {
        Console.WriteLine($"{policy.PolicyName} with ARN: {policy.PolicyArn}");
    });

    if (response.IsTruncated)
    {
        request.Marker = response.Marker;
        response = await client.ListAttachedRolePoliciesAsync(request);
    }

} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListAttachedRolePolicies](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListAttachedRolePolicies) in *AWS SDK for \.NET API Reference*\. 

### List roles<a name="iam_ListRoles_csharp_topic"></a>

The following code example shows how to list IAM roles\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();

// Without the MaxItems value, the ListRolesAsync method will
// return information for up to 100 roles. If there are more
// than the MaxItems value or more than 100 roles, the response
// value IsTruncated will be true.
var request = new ListRolesRequest
{
    MaxItems = 10,
};

var response = new ListRolesResponse();

do
{
    response = await client.ListRolesAsync(request);
    response.Roles.ForEach(role =>
    {
        Console.WriteLine($"{role.RoleName} - ARN {role.Arn}");
    });

    // As long as response.IsTruncated is true, set request.Marker equal
    // to response.Marker and call ListRolesAsync again.
    if (response.IsTruncated)
    {
        request.Marker = response.Marker;
    }
} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListRoles](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListRoles) in *AWS SDK for \.NET API Reference*\. 

### List users<a name="iam_ListUsers_csharp_topic"></a>

The following code example shows how to list IAM users\.

**AWS SDK for \.NET**  
  

```
using System;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
var request = new ListUsersRequest
{
    MaxItems = 10,
};
var response = await client.ListUsersAsync(request);

do
{
    response.Users.ForEach(user =>
    {
        Console.WriteLine($"{user.UserName} created on {user.CreateDate}.");
        Console.WriteLine($"ARN: {user.Arn}\n");
    });

    request.Marker = response.Marker;
    response = await client.ListUsersAsync(request);
} while (response.IsTruncated);
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM#code-examples)\. 
+  For API details, see [ListUsers](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/ListUsers) in *AWS SDK for \.NET API Reference*\. 

## Scenarios<a name="w150aac21c18b9c19c15"></a>

### Create a user and assume a role<a name="iam_Scenario_CreateUserAssumeRole_csharp_topic"></a>

The following code example shows how to:
+ Create a user who has no permissions\.
+ Create a role that grants permission to list Amazon S3 buckets for the account\.
+ Add a policy to let the user assume the role\.
+ Assume the role and list Amazon S3 buckets using temporary credentials\.
+ Delete the policy, role, and user\.

**AWS SDK for \.NET**  
  

```
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.IdentityManagement;
    using Amazon.IdentityManagement.Model;
    using Amazon.S3;
    using Amazon.SecurityToken;
    using Amazon.SecurityToken.Model;

    public class IAM_Basics
    {
        // Values needed for user, role, and policies.
        private const string UserName = "example-user";
        private const string S3PolicyName = "s3-list-buckets-policy";
        private const string RoleName = "temporary-role";
        private const string AssumePolicyName = "sts-trust-user";

        private static readonly RegionEndpoint Region = RegionEndpoint.USEast2;

        public static async Task Main()
        {
            DisplayInstructions();

            // Create the IAM client object.
            var client = new AmazonIdentityManagementServiceClient(Region);

            // First create a user. By default, the new user has
            // no permissions.
            Console.WriteLine($"Creating a new user with user name: {UserName}.");
            var user = await CreateUserAsync(client, UserName);
            var userArn = user.Arn;
            Console.WriteLine($"Successfully created user: {UserName} with ARN: {userArn}.");

            // Create an AccessKey for the user.
            var accessKey = await CreateAccessKeyAsync(client, UserName);

            var accessKeyId = accessKey.AccessKeyId;
            var secretAccessKey = accessKey.SecretAccessKey;

            // Try listing the Amazon Simple Storage Service (Amazon S3)
            // buckets. This should fail at this point because the user doesn't
            // have permissions to perform this task.
            var s3Client1 = new AmazonS3Client(accessKeyId, secretAccessKey);
            await ListMyBucketsAsync(s3Client1);

            // Define a role policy document that allows the new user
            // to assume the role.
            // string assumeRolePolicyDocument = File.ReadAllText("assumePolicy.json");
            string assumeRolePolicyDocument = "{" +
                "\"Version\": \"2012-10-17\"," +
                "\"Statement\": [{" +
                "\"Effect\": \"Allow\"," +
                "\"Principal\": {" +
                $"	\"AWS\": \"{userArn}\"" +
                "}," +
                    "\"Action\": \"sts:AssumeRole\"" +
                "}]" +
            "}";

            // Permissions to list all buckets.
            string policyDocument = "{" +
                "\"Version\": \"2012-10-17\"," +
                "	\"Statement\" : [{" +
                    "	\"Action\" : [\"s3:ListAllMyBuckets\"]," +
                    "	\"Effect\" : \"Allow\"," +
                    "	\"Resource\" : \"*\"" +
                "}]" +
            "}";

            // Create the role to allow listing the S3 buckets. Role names are
            // not case sensitive and must be unique to the account for which it
            // is created.
            var role = await CreateRoleAsync(client, RoleName, assumeRolePolicyDocument);
            var roleArn = role.Arn;

            // Create a policy with permissions to list S3 buckets
            var policy = await CreatePolicyAsync(client, S3PolicyName, policyDocument);

            // Wait 15 seconds for the policy to be created.
            WaitABit(15, "Waiting for the policy to be available.");

            // Attach the policy to the role you created earlier.
            await AttachRoleAsync(client, policy.Arn, RoleName);

            // Wait 15 seconds for the role to be updated.
            Console.WriteLine();
            WaitABit(15, "Waiting to time for the policy to be attached.");

            // Use the AWS Security Token Service (AWS STS) to have the user
            // assume the role we created.
            var stsClient = new AmazonSecurityTokenServiceClient(accessKeyId, secretAccessKey);

            // Wait for the new credentials to become valid.
            WaitABit(10, "Waiting for the credentials to be valid.");

            var assumedRoleCredentials = await AssumeS3RoleAsync(stsClient, "temporary-session", roleArn);

            // Try again to list the buckets using the client created with
            // the new user's credentials. This time, it should work.
            var s3Client2 = new AmazonS3Client(assumedRoleCredentials);

            await ListMyBucketsAsync(s3Client2);

            // Now clean up all the resources used in the example.
            await DeleteResourcesAsync(client, accessKeyId, UserName, policy.Arn, RoleName);

            Console.WriteLine("IAM Demo completed.");
        }


        /// <summary>
        /// Create a new IAM user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="userName">A string representing the user name of the
        /// new user.</param>
        /// <returns>The newly created user.</returns>
        public static async Task<User> CreateUserAsync(
            AmazonIdentityManagementServiceClient client,
            string userName)
        {
            var request = new CreateUserRequest
            {
                UserName = userName,
            };

            var response = await client.CreateUserAsync(request);

            return response.User;
        }



        /// <summary>
        /// Create a new AccessKey for the user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="userName">The name of the user for whom to create the key.</param>
        /// <returns>A new IAM access key for the user.</returns>
        public static async Task<AccessKey> CreateAccessKeyAsync(
            AmazonIdentityManagementServiceClient client,
            string userName)
        {
            var request = new CreateAccessKeyRequest
            {
                UserName = userName,
            };

            var response = await client.CreateAccessKeyAsync(request);

            if (response.AccessKey is not null)
            {
                Console.WriteLine($"Successfully created Access Key for {userName}.");
            }

            return response.AccessKey;
        }



        /// <summary>
        /// Create a policy to allow a user to list the buckets in an account.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="policyName">The name of the poicy to create.</param>
        /// <param name="policyDocument">The permissions policy document.</param>
        /// <returns>The newly created ManagedPolicy object.</returns>
        public static async Task<ManagedPolicy> CreatePolicyAsync(
            AmazonIdentityManagementServiceClient client,
            string policyName,
            string policyDocument)
        {
            var request = new CreatePolicyRequest
            {
                PolicyName = policyName,
                PolicyDocument = policyDocument,
            };

            var response = await client.CreatePolicyAsync(request);

            return response.Policy;
        }



        /// <summary>
        /// Attach the policy to the role so that the user can assume it.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="policyArn">The ARN of the policy to attach.</param>
        /// <param name="roleName">The name of the role to attach the policy to.</param>
        public static async Task AttachRoleAsync(
            AmazonIdentityManagementServiceClient client,
            string policyArn,
            string roleName)
        {
            var request = new AttachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            };

            var response = await client.AttachRolePolicyAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Successfully attached the policy to the role.");
            }
            else
            {
                Console.WriteLine("Could not attach the policy.");
            }
        }



        /// <summary>
        /// Create a new IAM role which we can attach to a user.
        /// </summary>
        /// <param name="client">The initialized IAM client object.</param>
        /// <param name="roleName">The name of the IAM role to create.</param>
        /// <param name="rolePermissions">The permissions which the role will have.</param>
        /// <returns>A Role object representing the newly created role.</returns>
        public static async Task<Role> CreateRoleAsync(
            AmazonIdentityManagementServiceClient client,
            string roleName,
            string rolePermissions)
        {
            var request = new CreateRoleRequest
            {
                RoleName = roleName,
                AssumeRolePolicyDocument = rolePermissions,
            };

            var response = await client.CreateRoleAsync(request);

            return response.Role;
        }



        /// <summary>
        /// List the Amazon S3 buckets owned by the user.
        /// </summary>
        /// <param name="accessKeyId">The access key Id for the user.</param>
        /// <param name="secretAccessKey">The Secret access key for the user.</param>
        public static async Task ListMyBucketsAsync(AmazonS3Client client)
        {
            Console.WriteLine("\nPress <Enter> to list the S3 buckets using the new user.\n");
            Console.ReadLine();

            try
            {
                // Get the list of buckets accessible by the new user.
                var response = await client.ListBucketsAsync();

                // Loop through the list and print each bucket's name
                // and creation date.
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Listing S3 buckets:\n");
                response.Buckets
                    .ForEach(b => Console.WriteLine($"Bucket name: {b.BucketName}, created on: {b.CreationDate}"));
            }
            catch (AmazonS3Exception ex)
            {
                // Something else went wrong. Display the error message.
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press <Enter> to continue.");
            Console.ReadLine();
        }



        /// <summary>
        /// Have the user assume the role that allows the role to be used to
        /// list all S3 buckets.
        /// </summary>
        /// <param name="client">An initialized AWS STS client object.</param>
        /// <param name="roleSession">The name of the session where the role
        /// assumption will be active.</param>
        /// <param name="roleToAssume">The Amazon Resource Name (ARN) of the
        /// role to assume.</param>
        /// <returns>The AssumedRoleUser object needed to perform the list
        /// buckets procedure.</returns>
        public static async Task<Credentials> AssumeS3RoleAsync(
            AmazonSecurityTokenServiceClient client,
            string roleSession,
            string roleToAssume)
        {
            // Create the request to use with the AssumeRoleAsync call.
            var request = new AssumeRoleRequest()
            {
                RoleSessionName = roleSession,
                RoleArn = roleToAssume,
            };

            var response = await client.AssumeRoleAsync(request);

            return response.Credentials;
        }



        /// <summary>
        /// Delete the user, and other resources created for this example.
        /// </summary>
        /// <param name="client">The initialized client object.</param>
        /// <param name=accessKeyId">The Id of the user's access key.</param>"
        /// <param name="userName">The user name of the user to delete.</param>
        /// <param name="policyName">The name of the policy to delete.</param>
        /// <param name="policyArn">The Amazon Resource Name ARN of the Policy to delete.</param>
        /// <param name="roleName">The name of the role that will be deleted.</param>
        public static async Task DeleteResourcesAsync(
            AmazonIdentityManagementServiceClient client,
            string accessKeyId,
            string userName,
            string policyArn,
            string roleName)
        {
            var detachPolicyResponse = await client.DetachRolePolicyAsync(new DetachRolePolicyRequest
            {
                PolicyArn = policyArn,
                RoleName = roleName,
            });

            var delPolicyResponse = await client.DeletePolicyAsync(new DeletePolicyRequest
            {
                PolicyArn = policyArn,
            });

            var delRoleResponse = await client.DeleteRoleAsync(new DeleteRoleRequest
            {
                RoleName = roleName,
            });

            var delAccessKey = await client.DeleteAccessKeyAsync(new DeleteAccessKeyRequest
            {
                AccessKeyId = accessKeyId,
                UserName = userName,
            });

            var delUserResponse = await client.DeleteUserAsync(new DeleteUserRequest
            {
                UserName = userName,
            });

        }


        /// <summary>
        /// Display a countdown and wait for a number of seconds.
        /// </summary>
        /// <param name="numSeconds">The number of seconds to wait.</param>
        public static void WaitABit(int numSeconds, string msg)
        {
            Console.WriteLine(msg);

            // Wait for the requested number of seconds.
            for (int i = numSeconds; i > 0; i--)
            {
                System.Threading.Thread.Sleep(1000);
                Console.Write($"{i}...");
            }

            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();
        }

        /// <summary>
        /// Shows the a description of the features of the program.
        /// </summary>
        public static void DisplayInstructions()
        {
            var separator = new string('-', 80);

            Console.WriteLine(separator);
            Console.WriteLine("IAM Basics");
            Console.WriteLine("This application uses the basic features of the AWS Identity and Access");
            Console.WriteLine("Management (IAM) creating, managing, and controlling access to resources for");
            Console.WriteLine("users. The application was created using the AWS SDK for .NET version 3.7 and");
            Console.WriteLine(".NET Core 5. The application performs the following actions:");
            Console.WriteLine();
            Console.WriteLine("1. Creates a user with no permissions");
            Console.WriteLine("2. Creates a rolw and policy that grants s3:ListAllMyBuckets permission");
            Console.WriteLine("3. Grants the user permission to assume the role");
            Console.WriteLine("4. Creates an Amazon Simple Storage Service (Amazon S3) client and tries");
            Console.WriteLine("   to list buckets. (This should fail.)");
            Console.WriteLine("5. Gets temporary credentials by assuming the role.");
            Console.WriteLine("6. Creates an Amazon S3 client object with the temporary credentials and");
            Console.WriteLine("   lists the buckets. (This time it should work.)");
            Console.WriteLine("7. Deletes all of the resources created.");
            Console.WriteLine(separator);
            Console.WriteLine("Press <Enter> to continue.");
            Console.ReadLine();
        }
    }
```
+  Find instructions and more code on [GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/IAM/IAM_Basics_Scenario#code-examples)\. 
+ For API details, see the following topics in *AWS SDK for \.NET API Reference*\.
  + [AttachRolePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/AttachRolePolicy)
  + [CreateAccessKey](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateAccessKey)
  + [CreatePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreatePolicy)
  + [CreateRole](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateRole)
  + [CreateUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/CreateUser)
  + [DeleteAccessKey](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteAccessKey)
  + [DeletePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeletePolicy)
  + [DeleteRole](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteRole)
  + [DeleteUser](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteUser)
  + [DeleteUserPolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DeleteUserPolicy)
  + [DetachRolePolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/DetachRolePolicy)
  + [PutUserPolicy](https://docs.aws.amazon.com/goto/DotNetSDKV3/iam-2010-05-08/PutUserPolicy)