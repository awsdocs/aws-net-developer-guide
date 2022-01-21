--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Deploying a webapp to Amazon ECS<a name="deployment-tool-deploy-ecs"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

This tutorial shows a deployment to Amazon ECS using AWS Fargate\. The tutorial uses the defaults that are provided by the deployment tool\.

## Prerequisites<a name="dt-deploy-ecs-prereq"></a>
+ You have completed [environment setup](deployment-tool-setup-env.md) and [tool setup](deployment-tool-setup.md)\.
+ The `[default]` credentials profile has the required permissions\.
+ Docker is installed and running, but no Dockerfile exists for the tutorial\.

## Deploy<a name="dt-deploy-ecs-deploy"></a>

When you're ready to deploy your application to AWS for the first time, this is where you start\.

**To deploy the example web app to Amazon ECS using AWS Fargate**

1. Go to a directory where you want to work and create the basic web app:

   `dotnet new webapp --name SimpleWebAppForECS`

1. Go to the application directory:

   `cd SimpleWebAppForECS`

1. Run the deployment tool:

   `dotnet aws deploy`

1. In **Name the AWS stack to deploy your application to**, press the **Enter** key to accept the default name\. If there are existing stacks, this shows **Select the AWS stack to deploy your application to** instead\. In this case, choose the last option to **Create new**, then press the **Enter** key to accept the default name\.

1. For the next inquiry, **Choose deployment option**, choose the option for **Amazon ECS using Fargate** and press the **Enter** key\. For this tutorial, this is the second option \(under **Additional Deployment Options**\), not the default\.
**Note**  
When deploying a real application, if the deployment tool finds a Dockerfile for the project, it displays **Amazon ECS using Fargate** as the default option\. If there is no Dockerfile for the project, but you choose the **Amazon ECS using Fargate** option, the tool generates a Dockerfile\.

1. Press the **Enter** key again to accept the defaults for application and stack settings and start the deployment\. For this tutorial, since no Dockerfile was found for the project but the **Amazon ECS using Fargate** option was chosen, the tool also generates a Dockerfile\.

1. Wait for the deployment process to finish\.

1. At the end of the tool's output, you see the following line: "SimpleWebAppForECS\.FargateServiceServiceURL\.\.\."\. This line contains the URL for the resulting web site\. Open the URL in a web browser to see resulting web site\. Leave this web site open for now\.

1. If you want to see the resources that were created by the tool, open the Amazon ECS console at [https://console\.aws\.amazon\.com/ecs/](https://console.aws.amazon.com/ecs/)\. Select the appropriate AWS Region, if necessary\. On the **Clusters** page you can see the new cluster that was created: **SimpleWebAppForECS**\.

## Update and redeploy<a name="dt-deploy-ecs-redeploy"></a>

Now that you have deployed an application and can see the resulting web site, it's time make some changes to the content and redeploy the application\.

**To make changes to the web content and redeploy the application**

1. In the `Pages` sub\-directory of the tutorial project, open `Index.cshtml` in a text editor\. 

1. Make some changes to the HTML content and save the file\.

1. In the main directory for the project, run the deployment tool again:

   `dotnet aws deploy`

1. In **Select the AWS stack to deploy your application to**, choose the stack name that corresponds to this tutorial and press the **Enter** key\. For this tutorial, this is **SimpleWebAppForECS**, and it's the default choice\.

1. Press the **Enter** key again to accept the same defaults as before and wait for the application to redeploy\.

1. Refresh the application's web site to see your changes\.

## Cleanup<a name="dt-deploy-ecs-cleanup"></a>

To avoid unexpected costs, be sure to remove the tutorial's clusters, tasks, and ECR repositories when you're finished with them\. 

You can also do this cleanup manually by using the Amazon ECS console at [https://console\.aws\.amazon\.com/ecs/](https://console.aws.amazon.com/ecs/)\.

**To remove tutorial artifacts**

1. Get a list of the existing cloud applications:

   `dotnet aws list-deployments`

   The list includes the deployment for this tutorial: **SimpleWebAppForECS**\.

1. Delete the deployment:

   `dotnet aws delete-deployment SimpleWebAppForECS`

1. Enter "y" to confirm deletion and wait for the deployment to be deleted\.

1. In the [Amazon ECS console](https://console.aws.amazon.com/ecs/), look at the **Clusters** and **Task Definitions** pages to verify that the tutorial deployment has been deleted\.

1. \(Optional\) On the **Amazon ECR**, **Repositories** page, you can delete the repository that was created for the tutorial: `simplewebappforecs`

1. Refresh the web site that had been created during the tutorial to verify that it's no longer available\.