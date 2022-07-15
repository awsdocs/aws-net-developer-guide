# AWS Lambda projects<a name="deploying-lambda"></a>

AWS Lambda is a compute service that lets you run code without provisioning or managing servers\. It runs your code on a high\-availability compute infrastructure and performs all of the administration of the compute resources\. For more information about Lambda, see [What is AWS Lambda?](https://docs.aws.amazon.com/lambda/latest/dg/welcome.html) in the *AWS Lambda Developer Guide*\.

You can deploy Lambda functions by using the \.NET command line interface \(CLI\)\.

**Topics**
+ [Prerequisites](#lambda-cli-prereqs)
+ [Available Lambda commands](#listing-the-lam-commands-available-through-the-cli)
+ [Steps to deploy](#publishing-a-net-core-lam-project-from-the-net-core-cli)

## Prerequisites<a name="lambda-cli-prereqs"></a>

Before you start using the \.NET CLI to deploy Lambda functions, you must meet the following prerequisites:
+ Check to see if you have the \.NET CLI installed\. For example: `dotnet --version`\. If needed, go to [https://dotnet\.microsoft\.com/download](https://dotnet.microsoft.com/download) to install it\.
+ Set up the \.NET CLI to work with Lambda\. For a description of how to do so, see [\.NET Core CLI](https://docs.aws.amazon.com/lambda/latest/dg/csharp-package-cli.html) in the *AWS Lambda Developer Guide*\. In that procedure, the following is the deployment command:

  ```
  dotnet lambda deploy-function MyFunction --function-role role
  ```

  If you're not sure how to create an IAM role for this exercise, don't include the `--function-role role` part and the tool will help you create a new role\.

## Available Lambda commands<a name="listing-the-lam-commands-available-through-the-cli"></a>

To list the Lambda commands that are available through the \.NET CLI, open a command prompt or terminal and enter `dotnet lambda --help`\. The command output will be similar to the following:

```
Amazon Lambda Tools for .NET applications
Project Home: https://github.com/aws/aws-extensions-for-dotnet-cli, https://github.com/aws/aws-lambda-dotnet

Commands to deploy and manage AWS Lambda functions:

        deploy-function         Command to deploy the project to AWS Lambda
        ...
        (etc.)

To get help on individual commands execute:
        dotnet lambda help <command>
```

The output lists all the commands that are currently available\.

## Steps to deploy<a name="publishing-a-net-core-lam-project-from-the-net-core-cli"></a>

The following instructions assume you've created an AWS Lambda \.NET project\. For the purposes of this procedure, the project is named "DotNetCoreLambdaTest"\.

1. Open a command prompt or terminal, and navigate to the folder containing your \.NET Lambda project file\.

1. Enter `dotnet lambda deploy-function`\.

1. If prompted, enter the AWS Region \(the Region to which your Lambda function will be deployed\)\.

1. When prompted, enter the name of the function to deploy, for example, "DotNetCoreLambdaTest"\. It can be the name of a function that already exists in your AWS account or one that hasn't been deployed there yet\.

1. When prompted, select or create the IAM role that Lambda will assume when executing the function\.

On successful completion, the message **New Lambda function created** is displayed\.

```
Executing publish command
...
(etc.)
New Lambda function created
```

If you deploy a function that already exists in your account, the deploy function asks only for the AWS Region \(if necessary\)\. In this case, the command output ends with "Updating code for existing function"\.

After your Lambda function is deployed, it's ready to use\. For more information, see [Examples of How to Use AWS Lambda](https://docs.aws.amazon.com/lambda/latest/dg/use-cases.html)\.

Lambda automatically monitors Lambda functions for you, reporting metrics through Amazon CloudWatch\. To monitor and troubleshoot your Lambda function, see [Monitoring and troubleshooting Lambda applications](https://docs.aws.amazon.com/lambda/latest/dg/monitoring-functions.html)\.