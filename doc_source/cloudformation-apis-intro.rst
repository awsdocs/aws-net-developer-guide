.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _cloudformation-apis-intro:

########################################
|CFNLong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |CFNLong|, which creates and provision AWS infrastructure deployments
predictably and repeatedly. For more information, see |CFN-gsg|_.

The following information introduces you to the |CFN| programming models in the the SDK.

.. _cloudformation-apis-intro-models:

Programming Models
==================

The the SDK provides two programming models for working with |CFN|. These programming models are
known as the *low-level* and *resource* models. The following information describes these models,
how to use them, and why you would want to use them.

.. _cloudformation-apis-intro-low-level:

Low-Level APIs
--------------

The the SDK provides low-level APIs for programming with |CFN|. These low-level APIs typically
consist of sets of matching request-and-response objects that correspond to HTTP-based API calls
focusing on their corresponding service-level constructs.

The following example shows how to use the low-level APIs to list accessible resources in |CFN|:

.. literalinclude:: how-to/cloudformation/cloudformation-list-stacks-low-level.txt
   :language: csharp

For related API reference information, see :code:`Amazon.CloudFormation` and
:code:`Amazon.CloudFormation.Model` in the sdk-net-api-v2_.


.. _cloudformation-apis-intro-resource-level:

Resource APIs
-------------

The the SDK provides the AWS Resource APIs for .NET for programming with |CFN|. These resource APIs 
provide a resource-level programming model that enables you to write code to work more directly with 
|CFN| resources as compared to their low-level API counterparts. (For more information about the AWS 
Resource APIs for .NET, including how to download and reference these resource APIs, see 
:ref:`resource-level-apis-intro`.)

The following example shows how to use the AWS Resource APIs for .NET to list accessible resources
in |CFN|:

.. literalinclude:: how-to/cloudformation/cloudformation-list-stacks-resource-level.txt
   :language: csharp

For related API reference information, see 
:sdk-net-api-v2:`Amazon.CloudFormation.Resources <NCloudFormationResourcesNET45>`.
