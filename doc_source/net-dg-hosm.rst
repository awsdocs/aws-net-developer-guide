.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-roles:

##########################################################
Tutorial: Grant Access Using an IAM Role and the |sdk-net|
##########################################################

All requests to AWS must be cryptographically signed using credentials issued by AWS. Therefore, you
need a strategy for managing credentials for software that runs on |EC2| instances. You must
distribute, store, and rotate these credentials in a way that keeps them secure, but also accessible
to the software.

We designed IAM roles so that you can effectively manage AWS credentials for software running on EC2
instances. You create an IAM role and configure it with the permissions the software requires. For
more information about the benefits of this approach, see 
:ec2-ug:`IAM Roles for Amazon EC2 <iam-roles-for-amazon-ec2>` in the |ec2-ug-win| and 
:iam-ug:`Roles (Delegation and Federation) <WorkingWithRoles>` in the |IAM-ug|.

To use the permissions, the software constructs a client object for the AWS service. The constructor
searches the credentials provider chain for credentials. For .NET, the credentials provider chain is
as follows:

* The :file:`App.config` file

* The instance metadata associated with the IAM role for the EC2 instance

If the client does not find credentials in :file:`App.config`, it retrieves temporary credentials
that have the same permissions as those associated with the IAM role. The credentials are retrieved
from instance metadata. The credentials are stored by the constructor on behalf of the customer
software and are used to make calls to AWS from that client object. Although the credentials are
temporary and eventually expire, the SDK client periodically refreshes them so that they continue to
enable access. This periodic refresh is completely transparent to the application software.

The following walkthrough uses a sample program that retrieves an object from |S3| using the AWS
credentials you've configured. Next, we create an IAM role to provide the AWS credentials. Finally,
we launch an instance with an IAM role that provides the AWS credentials to the sample program
running on the instance.


.. contents:: **Walkthrough**
    :local:
    :depth: 1

.. _net-dg-using-hosm-to-retrieve-an-object-from-ec2:

Create a Sample that Retrieves an Object from |S3|
==================================================

The following sample code retrieves an object from |S3|. It requires a text file in an |S3| bucket
that you have access to. For more information about creating an |S3| bucket and uploading an object,
see the |S3-gsg|_. It also requires AWS credentials that provide you with access to the |S3|
bucket. For more information, see :ref:`net-dg-config-creds`.

.. literalinclude:: samples/iamroles.cs
    :language: csharp

**To test the sample code**

1. Open Visual Studio and create an AWS Console project.

2. Add the `AWSSDK.S3 <http://www.nuget.org/packages/AWSSDK.S3>`_ package to your project.

3. Replace the code in the :file:`Program.cs` file with the sample code.

4. Replace the following values:

    :code:`bucket-name`
        The name of your |S3| bucket.

    :code:`s3-file-name`
        The path and name of a text file in the bucket.

    :code:`output-file-name`
        The path and file name to write the file to.

5. Compile and run the sample program. If the program succeeds, it displays the following output and
   creates a file named :file:`s3Object.txt` on your local drive that contains the text it
   retrieved from the text file in |S3|.

    .. code-block:: none

        Retrieving (GET) an object

   If the program fails, ensure you are using credentials that provide you with access to the
   bucket.

6. (Optional) Transfer the sample program to a running Windows instance on which you haven't set up
   credentials. Run the program and verify that it fails because it can't locate credentials.


.. _net-dg-create-the-role:

Create an IAM Role
==================

Create an IAM role that has the appropriate permissions to access |S3|.

**To create the IAM role**

1. Open the |IAM| console.

2. In the navigation pane, choose :guilabel:`Roles`, and then choose :guilabel:`Create New Role`.

3. Type a name for the role, and then choose :guilabel:`Next Step`. Remember this name because you'll
   need it when you launch your EC2 instance.

4. Under :guilabel:`AWS Service Roles`, choose :guilabel:`Amazon EC2`. Under :guilabel:`Select Policy Template`, 
   choose :guilabel:`Amazon S3 Read Only Access`. Review the policy, and then choose :guilabel:`Next Step`.

5. Review the role information, and then choose :guilabel:`Create Role`.


.. _net-dg-launch-ec2-instance-with-instance-profile:

Launch an EC2 Instance and Specify the IAM Role
===============================================

You can launch an EC2 instance with an IAM role using the |EC2| console or the |sdk-net|.

* To launch an EC2 instance using the console, follow the directions in 
  :ec2-ug:`Launching a Windows Instance <EC2Win_GetStarted.html#EC2Win_LaunchInstance>` in the |ec2-ug-win|. 
  When you reach the :guilabel:`Review Instance Launch` page, choose :guilabel:`Edit instance details`. 
  In :guilabel:`IAM role`, specify the IAM role you created previously. Complete the procedure as
  directed. You'll need to create or use an existing security group and key pair to connect to the
  instance.

* To launch an EC2 instance with an IAM role using the |sdk-net|, see :ref:`run-instance`.

An IAM user can't launch an instance with an IAM role without the permissions granted by the
following policy.

.. code-block:: json

    {
      "Version": "2012-10-17",
      "Statement": [{
        "Effect": "Allow",
        "Action": [
          "iam:PassRole",
          "iam:ListInstanceProfiles",
          "ec2:*"
        ],
        "Resource": "*"
      }]
    }


.. _net-dg-run-the-program:

Run the Sample Program on the EC2 Instance
==========================================

To transfer the sample program to your EC2 instance, connect to the instance using the |console| as
described in the following procedure.

.. note:: Alternatively, connect using the |TVS| (as described in 
   :tvs-ug:`Connecting to an Amazon EC2 Instance <managing-ec2.html#connect-ec2>` in the |TVSlong|) 
   and then copy the files from your local drive to the instance. The Remote Desktop session is 
   automatically configured so that your local drives are available to the instance.

**To run the sample program on the EC2 instance**

1. Open the |EC2| console.

2. Get the password for your EC2 instance as follows:

  a. In the navigation pane, choose :guilabel:`Instances`. Choose the instance, and then choose
     :guilabel:`Connect`.

  b. In the :guilabel:`Connect To Your Instance` dialog box, choose :guilabel:`Get Password`. (It will
     take a few minutes after the instance is launched before the password is available.)

  c. Choose :guilabel:`Browse` and navigate to the private key file you created when you launched the
     instance. Choose the file and choose :guilabel:`Open` to copy the entire contents of the
     file into the contents box.

  d. Choose :guilabel:`Decrypt Password`. The console displays the default administrator password for the
     instance in the :guilabel:`Connect To Your Instance` dialog box, replacing the link to
     :guilabel:`Get Password` shown earlier with the password.

  e. Record the default administrator password, or copy it to the clipboard. You need this password to
     connect to the instance.

3. Connect to your EC2 instance as follows:

  a. Choose :guilabel:`Download Remote Desktop File`. When your browser prompts you to do so, save the
     :file:`.rdp` file. When you have finished, you can choose :guilabel:`Close` to dismiss the
     :guilabel:`Connect To Your Instance` dialog box.

  b. Navigate to your downloads directory, right-click the :file:`.rdp` file, and then choose
     :guilabel:`Edit`. On the :guilabel:`Local Resources` tab, under :guilabel:`Local devices and
     resources`, choose :guilabel:`More`. Choose :guilabel:`Drives` to make your local drives
     available to your instance, and then choose :guilabel:`OK`.

  c. Choose :guilabel:`Connect` to connect to your instance. You may get a warning that the publisher of
     the remote connection is unknown.

  d. Sign in to the instance as prompted, using the default :guilabel:`Administrator` account and the
     default administrator password you recorded or copied previously.

     Sometimes copying and pasting content can corrupt data. If you encounter a "Password Failed"
     error when you sign in, try typing in the password manually. For more information, see
     :ec2-ug-win:`Connecting to Your Windows Instance Using RDP <connecting_to_windows_instance>` and
     :ec2-ug-win:`Troubleshooting Windows Instances <troubleshooting-windows-instances>` in the
     |ec2-ug-win|.

4. Copy the program and the AWS assemblies (:file:`AWSSDK.Core.dll` and :file:`AWSSDK.S3.dll`) from
   your local drive to the instance.

5. Run the program and verify that it succeeds using the credentials provided by the IAM role.

   .. code-block:: none

      Retrieving (GET) an object
