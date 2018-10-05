.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-config-creds:

###########################
Configuring AWS Credentials
###########################

You must manage your AWS credentials securely and avoid practices that can unintentionally expose
your credentials to the public. In this topic, we describe how you configure your application's AWS
credentials so that they remain secure.

* Don't use your account's root credentials to access your AWS resources. These credentials provide
  unrestricted account access and are difficult to revoke.

* Don't put literal access keys in your application, including the project's :file:`App.config` or
  :file:`Web.config` file. If you do, you create a risk of accidentally exposing your credentials if,
  for example, you upload the project to a public repository.

.. note::

    We assume you have created an AWS account and have access to your credentials. If you haven't yet, see :ref:`net-dg-signup`.

The following are general guidelines for securely managing credentials:

* Create |IAM| users and use their IAM user credentials instead of using your AWS root
  user. |IAM| user credentials are easier to revoke if they're compromised. You can apply a policy
  to each |IAM| user that restricts the user to a specific  set of resources and actions.

* During application development, the preferred approach for managing credentials is to put a profile
  for each set of |IAM| user credentials in the |sdk-store|. You can also use a plaintext
  credentials file to store profiles that contain credentials. Then you can reference a specific
  profile programmatically instead of storing the credentials in your project files. To limit the
  risk of unintentionally exposing credentials, you should store the |sdk-store| or credentials file
  separately from your project files.

* Use `IAM Roles for Tasks <http://docs.aws.amazon.com/AmazonECS/latest/developerguide/task-iam-roles.html>`_ for |ECSlong| (|ECS|) tasks.

* Use |IAM| roles for applications that are running on |EC2| instances.

* Use temporary credentials or environment variables for applications that are available to users
  outside your organization.

The following topics describe how to manage credentials for an |sdk-net| application. For a discussion
of how to securely manage AWS credentials, see
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.


.. _sdk-store:

Using the |sdk-store|
---------------------

During development of your |sdk-net| application, add a profile to the |sdk-store| for
each set of credentials you want to use in your application. This prevents the accidental
exposure of your AWS credentials. The |sdk-store| is located in the :code:`C:\Users\<username>\AppData\Local\AWSToolkit` folder in the :code:`RegisteredAccounts.json`
file. The |sdk-store| provides the following benefits:

* The |sdk-store| can contain multiple profiles from any number of accounts.

* The credentials in the |sdk-store| are encrypted, and the |sdk-store| resides in the user's home
  directory. This limits the risk of accidentally exposing your credentials.

* You reference the profile by name in your application and the associated credentials are referenced
  at run time. Your source files never contain the credentials.

* If you include a profile named :code:`default`, the |sdk-net| uses that profile. This is also
  true if you don't provide another profile name, or if the specified name isn't found.

* The |sdk-store| also provides credentials to |TWPlong| and the |TVSlong|.

.. note::

    |sdk-store| profiles are specific to a particular user on a particular host. They cannot be copied to other hosts or other users. For this reason, you cannot use |sdk-store| profiles in production applications. For more information, see :ref:`creds-assign`.

You can manage the profiles in the |sdk-store| in several ways.

* Use the graphical user interface (GUI) in the |TVSlong| to manage profiles. For more information about
  adding credentials to the |sdk-store| by using the GUI, see
  :tvs-ug:`Specifying Credentials <tkv_setup.html#tkv_setup.creds>` in the |TVSlong|.

* You can manage your profiles from the command line by using the :code:`Set-AWSCredentials` cmdlet in
  |TWPlong|. For more information, see :twp-ug:`Using AWS Credentials <specifying-your-aws-credentials>`.

* You can create and manage your profiles programmatically by using the
  :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
  class.

The following examples show how to create a basic profile and SAML profile and add them to
the |sdk-store| by using the :sdk-net-api:`RegisterProfile <Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile>`
method.

Create a Profile and Save it to the .NET Credentials File
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileOptions <Runtime/TCredentialProfileOptions>`
    object and set its :code:`AccessKey` and :code:`SecretKey` properties. Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
    object. Provide the name of the profile and the :code:`CredentialProfileOptions` object
    you created. Optionally, set the Region property for the profile. Instantiate a NetSDKCredentialsFile object
    and call the :sdk-net-api:`RegisterProfile <Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile>`
    method to register the profile.

    .. code-block:: csharp

             var options = new CredentialProfileOptions
            {
                AccessKey = "access_key",
                SecretKey = "secret_key"
            };
            var profile = new Amazon.Runtime.CredentialManagement.CredentialProfile("basic_profile", options);
            profile.Region = RegionEndpoint.USWest1;
            var netSDKFile = new NetSDKCredentialsFile();
            netSDKFile.RegisterProfile(profile);

    The :methodname:`RegisterProfile` method is used to register a new profile. Your application
    typically calls this method only once for each profile.

Create a SAMLEndpoint and an Associated Profile and Save it to the .NET Credentials File
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.SAMLEndpoint <Runtime/TSAMLEndpoint>`
    object. Provide the name and endpoint URI parameters. Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.SAMLEndpointManager <Runtime/TSAMLEndpointManager>`
    object.  Call the :sdk-net-api:`RegisterEndpoint <Runtime/MSAMLEndpointManagerRegisterEndpointSAMLEndpoint>`
    method to register the endpoint. Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileOptions <Runtime/TCredentialProfileOptions>`
    object and set its :code:`EndpointName` and :code:`RoleArn` properties. Create an
    :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
    object and provide the name of the profile and the :code:`CredentialProfileOptions` object you created.
    Optionally, set the Region property for the profile. Instantiate a NetSDKCredentialsFile object
    and call the :sdk-net-api:`RegisterProfile <Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile>`
    method to register the profile.

    .. code-block:: csharp

            var endpoint = new SAMLEndpoint("endpoint1", new Uri("https://some_saml_endpoint"), SAMLAuthenticationType.Kerberos);
            var endpointManager = new SAMLEndpointManager();
            endpointManager.RegisterEndpoint(endpoint);
            options = new CredentialProfileOptions
            {
                EndpointName = "endpoint1",
                RoleArn = "arn:aws:iam::999999999999:role/some-role"
            };
            profile = new CredentialProfile("federated_profile", options);
            netSDKFile = new NetSDKCredentialsFile();
            netSDKFile.RegisterProfile(profile);

.. _creds-file:

Using a Credentials File
------------------------

You can also store profiles in a shared credentials file. This file can be used by the other AWS SDKs, the
|CLI| and |TWPLong|. To reduce the risk of accidentally exposing credentials, store the credentials file
separately from any project files, usually in the user's home folder. *Be aware
that the profiles in credentials files are stored in plaintext.*

Use can manage the profiles in the shared credentials file in two ways:

* You can use a text editor. The file is named
  :file:`credentials`, and the default location is under your user's home folder. For example, if your
  user name is :code:`awsuser`, the credentials file would be
  :file:`C:\\users\\awsuser\\.aws\\credentials`.

  The following is an example of a profile in the credentials file.

 .. code-block:: none

     [{profile_name}]
     aws_access_key_id = {accessKey}
     aws_secret_access_key = {secretKey}

   For more information, see
  `Best Practices for Managing AWS Access Keys <http://docs.aws.amazon.com/general/latest/gr/aws-access-keys-best-practices.html>`_.

 .. tip:: If you include a profile named :code:`default`, the |sdk-net| uses that profile by default if it can't find the specified profile.

  You can store the credentials file that contains the profiles in a location you choose, such as
  :file:`C:\\aws_service_credentials\\credentials`. You then explicitly specify the file path in the
  :code:`AWSProfilesLocation` attribute in your project's :file:`App.config` or :file:`Web.config`
  file. For more information, see :ref:`net-dg-config-creds-assign-profile`.

* You can programmatically manage the credentials file by using the classes in the :sdk-net-api:`Amazon.Runtime.CredentialManagement <Runtime/NRuntimeCredentialManagement>` namespace.

Create a Profile and Save it to the Shared Credentials File
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

      Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileOptions <Runtime/TCredentialProfileOptions>`
      object and set its :code:`AccessKey` and :code:`SecretKey` properties.
      Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
      object. Provide the name of the profile and the :code:`CredentialProfileOptions` you created.
      Optionally, set the Region property for the profile. Instantiate an
      :sdk-net-api:`Amazon.Runtime.CredentialManagement.SharedCredentialsFile <Runtime/TSharedCredentialsFile>`
      object and call the :sdk-net-api:`RegisterProfile <Runtime/MSharedCredentialsFileRegisterProfileCredentialProfile>`
      method to register the profile.

      .. code-block:: csharp

        options = new CredentialProfileOptions
        {
            AccessKey = "access_key",
            SecretKey = "secret_key"
        };
        profile = new CredentialProfile("shared_profile", options);
        profile.Region = RegionEndpoint.USWest1;
        var sharedFile = new SharedCredentialsFile();
        sharedFile.RegisterProfile(profile);

      The :methodname:`RegisterProfile` method is used to register a new profile. Your application
      will normally call this method only once for each profile.

Create a Source Profile and an Associated Assume Role Profile and Save It to the Credentials File
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

      Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileOptions <Runtime/TCredentialProfileOptions>`
      object for the source profile and set its :code:`AccessKey` and :code:`SecretKey` properties.
      Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
      object. Provide the name of the profile and the :code:`CredentialProfileOptions`
      you created. Instantiate an :sdk-net-api:`Amazon.Runtime.CredentialManagement.SharedCredentialsFile <Runtime/TSharedCredentialsFile>`
      object and call the :sdk-net-api:`RegisterProfile <Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile>`
      method to register the profile. Create another :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileOptions <Runtime/TCredentialProfileOptions>`
      object for the assumed role profile and set the :code:`SourceProfile` and :code:`RoleArn` properties
      for the profile. Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
      object for the assumed role. Provide the name of the profile and the :code:`CredentialProfileOptions`
      you created.

      .. code-block:: csharp

        // Create the source profile and save it to the shared credentials file
        var sourceProfileOptions = new CredentialProfileOptions
        {
            AccessKey = "access_key",
            SecretKey = "secret_key"
        };
        var sourceProfile = new CredentialProfile("source_profile", sourceProfileOptions);
        sharedFile = new SharedCredentialsFile();
        sharedFile.RegisterProfile(sourceProfile);

        // Create the assume role profile and save it to the shared credentials file
        var assumeRoleProfileOptions = new CredentialProfileOptions
        {
            SourceProfile = "source_profile",
            RoleArn = "arn:aws:iam::999999999999:role/some-role"
        };
        var assumeRoleProfile = new CredentialProfile("assume_role_profile", assumeRoleProfileOptions);
        sharedFile.RegisterProfile(assumeRoleProfile);

Update an Existing Profile in the Shared Credentials File
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

      Create an :sdk-net-api:`Amazon.Runtime.CredentialManagement.SharedCredentialsFile <Runtime/TSharedCredentialsFile>`
      object. Set the :code:`Region`, :code:`AccessKey` and :code:`SecretKey` properties for the profile.
      Call the :sdk-net-api:`TryGetProfile <Runtime/MSharedCredentialsFileTryGetProfileStringCredentialProfile>`
      method. If the profile exists, use an
      :sdk-net-api:`Amazon.Runtime.CredentialManagement.SharedCredentialsFile <Runtime/TSharedCredentialsFile>`
      instance to call the :sdk-net-api:`RegisterProfile <Runtime/MNetSDKCredentialsFileRegisterProfileCredentialProfile>`
      method to register the updated profile.

      .. code-block:: csharp

            sharedFile = new SharedCredentialsFile();
            CredentialProfile basicProfile;
            if (sharedFile.TryGetProfile("basicProfile", out basicProfile))
            {
                basicProfile.Region = RegionEndpoint.USEast1;
                basicProfile.Options.AccessKey = "different_access_key";
                basicProfile.Options.SecretKey = "different_secret_key";

                sharedFile.RegisterProfile(basicProfile);
            }

.. _creds-locate:

Accessing Credentials and Profiles in an Application
----------------------------------------------------

You can easily locate credentials and profiles in the .NET credentials file or in the shared credentials file by using the
:sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain <Runtime/TCredentialProfileStoreChain>`
class. This is the way the .NET SDK looks for credentials and profiles.  The :code:`CredentialProfileStoreChain`
class automatically checks in both credentials files.

You can get credentials or profiles by using the
:sdk-net-api:`TryGetAWSCredentials <Runtime/MCredentialProfileStoreChainTryGetAWSCredentialsStringAWSCredentials>`
or :sdk-net-api:`TryGetProfile <Runtime/MCredentialProfileStoreChainTryGetProfileStringCredentialProfile>`
methods.  The :code:`ProfilesLocation` property determines the behavior of the
:code:`CredentialsProfileChain`, as follows:

#. If ProfilesLocation is non-null and non-empty, search the shared credentials file at the disk path
   in the :code:`ProfilesLocation` property.

#. If :code:`ProfilesLocation` is null or empty and the platform supports the .NET credentials file, search
   the .NET credentials file. If the profile is not found, search the shared credentials file in the
   default location.

#. If :code:`ProfilesLocation` is null or empty and the platform doesn’t support the .NET credentials
   file, search the shared credentials file in the default location.

Get Credentials from the SDK Credentials File or the Shared Credentials File in the Default Location.
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

  Create a :code:`CredentialProfileStoreChain` object and an :sdk-net-api:`Amazon.Runtime.AWSCredentials <Runtime/TAWSCredentials>`
  object. Call the :code:`TryGetAWSCredentials` method. Provide the profile name and the :code:`AWSCredentials`
  object in which to return the credentials.

  .. code-block:: csharp

            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials("basic_profile", out awsCredentials))
            {
                // use awsCredentials
            }

Get a Profile from the SDK Credentials File or the Shared Credentials File in the Default Location
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Create a :code:`CredentialProfileStoreChain` object and an :sdk-net-api:`Amazon.Runtime.CredentialManagement.CredentialProfile <Runtime/TCredentialProfile>`
object. Call the :code:`TryGetProfile` method and  provide the profile name and :code:`CredentialProfile`
object in which to return the credentials.

.. code-block:: csharp

            var chain = new CredentialProfileStoreChain();
            CredentialProfile basicProfile;
            if (chain.TryGetProfile("basic_profile", out basicProfile))
            {
                // Use basicProfile
            }

Get AWSCredentials from a File in the Shared Credentials File Format at a File Location
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Create a :code:`CredentialProfileStoreChain` object and provide the path to the credentials file. Create an
:code:`AWSCredentials` object. Call the :code:`TryGetAWSCredentials` method. Provide the profile name and the
:code:`AWSCredentials` object in which to return the credentials.

.. code-block:: csharp

            var chain = new
                CredentialProfileStoreChain("c:\\Users\\sdkuser\\customCredentialsFile.ini");
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials("basic_profile", out awsCredentials))
            {
                // Use awsCredentials
            }

How to Create an AmazonS3Client Using the SharedCredentialsFile Class
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You can create an :sdk-net-api:`AmazonS3Client <S3/TS3Client>`
object that uses the credentials for a specific profile by using the
:sdk-net-api:`Amazon.Runtime.CredentialManagement.SharedCredentialsFile <Runtime/TSharedCredentialsFile>`
class. The |sdk-net| loads the credentials contained in the profile automatically. You might do this
if you want to use a specific profile for a given client that is different from the :code:`profile`
you specify in :code:`App.Config`.

.. code-block:: csharp

        CredentialProfile basicProfile;
        AWSCredentials awsCredentials;
        var sharedFile = new SharedCredentialsFile();
        if (sharedFile.TryGetProfile("basic_profile", out basicProfile) &&
            AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, sharedFile, out awsCredentials))
        {
            using (var client = new AmazonS3Client(awsCredentials, basicProfile.Region))
            {
                var response = client.ListBuckets();
            }
        }

If you want to use the default profile, and have the |sdk-net| automatically use your default
credentials to create the client object use the following code.

.. code-block:: csharp

        using (var client = new AmazonS3Client(RegionEndpoint.US-West2))
        {
            var response = client.ListBuckets();
        }

.. _creds-assign:

Credential and Profile Resolution
---------------------------------

The |sdk-net| searches for credentials in the following order and uses the first available set for
the current application.

1. The client configuration, or what is explicitly set on the AWS service client.

2. :code:`BasicAWSCredentials` that are created from the :code:`AWSAccessKey` and :code:`AWSSecretKey`
   :code:`AppConfig` values, if they're available.

3. A credentials profile with the name specified by a value in 
   :code:`AWSConfigs.AWSProfileName` (set explicitly or in :code:`AppConfig`). 
   
4. The :code:`default` credentials profile. 

5. :code:`SessionAWSCredentials` that are created from the :code:`AWS_ACCESS_KEY_ID`, :code:`AWS_SECRET_ACCESS_KEY`,
   and :code:`AWS_SESSION_TOKEN` environment variables, if they're all non-empty.

6. :code:`BasicAWSCredentials` that are created from the :code:`AWS_ACCESS_KEY_ID` and :code:`AWS_SECRET_ACCESS_KEY`
   environment variables, if they're both non-empty.

7. IAM Roles for Tasks for Amazon EC2 Container Service (Amazon ECS) tasks.

8. EC2 instance metadata.

|sdk-store| profiles are specific to a particular user on a particular host. You can't copy them
to other hosts or other users. For this reason, you can't reuse |sdk-store| profiles that are on
your development machine on other hosts or developer machines. If your application is running on an |EC2|
instance, use an |IAM| role as described in :ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET <net-dg-roles>`.
Otherwise, store your credentials in a credentials file that your web application has access to on the server.

.. _net-dg-config-creds-profile-resolution:

Profile Resolution
~~~~~~~~~~~~~~~~~~

With two different credentials file types, it's important to understand how to configure the |sdk-net| and
|TWPLong| to use them.  The :code:`AWSConfigs.AWSProfilesLocation` (set explicitly or in :code:`AppConfig`)
controls how the |sdk-net| finds credential profiles. The :code:`-ProfileLocation` command line argument
controls how |TWPLong| finds a profile.  Here's how the configuration works in both cases.

.. list-table::
   :widths: 1 2
   :header-rows: 1

   * - Profile Location Value
     - Profile Resolution Behavior

   * - null (not set) or empty
     - First search the .NET credentials file for a profile with the specified name.  If the profile
       isn't there, search :code:`%HOME%\.aws\credentials`.  If the profile isn't there, search
       :code:`%HOME%\.aws\config`.

   * - The path to a file in the shared credentials file format
     - Search *only* the specified file for a profile with the specified name.

.. _net-dg-config-creds-assign-profile:

Specifying a Profile
~~~~~~~~~~~~~~~~~~~~

Profiles are the preferred way to use credentials in an |sdk-net| application. You don't have to
specify where the profile is stored. You only reference the profile by name. The |sdk-net| retrieves
the corresponding credentials, as described in the previous section.

The preferred way to specify a profile is to define an :code:`AWSProfileName` value in the
:code:`appSettings` section of your application's :file:`App.config` or :file:`Web.config` file. The
associated credentials are incorporated into the application during the build process.

The following example specifies a profile named :code:`development`.

.. code-block:: xml

    <configuration>
      <appSettings>
        <add key="AWSProfileName" value="development"/>
      </appSettings>
    </configuration>

This example assumes the profile exists in the |sdk-store| or in a credentials file in the default
location.

If your profiles are stored in a credentials file in another location, specify the location by
adding a :code:`AWSProfilesLocation` attribute value in the :code:`<appSettings>` element. The
following example specifies :file:`C:\\aws_service_credentials\\credentials` as the credentials file.

.. code-block:: xml

    <configuration>
      <appSettings>
        <add key="AWSProfileName" value="development"/>
        <add key="AWSProfilesLocation" value="C:\aws_service_credentials\credentials"/>
      </appSettings>
    </configuration>

The deprecated alternative way to specify a profile is shown below for completeness, but we do not
recommend it.

.. code-block:: xml

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
    </configuration>

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection,AWSSDK.Core"/>
      </configSections>
      <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
    </configuration>

.. _net-dg-config-creds-saml:

Using Federated User Account Credentials
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Applications that use the |sdk-net| (:file:`AWSSDK.Core` version 3.1.6.0 and later) can use
federated user accounts through Active Directory Federation Services (AD FS) to access AWS web services
by using Security Assertion Markup Language (SAML).

Federated access support means users can authenticate using your Active Directory. Temporary
credentials are granted to the user automatically. These temporary credentials, which are valid
for one hour, are used when your application invokes AWS web services. The SDK handles management of the
temporary credentials. For domain-joined user accounts, if your application makes a call but the
credentials have expired, the user is reauthenticated automatically and fresh credentials are
granted. (For non-domain-joined accounts, the user is prompted to enter credentials before
reauthentication.)

To use this support in your .NET application, you must first set up the role profile by using a
PowerShell cmdlet. To learn how, see the
:twp-ug:`AWS Tools for Windows PowerShell documentation <saml-pst>`.

After you setup the role profile, reference the profile in your application's
app.config/web.config file with the :code:`AWSProfileName` key in the same way you would with
other credential profiles.

The SDK Security Token Service assembly (:file:`AWSSDK.SecurityToken.dll`), which is loaded at
runtime, provides the SAML support to obtain AWS credentials. Be sure this assembly is available
to your application at run time.


.. _net-dg-config-creds-assign-role:

Specifying Roles or Temporary Credentials
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

For applications that run on |EC2| instances, the most secure way to manage credentials is to use
IAM roles, as described in
:ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET <net-dg-roles>`.

For application scenarios in which the software executable is available to users outside your
organization, we recommend you design the software to use *temporary security credentials*. In
addition to providing restricted access to AWS resources, these credentials have the benefit of
expiring after a specified period of time. For more information about temporary security
credentials, see the following:

* :iam-ug:`Using Security Tokens to Grant Temporary Access to Your AWS Resources <TokenBasedAuth>`

* :aws-articles:`Authenticating Users of AWS Mobile Applications with a Token Vending Machine <4611615499399490>`.

Although the title of the second article refers specifically to mobile applications, the article
contains information that is useful for any AWS application deployed outside of your organization.


.. _net-dg-config-creds-proxy:

Using Proxy Credentials
~~~~~~~~~~~~~~~~~~~~~~~

If your software communicates with AWS through a proxy, you can specify credentials for the proxy by
using the :code:`ProxyCredentials` property on the
:sdk-net-api:`AmazonS3Config <S3/TS3Config>`
class for the service. For example, for |S3| you could use code
similar to the following, where {my-username} and {my-password} are the proxy user name and password
specified in a `NetworkCredential <https://msdn.microsoft.com/en-us/library/system.net.networkcredential.aspx>`_
object.

.. code-block:: csharp

    AmazonS3Config config = new AmazonS3Config();
    config.ProxyCredentials = new NetworkCredential("my-username", "my-password");

Earlier versions of the SDK used :code:`ProxyUsername` and :code:`ProxyPassword`, but these
properties are deprecated.
