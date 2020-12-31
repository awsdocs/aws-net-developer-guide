--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Using VPC Endpoints with Amazon EC2<a name="using-vpc-endpoints"></a>

This \.NET example shows you how to create, describe, modify, and delete VPC endpoints\.

## The Scenario<a name="the-scenario"></a>

An endpoint enables you to create a private connection between your VPC and another AWS service in your account\. You can specify a policy to attach to the endpoint that will control access to the service from your VPC\. You can also specify the VPC route tables that use the endpoint\.

This example uses the following [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) methods:
+  [CreateVpcEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateVpcEndpointCreateVpcEndpointRequest.html) 
+  [DescribeVpcEndpoints](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest.html) 
+  [ModifyVpcEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2ModifyVpcEndpointModifyVpcEndpointRequest.html) 
+  [DeleteVpcEndpoints](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DeleteVpcEndpointsDeleteVpcEndpointsRequest.html) 

## Create a VPC Endpoint<a name="create-a-vpc-endpoint"></a>

The following example creates a VPC endpoint for an Amazon Simple Storage Service \(S3\)\.

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance\. You’ll create a new VPC so that you can create a VPC endpoint\.

Create a [CreateVpcRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateVpcRequest.html) object specifying an IPv4 CIDR block as its constructor’s parameter\. Using that [CreateVpcRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateVpcRequest.html) object, use the [CreateVpc](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateVpcCreateVpcRequest.html) method to create a VPC\. Use that VPC to instantiate a [CreateVpcEndpointRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TCreateVpcEndpointRequest.html) object, specifying the service name for the endpoint\. Then, use that request object to call the [CreateVpcEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2CreateVpcEndpointCreateVpcEndpointRequest.html) method and create the [VpcEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TVpcEndpoint.html)\.

```
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
```

## Describe a VPC Endpoint<a name="describe-a-vpc-endpoint"></a>

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance\. Next, create a [DescribeVpcEndpointsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeVpcEndpointsRequest.html) object and limit the maximum number of results to return to 5\. Use that `DescribeVpcEndpointsRequest` object to call the [DescribeVpcEndpoints](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest.html) method\. The [DescribeVpcEndpointsResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDescribeVpcEndpointsResponse.html) that is returned contains the list of VPC Endpoints\.

```
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
```

## Modify a VPC Endpoint<a name="modify-a-vpc-endpoint"></a>

The following example modifies attributes of a specified VPC endpoint\. You can modify the policy associated with the endpoint, and you can add and remove route tables associated with the endpoint\.

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance\. Create a [ModifyVpcEndpointRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TModifyVpcEndpointRequest.html) object using the ID of the VPC endpoint and the ID of the route table to add to it\. Call the [ModifyVpcEndpoint](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2ModifyVpcEndpointModifyVpcEndpointRequest.html) method using the `ModifyVpcEndpointRequest` object\. The [ModifyVpcEndpointResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TModifyVpcEndpointResponse.html) object that is returned contains an HTTP status code indicating whether the modify request succeeded\.

```
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
```

## Delete a VPC Endpoint<a name="delete-a-vpc-endpoint"></a>

You can delete one or more specified VPC endpoints\. Deleting the endpoint also deletes the endpoint routes in the route tables that were associated with the endpoint\.

Create an [AmazonEC2Client](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html) instance\. Use the [DescribeVpcEndpoints](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DescribeVpcEndpointsDescribeVpcEndpointsRequest.html) method to list the VPC endpoints associated with the EC2 client\. Use the list of VPC endpoints to create a list of VPC endpoint IDs\. Use that list to create a [DeleteVpcEndpointsRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TDeleteVpcEndpointsRequest.html) object to be used by the [DeleteVpcEndpoints](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/MEC2DeleteVpcEndpointsDeleteVpcEndpointsRequest.html) method\.

```
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
```