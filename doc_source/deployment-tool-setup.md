# Setting up the deployment tool<a name="deployment-tool-setup"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

The following instructions show you how to install, update, and uninstall the deployment tool\.

**To install the deployment tool**

1. Open a command prompt or terminal\.

1. Install the tool: `dotnet tool install --global aws.deploy.cli`

1. Verify the installation by checking the version: `dotnet aws --version`

**To update the deployment tool**

1. Open a command prompt or terminal\.

1. Check the version: `dotnet aws --version`

1. \(Optional\) Check to see if a later version of the tool is available at [https://www\.nuget\.org/packages/aws\.deploy\.cli/](https://www.nuget.org/packages/aws.deploy.cli/)\.

1. Update the tool: `dotnet tool update -g aws.deploy.cl`i

1. Verify the installation by checking the version again: `dotnet aws --version`

**To remove the deployment tool from your system**

1. Open a command prompt or terminal\.

1. Uninstall the tool: `dotnet tool uninstall -g aws.deploy.cli`

1. Verify that the tool is no longer installed: `dotnet aws --version`