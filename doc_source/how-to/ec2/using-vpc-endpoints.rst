.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _vpc-endpoints-ec2:

##############################
Using VPC Endpoints with |EC2|
##############################

.. meta::
   :description: Use this .NET code example to learn how to work with VPC endpoints in Amazon EC2.
   :keywords: AWS SDK for .NET examples, VPC endpoints

This .NET example shows you how to create, describe, modify, and delete VPC endpoints.

The Scenario
============

An endpoint enables you to create a private connection between your VPC and another AWS service in your
account. You can specify a policy to attach to the endpoint that will control access to the service
from your VPC. You can also specify the VPC route tables that use the endpoint.

This example uses the following :sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` methods:

* :sdk-net-api:`CreateVpcEndpoint <EC2/MEC2CreateVpcEndpointCreateVpcEndpointRequest>`
* :sdk-net-api:`DescribeVpcEndpoints <EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest>`
* :sdk-net-api:`ModifyVpcEndpoint <EC2/MEC2ModifyVpcEndpointModifyVpcEndpointRequest>`
* :sdk-net-api:`DeleteVpcEndpoints <EC2/MEC2DeleteVpcEndpointsDeleteVpcEndpointsRequest>`

Create a VPC Endpoint
=====================

The following example creates a VPC endpoint for an Amazon Simple Storage Service (S3).

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` instance. You'll create a new VPC
so that you can create a VPC endpoint.

Create a :sdk-net-api:`CreateVpcRequest <EC2/TCreateVpcRequest>`
object specifying an IPv4 CIDR block as its constructor's parameter. Using that :sdk-net-api:`CreateVpcRequest <EC2/TCreateVpcRequest>`
object, use the :sdk-net-api:`CreateVpc <EC2/MEC2CreateVpcCreateVpcRequest>` method to create a VPC.
Use that VPC to instantiate a :sdk-net-api:`CreateVpcEndpointRequest <EC2/TCreateVpcEndpointRequest>`
object, specifying the service name for the endpoint. Then, use that request object to call the
:sdk-net-api:`CreateVpcEndpoint <EC2/MEC2CreateVpcEndpointCreateVpcEndpointRequest>` method and
create the :sdk-net-api:`VpcEndpoint <EC2/TVpcEndpoint>`.

.. code-block:: csharp

        public static void CreateVPCEndpoint()
        {
            AmazonEC2Client client = new AmazonEC2Client();
            CreateVpcRequest vpcRequest = new CreateVpcRequest("10.32.0.0/16");
            CreateVpcResponse vpcResponse = client.CreateVpc(vpcRequest);
            Vpc vpc = vpcResponse.Vpc;
            CreateVpcEndpointRequest endpointRequest = new CreateVpcEndpointRequest();
            endpointRequest.VpcId = vpc.VpcId;
            endpointRequest.ServiceName = "com.amazonaws.us-west-2.s3";
            CreateVpcEndpointResponse cVpcErsp = client.CreateVpcEndpoint(endpointRequest);
            VpcEndpoint vpcEndPoint = cVpcErsp.VpcEndpoint;
        }


Describe a VPC Endpoint
=======================

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` instance. Next, create a
:sdk-net-api:`DescribeVpcEndpointsRequest <EC2/TDescribeVpcEndpointsRequest>` object and limit the
maximum number of results to return to 5. Use that :code:`DescribeVpcEndpointsRequest` object to call the
:sdk-net-api:`DescribeVpcEndpoints <EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest>` method.
The :sdk-net-api:`DescribeVpcEndpointsResponse <EC2/TDescribeVpcEndpointsResponse>` that is returned
contains the list of VPC Endpoints.

.. code-block:: csharp

        public static void DescribeVPCEndPoints()
        {
            AmazonEC2Client client = new AmazonEC2Client();
            DescribeVpcEndpointsRequest endpointRequest = new DescribeVpcEndpointsRequest();
            endpointRequest.MaxResults = 5;
            DescribeVpcEndpointsResponse endpointResponse = client.DescribeVpcEndpoints(endpointRequest);
            List<VpcEndpoint> endpointList = endpointResponse.VpcEndpoints;
            foreach (VpcEndpoint vpc in endpointList)
            {
                Console.WriteLine("VpcEndpoint ID = " + vpc.VpcEndpointId);
                List<string> routeTableIds = vpc.RouteTableIds;
                foreach (string id in routeTableIds)
                {
                    Console.WriteLine("\tRoute Table ID = " + id);
                }

            }
        }

Modify a VPC Endpoint
=====================

The following example modifies attributes of a specified VPC endpoint. You can modify the policy associated
with the endpoint, and you can add and remove route tables associated with the endpoint.

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` instance. Create a :sdk-net-api:`ModifyVpcEndpointRequest <EC2/TModifyVpcEndpointRequest>`
object using the ID of the VPC endpoint and the ID of the route table to add to it. Call the
:sdk-net-api:`ModifyVpcEndpoint <EC2/MEC2ModifyVpcEndpointModifyVpcEndpointRequest>` method using the
:code:`ModifyVpcEndpointRequest` object. The :sdk-net-api:`ModifyVpcEndpointResponse <EC2/TModifyVpcEndpointResponse>`
object that is returned contains an HTTP status code indicating whether the modify request succeeded.

.. code-block:: csharp

        public static void ModifyVPCEndPoint()
        {
            AmazonEC2Client client = new AmazonEC2Client();
            ModifyVpcEndpointRequest modifyRequest = new ModifyVpcEndpointRequest();
            modifyRequest.VpcEndpointId = "vpce-17b05a7e";
            modifyRequest.AddRouteTableIds = new List<string> { "rtb-c46f15a3" };
            ModifyVpcEndpointResponse modifyResponse = client.ModifyVpcEndpoint(modifyRequest);
            HttpStatusCode status = modifyResponse.HttpStatusCode;
            if (status.ToString() == "OK")
                Console.WriteLine("ModifyHostsRequest succeeded");
            else
                Console.WriteLine("ModifyHostsRequest failed");


Delete a VPC Endpoint
=====================

You can delete one or more specified VPC endpoints. Deleting the endpoint also deletes the endpoint routes
in the route tables that were associated with the endpoint.

Create an :sdk-net-api:`AmazonEC2Client <EC2/TEC2Client>` instance. Use the
:sdk-net-api:`DescribeVpcEndpoints <EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest>` method
to list the VPC endpoints associated with the EC2 client. Use the list of VPC endpoints to create a list
of VPC endpoint IDs. Use that list to create a :sdk-net-api:`DeleteVpcEndpointsRequest <EC2/TDeleteVpcEndpointsRequest>`
object to be used by the :sdk-net-api:`DeleteVpcEndpoints <EC2/MEC2DeleteVpcEndpointsDeleteVpcEndpointsRequest>`
method.

.. code-block:: csharp

        private static void DeleteVPCEndPoint()
        {
            AmazonEC2Client client = new AmazonEC2Client();
            DescribeVpcEndpointsRequest endpointRequest = new DescribeVpcEndpointsRequest();
            endpointRequest.MaxResults = 5;
            DescribeVpcEndpointsResponse endpointResponse = client.DescribeVpcEndpoints(endpointRequest);
            List<VpcEndpoint> endpointList = endpointResponse.VpcEndpoints;
            var vpcEndPointListIds = new List<string>();
            foreach (VpcEndpoint vpc in endpointList)
            {
                Console.WriteLine("VpcEndpoint ID = " + vpc.VpcEndpointId);
                vpcEndPointListIds.Add(vpc.VpcEndpointId);
            }
            DeleteVpcEndpointsRequest deleteRequest = new DeleteVpcEndpointsRequest();
            deleteRequest.VpcEndpointIds = vpcEndPointListIds;
            client.DeleteVpcEndpoints(deleteRequest);
        }
