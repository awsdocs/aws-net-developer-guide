.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

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
However, not all SDK functionality is available on each of these platforms. This topic describes the
differences in support for each platform.

.. note:: The |sdk-net| is not Federal Information Processing Standard (FIPS) compliant when used 
   by applications built against version 2.0 of the CLR. For details on how you can substitute a 
   FIPS compliant implementation in that environment, refer to 
   `CryptoConfig <https://blogs.msdn.microsoft.com/shawnfa/2008/12/02/cryptoconfig/>`_ on the 
   Microsoft blog and the `CLR Security <http://clrsecurity.codeplex.com/>`_ team's  HMACSHA256 class 
   ( HMACSHA256Cng ) in Security.Cryptography.dll.

.. _net-dg-platform-diff-netfx35:

|sdk-net| Framework 3.5
=======================

This version of the |sdk-net| is the one most similar to version 1. This version, compiled against
.NET Framework 3.5, supports the same set of services as version 1. It also uses the same
:ref:`pattern for making asynchronous calls <sdk-net-async-api>`.

.. note:: This version contains a number of changes that may break code that was designed for version 1. For
   more information, see the :ref:`Migration Guide <net-dg-migration-guide-v3>`.


.. _net-dg-platform-diff-netfx45:

|sdk-net| Framework 4.5
=======================

The version of the |sdk-net| compiled against .NET Framework 4.5 supports the same set of services
as version 1 of |sdk-net|. However, it uses a different pattern for asynchronous calls. Instead of
the Begin/End pattern, it uses the task-based pattern, which allows developers to use the new 
`async and await <http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx>`_ keywords introduced 
in `C# 5.0 <https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29#Versions>`_.


.. _net-dg-platform-diff-winrt:

AWS SDK for Portable Class Library
==================================

The |sdk-net| supports Portable Class Library projects, which allow you to target multiple platforms, 
including Windows Store, Windows Phone, and Xamarin on iOS and Android. See the AWS Mobile SDK for 
.NET and Xamarin for more details.

Unity Support
=============

The |sdk-net| supports generating Assemplies for Unity. More information can be found in the 
`Unity README <https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md>`_.
