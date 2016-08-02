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

You can access the features of |EC2| using the |sdk-net|. For example, you can create, start, and 
terminate EC2 instances.

The sample code in this tutorial is written in C#, but you can use the |sdk-net| with any compatible 
language. The |sdk-net| installs a set of C# project templates, so the simplest way to start this 
project is to open Visual Studio, choose :guilabel:`New Project` from the :guilabel:`File` menu, 
and then choose :guilabel:`AWS Empty Project`.

**Prerequisites**

Before you begin, be sure that you have created an AWS account and set up your AWS credentials. For
more information, see :ref:`net-dg-setup`.

Tasks
=====

The following tasks demonstrate how to manage EC2 instances using the |sdk-net|.

.. toctree::
    :titlesonly:
    :maxdepth: 1

    init-ec2-client
    security-groups
    key-pairs
    run-instance
    terminate-instance
