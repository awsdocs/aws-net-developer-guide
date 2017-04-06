.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _run-instance:

###########################
Launching an |EC2| Instance
###########################

.. meta::
   :description: Use this .NET code example to learn how to launch an Amazon EC2 instance.
   :keywords: AWS SDK for .NET examples, EC2 instances

Use the following procedure to launch one or more identically configured |EC2| instances from the same
Amazon Machine Image (AMI). After you create your EC2 instances, you can check their status. When
your EC2 instances are running, you can connect to them.

.. _launch-instance:

Launch an EC2 Instance in EC2-Classic or in a VPC
=================================================

You can launch an instance in either EC2-Classic or in a VPC. For more information about EC2-Classic
and EC2-VPC, see :ec2-ug-win:`Supported Platforms <ec2-supported-platforms>` in the |EC2-ug-win|.

.. topic:: To launch an EC2 instance in EC2-Classic

#. Create and initialize a :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>` object.
   Be sure the AMI, key pair, and security group you specify exist in the region you specified when
   you created the client object.

   .. code-block:: csharp

      string amiID = "ami-e189c8d1";
      string keyPairName = "my-sample-key";

      List<string> groups = new List<string>() { mySG.GroupId };
      var launchRequest = new RunInstancesRequest()
      {
        ImageId = amiID,
        InstanceType = InstanceType.T1Micro,
        MinCount = 1,
        MaxCount = 1,
        KeyName = keyPairName,
        SecurityGroupIds = groups
      };

   :code:`ImageId`
      The ID of the AMI. For a list of public AMIs, see
      :mktplace-search:`Amazon Machine Images <AMISAWS>`.

   :code:`InstanceType`
      An instance type that is compatible with the specified AMI. For more information, see
      :ec2-ug-win:`Instance Types <instance-types.html>` in the |EC2-ug-win|.

   :code:`MinCount`
      The minimum number of EC2 instances to launch. If this is more instances than |EC2| can
      launch in the target Availability Zone, |EC2| launches no instances.

   :code:`MaxCount`
      The maximum number of EC2 instances to launch. If this is more instances than |EC2| can
      launch in the target Availability Zone, |EC2| launches the largest possible number of
      instances above :code:`MinCount`. You can launch between 1 and the maximum number of
      instances you're allowed for the instance type. For more information, see
      :ec2-faq:`How many instances can I run in Amazon EC2 <#How_many_instances_can_I_run_in_Amazon_EC2>`
      in the |EC2| General FAQ.

    :code:`KeyName`
      The name of the EC2 key pair. If you launch an instance without specifying a key pair, you
      can't connect to it. For more information, see :ref:`create-key-pair`.

    :code:`SecurityGroupIds`
      The identifiers of one or more security groups. For more information, see
      :ref:`create-security-group`.

#. (Optional) To launch the instance with an :ref:`IAM role <net-dg-roles>`, specify an IAM instance
   profile in the :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>` object.

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

   For example, the following snippet instantiates and configures an
   :sdk-net-api:`IamInstanceProfileSpecification <EC2/TEC2IamInstanceProfileSpecification>` object
   for an IAM role named :code:`winapp-instance-role-1`.

   .. code-block:: csharp

      var instanceProfile = new IamInstanceProfile();
      instanceProfile.Id  = "winapp-instance-role-1";

   To specify this instance profile in the :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>`
   object, add the following line.

   .. code-block:: csharp

      launchRequest.IamInstanceProfile = instanceProfile;

#. Launch the instance by passing the request object to the
   :sdk-net-api:`RunInstances <EC2/MEC2EC2RunInstancesRunInstancesRequest>` method. Save the
   ID of the instance because you need it to manage the instance.

   Use the returned :sdk-net-api:`RunInstancesResponse <EC2/TEC2RunInstancesResponse>` object
   to get the instance IDs for the new instances. The :code:`Reservation.Instances` property
   contains a list of :sdk-net-api:`Instance <EC2/TEC2Instance>` objects, one for each EC2
   instance you successfully launched. You can retrieve the ID for each instance from the
   :code:`InstanceId` property of the :sdk-net-api:`Instance <EC2/TEC2Instance>` object.

   .. code-block:: csharp

      var launchResponse = ec2Client.RunInstances(launchRequest);
      var instances = launchResponse.Reservation.Instances;
      var instanceIds = new List<string>();
      foreach (Instance item in instances)
      {
        instanceIds.Add(item.InstanceId);
        Console.WriteLine();
        Console.WriteLine("New instance: " + item.InstanceId);
        Console.WriteLine("Instance state: " + item.State.Name);
      }

.. topic:: To launch an EC2 instance in a VPC

#. Create and initialize an elastic network interface in a subnet of the VPC.

   .. code-block:: csharp

      string subnetID = "subnet-cb663da2";

      List<string> groups = new List<string>() { mySG.GroupId };
      var eni = new InstanceNetworkInterfaceSpecification()
      {
        DeviceIndex = 0,
        SubnetId = subnetID,
        Groups = groups,
        AssociatePublicIpAddress = true
      };
      List<InstanceNetworkInterfaceSpecification> enis = new List<InstanceNetworkInterfaceSpecification>() {eni};

   :code:`DeviceIndex`
       The index of the device on the instance for the network interface attachment.

   :code:`SubnetId`
       The ID of the subnet where the instance will be launched.

   :code:`GroupIds`
       One or more security groups. For more information, see :ref:`create-security-group`.

   :code:`AssociatePublicIpAddress`
       Indicates whether to auto-assign a public IP address to an instance in a VPC.

#. Create and initialize a :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>`
   object. Be sure the AMI, key pair, and security group you specify exist in the region you
   specified when you created the client object.

   .. code-block:: csharp

       string amiID = "ami-e189c8d1";
       string keyPairName = "my-sample-key";

       var launchRequest = new RunInstancesRequest()
       {
         ImageId = amiID,
         InstanceType = InstanceType.T1Micro,
         MinCount = 1,
         MaxCount = 1,
         KeyName = keyPairName,
         NetworkInterfaces = enis
       };

   :code:`ImageId`
       The ID of the AMI. For a list of public AMIs provided by Amazon, see
       :mktplace-search:`Amazon Machine Images <AMISAWS>`.

   :code:`InstanceType`
       An instance type that is compatible with the specified AMI. For more information, see
       :ec2-ug-win:`Instance Types <instance-types>` in the |EC2-ug-win|.

   :code:`MinCount`
       The minimum number of EC2 instances to launch. If this is more instances than |EC2| can
       launch in the target Availability Zone, |EC2| launches no instances.

   :code:`MaxCount`
       The maximum number of EC2 instances to launch. If this is more instances than |EC2| can
       launch in the target Availability Zone, |EC2| launches the largest possible number of
       instances above :code:`MinCount`. You can launch between 1 and the maximum number of
       instances you're allowed for the instance type. For more information, see
       :ec2-faq:`How many instances can I run in Amazon EC2 <#How_many_instances_can_I_run_in_Amazon_EC2>`
       in the |EC2| General FAQ.

   :code:`KeyName`
       The name of the EC2 key pair. If you launch an instance without specifying a key pair, you
       can't connect to it. For more information, see :ref:`create-key-pair`.

   :code:`NetworkInterfaces`
       One or more network interfaces.

#. (Optional) To launch the instance with an :ref:`IAM role <net-dg-roles>`, specify an |IAM| instance
   profile in the :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>` object.

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

   For example, the following snippet instantiates and configures an
   :sdk-net-api:`IamInstanceProfileSpecification <EC2/TEC2IamInstanceProfileSpecification>` object
   for an IAM role named :code:`winapp-instance-role-1`.

   .. code-block:: csharp

      var instanceProfile = new IamInstanceProfileSpecification();
      instanceProfile.Name  = "winapp-instance-role-1";

   To specify this instance profile in the :sdk-net-api:`RunInstancesRequest <EC2/TEC2RunInstancesRequest>`
   object, add the following line.

   .. code-block:: csharp

      launchRequest.IamInstanceProfile = instanceProfile;

#. Launch the instances by passing the request object to the
   :sdk-net-api:`RunInstances <EC2/MEC2EC2RunInstancesRunInstancesRequest>` method. Save the
   IDs of the instances because you need them to manage the instances.

   Use the returned :sdk-net-api:`RunInstancesResponse <EC2/TEC2RunInstancesResponse>` object
   to get a list of instance IDs for the new instances. The :code:`Reservation.Instances` property
   contains a list of :sdk-net-api:`Instance <EC2/TEC2Instance>` objects, one for each EC2
   instance you successfully launched. You can retrieve the ID for each instance from the
   :code:`InstanceId` property of the :sdk-net-api:`Instance <EC2/TEC2Instance>` object'.

   .. code-block:: csharp

      RunInstancesResponse launchResponse = ec2Client.RunInstances(launchRequest);

      List<String> instanceIds = new List<string>();
      foreach (Instance instance in launchResponse.Reservation.Instances)
      {
        Console.WriteLine(instance.InstanceId);
        instanceIds.Add(instance.InstanceId);
      }


.. _check-instance-state:

Check the State of Your Instance
================================

Use the following procedure to get the current state of your instance. Initially, your instance is
in the :code:`pending` state. You can connect to your instance after it enters the :code:`running`
state.

#. Create and configure a :sdk-net-api:`DescribeInstancesRequest <EC2/TEC2DescribeInstancesRequest>`
   object and assign your instance's instance ID to the :code:`InstanceIds` property. You can also
   use the :code:`Filter` property to limit the request to certain instances, such as instances with a
   particular user-specified tag.

   .. code-block:: csharp

      var instanceRequest = new DescribeInstancesRequest();
      instanceRequest.InstanceIds = new List<string>();
      instanceRequest.InstanceIds.Add(instanceId);

#. Call the :sdk-net-api:`DescribeInstances <EC2/MEC2EC2DescribeInstancesDescribeInstancesRequest>`
   method, and pass it the request object from step 1. The method returns a
   :sdk-net-api:`DescribeInstancesResponse <EC2/TEC2DescribeInstancesResponse>` object that
   contains information about the instance.

   .. code-block:: csharp

      var response = ec2Client.DescribeInstances(instanceRequest);

#. The :code:`DescribeInstancesResponse.Reservations` property contains a list of reservations. In this
   case, there is only one reservation. Each reservation contains a list of :code:`Instance`
   objects. Again, in this case, there is only one instance. You can get the instance's status from
   the :code:`State` property.

   .. code-block:: csharp

      Console.WriteLine(response.Reservations[0].Instances[0].State.Name);


.. _connect-to-instance:

Connect to Your Running Instance
================================

After an instance is running, you can remotely connect to it by using the appropriate remote client.

For Linux instances, use an SSH client. You must ensure that the instance's SSH port (22) is open to
traffic. You will need the instance's public IP address or public DNS name and the private portion
of the key pair used to launch the instance. For more information, see
:ec2-ug:`Connecting to Your Linux Instance <AccessingInstances>` in the |EC2-ug|.

For Windows instances, use an RDP client. You must ensure the instance's RDP port (3389) is open to
traffic. You will need the instance's public IP address or public DNS name and the administrator
password. The administrator password is obtained with the
:sdk-net-api:`GetPasswordData <EC2/MEC2EC2GetPasswordDataGetPasswordDataRequest>` and
:sdk-net-api:`GetPasswordDataResult.GetDecryptedPassword <EC2/MEC2GetPasswordDataResponseGetDecryptedPasswordString>`
methods, which require the private portion of the key pair used to launch the instance. For more
information, see :ec2-ug-win:`Connecting to Your Windows Instance Using RDP <connecting_to_windows_instance>`in the |EC2-ug-win|. The following example demonstrates how to get the password for a Windows instance.

.. code-block:: csharp

    public static string GetWindowsPassword(
      AmazonEC2Client ec2Client,
      string instanceId,
      FileInfo privateKeyFile)
    {
      string password = "";

      var request = new GetPasswordDataRequest();
      request.InstanceId = instanceId;

      var response = ec2Client.GetPasswordData(request);
      if (null != response.PasswordData)
      {
        using (StreamReader sr = new StreamReader(privateKeyFile.FullName))
        {
          string privateKeyData = sr.ReadToEnd();
          password = response.GetDecryptedPassword(privateKeyData);
        }
      }
      else
      {
        Console.WriteLine("The password is not available. The password for " +
          "instance {0} is either not ready, or it is not a Windows instance.",
          instanceId);
      }

      return password;
    }

When you no longer need your EC2 instance, see :ref:`terminate-instance`.



