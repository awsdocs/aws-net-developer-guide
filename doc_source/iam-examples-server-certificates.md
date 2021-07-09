--------

This content focuses on **\.NET Framework** and **ASP\.NET 4\.x**\. It covers Windows and Visual Studio\.

Working with **\.NET Core** or **ASP\.NET Core**? Go to the content for *[version 3\.5 or later](https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/welcome.html)* of the AWS SDK for \.NET\. It covers cross\-platform development in addition to Windows and Visual Studio\.

## <a name="w8aab3b5"></a>

Hello AWS \.NET community\! Please share your experience and help us improve the AWS SDK for \.NET and its learning resources by [taking a survey](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)\. This survey takes approximately 10 minute to complete\.

 [ ![\[Image NOT FOUND\]](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/images/SurveyButton.png) ](https://amazonmr.au1.qualtrics.com/jfe/form/SV_bqfQLfZ5nhFUiV0)

--------

# Working with IAM Server Certificates<a name="iam-examples-server-certificates"></a>

These \.NET examples show you how to:
+ List server certificates
+ Get server certificates
+ Update server certificates
+ Delete server certificates

## The Scenario<a name="the-scenario"></a>

In these, examples, you’ll basic tasks for managing server certificates for HTTPS connections\. To enable HTTPS connections to your website or application on AWS, you need an SSL/TLS server certificate\. To use a certificate that you obtained from an external provider with your website or application on AWS, you must upload the certificate to IAM or import it into AWS Certificate Manager\.

These examples use the AWS SDK for \.NET to send and receive messages by using these methods of the [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) class:
+  [ListServerCertificates](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListServerCertificatesListServerCertificatesRequest.html) 
+  [GetServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetServerCertificateGetServerCertificateRequest.html) 
+  [UpdateServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateServerCertificateUpdateServerCertificateRequest.html) 
+  [DeleteServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteServerCertificateDeleteServerCertificateRequest.html) 

For more information about server certificates, see [Working with Server Certificates](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_credentials_server-certs.html) in the IAM User Guide\.

## List Your Server Certificates<a name="list-your-server-certificates"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [ListServerCertificatesRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TListServerCertificatesRequest.html) object\.

There are no required parameters\. Call the [ListServerCertificates](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceListServerCertificatesListServerCertificatesRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void ListCertificates()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new ListServerCertificatesRequest();
        var response = iamClient.ListServerCertificates(request);
        foreach (KeyValuePair<string, string> kvp in response.ResponseMetadata.Metadata)
        {
            Console.WriteLine("Key = {0}, Value = {1}",
                kvp.Key, kvp.Value);
        }
    }
    catch(Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
```

## Get a Server Certificate<a name="get-a-server-certificate"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [GetServerCertificateRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TGetServerCertificateRequest.html) object, specifying the `ServerCertificateName`\. Call the [GetServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceGetServerCertificateGetServerCertificateRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void GetCertificate()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new GetServerCertificateRequest();
        request.ServerCertificateName = "CERTIFICATE_NAME";
        var response = iamClient.GetServerCertificate(request);
        Console.WriteLine("CertificateName = " + response.ServerCertificate.ServerCertificateMetadata.ServerCertificateName);
        Console.WriteLine("Certificate Arn = " + response.ServerCertificate.ServerCertificateMetadata.Arn);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
```

## Update a Server Certificate<a name="update-a-server-certificate"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create an [UpdateServerCertificateRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TUpdateServerCertificateRequest.html) object, specifying the `ServerCertificateName` and the `NewServerCertificateName`\. Call the [UpdateServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceUpdateServerCertificateUpdateServerCertificateRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void UpdateCertificate()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new UpdateServerCertificateRequest();
        request.ServerCertificateName = "CERTIFICATE_NAME";
        request.NewServerCertificateName = "NEW_Certificate_NAME";
        var response = iamClient.UpdateServerCertificate(request);
        if (response.HttpStatusCode.ToString() == "OK")
            Console.WriteLine("Update succesful");
        else
            Console.WriteLine("HTTpStatusCode returned = " + response.HttpStatusCode.ToString());
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

}
```

## Delete a Server Certificate<a name="delete-a-server-certificate"></a>

Create an [AmazonIdentityManagementServiceClient](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TIAMServiceClient.html) object\. Next, create a [DeleteServerCertificateRequest](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/TDeleteServerCertificateRequest.html) object, specifying the `ServerCertificateName`\. Call the [DeleteServerCertificate](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/IAM/MIAMServiceDeleteServerCertificateDeleteServerCertificateRequest.html) method of the `AmazonIdentityManagementServiceClient` object\.

```
public static void DeleteCertificate()
{
    try
    {
        var iamClient = new AmazonIdentityManagementServiceClient();
        var request = new DeleteServerCertificateRequest();
        request.ServerCertificateName = "CERTIFICATE_NAME";
        var response = iamClient.DeleteServerCertificate(request);
        if (response.HttpStatusCode.ToString() == "OK")
            Console.WriteLine(request.ServerCertificateName + " deleted");
        else
            Console.WriteLine("HTTpStatusCode returned = " + response.HttpStatusCode.ToString());
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
```