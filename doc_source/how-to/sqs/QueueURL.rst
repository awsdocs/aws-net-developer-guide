.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _sqs-queue-url:

#####################
Amazon SQS Queue URLs
#####################

You require the queue URL to send, receive, and delete queue messages. A queue URL is constructed in
the following format:

.. code-block:: none

     https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}

To find your AWS account number, go to :console:`Security Credentials <iam>`. 
Your account number is located under :guilabel:`Account Number` in the upper-right of the page.

For information about sending a message to a queue, see :ref:`send-sqs-message`.

For information about receiving messages from a queue, see :ref:`receive-sqs-message`.

For information about deleting messages from a queue, see :ref:`delete-sqs-message`.


