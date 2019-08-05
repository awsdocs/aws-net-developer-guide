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
  for each set of |IAM| user credentials in the |sdk-store|. You can also use a credentials file
  to store profiles that contain credentials. You can then reference a particular profile
  programmatically or in your application's :file:`App.config` or :file:`Web.config` file instead
  of storing the credentials in your project files. To limit the risk of unintentionally exposing
  credentials, the |sdk-store| or credentials file should be stored separately from your project
  files.

* Use |IAM| roles for applications that are running on |EC2| instances.

* Use temporary credentials for applications that are available to users outside your organization.

The following topics describe how to manage credentials for an |sdk-net| application. For a general
discussion of how to securely manage AWS credentials, see 
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.


.. contents:: **Topics**
    :local:
    :depth: 1

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

* You reference the profile by name in your application and the associated credentials are
  incorporated at build time. Your source files never contain the credentials.

* If you include a profile named :code:`default`, the |sdk-net| will use that profile by default.

* The |sdk-store| also provides credentials to the |TWP-ug|_.

|sdk-store| profiles are specific to a particular user on a particular host. They cannot be copied
to other hosts or other users. For this reason, |sdk-store| profiles cannot be used in production
applications. For more information, see :ref:`creds-assign`.

There are several ways to manage the profiles in the |sdk-store|.

* The |TVS| includes a graphical user interface for managing profiles. For more information about
  adding credentials to the |sdk-store| with the graphical user interface, see 
  :tvs-ug:`Specifying Credentials <tkv_setup>` in the |TVS-ug|.

* You can manage your profiles from the command line by using the |TWPlong|. For more information, 
  see :twp-ug:`Using AWS Credentials <specifying-your-aws-credentials>` in the |TWP-ug|.

* You can manage your profiles programmatically using the 
  :sdk-net-api-v2:`Amazon.Util.ProfileManager <TUtilProfileManagerNET45>` class. The following example 
  uses the :sdk-net-api-v2:`RegisterProfile <MUtilProfileManagerRegisterProfileStringStringStringNET45>` 
  method to add a new profile to the |sdk-store|.

  .. code-block:: csharp

      Amazon.Util.ProfileManager.RegisterProfile({profileName}, {accessKey}, {secretKey})

  The :methodname:`RegisterProfile` method is used to register a new profile. Your application
  will normally call this method only once for each profile.


.. _creds-file:

Using a Credentials File
------------------------

You can also store profiles in a credentials file, which can be used by the other AWS SDKs, the
|CLI|, and |TWP|. To reduce the risk of accidentally exposing credentials, the credentials file
should be stored separately from any project files, usually in the user's home folder. Be aware that
the profiles in a credentials files are stored in plaintext.

You use a text editor to manage the profiles in a credentials file. The file is named
:file:`credentials`, and the default location is under your user's home folder. For example, if your
user name is :code:`awsuser`, the credentials file would be
:file:`C:\\users\\awsuser\\.aws\\credentials`.

Each profile has the following format:

.. code-block:: none

    [{profile_name}]
    aws_access_key_id = {accessKey} 
    aws_secret_access_key = {secretKey}

A profile can optionally include a session token. For more information, see `Best Practices for 
Managing AWS Access Keys <aws-access-keys-best-practices.html>`_.

If a federated account information is used to access, the credential file must include the session token. Otherwise the SDK would return invalid session token exception. 
The example profile format with session token:

.. code-block:: none
   
   [{profile_name}]
   aws_access_key_id = {accessKey} 
   aws_secret_access_key = {secretKey}
   aws_session_token = {session token}

.. tip:: If you include a profile named :code:`default`, the |sdk-net| will use that profile by 
   default if it cannot find the specified profile.

You can store profiles in a credentials file in a location you choose, such as
:file:`C:\\aws_service_credentials\\credentials`. You then explicitly specify the file path in the
:code:`profilesLocation` attribute in your project's :file:`App.config` or :file:`Web.config` file.
For more information, see :ref:`net-dg-config-creds-assign-profile`.


.. _creds-assign:

Using Credentials in an Application
-----------------------------------

The |sdk-net| searches for credentials in the following order and uses the first available set for
the current application.

1. Access key and secret key values that are stored in the application's :file:`App.config` or
   :file:`Web.config` file. We strongly recommend using profiles rather than storing literal
   credentials in your project files.

2. If a profile is specified:

   1. The specified profile in the |sdk-store|.

   2. The specified profile in the credentials file.

   If no profile is specified:

   1. A profile named :code:`default` in the |sdk-store|.

   2. A profile named :code:`default` in the credentials file.

3. Credentials stored in the :code:`AWS_ACCESS_KEY_ID` and :code:`AWS_SECRET_KEY` environment
   variables.

4. For applications running on an |EC2| instance, credentials stored in an instance profile.

|sdk-store| profiles are specific to a particular user on a particular host. They cannot be copied
to other hosts or other users. For this reason, |sdk-store| profiles cannot be used in production
applications. If your application is running on an |EC2| instance, you should use an |IAM| role as
described in :ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET <net-dg-roles>`.
Otherwise, you should store your credentials in a credentials file on the server your web
application has access to.

.. _net-dg-config-creds-assign-profile:

Specifying a Profile
~~~~~~~~~~~~~~~~~~~~

Profiles are the preferred way to use credentials in an |sdk-net| application. You don't have to
specify where the profile is stored; you only reference the profile by name. The |sdk-net| retrieves
the corresponding credentials, as described in the previous section.

The recommended way to specify a profile is to define an :code:`<aws>` element in your application's
:file:`App.config` or :file:`Web.config` file. The associated credentials are incorporated into the
application during the build process.

The following example specifies a profile named :code:`development`.

.. code-block:: xml

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws profileName="development"/>
    </configuration>

.. note:: The :code:`<configSections>` element must be the first child of the :code:`<configuration>` element.

Another way to specify a profile is to define an :code:`AWSProfileName` value in the
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
location. If your profiles are stored in a credentials file in another location, specify the
location by adding a :code:`profilesLocation` attribute value to the :code:`<aws>` element. The
following example specifies :file:`C:\aws_service_credentials\credentials` as the credentials file
by using the recommended :code:`<aws>` element.

.. code-block:: xml

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
      </configSections>
      <aws profileName="development" profilesLocation="C:\aws_service_credentials\credentials"/>
    </configuration>


Another way to specify a credentials file is with the :code:`<appSettings>` element.

.. code-block:: xml

    <configuration>
      <appSettings>
        <add key="AWSProfileName" value="development"/>
        <add key="AWSProfilesLocation" value="C:\aws_service_credentials\credentials"/>
      </appSettings>
    </configuration>

Although you can reference a profile programmatically using the
:sdk-net-api-v2:`Amazon.Runtime.StoredProfileAWSCredentials <TRuntimeStoredProfileAWSCredentialsNET45>` 
class, we recommend that you use the :code:`aws` element instead. The following example demonstrates 
how to create an :classname:`AmazonS3Client` object that uses the credentials for a specific profile.

.. code-block:: csharp

    var credentials = new StoredProfileAWSCredentials(profileName);
    var s3Client = new AmazonS3Client(credentials, RegionEndpoint.USWest2);

.. tip:: If you want to use the default profile, omit the :code:`AWSCredentials` object, and the 
   |sdk-net| will automatically use your default credentials to create the client object.


.. _net-dg-config-creds-assign-role:

Specifying Roles or Temporary Credentials
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

For applications that run on |EC2| instances, the most secure way to manage credentials is to use
IAM roles, as described in :ref:`Using IAM Roles for EC2 Instances with the AWS SDK for .NET
<net-dg-roles>`.

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
using the :code:`ProxyCredentials` property on the :sdk-net-api-v2:`ClientConfig
<TRuntimeClientConfigNET45>` class for the service. For example, for |S3|, you could use code
similar to the following, where {my-username} and {my-password} are the proxy user name and password
specified in a `NetworkCredential <http://msdn.microsoft.com/en-us/library/system.net.networkcredential.aspx>`_ 
object.

.. code-block:: csharp

    AmazonS3Config config = new AmazonS3Config();
    config.ProxyCredentials = new NetworkCredential("my-username", "my-password");

Earlier versions of the SDK used :code:`ProxyUsername` and :code:`ProxyPassword`, but these
properties have been deprecated.
