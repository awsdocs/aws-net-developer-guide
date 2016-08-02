.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _terminate-instance:

Terminate an EC2 Instance Using the the SDK
===========================================

When you no longer need one or more of your EC2 instances, you can terminate them.

**To terminate an EC2 instance**

Create and initialize a :sdk-net-api-v2:`TerminateInstancesRequest <TEC2TerminateInstancesRequestNET45>` 
object. Set the :sdk-net-api-v2:`InstanceIds <PEC2TerminateInstancesRequestInstanceIdsNET45>` property to a
list of one or more instance IDs. In this example, :code:`instanceIds` is the list that you saved
when you launched the instances.

Pass the request object to the client object's 
:sdk-net-api-v2:`TerminateInstances <MEC2EC2TerminateInstancesTerminateInstancesRequestNET45>` method.

.. code-block:: csharp

    var deleteRequest = new TerminateInstancesRequest()
    {
        InstanceIds = instanceIds
    };
    var deleteResponse = ec2Client.TerminateInstances(deleteRequest);
    foreach (InstanceStateChange item in deleteResponse.TerminatingInstances)
    {
        Console.WriteLine();
        Console.WriteLine("Terminated instance: " + item.InstanceId);
        Console.WriteLine("Instance state: " + item.CurrentState.Name);
    }

**To list the terminated instances**

You can use the response object as follows to list the terminated instances.

.. code-block:: csharp

    List<InstanceStateChange> terminatedInstances = termResponse.TerminateInstancesResult.TerminatingInstance;
    foreach(InstanceStateChange item in terminatedInstances)
    {
      Console.WriteLine("Terminated Instance: " + item.InstanceId);
    }


