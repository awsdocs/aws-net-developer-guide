.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _how-to-ec2:

#####################################################
Tutorial: Creating |EC2| Instances with the |sdk-net|
#####################################################

You can access the features of |EC2| using the |sdk-net|_. For
example, you can create, start, and terminate EC2 instances.

The sample code in this tutorial is written in C#, but you can use the |sdk-net| with any compatible 
language. The |sdk-net| installs a set of C# project templates, so the simplest way to start this 
project is to open Visual Studio, select :guilabel:`New Project` from the :guilabel:`File` menu, 
and then select :guilabel:`AWS Empty Project`.

**Prerequisites**

Before you begin, be sure that you have created an AWS account and that you have set up your AWS
credentials. For more information, see :ref:`net-dg-setup`.


Tasks
=====

The following tasks demonstrate how to manage EC2 instances using the |sdk-net|.

* :ref:`init-ec2-client`

* :ref:`create-security-group`

* :ref:`create-key-pair`

* :ref:`run-instance`

* :ref:`terminate-instance`

.. _init-ec2-client:

Create an |EC2| Client Using the the SDK
========================================

Create an *Amazon EC2 client* to manage your EC2 resources, such as instances and security groups.
This client is represented by an :sdk-net-api-v2:`AmazonEC2Client <TEC2EC2NET45>` object, which you can 
create as follows:

.. code-block:: csharp

    var ec2Client = new AmazonEC2Client();

The permissions for the client object are determined by the policy that is attached to the profile
that you specified in the :file:`App.config` file. By default, we use the region specified in
:file:`App.config`. To use a different region, pass the appropriate 
:sdk-net-api-v2:`RegionEndpoint <TRegionEndpointNET45>` value to the constructor. For more information, 
see :rande:`Regions and Endpoints <ec2>` in the |AWS-gr|.


.. _create-security-group:

Create a Security Group Using the the SDK
=========================================

Create a *security group*, which acts as a virtual firewall that controls the network traffic for
one or more EC2 instances. By default, |EC2| associates your instances with a security group that
allows no inbound traffic. You can create a security group that allows your EC2 instances to accept
certain traffic. For example, if you need to connect to an EC2 Windows instance, you must configure
the security group to allow RDP traffic. You can create a security group using the |EC2| console or
the the SDK.

You create a security group for use in either EC2-Classic or EC2-VPC. For more information about
EC2-Classic and EC2-VPC, see :ec2-ug-win:`Supported Platforms <ec2-supported-platforms>` in the
|EC2-ug-win|.

Alternatively, you can create a security group using the |EC2| console. For more information, see
:ec2-ug-win:`Amazon EC2 Security Groups <using-network-security>` in the |EC2-ug-win|.


.. contents:: **Contents**
    :local:
    :depth: 1

.. _enumerate-security-groups:

Enumerating Your Security Groups
--------------------------------

You can enumerate your security groups and check whether a particular security group exists.

To enumerate your security groups for EC2-Classic
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Get the complete list of your security groups using 
:sdk-net-api-v2:`DescribeSecurityGroups <MEC2EC2DescribeSecurityGroupsNET45>` with no parameters. The 
following example checks each security group to see whether its name is :code:`my-sample-sg`.

.. code-block:: csharp

    string secGroupName = "my-sample-sg";
    SecurityGroup mySG = null;
    
    var dsgRequest = new DescribeSecurityGroupsRequest();
    var dsgResponse = ec2Client.DescribeSecurityGroups(dsgRequest);
    List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
    foreach (SecurityGroup item in mySGs)
    {
        Console.WriteLine("Existing security group: " + item.GroupId);
        if (item.GroupName == secGroupName)
        {
            mySG = item;
        }
    }

To enumerate your security groups for a VPC
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

To enumerate the security groups for a particular VPC, use 
:sdk-net-api-v2:`DescribeSecurityGroups <MEC2EC2DescribeSecurityGroupsNET45>` with a filter. The 
following example checks each security group for a security group with the name 
:code:`my-sample-sg-vpc`.

.. code-block:: csharp

    string secGroupName = "my-sample-sg-vpc";
    SecurityGroup mySG = null;
    string vpcID = "vpc-f1663d98";
    
    Filter vpcFilter = new Filter
    {
        Name = "vpc-id",
        Values = new List<string>() {vpcID}
    };
    var dsgRequest = new DescribeSecurityGroupsRequest();
    dsgRequest.Filters.Add(vpcFilter);
    var dsgResponse = ec2Client.DescribeSecurityGroups(dsgRequest);
    List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
    foreach (SecurityGroup item in mySGs)
    {
        Console.WriteLine("Existing security group: " + item.GroupId);
        if (item.GroupName == secGroupName)
        {
            mySG = item;
        }
    }


.. _creating-security-group:

Creating a Security Group
-------------------------

The examples in this section follow from the examples in the previous section. If the security group
doesn't already exist, create it. Note that if you were to specify the same name as an existing
security group, :code:`CreateSecurityGroup` throws an exception.

To create a security group for EC2-Classic
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Create and initialize a :sdk-net-api-v2:`CreateSecurityGroupRequest <TEC2CreateSecurityGroupRequestNET45>`
object. Assign a name and description to the 
:sdk-net-api-v2:`GroupName <PEC2CreateSecurityGroupRequestGroupNameNET45>` and 
:sdk-net-api-v2:`Description <PEC2CreateSecurityGroupRequestDescriptionNET45>` properties, respectively.

The :sdk-net-api-v2:`CreateSecurityGroup <MEC2EC2CreateSecurityGroupCreateSecurityGroupRequestNET45>` method
returns a :sdk-net-api-v2:`CreateSecurityGroupResponse <TEC2CreateSecurityGroupRequestNET45>` object. 
You can get the ID of the new security group from the response and then use 
:sdk-net-api-v2:`DescribeSecurityGroups <MEC2EC2DescribeSecurityGroupsNET45>` with the security group ID 
to get the :code:`SecurityGroup` object for the security group.

.. code-block:: csharp

    if (mySG == null) 
    {
        var newSGRequest = new CreateSecurityGroupRequest()
        {
            GroupName = secGroupName,
            Description = "My sample security group for EC2-Classic"
        };
        var csgResponse = ec2Client.CreateSecurityGroup(newSGRequest);
        Console.WriteLine();
        Console.WriteLine("New security group: " + csgResponse.GroupId);
    
        List<string> Groups = new List<string>() { csgResponse.GroupId };
        var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
        var newSgResponse = ec2Client.DescribeSecurityGroups(newSgRequest);
        mySG = newSgResponse.SecurityGroups[0];
    }

To create a security group for EC2-VPC
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Create and initialize a :sdk-net-api-v2:`CreateSecurityGroupRequest <TEC2CreateSecurityGroupRequestNET45>`
object. Assign values to the :sdk-net-api-v2:`GroupName <PEC2CreateSecurityGroupRequestGroupNameNET45>`,
:sdk-net-api-v2:`Description <PEC2CreateSecurityGroupRequestDescriptionNET45>`, and 
:sdk-net-api-v2:`VpcId <PEC2CreateSecurityGroupRequestVpcIdNET45>` properties.

The :sdk-net-api-v2:`CreateSecurityGroup <MEC2EC2CreateSecurityGroupCreateSecurityGroupRequestNET45>` method
returns a :sdk-net-api-v2:`CreateSecurityGroupResponse <TEC2CreateSecurityGroupRequestNET45>` object. 
You can get the ID of the new security group from the response and then use 
:sdk-net-api-v2:`DescribeSecurityGroups <MEC2EC2DescribeSecurityGroupsNET45>` with the security group ID 
to get the :code:`SecurityGroup` object for the security group.

.. code-block:: csharp

    if (mySG == null)
    {
        var newSGRequest = new CreateSecurityGroupRequest()
        {
            GroupName = secGroupName,
            Description = "My sample security group for EC2-VPC",
            VpcId = vpcID
        };
        var csgResponse = ec2Client.CreateSecurityGroup(newSGRequest);
        Console.WriteLine();
        Console.WriteLine("New security group: " + csgResponse.GroupId);
    
        List<string> Groups = new List<string>() { csgResponse.GroupId };
        var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
        var newSgResponse = ec2Client.DescribeSecurityGroups(newSgRequest);
        mySG = newSgResponse.SecurityGroups[0];
    }


.. _authorize-ingress:

Adding Rules to Your Security Group
-----------------------------------

Use the following procedure to add a rule to allow inbound traffic on TCP port 3389 (RDP). This
enables you to connect to a Windows instance. If you're launching a Linux instance, use TCP port 22
(SSH) instead.

.. tip:: You can get the public IP address of your local computer using a service. For example, we provide
   the following service: http://checkip.amazonaws.com/. To locate another service that provides
   your IP address, use the search phrase "what is my IP address". If you are connecting through an
   ISP or from behind your firewall without a static IP address, you need to find out the range of
   IP addresses used by client computers.

The examples in this section follow from the examples in the previous sections. They assume that
:code:`mySG` is an existing security group.

To add a rule to a security group
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

1. Create and initialize an :sdk-net-api-v2:`IpPermission <TEC2IpPermissionNET45>` object.

   .. code-block:: csharp
   
       string ipRange = "0.0.0.0/0";
       List<string> ranges = new List<string>() {ipRange};
       
       var ipPermission = new IpPermission()
       {
           IpProtocol = "tcp",
           FromPort = 3389,
           ToPort = 3389,
           IpRanges = ranges
       };
   
   :sdk-net-api-v2:`IpProtocol <PEC2IpPermissionIpProtocolNET45>`
       The IP protocol.
   
   :sdk-net-api-v2:`FromPort <PEC2IpPermissionFromPortNET45>` and :sdk-net-api-v2:`ToPort <PEC2IpPermissionToPortNET45>` 
       The beginning and end of the port range. This example specifies a single port, 3389, which
       is used to communicate with Windows over RDP.
   
   :sdk-net-api-v2:`IpRanges <PEC2IpPermissionIpRangesNET45>`
       The IP addresses or address ranges, in CIDR notation. For convenience, this example uses
       :code:`0.0.0.0/0`, which authorizes network traffic from all IP addresses. This is
       acceptable for a short time in a test environment, but it's unsafe in a production
       environment.

2. Create and initialize an :sdk-net-api-v2:`AuthorizeSecurityGroupIngressRequest <TEC2AuthorizeSecurityGroupIngressRequestNET45>` object.

   .. code-block:: csharp

       var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
       ingressRequest.GroupId = mySG.GroupId;
       ingressRequest.IpPermissions.Add(ipPermission);

   :sdk-net-api-v2:`GroupId <PEC2AuthorizeSecurityGroupIngressRequestGroupIdNET45>`
       The ID of the security group.

   :sdk-net-api-v2:`IpPermissions <PEC2AuthorizeSecurityGroupIngressRequestIpPermissionsNET45>`
       The :code:`IpPermission` object from step 1.

3. (Optional) You can add additional rules to the :code:`IpPermissions` collection before going to the
   next step.

4. Pass the request object to the :sdk-net-api-v2:`AuthorizeSecurityGroupIngress 
   <MEC2EC2AuthorizeSecurityGroupIngressAuthorizeSecurityGroupIngressRequestNET45>` method,
   which returns an :sdk-net-api-v2:`AuthorizeSecurityGroupIngressResponse 
   <TEC2AuthorizeSecurityGroupIngressResponseNET45>` object.

   .. code-block:: csharp

       var ingressResponse =  ec2Client.AuthorizeSecurityGroupIngress(ingressRequest);
       Console.WriteLine("New RDP rule for: " + ipRange);



.. _create-key-pair:

Create a Key Pair Using the the SDK
===================================

You must specify a key pair when you launch an EC2 instance and specify the private key of the key
pair when you connect to the instance. You can create a key pair or use an existing key pair that
you've used when launching other instances. For more information, see 
:ec2-ug-win:`Amazon EC2 Key Pairs <ec2-key-pairs>` in the |EC2-ug-win|.

.. _enumerate-key-pairs:

Enumerating Your Key Pairs
--------------------------

You can enumerate your key pairs and check whether a particular key pair exists.

**To enumerate your key pairs**

Get the complete list of your key pairs using :sdk-net-api-v2:`DescribeKeyPairs <MEC2EC2DescribeKeyPairsNET45>`
with no parameters. The following example checks each key pair to see whether its name is 
:code:`my-sample-key`.

.. code-block:: csharp

    string keyPairName = "my-sample-key";
    KeyPairInfo myKeyPair = null;
    
    var dkpRequest = new DescribeKeyPairsRequest();
    var dkpResponse = ec2Client.DescribeKeyPairs(dkpRequest);
    List<KeyPairInfo> myKeyPairs = dkpResponse.KeyPairs;
    
    foreach (KeyPairInfo item in myKeyPairs)
    {
        Console.WriteLine("Existing key pair: " + item.KeyName);
        if (item.KeyName == keyPairName)
        {
            myKeyPair = item;
        }
    }


.. _create-save-key-pair:

Creating a Key Pair and Saving the Private Key
----------------------------------------------

The example in this section follows from the example in the previous section. If the key pair
doesn't already exist, create it. Be sure to save the private key now, because you can't retrieve it
later.

**To create a key pair and save the private key**

Create and initialize a :sdk-net-api-v2:`CreateKeyPairRequest <TEC2CreateKeyPairRequestNET45>` object. 
Set the :sdk-net-api-v2:`KeyName <PEC2CreateKeyPairRequestKeyNameNET45>` property to the name of the key pair.

Pass the request object to the :sdk-net-api-v2:`CreateKeyPair <MEC2EC2CreateKeyPairCreateKeyPairRequestNET45>`
method, which returns a :sdk-net-api-v2:`CreateKeyPairResponse <TEC2CreateKeyPairResponseNET45>` object.

The response object includes a :sdk-net-api-v2:`CreateKeyPairResult <TEC2CreateKeyPairResultNET45>` property
that contains the new key's :sdk-net-api-v2:`KeyPair <TEC2KeyPairNET45>` object. The :code:`KeyPair` object's
:sdk-net-api-v2:`KeyMaterial <PEC2KeyPairKeyMaterialNET45>` property contains the unencrypted private key for
the key pair. Save the private key as a :file:`.pem` file in a safe location. You'll need this file
when you connect to your instance. This example saves the private key in the current directory,
using the name of the key pair as the base file name of the :file:`.pem` file.

.. code-block:: csharp

    if (myKeyPair == null)
    {
        var newKeyRequest = new CreateKeyPairRequest()
        {
            KeyName = keyPairName
        };
        var ckpResponse = ec2Client.CreateKeyPair(newKeyRequest);
        Console.WriteLine();
        Console.WriteLine("New key: " + keyPairName);
    
        // Save the private key in a .pem file
        using (FileStream s = new FileStream(keyPairName + ".pem", FileMode.Create))
        using (StreamWriter writer = new StreamWriter(s))
        {
            writer.WriteLine(ckpResponse.KeyPair.KeyMaterial);
        }
    }

.. toctree::
   :titlesonly:
   :maxdepth: 1
   
   run-instance
   terminate-instance
   