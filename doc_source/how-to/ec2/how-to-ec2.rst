.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _how-to-ec2:

########################
|EC2| Instances Examples
########################

You can access the features of |EC2| using the |sdk-net|. For example, you can create, start, and
terminate |EC2| instances.

The sample code is written in C#, but you can use the |sdk-net| with any compatible
language. When you install the |TVSlong| a set of C# project templates are installed. So the easiest
way to start this project is to open Visual Studio, and then choose :guilabel:`File`, :guilabel:`New Project`,
:guilabel:`AWS Sample Projects`, :guilabel:`Compute and Networking`,
:guilabel:`AWS EC2 Sample`.

**Prerequisites**

Before you begin, be sure that you have created an AWS account and set up your AWS credentials. For
more information, see :ref:`net-dg-setup`.

**Examples**

.. toctree::
    :titlesonly:
    :maxdepth: 1

    init-ec2-client
    security-groups
    key-pairs
    run-instance
    terminate-instance
    using-regions-and-availability-zones
    using-vpc-endpoints
    ec2-example-elastic-ip-addresses
