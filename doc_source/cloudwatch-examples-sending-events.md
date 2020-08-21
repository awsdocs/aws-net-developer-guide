--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](../../latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Sending Events to Amazon CloudWatch Events<a name="cloudwatch-examples-sending-events"></a>

This \.NET code example shows you how to:
+ Create and update a scheduled rule to trigger an event
+ Add a AWS Lambda function target to respond to an event
+ Send events that are matched to targets

## The Scenario<a name="the-scenario"></a>

Amazon CloudWatch Events delivers a near real\-time stream of system events that describe changes in AWS resources to various targets\. Using simple rules, you can match events and route them to one or more target functions or streams\. This \.NET example shows you how to create and update a rule used to trigger an event, define one or more targets to respond to an event, and send events that are matched to targets for handling\.

The code manages instances using these methods of the [AmazonCloudWatchEventsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TCloudWatchEventsClient.html) class:
+  [PutRule](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutRulePutRuleRequest.html) 
+  [PutTargets](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutTargetsPutTargetsRequest.html) 
+  [PutEvents](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutEventsPutEventsRequest.html) 

For more information about Amazon CloudWatch Events, see [Adding Events with PutEvents](https://docs.aws.amazon.com/AmazonCloudWatch/latest/events/AddEventsPutEvents.html) in the Amazon CloudWatch Events User Guide\.

## Prerequisite Tasks<a name="prerequisite-tasks"></a>

To set up and run this example, you must first:
+  [Get set up to use Amazon CloudWatch](https://docs.aws.amazon.com/AmazonCloudWatch/latest/monitoring/GettingSetup.html)\.
+ [Set up and configure the AWS SDK for \.NET\.](net-dg-setup.md)
+ Create a Lambda function using the hello\-world blueprint to serve as the target for events\. To learn how, see [Step 1: Create an AWS Lambda function](https://docs.aws.amazon.com/lambda/latest/dg/tutorial-scheduled-events-create-function.html) in the Amazon CloudWatch Events User Guide\.

## Create an IAM Role to Run the Examples<a name="create-an-iam-role-to-run-the-examples"></a>

The following examples require an IAM role whose policy grants permission to CloudWatch Events and that includes `events.amazonaws.com` as a trusted entity\. This example creates a role named CWEvents, setting it’s trust relationship and role policy\.

```
static void Main()
{
    var client = new AmazonIdentityManagementServiceClient();
    // Create a role and it's trust relationship policy
    var role = client.CreateRole(new CreateRoleRequest
    {
        RoleName = "CWEvents",
        AssumeRolePolicyDocument =
        @"{""Statement"":[{""Principal"":{""Service"":[""events.amazonaws.com""]}," +
        @"""Effect"":""Allow"",""Action"":[""sts:AssumeRole""]}]}"
    }).Role;
    // Create a role policy and add it to the role
    string policy = GenerateRolePolicyDocument();
    var request = new CreatePolicyRequest
    {
        PolicyName = "DemoCWPermissions",
        PolicyDocument = policy
    };
    try
    {
        var createPolicyResponse = client.CreatePolicy(request);
    }
    catch (EntityAlreadyExistsException)
    {
        Console.WriteLine
          ("Policy 'DemoCWPermissions' already exits.");
    }
    var request2 = new AttachRolePolicyRequest()
    {
        PolicyArn = "arn:aws:iam::192484417122:policy/DemoCWPermissions",
        RoleName = "CWEvents"
    };
    try
    {
        var response = client.AttachRolePolicy(request2);    //managedpolicy
        Console.WriteLine("Policy DemoCWPermissions attached to Role TestUser");
    }
    catch (NoSuchEntityException)
    {
        Console.WriteLine
          ("Policy 'DemoCWPermissions' does not exist");
    }
    catch (InvalidInputException)
    {
        Console.WriteLine
          ("One of the parameters is incorrect");
    }

}
public static string GenerateRolePolicyDocument()
{
    /* This method produces the following managed policy:
       "Version": "2012-10-17",
       "Statement": [
          {
             "Sid": "CloudWatchEventsFullAccess",
             "Effect": "Allow",
             "Action": "events:*",
             "Resource": "*"
          },
          {
             "Sid": "IAMPassRoleForCloudWatchEvents",
             "Effect": "Allow",
             "Action": "iam:PassRole",
             "Resource": "arn:aws:iam::*:role/AWS_Events_Invoke_Targets"
          }
       ]
    }
    */
    var actionList = new ActionIdentifier("events:*");
    var actions = new List<ActionIdentifier>();
    actions.Add(actionList);
    var resource = new Resource("*");
    var resources = new List<Resource>();
    resources.Add(resource);
    var statement = new Amazon.Auth.AccessControlPolicy.Statement
        (Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
    {
        Actions = actions,
        Id = "CloudWatchEventsFullAccess",
        Resources = resources
    };
    var statements = new List<Amazon.Auth.AccessControlPolicy.Statement>();
    statements.Add(statement);
    var actionList2 = new ActionIdentifier("iam:PassRole");
    var actions2 = new List<ActionIdentifier>();
    actions2.Add(actionList2);
    var resource2 = new Resource("arn:aws:iam::*:role/AWS_Events_Invoke_Targets");
    var resources2 = new List<Resource>();
    resources2.Add(resource2);
    var statement2 = new Amazon.Auth.AccessControlPolicy.Statement(Amazon.Auth.AccessControlPolicy.Statement.StatementEffect.Allow)
    {
        Actions = actions2,
        Id = "IAMPassRoleForCloudWatchEvents",
        Resources = resources2
    };

    statements.Add(statement2);
    var policy = new Policy
    {
        Id = "DemoEC2Permissions",
        Version = "2012-10-17",
        Statements = statements
    };
    return policy.ToJson();
}
```

## Create a Scheduled Rule<a name="create-a-scheduled-rule"></a>

Create an [AmazonCloudWatchEventsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TCloudWatchEventsClient.html) instance and a [PutRuleRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TPutRuleRequest.html) object containing the parameters needed to specify the new scheduled rule, which include the following:
+ A name for the rule
+ The ARN of the IAM role you created previously
+ An expression to schedule triggering of the rule every five minutes

Call the [PutRule](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutRulePutRuleRequest.html) method to create the rule\. The [PutRuleResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TPutRuleResponse.html) object returns the ARN of the new or updated rule\.

```
AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

var putRuleRequest = new PutRuleRequest
{
    Name = "DEMO_EVENT",
    RoleArn = "IAM_ROLE_ARN",
    ScheduleExpression = "rate(5 minutes)",
    State = RuleState.ENABLED
};

var putRuleResponse = client.PutRule(putRuleRequest);
Console.WriteLine("Successfully set the rule {0}", putRuleResponse.RuleArn);
```

## Add a Lambda Function Target<a name="add-a-lam-function-target"></a>

Create an [AmazonCloudWatchEventsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TCloudWatchEventsClient.html) instance and a [PutTargetsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TPutTargetsRequest.html) object containing the parameters needed to specify the rule to which you want to attach the target, including the ARN of the Lambda function you created\. Call the [PutTargets](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutTargetsPutTargetsRequest.html) method of the `AmazonCloudWatchClient` instance\.

```
AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

var putTargetRequest = new PutTargetsRequest
{
    Rule = "DEMO_EVENT",
    Targets =
    {
        new Target { Arn = "LAMBDA_FUNCTION_ARN", Id = "myCloudWatchEventsTarget"}
    }
};
client.PutTargets(putTargetRequest);
```

## Send Events<a name="send-events"></a>

Create an [AmazonCloudWatchEventsClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TCloudWatchEventsClient.html) instance and a [PutEventsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/TPutEventsRequest.html) object containing the parameters needed to send events\. For each event, include the source of the event, the ARNs of any resources affected by the event, and details for the event\. Call the [PutEvents](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/CloudWatchEvents/MCloudWatchEventsPutEventsPutEventsRequest.html) method of the `AmazonCloudWatchClient` instance\.

```
AmazonCloudWatchEventsClient client = new AmazonCloudWatchEventsClient();

var putEventsRequest = new PutEventsRequest
{
    Entries = new List<PutEventsRequestEntry>
    {
        new PutEventsRequestEntry
        {
            Detail = @"{ ""key1"" : ""value1"", ""key2"" : ""value2"" }",
            DetailType = "appRequestSubmitted",
            Resources =
            {
                "RESOURCE_ARN"
            },
            Source = "com.compnay.myapp"
        }
    }
};
client.PutEvents(putEventsRequest);
```