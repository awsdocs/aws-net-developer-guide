# \.NET Console apps<a name="deploying-console"></a>

The [AWS Deploy Tool](https://aws.github.io/aws-dotnet-deploy/) for the \.NET CLI helps you deploy your \.NET Console applications as a service or a scheduled task as a container image on Linux and guides you through a deployment process\. If your application doesn't have a Dockerfile, the tool automatically generates it, otherwise an existing Dockerfile is used\.

The Deploy Tool has the following capabilities:
+ **Compute recommendations for your application** \- Get the compute recommendations and learn which AWS compute is best suited for your application\.
+ **Dockerfile generation** \- The tool generates a Dockerfile if needed, or uses an existing Dockerfile\.
+ **Auto packaging and deployment** – The tool builds the deployment artifacts, provisions the infrastructure by using a generated AWS CDK deployment project, and deploys your application to the chosen AWS compute\.
+ **Repeatable and shareable deployments** – You can generate and modify AWS CDK deployment projects to fit your specific use\-case\. You can also version control them and share with your team for repeatable deployments\.
+ **Help with learning AWS CDK for \.NET\!** \- The tool helps you gradually learn the underlying AWS tools that it is built on, such as the AWS CDK\.

The [AWS Deploy Tool](https://aws.github.io/aws-dotnet-deploy/) supports deploying \.NET Console applications to the following AWS services:
+ **[Amazon ECS Service](https://aws.amazon.com/ecs/) using [AWS Fargate](https://aws.amazon.com/fargate/)** \- Supports deployments of \.NET applications as a service \(for example, a background processor\) to Amazon Elastic Container Service \(Amazon ECS\) with compute power managed by AWS Fargate serverless compute engine\.
+ **[Amazon ECS Scheduled Task](https://aws.amazon.com/ecs/) using [AWS Fargate](https://aws.amazon.com/fargate/)** \- Supports deployments of \.NET applications as a scheduled task \(for example, end\-of\-day process\) to Amazon ECS with compute power managed by AWS Fargate serverless compute engine\.

To learn more, see the [tool overview](https://aws.github.io/aws-dotnet-deploy/)\. To get started from there, navigate to **Documentation**, **Getting started**, and choose **[How to install](https://aws.github.io/aws-dotnet-deploy/docs/getting-started/installation/)** for installation instructions\.