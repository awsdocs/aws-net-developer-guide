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