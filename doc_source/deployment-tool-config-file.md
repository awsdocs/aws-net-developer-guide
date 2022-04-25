--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.Tools/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [https://github.com/aws/aws-dotnet-deploy/](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Working with configuration files<a name="deployment-tool-config-file"></a>


****  

|  | 
| --- |
| This is prerelease documentation for a feature in preview release\. It is subject to change\. | 

When you run the `deploy` command of the AWS \.NET deployment tool, you can select deployment options in response to prompts from the tool\.

Alternatively, you can provide a JSON configuration file by using the `--apply` option of the command\. The tool reads deployment options from the JSON parameters that are specified in the file, and uses those options in its prompts\. It uses default values for any parameters that aren't specified\.

If you use a JSON configuration file in conjunction with the `-s` \(`--silent`\) option, you can specify deployment options without being prompted by the deployment tool at all\.

This section defines the JSON definitions and syntax that you use to construct a configuration file\.

**Note**  
Support for JSON configuration files was added in version 0\.11\.16 of the deployment tool\.

## Common JSON parameters<a name="deployment-config-file-common"></a>

The parameters you can include in a JSON configuration file depend on the type of deployment you're doing, as shown later in this topic\. The following parameters are common to all deployment types\.

*AWSProfile*  
The name of the AWS profile to use if not using the `[default]` profile\.

*AWSRegion*  
The name of the AWS Region to use if not using the region from the `[default]` profile\.

*StackName*  
The name of the AWS CloudFormation stack to use for your application\. It can be the name of an existing stack or the name of a new stack to create\.

## JSON syntax for Amazon ECS deployments<a name="deployment-config-file-syntax-ecs"></a>

```
{
    "AWSProfile": "string",
    "AWSRegion": "string",
    "StackName": "string",
    "RecipeId": "AspNetAppEcsFargate" | "ConsoleAppEcsFargateService" | "ConsoleAppEcsFargateScheduleTask",
    "OptionSettingsConfig":
    {
        "ECSCluster":
        {
            "CreateNew": boolean,
            "NewClusterName": "string" | "ClusterArn": "string"
        },
        "ECSServiceName": "string",
        "DesiredCount": integer,
        "ApplicationIAMRole":
        {
            "CreateNew": boolean,
            "RoleArn": "string"
        },
        "Schedule": "string",
        "Vpc":
        {
            "IsDefault": boolean,
            "CreateNew": boolean | "VpcId": "string"
        },
        "AdditionalECSServiceSecurityGroups": "string",
        "ECSServiceSecurityGroups": "string",
        "TaskCpu": integer,
        "TaskMemory": integer,
        "DockerExecutionDirectory": "string"
    }
}
```

The following parameter definitions are specific to the JSON syntax for an Amazon ECS deployment\. Also see [Common JSON parameters](#deployment-config-file-common)\.

*RecipeId*  
A value that identifies the type of Amazon ECS deployment you want to perform: an ASP\.NET Core web app, a long running service app, or a scheduled task\.

*OptionSettingsConfig*  
The following options are available to configure an Amazon ECS deployment\.    
*ECSCluster*  
The ECS cluster to use for your deployment\. It can be a new cluster \(the default\) or an existing one\. If this parameter isn't present, a new cluster is created with the same name as your project\. If you want to give the new cluster a name, provide the name in `NewClusterName`\. If you're using an existing cluster, set `CreateNew` to `false` and include the cluster's ARN in `ClusterArn`\.  
*ECSServiceName*  
This parameter is valid only for the `AspNetAppEcsFargate` and `ConsoleAppEcsFargateService` recipes\.  
The name of the ECS service running in the cluster\. If this parameter isn't present, the service will be named "<YourProjectName>\-service"\.  
*DesiredCount*  
This parameter is valid only for the `AspNetAppEcsFargate` and `ConsoleAppEcsFargateService` recipes\.  
The number of ECS tasks you want to run for the service\. If given, the value must be at least 1 and at most 5000\. The default is 3 for the `AspNetAppEcsFargate` recipe and 1 for the `ConsoleAppEcsFargateService` recipe\.  
*ApplicationIAMRole*  
The IAM role that provides AWS credentials to the application to access AWS services\. You can create a new role \(the default\) or use an existing role\. To use an existing role, set `CreateNew` to `false` and include the role's ARN in `RoleArn`\.  
*Schedule*  
This parameter is valid only for the `ConsoleAppEcsFargateScheduleTask` recipe\.  
The schedule or rate \(frequency\) that determines when Amazon CloudWatch Events runs the task\. For details about the format of this value, see [Schedule Expressions for Rules](https://docs.aws.amazon.com/AmazonCloudWatch/latest/events/ScheduledEvents.html) in the [Amazon CloudWatch Events User Guide](https://docs.aws.amazon.com/AmazonCloudWatch/latest/events/)\.  
*Vpc*  
The Amazon Virtual Private Cloud \(VPC\) in which to launch the application\. It can be the Default VPC \(the default behavior\), a new VPC, or an existing VPC\. To create a new VPC, set `IsDefault` to `false` and `CreateNew` to `true`\. To use an existing VPC, set `IsDefault` to `false` and include the VPC ID in `VpcId`\.  
*AdditionalECSServiceSecurityGroups*  
This parameter is valid only for the `AspNetAppEcsFargate` recipe\.  
A comma\-delimited list of EC2 security groups to assign to the ECS service\.  
*ECSServiceSecurityGroups*  
This parameter is valid only for the `ConsoleAppEcsFargateService` recipe\.  
A comma\-delimited list of EC2 security groups to assign to the ECS service\.  
*TaskCpu*  
The number of CPU units used by the task\. Valid values are "256" \(the default\), "512", "1024", "2048", and "4096"\. For more information, see [Amazon ECS on AWS Fargate](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/AWS_Fargate.html#fargate-task-defs) in the [Amazon Elastic Container Service Developer Guide](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/), specifically, **Task CPU and memory**\.  
*TaskMemory*  
The amount of memory \(in MB\) used by the task\. Valid values are "512" \(the default\), "1024", "2048", "3072", "4096", "5120", "6144", "7168", "8192", "9216", "10240", "11264", "12288", "13312", "14336", "15360", "16384", "17408", "18432", "19456", "20480", "21504", "22528", "23552", "24576", "25600", "26624", "27648", "28672", "29696", and "30720"\. For more information, see [Amazon ECS on AWS Fargate](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/AWS_Fargate.html#fargate-task-defs) in the [Amazon Elastic Container Service Developer Guide](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/), specifically, **Task CPU and memory**\.  
*DockerExecutionDirectory*  
If you're using Docker, the path to the Docker execution environment, formatted as a string that's properly escaped for your operating system \(for example on Windows: "C:\\\\codebase"\)\.

## JSON syntax for AWS Elastic Beanstalk deployments<a name="deployment-config-file-syntax-eb"></a>

```
{
    "AWSProfile": "string",
    "AWSRegion": "string",
    "StackName": "string",
    "RecipeId": "AspNetAppElasticBeanstalkLinux",
    "OptionSettingsConfig":
    {
        "BeanstalkApplication":
        {
            "CreateNew": boolean,
            "ApplicationName": "string"
        },
        "EnvironmentName": "string",
        "InstanceType": "string",
        "EnvironmentType": "SingleInstance" | "LoadBalanced",
        "LoadBalancerType": "application" | "classic" | "network",
        "ApplicationIAMRole":
        {
            "CreateNew": boolean,
            "RoleArn": "string"
        },
        "EC2KeyPair": "string",
        "ElasticBeanstalkPlatformArn": "string",
        "ElasticBeanstalkManagedPlatformUpdates":
        {
            "ManagedActionsEnabled": boolean,
            "PreferredStartTime": "Mon:12:00",
            "UpdateLevel": "minor"
        }
    }
}
```

The following parameter definitions are specific to the JSON syntax for an Elastic Beanstalk deployment\. Also see [Common JSON parameters](#deployment-config-file-common)\.

*RecipeId*  
A value that identifies the type of AWS Elastic Beanstalk deployment you want to perform; in this case, an ASP\.NET Core web app\.

*OptionSettingsConfig*  
The following options are available to configure an Elastic Beanstalk deployment\.    
BeanstalkApplication  
The name of the Elastic Beanstalk application\. It can be a new application \(the default\) or an existing one\. If you don't supply a name through `ApplicationName`, the application will have the same name as your project\.  
EnvironmentName  
The name of the [Elastic Beanstalk environment](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/concepts.html#concepts-environment) in which to run the application\. If this parameter isn't present, the environment will be named "<YourProjectName>\-dev"\.  
InstanceType  
The [Amazon EC2 instance type](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/instance-types.html) of the EC2 instances created for the environment; for example, "t2\.micro"\. If this parameter isn't included, an instance type is chosen based on the requirements of your project\.  
EnvironmentType  
The [type of Elastic Beanstalk environment](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/using-features-managing-env-types.html) to create: a single instance for development work \(the default\) or load balanced for production\.  
LoadBalancerType  
The [type of load balancer](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/using-features.managing.elb.html) you want for your environment: classic, application \(the default\), or network\.  
This parameter is valid only if the value of `EnvironmentType` is "LoadBalanced"\.  
ApplicationIAMRole  
The IAM role that provides AWS credentials to the application to access AWS services\. You can create a new role \(the default\) or use an existing role\. To use an existing role, set `CreateNew` to `false` and include the role's ARN in `RoleArn`\.  
EC2KeyPair  
The EC2 key pair that you can use to SSH into EC2 instances for the Elastic Beanstalk environment\. If you don't include this parameter and you don't choose a key pair interactively when the deployment tool is running, you won't be able to SSH into the EC2 instance\.  
ElasticBeanstalkPlatformArn  
The ARN of the AWS Elastic Beanstalk platform to use with the environment\. If this parameter isn't present, the ARN of the latest Elastic Beanstalk platform is used\.  
For information about how to construct this ARN, see [ARN format](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/AWSHowTo.iam.policies.arn.html) in the [AWS Elastic Beanstalk Developer Guide](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/)\.  
ElasticBeanstalkManagedPlatformUpdates  
Use this parameter to configure automatic updates for your Elastic Beanstalk platform using [managed platform updates](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/environment-platform-update-managed.html), as described in the [AWS Elastic Beanstalk Developer Guide](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/)\. If `ManagedActionsEnabled` is set to `true` \(the default\), you can specify the weekly maintenance window through `PreferredStartTime`, which defaults to "Sun:00:00"\. Additionally, you can use `UpdateLevel` to specify the patch level to apply: "minor" \(the default\) or "patch"\. These options are described in [Managed action option namespaces](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/environment-platform-update-managed.html#environment-platform-update-managed-namespace) in the Elastic Beanstalk developer guide\.  
Your application continues to be available during the update process\.

## JSON syntax for Blazor WebAssembly app deployments<a name="deployment-config-file-syntax-blazor"></a>

```
{
    "AWSProfile": "string",
    "AWSRegion": "string",
    "StackName": "string",
    "RecipeId": "BlazorWasm",
    "OptionSettingsConfig":
    {
        "IndexDocument": "string",
        "ErrorDocument": "string",
        "Redirect404ToRoot": boolean
    }
}
```

**Note**  
This deployment task deploys a Blazor WebAssembly application to an Amazon S3 bucket\. The bucket created during deployment is configured for web hosting and its contents are open to the public with read access\.

The following parameter definitions are specific to the JSON syntax for a Blazor WebAssembly deployment\. Also see [Common JSON parameters](#deployment-config-file-common)\.

*RecipeId*  
A value that identifies the type of deployment you want to perform; in this case, a Blazor WebAssembly application\.

*OptionSettingsConfig*  
The following options are available to configure a Blazor WebAssembly deployment\.    
IndexDocument  
The name of the web page to use when the endpoint for your WebAssembly app is accessed with no resource path\. The default page name is `index.html`\.  
ErrorDocument  
The name of the web page to use when an error occurs while accessing a resource path\. The default value is an empty string\.  
Redirect404ToRoot  
If this parameter is set to `true` \(the default\), requests that result in a 404 are redirected the index document of the web app, which is specified by `IndexDocument`\.