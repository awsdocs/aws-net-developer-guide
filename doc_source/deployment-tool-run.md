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

## Most common sequences of commands<a name="deployment-tool-run-common"></a>

To deploy or redeploy an application:

```
cd <dotnet_core_app_directory>
dotnet aws deploy [--profile <profile_name>] [--region <region_code>]
```

To show a list of the CloudFormation stacks that you've created by using the tool:

```
dotnet aws list-deployments [—region <region_code>] [--profile <profile_name>]
```