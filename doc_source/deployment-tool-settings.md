--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools) for a simplified deployment experience\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

Read our [original blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) as well as the [update post](https://aws.amazon.com/blogs/developer/update-new-net-deployment-experience/) and the [post on deployment projects](https://aws.amazon.com/blogs/developer/dotnet-deployment-projects/)\. Submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\! For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Deployment settings available through the deployment tool<a name="deployment-tool-settings"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

One of the steps during deployment or redeployment is to choose the compute service you want to use, which is often the deployment tool's default suggestion\. After you have chosen the deployment option you want, the tool shows you a list of the common settings that are available for that option\. Advanced settings are also available, which you can see by entering "`more`" at the command prompt\.

Many of the settings are self\-explanatory\. The following settings are less obvious\.

**Deployment to AWS Elastic Beanstalk**
+ Application IAM Role

  The **Application IAM Role** provides AWS credentials for your application, which has been deployed to AWS on your behalf\. With an IAM role, the application can access AWS resources like Amazon S3 buckets\. 

  Using this setting, you can choose from existing IAM roles or allow the tool to create a role specifically for your application, which is the default behavior\. You can see the definitions of the existing IAM roles by opening the IAM console at [https://console\.aws\.amazon\.com/iam/](https://console.aws.amazon.com/iam/) and choosing the **Roles** page\.
+ EC2 Instance Type

  Using this setting, you can specify an Amazon EC2 instance type other than the one that the tool suggests\. Find the available instance types by opening the Amazon EC2 console at [https://console\.aws\.amazon\.com/ec2/](https://console.aws.amazon.com/ec2/) and choosing the **Instance Types** page under **Instances**\. For detailed descriptions of EC2 instance types, see [https://aws\.amazon\.com/ec2/instance\-types/](https://aws.amazon.com/ec2/instance-types/)\.
+ Key Pair

  The default for this setting is "EMPTY" \(no key pair\), meaning that you won't be able to SSH into the EC2 instance\. If you want to SSH into the EC2 instance you can choose a key pair from the list\. You can see the key pairs in the list by opening the [EC2 console](https://console.aws.amazon.com/ec2) and choosing the **Key Pairs** page under **Network & Security**\. You can also have the tool create a new key pair\. If you choose this option, enter a name for the key pair and \(when asked\) a directory where the private key will be stored\.
**Warning**  
If you chose to create a new key pair and store it on your computer, take appropriate precautions to protect the key pair\.

**Deployment to Amazon ECS using AWS Fargate**
+ Application IAM Role

  The **Application IAM Role** provides AWS credentials for your application, which has been deployed to AWS on your behalf\. With an IAM role, the application can access AWS resources like Amazon S3 buckets\. 

  Using this setting, you can choose from existing IAM roles or allow the tool to create a role specifically for your application, which is the default behavior\. You can see the definitions of the existing IAM roles by opening the IAM console at [https://console\.aws\.amazon\.com/iam/](https://console.aws.amazon.com/iam/) and choosing the **Roles** page\. If no IAM roles are listed, it means that no appropriate IAM role exists, so the only choice is for the tool to create one\.
+ Virtual Private Cloud

  The default value for this setting is the account Default VPC, which you can find by opening the VPC console at [https://console\.aws\.amazon\.com/vpc/](https://console.aws.amazon.com/vpc/) and choosing **Your VPCs**\. The account Default VPC has **Yes** in the **Default VPC** column of the VPC table\. You might have to scroll horizontally to see that column\. You can choose another VPC, or you can have the tool create a new VPC for your application\.