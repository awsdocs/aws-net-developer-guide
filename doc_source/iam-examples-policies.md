--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Working with IAM Policies<a name="iam-examples-policies"></a>

The following examples show you how to:
+ Create and delete IAM policies
+ Attach and detach IAM policies from roles

## The Scenario<a name="the-scenario"></a>

You grant permissions to a user by creating a policy, which is a document that lists the actions that a user can perform and the resources those actions can affect\. Any actions or resources that are not explicitly allowed are denied by default\. You can create policies and attach them to users, groups of users, roles assumed by users, and resources\.

Use the AWS SDK for \.NET to create and delete policies and attach and detach role policies by using these methods of the [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) class:
+  [CreatePolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreatePolicyCreatePolicyRequest.html) 
+  [GetPolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetPolicyGetPolicyRequest.html) 
+  [AttachRolePolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceAttachRolePolicyAttachRolePolicyRequest.html) 
+  [DetachRolePolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDetachRolePolicyDetachRolePolicyRequest.html) 

For more information about IAM users, see [Overview of Access Management: Permissions and Policies](https://docs.aws.amazon.com/IAM/latest/UserGuide/introduction_access-management.html.html) in the IAM User Guide\.

## Create an IAM Policy<a name="create-an-iam-policy"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [CreatePolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreatePolicyRequest.html) object containing the parameters needed to create a new policy, which consists of the name you want to use for the new policy and a policy document\. You create the policy document by calling the provided `GenerateRolePolicyDocument` method\. Upon returning from the `CreatePolicy` method call, the [CreatePolicyResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreatePolicyResponse.html) contains the policy ARN, which is displayed on the console\. Please make a note of it so you can use it in the following examples\.

```
public static void CreatePolicyExample()
{
    var client = new AmazonIdentityManagementServiceClient();
    // GenerateRolePolicyDocument is a custom method
    string policyDoc = GenerateRolePolicyDocument();

    var request = new CreatePolicyRequest
    {
        PolicyName = "DemoEC2Permissions",
        PolicyDocument = policyDoc
    };

    try
    {
        var createPolicyResponse = client.CreatePolicy(request);
        Console.WriteLine("Make a note, Policy named " + createPolicyResponse.Policy.PolicyName +
            " has Arn: : " + createPolicyResponse.Policy.Arn);
    }
    catch (EntityAlreadyExistsException)
    {
        Console.WriteLine
          ("Policy 'DemoEC2Permissions' already exits.");
    }

}

public static string GenerateRolePolicyDocument()
{
    // using Amazon.Auth.AccessControlPolicy;

    // Create a policy that looks like this:
    /*
    {
      "Version" : "2012-10-17",
      "Id"  : "DemoEC2Permissions",
      "Statement" : [
        {
          "Sid" : "DemoEC2PermissionsStatement",
          "Effect" : "Allow",
          "Action" : [
            "s3:Get*",
            "s3:List*"
          ],
          "Resource" : "*"
        }
      ]
    }
    */

    var actionGet = new ActionIdentifier("s3:Get*");
    var actionList = new ActionIdentifier("s3:List*");
    var actions = new List<ActionIdentifier>();

    actions.Add(actionGet);
    actions.Add(actionList);

    var resource = new Resource("*");
    var resources = new List<Resource>();

    resources.Add(resource);

    var statement = new Amazon.Auth.AccessControlPolicy.Statement(Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
    {
        Actions = actions,
        Id = "DemoEC2PermissionsStatement",
        Resources = resources
    };
    var statements = new List<Amazon.Auth.AccessControlPolicy.Statement>();

    statements.Add(statement);

    var policy = new Policy
    {
        Id = "DemoEC2Permissions",
        Version = "2012-10-17",
        Statements = statements
    };

    return policy.ToJson();
}
```

## Get an IAM Policy<a name="get-an-iam-policy"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [GetPolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetPolicyRequest.html) object containing the parameter needed to get the policy, the policy ARN, which was returned by the `CreatePolicy` method in the previous example\.

Call the [GetPolicy](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetPolicyGetPolicyRequest.html) method\.

```
public static void GetPolicy()
{
    var client = new AmazonIdentityManagementServiceClient();
    var request = new GetPolicyRequest
    {
        PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions"
    };

    try
    {
        var response = client.GetPolicy(request);
        Console.WriteLine("Policy " + response.Policy.PolicyName + "successfully retrieved");

    }
    catch (NoSuchEntityException)
    {
        Console.WriteLine
          ("Policy 'DemoEC2Permissions' does not exist.");
    }

}
```

## Attach a Managed Role Policy<a name="attach-a-managed-role-policy"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create an [AttachRolePolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TAttachRolePolicyRequest.html) object containing the parameters needed to attach the policy to the role, the role name, and the Jason policy returned by the `GenerateRolePolicyDocument` method\. Be sure to use a valid role from the roles associated with your AWS account\.

```
public static void AttachRolePolicy()
{
    var client = new AmazonIdentityManagementServiceClient();
    string policy = GenerateRolePolicyDocument();
    CreateRoleRequest roleRequest = new CreateRoleRequest()
    {
        RoleName = "tester",
        AssumeRolePolicyDocument = policy
    };

    var request = new AttachRolePolicyRequest()
    {
        PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions",
        RoleName = "tester"
    };
    try
    {
        var response = client.AttachRolePolicy(request);
        Console.WriteLine("Policy DemoEC2Permissions attached to Role TestUser");
    }
    catch (NoSuchEntityException)
    {
        Console.WriteLine
          ("Policy 'DemoEC2Permissions' does not exist");
    }
    catch (InvalidInputException)
    {
        Console.WriteLine
          ("One of the parameters is incorrect");
    }
}
```

## Detach a Managed Role Policy<a name="detach-a-managed-role-policy"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [DetachRolePolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDetachRolePolicyRequest.html) object containing the parameters needed to attach the policy to the role, the role name, and the Jason policy returned by the `GenerateRolePolicyDocument` method\. Be sure to use the role you used to attach the policy in the previous example\.

```
public static void DetachRolePolicy()
{
    var client = new AmazonIdentityManagementServiceClient();
    string policy = GenerateRolePolicyDocument();
    CreateRoleRequest roleRequest = new CreateRoleRequest()
    {
        RoleName = "tester",
        AssumeRolePolicyDocument = policy
    };

    var request = new DetachRolePolicyRequest()
    {
        PolicyArn = "arn:aws:iam::123456789:policy/DemoEC2Permissions",
        RoleName = "tester"
    };
    try
    {
        var response = client.DetachRolePolicy(request);
        Console.WriteLine("Policy DemoEC2Permissions detached from Role 'tester'");
    }
    catch (NoSuchEntityException e)
    {
        Console.WriteLine
          (e.Message);
    }
    catch (InvalidInputException i)
    {
        Console.WriteLine
          (i.Message);
    }
}
```