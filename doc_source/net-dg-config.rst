.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-config:

######################################
Configuring Your |sdk-net| Application
######################################

You can configure your |sdk-net| application to specify AWS credentials, logging options, endpoints,
or signature version 4 support with |S3|.

The recommended way to configure an application is to use the :code:`<aws>` element in the project's
:file:`App.config` or :file:`Web.config` file. The following example specifies the
:ref:`config-setting-awsregion` and :ref:`config-setting-awslogging` parameters.

.. code-block:: xml

    <configuration>
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws region="us-west-2">
        <logging logTo="Log4Net"/>
      </aws>
    </configuration>

Another way to configure an application is to edit the :code:`<appSettings>` element in the
project's :file:`App.config` or :file:`Web.config` file. The following example specifies the
:ref:`config-setting-awsregion` and :ref:`config-setting-awslogging` parameters.

.. code-block:: xml

    <configuration>
      <appSettings>
        <add key="AWSRegion" value="us-west-2"/>
        <add key="AWSLogging" value="log4net"/>
      </appSettings>
    </configuration>

These settings take effect only after the application has been rebuilt.

Although you can configure an |sdk-net| application programmatically by setting property values in
the :sdk-net-api:`AWSConfigs <Amazon/TAWSConfigs>` class, we recommend you use the :code:`aws`
element instead. The following example specifies the :ref:`config-setting-awsregion` and
:ref:`config-setting-awslogging` parameters:

.. code-block:: csharp

    AWSConfigs.AWSRegion = "us-west-2";
    AWSConfigs.Logging = LoggingOptions.Log4Net;

Programmatically defined parameters override any values that were specified in an :file:`App.config`
or :file:`Web.config` file. Some programmatically defined parameter values take effect immediately;
others take effect only after you create a new client object. For more information, see
:ref:`net-dg-config-creds`.

.. toctree::
    :hidden:
    :maxdepth: 1

    net-dg-config-netcore
    net-dg-config-creds
    net-dg-region-selection
    net-dg-config-other
    net-dg-config-ref

