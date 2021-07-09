--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

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