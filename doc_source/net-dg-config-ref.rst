.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-config-ref:

###########################################
Configuration Files Reference for |sdk-net|
###########################################

You can use a .NET project's :file:`App.config` or :file:`Web.config` file to specify AWS settings,
such as AWS credentials, logging options, AWS service endpoints, and AWS regions, as well as some
settings for AWS services, such as |DDBlong|, |EC2|, and |S3|. The following information describes
how to properly format an :file:`App.config` or :file:`Web.config` file to specify these types of
settings.

.. note:: Although you can continue to use the :code:`<appSettings>` element in an
   :file:`App.config` or :file:`Web.config` file to specify AWS settings, we recommend you use the
   :code:`<configSections>` and :code:`<aws>` elements as described later in this topic. For more
   information about the :code:`<appSettings>` element, see the :code:`<appSettings>` element
   examples in :ref:`net-dg-config`.


.. note:: Although you can continue to use the following :sdk-net-api:`AWSConfigs <Amazon/TAWSConfigs>` 
   class properties in a code file to specify AWS settings, the following
   properties are deprecated and may not be supported in future releases:

    * :code:`DynamoDBContextTableNamePrefix`

    * :code:`EC2UseSignatureVersion4`

    * :code:`LoggingOptions`

    * :code:`LogMetrics`

    * :code:`ResponseLoggingOption`

    * :code:`S3UseSignatureVersion4`

   In general, we recommend that instead of using :classname:`AWSConfigs` class properties in a
   code file to specify AWS settings, you should use the :code:`<configSections>` and :code:`<aws>`
   elements in an :file:`App.config` or :file:`Web.config` file to specify AWS settings, as
   described later in this topic. For more information about the preceding properties, see the
   :classname:`AWSConfigs` code examples in :ref:`net-dg-config`.

.. contents:: **Topics**
    :local:
    :depth: 1

.. _net-dg-config-ref-declaring:

Declaring an AWS Settings Section
=================================

You specify AWS settings in an :file:`App.config` or :file:`Web.config` file from within the
:code:`<aws>` element. Before you can begin using the :code:`<aws>` element, you must create a
:code:`<section>` element (which is a child element of the :code:`<configSections>` element) and set
its :code:`name` attribute to :code:`aws` and its :code:`type` attribute to
:code:`Amazon.AWSSection, AWSSDK.Core`, as shown in the following example:

.. code-block:: xml

    <?xml version="1.0"?>
    <configuration>
      ...
      <configSections>
        <section name="aws" type="Amazon.AWSSection, AWSSDK.Core"/>
      </configSections>
      <aws>
        <!-- Add your desired AWS settings declarations here. -->
      </aws>
      ...
    </configuration>

The Visual Studio Editor does not provide automatic code completion (IntelliSense) for the
:code:`<aws>` element or its child elements.

To assist you in creating a correctly formatted version of the :code:`<aws>` element, call the
:code:`Amazon.AWSConfigs.GenerateConfigTemplate` method. This outputs a canonical version of the
:code:`<aws>` element as a pretty-printed string, which you can adapt to your needs. The following
sections describe the :code:`<aws>` element's attributes and child elements.


.. _net-dg-config-ref-elements:

Allowed Elements
================

The following is a list of the logical relationships among the allowed elements in an AWS settings
section. You can generate the latest version of this list by calling the
:code:`Amazon.AWSConfigs.GenerateConfigTemplate` method, which outputs a canonical version of the
:code:`<aws>` element as a string you can adapt to your needs.

.. code-block:: xml

    <aws
      endpointDefinition="string value"
      region="string value"
      profileName="string value"
      profilesLocation="string value">
      <logging
        logTo="None, Log4Net, SystemDiagnostics"
        logResponses="Never | OnError | Always"
        logMetrics="true | false"
        logMetricsFormat="Standard | JSON"
        logMetricsCustomFormatter="NameSpace.Class, Assembly" />
      <dynamoDB
        conversionSchema="V1 | V2">
        <dynamoDBContext
          tableNamePrefix="string value">
          <tableAliases>
            <alias
              fromTable="string value"
              toTable="string value" />
          </tableAliases>
          <map
            type="NameSpace.Class, Assembly"
            targetTable="string value">
            <property
              name="string value"
              attribute="string value"
              ignore="true | false"
              version="true | false"
              converter="NameSpace.Class, Assembly" />
          </map>
        </dynamoDBContext>
      </dynamoDB>
      <s3
        useSignatureVersion4="true | false" />
      <ec2
        useSignatureVersion4="true | false" />
      <proxy
        host="string value"
        port="1234"
        username="string value"
        password="string value" />
    </aws>


.. _net-dg-config-ref-elements-ref:

Elements Reference
==================

The following is a list of the elements that are allowed in an AWS settings section. For each
element, its allowed attributes and parent-child elements are listed.

.. contents::
    :local:
    :depth: 1


.. _net-dg-config-ref-elements-alias:

alias
-----

The :code:`<alias>` element represents a single item in a collection of one or more from-table to
to-table mappings that specifies a different table than one configured for a type. This element
maps to an instance of the :code:`Amazon.Util.TableAlias` class from the
:code:`Amazon.AWSConfigs.DynamoDBConfig.Context.TableAliases` property in the |sdk-net|. Remapping
is done before applying a table name prefix.

This element can include the following attributes:

:code:`fromTable`
    The from-table portion of the from-table to to-table mapping. This attribute maps to the
    :code:`Amazon.Util.TableAlias.FromTable` property in the |sdk-net|.

:code:`toTable`
    The to-table portion of the from-table to to-table mapping. This attribute maps to the
    :code:`Amazon.Util.TableAlias.ToTable` property in the |sdk-net|.

The parent of the :code:`<alias>` element is the :code:`<tableAliases>` element.

The :code:`<alias>` element contains no child elements.

The following is an example of the :code:`<alias>` element in use:

.. code-block:: xml

    <alias
      fromTable="Studio"
      toTable="Studios" />


.. _net-dg-config-ref-elements-aws:

aws
---

The :code:`<aws>` element represents the top-most element in an AWS settings section. This element
can include the following attributes:

:code:`endpointDefinition`
    The absolute path to a custom configuration file that defines the AWS regions and endpoints to
    use. This attribute maps to the :code:`Amazon.AWSConfigs.EndpointDefinition` property in the
    |sdk-net|.

:code:`profileName`
    The profile name for stored AWS credentials that will be used to make service calls. This
    attribute maps to the :code:`Amazon.AWSConfigs.AWSProfileName` property in the |sdk-net|.

:code:`profilesLocation`
    The absolute path to the location of the credentials file shared with other AWS SDKs. By
    default, the credentials file is stored in the :file:`.aws` directory in the current user's home
    directory. This attribute maps to the :code:`Amazon.AWSConfigs.AWSProfilesLocation` property in
    the |sdk-net|.

:code:`region`
    The default AWS region ID for clients that have not explicitly specified a region. This
    attribute maps to the :code:`Amazon.AWSConfigs.AWSRegion` property in the |sdk-net|.

The :code:`<aws>` element has no parent element.

The :code:`<aws>` element can include the following child elements:

* :code:`<dynamoDB>`

* :code:`<ec2>`

* :code:`<logging>`

* :code:`<proxy>`

* :code:`<s3>`

The following is an example of the :code:`<aws>` element in use:

.. code-block:: xml

    <aws
      endpointDefinition="C:\Configs\endpoints.xml"
      region="us-west-2"
      profileName="development"
      profilesLocation="C:\Configs">
      <!-- ... -->
    </aws>


.. _net-dg-config-ref-elements-dynamoDB:

dynamoDB
--------

The :code:`<dynamoDB>` element represents a collection of settings for Amazon DynamoDB. This element can
include the *conversionSchema* attribute, which represents the version to use for converting between
.NET and DynamoDB objects. Allowed values include V1 and V2. This attribute maps to the
:code:`Amazon.DynamoDBv2.DynamoDBEntryConversion` class in the |sdk-net|. For more information, see
:aws-blogs-net:`DynamoDB Series - Conversion Schemas
<Tx2TCOGWG7ARUH5/DynamoDB-Series-Conversion-Schemas>`.

The parent of the :code:`<dynamoDB>` element is the :code:`<aws>` element.

The :code:`<dynamoDB>` element can include the :code:`<dynamoDBContext>` child element.

The following is an example of the :code:`<dynamoDB>` element in use:

.. code-block:: xml

    <dynamoDB
      conversionSchema="V2">
      <!-- ... -->
    </dynamoDB>


.. _net-dg-config-ref-elements-ddbcontext:

dynamoDBContext
---------------

The :code:`<dynamoDBContext>` element represents a collection of Amazon DynamoDB context-specific settings.
This element can include the *tableNamePrefix* attribute, which represents the default table name
prefix the DynamoDB context will use if it is not manually configured. This attribute maps to the
:code:`Amazon.Util.DynamoDBContextConfig.TableNamePrefix` property from the
:code:`Amazon.AWSConfigs.DynamoDBConfig.Context.TableNamePrefix` property in the |sdk-net|.
For more information, see
:aws-blogs-net:`Enhancements to the DynamoDB SDK <Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK>`.

The parent of the :code:`<dynamoDBContext>` element is the :code:`<dynamoDB>` element.

The :code:`<dynamoDBContext>`  element can include the following child elements:

*  :code:`<alias>` (one or more instances)

*  :code:`<map>` (one or more instances)

The following is an example of the :code:`<dynamoDBContext>` element in use:

.. code-block:: xml

    <dynamoDBContext
      tableNamePrefix="Test-">
      <!-- ... -->
    </dynamoDBContext>


.. _net-dg-config-ref-elements-ec2:

ec2
---

The :code:`<ec2>` element represents a collection of Amazon EC2 settings. This element can include the
*useSignatureVersion4* attribute, which specifies whether signature version 4 signing will be used for
all requests (true) or whether signature version 4 signing will not be used for all requests (false,
the default). This attribute maps to the :code:`Amazon.Util.EC2Config.UseSignatureVersion4`
property from the :code:`Amazon.AWSConfigs.EC2Config.UseSignatureVersion4` property in the |sdk-net|.

The parent of the :code:`<ec2>` element is the element.

The :code:`<ec2>` element contains no child elements.

The following is an example of the :code:`<ec2>` element in use:

.. code-block:: xml

    <ec2
      useSignatureVersion4="true" />


.. _net-dg-config-ref-elements-logging:

logging
-------

The :code:`<logging>` element represents a collection of settings for response logging and performance
metrics logging. This element can include the following attributes:

:code:`logMetrics`
    Whether performance metrics will be logged for all clients and configurations (true); otherwise,
    false. This attribute maps to the :code:`Amazon.Util.LoggingConfig.LogMetrics` property from the
    :code:`Amazon.AWSConfigs.LoggingConfig.LogMetrics` property in the |sdk-net|.

:code:`logMetricsCustomFormatter`
    The data type and assembly name of a custom formatter for logging metrics. This attribute maps
    to the :code:`Amazon.Util.LoggingConfig.LogMetricsCustomFormatter` property from the
    :code:`Amazon.AWSConfigs.LoggingConfig.LogMetricsCustomFormatter` property in the |sdk-net|.

:code:`logMetricsFormat`
    The format in which the logging metrics are presented (maps to the
    :code:`Amazon.Util.LoggingConfig.LogMetricsFormat` property from the
    :code:`Amazon.AWSConfigs.LoggingConfig.LogMetricsFormat` property in the |sdk-net|).

    Allowed values include:

    :code:`JSON`
        Use JSON format.

    :code:`Standard`
        Use the default format.


:code:`logResponses`
    When to log service responses (maps to the :code:`Amazon.Util.LoggingConfig.LogResponses`
    property from the :code:`Amazon.AWSConfigs.LoggingConfig.LogResponses` property in the
    |sdk-net|).

    Allowed values include:

    :code:`Always`
        Always log service responses.

    :code:`Never`
         Never log service responses.

    :code:`OnError`
        Only log service responses when there are errors.


:code:`logTo`
    Where to log to (maps to the :code:`LogTo` property from the
    :code:`Amazon.AWSConfigs.LoggingConfig.LogTo` property in the |sdk-net|).

    Allowed values include one or more of:

    :code:`Log4Net`
        Log to log4net.

    :code:`None`
        Disable logging.

    :code:`SystemDiagnostics`
        Log to System.Diagnostics.


The parent of the :code:`<logging>` element is the :code:`<aws>` element.

The :code:`<logging>` element contains no child elements.

The following is an example of the :code:`<logging>` element in use:

.. code-block:: xml

    <logging
      logTo="SystemDiagnostics"
      logResponses="OnError"
      logMetrics="true"
      logMetricsFormat="JSON"
      logMetricsCustomFormatter="MyLib.Util.MyMetricsFormatter, MyLib" />


.. _net-dg-config-ref-elements-map:

map
---

The :code:`<map>` element represents a single item in a collection of type-to-table mappings from
.NET types to DynamoDB tables (maps to an instance of the :code:`TypeMapping` class from the
:code:`Amazon.AWSConfigs.DynamoDBConfig.Context.TypeMappings` property in the |sdk-net|). For more
information, see :aws-blogs-net:`Enhancements to the DynamoDB SDK
<Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK>`.

This element can include the following attributes:

:code:`targetTable`
    The DynamoDB table to which the mapping applies. This attribute maps to the
    :code:`Amazon.Util.TypeMapping.TargetTable` property in the |sdk-net|.

:code:`type`
    The type and assembly name to which the mapping applies. This attribute maps to the
    :code:`Amazon.Util.TypeMapping.Type` property in the |sdk-net|.

The parent of the :code:`<map>` element is the :code:`<dynamoDBContext>` element.

The :code:`<map>` element can include one or more instances of the :code:`<property>` child element.

The following is an example of the :code:`<map>` element in use:

.. code-block:: xml

    <map
      type="SampleApp.Models.Movie, SampleDLL"
      targetTable="Movies">
      <!-- ... -->
    </map>


.. _net-dg-config-ref-elements-property:

property
--------

The :code:`<property>` element represents a DynamoDB property. (This element maps to an instance of
the Amazon.Util.PropertyConfig class from the :code:`AddProperty` method in the |sdk-net|) For more
information, see :aws-blogs-net:`Enhancements to the DynamoDB SDK 
<Tx2C4MHH2H0SA5W/Enhancements-to-the-DynamoDB-SDK>` and :ddb-dg:`DynamoDB Attributes 
<DeclarativeTagsList>`.

This element can include the following attributes:

:code:`attribute`
    The name of an attribute for the property, such as the name of a range key. This attribute maps
    to the :code:`Amazon.Util.PropertyConfig.Attribute` property in the |sdk-net|.

:code:`converter`
    The type of converter that should be used for this property. This attribute maps to the
    :code:`Amazon.Util.PropertyConfig.Converter` property in the |sdk-net|.

:code:`ignore`
    Whether the associated property should be ignored (true); otherwise, false. This attribute maps
    to the :code:`Amazon.Util.PropertyConfig.Ignore` property in the |sdk-net|.

:code:`name`
    The name of the property. This attribute maps to the :code:`Amazon.Util.PropertyConfig.Name` property
    in the |sdk-net|.

:code:`version`
    Whether this property should store the item version number (true); otherwise, false. This
    attribute maps to the :code:`Amazon.Util.PropertyConfig.Version` property in the |sdk-net|.

The parent of the :code:`<property>` element is the :code:`<map>` element.

The :code:`<property>` element contains no child elements.

The following is an example of the :code:`<property>` element in use:

.. code-block:: xml

    <property
      name="Rating"
      converter="SampleApp.Models.RatingConverter, SampleDLL" />


.. _net-dg-config-ref-elements-proxy:

proxy
-----

The :code:`<proxy>` element represents settings for configuring a proxy for the |sdk-net| to use.
This element can include the following attributes:

host
   The host name or IP address of the proxy server. This attributes maps to the
   :code:`Amazon.Util.ProxyConfig.Host` property from the :code:`Amazon.AWSConfigs.ProxyConfig.Host`
   property in the |sdk-net|.

password
   The password to authenticate with the proxy server. This attributes maps to the
   :code:`Amazon.Util.ProxyConfig.Password` property from the
   :code:`Amazon.AWSConfigs.ProxyConfig.Password` property in the |sdk-net|.

port
   The port number of the proxy. This attributes maps to the :code:`Amazon.Util.ProxyConfig.Port`
   property from the :code:`Amazon.AWSConfigs.ProxyConfig.Port` property in the |sdk-net|.

username
    The user name to authenticate with the proxy server. This attributes maps to the
    :code:`Amazon.Util.ProxyConfig.Username` property from the
    :code:`Amazon.AWSConfigs.ProxyConfig.Username` property in the |sdk-net|.

The parent of the :code:`<proxy>` element is the :code:`<aws>` element.

The :code:`<proxy>` element contains no child elements.

The following is an example of the :code:`<proxy>` element in use:

.. code-block:: xml

    <proxy
      host="192.0.2.0"
      port="1234"
      username="My-Username-Here"
      password="My-Password-Here" />


.. _net-dg-config-ref-elements-s3:

s3
---

The :code:`<s3>` element represents a collection of Amazon S3 settings. This element can include the
*useSignatureVersion4* attribute, which specifies whether signature version 4 signing will be used for
all requests (true) or whether signature version 4 signing will not be used for all requests (false,
the default). This attribute maps to the :code:`Amazon.AWSConfigs.S3Config.UseSignatureVersion4`
property in the |sdk-net|.

The parent of the :code:`<s3>` element is the :code:`<aws>` element.

The :code:`<s3>` element contains no child elements.

The following is an example of the :code:`<s3>` element in use:

.. code-block:: xml

    <s3 useSignatureVersion4="true" />

