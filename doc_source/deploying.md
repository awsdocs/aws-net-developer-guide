# Deploying applications to AWS<a name="deploying"></a>

After you've developed your cloud\-native \.NET Core application or service on a development machine, you'll want to deploy it to AWS\. You can do this by using the AWS Management Console or certain services like AWS CloudFormation or AWS Cloud Development Kit \(CDK\)\. You can also use AWS tools that have been created for the purpose of deployment\. By using these tools, you can do the following\.

## Deploy from the \.NET CLI<a name="deploying-from-net-cli"></a>

You can use the following AWS tools for \.NET CLI to deploy your applications to AWS:
+ [AWS Deploy Tool for \.NET CLI](https://www.nuget.org/packages/AWS.Deploy.Tools) \- Supports deployments of ASP\.NET Core, \.NET Console, and Blazor WebAssembly applications\.
+ [AWS Lambda Tools for \.NET CLI](https://www.nuget.org/packages/Amazon.Lambda.Tools) \- Supports deployments of AWS Lambda projects\.

## Deploy from the IDE toolkits<a name="deploying-from-toolkits"></a>

You can use AWS toolkits to deploy your applications directly from the IDE of your choice:
+ **[AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/deployment-chapt.html)**
**Note**  
The "Publish to AWS" feature in the toolkit exposes the same functionality as the AWS Deploy Tool for \.NET CLI\. To learn more, go to [Publish to AWS](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/publish-experience.html) in the *AWS Toolkit for Visual Studio User Guide*\.
+ **[AWS Toolkit for JetBrains](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/welcome.html)**

  See [Work with AWS Serverless Applications](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/key-tasks.html#key-tasks-sam) and [Work with AWS App Runner](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/key-tasks.html#key-tasks-app-runner)\.
+ **[AWS Toolkit for VS Code](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/welcome.html)**

  See [Working with serverless applications](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/serverless-apps.html) and [Using AWS App Runner](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/using-apprunner.html)\.
+ **[AWS Toolkit for Azure DevOps](https://docs.aws.amazon.com/vsts/latest/userguide/tutorial-eb.html)**

## Use cases<a name="w155aac17b9"></a>

The following sections contain use case scenarios for certain types of applications, including information about how you would use the \.NET CLI to deploy those applications\.
+ [ASP\.NET Core apps](deploying-asp-net.md)
+ [\.NET Console apps](deploying-console.md)
+ [Blazor WebAssembly apps](deploying-blazor.md)
+ [AWS Lambda projects](deploying-lambda.md)