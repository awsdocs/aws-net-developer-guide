.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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

To use the |sdk-net| to access AWS services, you need an AWS account and AWS credentials. To increase the
security of your AWS account, we recommend that you use an *IAM user* to provide access credentials
instead of using your root account credentials.

* To create an AWS account, see `How do I create and activate a new Amazon Web Services account? <https://aws.amazon.com/premiumsupport/knowledge-center/create-and-activate-aws-account>`_.

* Avoid using your root user account (the initial account you create) to access services. Instead, create an administrative user account, as explained in `Creating Your First IAM Admin User and Group <https://docs.aws.amazon.com/IAM/latest/UserGuide/getting-started_create-admin-group.html>`_. Note that the step for creating a user is toward the bottom of the page. The topic on creating a user also explains how to create keys for the user account so that you can access AWS services. AWS SDKs and services use access keys to create credentials for the user, and use those credentials to determine whether the user has permission to access various AWS services.

* To close your AWS account, see `Closing an Account <https://docs.aws.amazon.com/awsaccountbilling/latest/aboutv2/close-account.html>`_.
