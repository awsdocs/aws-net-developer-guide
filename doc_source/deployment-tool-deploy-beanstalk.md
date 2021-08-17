--------

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Deploying a webapp to Elastic Beanstalk<a name="deployment-tool-deploy-beanstalk"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

This tutorial shows a deployment to Elastic Beanstalk\. The tutorial uses the defaults that are provided by the deployment tool\.

## Prerequisites<a name="dt-deploy-beanstalk-prereq"></a>
+ You have completed [environment setup](deployment-tool-setup-env.md) and [tool setup](deployment-tool-setup.md)\.
+ The `[default]` credentials profile has the required permissions\.

## Deploy<a name="dt-deploy-beanstalk-deploy"></a>

When you're ready to deploy your application to AWS for the first time, this is where you start\.

**To deploy the example web app to Elastic Beanstalk**

1. Go to a directory where you want to work and create the basic web app:

   `dotnet new webapp --name SimpleWebAppForBeanstalk`

1. Go to the application directory:

   `cd SimpleWebAppForBeanstalk`

1. Run the deployment tool:

   `dotnet aws deploy`

1. In **Name the AWS stack to deploy your application to**, press the **Enter** key to accept the default name\. If there are existing stacks, this shows **Select the AWS stack to deploy your application to** instead\. In this case, choose the last option to **Create new**, then press the **Enter** key to accept the default name\.

1. For the next inquiry, **Choose deployment option**, choose the option for **AWS Elastic Beanstalk on Linux** and press the **Enter** key\. For this tutorial, this is the default option\.
**Note**  
When deploying a real application, if the tool doesn't find a Dockerfile for the project, the deployment tool displays **AWS Elastic Beanstalk on Linux** as the default option\. If there is a Dockerfile for the project, the tool displays a different default\. For more information about this alternative scenario, see [Deploying a webapp to Amazon ECS](deployment-tool-deploy-ecs.md)\.

1. Press the **Enter** key again to accept the defaults for application and stack settings and start the deployment\.

1. Wait for the deployment process to finish\.

1. At the end of the tool's output, you see the following line: "SimpleWebAppForBeanstalk\.EndpointURL\.\.\."\. This line contains the URL for the resulting web site\. You can open this URL in a web browser\. Alternatively, you can open the resulting web site from the Elastic Beanstalk console, as shown next\.

1. Sign in to the AWS Management Console and open the Elastic Beanstalk console at [https://console\.aws\.amazon\.com/elasticbeanstalk/](https://console.aws.amazon.com/elasticbeanstalk/)\.

   Select the appropriate AWS Region, if necessary\.

1. On the **Environments** page, choose the **SimpleWebAppForBeanstalk\-dev** environment\.

1. In the top section of the environment's page, verify that the **Health** status is **Ok**, and then open the link to see the resulting web site\. Leave this web site open for now\.

## Update and redeploy<a name="dt-deploy-beanstalk-redeploy"></a>

Now that you have deployed an application and can see the resulting web site, it's time make some changes to the content and redeploy the application\.

**To make changes to the web content and redeploy the application**

1. In the `Pages` sub\-directory of the tutorial project, open `Index.cshtml` in a text editor\.

1. Make some changes to the HTML content and save the file\.

1. In the main directory for the project, run the deployment tool again:

   `dotnet aws deploy`

1. In **Select the AWS stack to deploy your application to**, choose the stack name that corresponds to this tutorial and press the **Enter** key\. For this tutorial, this is **SimpleWebAppForBeanstalk**, and it's the default choice\.

1. Press the **Enter** key again to accept the same defaults as before and wait for the application to redeploy\.

1. In the [Elastic Beanstalk console](https://console.aws.amazon.com/elasticbeanstalk), look at the **SimpleWebAppForBeanstalk\-dev** environment again\. Verify that the **Health** status is **Ok**, and then refresh the application's web site to see your changes\.

## Cleanup<a name="dt-deploy-beanstalk-cleanup"></a>

To avoid unexpected costs, be sure to remove the tutorial's environment and application when you're finished with them\. 

You can also do this cleanup manually by using the Elastic Beanstalk console at [https://console\.aws\.amazon\.com/elasticbeanstalk](https://console.aws.amazon.com/elasticbeanstalk)\.

**To remove tutorial artifacts**

1. Get a list of the existing cloud applications:

   `dotnet aws list-deployments`\.

   The list includes the deployment for this tutorial: **SimpleWebAppForBeanstalk**\.

1. Delete the deployment:

   `dotnet aws delete-deployment SimpleWebAppForBeanstalk`

1. Enter "y" to confirm deletion and wait for the deployment to be deleted\.

1. In the [Elastic Beanstalk console](https://console.aws.amazon.com/elasticbeanstalk), look at the **Environments** and **Applications** pages to verify that the tutorial deployment has been deleted\.

1. Refresh the web site that had been created during the tutorial to verify that it's no longer available\.