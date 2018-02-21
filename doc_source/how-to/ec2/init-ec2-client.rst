.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _init-ec2-client:

########################
Creating an |EC2| Client
########################

.. meta::
   :description: Use this .NET code example to learn how to create an Amazon EC2 client.
   :keywords: AWS SDK for .NET examples, EC2 clients


Create an |EC2| client to manage your EC2 resources, such as instances and security groups. This
client is represented by an :sdk-net-api:`AmazonEC2Client <EC2/TEC2EC2Client>` object, which
you can create as follows.

.. code-block:: csharp

    var ec2Client = new AmazonEC2Client();

The permissions for the client object are determined by the policy attached to the profile you
specified in the :file:`App.config` file. By default, we use the region specified in
:file:`App.config`. To use a different region, pass the appropriate
:sdk-net-api:`RegionEndpoint <Amazon/TRegionEndpoint>` value to the constructor. For more information, see
:rande:`Regions and Endpoints: EC2 <ec2>` in the |AWS-gr|.
