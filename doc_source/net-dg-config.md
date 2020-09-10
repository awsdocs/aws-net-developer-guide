--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Setting up your AWS SDK for \.NET project<a name="net-dg-config"></a>

In addition to [setting up your environment](net-dg-setup.md), you need to configure each project that you create\.

**Note**  
The configuration shown here is centered around \.NET Core and ASP\.NET Core \(although there might be some overlap with \.NET Framework configuration\)\.  
If you're looking for \.NET Framework configuration, see [version 3](../../v3/developer-guide/net-dg-config.html) of the guide instead\.

There are a few essential things your application needs to access AWS services through the AWS SDK for \.NET:
+ An appropriate user account or role
+ Credentials for that user account or to assume that role
+ Specification of the AWS Region
+ AWSSDK packages or assemblies

Some of the topics in this section provide information about how to configure these essential things\.

Other topics in this section and other sections provide information about more advanced ways that you can configure your project\.

**Topics**
+ [Start a new project](net-dg-start-new-project.md)
+ [Create users and roles](net-dg-users-roles.md)
+ [Configure AWS credentials](net-dg-config-creds.md)
+ [Configure the AWS Region](net-dg-region-selection.md)
+ [Install AWSSDK packages with NuGet](net-dg-install-assemblies.md)
+ [Install AWSSDK assemblies without NuGet](net-dg-install-without-nuget.md)
+ [Advanced configuration](net-dg-advanced-config.md)