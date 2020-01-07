--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# AWS Identity and Access Management Code Examples with the AWS Resource APIs for \.NET<a name="iam-resource-api-examples"></a>

The following code examples demonstrate how to program with IAM by using the AWS Resource APIs for \.NET\.

The AWS Resource APIs for \.NET provide a resource\-level programming model that enables you to write code to work more directly with resources that are managed by AWS services\. For more information about the AWS Resource APIs for \.NET, including how to download and reference these resource APIs, see [Programming with the AWS Resource APIs for \.NET](resource-level-apis-intro.md)\.

The AWS Resource APIs for \.NET are currently provided as a preview\. This means that these resource APIs may frequently change in response to customer feedback, and these changes may happen without advance notice\. Until these resource APIs exit the preview stage, please be cautious about writing and distributing production\-quality code that relies on them\.

**Topics**
+ [Get User Account Information](#iam-resource-api-examples-get-user)
+ [Get Group Information](#iam-resource-api-examples-get-group)
+ [Get Role Information](#iam-resource-api-examples-get-role)
+ [Create a User Account](#iam-resource-api-examples-create-user)
+ [Create a Group](#iam-resource-api-examples-create-group)
+ [Create a Role](#iam-resource-api-examples-create-role)
+ [Add a User Account to a Group](#iam-resource-api-examples-add-user-to-group)
+ [Add a Policy to a User Account, Group, or Role](#iam-resource-api-examples-add-policy)
+ [Create an Access Key for a User Account](#iam-resource-api-examples-create-access-key)
+ [Create a Login Profile for a User Account](#iam-resource-api-examples-create-login-profile)
+ [Create an Instance Profile](#iam-resource-api-examples-create-instance-profile)
+ [Attach an Instance Profile to a Role](#iam-resource-api-examples-attach-instance-profile)

## Get User Account Information<a name="iam-resource-api-examples-get-user"></a>

The following example displays information about an existing user account, including its associated groups, policies, and access key IDs:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var user = iam.GetUserByName("DemoUser"); 
  
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
catch (NoSuchEntityException)
{
  Console.WriteLine("User 'DemoUser' does not exist.");
}
```

The following example displays a list of all accessible user accounts\. For each user account, its associated groups, policies, and access key IDs are also displayed:

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

## Get Group Information<a name="iam-resource-api-examples-get-group"></a>

The following example displays information about an existing group, including its associated policies and user accounts:

```
// using Amazon.IdentityManagement.Resources; 
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var group = iam.GetGroupByName("DemoGroup");

  Console.WriteLine("For group {0}:", group.Name);
  Console.WriteLine("  Policies:");

  foreach (var policy in group.GetPolicies())
  {
    Console.WriteLine("    {0}", policy.Name);
  }

  Console.WriteLine("  Users:");

  foreach (var user in group.GetUsers())
  {
    Console.WriteLine("    {0}", user.Name);
  }
}
catch (NoSuchEntityException)
{
  Console.WriteLine("Group 'DemoGroup' does not exist.");
}
```

The following example displays a list of all accessible groups\. For each group, its associated policies and user accounts are also displayed:

```
// using Amazon.IdentityManagement.Resources;

var iam = new IdentityManagementService();
var groups = iam.GetGroups(); 

foreach (var group in groups)
{
  Console.WriteLine("For group {0}:", group.Name);
  Console.WriteLine("  Policies:");

  foreach (var policy in group.GetPolicies())
  {
    Console.WriteLine("    {0}", policy.Name);
  }
  
  Console.WriteLine("  Users:");

  foreach (var user in group.GetUsers()) 
  {
    Console.WriteLine("    {0}", user.Name);
  }
}
```

## Get Role Information<a name="iam-resource-api-examples-get-role"></a>

The following example displays information about an existing role, including its associated policies and instance profiles:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var role = iam.GetRoleByName("DemoEC2");
  
  Console.WriteLine("For role {0}:", role.Name);
  Console.WriteLine("  Policies:");

  foreach (var policy in role.GetPolicies())
  {
    Console.WriteLine("    {0}", policy.Name);
  }

  Console.WriteLine("  InstanceProfiles:");

  foreach (var instanceProfile in role.GetInstanceProfiles())
  {
    Console.WriteLine("    {0}", instanceProfile.Name);
  }
}
catch (NoSuchEntityException)
{
  Console.WriteLine("Role 'DemoEC2' does not exist.");
}
```

The following example displays a list of all accessible roles\. For each role, its associated policies and instance profiles are also displayed:

```
// using Amazon.IdentityManagement.Resources;
      
var iam = new IdentityManagementService();
var roles = iam.GetRoles(); 

foreach (var role in roles)
{
  Console.WriteLine("For role {0}:", role.Name);
  Console.WriteLine("  Policies:");

  foreach (var policy in role.GetPolicies()) 
  {
    Console.WriteLine("    {0}", policy.Name);
  }

  Console.WriteLine("  InstanceProfiles:");

  foreach (var instanceProfile in role.GetInstanceProfiles()) 
  {
    Console.WriteLine("    {0}", instanceProfile.Name);
  }
}
```

## Create a User Account<a name="iam-resource-api-examples-create-user"></a>

The following example creates a new user account and then displays some information about it:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var user = iam.CreateUser("DemoUser");

  Console.WriteLine("User Name = '{0}', ARN = '{1}'", 
    user.Name, user.Arn);
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine("User 'DemoUser' already exists.");
}
```

## Create a Group<a name="iam-resource-api-examples-create-group"></a>

The following example creates a new group and then confirms whether the group was successfully created:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var group = iam.CreateGroup("DemoGroup");  

  Console.WriteLine(group.Name + " was created.");
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine("Group 'DemoGroup' already exists.");
}
```

## Create a Role<a name="iam-resource-api-examples-create-role"></a>

The following example creates a new role and then confirms whether the group was successfully created\.

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();
// GenerateAssumeRolePolicy() is a custom method.
string assumeRole = GenerateAssumeRolePolicy();

try
{
  var role = iam.CreateRole(new CreateRoleRequest
  {
    RoleName = "DemoEC2",
    AssumeRolePolicyDocument = assumeRole 
  });

  Console.WriteLine(role.Name + " was created.");
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine("Role 'DemoEC2' already exists.");
}
```

The preceding example relies on the following example to create the new policy\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating a policy document\. However, this example is presented for completeness:

```
public static string GenerateAssumeRolePolicy()
{
  // using Amazon.Auth.AccessControlPolicy;

  // Create a policy that looks like this:
  /* 
  {
    "Version": "2012-10-17",
    "Id": "DemoEC2Trust",
    "Statement": [
      {
        "Sid": "DemoEC2TrustStatement",
        "Effect": "Allow",
        "Principal": {
          "Service": "ec2.amazonaws.com"
        },
        "Action": "sts:AssumeRole"
      }
    ]
  } 
  */

  var action = new ActionIdentifier("sts:AssumeRole");
  var actions = new List<ActionIdentifier>();

  actions.Add(action);

  var principal = new Principal("ec2.amazonaws.com")
  {
    Provider = "Service"
  };
  var principals = new List<Principal>();

  principals.Add(principal);

  var statement = new Statement(Statement.StatementEffect.Allow)
  {
    Actions = actions,
    Id = "DemoEC2TrustStatement",
    Principals = principals
  };
  var statements = new List<Statement>();

  statements.Add(statement);

  var policy = new Policy
  {
    Id = "DemoEC2Trust",
    Version = "2012-10-17",
    Statements = statements
  };

  return policy.ToJson();
}
```

## Add a User Account to a Group<a name="iam-resource-api-examples-add-user-to-group"></a>

The following example adds an existing user account to an existing group and then displays a list of the group’s associated user accounts:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var group = iam.GetGroupByName("DemoGroup");

  group.AddUser("DemoUser");  

  Console.WriteLine("Users in group {0}:", group.Name);

  foreach (var user in group.GetUsers())
  {
    Console.WriteLine("  {0}", user.Name);
  }

}
catch (NoSuchEntityException)
{
  Console.WriteLine("Group 'DemoGroup' or " +
    "user 'DemoUser' does not exist.");
}
```

## Add a Policy to a User Account, Group, or Role<a name="iam-resource-api-examples-add-policy"></a>

### Add a Policy to a User Account<a name="iam-resource-api-examples-add-policy-user"></a>

The following example creates a new policy, adds the new policy to an existing user account, and then displays a list of the user account’s associated policies:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var user = iam.GetUserByName("DemoUser");
  // GenerateUserPolicyDocument() is a custom method.
  string policyDoc = GenerateUserPolicyDocument();

  user.CreatePolicy(policyDoc, "ListDeploymentsPolicy");  

  Console.WriteLine("Policies for user {0}:", user.Name);

  foreach (var policyItem in user.GetPolicies())
  {
    Console.WriteLine("  {0}", policyItem.Name);
  }

}
catch (NoSuchEntityException)
{
  Console.WriteLine("User 'DemoUser' does not exist.");
}
```

The preceding example relies on the following example to create the new policy\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating a policy document\. However, this example is presented for completeness:

```
public static string GenerateUserPolicyDocument()
{
  // using Amazon.Auth.AccessControlPolicy;

  // Create a policy that looks like this:
  /*
  {
    "Version" : "2012-10-17",
    "Id"  : "ListDeploymentsPolicy",
    "Statement" : [
      {
        "Sid" : "ListDeploymentsStatement",
        "Effect" : "Allow",
        "Action" : "codedeploy:ListDeployments",
        "Resource" : "*"
      }
    ]
  }
  */

  var action = new ActionIdentifier("codedeploy:ListDeployments");
  var actions = new List<ActionIdentifier>();

  actions.Add(action);

  var resource = new Resource("*");
  var resources = new List<Resource>();

  resources.Add(resource);

  var statement = new Statement(Statement.StatementEffect.Allow)
  {
    Actions = actions,
    Id = "ListDeploymentsStatement",
    Resources = resources
  };
  var statements = new List<Statement>();

  statements.Add(statement);

  var policy = new Policy
  {
    Id = "ListDeploymentsPolicy",
    Version = "2012-10-17",
    Statements = statements
  };

  return policy.ToJson();
}
```

### Add a Policy to a Group<a name="iam-resource-api-examples-add-policy-group"></a>

The following example creates a new policy, adds the new policy to an existing group, and then displays a list of the group’s associated policies:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var group = iam.GetGroupByName("DemoGroup");
  // GenerateGroupPolicyDocument() is a custom method.
  string policyDoc = GenerateGroupPolicyDocument();

  group.CreatePolicy(policyDoc, "ListDeploymentConfigsPolicy");

  Console.WriteLine("Policies for group {0}:", group.Name);

  foreach (var policyItem in group.GetPolicies())
  {
    Console.WriteLine("  {0}", policyItem.Name);
  }

}
catch (NoSuchEntityException)
{
  Console.WriteLine("Group 'DemoGroup' does not exist.");
}
```

The preceding example relies on the following example to create the new policy\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating a policy document\. However, this example is presented for completeness:

```
public static string GenerateGroupPolicyDocument()
{
  // using Amazon.Auth.AccessControlPolicy;

  // Create a policy that looks like this:
  /*
  {
    "Version" : "2012-10-17",
    "Id": "ListDeploymentConfigsPolicy",
    "Statement" : [
      {
        "Sid" : "ListDeploymentConfigsStatement",
        "Effect" : "Allow",
        "Action" : "codedeploy:ListDeploymentConfigs",
        "Resource" : "*"
      }
    ]
  }
  */

  var action = new ActionIdentifier("codedeploy:ListDeploymentConfigs");
  var actions = new List<ActionIdentifier>();

  actions.Add(action);

  var resource = new Resource("*");
  var resources = new List<Resource>();

  resources.Add(resource);

  var statement = new Statement(Statement.StatementEffect.Allow)
  {
    Actions = actions,
    Id = "ListDeploymentConfigsStatement",
    Resources = resources
  };
  var statements = new List<Statement>();

  statements.Add(statement);

  var policy = new Policy
  {
    Id = "ListDeploymentConfigsPolicy",
    Version = "2012-10-17",
    Statements = statements
  };

  return policy.ToJson();
}
```

### Add a Policy to a Role<a name="iam-resource-api-examples-add-policy-role"></a>

The following example creates a new policy and then adds the new policy to an existing role\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support adding a policy to a role\. However, this example is presented for completeness:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model;

var client = new AmazonIdentityManagementServiceClient();
// GenerateRolePolicyDocument() is a custom method.
string policyDoc = GenerateRolePolicyDocument();

var request = new PutRolePolicyRequest
{
  RoleName = "DemoEC2",
  PolicyName = "DemoEC2Permissions",
  PolicyDocument = policyDoc
};

try
{
  client.PutRolePolicy(request);
}
catch (NoSuchEntityException)
{
  Console.WriteLine
    ("Role 'DemoEC2' or policy 'DemoEC2Permissions' does not exist.");
}
```

The preceding example relies on the following example to create the new policy\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating a policy document\. However, this example is presented for completeness:

```
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

  var statement = new Statement(Statement.StatementEffect.Allow)
  {
    Actions = actions,
    Id = "DemoEC2PermissionsStatement",
    Resources = resources
  };
  var statements = new List<Statement>();

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

## Create an Access Key for a User Account<a name="iam-resource-api-examples-create-access-key"></a>

The following example creates an access key for a user account and then displays the access key’s ID and secret access key:

```
// using Amazon.IdentityManagement.Resources;
// using Amazon.IdentityManagement.Model;

var iam = new IdentityManagementService();

try
{
  var user = iam.GetUserByName("DemoUser");
  var accessKey = user.CreateAccessKey();
  
  Console.WriteLine("For user {0}:", user.Name);
  Console.WriteLine("  Access key = {0}", accessKey.Id);
  // This is the only time that the secret access key will be displayed.
  Console.WriteLine("  Secret access key = {0}", accessKey.SecretAccessKey);
}
catch (NoSuchEntityException)
{
  Console.WriteLine("User 'DemoUser' does not exist.");
}
catch (LimitExceededException)
{
  Console.WriteLine("You can have only 2 access keys per user.");
}
```

## Create a Login Profile for a User Account<a name="iam-resource-api-examples-create-login-profile"></a>

The following example creates a login profile for a user account\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating a login profile for a user account\. However, this example is presented for completeness:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model; 

var client = new AmazonIdentityManagementServiceClient();
var request = new CreateLoginProfileRequest
{
  UserName = "DemoUser",
  Password = "ksdD9JHm",
  PasswordResetRequired = true
};

try
{
  client.CreateLoginProfile(request); 
}
catch (NoSuchEntityException)
{
  Console.WriteLine("User 'DemoUser' doesn't exist.");
}
```

## Create an Instance Profile<a name="iam-resource-api-examples-create-instance-profile"></a>

The following example creates an instance profile\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support creating an instance profile\. However, this example is presented for completeness:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model; 

var client = new AmazonIdentityManagementServiceClient();
var request = new CreateInstanceProfileRequest
{
  InstanceProfileName = "DemoEC2-InstanceProfile"
};

try
{
  client.CreateInstanceProfile(request);
}
catch (EntityAlreadyExistsException)
{
  Console.WriteLine(
    "The instance profile 'DemoEC2-InstanceProfile' already exists.");
}
```

## Attach an Instance Profile to a Role<a name="iam-resource-api-examples-attach-instance-profile"></a>

The following example attaches an instance profile to a role\.

The following example doesn’t use the AWS Resource APIs for \.NET, as the resource APIs currently don’t support attaching an instance profile to a role\. However, this example is presented for completeness:

```
// using Amazon.IdentityManagement;
// using Amazon.IdentityManagement.Model; 

var client = new AmazonIdentityManagementServiceClient();
var request = new AddRoleToInstanceProfileRequest
{
  RoleName = "DemoEC2",
  InstanceProfileName = "DemoEC2-InstanceProfile"
};

try
{
  client.AddRoleToInstanceProfile(request);
}
catch (NoSuchEntityException)
{
  Console.WriteLine(
    "The role 'DemoEC2' or the instance profile " +
    "'DemoEC2-InstanceProfile' does not exist.");
}
catch (LimitExceededException)
{
  Console.WriteLine("The role 'DemoEC2' already has " + 
    "an instance profile attached.");
}
```