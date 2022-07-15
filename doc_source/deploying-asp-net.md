# ASP\.NET Core apps<a name="deploying-asp-net"></a>

The [AWS Deploy Tool](https://aws.github.io/aws-dotnet-deploy/) for the \.NET CLI helps you deploy your ASP\.NET applications and guides you through a deployment process\. It's an interactive tooling for the \.NET CLI that helps deploy \.NET applications with minimum AWS knowledge\.

The Deploy Tool has the following capabilities:
+ **Compute recommendations for your application** \- Get the compute recommendations and learn which AWS compute is best suited for your application\.
+ **Dockerfile generation** \- The tool generates a Dockerfile if needed, or uses an existing Dockerfile\.
+ **Auto packaging and deployment** – The tool builds the deployment artifacts, provisions the infrastructure by using a generated AWS CDK deployment project, and deploys your application to the chosen AWS compute\.
+ **Repeatable and shareable deployments** – You can generate and modify AWS CDK deployment projects to fit your specific use\-case\. You can also version control them and share with your team for repeatable deployments\.
+ **Help with learning AWS CDK for \.NET\!** \- The tool helps you gradually learn the underlying AWS tools that it is built on, such as the AWS CDK\.

The [AWS Deploy Tool](https://aws.github.io/aws-dotnet-deploy/) supports deploying ASP\.NET Core applications to the following AWS services:
+ **[Amazon ECS Service](https://aws.amazon.com/ecs/) using [AWS Fargate](https://aws.amazon.com/fargate/)** \- Supports deployments of web applications to Amazon Elastic Container Service \(Amazon ECS\) with compute power managed by AWS Fargate serverless compute engine\.
+ **[AWS App Runner](https://aws.amazon.com/apprunner/)** \- Supports deployments to a fully managed service that makes it easy for developers to deploy containerized web applications and APIs, at scale and with no prior infrastructure experience required\.
+ **[AWS Elastic Beanstalk](https://aws.amazon.com/elasticbeanstalk/)** \- Supports deployments to a service that makes it easy for developers to deploy web applications and APIs to a fully managed environment, at scale and with no prior infrastructure experience required\.

To learn more, see the [tool overview](https://aws.github.io/aws-dotnet-deploy/)\. To get started from there, navigate to **Documentation**, **Getting started**, and choose **[How to install](https://aws.github.io/aws-dotnet-deploy/docs/getting-started/installation/)** for installation instructions\.