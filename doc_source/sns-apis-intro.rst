.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sns-apis-intro:

########################################
|SNSlong| Programming with the |sdk-net|
########################################

The |sdk-net| supports |SNSlong| (|SNS|), which is a web service that enables applications, end
users, and devices to instantly send and receive notifications from the cloud. For more information,
see |sns|_.

The following example shows how to use the low-level APIs to list accessible topics in |SNS|. This
example uses the default client constructor which constructs 
:sdk-net-api:`AmazonSimpleNotificationServiceClient <SNS/MSNSSNSctor>` with the credentials loaded from 
the application's default configuration, and if unsuccessful from the Instance Profile service on an 
EC2 instance.

.. literalinclude:: how-to/sns/sns-list-topics-low-level.txt
   :language: csharp

For related API reference information, see :sdk-net-api:`Amazon.SimpleNotificationService <SNS/NSNS>`,
:sdk-net-api:`Amazon.SimpleNotificationService.Model <SNS/NSNSModel>`, and
:sdk-net-api:`Amazon.SimpleNotificationService.Util <SNS/NSNSUtil>` in the 
|sdk-net-api|_.


