--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Looking for **\.NET Core** or **ASP\.NET Core**? Go to *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)*\. It covers cross\-platform development in addition to Windows and Visual Studio\.

--------

# Managing IAM Access Keys<a name="iam-examples-managing-access-keys"></a>

These \.NET examples shows you how to:
+ Create an access key for a user
+ Get the date that an access key was last used
+ Update the status for an access key
+ Delete an access key

## The Scenario<a name="the-scenario"></a>

Users need their own access keys to make programmatic calls to AWS from the AWS SDK for \.NET\. To meet this need, you can create, modify, view, or rotate access keys \(access key IDs and secret access keys\) for IAM users\. When you create an access key, its status is Active by default, which means the user can use the access key for API calls\.

The C\# code uses the AWS SDK for \.NET to manage IAM access keys using these methods of the [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) class:
+  [CreateAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateAccessKeyCreateAccessKeyRequest.html) 
+  [ListAccessKeys](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccessKeysListAccessKeysRequest.html) 
+  [GetAccessKeyLastUsed](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetAccessKeyLastUsedGetAccessKeyLastUsedRequest.html) 
+  [UpdateAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateAccessKeyUpdateAccessKeyRequest.html) 
+  [DeleteAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteAccessKeyDeleteAccessKeyRequest.html) 

For more information about IAM access keys, see [Managing Access Keys for IAM Users](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_access-keys.html) in the IAM User Guide\.

## Create Access Keys for a User<a name="create-access-keys-for-a-user"></a>

Call the `CreateAccessKey` method to create an access key named `S3UserReadOnlyAccess` for the IAM access keys examples\. The `CreateAccessKey method first creates a user named :code:`S3UserReadOnlyAccess` with read only access rights by calling the `CreateUser` method\. It then creates an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object and a [CreateAccessKeyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateAccessKeyRequest.html) object containing the `UserName` parameter needed to create new access keys\. It then calls the [CreateAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateAccessKeyCreateAccessKeyRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void CreateAccessKey()
{
    try
    {
        CreateUser();
        var iamClient = new AmazonIdentityManagementServiceClient();
        // Create an access key for the IAM user that can be used by the SDK
        var accessKey = iamClient.CreateAccessKey(new CreateAccessKeyRequest
        {
            // Use the user created in the CreateUser example
            UserName = "S3UserReadOnlyAccess"
        }).AccessKey;

    }
    catch (LimitExceededException e)
    {
        Console.WriteLine(e.Message);
    }
}

public static User CreateUser()
{
    var iamClient = new AmazonIdentityManagementServiceClient();
    try
    {
        // Create the IAM user
        var readOnlyUser = iamClient.CreateUser(new CreateUserRequest
        {
            UserName = "S3UserReadOnlyAccess"
        }).User;

        // Assign the read-only policy to the new user
        iamClient.PutUserPolicy(new PutUserPolicyRequest
        {
            UserName = readOnlyUser.UserName,
            PolicyName = "S3ReadOnlyAccess",
            PolicyDocument = S3_READONLY_POLICY
        });
        return readOnlyUser;
    }
    catch (EntityAlreadyExistsException e)
    {
        Console.WriteLine(e.Message);
        var request = new GetUserRequest()
        {
            UserName = "S3UserReadOnlyAccess"
        };

        return iamClient.GetUser(request).User;

    }
}
```

## List a User’s Access Keys<a name="list-a-user-s-access-keys"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object and a [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html) object containing the parameters needed to retrieve the user’s access keys\. This includes the IAM user’s name and, optionally, the maximum number of access key pairs you want to list\. Call the [ListAccessKeys](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccessKeysListAccessKeysRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void ListAccessKeys()
{

    var iamClient = new AmazonIdentityManagementServiceClient();
    var requestAccessKeys = new ListAccessKeysRequest
    {
        // Use the user created in the CreateAccessKey example
        UserName = "S3UserReadOnlyAccess",
        MaxItems = 10
    };
    var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
    Console.WriteLine("  Access keys:");

    foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
    {
        Console.WriteLine("    {0}", accessKey.AccessKeyId);
     }
}
```

## Get the Last Used Date for Access Keys<a name="get-the-last-used-date-for-access-keys"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object and a [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html) object containing the `UserName` parameter needed to list the access keys\. Call the [ListAccessKeys](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccessKeysListAccessKeysRequest.html) method of the `AmazonIdentityManagementServiceClient` object\. Loop through the access keys returned, displaying the `AccessKeyId` of each key and using it to create a [GetAccessKeyLastUsedRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetAccessKeyLastUsedRequest.html) object\. Call the [GetAccessKeyLastUsed](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetAccessKeyLastUsedGetAccessKeyLastUsedRequest.html) method and display the time that the key was last used on the console\.

```
public static void GetAccessKeysLastUsed()
{

    var iamClient = new AmazonIdentityManagementServiceClient();
    var requestAccessKeys = new ListAccessKeysRequest
    {
        // Use the user we created in the CreateUser example
        UserName = "S3UserReadOnlyAccess"
    };
    var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
    Console.WriteLine("  Access keys:");

    foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
    {
        Console.WriteLine("    {0}", accessKey.AccessKeyId);
        GetAccessKeyLastUsedRequest request = new GetAccessKeyLastUsedRequest()
            { AccessKeyId = accessKey.AccessKeyId };
        var response = iamClient.GetAccessKeyLastUsed(request);
        Console.WriteLine("Key last used " + response.AccessKeyLastUsed.LastUsedDate.ToLongDateString());
    }
}
```

## Update the Status of an Access Key<a name="update-the-status-of-an-access-key"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object and a [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html) object containing the user name to list the keys for\. The user name in this example is the user created for the other examples\. Call the [ListAccessKeys](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccessKeysListAccessKeysRequest.html) method of the `AmazonIdentityManagementServiceClient`\. The [ListAccessKeysResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysResponse.html) that is returned contains a list of the access keys for that user\. Use the first access key in the list\. Create an [UpdateAccessKeyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TUpdateAccessKeyRequest.html) object, providing the `UserName`, `AccessKeyId`, and `Status` parameters\. Call the [UpdateAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateAccessKeyUpdateAccessKeyRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void UpdateKeyStatus()
{
    // This example changes the status of the key specified by its index in the list of access keys
    // Optionally, you could change the keynumber parameter to be an AccessKey ID
    var iamClient = new AmazonIdentityManagementServiceClient();
    var requestAccessKeys = new ListAccessKeysRequest
    {
        UserName = "S3UserReadOnlyAccess"
    };
    var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
    UpdateAccessKeyRequest updateRequest = new UpdateAccessKeyRequest
        {
            UserName = "S3UserReadOnlyAccess",
            AccessKeyId = responseAccessKeys.AccessKeyMetadata[0].AccessKeyId,
            Status = StatusType.Active
        };
    iamClient.UpdateAccessKey(updateRequest);
    Console.WriteLine("  Access key " + updateRequest.AccessKeyId + " updated");
}
```

## Delete Access Keys<a name="delete-access-keys"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object and a [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html) object containing the name of the user as a parameter\. Call the [ListAccessKeys](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccessKeysListAccessKeysRequest.html) method of the `AmazonIdentityManagementServiceClient`\. The [ListAccessKeysResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysResponse.html) that is returned contains a list of the access keys for that user\. Delete each access key in the list by calling the [DeleteAccessKey](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteAccessKeyDeleteAccessKeyRequest.html) method of the `AmazonIdentityManagementServiceClient`\.

```
public static void DeleteAccessKeys()
{
// Delete all the access keys created for the examples
    var iamClient = new AmazonIdentityManagementServiceClient();
    var requestAccessKeys = new ListAccessKeysRequest
    {
        // Use the user created in the CreateUser example
        UserName = "S3UserReadOnlyAccess"
    };
    var responseAccessKeys = iamClient.ListAccessKeys(requestAccessKeys);
    Console.WriteLine("  Access keys:");

    foreach (var accessKey in responseAccessKeys.AccessKeyMetadata)
    {
        Console.WriteLine("    {0}", accessKey.AccessKeyId);
        iamClient.DeleteAccessKey(new DeleteAccessKeyRequest
        {
            UserName = "S3UserReadOnlyAccess",
            AccessKeyId = accessKey.AccessKeyId
        });
        Console.WriteLine("Access Key " + accessKey.AccessKeyId + " deleted");
    }

}
```