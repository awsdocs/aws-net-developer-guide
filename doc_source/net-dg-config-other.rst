.. Copyright 2010-2017 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-config-other:

########################################
Configuring Other Application Parameters
########################################

In addition to :ref:`configuring credentials <net-dg-config-creds>`, you can configure a number of
other application parameters:

* :ref:`config-setting-awsendpointdefinition`

* :ref:`config-setting-awslogging`

* :ref:`config-setting-awslogmetrics`

* :ref:`config-setting-awsregion`

* :ref:`config-setting-awsresponselogging`

* :ref:`config-setting-aws-dynamodbcontext-tablenameprefix`

* :ref:`config-setting-aws-s3-usesignatureversion4`

These parameters can be configured in the application's :file:`App.config` or :file:`Web.config` 
file. Although you can also configure these with the |sdk-net| API, we recommend you use the
application's :file:`.config` file. Both approaches are described here.

For more information about use of the :code:`<aws>` element as described later in this topic, see
:ref:`net-dg-config-ref`.

.. _config-setting-awsendpointdefinition:

AWSEndpointDefinition
=====================

    Configures whether the SDK should use a custom configuration file that defines the regions and
    endpoints. 
    
    To set the endpoint definition file in the :file:`.config` file, we recommend setting
    the :code:`endpointDefinition` attribute value in the :code:`<aws>` element. 
    
    .. code-block:: xml

      <aws endpointDefinition="c:\config\endpoints.json"/>
    
    Alternatively, you can set the *AWSEndpointDefinition* key in the :code:`<appSettings>` section: 
    
    .. code-block:: xml

      <add key="AWSEndpointDefinition" value="c:\config\endpoints.json"/> 
    
    Alternatively, to set the endpoint definition file with the |sdk-net| API, set the 
    :sdk-net-api:`AWSConfigs.EndpointDefinition <Amazon/TAWSConfigs>` property: 

    .. code-block:: csharp

      AWSConfigs.EndpointDefinition = @"c:\config\endpoints.json"; 
    
    If no file name is provided, then a custom configuration file will not be used. Changes to this 
    setting take effect only for new AWS client instances. The endpoint.json file is available from 
    https://github.com/aws/aws-sdk-net/blob/master/sdk/src/Core/endpoints.json.

    
.. _config-setting-awslogging:
    
AWSLogging
==========

    Configures how the SDK should log events, if at all. For example, the recommended approach is to
    use the :code:`<logging>` element, which is a child element of the :code:`<aws>` element:
    
    .. code-block:: xml

      <aws> 
        <logging logTo="Log4Net"/> 
      </aws>
    
    Alternatively: 
    
    .. code-block:: xml

      <add key="AWSLogging" value="log4net"/> 
        
    The possible values are: 
    
    :code:`None` 
      Turn off event logging. This is the default. 
    
    :code:`log4net` 
      Log using log4net. 
    
    :code:`SystemDiagnostics` 
      Log using the :classname:`System.Diagnostics` class. 
      
    You can set multiple values for the :code:`logTo` attribute, separated by commas. The following 
    example sets both :code:`log4net` and :code:`System.Diagnostics` logging in the :file:`.config` 
    file: 
    
    .. code-block:: xml

      <logging logTo="Log4Net, SystemDiagnostics"/>
    
    Alternatively: 
    
    .. code-block:: xml

      <add key="AWSLogging" value="log4net, SystemDiagnostics"/> 
    
    Alternatively, using the |sdk-net| API, combine the values of the
    :sdk-net-api:`LoggingOptions <Amazon/TLoggingOptions.html>` enumeration and set the 
    :sdk-net-api:`AWSConfigs.Logging <Amazon/TAWSConfigs>` property: 
    
    .. code-block:: csharp

      AWSConfigs.Logging = LoggingOptions.Log4Net | LoggingOptions.SystemDiagnostics; 
    
    Changes to this setting take effect only for new AWS client instances.

    
.. _config-setting-awslogmetrics:
    
AWSLogMetrics
=============

    Specifies whether or not the SDK should log performance metrics. To set the metrics logging
    configuration in the :file:`.config` file, set the :code:`logMetrics` attribute value in the
    :code:`<logging>` element, which is a child element of the :code:`<aws>` element: 
    
    .. code-block:: xml

      <aws>
        <logging logMetrics="true"/> 
      </aws> 
        
    Alternatively, set the :code:`AWSLogMetrics` key in the :code:`<appSettings>` section: 
     
    .. code-block:: xml

      <add key="AWSLogMetrics" value="true">
        
    Alternatively, to set metrics logging with the |sdk-net| API, set the 
    :sdk-net-api:`AWSConfigs.LogMetrics <Amazon/TAWSConfigs>` property: 
    
    .. code-block:: csharp

      AWSConfigs.LogMetrics = true; 
        
    This setting configures the default :code:`LogMetrics` property for all clients/configs. Changes 
    to this setting take effect only for new AWS client instances.


.. _config-setting-awsregion:

AWSRegion
=========

    Configures the default AWS region for clients that have not explicitly specified a region. To
    set the region in the :file:`.config` file, the recommended approach is to set the
    :code:`region` attribute value in the :code:`aws` element: 
    
    .. code-block:: xml

        <aws region="us-west-2"/> 
        
    Alternatively, set the *AWSRegion* key in the :code:`<appSettings>` section: 
    
    .. code-block:: xml

      <add key="AWSRegion" value="us-west-2"/> 
        
    Alternatively, to set the region with the |sdk-net| API, set the 
    :sdk-net-api:`AWSConfigs.AWSRegion <Amazon/TAWSConfigs>` property: 
    
    .. code-block:: csharp

      AWSConfigs.AWSRegion = "us-west-2"; 
        
    For more information about creating an AWS client for a specific region, see 
    :ref:`net-dg-region-selection`. Changes to this setting take effect only for new AWS client 
    instances.


.. _config-setting-awsresponselogging:

AWSResponseLogging
==================

    Configures when the SDK should log service responses. The possible values are: 
    
    :code:`Never`
      Never log service responses. This is the default. 
      
    :code:`Always` 
      Always log service responses.
      
    :code:`OnError` 
      Only log service responses when an error occurs. 
    
    To set the service logging
    configuration in the :file:`.config` file, the recommended approach is to set the
    :code:`logResponses` attribute value in the :code:`<logging>` element, which is a child element
    of the :code:`<aws>` element: 
    
    .. code-block:: xml

      <aws> 
        <logging logResponses="OnError"/> 
      </aws>
    
    Alternatively, set the *AWSResponseLogging* key in the :code:`<appSettings>`
    section: 
    
    .. code-block:: xml
       
      <add key="AWSResponseLogging" value="OnError"/> 
      
    Alternatively, to set service logging with the |sdk-net| API, set the 
    :sdk-net-api:`AWSConfigs.ResponseLogging <Amazon/TAWSConfigs>` 
    property to one of the values of the 
    :sdk-net-api:`ResponseLoggingOption <Amazon/TResponseLoggingOption>` enumeration:
    
    .. code-block:: csharp

      AWSConfigs.ResponseLogging = ResponseLoggingOption.OnError; 
        
    Changes to this setting take effect immediately.

    
    
.. _config-setting-aws-dynamodbcontext-tablenameprefix:

AWS.DynamoDBContext.TableNamePrefix
===================================

    Configures the default :code:`TableNamePrefix` the :code:`DynamoDBContext` will use if not
    manually configured. 
    
    To set the table name prefix in the :file:`.config` file, the recommended
    approach is to set the :code:`tableNamePrefix` attribute value in the :code:`<dynamoDBContext>` 
    element, which is a child element of the :code:`<dynamoDB>` element, which itself is a child
    element of the :code:`<aws>` element: 
    
    .. code-block:: xml

      <dynamoDBContext tableNamePrefix="Test-"/>
        
    Alternatively, set the :code:`AWS.DynamoDBContext.TableNamePrefix` key in the
    :code:`<appSettings>` section: 
    
    .. code-block:: xml

      <add key="AWS.DynamoDBContext.TableNamePrefix" value="Test-"/>
        
    Alternatively, to set the table name prefix with the |sdk-net| API, set the
    :sdk-net-api:`AWSConfigs.DynamoDBContextTableNamePrefix <Amazon/TAWSConfigs>` property:
    
    .. code-block:: csharp
    
      AWSConfigs.DynamoDBContextTableNamePrefix = "Test-"; 
      
    Changes to this setting will take effect
    only in newly constructed instances of :code:`DynamoDBContextConfig` and
    :code:`DynamoDBContext`.

    
.. _config-setting-aws-s3-usesignatureversion4:

AWS.S3.UseSignatureVersion4
===========================

    Configures whether or not the |S3| client should use signature version 4 signing with requests.
    
    To set signature version 4 signing for |S3| in the :file:`.config` file, the recommended
    approach is to set the :code:`useSignatureVersion4` attribute of the :code:`<s3>` element, which
    is a child element of the :code:`<aws>` element: 
    
    .. code-block:: xml

      <aws> 
        <s3 useSignatureVersion4="true"/> 
      </aws>
        
    Alternatively, set the *AWS.S3.UseSignatureVersion4* key to *true* in the :code:`<appSettings>` 
    section: 
    
    .. code-block:: xml

      <add key="AWS.S3.UseSignatureVersion4" value="true"/> 
        
    Alternatively, to set signature version 4 signing with the |sdk-net| API, set the 
    :sdk-net-api:`AWSConfigs.S3UseSignatureVersion4 <Amazon/TAWSConfigs>` 
    property to :code:`true`: 
    
    .. code-block:: csharp

      AWSConfigs.S3UseSignatureVersion4 = true; 
        
    By default, this setting is :code:`false`, but signature version 4 may be used by default in some 
    cases or with some regions. When the setting is :code:`true`, signature version 4 will be used 
    for all requests. Changes to this setting take effect only for new |S3| client instances.
