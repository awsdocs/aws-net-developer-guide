--------

End of support announcement: [http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/](http://aws.amazon.com/blogs/developer/announcing-the-end-of-support-for-the-aws-sdk-for-net-version-2/)\.

This documentation is for version 2\.0 of the AWS SDK for \.NET\.** For current content, see the [latest version](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/) of the AWS SDK for \.NET developer guide instead\.**

--------

# Terminate an EC2 Instance Using the the SDK<a name="terminate-instance"></a>

## Version 2 content \(see announcement above\)<a name="w3aac13c13b7c11b3b1"></a>

When you no longer need one or more of your EC2 instances, you can terminate them\.

 **To terminate an EC2 instance** 

Create and initialize a [TerminateInstancesRequest](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/TEC2TerminateInstancesRequestNET45.html) object\. Set the [InstanceIds](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/PEC2TerminateInstancesRequestInstanceIdsNET45.html) property to a list of one or more instance IDs\. In this example, `instanceIds` is the list that you saved when you launched the instances\.

Pass the request object to the client object’s [TerminateInstances](https://docs.aws.amazon.com/sdkfornet/latest/apidocs/MEC2EC2TerminateInstancesTerminateInstancesRequestNET45.html) method\.

```
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
```

 **To list the terminated instances** 

You can use the response object as follows to list the terminated instances\.

```
List<InstanceStateChange> terminatedInstances = termResponse.TerminateInstancesResult.TerminatingInstance;
foreach(InstanceStateChange item in terminatedInstances)
{
  Console.WriteLine("Terminated Instance: " + item.InstanceId);
}
```