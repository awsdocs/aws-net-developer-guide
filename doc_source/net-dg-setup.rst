.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-setup:

##################################
Getting Started with the |sdk-net|
##################################

To get started with the |sdk-net|, complete the following tasks:


.. contents:: **Tasks**
    :local:
    :depth: 1

.. _net-dg-signup:

Create an AWS Account and Credentials
=====================================

To access AWS, you need an AWS account.

**To sign up for an AWS account**

1. Open http://aws.amazon.com/, and then choose :guilabel:`Create an AWS Account`.

2. Follow the instructions. Part of the sign-up procedure involves receiving a phone call and 
   entering a PIN using the phone keypad.

AWS sends you a confirmation email after the sign-up process is complete. At any time, you can view
your current account activity and manage your account by going to `http://aws.amazon.com <http://aws.amazon.com>`_
and clicking :guilabel:`My Account/Console`.

To use the the SDK, you must have a set of valid AWS credentials, which consist of an access key and
a secret key. These keys are used to sign programmatic web service requests and enable AWS to verify
that the request comes from an authorized source. You can obtain a set of account credentials when
you create your account. However, we recommend that you do not use these credentials with the SDK.
Instead, :iam-ug:`create one or more IAM users <Using_SettingUpUser>`, and use those credentials. For
applications that run on EC2 instances, you can use :iam-ug:`IAM roles <WorkingWithRoles>` to provide
temporary credentials.

The preferred approach for handling credentials is to create a profile for each set of credentials
in the SDK Store. You can create and manage profiles with the |TVSlong|, PowerShell cmdlets, or
programmatically with the the SDK. These credentials are encrypted and stored separately from any
project. You then reference the profile by name in your application, and the credentials are
inserted at build time. This approach ensures that your credentials are not unintentionally exposed
with your project on a public site. For more information, see 
:tvs-ug:`Setting Up the AWS Toolkit for Visual Studio <tkv_setup>` and :ref:`net-dg-config-creds`.

For more information about managing your credentials, see 
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.


.. _net-dg-dev-env:

Install the .NET Development Environment
========================================

To use the the SDK, you must have the following installed.


Requirements
------------

* (Required) Microsoft .NET Framework 3.5 or later

* (Required) Microsoft Visual Studio 2010 or later

* (Required) The the SDK

* (Recommended) |TVSlong|, a plugin that provides a user interface for managing your AWS resources
  from Visual Studio, and includes the the SDK. For more information, see 
  :tvs-ug:`Using the AWS Toolkit for Visual Studio <welcome>`.

.. note:: We recommend using Visual Studio Professional 2010 or higher to implement your applications.

.. _net-dg-install-net-sdk:

Install the |sdk-net|
=====================

The following procedure describes how to install the |TFW|, which contains the |sdk-net|.

**To install the the SDK**

1. Go to |sdk-net|_. Click the :guilabel:`Download` button
   in the upper right corner of the page. Your browser will prompt you to save the install file.

   .. tip:: The |sdk-net| is also available on `GitHub <https://github.com/aws/aws-sdk-net>`_.

2. To begin the install process, open the saved install file and follow the on-screen
   instructions. Version 2 of the the SDK can be found in the :file:`past-releases` folder of the the SDK installation directory.

   .. tip:: By default, the |TFW| is installed in the *Program Files* directory, which requires 
      administrator privileges. To install the |TFW| as a non-administrator, specify a different installation directory.

3. (Optional) You can install extensions for the the SDK, which include a session state provider and a
   trace listener. For more information, see :ref:`net-dg-nuget`.


.. _net-dg-start-new-project:

Start a New Project
===================

If you have installed the |TVS| on Visual Studio Professional, it includes C# project templates for
a variety of AWS services, including the following basic templates:

AWS Console Project
    A console application that makes basic requests to |S3|, |SDB|, and |EC2|.

AWS Empty Project
    A console application that does not include any code.

AWS Web Project
    An ASP.NET application that makes basic requests to |S3|, |SDB|, and |EC2|.

You can also base your application on one of the standard Visual Studio project templates. Just add
a reference to the AWS .NET library (:file:`AWSSDK.dll`), which is located in the
:file:`past-releases` folder of the the SDK installation directory.

The following procedure gets you started by creating and running a new AWS Console project for
Visual Studio 2012; the process is similar for other project types and Visual Studio versions. For
more information on how to configure an AWS application, see :ref:`net-dg-config`.

**To start a new project**

1. In Visual Studio, on the :guilabel:`File` menu, select :guilabel:`New`, and then click
   :guilabel:`Project` to open the :guilabel:`New Project` dialog box.

2. Select :guilabel:`AWS` from the list of installed templates and select the :guilabel:`AWS Console
   Project` project template. Enter a project name, and then click :guilabel:`OK`.

   .. figure:: images/new-proj-dlg-net-dg.png
       :scale: 50

3. Use the :guilabel:`AWS Access Credentials` dialog box to configure your application.

  *  Specify which account profile your code should use to access AWS. To use an existing profile, click
     :guilabel:`Use existing profile` and select the profile from the list. To add a new profile,
     click :guilabel:`Use a new profile` and enter the credentials information. For more
     information about profiles, see :ref:`net-dg-config`.

  * Specify a default AWS region.

  .. figure:: images/creds-new-proj-net-dg.png
      :scale: 50

4. Click :guilabel:`OK` to accept the configuration, which opens the project. Examine the project's
   :file:`App.config` file, which will contain something like the following:

   .. code-block:: xml

       <configuration>
           <appSettings>
               <add key="AWSProfileName" value="development"/>
               <add key="AWSRegion" value="us-west-2"/>
           </appSettings>
       </configuration>

   The |TVS| puts the values you specified in the :guilabel:`AWS Access Credentials` dialog box
   into the two key-value pairs in :code:`appSettings`.

   .. note:: Although using the :code:`appSettings` element is still supported, we recommend that 
      you move to using the :code:`aws` element instead, for example:

      .. code-block:: xml
     
          <configuration>
            <configSections>
              <section name="aws" type="Amazon.AWSSection, AWSSDK"/>
            </configSections>
            <aws region="us-west-2" profileName="development"/>
          </configuration>
     
      For more information on use of the :code:`aws` element, see :ref:`net-dg-config-ref`.

5. Click :kbd:`F5` to compile and run the application, which prints the number of EC2 instances, |SDB|
   tables, and |S3| buckets in your account.

For more information about configuring an AWS application, see :ref:`net-dg-config`.



