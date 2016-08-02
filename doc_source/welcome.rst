.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _welcome:

############
|sdk-net-dg|
############

The |sdk-net| makes it easier for Windows developers to build .NET applications that tap into the
cost-effective, scalable, and reliable AWS services such as |S3long| (|S3|) and |EC2long| (|EC2|).
The the SDK supports development on any platform that supports the .NET Framework 3.5 or later, and
you can develop applications with the SDK using Visual Studio 2010 or later.

The |sdk-net| includes the following:

* The current version of the |sdk-net|.

* All previous major versions of the |sdk-net|.

* Sample code that demonstrates how to use the |sdk-net| with several AWS services.

To simplify installation, AWS provides the |TFW|, which is a Windows installation package that
includes:

* The |sdk-net|.

* The |TWPlong|. For more information about the |TWPlong|, see the |TWP-ug|_.

* The |TVSlong|. For more information about the |TVSlong|, see the |TVS-ug|_.

As an alternative to installing the |TFW|, you can use NuGet to download the AWSSDK assembly for a
specific application project. For more information, see :ref:`net-dg-nuget`.

.. note:: We recommend using Visual Studio Professional 2010 or higher to implement your applications. It is
   possible to use Visual Studio Express to implement applications with the the SDK, including
   installing the |TVS|. However, the installation includes only the AWS project templates and the
   Standalone Deployment Tool. In particular, |TVS| on Visual Studio Express does not support AWS
   Explorer.

.. _guidemap:

How to Use This Guide
=====================

The *AWS SDK for .NET Developer Guide* describes how to implement applications for AWS using the the
SDK, and includes the following:

:ref:`net-dg-setup`
    How to install and configure the the SDK. If you have not used the the SDK before or are having
    trouble with its configuration, you should start here.

:ref:`net-dg-programming-techniques`
    The basics of how to implement applications with the the SDK that applies to all AWS services.
    This chapter also includes information about how to migrate code to the latest version of the
    the SDK, and describes the differences between the last version and this one.

:ref:`tutorials-examples`
    A set of tutorials, walkthroughs, and examples of how to use the the SDK to create applications
    for particular AWS services.

:ref:`net-dg-additional-resources`
    Additional resources outside of this guide that provide more information about AWS and the the
    SDK.


.. note:: A related document, |sdk-net-api|_, provides a detailed description
   of each namespace and class.


.. _supported-services:

Supported Services and Revision History
=======================================

The |sdk-net| supports most AWS infrastructure products, and we regularly release updates to the the
SDK to support new services and new service features. To see what changed with a given release, see
the `the SDK README file <https://github.com/aws/aws-sdk-net/blob/master/README.md>`_.

To see what changed in a given release, see the `the SDK change log
<https://github.com/aws/aws-sdk-net/blob/master/SDK.CHANGELOG.md>`_.


.. _about-aws:

About Amazon Web Services
=========================

Amazon Web Services (AWS) is a collection of digital infrastructure services that developers can
leverage when developing their applications. The services include computing, storage, database, and
application synchronization (messaging and queuing).

AWS uses a pay-as-you-go service model. You are charged only for the services that you |mdash| or
your applications |mdash| use. Also, to make AWS useful as a platform for prototyping and
experimentation, AWS offers a free usage tier, in which services are free below a certain level of
usage. For more information about AWS costs and the free usage tier go to 
`Test-Driving AWS in the Free Usage Tier <http://docs.aws.amazon.com/awsaccountbilling/latest/aboutv2/billing-free-tier.html>`_.

To obtain an AWS account, go to the |aws-home|_ and click :guilabel:`Sign Up Now`.



