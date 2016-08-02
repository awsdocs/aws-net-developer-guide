.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _create-security-group:

###########################################
Create a Security Group Using the |sdk-net|
###########################################

In |EC2|, a security group acts as a virtual firewall that controls the network traffic for one or
more EC2 instances. By default, |EC2| associates your instances with a security group that allows no
inbound traffic. You can create a security group that allows your EC2 instances to accept certain
traffic. For example, if you need to connect to an EC2 Windows instance, you must configure the
security group to allow RDP traffic. You can create a security group using the |EC2| console or the
|sdk-net|.

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
================================

You can enumerate your security groups and check whether a security group exists.

**To enumerate your security groups**

Get the complete list of your security groups using 
:sdk-net-api:`DescribeSecurityGroups <EC2/MEC2EC2DescribeSecurityGroupsDescribeSecurityGroupsRequest>` 
with no parameters. The following example enumerates all of the security groups in the region.

.. code-block:: csharp

    static void EnumerateSecurityGroups(AmazonEC2Client ec2Client)
    {
      var request = new DescribeSecurityGroupsRequest();
      var response = ec2Client.DescribeSecurityGroups(request);
      List<SecurityGroup> mySGs = response.SecurityGroups;
      foreach (SecurityGroup item in mySGs)
      {
        Console.WriteLine("Security group: " + item.GroupId);
        Console.WriteLine("\tGroupId: " + item.GroupId);
        Console.WriteLine("\tGroupName: " + item.GroupName);
        Console.WriteLine("\tVpcId: " + item.VpcId);
    
        Console.WriteLine();
      }
    }

**To enumerate your security groups for a VPC**

To enumerate the security groups for a particular VPC, use 
:sdk-net-api:`DescribeSecurityGroups <EC2/MEC2EC2DescribeSecurityGroupsDescribeSecurityGroupsRequest>` 
with a filter. The following example only retrieves the security groups that belong to the specified 
VPC.

.. code-block:: csharp

    static void EnumerateVpcSecurityGroups(AmazonEC2Client ec2Client, string vpcID)
    {
      Filter vpcFilter = new Filter
      {
        Name = "vpc-id",
        Values = new List<string>() { vpcID }
      };
    
      var request = new DescribeSecurityGroupsRequest();
      request.Filters.Add(vpcFilter);
      var response = ec2Client.DescribeSecurityGroups(request);
      List<SecurityGroup> mySGs = response.SecurityGroups;
      foreach (SecurityGroup item in mySGs)
      {
        Console.WriteLine("Security group: " + item.GroupId);
        Console.WriteLine("\tGroupId: " + item.GroupId);
        Console.WriteLine("\tGroupName: " + item.GroupName);
        Console.WriteLine("\tVpcId: " + item.VpcId);
    
        Console.WriteLine();
      }
    }


.. _creating-security-group:

Creating a Security Group
=========================

If you attempt to create a security group with a name of an existing security group,
:sdk-net-api:`CreateSecurityGroup <EC2/MEC2EC2CreateSecurityGroupCreateSecurityGroupRequest>` will throw 
an exception. To avoid this, the following examples search for a security group with the specified
name, and return the appropriate :sdk-net-api:`SecurityGroup <EC2/TEC2SecurityGroup>` object if one is found.

**To create a security group for EC2-Classic**

Create and initialize a :sdk-net-api:`CreateSecurityGroupRequest <TEC2CreateSecurityGroupRequest>` object.
Assign a name and description to the :code:`GroupName` and :code:`Description` properties,
respectively.

The :sdk-net-api:`CreateSecurityGroup <EC2/MEC2EC2CreateSecurityGroupCreateSecurityGroupRequest>` method
returns a :sdk-net-api:`CreateSecurityGroupResponse <EC2/TEC2CreateSecurityGroupRequest>` object. You 
can get the identifier of the new security group from the response and then use 
:sdk-net-api:`DescribeSecurityGroups <EC2/MEC2EC2DescribeSecurityGroupsDescribeSecurityGroupsRequest>` 
with the security group identifier to get the :sdk-net-api:`SecurityGroup <EC2/TEC2SecurityGroup>` object
for the security group.

.. code-block:: csharp

    static SecurityGroup CreateEc2SecurityGroup(
      AmazonEC2Client ec2Client, 
      string secGroupName)
    {
      // See if a security group with the specified name already exists.
      Filter nameFilter = new Filter();
      nameFilter.Name = "group-name";
      nameFilter.Values= new List<string>() { secGroupName };
    
      var describeRequest = new DescribeSecurityGroupsRequest();
      describeRequest.Filters.Add(nameFilter);
      var describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
    
      // If a match was found, return the SecurityGroup object for the security group.
      if(describeResponse.SecurityGroups.Count > 0)
      {
        return describeResponse.SecurityGroups[0];
      }
    
      // Create the security group.
      var createRequest = new CreateSecurityGroupRequest();
      createRequest.GroupName = secGroupName;
      createRequest.Description = "My sample security group for EC2-Classic";
    
      var createResponse = ec2Client.CreateSecurityGroup(createRequest);
    
      var Groups = new List<string>() { createResponse.GroupId };
      describeRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
      describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
      return describeResponse.SecurityGroups[0];
    }

**To create a security group for EC2-VPC**

Create and initialize a :sdk-net-api:`CreateSecurityGroupRequest <EC2/TEC2CreateSecurityGroupRequest>` 
object. Assign values to the :code:`GroupName`, :code:`Description`, and :code:`VpcId` properties.

The :sdk-net-api:`CreateSecurityGroup <EC2/MEC2EC2CreateSecurityGroupCreateSecurityGroupRequest>` method
returns a :sdk-net-api:`CreateSecurityGroupResponse <EC2/TEC2CreateSecurityGroupRequest>` object. You 
can get the identifier of the new security group from the response and then use 
:sdk-net-api:`DescribeSecurityGroups <EC2/MEC2EC2DescribeSecurityGroupsDescribeSecurityGroupsRequest>` 
with the security group identifier to get the :sdk-net-api:`SecurityGroup <EC2/TEC2SecurityGroup>` 
object for the security group.

.. code-block:: csharp

    static SecurityGroup CreateVpcSecurityGroup(
      AmazonEC2Client ec2Client, 
      string vpcId, 
      string secGroupName)
    {
      // See if a security group with the specified name already exists.
      Filter nameFilter = new Filter();
      nameFilter.Name = "group-name";
      nameFilter.Values = new List<string>() { secGroupName };
    
      var describeRequest = new DescribeSecurityGroupsRequest();
      describeRequest.Filters.Add(nameFilter);
      var describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
    
      // If a match was found, return the SecurityGroup object for the security group.
      if (describeResponse.SecurityGroups.Count > 0)
      {
        return describeResponse.SecurityGroups[0];
      }
    
      // Create the security group.
      var createRequest = new CreateSecurityGroupRequest();
      createRequest.GroupName = secGroupName;
      createRequest.Description = "My sample security group for EC2-VPC";
      createRequest.VpcId = vpcId;
    
      var createResponse = ec2Client.CreateSecurityGroup(createRequest);
    
      var Groups = new List<string>() { createResponse.GroupId };
      describeRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
      describeResponse = ec2Client.DescribeSecurityGroups(describeRequest);
      return describeResponse.SecurityGroups[0];
    }


.. _authorize-ingress:

Adding Rules to Your Security Group
===================================

Use the following procedure to add a rule to allow inbound traffic on TCP port 3389 (RDP). This
enables you to connect to a Windows instance. If you're launching a Linux instance, use TCP port 22
(SSH) instead.

.. tip:: You can use a service to get the public IP address of your local computer. For example, we provide
   the following service: http://checkip.amazonaws.com/. To locate another service that provides
   your IP address, use the search phrase "what is my IP address". If you are connecting through an
   ISP or from behind your firewall without a static IP address, you need to find out the range of
   IP addresses used by client computers.

The examples in this section follow from the examples in the previous sections. They assume
:code:`secGroup` is an existing security group.

**To add a rule to a security group**

1. Create and initialize an :sdk-net-api:`IpPermission <EC2/TEC2IpPermission>` object.

   .. code-block:: csharp

      string ipRange = "0.0.0.0/0";
      List<string> ranges = new List<string>() { ipRange };
      
      var ipPermission = new IpPermission();
      ipPermission.IpProtocol = "tcp";
      ipPermission.FromPort = 3389;
      ipPermission.ToPort = 3389;
      ipPermission.IpRanges = ranges;
 
   :code:`IpProtocol`
      The IP protocol.
 
   :code:`FromPort` and :code:`ToPort`
      The beginning and end of the port range. This example specifies a single port, 3389, which
      is used to communicate with Windows over RDP.
 
   :code:`IpRanges`
      The IP addresses or address ranges, in CIDR notation. For convenience, this example uses
      :code:`0.0.0.0/0`, which authorizes network traffic from all IP addresses. This is
      acceptable for a short time in a test environment, but is unsafe in a production
      environment.
 
2. Create and initialize an 
   :sdk-net-api:`AuthorizeSecurityGroupIngressRequest  <EC2/TEC2AuthorizeSecurityGroupIngressRequest>` object.

   .. code-block:: csharp

      var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
      ingressRequest.GroupId = secGroup.GroupId;
      ingressRequest.IpPermissions.Add(ipPermission);

   :code:`GroupId`
      The identifier of the security group.

   :code:`IpPermissions`
      The :code:`IpPermission` object from step 1.

3. (Optional) You can add additional rules to the :code:`IpPermissions` collection before going to the
   next step.

4. Pass the :sdk-net-api:`AuthorizeSecurityGroupIngressRequest <EC2/TEC2AuthorizeSecurityGroupIngressRequest>`
   object to the :sdk-net-api:`AuthorizeSecurityGroupIngress <EC2/MEC2EC2AuthorizeSecurityGroupIngressAuthorizeSecurityGroupIngressRequest>` 
   method, which returns an :sdk-net-api:`AuthorizeSecurityGroupIngressResponse <EC2/TEC2AuthorizeSecurityGroupIngressResponse>` 
   object. If a matching rule already exists, an :sdk-net-api:`AmazonEC2Exception <EC2/TEC2EC2Exception>` 
   is thrown.

   .. code-block:: csharp

      try
      {
        var ingressResponse = ec2Client.AuthorizeSecurityGroupIngress(ingressRequest);
        Console.WriteLine("New RDP rule for: " + ipRange);
      }
      catch (AmazonEC2Exception ex)
      {
        // Check the ErrorCode to see if the rule already exists.
        if ("InvalidPermission.Duplicate" == ex.ErrorCode)
        {
          Console.WriteLine("An RDP rule for: {0} already exists.", ipRange);
        }
        else
        {
          // The exception was thrown for another reason, so re-throw the exception.
          throw;
        }
      }
