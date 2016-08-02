.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _resource-level-apis-intro:

###############################################
Programming with the AWS Resource APIs for .NET
###############################################

The |sdk-net| provides the AWS Resource APIs for .NET. These resource APIs provide a resource-level
programming model that enables you to write code to work more directly with resources that are
managed by AWS services. A resource is a logical object that is exposed by an AWS service's APIs.
For example, |IAMlong| (|IAM|) exposes users and groups as resources that can be programmatically
accessed more directly by these resource APIs than by other means.

The AWS Resource APIs for .NET are currently provided as a preview. This means that these resource
APIs may frequently change in response to customer feedback, and these changes may happen without
advance notice. Until these resource APIs exit the preview stage, please be cautious about writing
and distributing production-quality code that relies on them.

Using the AWS Resource APIs for .NET provide these benefits:

* The resource APIs in the the SDK are easier to understand conceptually than their low-level API
  counterparts. The low-level APIs in the the SDK typically consist of sets of matching
  request-and-response objects that correspond to HTTP-based API calls focusing on somewhat
  isolated AWS service constructs. In contrast, these resource APIs represent logical
  relationships among resources within AWS services and intuitively use familiar .NET programming
  constructs.

* Code that you write with the resource APIs is easier for you and others to comprehend when compared
  to their low-level API equivalents. Instead of writing somewhat complex request-and-response
  style code with the low-level APIs to access resources, you can get directly to resources with
  the resource APIs. If you're working with a team of developers in the same code base, it's
  typically easier to understand what has already been coded and to start contributing quickly to
  existing code.

* You will typically write less code with the resource APIs than with equivalent low-level API code.
  Request-and-response style code with the low-level APIs can sometimes be quite long. Equivalent
  resource APIs code is typically much shorter, more compact, and easer to debug.

Here's a brief example of using C# and the AWS Resource APIs for .NET to create a new |IAM| user
account:

.. literalinclude:: how-to/iam/iam-create-user-resource-level.txt
   :language: csharp

Compare this to an equivalent example of using the low-level APIs:

.. literalinclude:: how-to/iam/iam-create-user-low-level.txt
   :language: csharp

Even with this brief code example, you'll see that the resource APIs code is a bit easier to
comprehend than the low-level code, and the resource APIs code is a bit shorter and more compact
than its low-level counterpart.

There are a few limitations to note when using the resource APIs as compared to the low-level APIs
in the the SDK:

* Not all of the AWS services currently have resource APIs (although this number is growing).
  Currently, the following AWS services have resource APIs in the the SDK:

    * |CFNLong|

    * |GLlong|

    * |IAMlong| (|IAM|)

    * |SNSlong| (|SNS|)

    * |SQSlong| (|SQS|)

* The resource APIs are currently provided as a preview. Please be cautious about writing and
  distributing production-quality code that relies on these resource APIs, especially as the
  resource APIs may undergo frequent changes during the preview stage.

The following information describes how to download and reference the resource APIs. Links to code
examples and related programming concepts for supported AWS services are also provided.

.. _resource-level-apis-intro-setup:

Download and Reference the AWS Resource APIs for .NET
=====================================================

1. If you have an existing project in Visual Studio that you want to use the resource APIs with, and
   that project is already referencing the AWS .NET library file (:file:`AWSSDK.dll`), you must
   remove this reference. This reference is set by default if you have the |TVSlong| installed and
   you have created a project based upon one of the AWS project templates (for example, the Visual
   C# AWS Console Project template). Or, you may have previously set a reference to the library
   explicitly, which the the SDK typically installs to :file:`drive:\\Program Files (x86)\\AWS SDK
   for .NET\\bin`. To remove the reference for example in :guilabel:`Solution Explorer` in Visual
   Studio, in the :guilabel:`References` folder, right-click :guilabel:`AWSSDK` and then click
   :guilabel:`Remove`.

2. Download the AWS Resource APIs for .NET library file from the `resourceAPI-preview
   <https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview>`_ branch of the
   :code:`aws-sdk-net` GitHub repository onto your development machine. To do this, in the
   `binaries <https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries>`_ folder at
   that location, download and then unzip the file named `dotnet35.zip
   <https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries/dotnet35.zip>`_ (for
   projects that rely on the .NET Framework 3.5) or `dotnet45.zip
   <https://github.com/aws/aws-sdk-net/tree/resourceAPI-preview/binaries/dotnet45.zip>`_ (for
   projects that rely on the .NET Framework 4.5). Note that because these zip files contains a file
   that is also named :file:`AWSSDK.dll`, make sure to unzip the file to a location *other* than
   where your AWS .NET library file is already installed. For example, unzip the file to any
   location *other* than :file:`drive:\\Program Files (x86)\\AWS SDK for .NET\\bin`. The unzipped
   contents contain both .NET Framework 3.5 and 4.5 versions of the AWS Resource APIs for .NET
   library file (:file:`AWSSDK.dll`), which you can set a reference to from your projects.

   Note that after unzipping, there will be three files: :file:`AWSSDK.dll`, :file:`AWSSDK.pdb`,
   and :file:`AWSSDK.xml`. To enable robust debugging and help within Visual Studio, make sure that
   these three files remain together in the same folder.

3. From the project in Visual Studio that you want to use the resource APIs with, set a reference to
   the AWS Resource APIs for .NET library file that you just unzipped. To do this for example in
   :guilabel:`Solution Explorer` in Visual Studio, right-click the :guilabel:`References` folder;
   click :guilabel:`Add Reference`; click :guilabel:`Browse`; browse to and select the
   :file:`AWSSDK.dll` file that you just unzipped; click :guilabel:`Add` and then click
   :guilabel:`OK`.

4. Import the specific resource APIs in the AWS Resource APIs for .NET that you want to use in your
   project's code. These APIs typically take the format :code:`Amazon.ServiceName.Resources`, where
   {ServiceName} is typically some recognizable phrase that corresponds to the specific service.
   For example for the |IAMlong| resource APIs, in C# you would include the following :code:`using`
   directive at the top of a class file:
 
   .. code-block:: csharp
 
       using Amazon.IdentityManagement.Resources;
 
5. As needed, import any corresponding low-level APIs that the specific resource APIs rely upon. These
   APIs typically take the format :code:`Amazon.ServiceName.Model` and sometimes also
   :code:`Amazon.ServiceName`, where {ServiceName} is typically some recognizable phrase that
   corresponds to the specific service. For example for the |IAMlong| low-level APIs, in C# you
   would include the following :code:`using` directives at the top of a class file:

   .. code-block:: csharp
 
       using Amazon.IdentityManagement.Model;
       // Possibly also the following, depending on which of the resource APIs that you use:
       using Amazon.IdentityManagement
 
6. Because the resource APIs are currently provided as a preview, you should be cautious about writing
   production-quality code that relies on them, especially as the resource APIs may undergo
   frequent changes during the preview stage. However, if you choose to distribute the project
   anyway, make sure to include a copy of the AWS Resource APIs for .NET library file. To do this
   for example in :guilabel:`Solution Explorer` in Visual Studio, within the :guilabel:`References`
   folder, click :guilabel:`AWSSDK`; in the :guilabel:`Properties` window, next to :guilabel:`Copy
   Local`, select :guilabel:`True` if it is not already selected.
 
   .. note:: If you distribute a project that has a copy of the resource APIs library file included, 
      and then the resource library APIs change, the only way for your project to include the new 
      changes is to redistribute your project with an updated resource APIs library file copied locally.

.. _resource-level-apis-intro-examples:

Code Examples for Resource APIs
===============================

The following links provide code examples for AWS services that support resource-level APIs in the
the SDK.

* :ref:`CloudFormation <cloudformation-apis-intro-resource-level>`

* :ref:`Amazon Glacier <glacier-apis-intro-resource-level>`

* :ref:`AWS Identity and Access Management (IAM) <iam-resource-api-examples>`

* :ref:`Amazon Simple Notification Service (Amazon SNS) <sns-apis-intro-resource-level>`

* :ref:`Amazon Simple Queue Service (Amazon SQS) <sqs-apis-intro-resource-level>`



