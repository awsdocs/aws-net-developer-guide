.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

This topic describes how to configure your application's AWS credentials. It assumes you have
created an AWS account and have access to your credentials, as described in :ref:`net-dg-signup`. It
is important to manage your credentials securely and avoid practices that could unintentionally
expose your credentials publicly. In particular:

* Don't use your account's root credentials to access your AWS resources. These credentials provide
  unrestricted account access and are difficult to revoke.

* Don't put literal access keys in your application, including the project's :file:`App.config` or
  :file:`Web.config` file. Doing so creates a risk of accidentally exposing your credentials if,
  for example, you upload the project to a public repository.

Some general guidelines for securely managing credentials include:

* Create |IAM| users and use the credentials for the |IAM| users instead of your account's root
  credentials to provide account access. |IAM| user credentials are easier to revoke if they are
  compromised. You can apply to each |IAM| user a policy that restricts the user to a specified
  set of resources and actions.

* The preferred approach for managing credentials during application development is to put a profile
  for each set of |IAM| user credentials in the |sdk-store|. You can also use a plaintext
  credentials file to store profiles that contain credentials. You can then reference a particular
  profile programmatically or in your application's :file:`App.config` or :file:`Web.config` file
  instead of storing the credentials in your project files. To limit the risk of unintentionally
  exposing credentials, the |sdk-store| or credentials file should be stored separately from your
  project files.

* Use |IAM| roles for applications that are running on |EC2| instances.

* Use temporary credentials or environment variables for applications that are available to users
  outside your organization.

The following topics describe how to manage credentials for an |sdk-net| application. For a general
discussion of how to securely manage AWS credentials, see 
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.


.. _sdk-store:

Using the |sdk-store|
---------------------

During development of your |sdk-net| application, you should add a profile to the |sdk-store| for
each set of credentials you want to use in your application. This will prevent the accidental
exposure of your AWS credentials while developing your application. The |sdk-store| provides the
following benefits:

* The |sdk-store| can contain multiple profiles from any number of accounts.

* The credentials in the |sdk-store| are encrypted, and the |sdk-store| resides in the user's home
  directory, which limits the risk of accidentally exposing your credentials.

* You reference the profile by name in your application and the associated credentials are referenced
  at run time. Your source files never contain the credentials.

* If you include a profile named :code:`default`, the |sdk-net| will use that profile. This is also
  true if no other profile name is supplied, or the given name is not found

* The |sdk-store| also provides credentials to the |TWPlong| and the |TVSlong|.

.. note:: |sdk-store| profiles are specific to a particular user on a particular host. They cannot 
   be copied to other hosts or other users. For this reason, |sdk-store| profiles cannot be used in
   production applications. For more information, see :ref:`creds-assign`.

There are several ways to manage the profiles in the |sdk-store|.

* The |TVS| includes a graphical user interface for managing profiles. For more information about
  adding credentials to the |sdk-store| with the graphical user interface, see 
  :tvs-ug:`Specifying Credentials <tkv_setup.html#tkv_setup.creds>` in the |TVSlong|.

* You can manage your profiles from the command line by using the :code:`Set-AWSCredentials` cmdlet in
  the |TWPlong|. For more information, see 
  :twp-ug:`Using AWS Credentials <specifying-your-aws-credentials>` in the |TWPlong|.

* While it is not a commonly used feature, you can manage your profiles programmatically using the
  :sdk-net-api:`Amazon.Util.ProfileManager <Util/TUtilProfileManager>` class. The following 
  example uses the 
  :sdk-net-api:`RegisterProfile <Util/MUtilProfileManagerRegisterProfileStringStringString>` method 
  :sdk-net-api:`RegisterProfile <Util/MUtilProfileManagerRegisterProfileStringStringString>` method 
  to add a new profile to the |sdk-store|.

  .. code-block:: csharp

      Amazon.Util.ProfileManager.RegisterProfile({profileName}, {accessKey}, {secretKey})

  The :methodname:`RegisterProfile` method is used to register a new profile. Your application
  will normally call this method only once for each profile.


.. _creds-file:

Using a Credentials File
------------------------

You can also store profiles in a credentials file, which can be used by the other AWS SDKs, the
|CLI|, and |TWPLong|. To reduce the risk of accidentally exposing credentials, the credentials file
should be stored separately from any project files, usually in the user's home folder. *Be aware
that the profiles in a credentials files are stored in plaintext.*

You use a text editor to manage the profiles in a credentials file. The file is named
:file:`credentials`, and the default location is under your user's home folder. For example, if your
user name is :code:`awsuser`, the credentials file would be
:file:`C:\users\awsuser\.aws\credentials`.

Each profile has the following format:

.. code-block:: none

   [{profile_name}] 
   aws_access_key_id = {accessKey} 
   aws_secret_access_key = {secretKey}

A profile can optionally include a session token. For more information, see 
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`. 
Profiles in the SDK Store do not accept session tokens. The |sdk-store| is for "long lived" 
credentials.

.. tip:: If you include a profile named :code:`default`, the |sdk-net| will use that profile by 
   default if it cannot find the specified profile.

You can store profiles in a credentials file in a location you choose, such as
:file:`C:\\aws_service_credentials\\credentials`. You then explicitly specify the file path in the
:code:`AWSProfilesLocation` attribute in your project's :file:`App.config` or :file:`Web.config`
file. For more information, see :ref:`net-dg-config-creds-assign-profile`.


.. _creds-assign:

Using Credentials in an Application
-----------------------------------

The |sdk-net| searches for credentials in the following order and uses the first available set for
the current application.

1. Access key and secret key values that are passed to the service client constructors in your source
   code, or stored in the application's :file:`App.config` or :file:`Web.config` file. We *strongly
   recommend* using profiles rather than storing literal credentials in your project files.

2. If a profile is specified:

  a. The specified profile in the |sdk-store|.

  b. The specified profile in the credentials file.

  If no profile is specified:

  a. A profile named :code:`default` in the |sdk-store|.

  b. A profile named :code:`default` in the credentials file.

3. Credentials stored in the :code:`AWS_ACCESS_KEY_ID` and :code:`AWS_SECRET_ACCESS_KEY` environment
   variables.

4. For applications running on an |EC2| instance, credentials stored in an instance profile.

|sdk-store| profiles are specific to a particular user on a particular host. They cannot be copied
to other hosts or other users. For this reason, |sdk-store| profiles on your development machine
cannot be re-used on other hosts or developer machines. If your application is running on an |EC2|
instance, you should use an |IAM| role as described in 
:ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET <net-dg-roles>`. 
Otherwise, you should store your credentials in a credentials file that your web application has 
access to on the server.

.. _net-dg-config-creds-assign-profile:

Specifying a Profile
~~~~~~~~~~~~~~~~~~~~

Profiles are the preferred way to use credentials in an |sdk-net| application. You don't have to
specify where the profile is stored; you only reference the profile by name. The |sdk-net| retrieves
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

This example assumes the profile exists in the |sdk-store| or a credentials file in the default
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

The deprecated alternative way to specify a profile is shown below for completeness, but is not
recommended.

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

You can also reference a profile programmatically using the
:sdk-net-api:`Amazon.Runtime.StoredProfileAWSCredentials <Runtime/TRuntimeStoredProfileAWSCredentials>` 
class. The following example demonstrates how to create an :classname:`AmazonS3Client` object that 
uses the credentials for a specific profile. The SDK will load the credentials contained in the profile
automatically. You might do this if you want to use a specific profile for a given client that is
different from the "global" profile you specify in App.Config.

.. code-block:: csharp

    var credentials = new StoredProfileAWSCredentials(profileName);
    var s3Client = new AmazonS3Client(credentials, RegionEndpoint.USWest2);

.. tip:: If you want to use the default profile, omit the :code:`AWSCredentials` object, and the |sdk-net|
   will automatically use your default credentials to create the client object.


.. _net-dg-config-creds-saml:

Using Federated User Account Credentials
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Applications that use the |sdk-net| (:file:`AWSSDK.Core` version 3.1.6.0 and higher) can use
federated user accounts through Active Directory Federation Services (AD FS) to access AWS services
by using Security Assertion Markup Language (SAML).

Federated access support means users can authenticate using your Active Directory; temporary
credentials will be granted to the user automatically. These temporary credentials, which are valid
for one hour, are used when your application invokes AWS services. The SDK handles management of the
temporary credentials. For domain-joined user accounts, if your application makes a call but the
credentials have expired, the user is re-authenticated automatically and fresh credentials are
granted. (For non-domain-joined accounts, the user is prompted to enter credentials prior to
re-authentication.)

To use this support in your .NET application, you must first set up the role profile using a
PowerShell cmdlet. Use PowerShell cmdlets to set up the role profile as described in the 
:twp-ug:`Tools for Windows PowerShell documentation <saml-pst>`.

After you setup the role profile, simply reference the profile in your application's
app.config/web.config file with the AWSProfileName appsetting key in the same way you would with
other credential profiles.

The SDK Security Token Service assembly (:file:`AWSSDK.SecurityToken.dll`), which is loaded at
runtime, provides the SAML support to obtain AWS credentials, so be sure this assembly is available
to your application at runtime.


.. _net-dg-config-creds-assign-role:

Specifying Roles or Temporary Credentials
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

For applications that run on |EC2| instances, the most secure way to manage credentials is to use
IAM roles, as described in 
:ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET <net-dg-roles>`.

For application scenarios in which the software executable will be available to users outside your
organization, we recommend you design the software to use *temporary security credentials*. In
addition to providing restricted access to AWS resources, these credentials have the benefit of
expiring after a specified period of time. For more information about temporary security
credentials, go to:

* :iam-ug:`Using Security Tokens to Grant Temporary Access to Your AWS Resources <TokenBasedAuth>`

* :aws-articles:`Authenticating Users of AWS Mobile Applications with a Token Vending Machine <4611615499399490>`.

Although the title of the second article refers specifically to mobile applications, the article
contains information that is useful for any AWS application deployed outside of your organization.


.. _net-dg-config-creds-proxy:

Using Proxy Credentials
~~~~~~~~~~~~~~~~~~~~~~~

If your software communicates with AWS through a proxy, you can specify credentials for the proxy
using the :code:`ProxyCredentials` property on the 
:sdk-net-api:`ClientConfig <TRuntimeClientConfig>` 
class for the service. For example, for |S3|, you could use code
similar to the following, where {my-username} and {my-password} are the proxy user name and password
specified in a `NetworkCredential <http://msdn.microsoft.com/en-us/library/system.net.networkcredential.aspx>`_ 
object.

.. code-block:: csharp

    AmazonS3Config config = new AmazonS3Config();
    config.ProxyCredentials = new NetworkCredential("my-username", "my-password");

Earlier versions of the SDK used :code:`ProxyUsername` and :code:`ProxyPassword`, but these
properties have been deprecated.
