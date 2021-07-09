--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Managing IAM Users<a name="iam-examples-managing-users"></a>

This \.NET example shows you how to retrieve a list of IAM users, create and delete IAM users, and update an IAM user name\.

You can create and manage users in IAM using these methods of the [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) class:
+  [CreateUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateUserCreateUserRequest.html) 
+  [ListUsers](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListUsersListUsersRequest.html) 
+  [UpdateUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateUserUpdateUserRequest.html) 
+  [GetUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetUserGetUserRequest.html) 
+  [DeleteUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteUserDeleteUserRequest.html) 

For more information about IAM users, see [IAM Users](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_users.html) in the IAM User Guide\.

For information about limitations on the number of IAM users you can create, see [Limitations on IAM Entities](https://docs.aws.amazon.com/IAM/latest/UserGuide/iam-limits.html.html) in the IAM User Guide\.

## Create a User for Your AWS Account<a name="create-a-user-for-your-aws-account"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [CreateUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateUserRequest.html) object containing the user name you want to use for the new user\. Call the [CreateUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateUserCreateUserRequest.html) method of the `AmazonIAMClient` object\. If the user name doesn’t currently exist, display the name and the ARN for the user on the console\. If the name already exists, write a message to that effect to the console\.

```
var client = new AmazonIdentityManagementServiceClient();
var request = new CreateUserRequest
{
    UserName = "DemoUser"
};

try
{
    var response = client.CreateUser(request);

    Console.WriteLine("User Name = '{0}', ARN = '{1}'",
      response.User.UserName, response.User.Arn);
}
catch (EntityAlreadyExistsException)
{
    Console.WriteLine("User 'DemoUser' already exists.");
}
```

## List Users in Your AWS Account<a name="list-users-in-your-aws-account"></a>

This example lists the IAM users that have the specified path prefix\. If no path prefix is specified, the action returns all users in the AWS account\. If there are no users, the action returns an empty list\.

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [ListUsersRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListUsersRequest.html) object containing the parameters needed to list your users\. Limit the number returned by setting the `MaxItems` parameter to 10\. Call the [ListUsers](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListUsersListUsersRequest.html) method of the `AmazonIdentityManagementServiceClient` object\. Write each user’s name and creation date to the console\.

```
public static void ListUsers()
{
    var iamClient = new AmazonIdentityManagementServiceClient();
    var requestUsers = new ListUsersRequest() { MaxItems = 10 };
    var responseUsers = iamClient.ListUsers(requestUsers);

    foreach (var user in responseUsers.Users)
    {
        Console.WriteLine("User " + user.UserName  + " Created: " + user.CreateDate.ToShortDateString());
    }

}
```

## Update a User’s Name<a name="update-a-user-s-name"></a>

This example shows how to update the name or the path of the specified IAM user\. Be sure you understand the implications of changing an IAM user’s path or name\. For more information, see [Renaming an IAM User](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_users_renaming.html) in the IAM User Guide\.

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create an [UpdateUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TUpdateUserRequest.html) object, specifying both the current and new user names as parameters\. Call the [UpdateUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateUserUpdateUserRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
 public static void UpdateUser()
{
    var client = new AmazonIdentityManagementServiceClient();
    var request = new UpdateUserRequest
    {
        UserName = "DemoUser",
        NewUserName = "NewUser"
    };

    try
    {
        var response = client.UpdateUser(request);

    }
    catch (EntityAlreadyExistsException)
    {
        Console.WriteLine("User 'NewUser' already exists.");
    }
}
```

## Get Information about a User<a name="get-information-about-a-user"></a>

This example shows how to retrieve information about the specified IAM user, including the user’s creation date, path, unique ID, and ARN\. If you don’t specify a user name, IAM determines the user name implicitly based on the AWS access key ID used to sign the request to this API\.

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [GetUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetUserRequest.html) object containing the user name you want to get information about\. Call the [GetUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetUserGetUserRequest.html) method of the `AmazonIdentityManagementServiceClient` object to get the information\. If the user doesn’t exist, an exception is thrown\.

```
public static void GetUser()
{
    var client = new AmazonIdentityManagementServiceClient();
    var request = new GetUserRequest()
    {
        UserName = "DemoUser"
    };

    try
    {
        var response = client.GetUser(request);
        Console.WriteLine("Creation date: " + response.User.CreateDate.ToShortDateString());
        Console.WriteLine("Password last used: " + response.User.PasswordLastUsed.ToShortDateString());
        Console.WriteLine("UserId = " + response.User.UserId);

    }
    catch (NoSuchEntityException)
    {
        Console.WriteLine("User 'DemoUser' does not exist.");
    }
}
```

## Delete a User<a name="delete-a-user"></a>

This example shows how to delete the specified IAM user\. The user must not belong to any groups or have any access keys, signing certificates, or attached policies\.

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [DeleteUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDeleteUserRequest.html) object containing the parameters needed, which consists of the user name you want to delete\. Call the [DeleteUser](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteUserDeleteUserRequest.html) method of the `AmazonIdentityManagementServiceClient` object to delete it\. If the user doesn’t exist, an exception is thrown\.

```
public static void DeleteUser()
{
    var client = new AmazonIdentityManagementServiceClient();
    var request = new DeleteUserRequest()
    {
        UserName = "DemoUser"
    };

    try
    {
        var response = client.DeleteUser(request);

    }
    catch (NoSuchEntityException)
    {
        Console.WriteLine("User DemoUser' does not exist.");
    }
}
```