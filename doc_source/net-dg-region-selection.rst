.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

AWS Regions allow you to access AWS services that physically reside in a specific geographic region.
This can be useful for redundancy and to keep your data and applications running close to
where you and your users will access them. You can specify a region when creating the AWS service
client by using the :sdk-net-api:`RegionEndpoint <Amazon/TRegionEndpoint>` class.

Here is an example that instantiates an |EC2| client in a specific region.

.. code-block:: none

     AmazonEC2Client ec2Client = new AmazonEC2Client(RegionEndpoint.USEast1);

Regions are isolated from each other. For example, you can't access |region-us-east-1| resources
when using the |region-eu-west-1| region. If your code needs access to multiple AWS Regions, we
recommend you create a separate client for each region.

To use services in the China (Beijing) Region, you must have an account and credentials that are 
specific to the China (Beijing) Region. Accounts and credentials for other AWS Regions won't work for 
the China (Beijing) Region. Likewise, accounts and credentials for the China (Beijing) Region won't 
work for other AWS Regions. For information about endpoints and protocols that are available in the 
China (Beijing) Region, see `China (Beijing) Region <http://docs.amazonaws.cn/en_us/general/latest/gr/rande.html#cnnorth_region>`_.

New AWS services can be launched initially in a few regions and then supported in other regions. In 
these cases you don't need to install the latest SDK to access the new regions. You can specify newly 
added regions on a per-client basis or globally.

Per-Client
==========

Setting the Region in a client takes precedence over any global setting.

Construct the new region endpoint by using :sdk-net-api:`GetBySystemName <Amazon/MRegionEndpointGetBySystemNameString>`:

.. code-block:: c#

            var newRegion = RegionEndpoint.GetBySystemName("us-west-new");
            using (var ec2Client = new AmazonEC2Client(newRegion))
            {
              // Make a request to EC2 using ec2Client
            }
 
You can also use the :code:`ServiceURL` property of the service client configuration class to specify the 
region. This technique works even if the region endpoint does not follow the regular region endpoint pattern. 
  
.. code-block:: c#
  
            var ec2ClientConfig = new AmazonEC2Config
            {
                // Specify the endpoint explicitly 
                ServiceURL = "https://ec2.us-west-new.amazonaws.com" 
            };

            using (var ec2Client = new AmazonEC2Client(newRegion))
            {
              // Make a request to EC2 using ec2Client
            }

Globally
========

There are a number of ways you can specify a Region for all clients.
The |sdk-net| looks for a Region value in the following order:

Set as a :code-csharp:`AWSConfigs.AWSRegion` property, 
  
.. code-block:: c#

            AWSConfigs.AWSRegion = "us-west-new";
            using (var ec2Client = new AmazonEC2Client())
            {
              // Make request to Amazon EC2 using ec2Client
            }

Set as a :code:`AWSRegion` key in the :code:`appSettings` section of the :code:`app.config` 
file.

.. code-block:: c#
 
            <configuration>
              <appSettings>
                <add key="AWSRegion" value="us-west-2"/>
              </appSettings>
            </configuration>
            
Set as a :code:`region` attribute in the :code:`aws` section as described in 
`AWSRegion <http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-other.html#awsregion>`_.
   
.. code-block:: c#
 
        <aws region="us-west-2"/>

To view the current list of all supported regions and endpoints for each AWS service, see |regions-and-endpoints|_ 
in the |AWS-gr|.
