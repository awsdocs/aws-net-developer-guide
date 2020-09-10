--------

**Take the survey\!**

Help us improve the AWS SDK for \.NET and its documentation by sharing your experience\. [Click here to take a quick survey\.](https://amazonmr.au1.qualtrics.com/jfe/form/SV_2nThyxw3YlloC7H)

--------

# Creating and listing users for your AWS account<a name="iam-users-create"></a>

This example shows you how to use the AWS SDK for \.NET to create a new IAM user\. With the information you supply to the application, it creates a user, attaches the given managed policy, obtains credentials for the user, and then displays a list of all the users in your AWS account\.

In you don't supply any command\-line arguments, the application simply displays a list of all the users in your AWS account\.

One of the inputs you supply is the Amazon Resource Name \(ARN\) for an existing managed policy\. You can find the available policies and their ARNs in the [IAM console](https://console.aws.amazon.com/iam/home#/policies)\.

The following sections provide snippets of this example\. The [complete code for the example](#iam-users-create-complete-code) is shown after that, and can be built and run as is\.

**Topics**
+ [Create a user](#iam-users-create-create)
+ [Display a list of users](#iam-users-create-display)
+ [Complete code](#iam-users-create-complete-code)
+ [Additional considerations](#iam-users-create-additional)

## Create a user<a name="iam-users-create-create"></a>

The following snippet creates an IAM user, adds the given managed security policy, and then creates and stores credentials for the user\.

The example [at the end of this topic](#iam-users-create-complete-code) shows this snippet in use\.

```
    //
    // Method to create the user
    private static async Task<CreateUserResponse> CreateUser(
      IAmazonIdentityManagementService iamClient, string userName,
      string policyArn, string csvFilename)
    {
      // Create the user
      // Could also create a login profile for the user by using CreateLoginProfileAsync
      CreateUserResponse responseCreate =
        await iamClient.CreateUserAsync(new CreateUserRequest(userName));

      // Attach an existing managed policy
      await iamClient.AttachUserPolicyAsync(new AttachUserPolicyRequest{
        UserName = responseCreate.User.UserName,
        PolicyArn = policyArn});

      // Create credentials and write them to a CSV file.
      CreateAccessKeyResponse responseCreds =
        await iamClient.CreateAccessKeyAsync(new CreateAccessKeyRequest{
          UserName = responseCreate.User.UserName});
      using (FileStream s = new FileStream(csvFilename, FileMode.Create))
      using (StreamWriter writer = new StreamWriter(s))
      {
        writer.WriteLine("User name,Access key ID,Secret access key");
        writer.WriteLine("{0},{1},{2}", responseCreds.AccessKey.UserName,
          responseCreds.AccessKey.AccessKeyId,
          responseCreds.AccessKey.SecretAccessKey);
      }

      return responseCreate;
    }
```

## Display a list of users<a name="iam-users-create-display"></a>

The following snippet displays a list of existing users, as well as information about each user such as access key IDs and attached policies\.

The example [at the end of this topic](#iam-users-create-complete-code) shows this snippet in use\.

```
    //
    // Method to print out a list of the existing users and information about them
    private static async Task ListUsers(IAmazonIdentityManagementService iamClient)
    {
      // Get the list of users
      ListUsersResponse responseUsers = await iamClient.ListUsersAsync();
      Console.WriteLine("\nFull list of users...");
      foreach (var user in responseUsers.Users)
      {
        Console.WriteLine($"User {user.UserName}:");
        Console.WriteLine($"\tCreated: {user.CreateDate.ToShortDateString()}");

        // Show the list of groups this user is part of
        ListGroupsForUserResponse responseGroups =
          await iamClient.ListGroupsForUserAsync(
            new ListGroupsForUserRequest(user.UserName));
        foreach (var group in responseGroups.Groups)
          Console.WriteLine($"\tGroup: {group.GroupName}");

        // Show the list of access keys for this user
        ListAccessKeysResponse responseAccessKeys =
          await iamClient.ListAccessKeysAsync(
            new ListAccessKeysRequest{UserName = user.UserName});
        foreach(AccessKeyMetadata accessKey in responseAccessKeys.AccessKeyMetadata)
          Console.WriteLine($"\tAccess key ID: {accessKey.AccessKeyId}");

        // Show the list of managed policies attached to this user
        var requestManagedPolicies = new ListAttachedUserPoliciesRequest{
          UserName = user.UserName};
        ListAttachedUserPoliciesResponse responseManagedPolicies =
          await iamClient.ListAttachedUserPoliciesAsync(
            new ListAttachedUserPoliciesRequest{UserName = user.UserName});
        foreach(var policy in responseManagedPolicies.AttachedPolicies)
          Console.WriteLine($"\tManaged policy name: {policy.PolicyName}");

        // Show the list of inline policies attached to this user
        ListUserPoliciesResponse responseInlinePolicies =
          await iamClient.ListUserPoliciesAsync(
            new ListUserPoliciesRequest(user.UserName));
        foreach(var policy in responseInlinePolicies.PolicyNames)
          Console.WriteLine($"\tInline policy name: {policy}");
      }
    }
```

## Complete code<a name="iam-users-create-complete-code"></a>

This section shows relevant references and the complete code for this example\.

### SDK references<a name="w4aac19c21c19c23b5b1"></a>

NuGet packages:
+ [AWSSDK\.IdentityManagement](https://www.nuget.org/packages/AWSSDK.IdentityManagement)

Programming elements:
+ Namespace [Amazon\.IdentityManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAM.html)

  Class [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html)
+ Namespace [Amazon\.IdentityManagement\.Model](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/NIAMModel.html)

  Class [AttachUserPolicyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TAttachUserPolicyRequest.html)

  Class [CreateAccessKeyRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateAccessKeyRequest.html)

  Class [CreateAccessKeyResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateAccessKeyResponse.html)

  Class [CreateUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateUserRequest.html)

  Class [CreateUserResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateUserResponse.html)

  Class [ListAccessKeysRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysRequest.html)

  Class [ListAccessKeysResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAccessKeysResponse.html)

  Class [ListAttachedUserPoliciesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAttachedUserPoliciesRequest.html)

  Class [ListAttachedUserPoliciesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListAttachedUserPoliciesResponse.html)

  Class [ListGroupsForUserRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListGroupsForUserRequest.html)

  Class [ListGroupsForUserResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListGroupsForUserResponse.html)

  Class [ListUserPoliciesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListUserPoliciesRequest.html)

  Class [ListUserPoliciesResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListUserPoliciesResponse.html)

  Class [ListUsersResponse](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListUsersResponse.html)

### The code<a name="w4aac19c21c19c23b7b1"></a>

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

namespace IamCreateUser
{
  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class to create a user
  class Program
  {
    private const int MaxArgs = 3;

    static async Task Main(string[] args)
    {
      // Create an IAM service client
      var iamClient = new AmazonIdentityManagementServiceClient();

      // Parse the command line and show help if necessary
      var parsedArgs = CommandLine.Parse(args);
      if((parsedArgs.Count == 0) || (parsedArgs.Count > MaxArgs))
      {
        PrintHelp();
        Console.WriteLine("\nIncorrect number of arguments specified.");
        Console.Write("Do you want to see a list of the existing users? ((y) or n): ");
        string response = Console.ReadLine();
        if((string.IsNullOrEmpty(response)) || (response.ToLower() == "y"))
          await ListUsers(iamClient);
        return;
      }

      // Get the application parameters from the parsed arguments
      string userName =
        CommandLine.GetParameter(parsedArgs, null, "-u", "--user-name");
      string policyArn =
        CommandLine.GetParameter(parsedArgs, null, "-p", "--policy-arn");
      string csvFilename =
        CommandLine.GetParameter(parsedArgs, null, "-c", "--csv-filename");
      if(   (string.IsNullOrEmpty(policyArn) || !policyArn.StartsWith("arn:"))
         || (string.IsNullOrEmpty(csvFilename) || !csvFilename.EndsWith(".csv"))
         || (string.IsNullOrEmpty(userName)))
        CommandLine.ErrorExit(
          "\nOne or more of the required arguments is missing or incorrect." +
          "\nRun the command with no arguments to see help.");

      // Create a user, attach a managed policy, and obtain credentials
      var responseCreate =
        await CreateUser(iamClient, userName, policyArn, csvFilename);
      Console.WriteLine($"\nUser {responseCreate.User.UserName} was created.");
      Console.WriteLine($"User ID: {responseCreate.User.UserId}");

      // Output a list of the existing users
      await ListUsers(iamClient);
    }


    //
    // Method to create the user
    private static async Task<CreateUserResponse> CreateUser(
      IAmazonIdentityManagementService iamClient, string userName,
      string policyArn, string csvFilename)
    {
      // Create the user
      // Could also create a login profile for the user by using CreateLoginProfileAsync
      CreateUserResponse responseCreate =
        await iamClient.CreateUserAsync(new CreateUserRequest(userName));

      // Attach an existing managed policy
      await iamClient.AttachUserPolicyAsync(new AttachUserPolicyRequest{
        UserName = responseCreate.User.UserName,
        PolicyArn = policyArn});

      // Create credentials and write them to a CSV file.
      CreateAccessKeyResponse responseCreds =
        await iamClient.CreateAccessKeyAsync(new CreateAccessKeyRequest{
          UserName = responseCreate.User.UserName});
      using (FileStream s = new FileStream(csvFilename, FileMode.Create))
      using (StreamWriter writer = new StreamWriter(s))
      {
        writer.WriteLine("User name,Access key ID,Secret access key");
        writer.WriteLine("{0},{1},{2}", responseCreds.AccessKey.UserName,
          responseCreds.AccessKey.AccessKeyId,
          responseCreds.AccessKey.SecretAccessKey);
      }

      return responseCreate;
    }


    //
    // Method to print out a list of the existing users and information about them
    private static async Task ListUsers(IAmazonIdentityManagementService iamClient)
    {
      // Get the list of users
      ListUsersResponse responseUsers = await iamClient.ListUsersAsync();
      Console.WriteLine("\nFull list of users...");
      foreach (var user in responseUsers.Users)
      {
        Console.WriteLine($"User {user.UserName}:");
        Console.WriteLine($"\tCreated: {user.CreateDate.ToShortDateString()}");

        // Show the list of groups this user is part of
        ListGroupsForUserResponse responseGroups =
          await iamClient.ListGroupsForUserAsync(
            new ListGroupsForUserRequest(user.UserName));
        foreach (var group in responseGroups.Groups)
          Console.WriteLine($"\tGroup: {group.GroupName}");

        // Show the list of access keys for this user
        ListAccessKeysResponse responseAccessKeys =
          await iamClient.ListAccessKeysAsync(
            new ListAccessKeysRequest{UserName = user.UserName});
        foreach(AccessKeyMetadata accessKey in responseAccessKeys.AccessKeyMetadata)
          Console.WriteLine($"\tAccess key ID: {accessKey.AccessKeyId}");

        // Show the list of managed policies attached to this user
        var requestManagedPolicies = new ListAttachedUserPoliciesRequest{
          UserName = user.UserName};
        ListAttachedUserPoliciesResponse responseManagedPolicies =
          await iamClient.ListAttachedUserPoliciesAsync(
            new ListAttachedUserPoliciesRequest{UserName = user.UserName});
        foreach(var policy in responseManagedPolicies.AttachedPolicies)
          Console.WriteLine($"\tManaged policy name: {policy.PolicyName}");

        // Show the list of inline policies attached to this user
        ListUserPoliciesResponse responseInlinePolicies =
          await iamClient.ListUserPoliciesAsync(
            new ListUserPoliciesRequest(user.UserName));
        foreach(var policy in responseInlinePolicies.PolicyNames)
          Console.WriteLine($"\tInline policy name: {policy}");
      }
    }


    //
    // Command-line help
    private static void PrintHelp()
    {
      Console.WriteLine(
        "\nUsage: IamCreateUser -u <user-name> -p <policy-arn> -c <csv-filename>" +
        "\n  -u, --user-name: The name of the user you want to create." +
        "\n  -p, --policy-arn: The ARN of an existing managed policy." +
        "\n  -c, --csv-filename: The name of a .csv file to write the credentials to.");
    }
  }


  // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
  // Class that represents a command line on the console or terminal.
  // (This is the same for all examples. When you have seen it once, you can ignore it.)
  static class CommandLine
  {
    // Method to parse a command line of the form: "--param value" or "-p value".
    // If "param" is found without a matching "value", Dictionary.Value is an empty string.
    // If "value" is found without a matching "param", Dictionary.Key is "--NoKeyN"
    //  where "N" represents sequential numbers.
    public static Dictionary<string,string> Parse(string[] args)
    {
      var parsedArgs = new Dictionary<string,string>();
      int i = 0, n = 0;
      while(i < args.Length)
      {
        // If the first argument in this iteration starts with a dash it's an option.
        if(args[i].StartsWith("-"))
        {
          var key = args[i++];
          var value = string.Empty;

          // Is there a value that goes with this option?
          if((i < args.Length) && (!args[i].StartsWith("-"))) value = args[i++];
          parsedArgs.Add(key, value);
        }

        // If the first argument in this iteration doesn't start with a dash, it's a value
        else
        {
          parsedArgs.Add("--NoKey" + n.ToString(), args[i++]);
          n++;
        }
      }

      return parsedArgs;
    }

    //
    // Method to get a parameter from the parsed command-line arguments
    public static string GetParameter(
      Dictionary<string,string> parsedArgs, string def, params string[] keys)
    {
      string retval = null;
      foreach(var key in keys)
        if(parsedArgs.TryGetValue(key, out retval)) break;
      return retval ?? def;
    }

    //
    // Exit with an error.
    public static void ErrorExit(string msg, int code=1)
    {
      Console.WriteLine("\nError");
      Console.WriteLine(msg);
      Environment.Exit(code);
    }
  }

}
```

## Additional considerations<a name="iam-users-create-additional"></a>
+ You can also see the list of users and the results of this example in the [IAM console](https://console.aws.amazon.com/iam/home#/users)\.