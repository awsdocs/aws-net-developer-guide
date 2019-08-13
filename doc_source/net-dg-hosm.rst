.. Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-roles:

###################################
Granting Access Using an |IAM| Role
###################################

.. meta::
   :description: Use this .NET code example to learn how to grant access to an application running on Amazon EC2.
   :keywords: AWS SDK for .NET examples, IAM roles

This .NET example shows you how to:

* Create a sample program that retrieves an object from |S3|
* Create an |IAM| role
* Launch an |EC2| instance and specify the |IAM| role
* Run the sample on the |EC2| instance

The Scenario
============

All requests to AWS must be cryptographically signed by using credentials issued by AWS. Therefore, you
need a strategy to manage credentials for software that runs on |EC2| instances. You have to
distribute, store, and rotate these credentials securely, but also keep them accessible
to the software.

|IAM| roles enable you to effectively manage AWS credentials for software running on EC2
instances. You create an |IAM| role and configure it with the permissions the software requires. For
more information about the benefits of using |IAM| roles, see
:ec2-ug:`IAM Roles for Amazon EC2 <iam-roles-for-amazon-ec2>` in the |EC2-ug-win| and
:iam-ug:`Roles (Delegation and Federation) <WorkingWithRoles>` in the |IAM-ug|.

To use the permissions, the software constructs a client object for the AWS service. The constructor
searches the credentials provider chain for credentials. For .NET, the credentials provider chain is
as follows:

* The :file:`App.config` file

* The instance metadata associated with the |IAM| role for the EC2 instance

If the client doesn't find credentials in :file:`App.config`, it retrieves temporary credentials
that have the same permissions as those associated with the |IAM| role from instance metadata. The credentials
are stored by the constructor on behalf of the application
software, and are used to make calls to AWS from that client object. Although the credentials are
temporary and eventually expire, the SDK client periodically refreshes them so that they continue to
enable access. This periodic refresh is completely transparent to the application software.

The following examples show a sample program that retrieves an object from |S3| using the AWS
credentials you configure. You create an |IAM| role to provide the AWS credentials. Finally,
you launch an instance with an |IAM| role that provides the AWS credentials to the sample program
running on the instance.

.. _net-dg-using-hosm-to-retrieve-an-object-from-ec2:

Create a Sample that Retrieves an Object from |S3|
==================================================

The following sample code requires a text file in an |S3| bucket
that you have access to, and AWS credentials that provide you with access to the |S3|
bucket.

For more information about creating an |S3| bucket and uploading an object,
see the |S3-gsg|_. For more information about AWS credentials, see :ref:`net-dg-config-creds`.

.. literalinclude:: samples/iamroles.cs
    :language: csharp

.. topic:: To test the sample code

    #. Open Visual Studio and create an AWS Console project.

    #. Add the `AWSSDK.S3 <http://www.nuget.org/packages/AWSSDK.S3>`_ package to your project.

    #. Replace the code in the :file:`Program.cs` file with the sample code.

    #. Replace the following values:

        :code:`bucket-name`
            The name of your |S3| bucket.

        :code:`s3-file-name`
            The path and name of a text file in the bucket.

        :code:`output-file-name`
            The path and file name to write the file to.

    #. Compile and run the sample program. If the program succeeds, it displays the following output and
       creates a file named :file:`s3Object.txt` on your local drive that contains the text it
       retrieved from the text file in |S3|.

        .. code-block:: none

            Retrieving (GET) an object

       If the program fails, be sure you're using credentials that provide you with access to the
       bucket.

    #. (Optional) Transfer the sample program to a running Windows instance on which you haven't set up
       credentials. Run the program and verify that it fails because it can't locate credentials.


.. _net-dg-create-the-role:

Create an |IAM| Role
====================

Create an IAM role that has the appropriate permissions to access |S3|.

.. topic:: To create the IAM role

    #. Open the |IAM| console.

    #. In the navigation pane, choose :guilabel:`Roles`, and then choose :guilabel:`Create New Role`.

    #. Type a name for the role, and then choose :guilabel:`Next Step`. Remember this name because you'll
       need it when you launch your EC2 instance.

    #. Under :guilabel:`AWS Service Roles`, choose :guilabel:`Amazon EC2`. Under :guilabel:`Select Policy Template`,
       choose :guilabel:`Amazon S3 Read Only Access`. Review the policy, and then choose :guilabel:`Next Step`.

    #. Review the role information, and then choose :guilabel:`Create Role`.


.. _net-dg-launch-ec2-instance-with-instance-profile:

Launch an EC2 Instance and Specify the |IAM| Role
=================================================

You can use the |EC2| console or the |sdk-net| to launch an EC2 instance with an |IAM| role.

* Using the console: Follow the directions in
  :ec2-ug:`Launching a Windows Instance <EC2Win_GetStarted.html#EC2Win_LaunchInstance>`
  in the |EC2-ug-win|.
  When you reach the :guilabel:`Review Instance Launch` page, choose :guilabel:`Edit instance details`.
  In :guilabel:`IAM role`, specify the IAM role you created previously. Complete the procedure as
  directed. You'll need to create or use an existing security group and key pair to connect to the
  instance.

* Using the |sdk-net|: See :ref:`run-instance`.

An |IAM| user can't launch an instance with an |IAM| role without the permissions granted by the
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

.. note:: Alternatively, connect using the |TVS| (see
   :tvs-ug:`Connecting to an Amazon EC2 Instance <managing-ec2.html#connect-ec2>` in the |TVSlong|)
   and then copy the files from your local drive to the instance. The Remote Desktop session is
   automatically configured so that your local drives are available to the instance.

.. topic:: To run the sample program on the EC2 instance

    #. Open the |EC2| console.

    #. Get the password for your EC2 instance:

       #. In the navigation pane, choose :guilabel:`Instances`. Choose the instance, and then choose
         :guilabel:`Connect`.
                   
       #. In the :guilabel:`Connect To Your Instance` dialog box, choose :guilabel:`Get Password`. (It will
          take a few minutes after the instance is launched before the password is available.)
          
       #. Choose :guilabel:`Browse` and navigate to the private key file you created when you launched the
          instance. Choose the file, and then choose :guilabel:`Open` to copy the file's contents into the contents box.
          
       #. Choose :guilabel:`Decrypt Password`. The console displays the default administrator password for the
          instance in the :guilabel:`Connect To Your Instance` dialog box, replacing the link to
          :guilabel:`Get Password` shown earlier with the password.
                    
       #. Record the default administrator password or copy it to the clipboard. You need this password to
          connect to the instance.

    #. Connect to your EC2 instance:

       #. Choose :guilabel:`Download Remote Desktop File`. When your browser prompts you, save the
          :file:`.rdp` file. When you finish, choose :guilabel:`Close` to close the
          :guilabel:`Connect To Your Instance` dialog box.

       #. Navigate to your downloads directory, right-click the :file:`.rdp` file, and then choose
          :guilabel:`Edit`. On the :guilabel:`Local Resources` tab, under :guilabel:`Local devices and
          resources`, choose :guilabel:`More`. Choose :guilabel:`Drives` to make your local drives
          available to your instance. Then choose :guilabel:`OK`.

       #. Choose :guilabel:`Connect` to connect to your instance. You may get a warning that the publisher
          of the remote connection is unknown.

       #. Sign in to the instance when prompted, using the default :guilabel:`Administrator` account and the
          default administrator password you recorded or copied previously.

          Sometimes copying and pasting content can corrupt data. If you encounter a "Password Failed"
          error when you sign in, try typing in the password manually. For more information, see
          :ec2-ug-win:`Connecting to Your Windows Instance Using RDP <connecting_to_windows_instance>` and
          :ec2-ug-win:`Troubleshooting Windows Instances <troubleshooting-windows-instances>` in the
          |EC2-ug-win|.

    #. Copy the program and the AWS assemblies (:file:`AWSSDK.Core.dll` and :file:`AWSSDK.S3.dll`) from
       your local drive to the instance.

    #. Run the program and verify that it succeeds using the credentials provided by the |IAM| role.

       .. code-block:: none

          Retrieving (GET) an object
