--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools) for a simplified deployment experience\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

Read our [original blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) as well as the [update post](https://aws.amazon.com/blogs/developer/update-new-net-deployment-experience/) and the [post on deployment projects](https://aws.amazon.com/blogs/developer/dotnet-deployment-projects/)\. Submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\! For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Setting up credentials for the deployment tool<a name="deployment-tool-setup-creds"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

This information is about how to set up credentials for the deployment tool\. If you're looking for information about setting up credentials for your project, see [Configure AWS credentials](net-dg-config-creds.md) instead\.

To run the deployment tool against your AWS account, you must have a credential profile\. The profile must be set up with at least an access key ID and a secret access key for an AWS Identity and Access Management \(IAM\) user\. For information about how to do this, see [Create users and roles](net-dg-users-roles.md) and [Using the shared AWS credentials file](creds-file.md)\.

The credentials that you use to run the deployment tool must have permissions for certain services, depending on the tasks that you're trying to perform\. The following are some examples of the typical permissions that are required to run the tool\. Additional permissions might be required, depending on the type of application you're deploying and the services it uses\.


| Task | Permissions for services | 
| --- |--- |
| Display a list of AWS CloudFormation stacks \(list\-deployments\) | CloudFormation | 
| Deploy and redeploy to Elastic Beanstalk \(deploy\) | CloudFormation, Elastic Beanstalk | 
| Deploy and redeploy to Amazon ECS \(deploy\) | CloudFormation, Elastic Beanstalk, Elastic Container Registry | 

The deployment tool automatically uses the `[default]` profile from your [shared AWS config and credentials files](creds-file.md) if that profile exists\. You can change this behavior by specifying a profile for the tool to use, either system\-wide or in a particular context\.

To specify a system\-wide profile, do the following:
+ Specify the `AWS_PROFILE` environment variable globally, as appropriate for your operating system\. Be sure to reopen command prompts or terminals as necessary\. If the profile you specify doesn't include an AWS Region, the tool might ask you to choose one\.
**Warning**  
If you set the `AWS_PROFILE` environment variable globally for your system, other SDKs, CLIs, and tools will also use that profile\. If this behavior is unacceptable, specify a profile for a particular context instead\.

To specify a profile for a particular context, do one of the following:
+ Specify the `AWS_PROFILE` environment variable in the command prompt or terminal session from which you're running the tool \(as appropriate for your operating system\)\.
+ Specify the `--profile` and `--region` switches within the command\. For example: `dotnet aws list-stacks --region us-west-2`\. For additional information about the deployment tool's commands, see [Running the deployment tool](deployment-tool-run.md)\.
+ Specify nothing and the tool will ask you to choose a profile and an AWS Region\.