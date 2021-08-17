--------

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Working with Amazon EC2 key pairs<a name="key-pairs"></a>

Amazon EC2 uses public–key cryptography to encrypt and decrypt login information\. Public–key cryptography uses a public key to encrypt data, and then the recipient uses the private key to decrypt the data\. The public and private keys are known as a key pair\. If you want to log into an EC2 instance, you must specify a key pair when you launch it, and then provide the private key of the pair when you connect to it\.

When you launch an EC2 instance, you can create a key pair for it or use one that you've already used when launching other instances\. Read more about Amazon EC2 key pairs in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-key-pairs.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/ec2-key-pairs.html)\.

For information about the APIs and prerequisites, see the parent section \([Working with Amazon EC2](ec2-apis-intro.md)\)\.

**Topics**
+ [Creating and displaying key pairs](create-save-key-pair.md)
+ [Deleting key pairs](delete-key-pairs.md)