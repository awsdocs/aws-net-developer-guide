# Blazor WebAssembly apps<a name="deploying-blazor"></a>

The [AWS Deploy Tool](https://aws.github.io/aws-dotnet-deploy/) for the \.NET CLI helps you host your Blazor WebAssembly application in Amazon S3, using Amazon CloudFront for content network delivery\. Your app is deployed to an S3 bucket for web hosting\. The tool creates and configures an S3 bucket, and then uploads your Blazor application to the bucket\.

The Deploy Tool has the following capabilities:
+ **Auto packaging and deployment** – The tool builds the deployment artifacts, provisions the infrastructure by using a generated AWS CDK deployment project, and deploys your application to the chosen AWS compute\.
+ **Repeatable and shareable deployments** – You can generate and modify AWS CDK deployment projects to fit your specific use case\. You can also version control your projects and share them with your team for repeatable deployments\.
+ **Help with learning AWS CDK for \.NET** \- The tool helps you gradually learn the underlying AWS tools that it is built on, such as the AWS CDK\.

To learn more, see the [tool overview](https://aws.github.io/aws-dotnet-deploy/)\. To get started from there, navigate to **Documentation**, **Getting started**, and choose **[How to install](https://aws.github.io/aws-dotnet-deploy/docs/getting-started/installation/)** for installation instructions\.