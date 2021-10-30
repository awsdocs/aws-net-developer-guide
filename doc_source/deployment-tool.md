--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/BannerButton.png) ](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# AWS \.NET deployment tool for the \.NET CLI<a name="deployment-tool"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

After you've developed your cloud\-native \.NET Core application on a development machine, you'll most likely want to deploy it to AWS\.

Deployment to AWS sometimes involves multiple AWS services and resources, each of which must be configured\. To ease this deployment work, you can use the AWS \.NET deployment tool for the \.NET CLI, or *deployment tool* for short\.

**Note**  
If you develop on Windows with Visual Studio, you might have the AWS Toolkit for Visual Studio installed\. The toolkit provides similar deployment functionality in its **Publish to AWS** feature\. For information about toolkit versions and using the feature, see [Publish to AWS](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/publish-experience.html) in the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\.

When you run the deployment tool for a \.NET Core application, the tool shows you all of the AWS compute\-service options that are available to deploy your application\. It suggests the most likely choice, as well as the most likely settings to go along with that choice\. It then builds and packages your application as required by the chosen compute service\. It generates the deployment infrastructure, deploys your application by using the AWS Cloud Development Kit \(CDK\), and then displays the endpoint\.

You can select deployment options interactively or specify them in a [JSON configuration file](deployment-tool-config-file.md)\. You can also keep the default values that the tool selects for you\.

**Capabilities**
+ Deploys to AWS Elastic Beanstalk or Amazon ECS \(using AWS Fargate\)\.
+ Deploys cloud\-native \.NET applications that are built with \.NET Core 2\.1 and later, and that are written with the intent to deploy to Linux\. Such an application isn't tied to any Windows\-specific technology such as the Windows Registry, IIS, or MSMQ, and can be deployed on virtualized compute\.
+ Deploys ASP\.NET Core web apps, Blazor WebAssembly apps, long\-running service apps, and scheduled tasks\. For more information, see the [README](https://github.com/aws/aws-dotnet-deploy#supported-application-types) in the GitHub repo\.

**Additional information**
+ The [aws\-dotnet\-deploy](https://github.com/aws/aws-dotnet-deploy) GitHub repo\.
+ Blog post [Reimagining the AWS \.NET deployment experience](http://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/)\.
+ Blog post [Update on our new AWS \.NET Deployment Experience](https://aws.amazon.com/blogs/developer/update-new-net-deployment-experience/)\.
+ Blog post [Deployment Projects with the new AWS \.NET Deployment Experience](https://aws.amazon.com/blogs/developer/dotnet-deployment-projects/)\.

**Topics**
+ [Setting up your environment](deployment-tool-setup-env.md)
+ [Setting up the tool](deployment-tool-setup.md)
+ [Setting up credentials](deployment-tool-setup-creds.md)
+ [Running the tool](deployment-tool-run.md)
+ [Deploying: Tutorials](deployment-tool-deploy.md)
+ [Redeploying](deployment-tool-redeploy.md)
+ [Deployment settings](deployment-tool-settings.md)
+ [Configuration files](deployment-tool-config-file.md)