.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _elastic-ip-addresses-ec2:

###################################
Using Elastic IP Addresses in |EC2|
###################################

.. meta::
   :description: Use this .NET code example to learn how to work with Elastic IP addresses in Amazon EC2.
   :keywords: AWS SDK for .NET examples, Elastic IP addresses

This .NET example shows you how to:

* Retrieve descriptions of your Elastic IP addresses
* Allocate and associate an Elastic IP address with an |EC2| instance
* Release an Elastic IP address

The Scenario
============

An Elastic IP address is a static IP address designed for dynamic cloud computing. An Elastic IP address
is associated with your AWS account, and is a public IP address reachable from the Internet.

If your |EC2| instance doesn't have a public IP address, you can associate an Elastic IP address  with
your instance to enable communication with the Internet.

In this example, you use the |sdk-net| to manage Elastic IP addresses by using these methods of the
|EC2| client class:

* :sdk-net-api:`DescribeAddresses <EC2/MEC2EC2DescribeAddressesDescribeAddressesRequest>`
* :sdk-net-api:`AllocateAddress <EC2/MEC2EC2AllocateAddressAllocateAddressRequest>`
* :sdk-net-api:`AssociateAddress <EC2/MEC2EC2AssociateAddressAssociateAddressRequest>`
* :sdk-net-api:`ReleaseAddress <EC2/MEC2EC2ReleaseAddressReleaseAddressRequest>`


For more information about Elastic IP addresses in |EC2|, see
:ec2-ug-win:`Elastic IP Addresses <elastic-ip-addresses-eip>` in the |EC2-ug-win|.

Describe Elastic IP Addresses
=============================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` object. Next, create a :sdk-net-api:`DescribeAddressesRequest <EC2/TEC2DescribeAddressesRequest>`
object to pass as parameter, filtering the addresses returned by those in your VPC. To retrieve descriptions
of all your Elastic IP addresses, omit the filter from the parameters.
Then call the :sdk-net-api:`DescribeAddresses <EC2/MEC2EC2DescribeAddressesDescribeAddressesRequest>`
method of the :code:`AmazonEC2Client` object.

.. code-block:: csharp

        public void DescribeElasticIps()
        {
            using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
            {
                var addresses = client.DescribeAddresses(new DescribeAddressesRequest
                {
                    Filters = new List<Filter>
                    {
                        new Filter
                        {
                            Name = "domain",
                            Values = new List<string> { "vpc" }
                        }
                    }
                }).Addresses;

                foreach(var address in addresses)
                {
                    Console.WriteLine(address.PublicIp);
                    Console.WriteLine("\tAllocation Id: " + address.AllocationId);
                    Console.WriteLine("\tPrivate IP Address: " + address.PrivateIpAddress);
                    Console.WriteLine("\tAssociation Id: " + address.AssociationId);
                    Console.WriteLine("\tInstance Id: " + address.InstanceId);
                    Console.WriteLine("\tNetwork Interface Owner Id: " + address.NetworkInterfaceOwnerId);
                }
            }
        }



Allocate and Associate an Elastic IP Address
============================================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` object. Next, create an :sdk-net-api:`AllocateAddressRequest <EC2/TEC2AllocateAddressRequest>`
object for the parameters used to allocate an Elastic IP address, which in this case specifies that the
domain is a VPC. Call the :sdk-net-api:`AllocateAddress <EC2/MEC2EC2AllocateAddressAllocateAddressRequest>` method of the
:code:`AmazonEC2Client` object.

Upon success, the returned :sdk-net-api:`AllocateAddressResponse <EC2/TEC2AllocateAddressResponse>` object
has an :code:`AllocationId` property that identifies the allocated Elastic IP address.

Create an :sdk-net-api:`AssociateAddressRequest <EC2/TEC2AssociateAddressRequest>` object for the parameters
used to associate an Elastic IP address to an |EC2| instance. Include the :code:`AllocationId` from the
newly allocated address and the :code:`InstanceId` of the |EC2| instance. Then call the
:sdk-net-api:`AssociateAddress <EC2/MEC2EC2AssociateAddressAssociateAddressRequest>` method of
the :code:`AmazonEC2Client` object.

.. code-block:: csharp

        public void AllocateAndAssociate(string instanceId)
        {
            using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
            {
                var allocationId = client.AllocateAddress(new AllocateAddressRequest
                {
                    Domain = DomainType.Vpc
                }).AllocationId;

                Console.WriteLine("Allocation Id: " + allocationId);

                var associationId = client.AssociateAddress(new AssociateAddressRequest
                {
                    AllocationId = allocationId,
                    InstanceId = instanceId
                }).AssociationId;

                Console.WriteLine("Association Id: " + associationId);
            }
        }



Release an Elastic IP Address
=============================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` object. Next, create a :sdk-net-api:`ReleaseAddressRequest <EC2/TEC2ReleaseAddressRequest>`
object for the parameters used to release an Elastic IP address, which in this case specifies the
:code:`AllocationId` for the Elastic IP address. Releasing an Elastic IP address also disassociates it
from any |EC2| instance. Call the :sdk-net-api:`ReleaseAddress <EC2/MEC2EC2ReleaseAddressReleaseAddressRequest>`
method
of the |EC2| service object.

.. code-block:: csharp

        public void Release(string allocationId)
        {
            using (var client = new AmazonEC2Client(RegionEndpoint.USWest2))
            {
                client.ReleaseAddress(new ReleaseAddressRequest
                {
                    AllocationId = allocationId
                });
            }
        }
