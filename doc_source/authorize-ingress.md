--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Updating security groups<a name="authorize-ingress"></a>

This example shows you how to use the AWS SDK for \.NET to add a rule to a security group\. In particular, the example adds a rule to allow inbound traffic on a given TCP port, which can be used, for example, for remote connections to an EC2 instance\. The application takes the ID of an existing security group, an IP address \(or address range\) in CIDR format, and optionally a TCP port number\. It then adds an inbound rule to the given security group\.

**Note**  
To use this example, you need an IP address \(or address range\) in CIDR format\. See **Additional considerations** at this end of this topic for methods to obtain the IP address of your local computer\.

The following sections provide snippets of this example\. The [complete code for the example](#authorize-ingress-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Add an inbound rule](#authorize-ingress-add-rule)
+ [Complete code](#authorize-ingress-complete-code)

## Add an inbound rule<a name="authorize-ingress-add-rule"></a>

The following snippet adds an inbound rule to a security group for a particular IP address \(or range\) and TCP port\.

The example [at the end of this topic](#authorize-ingress-complete-code) shows this snippet in use\.

```
    //
    // Method that adds a TCP ingress rule to a security group
    private static async Task AddIngressRule(
      IAmazonEC2 eC2Client, string groupID, string ipAddress, int port)
    {
      // Create an object to hold the request information for the rule.
      // It uses an IpPermission object to hold the IP information for the rule.
      var ingressRequest = new AuthorizeSecurityGroupIngressRequest{
        GroupId = groupID};
      ingressRequest.IpPermissions.Add(new IpPermission{
        IpProtocol = "tcp",
        FromPort = port,
        ToPort = port,
        Ipv4Ranges = new List<IpRange>() { new IpRange { CidrIp = ipAddress } }
      });

      // Create the inbound rule for the security group
      AuthorizeSecurityGroupIngressResponse responseIngress =
        await eC2Client.AuthorizeSecurityGroupIngressAsync(ingressRequest);
      Console.WriteLine($"\nNew RDP rule was written in {groupID} for {ipAddress}.");
      Console.WriteLine($"Result: {responseIngress.HttpStatusCode}");
    }
```

## Complete code<a name="authorize-ingress-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c19c13c17c17b5b1"></a>

NuGet packages:
+ [AWSSDK\.EC2](https://www.nuget.org/packages/AWSSDK.EC2)

Programming elements:
+ Namespace [Amazon\.EC2](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2.html)

  Class [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html)
+ Namespace [Amazon\.EC2\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/NEC2Model.html)

  Class [AuthorizeSecurityGroupIngressRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAuthorizeSecurityGroupIngressRequest.html)

  Class [AuthorizeSecurityGroupIngressResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TAuthorizeSecurityGroupIngressResponse.html)

  Class [IpPermission](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIpPermission.html)

  Class [IpRange](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIpRange.html)

### The code<a name="w4aac19c19c13c17c17b7b1"></a>

```
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2AddRuleForRDP
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to add a rule that allows inbound traffic on TCP a port
  class Program
  {
    private const int DefaultPort = 3389;

    static async Task Main(string[] args)
    {
      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if(parsedArgs.Count == 0)
      {
        PrintHelp();
        return;
      }

      // Get the application parameters from the parsed arguments
      var groupID = CommandLine.GetParameter(parsedArgs, null, "-g", "--group-id");
      var ipAddress = CommandLine.GetParameter(parsedArgs, null, "-i", "--ip-address");
      var portStr = CommandLine.GetParameter(parsedArgs, DefaultPort.ToString(), "-p", "--port");
      if(string.IsNullOrEmpty(ipAddress))
        CommandLine.ErrorExit("\nYou must supply an IP address in CIDR format.");
      if(string.IsNullOrEmpty(groupID) || !groupID.StartsWith("sg-"))
        CommandLine.ErrorExit("\nThe ID for a security group is missing or incorrect.");
      if(int.Parse(portStr) == 0)
        CommandLine.ErrorExit($"\nThe given TCP port number, {portStr}, isn't allowed.");

      // Add a rule to the given security group that allows
      // inbound traffic on a TCP port
      await AddIngressRule(
        new AmazonEC2Client(), groupID, ipAddress, int.Parse(portStr));
    }


    //
    // Method that adds a TCP ingress rule to a security group
    private static async Task AddIngressRule(
      IAmazonEC2 eC2Client, string groupID, string ipAddress, int port)
    {
      // Create an object to hold the request information for the rule.
      // It uses an IpPermission object to hold the IP information for the rule.
      var ingressRequest = new AuthorizeSecurityGroupIngressRequest{
        GroupId = groupID};
      ingressRequest.IpPermissions.Add(new IpPermission{
        IpProtocol = "tcp",
        FromPort = port,
        ToPort = port,
        Ipv4Ranges = new List<IpRange>() { new IpRange { CidrIp = ipAddress } }
      });

      // Create the inbound rule for the security group
      AuthorizeSecurityGroupIngressResponse responseIngress =
        await eC2Client.AuthorizeSecurityGroupIngressAsync(ingressRequest);
      Console.WriteLine($"\nNew RDP rule was written in {groupID} for {ipAddress}.");
      Console.WriteLine($"Result: {responseIngress.HttpStatusCode}");
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: EC2AddRuleForRDP -g <group-id> -i <ip-address> [-p <port>]" +
        "\n  -g, --group-id: The ID of the security group to which you want to add the inbound rule." +
        "\n  -i, --ip-address: An IP address or address range in CIDR format." +
        "\n  -p, --port: The TCP port number. Defaults to 3389.");
    }
  }


  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal.
  // (This is the same for all examples. When you have seen it once, you can ignore it.)
  static class CommandLine
  {
    // Method to parse a command line of the form: "--param value" or "-p value".
    // If "param" is found without a matching "value", Dictionary.Value is an empty string.
    // If "value" is found without a matching "param", Dictionary.Key is "--NoKeyN"
    //  where "N" represents sequential numbers.
    public static Dictionary<string,string> Parse(string[] args)
    {
      var parsedArgs = new Dictionary<string,string>();
      int i = 0, n = 0;
      while(i < args.Length)
      {
        // If the first argument in this iteration starts with a dash it's an option.
        if(args[i].StartsWith("-"))
        {
          var key = args[i++];
          var value = string.Empty;

          // Is there a value that goes with this option?
          if((i < args.Length) && (!args[i].StartsWith("-"))) value = args[i++];
          parsedArgs.Add(key, value);
        }

        // If the first argument in this iteration doesn't start with a dash, it's a value
        else
        {
          parsedArgs.Add("--NoKey" + n.ToString(), args[i++]);
          n++;
        }
      }

      return parsedArgs;
    }

    //
    // Method to get a parameter from the parsed command-line arguments
    public static string GetParameter(
      Dictionary<string,string> parsedArgs, string def, params string[] keys)
    {
      string retval = null;
      foreach(var key in keys)
        if(parsedArgs.TryGetValue(key, out retval)) break;
      return retval ?? def;
    }

    //
    // Exit with an error.
    public static void ErrorExit(string msg, int code=1)
    {
      Console.WriteLine("\nError");
      Console.WriteLine(msg);
      Environment.Exit(code);
    }
  }

}
```

**Additional considerations**
+ If you don't supply a port number, the application defaults to port 3389\. This is the port for Windows RDP, which enables you to connect to an EC2 instance running Windows\. If you're launching an EC2 instance running Linux, you can use TCP port 22 \(SSH\) instead\.
+ Notice that the example sets `IpProtocol` to "tcp"\. The values for `IpProtocol` can be found in the description for the `IpProtocol` property of the [IpPermission](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TIpPermission.html) class\.
+ You might want the IP address of your local computer when you use this example\. The following are some of the ways in which you can obtain the address\.
  + If your local computer \(from which you will connect to your EC2 instance\) has a static public IP address, you can use a service to get that address\. One such service is [http://checkip\.amazonaws\.com/](http://checkip.amazonaws.com/)\. Read more about authorizing inbound traffic in the [EC2 user guide for Linux](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/authorizing-access-to-an-instance.html) or the [EC2 user guide for Windows](https://docs.aws.amazon.com/AWSEC2/latest/WindowsGuide/authorizing-access-to-an-instance.html)\.
  + Another way to obtain the IP address of your local computer is to use the [Amazon EC2 console](https://console.aws.amazon.com/ec2/v2/home#SecurityGroups)\.

    Select one of your security groups, select the **Inbound rules** tab, and choose **Edit inbound rules**\. In an inbound rule, open the drop\-down menu in the **Source** column and choose **My IP** to see the IP address of your local computer in CIDR format\. Be sure to **Cancel** the operation\.
+ You can verify the results of this example by examining the list of security groups in the [Amazon EC2 console](https://console.aws.amazon.com/ec2/v2/home#SecurityGroups)\.