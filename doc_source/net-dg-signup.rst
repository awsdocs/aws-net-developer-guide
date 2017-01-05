.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.


   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-signup:

#####################################
Create an AWS Account and Credentials
#####################################

To use the |sdk-net| to access AWS, you need an AWS account and AWS credentials. To increase the
security of your AWS account, we recommend that you use an *IAM user* to provide access credentials
instead of using your root account credentials.

.. tip:: For an overview of IAM users and why they are important for the security of your account,
         see :iam-ug:`Overview of Identity Management: Users <introduction_identity-management>`
         in the |IAM-ug|.

Signing Up for an AWS Account
=============================

.. topic:: To sign up for an AWS account

    #. Open http://aws.amazon.com/ and click :guilabel:`Sign Up`.

    #. Follow the on-screen instructions. Part of the sign-up procedure involves receiving a phone
       call and entering a PIN using your phone keypad.

Next, create an IAM user and download (or copy) its secret access key. To use the |sdk-net|,
you must have a set of valid AWS credentials, which consist of an access key
and a secret key. These keys are used to sign programmatic web service requests and enable AWS to
verify that the request comes from an authorized source. You can obtain a set of account credentials when
you create your account. However, we recommend that you do not use these credentials with |sdk-net|.
Instead, :iam-ug:`create one or more IAM users <Using_SettingUpUser>`, and use those credentials. For
applications that run on |EC2| instances, you can use :iam-ug:`IAM roles <WorkingWithRoles>` to provide
temporary credentials.

Creating an IAM User
====================

.. topic:: To create an IAM user

    #.  Go to the :console:`IAM console <iam>` (you may need to sign in to AWS first).

    #.  Click :guilabel:`Users` in the sidebar to view your IAM users.

    #.  If you don't have any IAM users set up, click :guilabel:`Create New Users` to create one.

    #.  Select the IAM user in the list that you'll use to access AWS.

    #.  Open the :guilabel:`Security Credentials` tab, and click :guilabel:`Create Access Key`.

        .. note:: You can have a maximum of two active access keys for any given IAM user. If your
           IAM user has two access keys already, then you'll need to delete one of them before
           creating a new key.

    #.  In the resulting dialog box, choose :guilabel:`Download Credentials` to download the
        credential file to your computer, or click :guilabel:`Show User Security Credentials` to
        view the IAM user's access key ID and secret access key (which you can copy and paste).

        .. important:: There is no way to obtain the secret access key once you close the dialog.
           You can, however, delete its associated access key ID and create a new one.

Next, you :doc:`set your credentials <setup-credentials>` in the AWS shared credentials file or in
the environment.

The preferred approach for handling credentials is to create a profile for each set of credentials
in the |sdk-store|. You can create and manage profiles with the |TVSlong|, PowerShell cmdlets, or
programmatically with the |sdk-net|. These credentials are encrypted and stored separately from any
project. You then reference the profile by name in your application, and the credentials are
inserted at build time. This approach ensures that your credentials are not unintentionally exposed
with your project on a public site. For more information, see
:tvs-ug:`Setting Up the AWS Toolkit for Visual Studio <tkv_setup>` and :ref:`net-dg-config-creds`.

For more information about managing your credentials, see
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.

To view your current account activity and manage your account at any time, go to
`http://aws.amazon.com <http://aws.amazon.com>`_ and choose :guilabel:`My Account/Console`.

