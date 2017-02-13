.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _iam-apis-server-certificates:


######################################
Working with |IAM| Server Certificates
######################################

.. meta::
   :description: Use this .NET code example to learn how to manage IAM server certificates for HTPPS connections.
   :keywords: AWS SDK for .NET examples, IAM server certificates


These .NET examples show you how to:

* List server certificates
* Get server certificates
* Update server certificates
* Delete server certificates

The Scenario
============

In these, examples, you'll basic tasks for managing server certificates for HTTPS
connections. To enable HTTPS connections to your website or application on AWS, you need an SSL/TLS server
certificate. To use a certificate that you obtained from an external provider with your website or application on AWS,
you must upload the certificate to |IAM| or import it into AWS Certificate Manager.

These examples use the |sdk-net| to send and receive messages by using these methods of the
:sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` class:

* :sdk-net-api:`ListServerCertificates <IAM/MIAMIAMServiceListServerCertificatesListServerCertificatesRequest>`
* :sdk-net-api:`GetServerCertificate <IAM/MIAMIAMServiceGetServerCertificateGetServerCertificateRequest>`
* :sdk-net-api:`UpdateServerCertificate <IAM/MIAMIAMServiceUpdateServerCertificateUpdateServerCertificateRequest>`
* :sdk-net-api:`DeleteServerCertificate <IAM/MIAMIAMServiceDeleteServerCertificateDeleteServerCertificateRequest>`

For more information about server certificates, see
:iam-ug:`Working with Server Certificates <id_credentials_server-certs>`
in the |IAM-ug|.

List Your Server Certificates
=============================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object.
Next, create a :sdk-net-api:`ListServerCertificatesRequest <IAM/TIAMListServerCertificatesRequest>` object.

There are no required parameters. Call the :sdk-net-api:`ListServerCertificates <IAM/MIAMIAMServiceListServerCertificatesListServerCertificatesRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

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


Get a Server Certificate
========================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>`
object. Next, create a :sdk-net-api:`GetServerCertificateRequest <IAM/TIAMGetServerCertificateRequest>`
object, specifying the :code:`ServerCertificateName`. Call the :sdk-net-api:`GetServerCertificate <IAM/MIAMIAMServiceGetServerCertificateGetServerCertificateRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

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


Update a Server Certificate
===========================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>`
object. Next, create an :sdk-net-api:`UpdateServerCertificateRequest <IAM/TIAMUpdateServerCertificateRequest>`
object, specifying the :code:`ServerCertificateName` and the :code:`NewServerCertificateName`. Call the
:sdk-net-api:`UpdateServerCertificate <IAM/MIAMIAMServiceUpdateServerCertificateUpdateServerCertificateRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

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



Delete a Server Certificate
=============================

Create an :sdk-net-api:`AmazonIdentityManagementServiceClient <IAM/TIAMIAMServiceClient>` object.
Next, create a :sdk-net-api:`DeleteServerCertificateRequest <IAM/TIAMDeleteServerCertificateRequest>`
object, specifying the :code:`ServerCertificateName`. Call the :sdk-net-api:`DeleteServerCertificate <IAM/MIAMIAMServiceDeleteServerCertificateDeleteServerCertificateRequest>`
method of the :code:`AmazonIdentityManagementServiceClient` object.

.. code-block:: c#

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
