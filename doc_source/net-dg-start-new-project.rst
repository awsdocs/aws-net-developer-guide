.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-start-new-project:

###################
Start a New Project
###################

The |TVS| includes C# project templates for a variety of AWS services, including the following basic
templates:

AWS Console Project
    A console application that makes basic requests to |S3|, |SDB|, and |EC2|.

AWS Empty Project
    A console application that does not include any code.

AWS Web Project
    An ASP.NET application that makes basic requests to |S3|, |SDB|, and |EC2|.

You can also base your application on one of the standard Visual Studio project templates. You can
add the NuGet packages you need as described in :ref:`net-dg-nuget`, or manually add references to
the AWSSDK assemblies you need, which are located in the :file:`bin\Net45` subdirectory where the
|sdk-net| was installed.

Use the following procedure to get started by creating and running a new AWS Console project for
Visual Studio 2012. The process is similar for other project types and Visual Studio versions. For
more information about how to configure an AWS application:, see :ref:`net-dg-config`.

.. topic:: To start a new project

1. In Visual Studio, on the :guilabel:`File` menu, choose :guilabel:`New`, and then choose :guilabel:`Project` to
   open the :guilabel:`New Project` dialog box.

2. Choose :guilabel:`AWS` from the list of installed templates, and then choose the
   :guilabel:`AWS Console Project` project template. Type a project name, and then click :guilabel:`OK`.

   .. figure:: images/new-proj-dlg-net-dg.png
       :scale: 50
       :alt: AWS template and AWS Console project selected in New Project dialog box

3. Use the :guilabel:`AWS Access Credentials` dialog box to configure your application.

   * Specify the account profile that your code should use to access AWS. To use an existing profile,
     choose :guilabel:`Use existing profile`, and then choose the profile from the list. To add a new
     profile, choose :guilabel:`Use a new profile`, and then type the credentials information. For more
     information about profiles, see :ref:`net-dg-config`.

   * Specify a default AWS Region to use.

   .. figure:: images/creds-new-proj-net-dg.png
       :scale: 50
       :alt: Configure application access account profile, credentials, and default
             AWS Region in AWS Access Credentials dialog

4. Choose :guilabel:`OK` to accept the configuration and open the project. The project's
   :file:`App.config` file will contain something similar to the following.

   .. code-block:: xml

      <configuration>
        <appSettings>
          <add key="AWSProfileName" value="development"/>
          <add key="AWSRegion" value="us-west-1"/>
        </appSettings>
      </configuration>


   The |TVS| puts the values you specified in the :guilabel:`AWS Access Credentials` dialog box
   into the two key-value pairs in :code:`appSettings`.

   .. note:: Although using the :code:`appSettings` element is still supported, we recommend you use the
      :code:`aws` element instead, for example:

      .. code-block:: xml

          <configuration>
            <configSections>
              <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
            </configSections>
            <aws region="us-east-1" profileName="development"/>
          </configuration>

      For more information about the :code:`aws` element, see :ref:`net-dg-config-ref`.

5. Choose :kbd:`F5` to compile and run the application, which prints the number of |EC2| instances, |SDB|
   tables, and |S3| buckets in your account.

For more information about configuring an AWS application, see :ref:`net-dg-config`.
