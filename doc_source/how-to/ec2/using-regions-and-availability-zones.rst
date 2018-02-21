.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _regions-and-availabilityzones-ec2:

###############################################
Using Regions and Availability Zones with |EC2|
###############################################

.. meta::
   :description: Use this .NET code example to describe Availability Zones and regions in Amazon EC2.
   :keywords: AWS SDK for .NET examples, regions and Availability Zones


This .NET example shows you how to:

* Get details about Availability Zones
* Get details about regions

The Scenario
============

|EC2| is hosted in multiple locations worldwide. These locations are composed of regions and
Availability Zones. Each region is a separate geographic area that has multiple, isolated
locations known as Availability Zones. |EC2| provides the ability to place instances and data in
multiple locations.

You can use the |sdk-net| to retrieve details about regions and Availability Zones by using
the following methods of the :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` class:

* :sdk-net-api:`DescribeAvailabilityZones <EC2/MEC2EC2DescribeAvailabilityZonesDescribeAvailabilityZonesRequest>`
* :sdk-net-api:`DescribeRegions <EC2/MEC2EC2DescribeRegionsDescribeRegionsRequest>`

For more information about regions and Availability Zones, see
:ec2-ug-win:`Regions and Availability Zones <using-regions-availability-zones>` in the
|EC2-ug-win|.

Describe Availability Zones
===========================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` instance and call the
:sdk-net-api:`DescribeAvailabilityZones <EC2/MEC2EC2DescribeAvailabilityZonesDescribeAvailabilityZonesRequest>`
method. The :sdk-net-api:`DescribeAvailabilityZonesResponse <EC2/TEC2DescribeAvailabilityZonesResponse>`
object that is
returned contains a list of Availability Zones.

.. code-block:: csharp

        public static void DescribeAvailabilityZones()
        {
            Console.WriteLine("Describe Availability Zones");
            AmazonEC2Client client = new AmazonEC2Client();
            DescribeAvailabilityZonesResponse response = client.DescribeAvailabilityZones();
            var availZones = new List<AvailabilityZone>();
            availZones = response.AvailabilityZones;
            foreach (AvailabilityZone az in availZones)
            {
                Console.WriteLine(az.ZoneName);
            }
        }

Describe Regions
================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` instance and call the
:sdk-net-api:`DescribeRegions <EC2/MEC2EC2DescribeRegionsDescribeRegionsRequest>` method. The
:sdk-net-api:`DescribeRegionsResponse <EC2/TEC2DescribeRegionsResponse>` object that is returned
contains a list of regions.

.. code-block:: csharp

        public static void DescribeRegions()
        {
            Console.WriteLine("Describe Regions");
            AmazonEC2Client client = new AmazonEC2Client();
            DescribeRegionsResponse response = client.DescribeRegions();
            var regions = new List<Region>();
            regions = response.Regions;
            foreach (Region region in regions)
            {
                Console.WriteLine(region.RegionName);
            }
        }
