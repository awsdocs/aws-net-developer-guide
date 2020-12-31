--------

This documentation is for version 3\.0 of the AWS SDK for \.NET, which is mostly centered around \.NET Framework and ASP\.NET 4\.*x*, Windows, and Visual Studio\.

The latest version of the documentation at [https://docs\.aws\.amazon\.com/sdk\-for\-net/latest/developer\-guide/](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html) is mostly centered around \.NET Core and ASP\.NET Core\. In addition to Windows and Visual Studio, it gives equal consideration to cross\-platform development\.

--------

# Managing IAM Aliases for your AWS account ID<a name="iam-examples-account-aliases"></a>

These \.NET examples show you how to:
+ Create an account alias for your AWS account ID
+ List an account alias for your AWS account ID
+ Delete an account alias for your AWS account ID

## The Scenario<a name="the-scenario"></a>

If you want the URL for your sign\-in page to contain your company name or other friendly identifier instead of your AWS account ID, you can create an alias for your AWS account ID\. If you create an AWS account alias, your sign\-in page URL changes to incorporate the alias\.

The following examples demonstrate how to manage your AWS account alias by using these methods of the [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) class:
+  [CreateAccountAlias](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateAccountAliasCreateAccountAliasRequest.html) 
+  [ListAccountAliases](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccountAliasesListAccountAliasesRequest.html) 
+  [DeleteAccountAlias](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteAccountAliasDeleteAccountAliasRequest.html) 

For more information about IAM account aliases, see [Your AWS Account ID and Its Alias](https://docs.aws.amazon.com/IAM/latest/UserGuide/console_account-alias.html) in the IAM User Guide\.

## Create an Account Alias<a name="create-an-account-alias"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [CreateAccountAliasRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateAccountAliasRequest.html) object containing the new account alias you want to use\. Call the [CreateAccountAlias](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceCreateAccountAliasCreateAccountAliasRequest.html) method of the `AmazonIAMClient` object\. If the account alias is created, display the new alias on the console\. If the name already exists, write the exception message to the console\.

```
public static void CreateAccountAlias()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new CreateAccountAliasRequest();
        request.AccountAlias = "my-aws-account-alias-2017";
        var response = iamClient.CreateAccountAlias(request);
        if (response.HttpStatusCode.ToString() == "OK")
            Console.WriteLine(request.AccountAlias + " created.");
        else
            Console.WriteLine("HttpStatusCode returned = " + response.HttpStatusCode.ToString());
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
```

## List Account Aliases<a name="list-account-aliases"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [ListAccountAliasesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TCreateAccountAliasRequest.html) object\. Call the [ListAccountAliases](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListAccountAliasesListAccountAliasesRequest.html) method of the `AmazonIAMClient` object\. If there is an account alias, display it on the console\.

If there is no account alias, write the exception message to the console\.

**Note**  
There can be only one account alias\.

```
public static void ListAccountAliases()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new ListAccountAliasesRequest();
        var response = iamClient.ListAccountAliases(request);
        List<string> aliases = response.AccountAliases;
        foreach (string account in aliases)
        {
            Console.WriteLine("The account alias is: " + account);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
```

## Delete an Account Alias<a name="delete-an-account-alias"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [DeleteAccountAliasRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDeleteAccountAliasRequest.html) object containing the account alias you want to delete\. Call the [DeleteAccountAlias](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteAccountAliasDeleteAccountAliasRequest.html) method of the `AmazonIAMClient` object\. If the account alias is deleted, display the delete information on the console\. If the name doesn’t exist, write the exception message to the console\.

```
public static void DeleteAccountAlias()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new DeleteAccountAliasRequest();
        request.AccountAlias = "my-aws-account-alias-2017";
        var response = iamClient.DeleteAccountAlias(request);
        if (response.HttpStatusCode.ToString() == "OK")
            Console.WriteLine(request.AccountAlias + " deleted.");
        else
            Console.WriteLine("HttpStatusCode returned = " + response.HttpStatusCode.ToString());
    }
    catch (NoSuchEntityException e)
    {
        Console.WriteLine(e.Message);
    }
}
```