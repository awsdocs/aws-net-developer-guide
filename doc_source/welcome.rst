.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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
The SDK supports development on any platform that supports the .NET Framework 3.5 or later, and
you can develop applications with the SDK using Visual Studio 2010 or later.

.. contents:: Topics
     :local:
     :depth: 1

What's in the SDK
=================

The |sdk-net| includes the following:

* The current version of the |sdk-net|

* All previous major versions of the |sdk-net|

* Sample code that demonstrates how to use the |sdk-net| with several AWS services

To simplify installation, AWS provides the |TFW|, which is a Windows installation package that
includes:

* The |sdk-net|_

* The |TWPlong| (see the |TWP-ug|_)

* The |TVSlong| (see the |TVS-ug|_)

As an alternative to installing the |TFW|, you can use NuGet to download individual AWSSDK service
assemblies for a specific application project. For more information, see :ref:`net-dg-nuget`.

.. note:: We recommend using Visual Studio Professional 2010 or later to implement your applications.

.. _guidemap:

How to Use This Guide
=====================

The *AWS SDK for .NET Developer Guide* describes how to implement applications for AWS using the
|sdk-net|, and includes the following:

:ref:`net-dg-setup`
    How to install and configure the |sdk-net|. If you have not used the |sdk-net| before or are
    having trouble with its configuration, you should start here.

:ref:`net-dg-programming-techniques`
    The basics of how to implement applications with the |sdk-net| that applies to all AWS services.
    This section also includes information about how to migrate code to the latest version of the
    |sdk-net|, and describes the differences between the last version and this one.

:ref:`tutorials-examples`
    A set of tutorials, walkthroughs, and examples showing how to use the |sdk-net| to create
    applications for particular AWS services.

:ref:`net-dg-additional-resources`
    More resources outside of this guide that provide valuable information about AWS and the
    |sdk-net|.

A related document, |sdk-net-api|_, provides a detailed description
of each namespace and class.


.. _supported-services:

Supported Services and Revision History
=======================================

The |sdk-net| supports most AWS infrastructure products, and more services are added frequently. For
a list of the AWS services supported by the SDK, see the `SDK README file
<https://github.com/aws/aws-sdk-net/blob/master/README.md>`_.

To see what changed in a given release, see the `SDK change log
<https://github.com/aws/aws-sdk-net/blob/master/SDK.CHANGELOG.md>`_.
