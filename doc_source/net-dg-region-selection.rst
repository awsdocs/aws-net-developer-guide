.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

AWS regions allow you to access AWS services that reside physically in a specific geographic region.
This can be useful both for redundancy and to keep your data and applications running close to where
you and your users will access them. To select a particular region, configure the AWS client object
with an endpoint that corresponds to that region.

For example:

.. code-block:: csharp

    AmazonEC2Config config = new AmazonEC2Config();
    config.ServiceURL = "https://us-west-2.amazonaws.com";
    Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials("profile_name");
    AmazonEC2Client ec2 = new AmazonEC2Client(credentials, config);

You can also specify the region using the :sdk-net-api-v2:`RegionEndpoint <TRegionEndpointNET45>` class. 
Here is an example that instantiates an |EC2| client using :sdk-net-api-v2:`AWSClientFactory
<TAWSClientFactoryNET45>` and specifies the region:

.. code-block:: csharp

    Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials("profile_name");
    AmazonEC2Client ec2 = AWSClientFactory.CreateAmazonEC2Client(
       credentials, RegionEndpoint.USEast1 );

Regions are isolated from each other. For example, you can't access *US East* resources when using
the *EU West* region. If your code needs access to multiple AWS regions, we recommend that you
create a client specific to each region.

Regions are logically isolated from each other; you can't access another region's resources when
communicating with the |cnnorth1-name| endpoint.

Go to |regions-and-endpoints|_ in the |aws-gr| to view the current list of regions and
corresponding endpoints for each of the services offered by AWS.


