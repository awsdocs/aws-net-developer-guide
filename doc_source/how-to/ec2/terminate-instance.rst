.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _terminate-instance:

###########################################
Terminate an EC2 Instance Using the the SDK
###########################################

When you no longer need one or more of your EC2 instances, you can terminate them.

**To terminate an EC2 instance**

1. Create and initialize a :sdk-net-api:`TerminateInstancesRequest <EC2/TEC2TerminateInstancesRequest>` object.

2. Set the :code:`TerminateInstancesRequest.InstanceIds` property to a list of one or more instance
   IDs. These identify the instances to terminate.

3. Pass the request object to the 
   :sdk-net-api:`TerminateInstances<EC2/MEC2EC2TerminateInstancesTerminateInstancesRequest>` 
   method. If the specified instance
   does not exist, an :sdk-net-api:`AmazonEC2Exception <EC2/TEC2EC2Exception>` is thrown.

4. You can use the :sdk-net-api:`TerminateInstancesResponse <EC2/TEC2TerminateInstancesResponse>` object as 
   follows to list the terminated instances.

.. code-block:: csharp

    public static void TerminateInstance(
      AmazonEC2Client ec2Client, 
      string instanceId)
    {
      var request = new TerminateInstancesRequest();
      request.InstanceIds = new List<string>() { instanceId };
    
      try
      {
        var response = ec2Client.TerminateInstances(request);
        foreach (InstanceStateChange item in response.TerminatingInstances)
        {
          Console.WriteLine("Terminated instance: " + item.InstanceId);
          Console.WriteLine("Instance state: " + item.CurrentState.Name);
        }
      }
      catch(AmazonEC2Exception ex)
      {
        // Check the ErrorCode to see if the instance does not exist.
        if ("InvalidInstanceID.NotFound" == ex.ErrorCode)
        {
          Console.WriteLine("Instance {0} does not exist.", instanceId);
        }
        else
        {
          // The exception was thrown for another reason, so re-throw the exception.
          throw;
        }
      }
    }


