.. Copyright 2010-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-config-netcore:

#########################################
Configuring the |sdk-net| with |net-core|
#########################################

One of the biggest changes in |net-core| is the removal of :code:`ConfigurationManager` and the standard 
:file:`app.config` and :file:`web.config` files that were used ubiquitously with .NET Framework and 
ASP.NET applications. For traditional .NET applications, the |sdk-net| uses this configuration 
system to set things like AWS credentials and region so that you don't have to do this in code.

The configuration system in |net-core| allows any type of input source from any location. Also, the 
configuration object isn't a global singleton like the ConfigurationManager in standard .NET 
applications, so the |sdk-net| doesn't have access to read settings from it.

.. note:: For background on the |net-core| configuration system, read the 
   `Configuration <https://docs.asp.net/en/latest/fundamentals/configuration.html>`_ topic in the 
   |net-core| documentation.

To make it easy to use the |sdk-net| with |net-core|, you can use the 
:code:`AWSSDK.Extensions.NETCore.Setup` NuGet package. Like many |net-core| libraries, it adds 
extension methods to the :code:`IConfiguration` interface to make getting the AWS configuration 
seamless.

.. _net-core-configuration-builder:

Using AWSSDK.Extensions.NETCore.Setup
======================================

When you create an ASP.NET Core MVC application in Visual Studio, the constructor for :file:`Startup.cs` 
handles configuration by reading in various input sources, using the :code:`ConfigurationBuilder` 
and setting the :code:`Configuration` property to the built :code:`IConfiguration` object. 

.. literalinclude:: how-to/net-core/configurationBuilder.cs
   :language: csharp

To use the :code:`Configuration` object to get the AWS options, first add the 
:code:`AWSSDK.Extensions.NETCore.Setup` NuGet package. Then, add your options to the configuration 
file. Notice one of the files added to the :code:`ConfigurationBuilder` is called 
:code:`$"appsettings.{env.EnvironmentName}.json"`. If you look at the Debug tab in your project's 
properties, you can see this file is set to **Development**. This works great for local testing 
because you can put your configuration in the :file:`appsettings.Development.json` file, which is 
read-only during local testing. When you deploy an |EC2| instance that has :code:`EnvironmentName` 
set to **Production**, this file is ignored and the |sdk-net| falls back to the IAM credentials 
and region configured for the |EC2| instance.

The configuration below shows an example of the values you can add in the 
:file:`appsettings.Development.json` file in your project to supply AWS settings. 

.. literalinclude:: how-to/net-core/appsettings-development.json  
   :language: json
   
To access the AWS options set in the file from code, call the :code:`GetAWSOptions` extension method 
added on :code:`IConfiguration`. To construct a service client from these options, call 
:code:`CreateServiceClient`. The following example code shows how to create an |S3| service client. 

.. literalinclude:: how-to/net-core/create-s3-client.cs
   :language: csharp

.. _net-core-appsettings-values:

Allowed Values in appsettings File
----------------------------------

The following app configuration values can be set in the :file:`appsettings.Development.json` file. 
The field names must use the casing shown in the list below. For details on these settings, refer to 
the :sdk-net-api:`AWS.Runtime.ClientConfg <Runtime/TRuntimeClientConfig>` class.

* Region
* Profile
* ProfilesLocation
* SignatureVersion
* RegionEndpoint
* UseHttp
* ServiceURL
* AuthenticationRegion
* AuthenticationServiceName
* MaxErrorRetry
* LogResponse
* BufferSize
* ProgressUpdateInterval
* ResignRetries
* AllowAutoRedirect
* LogMetrics
* DisableLogging
* UseDualstackEndpoint

   
.. _net-core-dependency-injection:

ASP.NET Core Dependency Injection 
=================================

The *AWSSDK.Extensions.NETCore.Setup* NuGet package also integrates with a new dependency injection 
system in ASP.NET Core. The :code:`ConfigureServices` method in :code:`Startup` is where the MVC 
services are added. If the application is using Entity Framework, this is also where that is 
initialized. 

.. literalinclude:: how-to/net-core/configureServices.cs
   :language: csharp
   
.. note:: Background on dependency injection in |net-core| is available on the |net-core| 
   `documentation site <https://docs.asp.net/en/latest/fundamentals/dependency-injection.html>`_. 

The :code:`AWSSDK.Extensions.NETCore.Setup` NuGet package adds new extension methods to 
:code:`IServiceCollection` that you can use to add AWS services to the dependency injection. The 
following code shows how to add the AWS options that are read from :code:`IConfiguration` to add 
|S3| and |DDB| to our list of services. 

.. literalinclude:: how-to/net-core/configureAwsServices.cs
   :language: csharp
   
Now, if your MVC controllers use either :code:`IAmazonS3` or :code:`IAmazonDynamoDB` as parameters 
in their constructors, the dependency injection system passes in those services. 
   
.. literalinclude:: how-to/net-core/homeController.cs
   :language: csharp
   
   
