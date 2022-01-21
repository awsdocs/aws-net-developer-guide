--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Running the deployment tool<a name="deployment-tool-run"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

To help you become familiar with the deployment tool, this topic contains a quick tour of the tool's command line\.

## Command line help<a name="deployment-tool-run-help"></a>

For basic help, run the following:

```
dotnet aws --help
```

The available commands are listed in the **Commands** section of the help text; for example, `deploy` and `list-deployments`\.

To get help on a specific command, use `--help` in the context of that command\. For example:

```
dotnet aws deploy --help
```

## Some common commands<a name="deployment-tool-run-common"></a>

To deploy or redeploy an application:

```
cd <dotnet_core_app_directory>
dotnet aws deploy [--profile <profile_name>] [--region <region_code>] [--apply <configuration_file>] [-s|--silent]
```

To show a list of the CloudFormation stacks that you've created by using the tool:

```
dotnet aws list-deployments [—region <region_code>] [--profile <profile_name>]
```