.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-platform-diffs-v3:

#####################################
Platforms Supported by the |sdk-net|
#####################################

The |sdk-net| provides distinct groups of assemblies for developers to target different platforms.
However, not all SDK functionality is the same on each of these platforms. This topic describes the
differences in support for each platform.

.. _net-dg-platform-diff-netfx35:

.. _net-dg-platform-diff-netfx45:

.NET Framework 4.5
==================

This version of the |sdk-net| is compiled against .NET Framework 4.5 and runs in the .NET 4.0
runtime. AWS service clients support synchronous and asynchronous calling patterns and use the
`async and await <http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx>`_ keywords
introduced in `C# 5.0 <https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29#Versions>`_.


.. _net-dg-platform-diff-winrt:

.NET Framework 3.5
==================

This version of the |sdk-net| is compiled against .NET Framework 3.5, and runs either the .NET 2.0 or .NET 4.0
runtime. AWS service clients support synchronous and asynchronous calling patterns and use the older Begin and
End pattern.

.. note:: The |sdk-net| is not Federal Information Processing Standard (FIPS) compliant when used
   by applications built against version 2.0 of the CLR. For details on how you can substitute a
   FIPS compliant implementation in that environment, refer to
   `CryptoConfig <https://blogs.msdn.microsoft.com/shawnfa/2008/12/02/cryptoconfig/>`_ on the
   Microsoft blog and the `CLR Security <http://clrsecurity.codeplex.com/>`_ team's  HMACSHA256 class
   ( HMACSHA256Cng ) in Security.Cryptography.dll.


|net-core|
==========

The |sdk-net| supports applications written for .NET Core. AWS service clients only support asynchronous calling
patterns in .NET core. This also affects many of the high level abstractions built on top of service clients
like Amazon S3's :code:`TransferUtility` which will only support asynchronous calls in the .NET Core environment.
For details, see :doc:`net-dg-config-netcore`.

Portable Class Library
======================

The |sdk-net| also contains a Portable Class Library implementation. The Portable Class Library implementation
can target multiple platforms,including Universal Windows Platform (UWP), and Xamarin on iOS and Android. See
the AWS Mobile SDK for .NET and Xamarin for more details. AWS service clients only support asynchronous calling
patterns.

Unity Support
=============

The |sdk-net| supports generating Assemblies for Unity. More information can be found in the
`Unity README <https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md>`_.

More Info
=========

* :doc:`migration-v3`


