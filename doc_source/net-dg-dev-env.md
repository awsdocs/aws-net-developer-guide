--------

Do you want to deploy your \.NET applications to AWS in just a few simple clicks? Try our new [\.NET CLI tooling](https://www.nuget.org/packages/AWS.Deploy.CLI/) for a simplified deployment experience\! Read our [blog post](https://aws.amazon.com/blogs/developer/reimagining-the-aws-net-deployment-experience/) and submit your feedback on [GitHub](https://github.com/aws/aws-dotnet-deploy)\!

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/BannerButton.png) ](https://github.com/aws/aws-dotnet-deploy/)

For additional information, see the section for the [deployment tool](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/deployment-tool.html) in this guide\.

--------

# Install and configure your toolchain<a name="net-dg-dev-env"></a>

To use the AWS SDK for \.NET, you must have certain development tools installed\.

**Note**  
If you performed the [quick start for the SDK](quick-start.md), you might already have some of these tools installed\. If you didn't do the quick start and are new to \.NET development on AWS, consider doing that first for an introduction to the AWS SDK for \.NET\.

## Cross\-platform development<a name="net-dg-dev-env-cross"></a>

The following are required for cross\-platform \.NET development on Windows, Linux, or macOS:
+ Microsoft [\.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/), version 2\.1, 3\.1, or later, which includes the \.NET command line interface \(CLI\) \(**`dotnet`**\) and the \.NET Core runtime\.
+ A code editor or integrated development environment \(IDE\) that is appropriate for your operating system and requirements\. This is typically one that provides some support for \.NET Core\.

  Examples include [Microsoft Visual Studio Code \(VS Code\)](https://code.visualstudio.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), and [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/)\.
+ \(Optional\) An AWS toolkit if one is available for the editor you chose and your operating system\.

  Examples include the [AWS Toolkit for Visual Studio Code](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/welcome.html), [AWS Toolkit for JetBrains](https://docs.aws.amazon.com/toolkit-for-jetbrains/latest/userguide/welcome.html), and [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/welcome.html)\.

## Windows with Visual Studio and \.NET Core<a name="net-dg-dev-env-winvs"></a>

The following are required for development on Windows with Visual Studio and \.NET Core:
+ [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/)
+ Microsoft \.NET Core 2\.1, 3\.1 or later

  This is typically included by default when installing a recent version of Visual Studio\.
+ \(Optional\) The AWS Toolkit for Visual Studio, which is a plugin that provides a user interface for managing your AWS resources and local profiles from Visual Studio\. To install the toolkit, see [Setting up the AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/getting-set-up.html)\.

  For more information, see the [AWS Toolkit for Visual Studio User Guide](https://docs.aws.amazon.com/AWSToolkitVS/latest/UserGuide/)\.

## Next step<a name="net-dg-dev-env-next"></a>

[Setting up your project](net-dg-config.md)