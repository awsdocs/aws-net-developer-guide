.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-region-selection:

####################
AWS Region Selection
####################

AWS regions allow you to access AWS services that physically reside in a specific geographic region.
This can be useful both for redundancy, and to keep your data and applications running close to
where you and your users will access them. You can specify a region when creating the AWS service
client using the :sdk-net-api:`RegionEndpoint <Amazon/TRegionEndpoint>` class.

Here is an example that instantiates an |EC2| client in a specific region:

.. code-block:: none

     AmazonEC2Client ec2Client = new AmazonEC2Client(RegionEndpoint.USEast1);

Regions are isolated from each other. For example, you can't access |region-us-east-1| resources
when using the |region-eu-west-1| region. If your code needs access to multiple AWS regions, we
recommend you create a separate client for each region.

Regions are logically isolated from each other. You can't access another region's resources when
communicating with the |region-cn-north-1| region endpoint.

To view the current list of regions and endpoints for each AWS service, see |regions-and-endpoints|_ 
in the |AWS-gr|.


