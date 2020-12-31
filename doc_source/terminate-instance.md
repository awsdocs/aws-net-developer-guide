--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Terminating an Amazon EC2 Instance<a name="terminate-instance"></a>

When you no longer need one or more of your Amazon EC2 instances, you can terminate them\.

For information on creating an Amazon EC2 client, see [Creating an Amazon EC2 Client](init-ec2-client.md)\.

**To terminate an EC2 instance**

1. Create and initialize a [TerminateInstancesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TTerminateInstancesRequest.html) object\.

1. Set the `TerminateInstancesRequest.InstanceIds` property to a list of one or more instance IDs for the instances to terminate\.

1. Pass the request object to the [TerminateInstances](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2TerminateInstancesTerminateInstancesRequest.html) method\. If the specified instance doesn’t exist, an [AmazonEC2Exception](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Exception.html) is thrown\.

1. You can use the [TerminateInstancesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TTerminateInstancesResponse.html) object to list the terminated instances, as follows\.

   ```
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
   ```