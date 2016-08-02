.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-platform-diffs-v2:

#####################################
Platform Differences in the |sdk-net|
#####################################

The |sdk-net| provides four distinct assemblies for developers to target different platforms.
However, not all SDK functionality is available on each of these platforms. This topic describes the
differences in support for each platform.

.. _net-dg-platform-diff-netfx35:

|sdk-net| Framework 3.5
=======================

This version of the the SDK is the one most similar to version 1. This version, compiled against
.NET Framework 3.5, supports the same set of services as version 1. It also uses the same
:ref:`pattern for making asynchronous calls <sdk-net-async-api>`.

.. note:: This version contains a number of changes that may break code that was designed for version 1. For
    more information, see the :ref:`Migration Guide <net-dg-migration-guide-v2>`.


.. _net-dg-platform-diff-netfx45:

|sdk-net| Framework 4.5
=======================

The version of the the SDK compiled against .NET Framework 4.5 supports the same set of services as
version 1 of the SDK. However, it uses a different pattern for asynchronous calls. Instead of the
Begin/End pattern it uses the task-based pattern, which allows developers to use the new `async and
await <http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx>`_ keywords introduced in `C#
5.0 <https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29#Versions>`_.


.. _net-dg-platform-diff-winrt:

AWS SDK for |WinRT|
===================

The version of the the SDK compiled for `WinRT
<http://windows.microsoft.com/en-us/windows/rt-welcome>`_ supports only asynchronous method calls
using :code:`async` and :code:`await`.

This version does not provide all of the functionality for |S3| and |DDB| that was available in
version 1 of the SDK. The following |S3| functionality is currently unavailable in the |WinRT|
version of SDK.

* `Transfer Utility <TS3TransferTransferUtilityNET45.html>`_

* `IO Namespace <NS3IONET45.html>`_

The |WinRT| version of the SDK does not support decryption of the Windows password using the
`GetDecryptedPassword <MEC2GetPasswordDataResultGetDecryptedPasswordStringNET45.html>`_ method.


.. _net-dg-platform-diff-winphone:

AWS SDK for |WP8|
=================

The version of the the SDK compiled for |WP8| has a programming model similar to |WinRT|. As with
the |WinRT| version, it supports only asynchronous method calls using :code:`async` and
:code:`await`. Also, because |WP8| doesn't natively support :code:`System.Net.Http.HttpClient`, the
SDK depends on Microsoft's portable class implementation of :code:`HttpClient`, which is hosted on
nuget at the following URL:

* http://nuget.org/packages/Microsoft.Net.Http/2.1.10

This version of the |sdk-net| supports the same set of services supported in the 
|sdk-android|_ and the |sdk-ios|_:

* |EC2|

* |ELB|

* |AS|

* |S3|

* |SNS|

* |SQS|

* |SES|

* |DDB|

* |SDB|

* |CW|

* |STS|

This version does not provide all of the functionality for |S3| and |DDB| available in version 1 of
the SDK. The following |S3| functionality is currently unavailable in the |WP8| version of SDK.

* :sdk-net-api-v2:`Transfer Utility <TS3TransferTransferUtilityNET45>`

* :sdk-net-api-v2:`IO Namespace <NS3IONET45>`

Also, the |WP8| version of the SDK does not support decryption of the Windows password using the
:sdk-net-api-v2:`GetDecryptedPassword <MEC2GetPasswordDataResultGetDecryptedPasswordStringNET45>` method.



