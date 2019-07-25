.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _route53-apis-intro:

###########################################################
Managing Domain Name System (DNS) Resources Using |R53long|
###########################################################

The |sdk-net| supports |R53long|, which is a Domain Name System (DNS) web service that provides
secure and reliable routing to your infrastructure that uses Amazon Web Services (AWS) products,
such as |EC2long| (|EC2|), |ELB|, or |S3long| (|S3|). You can also use |R53| to route users to your
infrastructure outside of AWS. This topic describes how to use the |sdk-net| to create an |R53|
:r53-dg:`hosted zone <AboutHZWorkingWith>` and add a new 
:r53-dg:`resource record set <resource-record-sets-values>` to that zone.

.. note:: This topic assumes you are already familiar with how to use |R53| and have already 
   installed the |sdk-net|. For more information about |R53|, see the 
   :r53-dg:`Amazon Route 53 Developer Guide <Welcome>`. For information about how to install the 
   |sdk-net|, see :ref:`net-dg-setup`.

The basic procedure is as follows.

**To create a hosted zone and update its record sets**

1. Create a hosted zone.

2. Create a change batch that contains one or more record sets, and instructions on which action to
   take for each set.

3. Submit a change request to the hosted zone that contains the change batch.

4. Monitor the change to verify it is complete.

The example is a simple console application that shows how to use the |sdk-net| to implement this
procedure for a basic record set.

**To run this example**

1. In the Visual Studio :guilabel:`File` menu, choose :guilabel:`New`, and then choose
   :guilabel:`Project`.

2. Choose the :guilabel:`AWS Empty Project` template and specify the project's name and location.

3. Specify the application's default credentials profile and AWS region, which are added to the
   project's :file:`App.config` file. This example assumes the region is set to |region-us-east-1|
   and the profile is set to default. For more information on profiles, see
   :ref:`net-dg-config-creds`.

4. Open :file:`program.cs` and replace the :code:`using` declarations and the code in :code:`Main` with
   the corresponding code from the following example. If you are using your default credentials
   profile and region, you can compile and run the application as-is. Otherwise, you must provide
   an appropriate profile and region, as discussed in the notes that follow the example.

.. code-block:: csharp

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

The numbers in the following sections are keyed to the comments in the preceding example.

[1] Create a Client Object
  The object must have the following information: 

  An AWS region
      When you call a client method, the underlying HTTP request is sent to this endpoint.

  A credentials profile
      The profile must grant permissions for the actions you intend to use |mdash| the |R53|
      actions in this case. Attempts to call actions that lack permissions will fail. For more
      information, see :ref:`net-dg-config-creds`.

  The :sdk-net-api:`AmazonRoute53Client <Route53/TRoute53Client>` class supports a set of public methods
  that you use to invoke :r53-dg:`Amazon Route 53 actions <Welcome>`. You create the client object
  by instantiating a new instance of the :classname:`AmazonRoute53Client` class. There are
  multiple constructors. 
  
[2] Create a hosted zone
  A hosted zone serves the same purpose as a traditional DNS zone file. It represents a collection
  of resource record sets that are managed together under a single domain name.

  **To create a hosted zone**

  1. Create a :sdk-net-api:`CreateHostedZoneRequest <Route53/TRoute53CreateHostedZoneRequest>` object 
     and specify the following request parameters. There are also two optional parameters that 
     aren't used by this example.

    :code:`Name`
        (Required) The domain name you want to register, :code:`www.example.com` for this
        example. This domain name is intended only for examples. It can't be registered with a
        domain name registrar, but you can use it to create a hosted zone for learning purposes.
    
    :code:`CallerReference`
        (Required) An arbitrary user-defined string that serves as a request ID and can be used
        to retry failed requests. If you run this application multiple times, you must change
        the :code:`CallerReference` value.
    
  2. Pass the :classname:`CreateHostedZoneRequest` object to the client object's 
     :sdk-net-api:`CreateHostedZone <Route53/MRoute53Route53CreateHostedZoneCreateHostedZoneRequest>` 
     method. The method returns a :sdk-net-api:`CreateHostedZoneResponse <Route53/TRoute53CreateHostedZoneResponse>` 
     object that contains information about the request, including the 
     :sdk-net-api:`HostedZone.Id <Route53/TRoute53HostedZone>` property that identifies zone.

[3] Create a resource record set change batch
  A hosted zone can have multiple resource record sets. Each set specifies how a subset of the 
  domain's traffic, such as email requests, should be routed. You can update a zone's resource record 
  sets with a single request. The first step is to package all the updates in a 
  :sdk-net-api:`ChangeBatch <Route53/TRoute53ChangeBatch>` object. This example specifies only one update, 
  adding a basic resource record set to the zone, but a :code:`ChangeBatch` object can contain updates
  for multiple resource record sets.

  **To create a ChangeBatch object**
 
  1. Create a :sdk-net-api:`ResourceRecordSet <Route53/TRoute53ResourceRecordSet>` object for each 
     resource record set you want to update. The group of properties you specify depends on the 
     type of resource record set. For a complete description of the properties used by the different 
     resource record sets, see 
     :r53-dg:`Values that You Specify When You Create or Edit Amazon Route 53 Resource Record Sets <resource-record-sets-values>`. 
     The example :classname:`ResourceRecordSet` object represents a 
     :r53-dg-deep:`basic resource record set <resource-record-sets-values.html#resource-record-sets-values-basic>`
     , and specifies the following required properties.
 
     :code:`Name`
        The domain or subdomain name, :code:`www.example.com` for this example.
     
     :code:`TTL`
        The amount of time, in seconds, the DNS recursive resolvers should cache information
        about this resource record set, 60 seconds for this example.
     
     :code:`Type`
        The DNS record type, :code:`A` for this example. For a complete list, see 
        :r53-dg:`Supported DNS Resource Record Types <ResourceRecordTypes>`.
     
     :code:`ResourceRecords`
        A list of one or more :sdk-net-api:`ResourceRecord <Route53/TRoute53ResourceRecord>` objects, each of
        which contains a DNS record value that depends on the DNS record type. For an :code:`A`
        record type, the record value is an IPv4 address, which for this example is set to a
        standard example address, :code:`192.0.2.235`.
 
  2. Create a :sdk-net-api:`Change <Route53/TRoute53Change>` object for each resource record set, and set the following
     properties.
 
     :code:`ResourceRecordSet`
        The :classname:`ResourceRecordSet` object you created in the previous step.
 
     :code:`Action`
        The action to be taken for this resource record set: :code:`CREATE`, :code:`DELETE`, or
        :code:`UPSERT`. For more information about these actions, see 
        :r53-dg-deep:`Elements <ChangeResourceRecordSets_Requests.html#API_ChangeResourceRecordSets_RequestParameters>`.
        This example creates a new resource record set in the hosted zone, so :code:`Action` is
        set to :code:`CREATE`.
 
  3. Create a :sdk-net-api:`ChangeBatch <Route53/TRoute53ChangeBatch>` object and set its :code:`Changes` 
     property to a list of the :classname:`Change` objects that you created in the previous step.
 
[4] Update the zone's resource record sets
  To update the resource record sets, pass the :classname:`ChangeBatch` object to the hosted zone,
  as follows. 
  
  **To update a hosted zone's resource record sets**

  1. Create a :sdk-net-api:`ChangeResourceRecordSetsRequest <Route53/TRoute53ChangeResourceRecordSetsRequest>` 
     object with the following property settings.

     :code:`HostedZoneId`
         The hosted zone's ID, which the example sets to the ID that was returned in the
         :classname:`CreateHostedZoneResponse` object. To get the ID of an existing hosted zone,
         call :sdk-net-api:`ListHostedZones <Route53/MRoute53Route53ListHostedZones>`.

     :code:`ChangeBatch`
         A :classname:`ChangeBatch` object that contains the updates.

  2. Pass the :classname:`ChangeResourceRecordSetsRequest` object to the 
     :sdk-net-api:`ChangeResourceRecordSets <Route53/MRoute53Route53ChangeResourceRecordSetsChangeResourceRecordSetsRequest>` 
     method of the client object. It returns a 
     :sdk-net-api:`ChangeResourceRecordSetsResponse <Route53/TRoute53ChangeResourceRecordSetsResponse>` 
     object, which contains a request ID you can use to monitor the request's progress.

[5] Monitor the update status
  Resource record set updates typically take a minute or so to propagate through the system. You
  can monitor the update's progress and verify that it is complete as follows. 
  
  **To monitor update status**

  1. Create a :sdk-net-api:`GetChangeRequest <Route53/TRoute53GetChangeRequest>` object and set its 
     :code:`Id` property to the request ID that was returned by :methodname:`ChangeResourceRecordSets`.

  2. Use a wait loop to periodically call the :sdk-net-api:`GetChange <Route53/MRoute53Route53GetChangeGetChangeRequest>` 
     method of the client object. :methodname:`GetChange` returns :code:`PENDING` while the update 
     is in progress and :code:`INSYNC` after the update is complete. You can use the same
     :classname:`GetChangeRequest` object for all of the method calls.
