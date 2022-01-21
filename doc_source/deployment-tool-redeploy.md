--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Redeploying your application after changes<a name="deployment-tool-redeploy"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

When you make changes to your application, you can use the deployment tool to redeploy it\. To do so, you go to the application's project directory and run `dotnet aws deploy` again\.

At a certain point in the redeployment, the tool displays **Select the AWS stack to deploy your application to**\. Scan the list of deployment stacks and do one of the following:
+ Choose the stack name that corresponds to your \.NET application\.

  If you do this, the settings from the last deployment of the application are shown\. Only a few of the advanced settings can be changed, the rest are read\-only\.
+ Choose the last option, **Create new**, and then enter a new name\.

  If you do this, the tool will create a new deployment stack and you can decide what you want to do with the settings: accept the defaults or make changes\.