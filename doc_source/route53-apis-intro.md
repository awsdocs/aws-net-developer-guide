--------

This documentation is for version 2\.0 of the AWS SDK for \.NET\. For the latest version, see the [AWS SDK for \.NET Developer Guide for version 3](https://docs.aws.amazon.com/AWSSdkDocsNET/V3/DeveloperGuide/welcome.html)\.

--------

# Amazon Route 53 Programming with the AWS SDK for \.NET<a name="route53-apis-intro"></a>

The AWS SDK for \.NET supports Amazon Route 53, which is a Domain Name System \(DNS\) web service that provides secure and reliable routing to your infrastructure that uses Amazon Web Services \(AWS\) products, such as Amazon Elastic Compute Cloud \(Amazon EC2\), Elastic Load Balancing, or Amazon Simple Storage Service \(Amazon S3\)\. You can also use Route 53 to route users to your infrastructure outside of AWS\. This topic describes how to use the AWS SDK for \.NET to create an Route 53[hosted zone](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/AboutHZWorkingWith.html) and add a new [resource record set](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/resource-record-sets-values.html) to that zone\.

**Note**  
This topic assumes that you are already familiar with how to use Route 53 and have already installed the AWS SDK for \.NET\. For more information on Route 53, see the [Amazon Route 53 Developer Guide](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/Welcome.html)\. For information on how to install the AWS SDK for \.NET, see [Getting Started with the AWS SDK for \.NET](net-dg-setup.md)\.

The basic procedure is as follows\.

 **To create a hosted zone and update its record sets** 

1. Create a hosted zone\.

1. Create a change batch that contains one or more record sets, and instructions on what action to take for each set\.

1. Submit a change request to the hosted zone that contains the change batch\.

1. Monitor the change to verify that it is complete\.

The example is a simple console application that shows how to use the the SDK to implement this procedure for a basic record set\.

 **To run this example** 

1. In the Visual Studio **File** menu, click **New** and then click **Project**\.

1. Select the **AWS Empty Project** template and specify the project’s name and location\.

1. Specify the application’s default credentials profile and AWS region, which are added to the project’s `App.config` file\. This example assumes that the region is set to US East \(Northern Virginia\) and the profile is set to default\. For more information on profiles, see [Configuring AWS Credentials](net-dg-config-creds.md)\.

1. Open `program.cs` and replace the `using` declarations and the code in `Main` with the corresponding code from the following example\. If you are using your default credentials profile and region, you can compile and run the application as\-is\. Otherwise, you must provide an appropriate profile and region, as discussed in the notes that follow the example\.

```
using System;
using System.Collections.Generic;
using System.Threading;

using Amazon;
using Amazon.Route53;
using Amazon.Route53.Model;

namespace Route53_RecordSet
{
  //Create a hosted zone and add a basic record set to it
  class recordset
  {
    public static void Main(string[] args)
    {
      string domainName = "www.example.org";

      //[1] Create an Amazon Route 53 client object
      var route53Client = new AmazonRoute53Client();

      //[2] Create a hosted zone
      var zoneRequest = new CreateHostedZoneRequest()
      {
        Name = domainName,
        CallerReference = "my_change_request"
      };

      var zoneResponse = route53Client.CreateHostedZone(zoneRequest);

      //[3] Create a resource record set change batch
      var recordSet = new ResourceRecordSet()
      {
        Name = domainName,
        TTL = 60,
        Type = RRType.A,
        ResourceRecords = new List<ResourceRecord>
        {
          new ResourceRecord { Value = "192.0.2.235" }
        }
      };

      var change1 = new Change()
      {
        ResourceRecordSet = recordSet,
        Action = ChangeAction.CREATE
      };

      var changeBatch = new ChangeBatch()
      {
        Changes = new List<Change> { change1 }
      };

      //[4] Update the zone's resource record sets
      var recordsetRequest = new ChangeResourceRecordSetsRequest()
      {
        HostedZoneId = zoneResponse.HostedZone.Id,
        ChangeBatch = changeBatch
      };

      var recordsetResponse = route53Client.ChangeResourceRecordSets(recordsetRequest);

      //[5] Monitor the change status
      var changeRequest = new GetChangeRequest()
      {
        Id = recordsetResponse.ChangeInfo.Id
      };

      while (ChangeStatus.PENDING ==
        route53Client.GetChange(changeRequest).ChangeInfo.Status)
      {
        Console.WriteLine("Change is pending.");
        Thread.Sleep(15000);
      }

      Console.WriteLine("Change is complete.");
      Console.ReadKey();
    }
  }
}
```

The numbers in the following sections are keyed to the comments in the preceding example\.

**\[1\] Create a Client Object**  
The [AmazonRoute53Client](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53Route53NET45.html) class supports a set of public methods that you use to invoke [Amazon Route 53 actions](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/Welcome.html)\. You create the client object by instantiating a new instance of the `AmazonRoute53Client` class\. There are multiple constructors\. The object must have the following information:    
**An AWS region**  
When you call a client method, the underlying HTTP request is sent to this endpoint\.  
**A credentials profile**  
The profile must grant permissions for the actions that you intend to use—the Route 53 actions in this case\. Attempts to call actions that lack permissions will fail\. For more information, see [Configuring AWS Credentials](net-dg-config-creds.md)\.
The example uses the default constructor to create the object, which implicitly specifies the application’s default profile and region\. Other constructors allow you to override either or both default values\.

**\[2\] Create a hosted zone**  
A hosted zone serves the same purpose as a traditional DNS zone file\. It represents a collection of resource record sets that are managed together under a single domain name\.  
 **To create a hosted zone**   

1. Create a [CreateHostedZoneRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53CreateHostedZoneRequestNET45.html) object and specify following request parameters\. There are also two optional parameters that aren’t used by this example\.  
** `Name` **  
\(Required\) The domain name that you want to register, `www.example.com` for this example\. This domain name is intended only for examples and can’t be registered with a domain name registrar for an actual site, but you can use it to create a hosted zone for learning purposes\.  
** `CallerReference` **  
\(Required\) An arbitrary user\-defined string that serves as a request ID and can be used to retry failed requests\. If you run this application multiple times, you must change the `CallerReference` value\.

1. Pass the `CreateHostedZoneRequest` object to the client object’s [CreateHostedZone](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MRoute53Route53CreateHostedZoneCreateHostedZoneRequestNET45.html) method\. The method returns a [CreateHostedZoneResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53CreateHostedZoneResponseNET45.html) object that contains a variety of information about the request, including the [HostedZone\.Id](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53HostedZoneNET45.html) property that identifies zone\.

**\[3\] Create a resource record set change batch**  
A hosted zone can have multiple resource record sets\. Each set specifies how a subset the domain’s traffic, such as email requests, should be routed\. You can update a zone’s resource record sets with a single request\. The first step is to package all the updates in a [ChangeBatch](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ChangeBatchNET45.html) object\. This example specifies only one update, adding a basic resource record set to the zone, but a `ChangeBatch` object can contain updates for multiple resource record sets\.  
 **To create a ChangeBatch object**   

1. Create a [ResourceRecordSet](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ResourceRecordSetNET45.html) object for each resource record set that you want to update\. The group of properties that you specify depends on the type of resource record set\. For a complete description of the properties used by the different resource record sets, see [Values that You Specify When You Create or Edit Amazon Route 53 Resource Record Sets](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/resource-record-sets-values.html)\. The example `ResourceRecordSet` object represents a [basic resource record set](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/resource-record-sets-values.html), and specifies the following required properties\.  
** `Name` **  
The domain or subdomain name, `www.example.com` for this example\.  
** `TTL` **  
The amount of time in seconds that the DNS recursive resolvers should cache information about this resource record set, 60 seconds for this example\.  
** `Type` **  
The DNS record type, `A` for this example\. For a complete list, see [Supported DNS Resource Record Types](ResourceRecordTypes.html)\.  
** `ResourceRecords` **  
A list of one or more [ResourceRecord](TRoute53ResourceRecordNET45.html) objects, each of which contains a DNS record value that depends on the DNS record type\. For an `A` record type, the record value is an IPv4 address, which for this example is set to a standard example address, `192.0.2.235`\.

1. Create a [Change](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ChangeNET45.html) object for each for each resource record set, and set the following properties\.  
** `ResourceRecordSet` **  
The `ResourceRecordSet` object that you created in the previous step\.  
** `Action` **  
The action to be taken for this resource record set: `CREATE`, `DELETE`, or `UPSERT`\. For more information on these actions, see [Elements](https://docs.aws.amazon.com/Route53/latest/APIReference/API_ChangeResourceRecordSets_Requests.html)\. This example creates a new resource record set in the hosted zone, so `Action` is set to `CREATE`\.

1. Create a [ChangeBatch](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ChangeBatchNET45.html) object and set its `Changes` property to a list of the `Change` objects that you created in the previous step\.

**\[4\] Update the zone’s resource record sets**  
To update the resource record sets, pass the `ChangeBatch` object to the hosted zone, as follows\.  
 **To update a hosted zone’s resource record sets**   

1. Create a [ChangeResourceRecordSetsRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ChangeResourceRecordSetsRequestNET45.html) object with the following property settings\.  
** `HostedZoneId` **  
The hosted zone’s ID, which the example sets to the ID that was returned in the `CreateHostedZoneResponse` object\. To get the ID of an existing hosted zone, call [ListHostedZones](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MRoute53Route53ListHostedZonesNET45.html)\.  
** `ChangeBatch` **  
A `ChangeBatch` object that contains the updates\.

1. Pass the `ChangeResourceRecordSetsRequest` object to the client object’s [ChangeResourceRecordSets](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MRoute53Route53ChangeResourceRecordSetsChangeResourceRecordSetsRequestNET45.html) method\. It returns a [ChangeResourceRecordSetsResponse](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53ChangeResourceRecordSetsResponseNET45.html) object, which contains a request ID that you can use to monitor the request’s progress\.

**\[5\] Monitor the update status**  
Resource record set updates typically take a minute or so to propagate through the system\. You can monitor the update’s progress and verify that it has completed as follows\.  
 **To monitor update status**   

1. Create a [GetChangeRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TRoute53GetChangeRequestNET45.html) object and set its `Id` property to the request ID that was returned by `ChangeResourceRecordSets`\.

1. Use a wait loop to periodically call the client object’s [GetChange](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MRoute53Route53GetChangeGetChangeRequestNET45.html) method\. `GetChange` returns `PENDING` while the update is in progress and `INSYNC` after the update is complete\. You can use the same `GetChangeRequest` object for all of the method calls\.