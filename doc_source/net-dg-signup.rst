.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.


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

To access AWS, you need an AWS account.

**To sign up for an AWS account**

AWS sends you a confirmation email after the sign-up process is complete. At any time, you can view
your current account activity and manage your account by going to `http://aws.amazon.com <http://aws.amazon.com>`_
and clicking :guilabel:`My Account/Console`.

To use the |sdk-net|, you must have a set of valid AWS credentials, which consist of an access key
and a secret key. These keys are used to sign programmatic web service requests and enable AWS to
verify the request comes from an authorized source. You can obtain a set of account credentials when
you create your account. However, we recommend that you do not use these credentials with |sdk-net|.
Instead, :iam-ug:`create one or more IAM users <Using_SettingUpUser>`, and use those credentials. For
applications that run on |EC2| instances, you can use :iam-ug:`IAM roles <WorkingWithRoles>` to provide
temporary credentials.

The preferred approach for handling credentials is to create a profile for each set of credentials
in the |sdk-store|. You can create and manage profiles with the |TVSlong|, PowerShell cmdlets, or
programmatically with the |sdk-net|. These credentials are encrypted and stored separately from any
project. You then reference the profile by name in your application, and the credentials are
inserted at build time. This approach ensures that your credentials are not unintentionally exposed
with your project on a public site. For more information, see
:tvs-ug:`Setting Up the AWS Toolkit for Visual Studio <tkv_setup>` and :ref:`net-dg-config-creds`.

For more information about managing your credentials, see
:aws-gr:`Best Practices for Managing AWS Access Keys <aws-access-keys-best-practices>`.

