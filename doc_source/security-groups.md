--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Working with security groups in Amazon EC2<a name="security-groups"></a>

In Amazon EC2, a *security group* acts as a virtual firewall that controls the network traffic for one or more EC2 instances\. By default, EC2 associates your instances with a security group that allows no inbound traffic\. You can create a security group that allows your EC2 instances to accept certain traffic\. For example, if you need to connect to an EC2 Windows instance, you must configure the security group to allow RDP traffic\.

Read more about security groups in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/using-network-security.html) and the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/using-network-security.html)\.

When using the AWS SDK for \.NET, you can create a security group for use in EC2 in a VPC or EC2\-Classic\. For more information about EC2 in a VPC versus EC2\-Classic, see the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-classic-platform.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-classic-platform.html)\.

For information about the APIs and prerequisites, see the parent section \([Working with Amazon EC2](ec2-apis-intro.md)\)\.

**Topics**
+ [Enumerating security groups](enumerate-security-groups.md)
+ [Creating security groups](creating-security-group.md)
+ [Updating security groups](authorize-ingress.md)