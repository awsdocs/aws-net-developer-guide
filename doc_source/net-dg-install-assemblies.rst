.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-install-assemblies:

#########################
Install AWSSDK Assemblies
#########################

You can install the AWSSDK assemblies by installing the |sdk-net| or by installing
the AWS assemblies with NuGet.

.. _net-dg-install-net-sdk:

Installing the |sdk-net|
========================

The following procedure describes how to install the |TFW|, which contains the |sdk-net|.

.. tip:: The |sdk-net| is also available on `GitHub <https://github.com/aws/aws-sdk-net>`_.

To install |TFW|

1. Go to |sdk-net|_.

2. In the :guilabel:`Downloads` section, choose :guilabel:`AWS SDK for .NET` to download the installer.

3. To start installation, run the downloaded installer and follow the on-screen
   instructions.

    .. tip:: By default, |TFW| are installed in the Program Files directory, which requires administrator
       privileges. To install |TFW| as a non-administrator, choose a different installation
       directory.

4. (Optional) You can use NuGet to install individual AWSSDK service assemblies and extensions for
   the |sdk-net|, which include a session state provider and a trace listener. For more information,
   see :ref:`net-dg-nuget`.


.. _net-dg-nuget:

Installing AWSSDK Assemblies with NuGet
=======================================

`NuGet <http://nuget.org/>`_ is a package management system for the .NET platform. With NuGet, you
can add the AWSSDK assemblies, as well as the
`TraceListener <http://www.nuget.org/packages/AWS.TraceListener>`_ and
`SessionProvider <http://www.nuget.org/packages/AWS.SessionProvider>`_ extensions, to your
application.

NuGet always has the most recent versions of the AWSSDK assemblies, and also enables you to install
previous versions. NuGet is aware of dependencies between assemblies and installs all required
assemblies automatically. Assemblies installed with NuGet are stored with your solution instead of
in a central location, such as in the Program Files directory. This enables you to install assembly
versions specific to a given application without creating compatibility issues for other applications.
For more information about NuGet, see the `NuGet documentation <http://docs.nuget.org/>`_.

NuGet is installed automatically with Visual Studio 2010 or later.
If you are using an earlier version of Visual Studio, you can install NuGet from the
`Visual Studio Gallery on MSDN
<http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c>`_.

You can use NuGet from :guilabel:`Solution Explorer` or from the Package Manager
Console.

NuGet AWSSDK Packages
---------------------

The NuGet website provides a page for every package available through NuGet. The page for each
package includes a sample command line for installing the package using the Package Manager Console.
Each page also includes a list of the previous versions of the package that are available through
NuGet. For a list of AWSSDK packages available from NuGet, see `AWSSDK Packages
<http://www.nuget.org/profiles/awsdotnet>`_.


.. _package-install-gui:

Using NuGet from Solution Explorer
----------------------------------

**To use NuGet from Solution Explorer**

#. In :guilabel:`Solution Explorer`, right-click your project, and then choose :guilabel:`Manage NuGet
   Packages` from the context menu.

#. In the left pane of the :guilabel:`Manage NuGet Packages` dialog box, choose :guilabel:`Online`.
   You can then use the search box in the top-right corner to search for the package you want to
   install.

   The following figure shows the :guilabel:`AWSSDK - Core Runtime` assembly package. You can see
   NuGet is aware that this package has a dependency on the :code:`AWSSDK.Core` assembly package;
   NuGet automatically installs the :code:`AWSSDK.Core` package, if it is not already installed.

   .. figure:: images/nuget-install-vs-dlg.png
      :scale: 65
      :alt: AWSSDK Core Runtime package and dependency on :code:`AWSSDK.Core`
            assembly shown in Manage NuGet Packages dialog


.. _package-install-cmd:

Using NuGet from the Package Manager Console
--------------------------------------------

**To use NuGet from the Package Manager Console in Visual Studio**

* *Visual Studio 2010*
      From the :guilabel:`Tools` menu, choose :guilabel:`Library Package Manager`,
      and then click :guilabel:`Package Manager Console`.

* *Visual Studio 2012 and later*
      From the :guilabel:`Tools` menu, choose :guilabel:`Nuget Package
      Manager`, and then click :guilabel:`Package Manager Console`.

You can install the AWSSDK assemblies you want from the Package Manager Console by using the
:command:`Install-Package` command. For example, to install the `AWSSDK.AutoScaling
<http://www.nuget.org/packages/AWSSDK.AutoScaling>`_ assembly, use the following command.

.. code-block:: sh

    PM> Install-Package AWSSDK.AutoScaling

NuGet also installs any dependencies, such as `AWSSDK.Core
<http://www.nuget.org/packages/AWSSDK.Core>`_.

To install an earlier version of a package, use the :code:`-Version` option and specify the
package version you want. For example, to install version 3.1.0.0 of the |sdk-net| assembly, use the
following command line.

.. code-block:: sh

    PM> Install-Package AWSSDK.Core -Version 3.1.0.0

For more information about Package Manager Console commands, see
`Package Manager Console Commands (v1.3)
<http://nuget.codeplex.com/wikipage?title=Package%20Manager%20Console%20Command%20Reference%20%28v1.3%29>`_.

